using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services
{
    public class SetupEmployeePost
    {
        private readonly ATSSEntities _db;

        public SetupEmployeePost()
        {
            _db = new ATSSEntities();
        }



        public IEnumerable<tblSetupPostion> LoadPosition()
        {


            return _db.tblSetupPostion.ToArray();
        }



        public string SavePosition(tblSetupPostion model)
        {
            try
            {
                var existing = _db.tblSetupPostion.FirstOrDefault(x => x.PosID == model.PosID);

                if (existing == null)
                {
                    _db.tblSetupPostion.Add(model);
                    _db.SaveChanges();
                    return "created";
                }
                else
                {
                    existing.Position = model.Position;



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




        public string DeletePosition(int id)
        {
            var Post = _db.tblSetupPostion.Find(id);
            if (Post == null) return "Position not found";

            _db.tblSetupPostion.Remove(Post);
            _db.SaveChanges();
            return "Position deleted successfully";
        }





    }
}
