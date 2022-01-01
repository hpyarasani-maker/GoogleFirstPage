<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GoogleFirstPage.Trackingstatus.WebForm1" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale = 1, shrink-to-fit=no" />
    <script src="https //code.jquery.com/jquery-3.3.1.slim.min.js integrity"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js" integrity="sha512-bLT0Qm9VnAYZDflyKcBaQ2gg0hSYNQrJ8RilYldYQ1FxQYoCLtUjuuRuZo+fjqhx/qtq/1itJ0C2ejDxltZVFg==" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

    <link href="../CSS/StyleSheet2.css" rel="stylesheet" />

    <script src="../js/jquery-1.10.2.js"></script>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../CSS/menu.css" rel="stylesheet" />

    <link href="../CSS/Newstyle.css" rel="stylesheet" />

    <link rel="icon" href="../images/default-avatar-logo.png" type="image/x-icon" />
    <style type="text/css">
        .boxDiv {
            //min-height:300px;
            height: 300px;
            padding-left: 20px;
            padding-top: 10px;
            background-color: #ffffff;
            font-weight: bold;
            color: black;
            width: 450px;
            margin: 10px;
            border: 1px solid #1583c2;
            enable-background: new;
        }
        table, th, td {
            border: 1px solid black;
        }
    </style>


</head>
<body style="background-color: white">
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
        <br />
        <br />
        <div>
            <div class="row">
                <div class="col-lg-3 col-md-6">
                    <div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="boxDiv">
                        <label style="font-family:Calibri;font-size:large;color:blue;">Server - 1</label>
                        <table runat="server" id="server1" style="width:300px;font-family:Tahoma;">
                            <tr>
                                <th style="width:100px;">Apps</th>
                                <th>Google UK - 58</th>
                                <th>Status</th>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Sending</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Receiving</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Jobid's</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Proxies</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Total</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Received</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Remaining</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="boxDiv">
                      <label style="font-family:Calibri;font-size:large;color:blue;">Server - 2</label>
                        <table style="width:300px;font-family:Tahoma;">
                            <tr>
                                <th style="width:100px;">Apps</th>
                                <th>Google Mobile</th>
                                <th>Status</th>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Sending</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Receiving</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Jobid's</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Proxies</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Total</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Received</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Remaining</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-6">
                    <div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="boxDiv">
                        <label style="font-family:Calibri;font-size:large;color:blue;">Server - 3</label>
                        <table style="width:300px;font-family:Tahoma;">
                            <tr>
                                <th style="width:100px;">Apps</th>
                                <th>Google US - 1</th>
                                <th>Status</th>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Sending</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Receiving</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Jobid's</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Proxies</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Total</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Received</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Remaining</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="boxDiv">
                        <label style="font-family:Calibri;font-size:large;color:blue;">Server - 4</label>
                        <table style="width:300px;font-family:Tahoma;">
                            <tr>
                                <th style="width:100px;">Apps</th>
                                <th>Google Desktop</th>
                                <th>Status</th>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Sending</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Receiving</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Jobid's</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Proxies</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Total</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Received</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Remaining</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-6">
                    <div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="boxDiv">
                        <label style="font-family:Calibri;font-size:large;color:blue;">Server - 5</label>
                        <table style="width:300px;font-family:Tahoma;">
                            <tr>
                                <th style="width:100px;">Apps</th>
                                <th>Google US(Mobile) - 102</th>
                                <th>Status</th>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Sending</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Receiving</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Jobid's</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Proxies</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Total</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Received</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Remaining</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="boxDiv">
                        <label style="font-family:Calibri;font-size:large;color:blue;">Server - 6</label>
                        <table style="width:300px;font-family:Tahoma;">
                            <tr>
                                <th style="width:100px;">Apps</th>
                                <th>Google UK(Mobile) - 106</th>
                                <th>Status</th>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Sending</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Receiving</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Jobid's</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Proxies</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Total</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Received</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                            <tr>
                                <td style="padding:2px 2px 2px 2px;">Remaining</td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                                <td style="padding:2px 2px 2px 2px;"></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />

        <div id="footer">
            <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics | Tracking pipeline.</div>
        </div>
    </form>
</body>
</html>
