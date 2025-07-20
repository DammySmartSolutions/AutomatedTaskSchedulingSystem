using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services
{
    public class SetupOrganization
    {


        private readonly ATSSEntities _db;

        public SetupOrganization()
        {
            _db = new ATSSEntities();
        }


       
        public IEnumerable<tblSetupOrg> LoadOrganization()
        {

          
            return _db.tblSetupOrg.ToArray();
        }



        public string SaveOrganization(tblSetupOrg model)
        {
            try
            {
                var existing = _db.tblSetupOrg.FirstOrDefault(x => x.ID == model.ID);

                if (existing == null)
                {
                    _db.tblSetupOrg.Add(model);
                    _db.SaveChanges();
                    return "created";
                }
                else
                {
                    existing.Telephone = model.Telephone;
                    existing.Address = model.Address;
                    existing.Name = model.Name;
                    existing.OrgID = model.OrgID;
                   

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


    }
}
