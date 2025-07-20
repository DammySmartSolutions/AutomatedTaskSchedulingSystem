<%@ Page Language="C#" MasterPageFile="~/Site.Master"  Title="Add Employee" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="AutomatedTaskSchedulingSystem.AddEmployee" %>











<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    

    
    
   
    <link rel="stylesheet" href="assets/vendor/bootstrap/css/bootstrap.min.css">
    <link href="assets/vendor/fonts/circular-std/style.css" rel="stylesheet">
    <link rel="stylesheet" href="assets/libs/css/style.css">
    <link rel="stylesheet" href="assets/vendor/fonts/fontawesome/css/fontawesome-all.css">
    <link rel="stylesheet" href="assets/vendor/datepicker/tempusdominus-bootstrap-4.css" />
    <link rel="stylesheet" href="assets/vendor/inputmask/css/inputmask.css" />


    <div class="alert alert-primary" runat="server" id="divalert">
         <asp:Label ID="lblmsg" runat="server" ClientIDMode="Static"></asp:Label>
                                         
         </div>



    <div class="row">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                <div class="page-header" id="top">
                                    <h2 class="pageheader-title"><%=Page.Title %> </h2>
                                   
                                   
                                </div>
                            </div>
                        </div>






    

						
										
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                
                                <div class="card">
                                  
                                    <div class="card-body">
                                       
                                       
                                         <div class="row">
                                             <div class="col-sm-6">

                                                 <div class="form-group">
                                                <label for="txtempid" class="col-form-label">EmployeeID</label>
                                                <input id="txtempid" type="text" class="form-control form-control-sm" runat="server" >
                                            </div>

                                             </div>

                                             
                                             <div class="col-sm-6">

                                                        <label for="ddlpos" class="col-form-label">Position</label>
                                                          <asp:DropDownList ID="ddlpos" AppendDataBoundItems="true" runat="server" DataSourceID="SqlDataSource1" DataTextField="Position" DataValueField="PosID" CssClass="form-control form-control-sm" >    
                                                          <asp:ListItem Text="--Select Position--" Value="0" />   
                                                         </asp:DropDownList>
                                                                 <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:SqlConnection %>"
                                                        SelectCommand="select * from tblSetupPostion">
                                                         </asp:SqlDataSource>

                                             </div>




                                             </div>

                                      
                                        

                                                  <div class="row">
                                                            <div class="col-sm-6">

                                                                <div class="form-group">
                                                               <label for="txtfname" class="col-form-label">First Name</label>
                                                               <input id="txtfname" type="text" class="form-control form-control-sm" runat="server" >
                                                           </div>

                                                            </div>

    
                                                            <div class="col-sm-6">

                                                                       <label for="txtlname" class="col-form-label">Last Name</label>
                                                                        <input id="txtlname" type="text" class="form-control form-control-sm" runat="server" >

                                                            </div>




                            </div>
                                                                    


                                                              <div class="row">
                                <div class="col-sm-6">

                                    <div class="form-group">
                                   <label for="ddlsex" class="col-form-label">Sex</label>
                                   <asp:DropDownList ID="ddlsex"  runat="server"  CssClass="form-control form-control-sm" >    
                                         <asp:ListItem Text="--Select Sex--" Value="0"   />  
                                        <asp:ListItem Text="M" Value="1"   />  
                                        <asp:ListItem Text="F" Value="2"   />  
                                        </asp:DropDownList>
                               </div>

                                </div>

    
                                <div class="col-sm-6">

                                          

                                </div>




</div>
                                        
                                        <div class="row">

                                            <div class="col-sm-6">


                                                <asp:Button CssClass="btn btn-primary form-control form-control-sm" runat="server" ID="btnsave" OnClick="btnsave_Click" Text="Save" />

                                            </div>


                                        </div>


                                         </div>
                                            


                                      <div class="card-body border-top">
                                          <%-- <h6>Upload Employee</h6>--%>


                                    <div class="row">
                                        <div class="col-sm-6">

                                            <div class="form-group">
                                                <label for="FileUpload1" class="col-form-label">Batch Upload Employee List</label>
                                               <input type="file" id="FileUpload1" runat="server" name="FileUpload1" class="form-control form-control-sm" />
                                            </div>

                                        </div>



                                         <div class="col-sm-6">

                                                    <label for="txtlname" class="col-form-label">Get Upload Template</label>
                                                      <asp:Button CssClass="btn  btn-outline-primary form-control form-control-sm" runat="server" ID="btnTemplate" OnClick="btnTemplate_Click"        Text="Download Template" />

                                         </div>

                                        


                                     </div>

                                          <div class="row">

                                              <div class="col-sm-6">


                                                  <asp:Button CssClass="btn btn-primary form-control form-control-sm" runat="server" ID="btnUpload" OnClick="btnUpload_Click"         Text="Upload Employee" />

                                              </div>


                                          </div>




                               
                                
                                


                                          <asp:HiddenField ID="ComID" runat="server" />
                                  
                               
                                </div>
                            </div>
                      
                   
                            </div>






       

         
    <script src="Scripts/ATSS/SetupTask.js?v=6"></script>

 


      
  
        
   





    </asp:Content>