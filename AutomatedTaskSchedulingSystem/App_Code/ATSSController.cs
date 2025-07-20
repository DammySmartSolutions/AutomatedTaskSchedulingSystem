using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.BusinessLogic.Services;
using AutomatedTaskSchedulingSystem.BusinessLogic.ViewModel;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutomatedTaskSchedulingSystem.App_Code
{
    public class ATSSController : ApiController
    {

        ATSSEntities Db = new ATSSEntities();

        private readonly AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupOrganization _setorg;

        private readonly AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupLocation _setloc;


        private readonly AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupTask _settask;

        private readonly AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupEmployeePost _setpost;

        private readonly AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetSystemUser _setsysuser;


        private readonly AutomatedTaskSchedulingSystem.BusinessLogic.Services.AddEmployeeAvail _setempavail;



        public ATSSController()
        {
            _setorg = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupOrganization();

            _setloc = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupLocation();

            _settask = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupTask();

            _setpost = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupEmployeePost();


            _setsysuser = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetSystemUser();



            _setempavail = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.AddEmployeeAvail();

        }





        [HttpGet, HttpPost]
        [ActionName("LoadOrganization")]
        public IEnumerable<tblSetupOrg> LoadOrganization()
        {


            try
            {

                return _setorg.LoadOrganization();



            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Organization: {ex.Message}");
                return Enumerable.Empty<tblSetupOrg>();
            }




        }


        [HttpGet, HttpPost]
        [ActionName("LoadLocation")]
        public IEnumerable<tblSetupLoc> LoadLocation()
        {


            try
            {

                return _setloc.LoadLocation();



            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Location: {ex.Message}");
                return Enumerable.Empty<tblSetupLoc>();
            }




        }



               

        

        [HttpDelete]
        [ActionName("DeleteLocation")]
        public string DeleteLocation(int id)
        {
            var result = _setloc.DeleteLocation(id);
            return result;
        }






        [HttpGet, HttpPost]
        [ActionName("LoadTask")]
        public IEnumerable<TaskWithLocationDTO> LoadTask()
        {


            try
            {

                return _settask.LoadTask();



            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Task: {ex.Message}");
                return Enumerable.Empty<TaskWithLocationDTO>();
            }




        }







        [HttpDelete]
        [ActionName("DeleteTask")]
        public string DeleteTask(int id)
        {
            var result = _settask.DeleteTask(id);
            return result;
            
        }








        [HttpGet, HttpPost]
        [ActionName("LoadPosition")]
        public IEnumerable<tblSetupPostion> LoadPosition()
        {


            try
            {

                return _setpost.LoadPosition();



            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Position: {ex.Message}");
                return Enumerable.Empty<tblSetupPostion>();
            }




        }







        [HttpDelete]
        [ActionName("DeletePosition")]
        public string DeletePosition(int id)
        {
            var result = _setpost.DeletePosition(id);
            return result;

        }








        [HttpGet, HttpPost]
        [ActionName("LoadSystemUser")]
        public IEnumerable<tblSetupSysUser> LoadSystemUser()
        {


            try
            {

                return _setsysuser.LoadSysUser();



            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error loading System User Table: {ex.Message}");
                return Enumerable.Empty<tblSetupSysUser>();
            }




        }







        [HttpDelete]
        [ActionName("DeleteSystemUser")]
        public string DeleteSystemUser(int id, string EmpID)
        {
            var result = _setsysuser.DeleteUser(id, EmpID);
            return result;

        }





        [HttpGet, HttpPost]
        [ActionName("LoadEmployeeList")]
        public IEnumerable<tblEmployee> LoadUser()
        {


            try
            {
                var query = Db.tblEmployee.ToArray();




                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Employee: {ex.Message}");
                return Enumerable.Empty<tblEmployee>();
            }



        }







        [HttpGet, HttpPost]
        [ActionName("LoadAvailability")]
        public IEnumerable<AvailClassTable>LoadAvailability()
        {


            try
            {

                return _setempavail.LoadAvailable();



            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Availability Table: {ex.Message}");
                return Enumerable.Empty<AvailClassTable>();
            }




        }


        [HttpGet, HttpPost]
        [ActionName("LoadTotalSysUser")]
        public string LoadTotalSysUser()
        {



            var query = Db.tblSetupSysUser
                .Select(x => new DashboardClass
                {

                    totalusers = x.SysUserID,


                }).Count();
            string total = Convert.ToString(query);

            return total;

        }



        [HttpGet, HttpPost]
        [ActionName("LoadTotalEmployee")]
        public string LoadTotalEmployee()
        {



            var query = Db.tblEmployee
                .Select(x => new DashboardClass
                {

                    totalemp = x.ID,


                }).Count();
            string total = Convert.ToString(query);

            return total;

        }



        [HttpGet, HttpPost]
        [ActionName("LoadTotalLocation")]
        public string LoadTotalLocation()
        {



            var query = Db.tblSetupLoc
                .Select(x => new DashboardClass
                {

                    totalloc = x.LocID,


                }).Count();
            string total = Convert.ToString(query);

            return total;

        }



        [HttpGet, HttpPost]
        [ActionName("LoadTotalTask")]
        public string LoadTotalTask()
        {



            var query = Db.tblSetupTask
                .Select(x => new DashboardClass
                {

                    totaltask = x.TaskID,


                }).Count();
            string total = Convert.ToString(query);

            return total;

        }


        [HttpGet, HttpPost]
        [ActionName("LoadEmployeeAvail")]

     


        public IEnumerable<DashboardClass> LoadEmployeeAvail()
        {
            var today = DateTime.Today;

            var query = Db.tblEmployeeAvail
                         .Where(x => System.Data.Entity.DbFunctions.TruncateTime(x.AvailDate) == today && x.Avail == true)
                         .GroupBy(x => x.AvailDate)
                         .Select(x => new DashboardClass
                         {
                             AvailID = x.Count(),
                             AvailDate = x.Key
                         })
                         .ToArray();

            return query;
        }










    }
}
