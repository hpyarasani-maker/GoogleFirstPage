<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="internallinks.aspx.cs" Inherits="GoogleFirstPage.Internallinks.internallinks" Title="Internal Links of a Website" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Internal Links of a Website</title>

    <meta charset="utf-8" />

    <link href="Content/bootstrap.cosmo.min.css" rel="stylesheet" />
    <link href="Content/StyleSheet.css" rel="stylesheet" />
    <link rel="icon" href="../images/default-avatar-logo.png" type="image/x-icon" />

    <link href="../CSS/StyleSheet2.css" rel="stylesheet" />
    <script src="../js/jquery-1.10.2.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width = device-width, initial-scale = 1.0, minimum-scale = 1.0, maximum-scale = 1.0, user-scalable = no" />

    <link href="../CSS/menu.css" rel="stylesheet" />

    <link href="../CSS/Newstyle.css" rel="stylesheet" />



    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link media="screen" rel="stylesheet" href='http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css' />
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=gridlinkfinder]').footable();
        });
    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

   


    <script type="text/javascript">
        function ShowDiv() { setTimeout('document.getElementById("PB").style.display ="inline";', 500); }
    </script>


    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            //min-height: 100%;
            width: 10%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            //border: 5px solid #67CFF5;
            width: 10px;
            height: 1px;
            display: none;
            position: fixed;
            background-color: white;
            z-index: 999;
            margin-top: 50px;
        }
    </style>

    <style type="text/css">
        .loading1 {
            font-family: Arial;
            font-size: 10pt;
            //border: 5px solid #67CFF5;
            width: 10px;
            height: 1px;
            display: none;
            position: fixed;
            background-color: white;
            z-index: 999;
            margin-top: 50px;
        }
    </style>


    <style type="text/css">
        .modalPopup {
            background-color: #ffffff;
            filter: alpha(opacity=40);
            opacity: 0.7;
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



    <style>
        .LblClass2 {
            font-family: Cambria;
            font-size: 20px;
            //color: #333333;
            color: brown;
            font-weight: bold;
        }

        .auto-style1 {
            font-size: small;
            font-weight: bold;
            margin-top: 50px;
        }

        .auto-style2 {
            font-size: small;
            font-weight: bold;
            margin-top: 20px;
            //margin-left:-1100px;
            //margin-right:1100px;
            color: darkblue;
        }
    </style>


    <style>
        .header2 {
            padding: 20px;
            text-align: center;
            font-family: Arial;
            font-size: 12px;
            height: 40px;
            background-color: #e4e8ef;
            padding-top: 30px;
            padding: 20px 20px 20px 20px;
        }

        @media only screen and (max-width: 320px) and (max-width:568px) {
            body {
                background-color: white;
            }

            .header2 {
                padding: 20px;
                text-align: center;
                font-family: Arial;
                font-size: 12px;
                height: 40px;
                background-color: #e4e8ef;
                padding-top: 30px;
                height: 130px;
                padding: 10px 10px 30px 5px;
            }

            .Topclass {
                margin-top: 10px;
            }
        }

        @media only screen and (min-width:321px) and (max-width:768px) {
            body {
                background-color: white;
            }

            .header2 {
                padding: 20px;
                text-align: center;
                font-family: Arial;
                font-size: 12px;
                height: 40px;
                background-color: #e4e8ef;
                padding-top: 30px;
                height: 120px;
                padding: 10px 10px 10px 10px;
            }
        }

        @media only screen and (min-width:769px) and (max-width:1024px) {
            body {
                background-color: white;
            }

            .header2 {
                padding: 20px;
                text-align: center;
                font-family: Arial;
                font-size: 12px;
                height: 40px;
                background-color: #e4e8ef;
                //padding-top: 30px;
                height: 80px;
                padding: 10px 10px 10px 10px;
            }
        }
    </style>

    <style>
        .header3 {
            padding: 20px;
            text-align: center;
            font-family: Arial;
            font-size: 15px;
            //height: 60px;
            background-color: #e4e8ef;
            //padding-top: 30px;
            height: 120px;
            padding: 10px 10px 10px 10px;
            border-radius: 10px 10px;
        }

        .imagexls {
            float: right;
            height: 40px;
            width: 40px;
            //margin-left:800px;
            //margin-right: 200px;
            //margin-left:400px;
            //margin-top: -5px;
        }

        .LblClass2 {
            font-family: Cambria;
            font-size: 16px;
            //color: #333333;
            color: brown;
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        .btn1 {
            background: transparent;
            border: 1.5px solid #1583c2;
            width: 85px;
            height: 28px;
            font-size: 15px;
            border-radius: 3px;
            display: inline-block;
            font-family: Arial;
            font-weight: bold;
            font-style: normal;
            color: #1583c2;
            margin-top: -2000px;
        }
            .btn1:hover {
                background-color: #1583c2;
                text-decoration: none;
                color: white;
                // margin-bottom: 200px;
            }
    </style>

</head>

<body style="background-color: white" onload="javascript:HideProgressBar()">

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
                                <nav style="margin-left: 850px; margin-top: 10px">
                                    <ul class="nav topnav bold">
                                        <li>
                                            <a href="../Dashboard.aspx">Home</a>
                                            <ul style="display: none;" class="dropdown-menu bold">
                                            </ul>
                                        </li>
                                        <li>
                                            <%--<asp:LinkButton ID="lnksignt" runat="server" OnClick="lnksignt_Click" Text="Sign out" PostBackUrl="~/Internallink/Internallinkslogin.aspx"></asp:LinkButton>--%>
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
                <div>
                    <div style="margin-top: 15px;">
                        &nbsp;<span class="auto-style1">Enter website with http:// or https:// as shown below :</span>&nbsp;
                    <asp:TextBox ID="txtinternallinks" runat="server" Height="30px" Width="550px" CssClass="text1" OnTextChanged="txtinternallinks_TextChanged" placeholder="Type any URL"></asp:TextBox>
                    <asp:Button runat="server" Text="Submit" ID="btnsearchlinks" Width="100px" OnClick="btnsearchlinks_Click" CssClass="btnlf" Height="25px" OnClientClick="ShowDiv()" />&nbsp;
                        <div style="float: right; margin-right: 131px;">
                            <asp:ImageButton ID="ImageExcel" runat="server" ImageUrl="~/images/export_csv-512.png" OnClick="ImageExcel_Click" CssClass="imagexls" ToolTip="Generate Csv" Style="margin-right: 157px; margin-top: -5px;" />
                        </div>
                        <br />
                    </div>
                </div>

                <div style="flex: auto; float: left; margin-left: 68px;">
                    <span class="auto-style2">https://www.pi-datametrics.com/about-us/</span>&nbsp;&nbsp;&nbsp;<br />
                    <span class="auto-style2">https://www.harpersbazaar.com/uk/travel/</span>&nbsp;&nbsp;
                </div>
                <div>
                    <asp:Label runat="server" Width="248px"></asp:Label>
                    <div id="progressbar"></div>
                    <asp:RequiredFieldValidator ID="req1" runat="server" ErrorMessage="Enter http websites only" ControlToValidate="txtinternallinks" Text="Please enter website"></asp:RequiredFieldValidator><br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtinternallinks" Text="Enter websites with http:// or https://" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" runat="server" />
                </div>

                <br />
                <div>
                    <asp:Label ID="error_lbl" runat="server" Font-Size="12" ForeColor="Chocolate" Visible="false"></asp:Label><br />
                    <asp:Label ID="lbl2" runat="server" Font-Size="12" CssClass="LblClass2" Visible="true"></asp:Label><br />
                    <asp:Label ID="Label1" runat="server" Font-Size="12" CssClass="LblClass2" Visible="true"></asp:Label><br />
                    <asp:Label ID="lbl" runat="server" CssClass="LblClass2" Visible="false"></asp:Label><br />
                </div>
                <br />
                <div>
                    <%--<div style="background-color: white; display: none; margin-top: 1px;" id="PB">--%>
                        <%--<img src="../images/spinner_squares_circle.gif" />--%>
                        <%--<p style="font-family: Calibri; font-weight: bold; color: forestgreen; font-size: 15px;margin-top:20px;margin-left:80px;">Loading.....</p>--%>
                        <%--<p style="font-family: Calibri; font-weight: bold; color: forestgreen;">Loading.....please wait.....</p>--%>
                    <%--</div>--%>
                </div>
                <div class="wrapper">
                    <asp:Label ID="lblalllinks" runat="server"></asp:Label>

                    <asp:GridView ID="gridlinkfinder" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" runat="server" CssClass="footable" Font-Bold="true" OnRowDataBound="gridlinkfinder_RowDataBound" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef">
                        <Columns>
                            <asp:TemplateField HeaderText="SlNo">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem,"SlNo") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="URLs">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <%--<%#DataBinder.Eval(Container.DataItem,"URL") %>--%>
                                    <%--<a href="<%#DataBinder.Eval(Container.DataItem,"URL") %> <%#DataBinder.Eval(Container.DataItem,"URL") %>"></a>--%>
                                    <a href="<%#DataBinder.Eval(Container.DataItem,"URL") %>" target="_blank"><%#DataBinder.Eval(Container.DataItem,"URL") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Text">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem,"Text") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TextType">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem,"TextType") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LinkType">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem,"LinkType") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <br />
                </div>
                <br />
                <br />
                <br />
                <asp:Label ID="lbl1" runat="server" CssClass="LblClass"></asp:Label>
                <br />
                <br />

                <div id="footer">
                    <%--<div style="font-weight: bold; font-size: 14px;">© 2020 Pi Datametrics | Internal Links of Website.</div>--%>
                    <div style="font-weight: bold; font-size: 14px;font-family:Calibri;">© <%= DateTime.Now.Year %> Pi Datametrics | Internal Links of Website.</div>
                </div>
            </div>
        </div>
        <div>
        </div>
    </form>
</body>
</html>
