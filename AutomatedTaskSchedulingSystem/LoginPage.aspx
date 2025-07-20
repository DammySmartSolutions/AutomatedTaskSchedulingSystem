<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="AutomatedTaskSchedulingSystem.LoginPage" %>










<html lang="en">
 
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Login</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="assets/vendor/bootstrap/css/bootstrap.min.css">
    <link href="assets/vendor/fonts/circular-std/style.css" rel="stylesheet">
    <link rel="stylesheet" href="assets/libs/css/style.css">
    <link rel="stylesheet" href="assets/vendor/fonts/fontawesome/css/fontawesome-all.css">



    <style>
    html,
    body {
        height: 100%;
    }

        body {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-align: center;
            align-items: center;
            padding-top: 40px;
            padding-bottom: 40px;
            background-image: url('assets/images/FedexBackground.png');
        }
    </style>
</head>


<body>
    <!-- ============================================================== -->
    <!-- login page  -->
    <!-- ============================================================== -->
    <div class="splash-container">
        <div class="card ">
            <div class="card-header text-center"><a href="#"><img class="logo-img" src="assets/images/Fedex-logo.png" alt="logo" width="100" height="100"></a><span class="splash-description">Please enter your Login Detail</span></div>
            <div class="card-body">
                <form method="post" runat="server">
                    <div class="form-group">
                        <input class="form-control form-control-lg" id="txtempid" type="text" placeholder="EmployeeID" autocomplete="off" runat="server">
                    </div>
                    <div class="form-group">
                        <input class="form-control form-control-lg" id="txtpassword" type="password" placeholder="Password" runat="server">
                    </div>
                    <div class="form-group">
                        <label class="custom-control custom-checkbox">
                            <input class="custom-control-input" type="checkbox"><span class="custom-control-label">Remember Me</span>
                             <a class="link-primary float-right" style="text-align: end" runat="server" href="ForgotPassword.aspx"><i class="fa fa-fw fas  fa-window-close"></i>Forgot Password</a>
                        </label>
                         
                    </div>
                    <button type="submit" class="btn btn-primary  btn-lg btn-block" runat="server" onserverclick="btnLogin_ServerClick"    id="btnLogin">Log in</button>
                </form>
            </div>
         <%--   <div class="card-footer bg-white p-0  ">
                <div class="card-footer-item card-footer-item-bordered">
                  
                    <a href="ForgotPassword.aspx" class="footer-link">Forgot Password</a>
                </div>
                <div class="card-footer-item card-footer-item-bordered">
                    <a href="WelcomePage.aspx" class="footer-link">Back to Home Page</a>
                </div>
            </div>--%>
              









        </div>
    </div>

          <div class="footer">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 col-12" style="color:blue">
                             
                      
                               Copyright © 2025 - <% = System.DateTime.Today.Year %> All rights reserved by  <a href="#" style="color:blue">ENGI 9874 _9839 SE TEAM GREAT</a>.

                        </div>
                       
                    </div>
                </div>
          
            
          
        </div>


  
    <!-- ============================================================== -->
    <!-- end login page  -->
    <!-- ============================================================== -->
    <!-- Optional JavaScript -->
    <script src="assets/vendor/jquery/jquery-3.3.1.min.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.js"></script>
</body>
 
</html>