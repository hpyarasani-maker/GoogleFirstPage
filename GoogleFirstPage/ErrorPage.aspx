<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="GoogleFirstPage.ErrorPage" Title="Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>

    <link rel="icon" href="images/default-avatar-logo.png" type="image/x-icon" />
    <link href="CSS/StyleSheet2.css" rel="stylesheet" />

    <link href="CSS/menu.css" rel="stylesheet" />


</head>
<body>
    <script src="js/jquery.js"></script>

    <script src="js/custom.js"></script>

    <form id="form1" runat="server">
        <div>
            <div class="header1">
                <div class="header-menu">
                    <a href="http://www.pi-datametrics.com/" title="Pi-Datametrics DashBoard Home">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/PILogo_1.jpg" CssClass="imgLogo" Height="51px" Width="54px" /></a>
                    <div class="container">
                        <div class="navbar navbar-static-top">
                            <div>
                                <div class="navigation">
                                    <nav style="margin-left: 950px; margin-top: 10px">
                                        <ul class="nav topnav bold">
                                            <li class="dropdown active">
                                                <a href="#">ERROR </a>&nbsp;<ul style="display: none;" class="dropdown-menu bold">
                                                    <li><a href="Dashboard.aspx">Back To DashBoard</a></li>
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
            </div>
            <div class="container">
                <br />
                <div class="errormain">
                    <asp:Image runat="server" ImageUrl="~/images/Error.jpg" /><br />
                    <asp:LinkButton runat="server" PostBackUrl="~/DashBoard.aspx" Text="Return To Dashboard" CssClass="Errorlink" />

                </div>

                <br />
                <div id="footer">
                    <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics  |  ErrorPage.</div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
