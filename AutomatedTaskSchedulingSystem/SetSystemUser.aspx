<%@ Page Language="C#" MasterPageFile="~/Site.Master"  Title="System User Setup" AutoEventWireup="true" CodeBehind="SetSystemUser.aspx.cs" Inherits="AutomatedTaskSchedulingSystem.SetSystemUser" %>








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






    
                        <div class="row">
						
										
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

                                                 <div class="form-group">
                                                     <label for="txtemail" class="col-form-label">Email</label>
                                                     <input id="txtemail" type="email" class="form-control form-control-sm" runat="server">
                                                 </div>

                                             </div>



                                             </div>

                                      
                                        
                                        <div class="row">
                                            <div class="col-sm-6">

                                                <div class="form-group">
                                               <label for="txtpass" class="col-form-label">Password</label>
                                               <input id="txtpass" type="password" class="form-control form-control-sm" runat="server" >
                                           </div>

                                            </div>

    
                                            <div class="col-sm-6">

                                                <div class="form-group">
                                                    <label for="txtconpass" class="col-form-label">Confirm Password</label>
                                                     <input id="txtconpass" type="password" class="form-control form-control-sm" runat="server" >
                                                </div>

                                            </div>



                                            </div>

                                                                    

                                        
                                        <div class="row">

                                             <div class="col-sm-6">
                                                 
                                                                                                                                                  
                                                   <asp:Button  CssClass="btn btn-primary form-control form-control-sm" runat="server" ID="btnsave" OnClick="btnsave_Click"        Text="Save" />

                                            </div>


                                        </div>


                                         </div>
                                            


                               
                                
                                <div class="card-body border-top">
                                      
                                      
                                          <div class="table-responsive">
                        <table id="table" class="table table-striped table-bordered second" style="width: 100%">

                            <thead>
                                <tr>

                                  
                                    <th>EmployeeID</th>
                                    <th>Email</th>
                                    
                                    <th>#</th>
                                    
                                     <th>#</th>
                             


                                </tr>
                            </thead>
                            <tbody style="width: 100%">
                            </tbody>





                        </table>
                    </div>
                                           
                                          

                                            
                                          




                                        
                                    </div>


                                          <asp:HiddenField ID="ComID" runat="server" />
                                  
                               
                                </div>
                            </div>
                      
                   
                            </div>






       

         





      
    <script src="Scripts/ATSS/SetupSystemUser.js?v=4"></script>
        
   





    </asp:Content>