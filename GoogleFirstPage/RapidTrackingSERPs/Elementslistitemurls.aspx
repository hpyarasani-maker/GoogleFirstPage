<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Elementslistitemurls.aspx.cs" Inherits="GoogleFirstPage.RapidTrackingSERPs.Elementlisturls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>SERP Elementslists Item Urls</title>

    <meta charset="utf-8" />

    <link href="Content/bootstrap.cosmo.min.css" rel="stylesheet" />
    <link href="Content/StyleSheet.css" rel="stylesheet" />

    <%--<link rel="icon" href="../images/PILogo_1.jpg" type="image/x-icon" />--%>

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
    <script type="text/javascript">
        $(function () {
            $('[id*=gvelements]').footable();
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
            <div class="header3">
                <div style="margin-top: 12px;">
                    &nbsp; <strong><span class="auto-style2">Search Engine :</span></strong>&nbsp;&nbsp; 
                   <asp:DropDownList ID="ddltype1" CssClass="text1" Height="30px" Width="150px" runat="server">
                       <asp:ListItem Selected="True" Enabled="true" Text="All Search Engines" Value="All"></asp:ListItem>
                       <asp:ListItem Enabled="true" Text="Google US" Value="1"></asp:ListItem>
                       <asp:ListItem Enabled="true" Text="Google UK" Value="58"></asp:ListItem>
                       <asp:ListItem Enabled="true" Text="Google US Mobile" Value="102"></asp:ListItem>
                       <asp:ListItem Enabled="true" Text="Google UK Mobile" Value="106"></asp:ListItem>
                       <asp:ListItem Enabled="true" Text="Other Desktop" Value="Other Desktop"></asp:ListItem>
                       <asp:ListItem Enabled="true" Text="Other Mobile" Value="Other Mobiles"></asp:ListItem>
                   </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Text="*" ControlToValidate="ddltype1" InitialValue="----Select----"></asp:RequiredFieldValidator>
                    &nbsp;
                <asp:Button runat="server" Text="Submit" CssClass="btnlf" Height="27px" ID="btnelementslisturls" Width="120px" OnClick="btnelementslisturls_Click" />
                    <br />
                    <br />
                    <br />
                </div>
            </div>
            <br />
            <br />
            <div class="wrapper">
                <asp:Label ID="lblalllinks" runat="server" ForeColor="Blue" Font-Size="Small" Font-Bold="true"></asp:Label><br />

                <asp:Label ID="lbl1" runat="server" CssClass="LblClassNew"></asp:Label>
                <br />
                <br />


                <asp:GridView ID="gvelementslisturls" AutoGenerateColumns="false" CssClass="footable" HeaderStyle-HorizontalAlign="Right" Font-Bold="true" HeaderStyle-Font-Bold="true" runat="server" OnRowDataBound="gvelementslisturls_RowDataBound" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" HeaderStyle-Height="30px" RowStyle-Width="1px" AllowPaging="true" OnPageIndexChanging="gvelementslisturls_PageIndexChanging" PageSize="30">                    
                    <Columns>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("Date") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Answer card (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("answerCard","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ai Overview (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 60px;">
                                    <%# Eval("aiOverview","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="App packs (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("apps","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Carousel (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("carousel","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Classic Link Carousel (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("classicLinkCarousel","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Classic Link SiteLinks (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("classicLinkSiteLinks","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Dataset (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("dataset","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Finance (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("finance","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FindResultsOn (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("findResultsOn","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Flight Pack (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("flightPack","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hotel Pack (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("hotelPack","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Images (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("images","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Knowledge panel (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("KnowledgeGraph","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Maps (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("maps","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="People also ask (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("PeopleAlsoask","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:TemplateField HeaderText="people Also Search (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("peopleAlsoSearch","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Product listed ads (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("productListedads","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Popular Products (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 100px;">
                                    <%# Eval("popularProducts","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sports results (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("eventResults","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Site links (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("siteLinks","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Sites Carousel (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 60px;">
                                    <%# Eval("sitesCarousel","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                         <asp:TemplateField HeaderText="Text ads (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("adwords","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Twitter cards (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("twitterCards","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Top stories (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("topStories","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Video carousel (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("videos","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Video link (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("video","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Video card (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("videoCard","{0:N0}") %>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Jobs (Item Urls)">
                            <ItemTemplate>
                                <div style="width: 75px;">
                                    <%# Eval("Jobs","{0:N0}") %>
                                </div>
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
            <%--<asp:Label ID="lbl1" runat="server" CssClass="LblClass"></asp:Label>--%>
            <br />
            <br />
            <div id="footer">
                <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics | SERP ElementsList Item Urls.</div>
            </div>
        </div>
    </form>
</body>
</html>
