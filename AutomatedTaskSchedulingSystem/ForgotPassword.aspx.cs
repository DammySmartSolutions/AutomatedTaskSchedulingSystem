using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.BusinessLogic.Services;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutomatedTaskSchedulingSystem
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        ATSSUtilityClass Utility = new ATSSUtilityClass();
        ATSSEntities Db = new ATSSEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            }
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }


        private void ShowMessage(string Message)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmessage", "alert('" + Message + "')", true);

        }

        protected void btnChgPass_ServerClick(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() != ViewState["CheckRefresh"].ToString())
            {
                Response.Redirect("ForgotPassword.aspx");
                return;
            }

            Session["CheckRefresh"] = Server.UrlDecode(DateTime.Now.ToString());

            try
            {
                var service = new ForgotPassService();
                string result = service.ChangePassword(
                    txtempid.Value.Trim(),
                    txtpassword.Value.Trim(),
                    txtconpass.Value.Trim()
                );

                if (result == "success")
                {
                    ////ShowMessage("Password Change Successful!!!");

                    ////Response.Redirect("LoginPage.aspx");

                    ShowMessage("Password Change Successful!!!");

                    // Inject a delayed redirect via JavaScript
                    ClientScript.RegisterStartupScript(this.GetType(), "redirect", "setTimeout(function(){ window.location='LoginPage.aspx'; }, 1000);", true);



                }
                else
                {
                    ShowMessage(result);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message);
            }
        }
   
    
    
    
    }
}