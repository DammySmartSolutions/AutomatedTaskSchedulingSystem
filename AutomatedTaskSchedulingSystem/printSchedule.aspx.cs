using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Stimulsoft.Base.StiDataLoaderHelper;

namespace AutomatedTaskSchedulingSystem
{
    public partial class printSchedule : System.Web.UI.Page
    {
        ATSSUtilityClass Utility = new ATSSUtilityClass();
        ATSSEntities Db = new ATSSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmployeeID"] == null)
            {
                Session["Page"] = HttpContext.Current.Request.Url.AbsolutePath;
                Response.Redirect("LoginPage.aspx");
                return;
            }

            if (!IsPostBack)
            {
                
                divalert.Visible = false;
                Session["CheckRefresh"] = Server.UrlDecode(DateTime.Now.ToString());

                
            }
            else
            {
               
                divalert.Visible = true;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }




        protected void btnprint_Click(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("printSchedule.aspx");
            }


            try
            {





                if (txtdate.Value == "")
                {
                    lblmsg.Text = "Please Select Date";
                    txtdate.Focus();
                    return;
                }







                DateTime schdate = Convert.ToDateTime(txtdate.Value).Date;


                                             
                string result = CheckTaskSchedule(schdate);



                
                    

                    if (result == "true")
                    {
                        Session["Date"] = schdate;

                        Response.Redirect("rptSchedule.aspx");

                    }


                    else
                    {


                        lblmsg.Text = "Schedule has not been generated for the Date Selected!!!";

                    }



                                                
            }



            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }




        }

        private string CheckTaskSchedule(DateTime schdate)
        {

            var checkschedule = Db.tblSchedule.Any(s => s.SchDate == schdate);

            if (checkschedule == false)
            {

                return "false";
            }


            else
            {
                return "true";

            }



        }



               
    }
}