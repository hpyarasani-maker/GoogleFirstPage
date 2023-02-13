<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="googletrendsdata.aspx.cs" Async="true" AsyncTimeout="100000" Inherits="GoogleFirstPage.Googletrends.keywordsdata" Title="GoogleTrends" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Google Trends Live</title>

    <link href="../CSS/StyleSheet2.css" rel="stylesheet" />
    <link href="css/main.css" rel="stylesheet" />

    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/jquery-1.10.2.min.js"></script>

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


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>


    <link rel="icon" href="images/default-avatar-logo.png" type="image/x-icon" />

    <script src="js/jquery-1.10.2.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=gvinterestot]').footable();
        });
    </script>

    <style type="text/css">
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
</head>
<body style="background-color: white">
    <script src="js/custom.js"></script>

    <script src="js/jquery.js"></script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="header1" style="width: 2200px;">
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
            <br />
            <%-- <div style="margin-left: 670px;">
                <p style="font-family: Cambria; color: black; font-size: 25px; font-weight: bold;"><u>GoogleTrends </u></p>
            </div>--%>

            <%--<asp:Label ID="lbl" runat="server" Font-Size="12" ForeColor="Chocolate" Visible="false"></asp:Label><br />--%>

            <div class="wrapper">
                <asp:Label ID="lblalllinks" runat="server"></asp:Label>
                <div style="margin-left: 50px;">
                    <p style="font-family: Cambria; color: black; font-size: 25px; font-weight: bold;"><u>Google Trends </u></p>

                    <table class="auto-style1" border="0">
                        <tr style="border-top: solid 2px black; border-bottom: solid 2px black; border-left: solid 2px black; border-right: solid 2px black;">
                            <td class="auto-style2">
                                <asp:Label ID="Label1" runat="server" Text="Keyword :" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtkeyword" runat="server" CssClass="text1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqkeyword" runat="server" ControlToValidate="txtkeyword" Text="           keyword" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style2">
                                <asp:Label ID="Label2" runat="server" Text="Location :" Font-Bold="true"></asp:Label>
                                <asp:DropDownList ID="ddllocation" runat="server" Width="160px" CssClass="text1">
                                    <asp:ListItem Selected="True" Text="United States" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="United Kingdom" Value="58"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqcntrycodr" runat="server" ControlToValidate="ddllocation" InitialValue="Drop down" Text="Select Location" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style2">
                                <asp:Label ID="Label3" runat="server" Text="Language :" Font-Bold="true"></asp:Label>
                                <asp:DropDownList ID="ddllanguage" runat="server" Width="160px" CssClass="text1">
                                    <asp:ListItem Selected="True" Text="English" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqlang" runat="server" ControlToValidate="ddllanguage" InitialValue="Drop down" Text="Select any language" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style2">
                                <asp:Label ID="Label4" runat="server" Text="Start Date :" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtstartdate" runat="server" placeholder="start date" Height="30px" Width="100px" CssClass="text1"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="strtimg" ImageUrl="~/images/calendarnew.png" Height="20px" Width="20px" />
                                <ajaxToolkit:CalendarExtender ID="stdate" runat="server" PopupButtonID="strtimg" TargetControlID="txtstartdate" Format="yyyy-MM-dd" />
                                <asp:RequiredFieldValidator ID="reqfvsdate" runat="server" ControlToValidate="txtstartdate" Text="Startdate required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style2">
                                <asp:Label ID="Label5" runat="server" Text="End Date :" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="txtenddate" runat="server" placeholder="end date" Height="30px" Width="100px" CssClass="text1"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="enddimg" ImageUrl="~/images/calendarnew.png" Height="20px" Width="20px" />
                                <ajaxToolkit:CalendarExtender ID="endt" runat="server" PopupButtonID="enddimg" TargetControlID="txtenddate" Format="yyyy-MM-dd" />
                                <asp:RequiredFieldValidator ID="reqfvendate" runat="server" ControlToValidate="txtenddate" Text="Enddate required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <div style="margin-left: -50px;">
                                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="btngoogletrends_Click" CssClass="btnlf" Height="27px" Width="120px" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <asp:Label ID="lblnodata" runat="server" ForeColor="Red" Font-Size="Large" CssClass="LblClass"></asp:Label><br />
                        <asp:Label ID="lblerror" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>
                    </div>
                    <br />
                    <div>
                        <asp:Label ID="Label6" runat="server" Text="Interest over time" Font-Size="Large"></asp:Label><br />
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvinterestot" runat="server" AutoGenerateColumns="true" Width="80%" CssClass="footable" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" RowStyle-Width="1px" HeaderStyle-Height="30px">
                                    <HeaderStyle BackColor="#e4e8ef" Font-Bold="true" ForeColor="Black" />
                                   <%-- <Columns>
                                        <asp:BoundField HeaderText="DateFrom" DataField="date_from" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="DateTo" DataField="date_to" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Missing Data" DataField="missing_data" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Values" DataField="value" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>--%>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br />
                    <br />
                    <div>
                        <asp:Label ID="Label7" runat="server" Text="Interest by subregion" Font-Size="Large"></asp:Label><br />
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvsubregion" runat="server" AutoGenerateColumns="true" Width="80%" CssClass="footable" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" RowStyle-Width="1px" HeaderStyle-Height="30px">
                                    <HeaderStyle BackColor="#e4e8ef" Font-Bold="true" ForeColor="Black" />
                                    <%--<Columns>
                                        <asp:BoundField HeaderText="Geo ID" DataField="geo_id" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Geo Name" DataField="geo_name" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Value" DataField="value" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Max Value Index" DataField="max_value_index" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>--%>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br />
                    <br />
                    <div>
                        <asp:Label ID="Label8" runat="server" Text="Related topics" Font-Size="Large"></asp:Label><br />
                        <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvrelatedtopics" runat="server" AutoGenerateColumns="true" Width="80%" CssClass="footable" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" RowStyle-Width="1px" HeaderStyle-Height="30px">
                                    <HeaderStyle BackColor="#e4e8ef" Font-Bold="true" ForeColor="Black" />
                                    <%--<Columns>
                                        <asp:BoundField HeaderText="Type" DataField="type" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Topic ID" DataField="topic_id" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Topic Title" DataField="topic_title" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Topic Type" DataField="topic_type" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Value" DataField="value" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>--%>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br />
                    <br />
                    <div>
                        <asp:Label ID="Label9" runat="server" Text="Related queries" Font-Size="Large"></asp:Label><br />
                        <asp:UpdateProgress ID="UpdateProgress4" runat="server">
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvrelatedqueries" runat="server" AutoGenerateColumns="true" Width="80%" CssClass="footable" Font-Bold="true" HeaderStyle-Font-Bold="true" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" RowStyle-Width="1px" HeaderStyle-Height="30px">
                                    <HeaderStyle BackColor="#e4e8ef" Font-Bold="true" ForeColor="Black" />
                                    <%--<Columns>
                                        <asp:BoundField HeaderText="Type" DataField="type" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Query ID" DataField="query" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Value" DataField="value" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>--%>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <br />



            </div>
            <br />
            <br />
            <br />


            <asp:Label ID="lbl1" runat="server" CssClass="LblClass"></asp:Label>
            <br />
            <br />
            <div id="footer">
                <div style="font-weight: bold; font-size: 14px; font-family: Calibri;">© <%= DateTime.Now.Year %> Pi Datametrics | GoogleTrends.</div>
            </div>

        </div>
    </form>
</body>
</html>
