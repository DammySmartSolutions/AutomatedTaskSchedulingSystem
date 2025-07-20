using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutomatedTaskSchedulingSystem
{
    public partial class GenerateSchedule : System.Web.UI.Page
    {
        ATSSUtilityClass Utility = new ATSSUtilityClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                divalert.Visible = false;
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
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


        protected void btngenerate_Click(object sender, EventArgs e)
        {

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("GenerateSchedule.aspx");
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








                //var generatesch = new tblSchedule
                //{
                //    SchDate = schdate,



                //};

                var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.GenerateSchedule();
                string result = service.GenerateTaskSchedule(schdate);

              

                if (result.StartsWith("error"))
                {
                    lblmsg.Text = result;
                }
                else
                {
                    //  lblmsg.Text = result == "created" ? "User Created Successfully"  : "User detail Updated Successfully";

                    if (result == "created")
                    {
                        Session["Date"] = schdate;

                        Response.Redirect("rptSchedule.aspx");

                    }

                    else if (result == "No employee available")
                    {
                        lblmsg.Text = "No employee available.  Go and Set Employee Availability for the Selected Date!!!";
                    }

                    else if (result == "No Task Setup")
                    {
                        lblmsg.Text = "No Task Setup, Kindly Setup Task";
                    }



                    else
                    {

                    }









                }




            }



            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }






        }
    }
}