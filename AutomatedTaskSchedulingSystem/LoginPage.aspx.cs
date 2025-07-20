using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.BusinessLogic.Services;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutomatedTaskSchedulingSystem
{
    public partial class LoginPage : System.Web.UI.Page
    {
        ATSSEntities Db = new ATSSEntities();



        ATSSUtilityClass Utility = new ATSSUtilityClass();
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
        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {



            try
            {
                string empId = txtempid.Value.Trim();
                string password = txtpassword.Value.Trim();


              

                var loginService = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.LoginService();

                string redirectUrl;

                string result = loginService.Login(empId, password, new HttpSessionStateWrapper(Session), out redirectUrl);

                if (result == "success")
                {
                    Response.Redirect(redirectUrl);
                }
                else
                {
                    ShowMessage(result);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }


        }





    }
}