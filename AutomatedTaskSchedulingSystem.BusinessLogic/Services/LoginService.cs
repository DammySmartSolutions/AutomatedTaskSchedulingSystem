using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace AutomatedTaskSchedulingSystem.BusinessLogic.Services
{
    public class LoginService
    {


        ATSSUtilityClass Utility = new ATSSUtilityClass();
        private readonly ATSSEntities _db;

       

        public LoginService(ATSSEntities db = null)
        {
            _db = db ?? new ATSSEntities();
        }



        public string Login(string empId, string password, HttpSessionStateBase session, out string redirectUrl)
            {
                redirectUrl = "~/Dashboard.aspx"; // default

                if (string.IsNullOrWhiteSpace(empId))
                    return "Please Enter your EmployeeID";

                if (string.IsNullOrWhiteSpace(password))
                    return "Please Enter password";

                string adminUser = "dammy4edu@gmail.com";
                string adminPass = "SuperAdmin@1234";

                if (empId == adminUser && password == adminPass)
                {
                    session["EmployeeID"] = "1234567";
                    session["FullName"] = "System Administrator";
                    session["Email"] = adminUser;
                    session["Position"] = "System Administrator";

                    if (session["Page"] != null)
                    {
                        redirectUrl = session["Page"].ToString() + ".aspx";
                    }

                    return "success";
                }

                string encryptedPass = Utility.Encrypt(password);

                var user = _db.tblSetupSysUser.FirstOrDefault(x => x.EmpID == empId && x.Password == encryptedPass);
                if (user == null)
                {
                    return "Invalid Username or Password";
                }

                var details = _db.tblEmployee.FirstOrDefault(s => s.EmpID == empId);
                if (details == null)
                {
                    return "User details not found";
                }

                session["EmployeeID"] = empId;
                session["Position"] = details.Position;

                if (session["Page"] != null)
                {
                    redirectUrl = session["Page"].ToString() + ".aspx";
                }

                return "success";
            }
        }










    }

