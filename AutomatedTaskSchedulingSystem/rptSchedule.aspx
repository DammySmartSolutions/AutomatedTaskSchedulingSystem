<%@ Page Language="C#"  MasterPageFile="~/Site.Master" ClientIDMode="Static"  Title="Schedule Report" AutoEventWireup="true" CodeBehind="rptSchedule.aspx.cs" Inherits="AutomatedTaskSchedulingSystem.rptSchedule" %>








<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>




<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    

    
    
   
    <link rel="stylesheet" href="assets/vendor/bootstrap/css/bootstrap.min.css">
    <link href="assets/vendor/fonts/circular-std/style.css" rel="stylesheet">
    <link rel="stylesheet" href="assets/libs/css/style.css">
    <link rel="stylesheet" href="assets/vendor/fonts/fontawesome/css/fontawesome-all.css">
    <link rel="stylesheet" href="assets/vendor/datepicker/tempusdominus-bootstrap-4.css" />
    <link rel="stylesheet" href="assets/vendor/inputmask/css/inputmask.css" />


    <%--<div class="alert alert-primary" runat="server" id="divalert">
         <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                         
         </div>--%>



    <div class="row">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                <div class="page-header" id="top">
                                    <h2 class="pageheader-title"><%=Page.Title %> </h2>
                                   
                                   
                                </div>
                            </div>
                        </div>


                       <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>





    
                        <div class="row">
						
										
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                
                                <div class="card">
                                  
                                    <div class="card-body">
                                       
                                   
        

                                         <div class="row">
                                             
                                                         <cc1:StiWebViewer ID="StiWebViewer1" runat="server" RenderMode="AjaxWithCache" />

                                             

                                             </div>

                                      


                                         
                                   

                                             

                                                                                                





                                        
                                        
                                      
                                                                           
                                        


                                         </div>
                                            


                               
                                
                                


                                         
                                  
                               
                                </div>
                            </div>
                      
                   
                            </div>






       
         

         
           
       
     


      

        





    </asp:Content>








