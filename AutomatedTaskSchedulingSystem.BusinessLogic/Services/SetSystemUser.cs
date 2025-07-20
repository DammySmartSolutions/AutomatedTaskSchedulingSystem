using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services
{
    public class SetSystemUser
    {

        private readonly ATSSEntities _db;

        ATSSUtilityClass Utility = new ATSSUtilityClass();
        public SetSystemUser()
        {
            _db = new ATSSEntities();
        }



        public IEnumerable<tblSetupSysUser> LoadSysUser()
        {


            return _db.tblSetupSysUser.ToArray();
        }



        public string SaveUser(tblSetupSysUser model)
        {
            try
            {
                var existing = _db.tblSetupSysUser.FirstOrDefault(x => x.SysUserID == model.SysUserID);

                if (existing == null)
                {
                    _db.tblSetupSysUser.Add(model);
                    _db.SaveChanges();

                    Utility.SendEmail(model.EmpID, model.Email);

                    return "created";
                }
                else
                {
                    existing.Password = model.Password;
                    existing.Email = model.Email;




                    _db.SaveChanges();
                    Utility.SendEmailforUpdate(model.EmpID, model.Email);

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




        public string DeleteUser(int id, string EmpID)
        {
            var Post = _db.tblSetupSysUser.Find(id, EmpID);
            if (Post == null) return "System User not found";

            _db.tblSetupSysUser.Remove(Post);
            _db.SaveChanges();
            return "System User deleted successfully";
        }




    }
}
