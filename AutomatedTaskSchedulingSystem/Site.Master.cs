using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutomatedTaskSchedulingSystem
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmployeeID"] == null)
            {
                Response.Redirect("LoginPage.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string position = Session["Position"]?.ToString();
                if (!string.IsNullOrEmpty(position))
                {
                    HideMenu(position);
                }
            }
        }

        private void HideMenu(string position)
        {
            switch (position?.Trim().ToLower())
            {
                case "cargo handler":
                    mnuSetup.Visible = false;
                    mnuUsers.Visible = false;
                    mnuschedule.Visible = false;
                    break;

                case "senior cargo handler":
                    mnuSetup.Visible = false;
                    mnuUsers.Visible = false;
                    break;

                case "team leader":
                    mnuSetup.Visible = false;
                    break;

                default:
                    // Admins or other roles – leave menus visible
                    break;
            }
        }



    }
}