<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="GoogleFirstPage.Trackingstatus.WebForm2" %>--%>

<%@ Import Namespace="GoogleFirstPage.Trackingstatus" %>

<!DOCTYPE html>
<%@ Page Language="C#" %>
 <script runat="server" language="C#">
     void Page_Load(object sender, EventArgs e)
     {
         TrackingModel tm = new TrackingModel();
         var a = tm.GetTrackingAll();
         for (int i = 0; i < a.Count; i++)
         {
             StringBuilder sb = new StringBuilder();
             sb = Getdata(i,a);
             switch (i)
             {
                 case 0:
                     PlaceHolder1.Controls.Add(new LiteralControl { Text = sb.ToString() });
                     PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                     PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                     break;
                 case 1:
                     PlaceHolder2.Controls.Add(new LiteralControl { Text = sb.ToString() });
                     PlaceHolder2.Controls.Add(new LiteralControl("<br />"));
                     PlaceHolder2.Controls.Add(new LiteralControl("<br />"));
                     break;
                 case 2:
                     PlaceHolder3.Controls.Add(new LiteralControl { Text = sb.ToString() });
                     PlaceHolder3.Controls.Add(new LiteralControl("<br />"));
                     PlaceHolder3.Controls.Add(new LiteralControl("<br />"));
                     break;
                 case 3:
                     PlaceHolder4.Controls.Add(new LiteralControl { Text = sb.ToString() });
                     PlaceHolder4.Controls.Add(new LiteralControl("<br />"));
                     PlaceHolder4.Controls.Add(new LiteralControl("<br />"));
                     break;
                 case 4:
                     PlaceHolder5.Controls.Add(new LiteralControl { Text = sb.ToString() });
                     PlaceHolder5.Controls.Add(new LiteralControl("<br />"));
                     PlaceHolder5.Controls.Add(new LiteralControl("<br />"));
                     break;
                 case 5:
                     PlaceHolder6.Controls.Add(new LiteralControl { Text = sb.ToString() });
                     PlaceHolder6.Controls.Add(new LiteralControl("<br />"));
                     PlaceHolder6.Controls.Add(new LiteralControl("<br />"));
                     break;
             }
             if(i==0)
             {
             }
         }
     }

     public StringBuilder Getdata(int i,List<TrackingModel> a)
     {
         StringBuilder sb = new StringBuilder();
         sb.Append("<table border = '2'>");
         sb.Append("<tr>");
         sb.Append("<th>Apps</th>");
         sb.Append("<th>"+a[i].Seid.ToString()+"</th>");
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
         if (a[i].Remaining.ToString()=="0")
         {
             sb.Append("<td style='width:100px;border: 1px solid #ccc'>Total</td>");
             sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Total.ToString() + "</td>");
             sb.Append("<td style='width:100px;border: 1px solid #ccc'>Completed</td>");
             sb.Append("</tr>");
             sb.Append("<tr>");
             sb.Append("<td style='width:100px;border: 1px solid #ccc'>Received</td>");
             sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Received.ToString() + "</td>");
             sb.Append("<td style='width:100px;border: 1px solid #ccc'>Completed</td>");
             sb.Append("</tr>");
             sb.Append("<tr>");
             sb.Append("<td style='width:100px;border: 1px solid #ccc'>Remaining</td>");
             sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Remaining.ToString() + "</td>");
             sb.Append("<td style='width:100px;border: 1px solid #ccc'>Completed</td>");
             sb.Append("</tr>");
         }
         else {
         sb.Append("<td style='width:100px;border: 1px solid #ccc'>Total</td>");
         sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Total.ToString() + "</td>");
         sb.Append("<td style='width:100px;border: 1px solid #ccc'>Running</td>");
         sb.Append("</tr>");
         sb.Append("<tr>");
         sb.Append("<td style='width:100px;border: 1px solid #ccc'>Received</td>");
         sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Received.ToString() + "</td>");
         sb.Append("<td style='width:100px;border: 1px solid #ccc'>Running</td>");
         sb.Append("</tr>");
         sb.Append("<tr>");
         sb.Append("<td style='width:100px;border: 1px solid #ccc'>Remaining</td>");
         sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + a[i].Remaining.ToString() + "</td>");
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
           <div style="margin-left:300px;">
            <table style="width: 70%">
                <tr>
                    <td>
                  <asp:PlaceHolder ID = "PlaceHolder1" runat="server" />
                    </td>
                    <td>
                  <asp:PlaceHolder ID = "PlaceHolder2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:PlaceHolder ID = "PlaceHolder3" runat="server" />
                    </td>
                    <td>
                        <asp:PlaceHolder ID = "PlaceHolder4" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:PlaceHolder ID = "PlaceHolder5" runat="server" />
                    </td>
                    <td>
                        <asp:PlaceHolder ID = "PlaceHolder6" runat="server" />
                    </td>
                </tr>
            </table>
        </div>

        </div>
    </form>
</body>
</html>
