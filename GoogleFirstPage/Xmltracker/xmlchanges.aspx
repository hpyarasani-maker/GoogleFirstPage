<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmlchanges.aspx.cs" Inherits="GoogleFirstPage.Xmltracker.xmlchanges" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>XML Tracker</title>

    <meta charset="utf-8" />

    <link href="Content/bootstrap.cosmo.min.css" rel="stylesheet" />
    <link href="Content/StyleSheet.css" rel="stylesheet" />

    <%--<link rel="icon" href="../images/PILogo_1.jpg" type="image/x-icon" />--%>

    <link href="../CSS/StyleSheet2.css" rel="stylesheet" />

    <script src="../js/jquery-1.10.2.js"></script>
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
    <script type="text/javascript">
        $(function () {
            $('[id*=gvelements]').footable();
        });
    </script>

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

    <style>
        .LblClass2 {
            font-family: Cambria;
            font-size: 20px;
            color: brown;
            font-weight: bold;
        }
    </style>


    <style type="text/css">
        .header3 {
            padding: 20px;
            text-align: center;
            font-family: Arial;
            font-size: 15px;
            background-color: #e4e8ef;
            height: 85px;
            padding: 10px 10px 10px 10px;
            border-radius: 10px 10px;
        }
    </style>

    <style>
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
    <style>
        table {
            padding: 1px;
            background-color: #FFFFFF;
            color: black;
        }

            table tr td {
                padding: 5px;
                border-right: 1px solid black;
            }

                table tr td:last-child {
                    border-right: none;
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
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

        <div class="header1">
            <div class="header-menu">
                <a href="../Dashboard.aspx" title="Pi-Datametrics Dashboard Home">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/PILogo_1.jpg" CssClass="imgLogo" Height="51px" Width="54px" /></a>
                <div class="container">
                    <div class="navbar navbar-static-top">
                        <div>
                            <div class="navigation">
                                <label style="font-family: Calibri; font-weight: bold; color: white; font-size: x-large; margin-left: -90px; margin-top: 20px">Elementary by Pi</label>
                                <nav style="margin-left: 950px; margin-top: -50px">
                                    <ul class="nav topnav bold">
                                        <li class="dropdown">
                                            <a href="../Dashboard.aspx">Home</a>&nbsp;<ul style="display: none;" class="dropdown-menu bold">
                                                <li><a href="../Dashboard.aspx">BACK TO DASHBOARD</a></li>
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
            <div class="header3">
                <div style="margin-top: 12px;">
                    <span class="auto-style1">Date :</span>&nbsp;&nbsp;
                             <asp:TextBox ID="txtdate" runat="server" placeholder="Date" CssClass="text1" Height="30px" Width="85px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calendar" PopupButtonID="img_pop" runat="server" TargetControlID="txtDate" Format="yyyy-MM-dd" StartDate="2022-10-31" />
                    &nbsp;&nbsp;
                <asp:ImageButton ID="img_pop" ImageUrl="~/images/calendarnew.png" ImageAlign="Bottom" runat="server" Height="25px" Width="30px" />&nbsp;&nbsp;&nbsp;

                <asp:Button runat="server" Text="Submit" CssClass="btnlf" Height="27px" ID="btnchanges" Width="120px" OnClick="btnchanges_Click" />
                </div>
            </div>
            <br />
            <br />
            <div class="wrapper">
                <asp:Label ID="lblalllinks" runat="server" ForeColor="Blue" Font-Size="Small" Font-Bold="true"></asp:Label><br />

                <asp:Label ID="lbl1" runat="server" CssClass="LblClassNew"></asp:Label>
                <br />

                <h1>XML Tracker Report</h1>

                <asp:UpdateProgress ID="UpdateProgress1" runat="server">

                 <%--   <ProgressTemplate>
                        <b>Processing...</b>
                    </ProgressTemplate>--%>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <p>
                            <asp:PlaceHolder ID="phr" runat="server"></asp:PlaceHolder>
                            &nbsp;
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <br />
            </div>
            <br />
            <br />
            <br />
            <%--<asp:Label ID="lbl1" runat="server" CssClass="LblClass"></asp:Label>--%>
            <br />
            <br />
            <div id="footer">
                <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics | XML Tracker.</div>
            </div>
        </div>
    </form>
</body>
</html>
