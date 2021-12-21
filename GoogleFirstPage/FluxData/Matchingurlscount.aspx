<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Matchingurlscount.aspx.cs" Inherits="GoogleFirstPage.FluxData.Matchingurlscount" Title="FluxResults MatchingURLs Count is 0"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">

    <title>FluxResults MatchingURLs Count is 0</title>

    <link href="../Content/bootstrap.cosmo.css" rel="stylesheet" />
    <link href="../Content/bootstrap.cosmo.min.css" rel="stylesheet" />
    
    <link href="Content/StyleSheet.css" rel="stylesheet" />


    <link href="../CSS/StyleSheet2.css" rel="stylesheet" />

    <script src="../js/jquery-1.10.2.js"></script>

    <script src="../js/jquery-1.10.2.min.js"></script>


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
            $('[id*=gvflux]').footable();
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



    <style type="text/css">
        .auto-style1 {
            font-size: small;
            font-weight: bold;
        }

        .auto-style2 {
            font-size: small;
        }

        body .wrapper {
            background: #FFF;
            //width: 960px;
            //width: 90%;
            width: 100%;
            padding: 10px;
            margin: 0px auto;
            margin-left: -10px;
        }

        table {
            //width: 100%;
        }

        .header3 {
            padding: 20px;
            text-align: center;
            font-family: Arial;
            font-size: 15px;
            //height: 60px;
            background-color: #e4e8ef;
            //padding-top: 30px;
            height: 85px;
            padding: 10px 10px 10px 10px;
            border-radius: 10px 10px;
        }

        @media (min-width: 320px) and (max-width: 480px) {
            .wrapper {
                max-width: 300px;
                width: 300px;
            }

            .header2 {
                padding: 20px;
                text-align: center;
                font-family: Arial;
                font-size: 12px;
                height: 40px;
                background-color: #e4e8ef;
                padding-top: 30px;
            }

            .gridview4 {
                font-family: Calibri;
                background-color: #FFFFFF;
                width: 100%;
            }

                .gridview4 th {
                    //background: #a8b0b7;
                    background: #25b1a2;
                    font-size: 15px;
                    height: 35px;
                    padding: 4px;
                    font-family: Calibri;
                    min-width: 100px;
                    max-width: 600px;
                }

                .gridview4 td {
                    //background: #e4e8ef;
                    color: #333333;
                    font-family: Calibri;
                    font-size: 14px;
                    font-weight: bold;
                    padding: 4px;
                    color: black;
                    height: 35px;
                    min-width: 500px;
                    max-width: 600px;
                    word-wrap: break-word;
                }

                    .gridview4 td:hover {
                        //background-color: #FFFFFF;
                        background-color: #e4e8ef;
                        //background-color:rgb(231, 51, 99);
                    }

                .gridview4 tr.even td {
                    //background: #FFFFFF;
                    background: #e4e8ef;
                    font-family: Calibri;
                }

                    .gridview4 tr.even td:hover {
                        //background-color: #a8b0b7;
                        background-color: #ffffff;
                    }
        }

        @media (min-width: 481px) and (max-width: 767px) {
            .wrapper {
                max-width: 480px;
                width: 480px;
            }

            .header2 {
                padding: 20px;
                text-align: center;
                font-family: Arial;
                font-size: 12px;
                height: 40px;
                background-color: #e4e8ef;
                padding-top: 30px;
            }


            .gridview4 {
                font-family: Calibri;
                background-color: #FFFFFF;
                width: 100%;
            }

                .gridview4 th {
                    //background: #a8b0b7;
                    background: #25b1a2;
                    font-size: 15px;
                    height: 35px;
                    padding: 4px;
                    font-family: Calibri;
                    min-width: 100px;
                    max-width: 600px;
                }

                .gridview4 td {
                    //background: #e4e8ef;
                    color: #333333;
                    font-family: Calibri;
                    font-size: 14px;
                    font-weight: bold;
                    padding: 4px;
                    color: black;
                    height: 35px;
                    min-width: 100px;
                    max-width: 600px;
                }

                    .gridview4 td:hover {
                        //background-color: #FFFFFF;
                        background-color: #e4e8ef;
                        //background-color:rgb(231, 51, 99);
                    }

                .gridview4 tr.even td {
                    //background: #FFFFFF;
                    background: #e4e8ef;
                    font-family: Calibri;
                }

                    .gridview4 tr.even td:hover {
                        //background-color: #a8b0b7;
                        background-color: #ffffff;
                    }
        }
    </style>

    <style>
        .btnlf {
            //background: transparent;
            //background: #ff8000;
            background: #1583c2;
            //border: 1.5px solid #1583c2;
            border: 1px solid #1583c2;
            width: 140px;
            height: 18px;
            font-size: 15px;
            border-radius: 4px;
            display: inline-block;
            font-family: Calibri;
            font-weight: bold;
            font-style: normal;
            //color: #1583c2;
            //color:#ff6a00;
            color: #FFFFFF;
            margin-left: 50px;
        }

            .btnlf:hover {
                //background-color: #1583c2;
                //background-color: #ff8000;
                background-color: white;
                border: 1px solid #1583c2;
                text-decoration: none;
                color: #1583c2;
                margin-bottom: 0px;
                //box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
            }
    </style>

</head>

<body style="background-color: white">

    <script src="js/custom.js"></script>

    <script src="js/jquery.js"></script>

    <form id="form1" runat="server">
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
                                        <li class="dropdown">
                                            <a href="../Dashboard.aspx">FluxData</a>&nbsp;<ul style="display: none;" class="dropdown-menu bold">
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
                <div class="header3"> 
                    <p></p>
                     <strong><span class="auto-style2">FluxResults MatchingURLs Count = 0 :</span></strong>&nbsp;&nbsp; 
                &nbsp;&nbsp;&nbsp;&nbsp;
                             <span class="auto-style1">Date :</span>&nbsp;&nbsp;
                             <asp:TextBox ID="txtDate" runat="server" placeholder="Date" CssClass="text1" Height="25px" Width="85px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calender" runat="server"  StartDate="2020-08-09" PopupButtonID="img_pop" TargetControlID="txtDate" Format="yyyy-MM-dd"/>
                &nbsp;<asp:ImageButton ID="img_pop" ImageUrl="~/images/calendarnew.png" ImageAlign="Bottom" runat="server" Height="25px" Width="30px" />
                <asp:Button runat="server" Text="Submit" CssClass="btnlf" Height="27px" ID="btnfluxmatching" Width="120px" OnClick="btnfluxmatching_Click" />
                <%--<div style="margin-left:1000px;margin-top:-30px;">
                <asp:ImageButton ID="dwndcsv" runat="server" ImageUrl="~/images/excel.png" CssClass="imgcsvdownloadNew1" ToolTip="Generate Excel" OnClick="dwndcsv_Click" />
                </div>--%>
            </div>

            <br />
            <asp:Label ID="lbl" runat="server" Font-Size="12" ForeColor="Chocolate" Visible="false"></asp:Label><br />
            <br />
            <br />
            <div class="wrapper">
                <asp:Label ID="lblalllinks" runat="server"></asp:Label>
                <asp:GridView ID="gvmatching" runat="server"  CssClass="table table-condensed table-striped table-bordered" AutoGenerateColumns="false" Font-Size="Small"  GridLines="Vertical" BackColor="White" HeaderStyle-BackColor="SlateBlue" HeaderStyle-ForeColor="#660033"  OnRowDataBound="gvmatching_RowDataBound" OnPageIndexChanging="gvmatching_PageIndexChanging" AllowPaging="true" PageSize="200" HeaderStyle-Height="30px">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="SEID">
                            <ItemTemplate>
                                <%# Eval("SEID") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Keyword" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <%# Eval("keyword") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Current_Date">
                            <ItemTemplate>
                                <%# Eval("C_Date") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="C_URLCount">
                            <ItemTemplate>
                                <%# Eval("C_URLCount") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Previous_Date">
                            <ItemTemplate>
                                <%# Eval("P_Date") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P_URLCount">
                            <ItemTemplate>
                                <%# Eval("P_URLCount") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Matching_URLs">
                            <ItemTemplate>
                                <%# Eval("Matching_URLs") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Top100_ExactMatch">
                            <ItemTemplate>
                                <%# Eval("Top100_ExactMatch") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="C_NewURLs">
                            <ItemTemplate>
                                <%# Eval("C_NewURLs") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P_NewURLs">
                            <ItemTemplate>
                                <%# Eval("P_NewURLs") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total_NewURLs">
                            <ItemTemplate>
                                <%# Eval("Total_NewURLs") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="C_TotalMovement">
                            <ItemTemplate>
                                <%# Eval("C_TotalMovement") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P_TotalMovement">
                            <ItemTemplate>
                                <%# Eval("P_TotalMovement") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total_Movement">
                            <ItemTemplate>
                                <%# Eval("Total_Movement") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FluxData_Average">
                            <ItemTemplate>
                                <%# Eval("FluxData_Average") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="C_Top20_Movement">
                            <ItemTemplate>
                                <%# Eval("C_Top20_Movement") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P_Top20_Movement">
                            <ItemTemplate>
                                <%# Eval("P_Top20_Movement") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Top20_ExactMatch">
                            <ItemTemplate>
                                <%# Eval("Top20_ExactMatch") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView><br /><br />

            </div>
            <br />
            <br />
            <br />
            <asp:Label ID="lbl1" runat="server" CssClass="LblClass"></asp:Label>
            <br />
            <br />
            <div id="footer">
                <div style="font-weight:bold;font-size:14px;font-family:Calibri;">© <%= DateTime.Now.Year %> Pi Datametrics | FluxResults MatchingURLs Count is 0.</div>
            </div>
        </div>
    </form>
</body>
</html>
