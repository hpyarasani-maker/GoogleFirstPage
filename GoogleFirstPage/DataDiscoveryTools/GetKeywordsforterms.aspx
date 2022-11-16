<%@ Page Language="C#" AutoEventWireup="true" Async="true" AsyncTimeout="100000" Title="" CodeBehind="GetKeywordsforterms.aspx.cs" Inherits="GoogleFirstPage.DataDiscoveryTools.GetKeywordsforterms" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Get Keywords For Terms</title>

    <link href="../CSS/StyleSheet2.css" rel="stylesheet" />

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



    <script type="text/javascript">
        function showDivtag() { setTimeout('document.getElementById("PB").style.display ="inline";', 10); }
    </script>

    <script type="text/javascript">
        function ClientValidate(source, arguments) {
            var txt = document.getElementById('txtfilter');
            var ddl = document.getElementById('ddlfilter');
            var decision = ddl.options[ddl.selectedIndex].text;
            if (decision == 'Drop down') {
                arguments.IsValid = txt.value.length > 0;
            } else {
                arguments.IsValid = true;
            }
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
            width: 100%;
            padding: 10px;
            margin: 0px auto;
            margin-left: -10px;
        }

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

</head>

<body style="background-color: white">

    <script src="js/custom.js"></script>

    <script src="js/jquery.js"></script>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <div class="header1" style="width: 2200px;">
            <div class="header-menu">
             <div style="margin-left:-150px;">
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
            <div style="margin-left: 600px;">
                <p style="font-family: Cambria; color: black; font-size: 25px; font-weight: bold;"><u> Get Keywords For Terms </u> </p>
            </div>

            <%--<asp:Label ID="lbl" runat="server" Font-Size="12" ForeColor="Chocolate" Visible="false"></asp:Label><br />--%>
       
            <div class="wrapper">
                <asp:Label ID="lblalllinks" runat="server"></asp:Label>
                <div style="margin-left: 300px;">

                    <table class="auto-style1" border="0">
                        <tr style="border-top: solid 2px black; border-bottom: solid 2px black; border-left: solid 2px black; border-right: solid 2px black;">
                            <td class="auto-style2">
                                <asp:Label ID="Label2" runat="server" Text="Country Code :" Font-Bold="true"></asp:Label>
                            </td>
                            <td class="auto-style2">
                                <asp:DropDownList ID="ddlcountrycode" runat="server" Width="160px" CssClass="text1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqcntrycodr" runat="server" ControlToValidate="ddlcountrycode" InitialValue="Drop down" Text="Select any country" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="border-bottom: solid 2px black; border-top: solid 2px solid; border-left: solid 2px black; border-right: solid 2px black;">
                            <td class="auto-style2">
                                <asp:Label ID="Label3" runat="server" Text="Language :" Font-Bold="true"></asp:Label>
                            </td>
                            <td class="auto-style2">
                                <asp:DropDownList ID="ddllanguage" runat="server" Width="160px" CssClass="text1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqlang" runat="server" ControlToValidate="ddllanguage" InitialValue="Drop down" Text="Select any language" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="border-bottom: solid 2px black; border-top: solid 2px solid; border-left: solid 2px black; border-right: solid 2px black;">
                            <td class="auto-style2">
                                <asp:Label ID="Label1" runat="server" Text="Keywords :" Font-Bold="true"></asp:Label>
                                <br />
                                <b style="color: red">(Max = 200)</b></td>
                            <td class="auto-style2">
                                <asp:TextBox ID="txtkw" runat="server" CssClass="text1" Height="181px" TextMode="MultiLine" Width="459px" placeholder="Enter one or more keywords, each on a separate line"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtkw" Text="Enter keywords"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr style="border-top: solid 2px black; border-bottom: solid 2px black; border-left: solid 2px black; border-right: solid 2px black;">
                            <td class="auto-style2">
                                <asp:Label ID="Label5" runat="server" Text="Limit :" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtlimit" runat="server" CssClass="text1"></asp:TextBox>&nbsp;&nbsp;<asp:Label ID="lbllimit" runat="server" Text="Max = 1000"></asp:Label>--%>
                                <asp:TextBox ID="txtlimit" runat="server" CssClass="text1" MaxLength="4" Min="1" Max="1000"></asp:TextBox>&nbsp;&nbsp;<asp:Label ID="lbllimit" runat="server" Text="Max = 1000" ForeColor="Red"></asp:Label>
                                &nbsp;
                                <asp:RangeValidator ID="myrange" runat="server" ControlToValidate="txtlimit" MaximumValue="1000" MinimumValue="10" ValidationGroup="temp" ErrorMessage="Max 1000" SetFocusOnError="True" Type="Double" Display="Dynamic" ForeColor="Red"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr style="border-top: solid 2px black; border-bottom: solid 2px black; border-left: solid 2px black; border-right: solid 2px black;">
                            <td class="auto-style2">
                                <asp:Label ID="Label6" runat="server" Text="Order by :" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlorderby" runat="server" Width="160px" CssClass="text1" AppendDataBoundItems="true">
                                    <asp:ListItem Text="Drop down" Value="0" />
                                    <asp:ListItem Text="search volume" Value="sv" />
                                    <asp:ListItem Enabled="true" Text="relevance" Value="relevance" />
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rb1orderby" runat="server" Text="Asc" GroupName="a1" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rb2orderby" runat="server" Text="Desc" GroupName="a1" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblrd11" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-top: solid 2px black; border-bottom: solid 2px black; border-left: solid 2px black; border-right: solid 2px black;">
                            <td class="auto-style2">
                                <asp:Label ID="Label4" runat="server" Text="Close_variants :" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                    <asp:RadioButton ID="rbcvfalse" runat="server" Text="False" GroupName="a2" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rbcvtrue" runat="server" Text="True" GroupName="a2" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%-- <asp:Label ID="lblnodata" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>--%>
                            </td>
                            <td>
                                <div style="margin-left: 120px;">
                                    <asp:Button ID="btngetkeywordsforterms" runat="server" Text="Submit" OnClick="btngetkeywordsforterms_Click" CssClass="btnlf" Height="27px" Width="120px"  />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <%-- <asp:Label ID="lblerror" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>--%>
                        </tr>
                    </table>
                    <div>
                        <asp:Label ID="kwderrorlbl" runat="server" ForeColor="Red" Font-Size="Large" CssClass="LblClass"></asp:Label>
                        <asp:Label ID="lblnodata" runat="server" ForeColor="Red" Font-Size="Large" CssClass="LblClass"></asp:Label>
                                    <asp:CustomValidator ID="cstlabel" runat="server" ForeColor="Red" OnServerValidate="cstlabel_ServerValidate" SetFocusOnError="true" ControlToValidate="txtkw" Font-Size="Medium" Font-Bold="true"></asp:CustomValidator>
                                <br />
                        <asp:Label ID="lblerror" runat="server" ForeColor="Red" Font-Size="Large"></asp:Label>
                    </div>

                </div>
            </div>
            <br />
            <br />
            <br />
            <%-- <div>
                 <asp:Label ID="lblnodata" runat="server" ForeColor="Red" Font-Size="Large" CssClass="LblClass"></asp:Label>
            </div>--%>

            <br />
            <br />
            <div id="footer">
                <%--<div style="font-weight: bold; font-size: 14px;">© 2020 Pi Datametrics | Get Keywords for Terms</div>--%>
                <div style="font-weight: bold; font-size: 14px;font-family:Calibri;">© <%= DateTime.Now.Year %> Pi Datametrics | Get Keywords for Terms.</div>
            </div>

        </div>
             
    </form>
</body>
</html>
