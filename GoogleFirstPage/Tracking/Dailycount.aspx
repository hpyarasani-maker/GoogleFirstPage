<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dailycount.aspx.cs" Inherits="GoogleFirstPage.OxylabsRequestCount.Dailycount" Title="Tracking Daily Count" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Tracking Daily Count</title>

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
        .wrapper12 {
            overflow: hidden;
            border: 1px solid gray;
        }

            .wrapper12 div {
                height: auto;
                padding: 10px;
            }

        #one {
            float: left;
            margin-right: 20px;
        }

        #two {
            background-color: white;
            overflow: hidden;
            margin-right: 1px;
            min-height: 200px;
        }
    </style>


    <style type="text/css">
        .grid-container {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            /*border: 2px solid #000;*/
            grid-template-columns: auto auto auto auto;
        }

        .grid-item {
            padding: 20px;
            text-align: center;
            /*border: 1px solid #ddd;*/
        }


        .grid-container {
            padding: 20px;
            background-color: white;
            border-top: 1px solid #000;
            border-bottom: 1px solid #000;
            border-left: 1px solid #000;
            border-right: 1px solid #000;
        }
    </style>


</head>
<body style="background-color: white">

    <script src="js/custom.js"></script>

    <script src="js/jquery.js"></script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

        <div class="header1" style="width: 2100px;">
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
                                                <li><a href="../Dashboard.aspx">BACK TO DASHBOARD</a></li>
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

            <br />
            <div class="wrapper">
                <asp:Label ID="lblalllinks" runat="server" Font-Names="Cambria" ForeColor="#0080ff" Font-Size="X-Large" Font-Bold="true"></asp:Label><br />
                <div class="panel-group">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="border-bottom: 1px solid black;"><b style="font-family: Cambria; font-weight: bold; font-size: large; color: black;">Daily Sending Request and Receiving keywords Count :</b></div>
                    </div>
                </div>
                <br />
                <div class="wrapper12">
                    <div id="one">
                        <label style="color: darkgreen; font-family: Cambria; font-size: x-large">Sending Count =</label><asp:Label runat="server" ID="lblsendcnt" Font-Names="Cambria" ForeColor="#0080ff" Font-Size="Large" Font-Bold="true" Text=" Google + Baidu  + Bing + Haosou  + Naver + Sogou + Yahoo + Yandex."></asp:Label><br />
                        <label style="color: darkgreen; font-family: Cambria; font-size: x-large">Total Sending Count =</label><asp:Label runat="server" ID="lbltotalsecnt" Font-Names="Cambria" ForeColor="#0080ff" Font-Size="Large" Font-Bold="true" Text=" Sending + Re-Tracked Keywords + ScriptMetaData."></asp:Label><br />
                        <asp:Label runat="server" ID="lbllessthan20view" Font-Names="Cambria" ForeColor="#0080ff" Font-Size="Large" Font-Bold="true" Text=""></asp:Label><br />
                        <br />
                        <br />
                        <asp:Label runat="server" ID="Label1" Font-Names="Cambria" ForeColor="#0080ff" Font-Size="Large" Font-Bold="true" Text="Search Engines like Baidu, Bing, Haosou, Naver, Sogou, Yahoo, Yandex."></asp:Label><br />
                        <label style="color: darkgreen; font-family: Cambria; font-size: x-large">Sending Request =</label><asp:Label runat="server" ID="Label2" Font-Names="Cambria" ForeColor="#0080ff" Font-Size="Large" Font-Bold="true" Text=" Number of keywords  *  10 Pages."></asp:Label><br />
                    </div>
                    <div id="two">
                        <asp:GridView ID="grdmonth" runat="server" AutoGenerateColumns="false" CssClass="footable" HeaderStyle-Height="25px" Font-Bold="true" HeaderStyle-Font-Bold="true" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef">
                            <Columns>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <%--  <div style="width: 80px;">--%>
                                        <%# Eval("Month") %>
                                        <%--</div>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month Requests">
                                    <ItemTemplate>
                                        <%--  <div style="width: 60px;">--%>
                                        <%# Eval("MonthSendingCount","{0:N0}") %>
                                        <%-- </div>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tracking-issue">
                                    <ItemTemplate>
                                        <%--<div style="width: 100px;">--%>
                                        <%# Eval("Tracking-issue","{0:N0}") %>
                                        <%--  </div>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pi-issue">
                                    <ItemTemplate>
                                        <%--<div style="width: 60px;">--%>
                                        <%# Eval("Pi-Issue","{0:N0}") %>
                                        <%-- </div>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Oxylabs issue">
                                    <ItemTemplate>
                                        <%--  <div style="width: 60px;">--%>
                                        <%# Eval("oxyissue","{0:N0}") %>
                                        <%-- </div>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Month Requests">
                                    <ItemTemplate>
                                        <%--<div style="width: 100px;">--%>
                                        <%# Eval("TotalMonthCount","{0:N0}") %>
                                        <%--  </div>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        &nbsp;&nbsp;&nbsp;<label style="color: darkgreen; font-family: Cambria; font-size: large">Total Month Requests = </label>
                        <label style="color: #0080ff; font-family: Cambria; font-size: large">Month Requests</label>
                        <label style="color: red; font-family: Cambria; font-size: x-large">- </label>
                        <label style="color: #0080ff; font-family: Cambria; font-size: large">Oxylabs Issue. </label>
                    </div>
                </div>

                <br />
                <asp:Label ID="lbl1" runat="server" CssClass="LblClassNew"></asp:Label>
                <br />
                <div>
                    <div class="panel-group">
                        <div class="panel panel-default">
                            <div class="panel-heading" style="border-bottom: 1px solid black;">
                                <b style="font-family: Cambria; font-size: large; color: black;">Download Tracking Requests Report</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:LinkButton ID="lnkdownloadreport" runat="server" OnClick="lnkdownloadreport_Click" Font-Bold="true" Font-Size="Large">Generate Report Csv</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="panel-group">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="border-bottom: 1px solid black;"><b style="font-family: Cambria; font-size: large; color: black;">Last 30 days</b></div>
                    </div>
                </div>
                <br />

                <asp:GridView ID="gvrequestcount" AutoGenerateColumns="false" Font-Bold="true" HeaderStyle-BackColor="#e4e8ef" AlternatingRowStyle-BackColor="#f5f5f5" HeaderStyle-Height="25px" CssClass="footable" HeaderStyle-Font-Bold="true" runat="server" OnRowDataBound="gvrequestcount_RowDataBound" DataKeyNames="Date" Font-Size="Small" AllowPaging="true" OnPageIndexChanging="gvrequestcount_PageIndexChanging" PageSize="30">
                    <Columns>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("Date") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pi-Daily Count">
                            <ItemTemplate>
                                <div style="width: 74px;">
                                    <%# Eval("Pi_DailyCount","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Google">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Google","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Baidu">
                            <ItemTemplate>
                                <div style="width: 65px;">
                                    <%# Eval("Baidu","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bing">
                            <ItemTemplate>
                                <div style="width: 65px;">
                                    <%# Eval("Bing","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Haosou">
                            <ItemTemplate>
                                <div style="width: 65px;">
                                    <%# Eval("Haosou","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Naver">
                            <ItemTemplate>
                                <div style="width: 65px;">
                                    <%# Eval("Naver","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Price-searcher">
                            <ItemTemplate>
                                <div style="width: 42px;">
                                    <%# Eval("PriceSearcher","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Sogou">
                            <ItemTemplate>
                                <div style="width: 65px;">
                                    <%# Eval("Sogou","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Yandex">
                            <ItemTemplate>
                                <div style="width: 65px;">
                                    <%# Eval("Yandex","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Yahoo">
                            <ItemTemplate>
                                <div style="width: 65px;">
                                    <%# Eval("Yahoo","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sending">
                            <ItemTemplate>
                                <div style="width: 65px;">
                                    <%# Eval("Sending","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Receiving">
                            <ItemTemplate>
                                <div style="width: 65px;">
                                    <%# Eval("Receiving","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trending">
                            <ItemTemplate>
                                <div style="width: 50px;">
                                    <%# Eval("Trending","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Empty Block">
                            <ItemTemplate>
                                <div style="width: 60px;">
                                    <%# Eval("Empty Block","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Full Block">
                            <ItemTemplate>
                                <div style="width: 60px;">
                                    <%# Eval("Full Block","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NO Block">
                            <ItemTemplate>
                                <div style="width: 60px;">
                                    <%# Eval("NO Block","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <div style="width: 60px;">
                                    <%# Eval("Total","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Re-Tracked">
                            <ItemTemplate>
                                <div style="width: 80px;">
                                    <%# Eval("Re-TrackedKeywords","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Error keywords">
                            <ItemTemplate>
                                <div style="width: 80px;">
                                    <%# Eval("Error_Keywords","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Lessthan20">
                            <ItemTemplate>
                                <div style="width: 65px;">
                                    <asp:HyperLink ID="lbllessthan20" runat="server" Text='<%# Eval("Lessthen20") %>' Target="_blank" NavigateUrl='<%# string.Format("~/Tracking/Lessthan20.aspx?Date={0}", HttpUtility.UrlEncode(Eval("Date").ToString())) %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Total sending">
                            <ItemTemplate>
                                <div style="width: 80px;">
                                    <%# Eval("TotalSending","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Message">
                            <ItemTemplate>
                                <div style="width: 120px;">
                                    <%# Eval("Message") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <br />
            </div>
            <br />
            <%--<asp:Label ID="lbl1" runat="server" CssClass="LblClass"></asp:Label>--%>

            <div id="footer">
                <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics | Tracking Daily Count. </div>
            </div>
        </div>
    </form>
</body>
</html>
