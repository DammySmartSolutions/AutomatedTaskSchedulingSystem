using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services
{
    public class SetupLocation
    {

        private readonly ATSSEntities _db;

       
        public SetupLocation(ATSSEntities db = null)
        {
            _db = db ?? new ATSSEntities();
        }



        public IEnumerable<tblSetupLoc> LoadLocation()
        {


            return _db.tblSetupLoc.ToArray();
        }



        public string SaveLocation(tblSetupLoc model)
        {
            try
            {
                var existing = _db.tblSetupLoc.FirstOrDefault(x => x.LocID == model.LocID);

                if (existing == null)
                {
                    _db.tblSetupLoc.Add(model);
                    _db.SaveChanges();
                    return "created";
                }
                else
                {
                    existing.Location = model.Location;
                   


                    _db.SaveChanges();
                    return "updated";
                }
            }
            //catch (Exception ex)
            //{
            //    return $"error: {ex.Message}";
            //}



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




        public string DeleteLocation(int id)
        {
            var Loc = _db.tblSetupLoc.Find(id);
            if (Loc == null) return "Location not found";

            _db.tblSetupLoc.Remove(Loc);
            _db.SaveChanges();
            return "Location deleted successfully";
        }


    }
}
