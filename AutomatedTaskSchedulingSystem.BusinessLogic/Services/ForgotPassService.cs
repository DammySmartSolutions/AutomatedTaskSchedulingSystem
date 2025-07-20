using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services
{
    public class ForgotPassService
    {

        ATSSUtilityClass Utility = new ATSSUtilityClass();

        private readonly ATSSEntities _db;

        //public ForgotPassService()
        //{
        //    _db = new ATSSEntities(); // Or inject via constructor
        //}

        public ForgotPassService(ATSSEntities db = null)
        {
            _db = db ?? new ATSSEntities();
        }




        public string ChangePassword(string empId, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(empId))
                return "Please Enter your EmployeeID";

            if (string.IsNullOrEmpty(newPassword))
                return "Please Enter your New Password";

            if (string.IsNullOrEmpty(confirmPassword))
                return "Please Enter your Confirm Password";

            if (newPassword != confirmPassword)
                return "Password MisMatch";

            var user = _db.tblSetupSysUser.FirstOrDefault(x => x.EmpID == empId);

            if (user == null)
                return "System User does not exist";

            user.Password = Utility.Encrypt(newPassword);
           // _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();

            return "success";
        }

    }
}
