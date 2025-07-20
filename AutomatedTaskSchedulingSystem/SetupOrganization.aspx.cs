using AutomatedTaskSchedulingSystem.BusinessLogic.Services;
using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AutomatedTaskSchedulingSystem
{
    public partial class SetupOrganization : System.Web.UI.Page
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

                Response.Redirect("SetupOrganization.aspx");
            }


            try
            {


                if (txtorgid.Value == "")
                {
                    lblmsg.Text = "Please Enter Organization Code";
                    txtorgid.Focus();
                    return;
                }


                if (txtname.Value == "")
                {
                    lblmsg.Text = "Please Enter Organization Name";
                    txtname.Focus();
                    return;
                }

                if (txtaddress.Value == "")
                {
                    lblmsg.Text = "Please Enter Institution Address";
                    txtaddress.Focus();
                    return;
                }


                if (txtphone.Value == "")
                {
                    lblmsg.Text = "Please Enter Institution Phone Number ";
                    txtphone.Focus();
                    return;
                }

                if (txtorgid.Value == "")
                {
                    lblmsg.Text = "Please Enter Organization Code";
                    txtorgid.Focus();
                    return;
                }






                int degid = 0;
                if (!string.IsNullOrEmpty(ComID.Value))
                {
                    degid = int.Parse(ComID.Value);

                }



                string comName = txtname.Value.Trim();

                string comAddr = txtaddress.Value.Trim();

                string comPhone = txtphone.Value.Trim();

              

                string orgcode = txtorgid.Value.Trim();

               // id = string.IsNullOrEmpty(ComID.Value) ? 0 : Convert.ToInt32(ComID.Value),

                var inst = new tblSetupOrg
                {
                    ID = degid,
                    OrgID = orgcode,
                    Address = Utility.ToSentenceCase(comAddr),
                    Telephone = comPhone,
                    Name = comName,

                };

                var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupOrganization();
                string result = service.SaveOrganization(inst);

                if (result.StartsWith("error"))
                {
                    lblmsg.Text = result;
                }
                else
                {
                    lblmsg.Text = result == "created" ? "Organization Created Successfully" : "Organization Updated Successfully";

                    txtname.Value = "";
                    txtaddress.Value = "";
                    txtphone.Value = "";
                    txtorgid.Value = "";
                   
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