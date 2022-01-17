<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="GoogleFirstPage.Trackingstatus.WebForm2" %>--%>

<%@ Import Namespace="GoogleFirstPage.Trackingstatus" %>

<!DOCTYPE html>

<%@ Page Language="C#" Debug="true" %>

<script runat="server" language="C#">
    void Page_Load(object sender, EventArgs e)
    {
        TrackingModel tm = new TrackingModel();
        var a = tm.GetTrackingAll();
        ArrayList al = tm.servers();
        for (int i = 0; i < a.Count; i++)
        {
            StringBuilder sb = new StringBuilder();
            bool st = false;
            sb = Getdata(i, a);
            switch (i)
            {
                case 0:
                    st = tm.PingServer((byte[])al[i]);
                    if (st == true) { Label1.Text = "online"; } else if (st == false) { Label1.Text = "offline"; }
                    PlaceHolder1.Controls.Add(new LiteralControl { Text = sb.ToString() });
                    PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                    PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                    break;
                case 1:
                    st = tm.PingServer((byte[])al[i]);
                    if (st == true) { Label2.Text = "online"; } else if (st == false) { Label2.Text = "offline"; }
                    PlaceHolder2.Controls.Add(new LiteralControl { Text = sb.ToString() });
                    PlaceHolder2.Controls.Add(new LiteralControl("<br />"));
                    PlaceHolder2.Controls.Add(new LiteralControl("<br />"));
                    break;
                case 2:
                    st = tm.PingServer((byte[])al[i]);
                    if (st == true) { Label3.Text = "online"; } else if (st == false) { Label3.Text = "offline"; }
                    PlaceHolder3.Controls.Add(new LiteralControl { Text = sb.ToString() });
                    PlaceHolder3.Controls.Add(new LiteralControl("<br />"));
                    PlaceHolder3.Controls.Add(new LiteralControl("<br />"));
                    break;
                case 3:
                    st = tm.PingServer((byte[])al[i]);
                    if (st == true) { Label4.Text = "online"; } else if (st == false) { Label4.Text = "offline"; }
                    PlaceHolder4.Controls.Add(new LiteralControl { Text = sb.ToString() });
                    PlaceHolder4.Controls.Add(new LiteralControl("<br />"));
                    PlaceHolder4.Controls.Add(new LiteralControl("<br />"));
                    break;
                case 4:
                    st = tm.PingServer((byte[])al[i]);
                    if (st == true) { Label5.Text = "online"; } else if (st == false) { Label5.Text = "offline"; }
                    PlaceHolder5.Controls.Add(new LiteralControl { Text = sb.ToString() });
                    PlaceHolder5.Controls.Add(new LiteralControl("<br />"));
                    PlaceHolder5.Controls.Add(new LiteralControl("<br />"));
                    break;
                case 5:
                    st = tm.PingServer((byte[])al[i]);
                    if (st == true) { Label6.Text = "online"; } else if (st == false) { Label6.Text = "offline"; }
                    PlaceHolder6.Controls.Add(new LiteralControl { Text = sb.ToString() });
                    PlaceHolder6.Controls.Add(new LiteralControl("<br />"));
                    PlaceHolder6.Controls.Add(new LiteralControl("<br />"));
                    break;
            }

        }
    }

    public StringBuilder Getdata(int i, List<TrackingModel> a)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table border = '2'>");
        sb.Append("<tr>");
        sb.Append("<th>Apps</th>");
        sb.Append("<th style='width:120px'>" + a[i].Seid.ToString() + "</th>");
        sb.Append("<th>Status</th>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'>sending</td>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'></td>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'>Receiving</td>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'></td>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'>Jobid's</td>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'></td>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'>Proxies</td>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'></td>");
        sb.Append("<td style='width:100px;border: 1px solid #ccc'></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        if (a[i].Remaining.ToString() == "0")
        {
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Total</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Total + "</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Completed</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Received</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Received + "</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Completed</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Remaining</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Remaining + "</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Completed</td>");
            sb.Append("</tr>");
        }
        else
        {
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Total</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Total + "</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Running</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Received</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Received + "</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Running</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Remaining</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Remaining + "</td>");
            sb.Append("<td style='width:100px;border: 1px solid #ccc'>Running</td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        return sb;
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Tracking</title>

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

    <style type="text/css">
        .boxDiv {
            //min-height:300px;
            //height: 300px;
            //padding-left: 20px;
            //padding-top: 10px;
            background-color: #ffffff;
            font-weight: bold;
            color: black;
            //width: 450px;
            margin: 10px;
            //border: 1px solid #1583c2;
            enable-background: new;
        }

        table, th, td {
            //border: 1px solid black;
        }
    </style>

</head>
<body>
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
        <br />
        <br />
        <div class="containerDB">
            <div style="margin-left: 300px;">
                <table style="width: 70%">
                    <tr>
                        <td style='border: 1px solid #ccc;'>
                            <div style="background-color: #faf6f6;">
                                <label style="font-family: Calibri; font-size: large; color: blue; font-size: 20px;">Server - 1</label>
                                <p>
                                    <asp:Label ID="Label1" runat="server" ForeColor="Green" Font-Size="Large" Font-Bold="true"></asp:Label>
                                </p>

                                &nbsp;<hr style="border: 1px solid #000000;" />
                            </div>
                            <div class="boxDiv">
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                            </div>
                        </td>
                        <td style='border: 1px solid #ccc'>
                            <div style="background-color: #faf6f6;">
                                <label style="font-family: Calibri; font-size: large; color: blue; font-size: 20px;">Server - 2</label>
                                <p>
                                    <asp:Label ID="Label2" runat="server" ForeColor="Green" Font-Size="Large" Font-Bold="true"></asp:Label>
                                </p>
                                &nbsp;<hr style="border: 1px solid #000000;" />
                            </div>
                            <div class="boxDiv">
                                <asp:PlaceHolder ID="PlaceHolder2" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style='border: 1px solid #ccc'>
                            <div style="background-color: #faf6f6;">
                                <label style="font-family: Calibri; font-size: large; color: blue; font-size: 20px;">Server - 3</label>
                                <p>
                                    <asp:Label ID="Label3" runat="server" ForeColor="Green" Font-Size="Large" Font-Bold="true"></asp:Label>
                                </p>
                                &nbsp;<hr style="border: 1px solid #000000;" />
                            </div>
                            <div class="boxDiv">
                                <asp:PlaceHolder ID="PlaceHolder3" runat="server" />
                            </div>
                        </td>
                        <td style='border: 1px solid #ccc'>
                            <div style="background-color: #faf6f6;">
                                <label style="font-family: Calibri; font-size: large; color: blue; font-size: 20px;">Server - 4</label>
                                <p>
                                    <asp:Label ID="Label4" runat="server" ForeColor="Green" Font-Size="Large" Font-Bold="true"></asp:Label>

                                </p>
                                &nbsp;<hr style="border: 1px solid #000000;" />
                            </div>
                            <div class="boxDiv">
                                <asp:PlaceHolder ID="PlaceHolder4" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style='border: 1px solid #ccc'>
                            <div style="background-color: #faf6f6;">
                                <label style="font-family: Calibri; font-size: large; color: blue; font-size: 20px;">Server - 5</label>
                                <p>
                                    <asp:Label ID="Label5" runat="server" ForeColor="Green" Font-Size="Large" Font-Bold="true"></asp:Label>
                                </p>
                                &nbsp;<hr style="border: 1px solid #000000;" />
                            </div>
                            <div class="boxDiv">
                                <asp:PlaceHolder ID="PlaceHolder5" runat="server" />
                            </div>
                        </td>
                        <td style='border: 1px solid #ccc'>
                            <div style="background-color: #faf6f6;">
                                <label style="font-family: Calibri; font-size: large; color: blue; font-size: 20px;">Server - 6</label>
                                <p>
                                    <asp:Label ID="Label6" runat="server" ForeColor="Green" Font-Size="Large" Font-Bold="true"></asp:Label>
                                </p>
                                &nbsp;<hr style="border: 1px solid #000000;" />
                            </div>
                            <div class="boxDiv">
                                <asp:PlaceHolder ID="PlaceHolder6" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <div id="footer">
                <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics | Trackingpipeline.</div>
            </div>
        </div>
    </form>
</body>
</html>
