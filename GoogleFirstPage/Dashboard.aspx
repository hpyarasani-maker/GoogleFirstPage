<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="GoogleFirstPage.Dashboard" Title="DashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <title>DashBoard</title>

    <link rel="icon" href="images/default-avatar-logo.png" type="image/x-icon" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link href="CSS/StyleSheet2.css" rel="stylesheet" />

    <link href="CSS/menu.css" rel="stylesheet" />

    <link href="CSS/CssRespnsive.css" rel="stylesheet" />


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        function validateForm() {
            var x = document.forms["form1"]["txtBrandName"].value;
            if (x == null || x == "") {
                alert("Brand cannot be Empty");
                return false;
            }
        }

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
    <style type="text/css">
        .auto-style1 {
            width: 80%;
        }

        .imgLogoNew {
            float: left;
            margin-left: 200px;
            margin-bottom: 20px;
            margin-top: 17px;
        }
    </style>


    <style type="text/css">
        #footerDB {
            background-color: #FFFFFF;
            padding: 15px;
            font-family: Arial;
            font-weight: bold;            
            border: solid 1.5px skyblue;
            border-left: none;
            border-right: none;
            border-bottom: none;
            text-align: left;
            font-size: 10px;
            clear: both;
            //margin-top:112px;
        }
    </style>

</head>
<body>

    <script src="js/custom.js"></script>

    <script src="js/jquery.js"></script>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

        <header style="height: 80px;width:2000px;">
            <div style="margin-left:-150px;">
                 <a href="Dashboard.aspx" title="Pi-Datametrics Dashboard Home">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/PILogo_1.jpg" CssClass="imgLogoNew" Height="50px" Width="54px" /></a>
            </div>
           <%-- <div class="container">
            </div>--%>
        </header>

        <div class="containerDB">
        </div>

        <div class="container">
            <u>
                <h2 style="font-family: Cambria; font-size: 22px; font-weight: bold; color: brown;">Prototype Projects :</h2>
            </u>
            <br />

            <div class="panel-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <b><a data-toggle="collapse" href="#collapse12">Elementary by Pi</a></b>
                        </h4>
                    </div>
                    <div id="collapse12" class="panel-collapse collapse">
                        <br />
                        <p style="font-family: Calibri; font-size: medium">Elementary by Pi Display Data in xml format with respect to HTML Tags.</p>
                        <a href="Xmltracker/keywords.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Elementary by Pi</b></></a>
                        <br />
                    </div>
                </div>
            </div>

            <div class="panel-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <b><a data-toggle="collapse" href="#collapse15">Tracking keywords Unit Test Report</a></b>
                        </h4>
                    </div>
                    <div id="collapse15" class="panel-collapse collapse">
                        <br />
                        <p style="font-family: Calibri; font-size: medium">Display Tracking keywords UnitTest Report.</p>
                        <a href="login/login.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Login To View Report</b></></a>
                        <br />
                    </div>
                </div>
            </div>


            <div class="panel-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <b><a data-toggle="collapse" href="#collapse17">RapidTracking</a></b>
                        </h4>
                    </div>
                    <div id="collapse17" class="panel-collapse collapse">
                        <br />
                        <p style="font-family: Calibri; font-size: medium">Display all SERP Features. </p>
                        <a href="RapidTrackingSERPs/Live.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Live Search</b></></a><br />
                        <hr style="border-color: black" />
                        <%--<p style="font-family: Calibri; font-size: medium">Downloads a Zipfile that contains Oxylabs Html Source, Xml file and also Googlesource. </p>--%>
                        <p style="font-family: Calibri; font-size: medium">Display all the Features such as Classic links and Blocks and populates oxylabs jobid source and Google link. </p>
                        <a href="RapidTrackingSERPs/Jobid.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Search by JobID</b></></a><br />
                        <hr style="border-color: black" />
                        <p style="font-family: Calibri; font-size: medium">To Fetch All Google Classic Links According to Search Engines. </p>
                        <a href="GoogleClassicLinks/Livesearch.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Top 100 Classic links</b></></a><br />
                        <%--<hr style="border-color: black" />
                        <p style="font-family: Calibri; font-size: medium">To Fetch All Oxylabs Jobids with SE. </p>
                        <a href="RapidTrackingSERPs/Getjobid.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Get All Jobids</b></></a><br />--%>
                    </div>
                </div>
            </div>

             <div class="panel-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <b><a data-toggle="collapse" href="#collapse20">Data Discovery Tools - Google Trends</a></b>
                        </h4>
                    </div>
                    <div id="collapse20" class="panel-collapse collapse">
                        <br />
                        <p style="font-family: Calibri; font-size: medium">Google Trends returns for Interest over time, Interest by subregion, Related topics, Related queries </p>
                        <a href="Googletrends/googletrendsdata.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Google Trends Live</b></></a><br />
                    </div>
                </div>
            </div>

            <div class="panel-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <b><a data-toggle="collapse" href="#collapse18">Data Discovery Tools</a></b>
                        </h4>
                    </div>
                    <div id="collapse18" class="panel-collapse collapse">
                        <br />
                        <p style="font-family: Calibri; font-size: medium">Related keywords collects search-related keywords straight from the bottom of the SERPs. By specifying the related depth you can obtain keywords from four subsequent levels of related search. </p>
                        <a href="DataDiscoveryTools/GetRelatedterms.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Get Related Keywords</b></></a><br />
                        <hr style="border-color: black" />
                        <p style="font-family: Calibri; font-size: medium">Get long-tail keyword ideas that include your targeted keyword. You're queuing a database of over 1.4 billion queries.</p>
                        <a href="DataDiscoveryTools/GetSimilarkeywords.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Get Similar Keywords</b></></a><br />
                        <hr style="border-color: black" />
                        <p style="font-family: Calibri; font-size: medium">Get keywords that any domain and any web page are ranking for.</p>
                        <a href="DataDiscoveryTools/GetRankedkeywords.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Get Ranked Keywords</b></></a><br />
                        <hr style="border-color: black" />
                        <p style="font-family: Calibri; font-size: medium">Generate keyword ideas from a limited number of 'seed' terms.</p>
                        <a href="DataDiscoveryTools/GetKeywordsforterms.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Get Keywords for Terms</b></></a><br />
                        <hr style="border-color: black" />
                        <p style="font-family: Calibri; font-size: medium">Get all essential data on domains ranking for a particular keyword. You can specify the number of domains that you want to get.</p>
                        <a href="DataDiscoveryTools/GetSerpcompetitors.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Get SERP Competitors</b></></a><br />
                        <hr style="border-color: black" />
                        <p style="font-family: Calibri; font-size: medium">Get domain names of your competitors determined by analysing the intersections of ranked keywords.</p>
                        <a href="DataDiscoveryTools/GetCompetitorsdomain.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Get Competitors Domain</b></></a><br />
                        <hr style="border-color: black" />
                        <p style="font-family: Calibri; font-size: medium">Get relevant product categories for website’s keywords. Find out what products or services websites advertise in paid search and what topics they rank for in organic SERPs.</p>
                        <a href="DataDiscoveryTools/GetCategoriesfordomain.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Get Categories For Domain</b></></a><br />
                    </div>
                </div>
            </div>


            <div class="panel-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <b><a data-toggle="collapse" href="#collapse14">Daily Tracking Requests</a></b>
                        </h4>
                    </div>
                    <div id="collapse14" class="panel-collapse collapse">
                        <br />
                        <p style="font-family: Calibri; font-size: medium">Display all Sending Request and Receiving Tracking keywords Count.</p>
                        <a href="Tracking/Dailycount.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Daily Count</b></></a>
                        <br />
                    </div>
                </div>
            </div>

            <div class="panel-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <b><a data-toggle="collapse" href="#collapse16">Flux Results</a></b>
                        </h4>
                    </div>
                    <div id="collapse16" class="panel-collapse collapse">
                        <br />
                        <p style="font-family: Calibri; font-size: medium">To Fetch all Elements keywords Count in RapidTracking According to Search Engine. </p>
                        <a href="RapidTrackingSERPs/Elementslist.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">ElementsList</b></></a><br />
                        <hr style="border-color: black" />
                        <p style="font-family: Calibri; font-size: medium">To Fetch all Elements Item Urls Count in RapidTracking According to Search Engine. </p>
                        <a href="RapidTrackingSERPs/Elementslistitemurls.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">ElementsList Item Urls</b></></a><br />
                        <hr style="border-color: black" />
                        <p style="font-family: Calibri; font-size: medium">To Fetch the Average Movement of FluxResults Occured For the Last 2 Days. </p>
                        <a href="FluxData/Fluxresults.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Flux Results</b></></a><br />
                        <hr style="border-color: black" />
                        <p style="font-family: Calibri; font-size: medium">To Fetch the MatchingURLs . </p>
                        <a href="FluxData/Matchingurlscount.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Flux Results Matching URLs Count=0</b></></a><br />
                    </div>
                </div>
            </div>

            <div class="panel-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <b><a data-toggle="collapse" href="#collapse13">Internal Links [ Website ]</a></b>
                        </h4>
                    </div>
                    <div id="collapse13" class="panel-collapse collapse">
                        <br />
                        <p style="font-family: Calibri; font-size: medium">Here in Internal Links we Scrap a Website and Collect all the Internal Links in a website,Text of the link,Text type (i.e) either image/text,Link Type (i.e) Internal/External. </p>
                        <a href="Internallinks/internallinks.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Internal Links</b></></a><br />
                    </div>
                </div>
            </div>

            <div class="panel-group">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <b><a data-toggle="collapse" href="#collapse19">Trending Workspace</a></b>
                        </h4>
                    </div>
                    <div id="collapse19" class="panel-collapse collapse">
                        <br />
                        <p style="font-family: Calibri; font-size: medium">Trending Workspace captures all the elements like Adwords, Answercard, topstories , twittercards , adwords , videos , images , knowledge graph, Classc Links based on their position for the specified keyword for all search engines.</p>
                        <a href="TrendingTwoHoursResultsFromDB/TrendingworkspaceLive.aspx" target="_blank"><b style="font-size: 16px; font-family:Cambria">Trending Workspace Live</b></></a>
                        <br />
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div id="footerDB">
                <div style="font-weight: bold; font-size: 14px;">© <%= DateTime.Now.Year %> Pi Datametrics  |  Dashboard.</div>
            </div>

        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
      

        <div class="loading" align="center">
            <b>Loading..... Please wait.....</b><br />
            <br />
            <img src="images/spinner_squares_circle.gif" alt="" />
        </div>

        <span id="lblIPAddress"></span>
    </form>
</body>
</html>
