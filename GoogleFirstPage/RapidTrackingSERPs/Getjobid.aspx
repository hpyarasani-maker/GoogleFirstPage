<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Getjobid.aspx.cs" Inherits="GoogleFirstPage.RapidTrackingSERPs.Missingelements" Title="Missing Element Jobid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Missing Elements</title>

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
            $('[id*=grmissing]').footable();
        });
    </script>

    <script type="text/javascript">
        function HideLabel() {
            var seconds = 15;
            setTimeout(function () {
                document.getElementById("<%=lblalllinks.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
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
        <asp:ScriptManager ID="script11" runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
                    &nbsp;<strong><span class="auto-style1">Select BlockType :</span></strong>&nbsp;&nbsp;
                    <asp:DropDownList ID="ddltype" runat="server" CssClass="text1" Height="30px" Width="160px">
                        <asp:ListItem Text="Adwords" Value="adwords" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="AnswerCard" Value="answerCard"></asp:ListItem>
                        <asp:ListItem Text="Apps" Value="apps"></asp:ListItem>
                        <asp:ListItem Text="Carousel" Value="carousel"></asp:ListItem>
                        <asp:ListItem Text="Dataset" Value="dataset"></asp:ListItem>
                        <asp:ListItem Text="Finance" Value="finance"></asp:ListItem>
                        <asp:ListItem Text="FindResultsOn" Value="findResultsOn"></asp:ListItem>
                        <asp:ListItem Text="FlightPack" Value="flightPack"></asp:ListItem>
                        <asp:ListItem Text="HotelPack" Value="hotelPack"></asp:ListItem>
                        <asp:ListItem Text="Images" Value="images"></asp:ListItem>
                        <asp:ListItem Text="Jobs" Value="Jobs"></asp:ListItem>
                        <asp:ListItem Text="KnowledgeGraph" Value="knowledgeGraph"></asp:ListItem>
                        <asp:ListItem Text="Maps" Value="maps"></asp:ListItem>
                        <asp:ListItem Text="PeopleAlsoAsk" Value="peopleAlsoAsk"></asp:ListItem>
                        <asp:ListItem Text="ProductListedAds" Value="productListedAds"></asp:ListItem>
                        <asp:ListItem Text="PopularProducts" Value="popularProducts"></asp:ListItem>
                        <asp:ListItem Text="EventResults" Value="eventResults"></asp:ListItem>
                        <asp:ListItem Text="SiteLinks" Value="siteLinks"></asp:ListItem>
                        <asp:ListItem Text="TwitterCards" Value="twitterCards"></asp:ListItem>
                        <asp:ListItem Text="TopStories" Value="topStories"></asp:ListItem>
                        <asp:ListItem Text="Videos" Value="videos"></asp:ListItem>
                        <asp:ListItem Text="Video" Value="video"></asp:ListItem>
                        <asp:ListItem Text="VideoCard" Value="videoCard"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqfv1" runat="server" ErrorMessage="*" Text="*" ControlToValidate="ddltype" InitialValue="----Select----" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp; <strong><span class="auto-style2">Search Engine :</span></strong>&nbsp;&nbsp; 
                   <asp:DropDownList ID="ddlseid" CssClass="text1" Height="30px" Width="300px" runat="server">
                   </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Text="*" ControlToValidate="ddlseid" InitialValue="----Select----" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;
                <asp:Button runat="server" Text="Submit" CssClass="btnlf" Height="27px" ID="btndata" Width="120px" OnClick="btndata_Click" />
                    <br />
                    <br />
                    <br />
                </div>
            </div>
            <br />
            <br />
            <br />
            <div class="wrapper">
                <asp:Label ID="lblalllinks" runat="server" ForeColor="Blue" Font-Size="Small" Font-Bold="true"></asp:Label>
            </div>
            <br />
            <br />
            <br />
            <%--<asp:GridView runat="server" ID="grmissing" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Left" Font-Bold="true" HeaderStyle-Font-Bold="true" OnRowDataBound="grmissing_RowDataBound" RowStyle-Height="1px" AlternatingRowStyle-BackColor="#f5f5f5" Font-Size="Small" HeaderStyle-BackColor="#e4e8ef" HeaderStyle-Height="2px" RowStyle-Width="1px">
                <Columns>
                        <asp:BoundField HeaderText="Seid" DataField="seid" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="Keyword" DataField="name" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="Oxylabs Jobid" DataField="jobid" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
            </asp:GridView>--%>
            <br />
            <br />
            <div id="footer">
                <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics | SERP - OxylabsJobid.</div>
            </div>
        </div>
    </form>
</body>
</html>
