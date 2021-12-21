<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="amazon.aspx.cs" Inherits="GoogleFirstPage.amazon" Title="Amazon Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Amazon Search</title>
    <link rel="icon" href="images/default-avatar-logo.png" type="image/x-icon" />
    <link href="~/CSS/StyleSheet2.css" rel="stylesheet" />
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />
    <link href="CSS/Newstyle.css" rel="stylesheet" />

    <link href="CSS/menu.css" rel="stylesheet" />

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
    <script type="text/javascript">
        google.load("visualization", "1", { packages: ['corechart', 'bar'] });
        //  google.setOnLoadCallback(drawChart);
        $(function () {
            debugger;
            $.ajax({

                type: 'POST',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                url: 'amazon.aspx/GetPiechartData',
                data: '{}',
                success: function (response) {
                    drawchart(response.d); // calling method
                },

                //error: function () {
                //    alert("Error loading data...........");
                //}
            });
        })

        function drawchart(dataValues) {
            // Callback that creates and populates a data table,
            // instantiates the pie chart, passes in the data and
            // draws it.
            var data = new google.visualization.DataTable();

            data.addColumn('string', 'Brand');
            data.addColumn('number', 'Percentage');

            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].Brand, dataValues[i].Count]);
            }
            // Instantiate and draw our chart, passing in some options
            var chart = new google.visualization.PieChart(document.getElementById('chart'));

            chart.draw(data,
                {
                    position: "top",
                    fontsize: "14px",
                    chartArea: { width: '50%' },
                });
        }
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
<body style="background-color: white;">

    <script src="js/custom.js"></script>

    <script src="js/jquery.js"></script>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

        <div class="header1">
            <div class="header-menu">
                <a href="Dashboard.aspx" title="Pi-Datametrics DashBoard Home">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/PILogo_1.jpg" CssClass="imgLogo" Height="51px" Width="54px" /></a>
                <div class="container">
                    <div class="navbar navbar-static-top">
                        <div>
                            <div class="navigation">
                                <nav style="margin-left: 950px; margin-top: 10px">
                                    <ul class="nav topnav bold">
                                        <li class="dropdown">
                                            <a href="#">Live Search</a>&nbsp;<ul style="display: none;" class="dropdown-menu bold">
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
            <div class="header2">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                     <strong><span class="auto-style2">Enter Keyword :</span></strong>&nbsp;&nbsp; 
                <asp:TextBox ID="kwd_txt" runat="server" CssClass="text1" placeholder="keyword" Width="360px" Height="28px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="kwd_txt" Text="*"></asp:RequiredFieldValidator>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                 <strong><span class="auto-style2">SE :</span></strong>&nbsp;&nbsp; 
               <asp:DropDownList ID="DropDownList1" runat="server" CssClass="text1" Height="30px" Width="360px">
                   <asp:ListItem Text="Amazon UK" Value="1"></asp:ListItem>
                   <asp:ListItem Text="Amazon UK Mobile" Value="2"></asp:ListItem>
                   <asp:ListItem Text="Amazon FR" Value="3"></asp:ListItem>
                   <asp:ListItem Text="Amazon DE" Value="4"></asp:ListItem>
                   <asp:ListItem Text="Amazon IT" Value="5"></asp:ListItem>
                   <asp:ListItem Text="Amazon NL" Value="6"></asp:ListItem>
                   <asp:ListItem Text="Amazon ES" Value="7"></asp:ListItem>
                   <asp:ListItem Text="Amazon US" Value="8"></asp:ListItem>
               </asp:DropDownList>
                <asp:Button runat="server" Text="Submit" CssClass="btnlf" Height="26px" ID="btnamazzonresult" Width="120px" OnClick="btnamazzonresult_Click" />

            </div>
            <br />
            <div>
                <asp:Panel ID="panelChart" runat="server" Visible="false">
                    <div id="chart" style="border: 2px solid skyblue; width: 1000px; height: 500px; margin-left: 150px; border-radius: 3px 3px"></div>
                </asp:Panel>
            </div>
            <br />
            <br />
            <div>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="True" CssClass="gridview4" Font-Bold="true" AlternatingRowStyle-CssClass="even" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="GridView2_RowDataBound">
                    <RowStyle HorizontalAlign="Left" />

                </asp:GridView>

            </div>
            <br />

            <div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" CssClass="gridview4" Font-Bold="true" AlternatingRowStyle-CssClass="even" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="GridView1_RowDataBound">
                    <RowStyle HorizontalAlign="Left" />
                </asp:GridView>

            </div>

            <br />
            <br />
            <div id="footer">
                <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics | Amazon Search.</div>
            </div>
        </div>
    </form>
</body>
</html>
