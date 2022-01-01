<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pipeline.aspx.cs" Inherits="GoogleFirstPage.Trackingstatus.pipeline" %>

<!DOCTYPE html>

<head runat="server">

    <title>Tracking Pipeline Status</title>


    <link href="~/CSS/StyleSheet2.css" rel="stylesheet" />

    <link href="Content/bootstrap.cosmo.min.css" rel="stylesheet" />
    <link href="Content/StyleSheet.css" rel="stylesheet" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />

    <link href="../CSS/menu.css" rel="stylesheet" />
    <link href="css/CssRespnsive.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/StyleSheet2.css" rel="stylesheet" />
    <link href="css/StyleSheet3.css" rel="stylesheet" />
    <link href="../CSS/NewGdstyle.css" rel="stylesheet" />

    <link rel="icon" href="../images/default-avatar-logo.png" type="image/x-icon" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link media="screen" rel="stylesheet" href='http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' />
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>

    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.4.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=GridView1]').footable();
            $('[id*=GridView2]').footable();
            $('[id*=GridView3]').footable();
            $('[id*=GridView4]').footable();
            $('[id*=GridView5]').footable();
            $('[id*=GridView6]').footable();
        });
    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>


    <style type="text/css">
        .wrapper_New {
            width: 100%;
            clear: both;
        }

        .wrapper_New1 {
            width: 100%;
            clear: both;
            margin-top: 330px;
        }

        .first_1 {
            width: 20%;
            float: left;
            border-radius: 10px;
            border: 1px solid black;
            height: 300px;
            padding: 20px;
        }

        .second_2 {
            width: 20%;
            float: left;
            margin-left: 100px;
            height: 300px;
            border-radius: 10px;
            border: 1px solid black;
            padding: 20px;
        }

        .third_3 {
            width: 20%;
            float: left;
            margin-left: 60px;
            border-radius: 10px;
            height: 300px;
            border: 1px solid black;
            padding: 20px;
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
                <a href="../Dashboard.aspx" title="Pi-Datametrics DashBoard Home">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/PILogo_1.jpg" CssClass="imgLogo" Height="51px" Width="54px" /></a>
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
            <%--<div class="header2">
            </div>--%>

            <br />
            <asp:Label ID="lbl" runat="server" Font-Size="12" ForeColor="Chocolate" Visible="false"></asp:Label><br />
            <br />
            <div style="margin-left: 100px;">
                <div class="wrapper_New">
                    <table style="width: 100%">
                        <tr>
                            <td style="border: 1px solid black">
                                <label style="font-family:Cambria;font-size:large;color:blue;">Server - 1</label><br /><br />
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="footable" HeaderStyle-HorizontalAlign="Right" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" HeaderStyle-Height="30px">
                                    <Columns>
                                        <asp:BoundField HeaderText="Seid" DataField="Seid" />
                                        <asp:BoundField HeaderText="Total" DataField="Total" />
                                        <asp:BoundField HeaderText="Received" DataField="Received" />
                                        <asp:BoundField HeaderText="Remaining" DataField="Remaining" />
                                    </Columns>
                                </asp:GridView>
                            </td>

                            <td style="border: 1px solid black;">
                                <label style="font-family:Cambria;font-size:large;color:blue;">Server - 2</label><br /><br />
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="footable" HeaderStyle-HorizontalAlign="Right" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" HeaderStyle-Height="30px">
                                    <Columns>
                                        <asp:BoundField HeaderText="Seid" DataField="Seid" />
                                        <asp:BoundField HeaderText="Total" DataField="Total" />
                                        <asp:BoundField HeaderText="Received" DataField="Received" />
                                        <asp:BoundField HeaderText="Remaining" DataField="Remaining" />
                                    </Columns>
                                </asp:GridView>

                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;">
                                <label style="font-family:Cambria;font-size:large;color:blue;">Server - 3</label><br /><br />
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" CssClass="footable" HeaderStyle-HorizontalAlign="Right" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" HeaderStyle-Height="30px">
                                    <Columns>
                                        <asp:BoundField HeaderText="Seid" DataField="Seid" />
                                        <asp:BoundField HeaderText="Total" DataField="Total" />
                                        <asp:BoundField HeaderText="Received" DataField="Received" />
                                        <asp:BoundField HeaderText="Remaining" DataField="Remaining" />
                                    </Columns>
                                </asp:GridView>

                            </td>
                            <td style="border: 1px solid black;">
                                <label style="font-family:Cambria;font-size:large;color:blue;">Server - 4</label><br /><br />
                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" CssClass="footable" HeaderStyle-HorizontalAlign="Right" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" HeaderStyle-Height="30px">
                                    <Columns>
                                        <asp:BoundField HeaderText="Seid" DataField="Seid" />
                                        <asp:BoundField HeaderText="Total" DataField="Total" />
                                        <asp:BoundField HeaderText="Received" DataField="Received" />
                                        <asp:BoundField HeaderText="Remaining" DataField="Remaining" />
                                    </Columns>
                                </asp:GridView>

                            </td>
                        </tr>
                        <tr>
                            <td style="border: 1px solid black;">
                                <label style="font-family:Cambria;font-size:large;color:blue;">Server - 5</label><br /><br />
                                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="false" CssClass="footable" HeaderStyle-HorizontalAlign="Right" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" HeaderStyle-Height="30px">
                                    <Columns>
                                        <asp:BoundField HeaderText="Seid" DataField="Seid" />
                                        <asp:BoundField HeaderText="Total" DataField="Total" />
                                        <asp:BoundField HeaderText="Received" DataField="Received" />
                                        <asp:BoundField HeaderText="Remaining" DataField="Remaining" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td style="border: 1px solid black;">
                                <label style="font-family:Cambria;font-size:large;color:blue;">Server - 6</label><br /><br />
                                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="false" CssClass="footable" HeaderStyle-HorizontalAlign="Right" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" HeaderStyle-Height="30px">
                                    <Columns>
                                        <asp:BoundField HeaderText="Seid" DataField="Seid" />
                                        <asp:BoundField HeaderText="Total" DataField="Total" />
                                        <asp:BoundField HeaderText="Received" DataField="Received" />
                                        <asp:BoundField HeaderText="Remaining" DataField="Remaining" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>

                </div>
            </div>
            <br />
            <br />
            <div class="wrapper">
                <div style="width: 200px; margin-left: 400px; margin-top: 20px">
                    <%--<asp:Label ID="lbldate" runat="server" Text="2017-06-09" CssClass="LblClass"></asp:Label><br /><br />--%>

                    <br />
                    <br />
                    <br />
                    <br />

                    <br />
                    <br />

                    <br />
                    <br />


                    <br />
                    <br />

                </div>
            </div>


            <br />
            <br />
            <br />
            <asp:Label ID="lbl1" runat="server" CssClass="LblClass"></asp:Label>
            <br />
            <br />
            <div id="footer">
                <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics | Tracking Pipeline Status.</div>
            </div>

        </div>
    </form>
</body>
