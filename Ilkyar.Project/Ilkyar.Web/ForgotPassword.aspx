<%@ Page Title="ForgotPassword" Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Ilkyar.Web.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>

    <!-- Global stylesheets -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,300,100,500,700,900" rel="stylesheet" type="text/css" />
    <link href="assets/css/icons/icomoon/styles.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/core.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/components.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/colors.css" rel="stylesheet" type="text/css" />
    <!-- /global stylesheets -->

    <!-- Core JS files -->
    <script type="text/javascript" src="assets/js/plugins/loaders/pace.min.js"></script>
    <script type="text/javascript" src="assets/js/core/libraries/jquery.min.js"></script>
    <script type="text/javascript" src="assets/js/core/libraries/bootstrap.min.js"></script>
    <script type="text/javascript" src="assets/js/plugins/loaders/blockui.min.js"></script>
    <!-- /core JS files -->

    <!-- Theme JS files -->
    <script type="text/javascript" src="assets/js/plugins/forms/styling/uniform.min.js"></script>

    <script type="text/javascript" src="assets/js/core/app.js"></script>
    <script type="text/javascript" src="assets/js/pages/login.js"></script>
    <!-- /theme JS files -->

    <script>
        function volunteerMouseOver() {
            document.getElementById("Volunteer").style.backgroundColor = "gray";
        }
    </script>

    <script>
        function volunteerMouseOut() {
            document.getElementById("Volunteer").style.backgroundColor = "lightgray";
        }
    </script>
    <script>
        function donatorMouseOver() {
            document.getElementById("Donator").style.backgroundColor = "gray";
        }
    </script>

    <script>
        function donatorMouseOut() {
            document.getElementById("Donator").style.backgroundColor = "lightgray";
        }
    </script>

    <style>
        .login-container .page-container .login-form {
            width: 400px !important;
        }
    </style>

</head>
<body class="login-container">

    <!-- Page container -->
    <div class="page-container">

        <!-- Page content -->
        <div class="page-content">

            <!-- Main content -->
            <div class="content-wrapper">

                <!-- Content area -->
                <form id="form1" runat="server">
                    <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
                    <div class="content pb-20">

                        <!-- Advanced login -->
                        <div class="panel panel-body login-form">
                            <div>
                                <img src="images/Ilkyarlogo.png" style="margin-left: auto; margin-right: auto; display: block; width: 75%;" />
                            </div>
                            <div class="text-center">
                                <h5 class="content-group-lg" style="font-weight: bold">İLKYAR'a Hoşgeldiniz!</h5>
                            </div>
                            <div class="content-divider text-muted form-group"><span>Şifrenizi Değiştirin</span></div>
                            <div class="form-group has-feedback has-feedback-left">
                                <telerik:RadTextBox ID="textBoxUsername" runat="server" class="form-control" placeholder="TC Kimlik No" Width="100%" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="textBoxUsername" ErrorMessage="Zorunlu alan!" />
                            </div>

                            <div class="form-group has-feedback has-feedback-left">
                                <telerik:RadTextBox ID="textBoxPassword" runat="server" class="form-control" placeholder="Yeni Şifre" TextMode="Password" Width="100%" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="textBoxPassword" ErrorMessage="Zorunlu alan!" />
                            </div>

                            <div class="form-group has-feedback has-feedback-left">
                                <telerik:RadTextBox ID="textBoxPasswordAgain" runat="server" class="form-control" placeholder="Yeni Şifre (Tekrar)" TextMode="Password" Width="100%" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="textBoxPasswordAgain" ErrorMessage="Zorunlu alan!" />
                            </div>

                            <div class="form-group">
                                <asp:Button ID="buttonLogin" runat="server" Text="Kaydet" class="btn btn-primary btn-user btn-block" OnClick="buttonLogin_Click" />
                                <asp:Label ID="labelErrorMessage" runat="server" Visible="false" />
                            </div>

                            <div class="row" id="divNotResetedPassword" runat="server" visible="false">
                                <div class="content-divider text-muted form-group" ><span>Giriş Yap</span></div>
                                <a href="Login.aspx" id="Volunteer" onmouseover="volunteerMouseOver()" onmouseout="volunteerMouseOut()" class="btn btn-default btn-block content-group" style="background-color: lightgray">Giriş Yap</a>
                            </div>


                        </div>
                        <!-- /advanced login -->
                    </div>
                </form>

                <!-- Footer -->
                <div class="footer text-muted text-center">
                    &copy; 2019
                </div>
                <!-- /footer -->

                <!-- /content area -->

            </div>
            <!-- /main content -->

        </div>
        <!-- /page content -->

    </div>
    <!-- /page container -->

</body>
</html>
