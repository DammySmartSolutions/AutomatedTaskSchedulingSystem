using AutomatedTaskSchedulingSystem.BusinessLogic;
using AutomatedTaskSchedulingSystem.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace AutomatedTaskSchedulingSystem
{
    public partial class AddEmployee : System.Web.UI.Page
    {
        ATSSUtilityClass Utility = new ATSSUtilityClass();
         ATSSEntities Db = new ATSSEntities();

        
       




        string Empid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Empid"] != null)
            {
                string GetId = Request.QueryString["Empid"].ToString();

                Empid = GetId;



                if (!IsPostBack)
                {
                    divalert.Visible = false;
                    LoadPageData(Empid);

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

                    divalert.Visible = false;
                    Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                }
                else
                {

                    divalert.Visible = true;
                }







            }


        }



        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }

        private void LoadPageData(string empid)
        {
            var query = (from s in Db.tblEmployee.Where(s => s.EmpID == empid)

                         select new
                         {
                             empid = s.EmpID,
                             fname = s.FirstName,
                             lname = s.LastName,
                             pos = s.Position,
                             sex = s.Sex,


                         }).ToArray();






            txtempid.Value = query[0].empid;
            txtfname.Value = query[0].fname;

            txtlname.Value = query[0].lname;

            ddlpos.SelectedItem.Text = query[0].pos;

            ddlsex.SelectedItem.Text = query[0].sex;


        }

        protected void btnsave_Click(object sender, EventArgs e)
        {


            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("AddEmployee.aspx");
            }


            try
            {





                if (txtempid.Value == "")
                {
                    lblmsg.Text = "Please Enter your EmployeeID";
                    txtempid.Focus();
                    return;
                }


                if (txtfname.Value == "")
                {
                    lblmsg.Text = "Please Enter your First Name";
                    txtfname.Focus();
                    return;
                }

                if (txtlname.Value == "")
                {
                    lblmsg.Text = "Please Enter your Last Name";
                    txtlname.Focus();
                    return;
                }



                if (ddlpos.SelectedItem.Text == "--Select Position--")
                {
                    lblmsg.Text = "Please Select Position";
                    ddlpos.Focus();
                    return;
                }



                if (ddlsex.SelectedItem.Text == "--Select Sex--")
                {
                    lblmsg.Text = "Please Select Sex";
                    ddlsex.Focus();
                    return;
                }






               



                string empid = txtempid.Value.Trim();
                string fname = txtfname.Value.Trim();

                string lname = txtlname.Value.Trim();
                string posn = ddlpos.SelectedItem.Text;

                string sex = ddlsex.SelectedItem.Text;


               

                string fullname = $"{fname} {lname}";


                // id = string.IsNullOrEmpty(ComID.Value) ? 0 : Convert.ToInt32(ComID.Value),

                var AddUser = new tblEmployee
                {
                    FirstName = Utility.ToSentenceCase(fname),
                    LastName = Utility.ToSentenceCase(lname),
                    EmpID = empid,
                    Position = posn,
                    Sex = sex,
                    FullName = Utility.ToSentenceCase(fullname),

                };

                var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.AddEmployee();
                string result = service.SaveEmployee(AddUser);

                if (result.StartsWith("error"))
                {
                    lblmsg.Text = result;
                }
                else
                {
                  //  lblmsg.Text = result == "created" ? "User Created Successfully"  : "User detail Updated Successfully";

                    if(result == "created")
                    {
                        Session["Alert"] = "Employee Added Successfully";



                    }



                    else
                    {

                        Session["Alert"] = "Employee Data Updated Successfully";



                       

                    }


                    Response.Redirect("EmployeeList.aspx");


                    txtempid.Value = "";
                    txtfname.Value = "";
                    txtlname.Value = "";
                    ddlpos.SelectedIndex = 0;
                    ddlsex.SelectedIndex = 0;
                }
            }








            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }


        }

        private void ShowMessage(string Message)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmessage", "alert('" + Message + "')", true);

        }





        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"]?.ToString() == ViewState["CheckRefresh"]?.ToString())
            {
                Session["CheckRefresh"] = Server.UrlDecode(DateTime.Now.ToString());
            }
            else
            {
                Response.Redirect("AddEmployee.aspx");
                return;
            }

            try
            {
                string strFolder = Server.MapPath("~/File/");
                string strFileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

                if (string.IsNullOrEmpty(FileUpload1.Value))
                {
                    ShowMessage("Choose CSV File to Upload!!!");
                    return;
                }

                if (!Directory.Exists(strFolder))
                    Directory.CreateDirectory(strFolder);

                string fileExt = Path.GetExtension(strFileName);
                if (fileExt != ".csv")
                {
                    ShowMessage("Please upload a valid .csv extension file!!!");
                    return;
                }

                string excelPath = Path.Combine(strFolder, strFileName);
                FileUpload1.PostedFile.SaveAs(excelPath);

                var service = new AutomatedTaskSchedulingSystem.BusinessLogic.Services.AddEmployee();
                string result = service.UploadEmployee(excelPath);

                if (result.StartsWith("error"))
                {
                    ShowMessage(result);
                    return;
                }

                string saveResult = service.SaveUploadEmployee();
                Session["Alert"] = saveResult == "success" ? "Employee(s) Added Successfully" : saveResult;

                Response.Redirect("EmployeeList.aspx");
            }
            catch (Exception ex)
            {
                ShowMessage("Upload Failed: " + ex.Message);
            }
        }







        protected void btnTemplate_Click(object sender, EventArgs e)
        {

            try
            {
                string fileName = "EmployeeUpload.csv";

                //Path of the File to be downloaded.
                string filePath = Server.MapPath(string.Format("~/UploadTemplate/{0}", fileName));

                //Content Type and Header.
                Response.ContentType = "application/csv";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

                //Writing the File to Response Stream.
                Response.WriteFile(filePath);

                //Flushing the Response.
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
               
            }

        }
    }
}