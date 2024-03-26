<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Live.aspx.cs" Inherits="GoogleFirstPage.RapidTrackingSERPs.Live" Title="SERP - Elements and Classic Links" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Rapid Tracking - Elements and Classic Links</title>

    <meta charset="utf-8" />

    <link href="Content/bootstrap.cosmo.min.css" rel="stylesheet" />
    <link href="Content/StyleSheet.css" rel="stylesheet" />

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

    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.4.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=gvtracking]').footable();
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

        $(function () {
            $('#txtlivesearch').blur(function () {                     
                $(this).val(
                    $.trim($(this).val())
                );
            });
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
            width: 100%;
            padding: 10px;
            margin: 0px auto;
            margin-left: -10px;
        }

        table {
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
                    background: #25b1a2;
                    font-size: 15px;
                    height: 35px;
                    padding: 4px;
                    font-family: Calibri;
                    min-width: 100px;
                    max-width: 600px;
                }

                .gridview4 td {
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
                        background-color: #e4e8ef;
                    }

                .gridview4 tr.even td {
                    background: #e4e8ef;
                    font-family: Calibri;
                }

                    .gridview4 tr.even td:hover {
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
                    background: #25b1a2;
                    font-size: 15px;
                    height: 35px;
                    padding: 4px;
                    font-family: Calibri;
                    min-width: 100px;
                    max-width: 600px;
                }

                .gridview4 td {
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
                        background-color: #e4e8ef;
                    }

                .gridview4 tr.even td {
                    background: #e4e8ef;
                    font-family: Calibri;
                }

                    .gridview4 tr.even td:hover {
                        background-color: #ffffff;
                    }
        }
    </style>

    <style type="text/css">
        .gridview7 {
            font-family: Calibri;
            background-color: #FFFFFF;
            width: 100%;
        }

            .gridview7 th {
                background: #a8b0b7;
                font-size: 15px;
                height: 35px;
                padding: 4px;
                font-family: Calibri;
                word-wrap: break-word;
            }

            .gridview7 tr {
                background-color: #FFFFFF;
                color: #333333;
                font-family: Calibri;
                font-size: 14px;
                font-weight: bold;
                min-width: 150px;
                word-wrap: break-word;
            }

                .gridview7 tr:hover {
                    background: #FFF8DC;
                    font-weight: bold;
                    word-wrap: break-word;
                }

                .gridview7 tr.even tr {
                    background: #FFFFFF;
                    font-family: Calibri;
                    font-weight: bold;
                    word-wrap: break-word;
                }

                    .gridview7 tr.even tr:hover {
                        background: #FFF8DC;
                        font-weight: bold;
                        word-wrap: break-word;
                    }

            .gridview7 div {
                max-width: 1000px;
                min-width: 700px;
                height: 10px;
            }

                .gridview7 div.color {
                    max-width: 500px;
                    min-width: 250px;
                    height: 10px;
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

</head>
<body style="background-color: white">

    <script src="js/custom.js"></script>

    <script src="js/jquery.js"></script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

        <div class="header1" style="width: 2500px;">
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

                        <!-- end navigation -->
                    </div>
                </div>
            </div>
        </div>
        <div class="containerDB">
            <div class="header3">
                <div style="margin-top: 12px;">
                    &nbsp;<span class="auto-style1">Keyword :</span>&nbsp;&nbsp;
                 <asp:TextBox ID="txtlivesearch" runat="server" Height="30px" Width="300px" CssClass="text1" OnTextChanged="txtlivesearch_TextChanged" AutoPostBack="false" placeholder="Search a keyword"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req1" runat="server" ErrorMessage="*" ControlToValidate="txtlivesearch" Text="*"></asp:RequiredFieldValidator>
                    &nbsp; <strong><span class="auto-style2">Search Engine :</span></strong>&nbsp;&nbsp; 
                   <asp:DropDownList ID="ddlseid" CssClass="text1" Height="30px" Width="300px" runat="server">
                   </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Text="*" ControlToValidate="ddlseid" InitialValue="----Select----" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;
                <asp:Button runat="server" Text="Submit" CssClass="btnlf" Height="27px" ID="btngetdata" Width="120px" OnClick="btngetdata_Click" />
                    <br />
                    <br />
                    <br />
                </div>
            </div>
            <br />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Size="Large" Font-Bold="true" Text="Elements and Classic Links"></asp:Label><br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div id="oxydiv1" runat="server">
                <table border="1" style="width: 800px">
                    <tr>
                        <td><b>Job ID from Oxylabs</b> </td>
                        <td>
                            <asp:Label ID="oxylabsjobid" runat="server" Visible="true" ForeColor="blue" Font-Size="15px" Font-Bold="true"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>No. Of Classic Links</b> </td>
                        <td>
                            <asp:Label ID="resultscnt" runat="server" Visible="true" ForeColor="blue" Font-Size="15px" Font-Bold="true"></asp:Label></td>
                    </tr>
                </table>
            </div>

            <div class="wrapper">
                <asp:Label ID="lblalllinks" runat="server" ForeColor="Blue" Font-Size="Small" Font-Bold="true"></asp:Label>
                <br />
                <br />
                <asp:GridView ID="gridviewlive" AutoGenerateColumns="false" CssClass="footable" HeaderStyle-HorizontalAlign="Right" Font-Bold="true" HeaderStyle-Font-Bold="true" runat="server" OnRowDataBound="gridviewlive_RowDataBound" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" HeaderStyle-Height="30px" RowStyle-Width="1px">
                    <Columns>
                        <asp:BoundField HeaderText="CL Position" DataField="Position" ItemStyle-Width="100px" />
                        <asp:BoundField HeaderText="Elements" DataField="Block Type" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="URLs">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <a href="<%#DataBinder.Eval(Container.DataItem,"URL") %>" target="_blank"><%#DataBinder.Eval(Container.DataItem,"URL") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Title" DataField="Title" />
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
                <%--<div style="font-weight: bold; font-size: 14px;">© 2020 Pi Datametrics | RapidTracking Live Search.</div>--%>
                <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics | SERP - Elements and Classic Links.</div>
            </div>
        </div>
    </form>
</body>
</html>
