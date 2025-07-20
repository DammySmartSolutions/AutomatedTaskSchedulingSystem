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
    public partial class SetupLocation : System.Web.UI.Page
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


        protected void btnsave_Click(object sender, EventArgs e)
        {


            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SetupLocation.aspx");
            }


            try
            {


               


                if (txtname.Value == "")
                {
                    lblmsg.Text = "Please Enter Location Name";
                    txtname.Focus();
                    return;
                }

              



                int degid = 0;
                if (!string.IsNullOrEmpty(ComID.Value))
                {
                    degid = int.Parse(ComID.Value);

                }



                string comName = txtname.Value.Trim();

              
                // id = string.IsNullOrEmpty(ComID.Value) ? 0 : Convert.ToInt32(ComID.Value),

                var Locatn = new tblSetupLoc
                {
                    LocID = degid,
                    Location = Utility.ToSentenceCase(comName),

                };

                var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupLocation();
                string result = service.SaveLocation(Locatn);

                if (result.StartsWith("error"))
                {
                    lblmsg.Text = result;
                }
                else
                {
                    lblmsg.Text = result == "created" ? "Location Created Successfully" : "Location Updated Successfully";

                    txtname.Value = "";
                   

                    ComID.Value = "0";
                }
            }








            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }








        }
    }
}