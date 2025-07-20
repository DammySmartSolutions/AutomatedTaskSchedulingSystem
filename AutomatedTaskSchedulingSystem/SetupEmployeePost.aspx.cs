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
    public partial class SetupEmployeePost : System.Web.UI.Page
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

                Response.Redirect("SetupEmployeePost.aspx");
            }


            try
            {





                if (txtpost.Value == "")
                {
                    lblmsg.Text = "Please Enter Position Name";
                    txtpost.Focus();
                    return;
                }





               




                int degid = 0;
                if (!string.IsNullOrEmpty(ComID.Value))
                {
                    degid = int.Parse(ComID.Value);

                }



                string post = txtpost.Value.Trim();

                


                // id = string.IsNullOrEmpty(ComID.Value) ? 0 : Convert.ToInt32(ComID.Value),

                var getPost = new tblSetupPostion
                {
                    PosID = degid,
                    Position = Utility.ToSentenceCase(post),
                   

                };

                var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.SetupEmployeePost();
                string result = service.SavePosition(getPost);

                if (result.StartsWith("error"))
                {
                    lblmsg.Text = result;
                }
                else
                {
                    lblmsg.Text = result == "created" ? "Position Created Successfully" : "Position Updated Successfully";

                    txtpost.Value = "";
                   


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