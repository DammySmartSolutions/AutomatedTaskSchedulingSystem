<%@ Page Language="C#" MasterPageFile="~/Site.Master"  Title="Employee List" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="AutomatedTaskSchedulingSystem.EmployeeList" %>







<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    








        <div class="alert alert-primary" runat="server" id="divalert">
            <asp:Label ID="lblmsg" runat="server"></asp:Label>

        </div>


        <div class="row">
            <div class="col-6">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                    <div class="page-header">
                        <h2 class="pageheader-title"><%=Page.Title %> </h2>

                    </div>
                </div>
            </div>
            <div class="col-5">
                <a class="btn btn-primary float-right" style="text-align: end" runat="server" href="AddEmployee.aspx"><i class="fa fa-fw fas fa-plus-circle"></i>Add Employee</a>

            </div>

        </div>


        <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
            <div class="card">

                <div class="card-body">
                  
                    <div class="table-responsive">
                        <table id="table" class="table table-striped table-bordered second" style="width: 100%">

                            <thead>
                                <tr>

                                    <th>EmpID</th>
                                    <th>Name </th>
                                    <th>Sex</th>
                                    <th>Position</th>
                                    <th>Action</th>



                                </tr>
                            </thead>
                            <tbody style="width: 100%">
                            </tbody>





                        </table>
                    </div>
                </div>


                
            </div>
        </div>


        <asp:HiddenField ID="userid" runat="server" />

      

    <script src="Scripts/ATSS/EmployeeList.js?v=2"></script>


   










</asp:Content>