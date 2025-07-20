<%@ Page Language="C#"  MasterPageFile="~/Site.Master"  Title="Print Schedule" AutoEventWireup="true" CodeBehind="printSchedule.aspx.cs" Inherits="AutomatedTaskSchedulingSystem.printSchedule" %>












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
                                                <label for="txtdate" class="col-form-label">Select Date</label>
                                               <input id="txtdate" type="date" class="form-control form-control-sm" runat="server">
                                            </div>

                                             </div>

                                             

                                             </div>

                                      
                                        


                                                                    

                                        
                                        <div class="row">

                                             <div class="col-sm-6">
                                                 
                                                                                                                                                  
                                                   <asp:Button  CssClass="btn btn-primary form-control form-control-sm" runat="server" ID="btnprint" OnClick="btnprint_Click"     Text="Print Schedule" />

                                            </div>


                                        </div>


                                         </div>
                                            


                               
                                
                                


                                          <asp:HiddenField ID="ComID" runat="server" />
                                  
                               
                                </div>
                            </div>
                      
                   
                            </div>






       

         


    


      
    <script src="Scripts/ATSS/SetupPost.js?v=1"></script>
        
   





    </asp:Content>