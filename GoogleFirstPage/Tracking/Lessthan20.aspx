<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lessthan20.aspx.cs" Inherits="GoogleFirstPage.Oxylabs.Lessthan20threads" Title="Lessthan20" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Lessthan20 Threads</title>

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
        .table table tbody tr td a,
        .table table tbody tr td span {
            position: relative;
            float: left;
            padding: 6px 12px;
            margin-left: -1px;
            line-height: 1.42857143;
            color: #337ab7;
            text-decoration: none;
            background-color: #fff;
            border: 1px solid #333333;
            font-family: Calibri;
        }

        .table table > tbody > tr > td > span {
            z-index: 3;
            color: #fff;
            cursor: default;
            background-color: #337ab7;
            border-color: #333333;
        }

        .table table > tbody > tr > td:first-child > a,
        .table table > tbody > tr > td:first-child > span {
            margin-left: 0;
            border-top-left-radius: 4px;
            border-bottom-left-radius: 4px;
        }

        .table table > tbody > tr > td:last-child > a,
        .table table > tbody > tr > td:last-child > span {
            border-top-right-radius: 4px;
            border-bottom-right-radius: 4px;
        }

        .table table > tbody > tr > td > a:hover,
        .table table > tbody > tr > td > span:hover,
        .table table > tbody > tr > td > a:focus,
        .table table > tbody > tr > td > span:focus {
            z-index: 2;
            color: #23527c;
            background-color: #eee;
            border-color: #333333;
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
                                <nav style="margin-left: 950px; margin-top: 10px">
                                    <ul class="nav topnav bold">
                                        <li class="dropdown">
                                            <a href="Dailycount.aspx">Back</a>&nbsp;<ul style="display: none;" class="dropdown-menu bold">
                                                <li><a href="Dailycount.aspx">BACK TO DASHBOARD</a></li>
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
        <div style="margin-left: 50px; margin-top: -90px;">
            <br />
            <br />
            <div>
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="lblalllinks" runat="server" ForeColor="Blue" Font-Size="Small" Font-Bold="true"></asp:Label><br />

                <asp:Label ID="lbl1" runat="server" CssClass="LblClassNew"></asp:Label>
                <br />

                <div>
                    <div class="panel-group">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="border-bottom:1px solid black;"><b style="font-family: Cambria; font-size: large">Download Lessthan20 Report</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:LinkButton ID="lnklessthan20" runat="server" OnClick="lnklessthan20_Click" Font-Bold="true" Font-Size="Large">Lessthan20 Csv</asp:LinkButton></div>
                    </div>
                </div>
                <br />
                <br />
                <asp:GridView ID="gvlessthan20" AutoGenerateColumns="false" runat="server" GridLines="Vertical" Font-Bold="true" AlternatingRowStyle-BackColor="#f5f5f5" OnRowDataBound="gvlessthan20_RowDataBound" Font-Size="Small" AllowPaging="true" CssClass="footable" HeaderStyle-BackColor="#e4e8ef" OnPageIndexChanging="gvlessthan20_PageIndexChanging" PageSize="30">
                    <Columns>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <div style="width: 80px;">
                                    <%# Eval("Date") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <div style="width: 50px;">
                                    <%# Eval("Total") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <div style="width: 30px;">
                                    <%# Eval("ID") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 1">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread1") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 2">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread2") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 3">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread3") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 4">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread4") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 5">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread5") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Thread 6">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread6") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 7">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread7") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 8">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread8") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 9">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread9") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 10">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread10") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 11">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread11") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 12">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread12") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 13">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread13") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 14">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread14") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 15">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread15") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 16">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread16") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 17">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread17") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 18">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread18") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 19">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread19") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 20">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread20") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 21">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread21") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 22">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread22") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 23">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread23") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 24">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread24") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 25">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread25") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 26">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread26") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 27">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread27") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 28">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread28") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 29">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread29") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thread 30">
                            <ItemTemplate>
                                <div style="width: 70px;">
                                    <%# Eval("Thread30") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

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
                <div style="font-weight: bold; font-size: 14px;font-family:Calibri;">© <%= DateTime.Now.Year %> Pi Datametrics | Lessthan20. </div>
            </div>
        </div>
    </form>
</body>
</html>
