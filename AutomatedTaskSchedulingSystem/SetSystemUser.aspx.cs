using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutomatedTaskSchedulingSystem
{
    public partial class SetSystemUser : System.Web.UI.Page
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

                Response.Redirect("SetSystemUser.aspx");
            }


            try
            {





                if (txtempid.Value == "")
                {
                    lblmsg.Text = "Please Enter your EmployeeID";
                    txtempid.Focus();
                    return;
                }


                if (txtemail.Value == "")
                {
                    lblmsg.Text = "Please Enter your Email Address";
                    txtemail.Focus();
                    return;
                }


                string checkemail = Utility.ValidateEmail(txtemail.Value.Trim());

                if (checkemail != "Valid")
                {
                    lblmsg.Text = checkemail;
                    txtemail.Focus();
                    return;
                }





                if (txtpass.Value == "")
                {
                    lblmsg.Text = "Please Enter your Password";
                    txtpass.Focus();
                    return;
                }

                if (txtconpass.Value == "")
                {
                    lblmsg.Text = "Please Enter your Confirm Password";
                    txtconpass.Focus();
                    return;
                }



                string newpass = txtpass.Value.Trim();
                string conpass = txtconpass.Value.Trim();



              
                if (newpass != conpass)
                {
                    lblmsg.Text = "Password Mismatch";
                    txtconpass.Focus();
                    return;
                }

               




                int degid = 0;
                if (!string.IsNullOrEmpty(ComID.Value))
                {
                    degid = int.Parse(ComID.Value);

                }



                string empid = txtempid.Value.Trim();
                string email = txtemail.Value.Trim();

               string pass = Utility.Encrypt(newpass);





                // id = string.IsNullOrEmpty(ComID.Value) ? 0 : Convert.ToInt32(ComID.Value),

                var getUser = new tblSetupSysUser
                {
                    SysUserID = degid,
                    EmpID = empid,
                    Email = email,
                    Password = pass,

                                    };

                var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetSystemUser();
                string result = service.SaveUser(getUser);

                if (result.StartsWith("error"))
                {
                    lblmsg.Text = result;
                }
                else
                {
                    lblmsg.Text = result == "created" ? "User Created Successfully" : "User detail Updated Successfully";

                    txtconpass.Value = "";
                    txtemail.Value = "";
                    txtempid.Value = "";
                    txtpass.Value = "";
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