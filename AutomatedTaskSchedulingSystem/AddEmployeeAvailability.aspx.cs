using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.BusinessLogic.ViewModel;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace AutomatedTaskSchedulingSystem
{
    public partial class AddEmployeeAvailability : System.Web.UI.Page
    {

        ATSSUtilityClass Utility = new ATSSUtilityClass();
        ATSSEntities Db = new ATSSEntities();

        string Empid = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Request.QueryString["Empid"] != null) && (Request.QueryString["AvailID"] != null))
            {
                string GetId = Request.QueryString["Empid"].ToString();
                int availId = Convert.ToInt32(Request.QueryString["AvailID"].ToString());

                Empid = GetId;



                if (!IsPostBack)
                {
                    divalert.Visible = false;
                    LoadPageData(Empid, availId);

                    Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                }
                else
                {

                    divalert.Visible = true;
                }





            }

            else
            {






                if (!IsPostBack)
                {

                    LoadEmployee();

                    divalert.Visible = false;
                    Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                }
                else
                {

                    divalert.Visible = true;
                }







            }






        }


        public void LoadEmployee()
        {
            var allow = (from s in Db.tblEmployee
                         select new
                         {
                             Display = s.FullName + " - " + s.EmpID,
                             s.EmpID
                         }).ToList();

            ddlemp.DataTextField = "Display";
            ddlemp.DataValueField = "EmpID";
            ddlemp.DataSource = allow;
            ddlemp.DataBind();
            ddlemp.Items.Insert(0, new ListItem("--Select Employee--", "0"));
        }




        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }

      

        private void LoadPageData(string empid, int availId)
        {
            var result = (from emp in Db.tblEmployee
                          join ava in Db.tblEmployeeAvail on emp.EmpID equals ava.EmpID
                          where ava.AvailID == availId && ava.EmpID == empid
                          select new
                          {
                              emp.FullName,
                              ava.EmpID,
                              ava.AvailDate,
                              ava.Avail,
                              ava.AvailID
                          }).FirstOrDefault();

            if (result != null)
            {
                // Populate dropdown with employee name
                ddlemp.Items.Clear();
                ddlemp.Items.Add(new ListItem($"{result.FullName} - {result.EmpID}", result.EmpID));
                ddlemp.SelectedValue = result.EmpID;

                // Set availability fields
                txtdate.Value = Convert.ToDateTime(result.AvailDate).ToString("yyyy-MM-dd");
                chkAvail.Checked = result.Avail;
                 AvailID.Value = result.AvailID.ToString(); // Assuming this is a hidden input
            }
        }






        protected void btnsave_Click(object sender, EventArgs e)
        {

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("AddEmployeeAvailability.aspx");
            }


            try
            {





                if (txtdate.Value == "")
                {
                    lblmsg.Text = "Please Select Date";
                    txtdate.Focus();
                    return;
                }



                if (ddlemp.SelectedItem.Text == "--Select Position--")
                {
                    lblmsg.Text = "Please Select Employee";
                    ddlemp.Focus();
                    return;
                }





                int degid = 0;
                if (!string.IsNullOrEmpty(AvailID.Value))
                {
                    degid = int.Parse(AvailID.Value);

                }






                DateTime getdate = Convert.ToDateTime(txtdate.Value).Date;
                string empid = ddlemp.SelectedItem.Value;

               

                // id = string.IsNullOrEmpty(ComID.Value) ? 0 : Convert.ToInt32(ComID.Value),

                var AddAvail = new tblEmployeeAvail
                {
                   AvailID = degid,
                    EmpID = empid,
                    AvailDate = getdate,
                    Avail = chkAvail.Checked,
                 
                };

                var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.AddEmployeeAvail();
                string result = service.SaveEmployeeAvail(AddAvail);





                if (result.StartsWith("error"))
                {
                    lblmsg.Text = result;
                }
                else
                {
                    lblmsg.Text = result == "created" ? "Availability Created Successfully" : "Availability Updated Successfully";




                    AvailID.Value = "0";
                }


            }








            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }



        }




    }
}