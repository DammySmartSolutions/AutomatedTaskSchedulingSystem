using AutomatedTaskSchedulingSystem.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutomatedTaskSchedulingSystem
{
    public partial class EmployeeList : System.Web.UI.Page
    {

        ATSSUtilityClass Utility = new ATSSUtilityClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["Alert"] != null)
                {
                    divalert.Visible = true;
                    lblmsg.Text = Session["Alert"].ToString();
                    Session["Alert"] = null;

                }
                else
                {
                    divalert.Visible = false;
                }



                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            }


        }



        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }






    }
}