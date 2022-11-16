<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Amazontrackingweekly.aspx.cs" Inherits="GoogleFirstPage.Amazontrackingweekly" Title="Amazon Weekly" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Amazon Weekly</title>
    <meta charset="utf-8" />

    <link rel="icon" href="../images/default-avatar-logo.png" type="image/x-icon" />

    <link href="CSS/StyleSheet2.css" rel="stylesheet" />

    <script src="../js/jquery-1.10.2.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />

    <link href="CSS/menu.css" rel="stylesheet" />

    <link href="CSS/Newstyle.css" rel="stylesheet" />


    <link rel="icon" href="../images/default-avatar-logo.png" type="image/x-icon" />


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link media="screen" rel="stylesheet" href='http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' />
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=gvLocalData]').footable();
        });
    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <style>
        .btnlf {
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

    <style>
        .LblClass2 {
            font-family:Cambria;
            font-size: 20px;    
            color:brown;
            font-weight: bold;
        }
    </style>

</head>
<body style="background-color: white">

    <script src="js/custom.js"></script>

    <script src="js/jquery.js"></script>

    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="header1" style="width:2100px;">
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
                                            <a href="#">Amazon Weekly</a>&nbsp;<ul style="display: none;" class="dropdown-menu bold">
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
                <div style="margin-top:10px;">
                    &nbsp; <strong><span class="auto-style2">SE :</span></strong>&nbsp;&nbsp; 
                   <asp:DropDownList ID="ddlseid" CssClass="text1" Height="28px" Width="300px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlseid_SelectedIndexChanged">
                   </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Text="*" ControlToValidate="ddlseid" InitialValue="----Select----"></asp:RequiredFieldValidator>
                &nbsp;&nbsp;
                &nbsp;<span class="auto-style1">Search Terms :</span>&nbsp;&nbsp;<asp:DropDownList ID="ddlkeyword" runat="server" CssClass="text1" Height="28px" Width="300px">
                   <%-- <asp:ListItem Text="blenders" Value="1"></asp:ListItem>--%>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="*" ControlToValidate="ddlkeyword" InitialValue="----Select----"></asp:RequiredFieldValidator>
                &nbsp;&nbsp;
                             <span class="auto-style1">Date :</span>&nbsp;&nbsp;
                             <asp:TextBox ID="txtDate" runat="server" placeholder="Date" CssClass="text1" Height="24px" Width="100px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calendar" PopupButtonID="img_pop" runat="server" TargetControlID="txtDate" Format="yyyy-MM-dd" StartDate="2020-01-10"/>
                &nbsp;<asp:ImageButton ID="img_pop" ImageUrl="~/images/calendarnew.png" ImageAlign="Bottom" runat="server" Height="25px" Width="30px" />
                <asp:Button runat="server" Text="Submit" CssClass="btnlf" Height="26px" ID="btnclickwkly" Width="120px" OnClick="btnclickwkly_Click" />
                </div>

            </div>
            <br />
            <br />
            <br />
            <asp:Label ID="error_lbl" runat="server" Font-Size="12" ForeColor="Chocolate" Visible="false"></asp:Label><br />
            <asp:Label ID="lbl2" runat="server" Font-Size="12" CssClass="LblClass2" Visible="true"></asp:Label><br />
            <br />
             <br />
            <br />
            <div class="wrapper">
                <asp:GridView ID="gvLocalData" AutoGenerateColumns="false" CssClass="footable" HeaderStyle-HorizontalAlign="Right" Font-Bold="true"  HeaderStyle-Font-Bold="true" runat="server" RowStyle-Height="10px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" HeaderStyle-Height="40px" OnRowDataBound="gvLocalData_RowDataBound">
                    <%--<RowStyle HorizontalAlign="Left" Width="100%" />--%>
                   <Columns>
                        <asp:TemplateField HeaderText="Rank">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem,"Position") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="URLs">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <a href="<%#DataBinder.Eval(Container.DataItem,"Urls") %>" target="_blank"><%#DataBinder.Eval(Container.DataItem,"Urls") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Title">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem,"Title") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Price">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem,"Price") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <br />
            <br />
            <br />
            <asp:Label ID="lbl1" runat="server" CssClass="LblClass"></asp:Label>
            <br />
            <br />
            <div id="footer">
                <div style="font-weight:bold;font-size:14px;">© <%= DateTime.Now.Year %> Pi Datametrics | Amazon Weekly.</div>
            </div>

        </div>
    </form>
</body>
</html>
