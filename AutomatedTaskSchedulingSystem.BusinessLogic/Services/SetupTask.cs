using AutomatedTaskSchedulingSystem.BusinessLogic.ViewModel;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services
{
    public class SetupTask
    {
        private readonly ATSSEntities _db;

        public SetupTask()
        {
            _db = new ATSSEntities();
        }



        public IEnumerable<TaskWithLocationDTO> LoadTask()
        {


           // return _db.tblSetupTask.ToArray();

            var query = from task in _db.tblSetupTask
                        join loc in _db.tblSetupLoc
                        on task.LocID equals loc.LocID
                        select new TaskWithLocationDTO
                        {
                            TaskID = task.TaskID,
                            Task = task.Task,
                            Location = loc.Location,
                            LocID = loc.LocID,
                            MinEmployees = (int)task.MinEmployees,
                            MaxEmployees = (int)task.MaxEmployees,
                        };

            return query.ToArray();



        }



        public string SaveTask(tblSetupTask model)
        {
            try
            {
                var existing = _db.tblSetupTask.FirstOrDefault(x => x.TaskID == model.TaskID);

                if (existing == null)
                {
                    _db.tblSetupTask.Add(model);
                    _db.SaveChanges();
                    return "created";
                }
                else
                {
                    existing.Task = model.Task;

                    existing.LocID = model.LocID;

                    existing.MaxEmployees = model.MaxEmployees;


                    existing.MinEmployees = model.MinEmployees;

                    _db.SaveChanges();
                    return "updated";
                }
            }
            
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"Property: {ve.PropertyName}, Error: {ve.ErrorMessage}");
                    }
                }
                throw; // Optional: rethrow if you want the error to bubble up
            }



        }




        public string DeleteTask(int id)
        {
            var Task = _db.tblSetupTask.Find(id);
            if (Task == null) return "Task not found";

            _db.tblSetupTask.Remove(Task);
            _db.SaveChanges();
            return "Task deleted successfully";
        }




    }
}
