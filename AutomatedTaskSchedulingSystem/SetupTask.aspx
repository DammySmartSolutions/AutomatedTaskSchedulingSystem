<%@ Page Language="C#" MasterPageFile="~/Site.Master"  Title="Task Setup" AutoEventWireup="true" CodeBehind="SetupTask.aspx.cs" Inherits="AutomatedTaskSchedulingSystem.SetupTask" %>







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






    
                        <div class="row">
						
										
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                
                                <div class="card">
                                  
                                    <div class="card-body">
                                       
                                       
                                         <div class="row">
                                             <div class="col-sm-6">

                                                 <div class="form-group">
                                                <label for="txttask" class="col-form-label">Task</label>
                                                <input id="txttask" type="text" class="form-control form-control-sm" runat="server" >
                                            </div>

                                             </div>

                                             
                                             <div class="col-sm-6">

                                                        <label for="ddlLoc" class="col-form-label">Location</label>
                                                          <asp:DropDownList ID="ddlLoc" AppendDataBoundItems="true" runat="server" DataSourceID="SqlDataSource1" DataTextField="Location" DataValueField="LocID" CssClass="form-control form-control-sm" >    
                                                          <asp:ListItem Text="--Select Location--" Value="0" />   
                                                         </asp:DropDownList>
                                                                 <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:SqlConnection %>"
                                                        SelectCommand="select * from tblSetupLoc">
                                                         </asp:SqlDataSource>

                                             </div>




                                             </div>

                                      
                                        

                                        <div class="row">
                                            <div class="col-sm-6">

                                                <div class="form-group">
                                                    <label for="txtmin" class="col-form-label">Min Number of Person</label>
                                                    <input id="txtmin" type="number" class="form-control form-control-sm" runat="server">
                                                </div>

                                            </div>


                                            <div class="col-sm-6">
                                                   <label for="txtmax" class="col-form-label">Max Number of Person</label>
                                                    <input id="txtmax" type="number" class="form-control form-control-sm" runat="server">

                                            </div>




                                        </div>
                                                                    

                                        
                                        <div class="row">

                                             <div class="col-sm-6">
                                                 
                                                                                                                                                  
                                                   <asp:Button  CssClass="btn btn-primary form-control form-control-sm" runat="server" ID="btnsave" OnClick="btnsave_Click" Text="Save" />

                                            </div>


                                        </div>


                                         </div>
                                            


                               
                                
                                <div class="card-body border-top">
                                      
                                      
                                          <div class="table-responsive">
                        <table id="table" class="table table-striped table-bordered second" style="width: 100%">

                            <thead>
                                <tr>

                                  
                                    <th>Task</th>
                                     <th>Location</th>
                                    <th>Min</th>
                                    <th>Max</th>
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






       

         
    <script src="Scripts/ATSS/SetupTask.js?v=7"></script>

 


      
  
        
   





    </asp:Content>