<%@ Page Language="C#" MasterPageFile="~/Site.Master"  Title="Add Employee Availability" AutoEventWireup="true" CodeBehind="AddEmployeeAvailability.aspx.cs" Inherits="AutomatedTaskSchedulingSystem.AddEmployeeAvailability" %>














<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    

    
    
   
    <link rel="stylesheet" href="assets/vendor/bootstrap/css/bootstrap.min.css">
    <link href="assets/vendor/fonts/circular-std/style.css" rel="stylesheet">
   <link rel="stylesheet" href="../assets/libs/css/style.css?v=4">
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
                                                    <label for="ddlemp" class="col-form-label">Employee</label>
                                                    <asp:DropDownList ID="ddlemp" runat="server" CssClass="form-control form-control-sm">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>


                                            <div class="col-sm-6">

                                                <label for="txtdate" class="col-form-label">Date</label>
                                                <input id="txtdate" type="date" class="form-control form-control-sm" runat="server">
                                            </div>




                                        </div>

                                      
                                        

                                        <div class="row">
                                            <div class="col-sm-6">

                                                <div class="form-group">
                                                 <label for="chkAvail" class="form-check-label" >Available CheckBox</label>
                                                    <input id="chkAvail" type="checkbox" class="form-check-inline"       runat="server">
                                                </div>

                                            </div>


                                           <%-- <div class="col-sm-6">

                                                <label for="txtlname" class="col-form-label">Last Name</label>
                                                <input id="txtlname" type="text" class="form-control form-control-sm" runat="server">
                                            </div>--%>




                                        </div>
                                                                    


                                        
                                        <div class="row">

                                            <div class="col-sm-6">


                                                <asp:Button CssClass="btn btn-primary form-control form-control-sm" runat="server" ID="btnsave" OnClick="btnsave_Click" Text="Save" />

                                            </div>


                                        </div>


                                         </div>
                                            


                                        <asp:HiddenField ID="AvailID" runat="server" />
                            </div>
                      
                   
                            </div>






       

   

 


      
  
        
   





    </asp:Content>