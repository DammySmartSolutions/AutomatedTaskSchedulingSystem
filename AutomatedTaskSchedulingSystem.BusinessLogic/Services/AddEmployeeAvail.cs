using AutomatedTaskSchedulingSystem.BusinessLogic.ViewModel;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services
{
    public class AddEmployeeAvail
    {


        private readonly ATSSEntities _db;

        ATSSUtilityClass Utility = new ATSSUtilityClass();
        //public AddEmployeeAvail()
        //{
        //    _db = new ATSSEntities();
        //}

        public AddEmployeeAvail(ATSSEntities db = null)
        {
            _db = db ?? new ATSSEntities();
        }






        public IEnumerable<AvailClassTable> LoadAvailable()
        {
            // First: Query and bring the data into memory
            var rawData = (from emp in _db.tblEmployee
                           join ava in _db.tblEmployeeAvail on emp.EmpID equals ava.EmpID
                           select new
                           {
                               emp.FullName,
                               ava.EmpID,
                               ava.AvailDate,
                               ava.Avail,
                               ava.AvailID
                           }).ToList(); // Execute the SQL here

            // Second: Format the data in memory
            var result = rawData.Select(x => new AvailClassTable
            {
                EmpData = $"{x.FullName} - {x.EmpID}",
                AvailDate = x.AvailDate,
                Avail = x.Avail,
                Empid = x.EmpID,
                AvailID = x.AvailID
            });

            return result.ToList();
        }





        public string SaveEmployeeAvail(tblEmployeeAvail model)
        {
            //if (model == null)
            //    throw new ArgumentNullException(nameof(model));


            try
            {
                var existing = _db.tblEmployeeAvail.FirstOrDefault(x => x.EmpID == model.EmpID && x.AvailDate == model.AvailDate);

                if (existing == null)
                {
                    _db.tblEmployeeAvail.Add(model);
                    _db.SaveChanges();



                    return "created";
                }
                else
                {
                    existing.AvailDate = model.AvailDate;
                    existing.Avail = model.Avail;
                   


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










    }
}
