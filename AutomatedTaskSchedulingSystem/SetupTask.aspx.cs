using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace AutomatedTaskSchedulingSystem
{
    public partial class SetupTask : System.Web.UI.Page
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

                Response.Redirect("SetupTask.aspx");
            }


            try
            {





                if (txttask.Value == "")
                {
                    lblmsg.Text = "Please Enter Task Name";
                    txttask.Focus();
                    return;
                }


                if (txtmin.Value == "")
                {
                    lblmsg.Text = "Please Enter Minimum Number of Employee for Task";
                    txtmin.Focus();
                    return;
                }


                if (txtmax.Value == "")
                {
                    lblmsg.Text = "Please Enter Maximum Number of Employee for Task";
                    txtmax.Focus();
                    return;
                }



                if (ddlLoc.SelectedItem.Text == "--Select Location--")
                {
                    lblmsg.Text = "Please Select Location";
                    ddlLoc.Focus();
                    return;
                }




                int degid = 0;
                if (!string.IsNullOrEmpty(ComID.Value))
                {
                    degid = int.Parse(ComID.Value);

                }



                string task = txttask.Value.Trim();

                int minnum  = int.Parse(txtmin.Value.Trim());

                int maxnum = int.Parse(txtmax.Value.Trim());

                int locid = Convert.ToInt32(ddlLoc.SelectedItem.Value);


                // id = string.IsNullOrEmpty(ComID.Value) ? 0 : Convert.ToInt32(ComID.Value),

                var getTask = new tblSetupTask
                {
                    TaskID = degid,
                    Task = Utility.ToSentenceCase(task),
                    LocID = locid,
                    MinEmployees = minnum,
                    MaxEmployees = maxnum,

                };

                var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupTask();
                string result = service.SaveTask(getTask);

                if (result.StartsWith("error"))
                {
                    lblmsg.Text = result;
                }
                else
                {
                    lblmsg.Text = result == "created" ? "Task Created Successfully" : "Task Updated Successfully";

                    txttask.Value = "";
                    ddlLoc.SelectedIndex = 0;


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