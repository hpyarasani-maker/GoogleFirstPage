<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchVolume.aspx.cs" Inherits="GoogleFirstPage.Googletrends.SearchVolume" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tracking Position and Volume Data</title>

    <link href="../CSS/StyleSheet2.css" rel="stylesheet" />
    <link href="css/main.css" rel="stylesheet" />

    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/jquery-1.10.2.min.js"></script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />

    <link href="../CSS/menu.css" rel="stylesheet" />
    <link href="../CSS/Newstyle.css" rel="stylesheet" />

    <link rel="icon" href="../images/default-avatar-logo.png" type="image/x-icon" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link media="screen" rel="stylesheet" href='http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' />
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>


    <link rel="icon" href="images/default-avatar-logo.png" type="image/x-icon" />

    <script src="js/jquery-1.10.2.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=gvdata]').footable();
        });
    </script>

    <style type="text/css">
        .btnlf {
            background: #1583c2;
            border: 1px solid #1583c2;
            width: 140px;
            height: 18px;
            font-size: 15px;
            border-radius: 4px;
            display: inline-block;
            font-family: Calibri;
            font-weight: bold;
            font-style: normal;
            color: #FFFFFF;
            margin-left: 50px;
        }

            .btnlf:hover {
                background-color: white;
                border: 1px solid #1583c2;
                text-decoration: none;
                color: #1583c2;
                margin-bottom: 0px;
            }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmgr" runat="server" EnablePageMethods="false"></asp:ScriptManager>
        <div class="header1" style="width: 2200px;">
            <div class="header-menu">
                <div style="margin-left: -150px;">
                    <a href="../Dashboard.aspx" title="Pi-Datametrics DashBoard Home">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/PILogo_1.jpg" CssClass="imgLogo" Height="51px" Width="54px" /></a>
                </div>
                <div class="container">
                    <div class="navbar navbar-static-top">
                        <div>
                            <div class="navigation">
                                <nav style="margin-left: 950px; margin-top: 10px">
                                    <ul class="nav topnav bold">
                                        <li class="dropdown">
                                            <a href="../Dashboard.aspx">Home</a>&nbsp;<ul style="display: none;" class="dropdown-menu bold">
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
        </div>

         
        <div class="containerDB">
            <br />
            <div style="align-items:center;margin-left: 400px;">
                
                    <asp:DropDownList ID="country_list" runat="server" Width="300px" CssClass="text1" Height="30px"></asp:DropDownList>
                    <asp:Button ID="sub_btn" runat="server" Text="Sumit" CssClass="btnlf" Height="25px"  Width="120px" OnClick="sub_btn_Click" />
                </div>
            <%-- <div style="margin-left: 670px;"></asp:
                <p style="font-family: Cambria; color: black; font-size: 25px; font-weight: bold;"><u>GoogleTrends </u></p>
            </div>--%>

            <%--<asp:Label ID="lbl" runat="server" Font-Size="12" ForeColor="Chocolate" Visible="false"></asp:Label><br />--%>
            
            <div class="wrapper">
               
            <div style="margin-left: 100px; margin-top: 100px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="name_lbl" runat="server" Text="" style="font-weight: bold;"></asp:Label>
                        <asp:GridView ID="gvdata" runat="server" AutoGenerateColumns="true" Width="80%" CssClass="footable" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" RowStyle-Width="1px" HeaderStyle-Height="30px">
                            <HeaderStyle BackColor="#e4e8ef" Font-Bold="true" ForeColor="Black" />

                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
            <br />
            <br />
            <br />

            <br />
            <br />
            <div id="footer">
                <div style="font-weight: bold; font-size: 14px; font-family: Calibri;">© <%= DateTime.Now.Year %> Pi Datametrics | Tracking Position and Volume Data.</div>
            </div>

        </div>
    </form>
</body>
</html>
