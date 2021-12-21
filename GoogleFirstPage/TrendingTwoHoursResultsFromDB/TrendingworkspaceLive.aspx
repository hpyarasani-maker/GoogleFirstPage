<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrendingworkspaceLive.aspx.cs" Inherits="GoogleFirstPage.TrendingTwoHoursResultsFromDB.TrendingworkspaceLive" Title="Trending Workspace Live Every 2 Hours" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Trending Workspace Live Every 2 Hours</title>
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="expires" content="-1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />

    <link rel="icon" href="../images/default-avatar-logo.png" type="image/x-icon" />
    <link href="~/CSS/StyleSheet2.css" rel="stylesheet" />
    <link href="CSS/Newstyle.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <link href="../CSS/menu.css" rel="stylesheet" />

    <link href="CSS/NewGdstyle.css" rel="stylesheet" />

    <link href="CSS/CssRespnsive.css" rel="stylesheet" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
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

        .btn1f:hover {
            background-color: #1583c2;
            text-decoration: none;
            color: white;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            min-height: 100%;
            width: 100%;
        }

        .loadingNew {
            font-family: Arial;
            font-size: 10pt;
            border: 1px solid #67CFF5;
            width: 200px;
            height: 80px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>

    <style type="text/css">
        .auto-style1 {
            font-size: small;
            font-weight: bold;
        }

        .center1 {
            margin: auto;
            width: 10%;
            padding: 10px;
        }
    </style>

</head>
<body style="background-color: white">

    <script src="js/custom.js"></script>

    <script src="js/jquery.js"></script>

    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

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
                                        <li>
                                            <a href="../Dashboard.aspx">Home</a>
                                            <ul style="display: none;" class="dropdown-menu bold">
                                            </ul>
                                        </li>
                                    </ul>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="containerDB">
            <div class="header2">
                &nbsp;<span class="auto-style1">Keyword :</span>&nbsp;&nbsp;
                <asp:DropDownList ID="ddlkeywordLive" runat="server" CssClass="text1" Height="28px" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlkeywordLive_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Text="*" ControlToValidate="ddlkeywordLive" InitialValue="----Select----"></asp:RequiredFieldValidator>
                &nbsp;
                &nbsp; <strong><span class="auto-style2">SE :</span></strong>&nbsp;&nbsp; 
                   <asp:DropDownList ID="ddlseid" CssClass="text1" Height="28px" Width="200px" runat="server">
                   </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Text="*" ControlToValidate="ddlseid" InitialValue="----Select----"></asp:RequiredFieldValidator>
                &nbsp;
               
                            
                 &nbsp; <strong><span class="auto-style2">Hours :</span></strong>&nbsp;&nbsp; 
                  <asp:DropDownList ID="ddlhr" runat="server" CssClass="text1" Height="28px" Width="200px">
                      <asp:ListItem Enabled="true" Text="00 - 02" Value="1"></asp:ListItem>
                      <asp:ListItem Enabled="true" Text="02 - 04" Value="3"></asp:ListItem>
                      <asp:ListItem Enabled="true" Text="04 - 06" Value="5"></asp:ListItem>
                      <asp:ListItem Enabled="true" Text="06 - 08" Value="7"></asp:ListItem>
                      <asp:ListItem Enabled="true" Text="08 - 10" Value="9"></asp:ListItem>
                      <asp:ListItem Enabled="true" Text="10 - 12" Value="11"></asp:ListItem>
                      <asp:ListItem Enabled="true" Text="12 - 14" Value="13"></asp:ListItem>
                      <asp:ListItem Enabled="true" Text="14 - 16" Value="15"></asp:ListItem>
                      <asp:ListItem Enabled="true" Text="16 - 18" Value="17"></asp:ListItem>
                      <asp:ListItem Enabled="true" Text="18 - 20" Value="19"></asp:ListItem>
                      <asp:ListItem Enabled="true" Text="20 - 22" Value="21"></asp:ListItem>
                      <asp:ListItem Enabled="true" Text="22 - 00" Value="23"></asp:ListItem>
                  </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="*" ControlToValidate="ddlhr" InitialValue="----Select----"></asp:RequiredFieldValidator>
                &nbsp;
                 <span class="auto-style1">Date :</span>&nbsp;&nbsp;
                             <asp:TextBox ID="txtDate" runat="server" placeholder="Date" CssClass="text1" Height="20px" Width="80px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendar" PopupButtonID="img_pop" runat="server" TargetControlID="txtDate" Format="yyyy-MM-dd" />
                &nbsp;&nbsp;
                <asp:ImageButton ID="img_pop" ImageUrl="~/images/calendarnew.png" ImageAlign="Bottom" runat="server" Height="25px" Width="30px" />&nbsp;&nbsp;&nbsp;

                <asp:Button runat="server" Text="Submit" CssClass="btnlf" Height="24px" ID="btnworklive" Width="120px" OnClick="btnworklive_Click" />

            </div>
            <br />
            <asp:Label ID="lbl" runat="server" Font-Size="12" ForeColor="Chocolate" Visible="false"></asp:Label><br />
            <br />
            <div class="center1">
                <asp:Label ID="lblnodata" runat="server" ForeColor="Black" Font-Size="Medium" Font-Bold="true"></asp:Label>
            </div>
            <br />
            <br />
            <br />

            <asp:Label ID="cntk" runat="server" ForeColor="Black" Font-Size="Medium" Font-Bold="true" Text="No.of Search Terms :"></asp:Label>
            <asp:Label ID="lbl1" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="Large"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />

            <div id="footer">
                <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics | Trending Workspace Live Every 2 Hours.</div>
            </div>
        </div>
    </form>
</body>
</html>
