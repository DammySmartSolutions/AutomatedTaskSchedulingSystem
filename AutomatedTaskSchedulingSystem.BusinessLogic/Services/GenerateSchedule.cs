using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services
{
    public class GenerateSchedule
    {


        private readonly ATSSEntities _db;

        ATSSUtilityClass Utility = new ATSSUtilityClass();
        //public GenerateSchedule()
        //{
        //    _db = new ATSSEntities();
        //}


        public GenerateSchedule(ATSSEntities db = null)
        {
            _db = db ?? new ATSSEntities();
        }








        public string GenerateTaskSchedule(DateTime scheduleDate)
        {
            // 1. Check if schedule already exists for the date
            if (_db.tblSchedule.Any(s => s.SchDate == scheduleDate))
            {

                var tasksForDate = _db.tblSchedule.Where(s => s.SchDate == scheduleDate).ToList();

                if (tasksForDate.Any())
                {
                    _db.tblSchedule.RemoveRange(tasksForDate);
                    _db.SaveChanges();
                }

            }

            // 2. Load available employees for the day
            var availableEmployees = (from emp in _db.tblEmployee
                                      join avail in _db.tblEmployeeAvail on emp.EmpID equals avail.EmpID
                                      where emp.Position == "Cargo Handler" && avail.AvailDate == scheduleDate && avail.Avail == true
                                      select new
                                      {
                                          emp.EmpID,
                                          emp.FullName,
                                          emp.Sex
                                      }).ToList();

            
            
                                        if (!availableEmployees.Any())
                                        {

                                            return "No employee available";
                                        }


            // Shuffle employees to improve distribution
            availableEmployees = availableEmployees.OrderBy(e => Guid.NewGuid()).ToList();

            // 3. Get all tasks joined with their locations
            var tasks = (from task in _db.tblSetupTask
                         join loc in _db.tblSetupLoc on task.LocID equals loc.LocID
                         orderby task.TaskID
                         select new
                         {
                             task.Task,
                             Location = loc.Location,
                             task.MinEmployees,
                             task.MaxEmployees
                         }).ToList();



                        if (!tasks.Any())
                        {

                            return "No Task Setup";
                 
            
            }

            // 4. Define equivalent task pairs
            var equivalentTasks = new Dictionary<string, string>
    {
        {"Trk Unloader", "Trailer Unloader"},
        {"Splitter Express", "Splitter Ground"},
        {"Smalls Sorter", "Shuttle Tls"},
        {"Smalls P-Scanner", "Ib Scanner"},
        {"Tls Express Small", "Trk Loader"},
        {"Cpost Express", "Cpost Ground"},
        {"Van Load Scanner Express", "Van Load Scanner Ground"},
        {"Van Loader Express", "Van Loader Ground"},
        {"P-Scanner", "Status Scan"},
        {"Tls  Scanner Express", "Tls Ground"},
        {"Express Floor Pallet Load", "Floor Pallet Load Ground"},
    };

            // 5. Determine previous schedule date if any
            var previousDate = _db.tblSchedule
                                  .Where(s => s.SchDate < scheduleDate)
                                  .OrderByDescending(s => s.SchDate)
                                  .Select(s => s.SchDate)
                                  .FirstOrDefault();

            var previousAssignments = _db.tblSchedule
                                         .Where(s => s.SchDate == previousDate)
                                         .AsEnumerable()
                                         .SelectMany(s => s.Name.Split(',').Select(name => new { name = name.Trim(), s.Task }))
                                         .GroupBy(x => x.name)
                                         .ToDictionary(g => g.Key, g => g.Select(x => x.Task).ToHashSet());

            var schedule = new List<tblSchedule>();
            var assignedEmployees = new HashSet<string>();

            foreach (var task in tasks)
            {
                if (equivalentTasks.ContainsValue(task.Task))
                    continue; // Skip equivalent pair (will be handled with main task)

                int min = (int)task.MinEmployees;
                int max = (int)task.MaxEmployees;

                // Select eligible employees
                var eligible = availableEmployees
                    .Where(e => !assignedEmployees.Contains(e.EmpID)
                             && !(task.Task == "Trailer Unloader" && e.Sex == "F")
                             && (!previousAssignments.ContainsKey(e.FullName) || !previousAssignments[e.FullName].Contains(task.Task)))
                    .ToList();

                var selected = eligible.Take(max).ToList();

                if (selected.Count < min)
                {
                    // Try to fill with any unassigned employee (even if they repeated a task)
                    var fallback = availableEmployees
                        .Where(e => !assignedEmployees.Contains(e.EmpID)
                                 && !(task.Task == "Trailer Unloader" && e.Sex == "F"))
                        .Except(selected)
                        .Take(min - selected.Count)
                        .ToList();

                    selected.AddRange(fallback);
                }

                if (selected.Count == 0) continue;

                var fullNames = selected.Select(e => e.FullName).ToList();
                foreach (var emp in selected)
                    assignedEmployees.Add(emp.EmpID);

                schedule.Add(new tblSchedule
                {
                    SchDate = scheduleDate,
                    Location = task.Location,
                    Task = task.Task,
                    Name = string.Join(", ", fullNames)
                });

                // Add equivalent task if applicable
                if (equivalentTasks.ContainsKey(task.Task))
                {
                    var eqTask = equivalentTasks[task.Task];
                    var eqLocation = tasks.FirstOrDefault(t => t.Task == eqTask)?.Location;
                    if (!string.IsNullOrEmpty(eqLocation))
                    {
                        schedule.Add(new tblSchedule
                        {
                            SchDate = scheduleDate,
                            Location = eqLocation,
                            Task = eqTask,
                            Name = string.Join(", ", fullNames)
                        });
                    }
                }
            }

            // 6. Save



            //_db.tblSchedule.AddRange(schedule);
            //_db.SaveChanges();

            //return "created";


            // 6. Save
            if (schedule.Any())
            {
                _db.tblSchedule.AddRange(schedule);
                _db.SaveChanges();
            }

            return "created";

        } 











































































    }
}
