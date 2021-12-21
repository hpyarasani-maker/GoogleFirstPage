<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="GoogleFirstPage.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="icon" href="../images/default-avatar-logo.png" type="image/x-icon" />
    <%--<link href="CSS/StyleSheet2.css" rel="stylesheet" />--%>
    <%--<link href="CSS/style.css" rel="stylesheet" />--%>
    <link href="../UnitTestReport/CSS/style.css" rel="stylesheet" />
    <link href="../CSS/style.css" rel="stylesheet" />
    <%--<link href="CSS/StyleSheet3.css" rel="stylesheet" />--%>

    <%--<link href="CSS/menu.css" rel="stylesheet" />--%>
</head>

<body>

    <%--<script src="js/custom.js"></script>--%>
    <script src="../js/custom.js"></script>
    <%--<script src="js/jquery.js"></script>--%>
    <script src="../js/jquery.js"></script>

    <%--<div class="header1">
        <div class="header-menu">
            <a href="http://www.pi-datametrics.com/" title="Pi-Datametrics DashBoard Home">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/PILogo_1.jpg" CssClass="imgLogo" Height="51px" Width="54px" /></a>
            <div class="container">
                <div class="navbar navbar-static-top">
                    <div>
                        <div class="navigation">
                            <nav style="margin-left:700px; margin-top:-70px;">
                                <ul class="nav topnav bold">
                                <li class="dropdown active">
                                    <a href="#">Login</a>&nbsp;<ul style="display: none;" class="dropdown-menu bold">
                                        <li><a href="Dashboard.aspx">BACK TO DASHBOARD</a></li>
                                    </ul>
                                </li>
                            </ul>
                            </nav>
                        </div>
                    </div>

                    <!-- end navigation -->
                </div>
            </div>
        </div>
    </div>--%>

    <div class="wrapper">
        <div style="border:double">
            <div class="container" align="center">
            <h1 style="font-family: Calibri":><b style="color:black;">LOGIN</b></h1>

            <form runat="server" class="form">
               <asp:TextBox ID="txtname" runat="server" placeholder="UserName"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtname" ErrorMessage="*">provide Valid UserName</asp:RequiredFieldValidator>
               <asp:TextBox ID="txtpswd" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpswd" ErrorMessage="*">provide valid password</asp:RequiredFieldValidator>
               <asp:Button ID="btn1" runat="server" Text="Sign In" OnClick="btn1_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;
           
                <asp:Label ID="lbl1" runat="server"></asp:Label>
            </form>
        </div>
        </div>
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <%--<ul class="bg-bubbles">
            <li></li>
        </ul>--%>
    </div>
    <%--<script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>--%>

    <%--<script src="ControlPanel/js/index.js"></script>--%>
</body>
</html>
