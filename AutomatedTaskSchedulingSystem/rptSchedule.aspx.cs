using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace AutomatedTaskSchedulingSystem
{
    public partial class rptSchedule : System.Web.UI.Page
    {

        private string Connectionstring;
        private string ReportName;
       

         DateTime date;

        ATSSEntities Db = new ATSSEntities();



        ATSSUtilityClass Utility = new ATSSUtilityClass();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["EmployeeID"] == null)
            {
                // Save current page for redirection after login
                Session["Page"] = HttpContext.Current.Request.Url.AbsolutePath;
                Response.Redirect("LoginPage.aspx");
                return;
            }

            // Ensure date is loaded after confirming session is valid
            if (Session["Date"] != null)
            {
                date = Convert.ToDateTime(Session["Date"]).Date;
            }
            else
            {
                // Optional: default to today or handle error
                date = DateTime.Today.Date;
            }

            if (!IsPostBack)
            {
                LoadReport();
                Session["CheckRefresh"] = DateTime.Now.ToString(); // URL decode not really needed
            }



        }

        private void LoadReport()
        {
            ReportName = "Schedule";
            StiReport Report = new StiReport();


            Connectionstring = ConfigurationManager.ConnectionStrings["ReportConnection"].ConnectionString;
            string AppDirectory = HttpContext.Current.Server.MapPath(string.Empty);
            string Path = AppDirectory + "\\Report\\" + ReportName + ".mrt";
            Report.Load(Path);

            Report.Dictionary.Databases.Clear();
            Report.Dictionary.Databases.Add(new StiSqlDatabase("ATSS", "ATSS", Connectionstring, false));





            Report["@date"] = date;





            Report.Compile();
            Report.Render();

            StiWebViewer1.Report = Report;
        }



    }
}