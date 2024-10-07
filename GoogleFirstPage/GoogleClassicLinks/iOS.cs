using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Linq;

namespace GoogleFirstPage.GoogleClassicLinks
{
    public class iOS
    {
        int orgLinks;
        string html;
        string seid = string.Empty;//23-06-2023
        public string ProcessDocument(string seid, string keyword, HtmlDocument doc, out int count)
        {
            this.seid = seid;//23-06-2023
            count = 0;
            if (doc == null) throw new Exception("No source found.");
            orgLinks = 0;
            string ndText = "";

            html = doc.DocumentNode.OuterHtml;
            StringBuilder sb = new StringBuilder();
            sb.Append("<searchResult searchEngine=\"" + seid + "\" keyword=\"" + WebUtility.HtmlEncode(keyword) + "\" date=\"" + DateTime.Today.ToString("yyyy-MM-dd") + "\" >");
            try  //28-09-2020  try catch.
            {
                sb.Append("<section col=\"main\">");
                string topStuff = GetTopStuff(doc);
                ndText = topStuff;
                sb.Append(topStuff);

                HtmlNodeCollection nodeCol = doc.DocumentNode.SelectNodes("//div[@class='Lgnr0e J88qA vgnU9e BmP5tf']/div[@class='MUxGbd v0nnCb lyLwlc']|//div[@class='Lgnr0e J88qA vgnU9e BmP5tf']/div/div[@class='MUxGbd v0nnCb lyLwlc']");   //29-04-2020
                if (nodeCol != null)
                    nodeCol = nodeCol[nodeCol.Count - 1].SelectNodes("a/div");  //28-04-2020
                if (nodeCol == null)//20-09-2023
                    nodeCol = doc.DocumentNode.SelectNodes("//div[@id='rso']/div[@class='MjjYud']/div|//div[@id='rso']/div[@class='MjjYud']/block-component|//div[@id='rso']/div[@class='MjjYud']/c-wiz|.//div[contains(@class,'TzHB6b')]|.//div[@class='EyBRub']/div/div[@class='Ww4FFb vt6azd']");//23-07-2024//22-07-2024
                if (nodeCol == null || doc.DocumentNode.SelectNodes("//div[@class='WtZO4e']/div|//div[@classname='WtZO4e']/div")?.Count > 1)//25-09-2023
                    nodeCol = doc.DocumentNode.SelectNodes("//div[@id='rso']/div|//div[@id='rso']/g-card|//div[@id='taw']/div[@class='med']/div[2]/div" +
                    "|//div[@id='rso']/nav|//div[@id='rso']/block-component/div|//div[contains(@id, 'arc-srp')]/div/div[@class='MjjYud']" +
                    "|//div[contains(@id, 'arc-srp')]/div/div/div[@class='MjjYud']|//div[contains(@class,'TzHB6b')]|//div[contains(@id, 'arc-srp')]/div/div/div[@id='tads']|//div[contains(@id, 'arc-srp')]/div/div[@id='tadsb']|.//div[@class='EyBRub']/div/div[@class='Ww4FFb vt6azd']");//24-07-2024//04-07-2024//23-05-2024
                if (nodeCol != null && nodeCol.Count == 1)
                    nodeCol = doc.DocumentNode.SelectNodes("//div[@id='rso']/div|//div[@class='vC5Ym DhKAUb']/div");  //23-03-2023  //17-09-2019
                if (nodeCol != null && nodeCol.Count <= 5 && doc.DocumentNode.SelectNodes("//div[contains(@id,'kp-wp-tab-')]") != null)//02-02-2024//31-01-2024
                    nodeCol = doc.DocumentNode.SelectNodes("//div[contains(@class,'Ww4FFb vt6azd DlUvEb')]|//div[contains(@class,'uVMCKf Ww4FFb vt6azd')]" +
                    "|//div[contains(@class,'wHYlTd Ww4FFb vt6azd')]|//div[contains(@class,'Lv2Cle Ww4FFb vt6azd')]|//div[contains(@class,'Ww4FFb vt6azd xpd')]" +
                    "|//div[contains(@class,'Ww4FFb vt6azd oGMpge')]|//div[contains(@class,'Ww4FFb vt6azd tRkSqb')]|//div[contains(@class,'Ww4FFb vt6azd F6CFcc')]" +
                    "|//div[@class='lU8tTd']|//g-card[@class='g F6CFcc']|.//div[@class='Ww4FFb vt6azd g']|.//div[@class='AGopnf']");//26-09-2024//10-09-2024//09-05-2024//12-03-2024
                if (nodeCol == null)
                    nodeCol = doc.DocumentNode.SelectNodes("//*[@id='tscffb']");

                if (nodeCol == null) throw new Exception("No block found.");
                //if (nodeCol == null) return string.Empty; 
                //if (nodeCol == null) goto BOTTOMSTUFF;             

                foreach (HtmlNode node in nodeCol)
                {
                    if (node.Attributes["class"]?.Value?.Contains("Ww4FFb vt6azd DlUvEb") == true && node.SelectSingleNode(".//div[@class='lU8tTd']") != null)//05-04-2024
                        continue;//05-04-2024
                    HtmlNode fsh = node.SelectSingleNode(".//*[@id='knowledge-finance-wholepage__fw-sticky-header']");
                    if (fsh != null)
                    {
                        HtmlNodeCollection nc = node.SelectNodes("./[@class='knowledge-finance-wholepage__section wp-ms']"); // /div[1]
                        if (node.HasClass("kp-wholepage"))
                        {
                            continue;
                        }
                        try
                        {
                            if (nc != null)
                            {
                                foreach (HtmlNode nd in nc)
                                {
                                    string s = ProcessNode(nd);
                                    ndText += s;
                                    if (s.Length > 0)
                                        sb.Append(s);
                                }
                                break;
                            }
                        }
                        catch { }

                    }
                    //changes on 06-08-2019
                    if ((node.SelectSingleNode(".//div[@id='knowledge-finance-wholepage__entity-summary']") != null
                        || node.InnerText.Contains("Finance results")) && node.SelectSingleNode(".//div[@class='srg']") != null)
                    {
                        
                    }
                    if (node.HasClass("kp-wholepage") || node.SelectNodes(".//div[contains(@class, 'kp-wholepage')]") != null)
                    {
                        // changed on 05-07-2019
                        HtmlNode n = node.SelectSingleNode(".//div[@class='PyJv1b kno-fb-ctx gsmt PZPZlf']/span[@role='heading']");
                        if (n != null)//25-09-2023
                        {
                            if (node.SelectSingleNode(".//div[contains(@class,'WlTAzf ')]|.//div[@jsname='dTDiAc']") == null || node.SelectSingleNode(".//div[contains(@class,'XbtRGb qxsd')]") != null)//23-10-2023//25-09-2023//22-09-2023
                            {
                                
                            }//22-09-2023
                        }//25-09-2023
                        continue;
                    }
                    try
                    {
                        if (node.InnerHtml != "")
                        {
                            string s = ProcessNode(node);
                            ndText += s;
                            if (s.Length > 0)
                                sb.Append(s);
                        }
                    }
                    catch
                    { }
                }

                if (string.IsNullOrEmpty(ndText.Trim()) || orgLinks == 0)
                {
                    foreach (HtmlNode node in nodeCol)
                    {
                        try
                        {
                            if (node.HasClass("kp-wholepage") || node.SelectNodes(".//div[contains(@class, 'kp-wholepage')]") != null)
                            {
                                //Current 15-12-2020 swapped from bottom HtmlNodeCollection
                                HtmlNodeCollection nc = node.SelectNodes(".//div[contains(@class,'Ww4FFb vt6azd')]|//div[contains(@id, 'arc-srp')]/div/div[@class='MjjYud']|//div[contains(@id, 'arc-srp')]/div/div/div[@class='MjjYud']");//13-08-2024//29-03-2023
                                if (nc == null)
                                    nc = node.SelectNodes(".//div[contains(@class,'TzHB6b')]|.//div[@class='MjjYud']/div/div/div[@class='Ww4FFb vt6azd xpd EtOod pkphOe']|.//div[@class='Lv2Cle Ww4FFb vt6azd']");//23-03-2023//14-12-2022
                                if (nc == null || nc.Count <= 3) //14-12-2022
                                    nc = node.SelectNodes(".//div[@class='TjcfIc eE3xqf B03h3d V14nKc ptcLIOszQJu__wholepage-card wp-ms']|.//div[@class='WvKfwe a3spGf']/div");//13-12-2022 //28-11-2022
                                if (nc == null)//28-11-2022
                                    nc = node.SelectNodes(".//div[@class='WvKfwe']/div|.//div[@class='WvKfwe a3spGf']/div" +
                                  "|.//div[@class='ChlgHf']|.//div[@class='a3spGf WvKfwe']/div|.//div[contains(@class,'TzHB6b mnr-c UBoxCb')]" + //17-11-2022
                                    "|.//div[@class='WvKfwe a3spGf']/g-card|.//div[@class='WvKfwe a3spGf']/block-component");//20-05-2022  
                                //if (nc == null || node.SelectNodes(".//div[@id='kp-wp-tab-overview']/div") != null)//07-10-2021 answer card and PAA blocks
                                if (nc == null)//02-09-2022
                                    nc = node.SelectNodes(".//div[@id='kp-wp-tab-overview']/div"); //15-12-2020
                                //end of swapped
                                if (nc == null) //|.//div[@class='a3spGf WvKfwe']/div //23-05-2020
                                    nc = node.SelectNodes(".//div[@class='Kot7x eXEBMb Znsfnf']/div[@class='GhpATe pttBJc']"); //15-04-2020
                                if (nc == null)//|.//div[@class='kp-blk c2xzTb OJXvsb']//23-05-2020
                                    nc = node.SelectNodes(".//div[@class='UDZeY']/div|.//div[@class='vC5Ym']/div|.//div[@class='kp-blk cUnQKe Wnoohf OJXvsb']");       //23-05-2020
                                if (nc == null)
                                    nc = node.SelectNodes(".//div[@class='MRWHue']");
                                if (nc == null)
                                    nc = node.SelectNodes(".//div[@class='Lgnr0e J88qA vgnU9e BmP5tf']/div"); //22-01-2020
                                if (nc == null)
                                    nc = node.SelectNodes(".//div[@class='a3spGf WvKfwe']");
                                foreach (HtmlNode nd in nc)
                                {
                                    if (nd.InnerHtml != "")
                                    {
                                        string s = string.Empty;
                                        try
                                        {
                                            s = ProcessNode(nd);
                                        }
                                        catch { }
                                        ndText += s;
                                        if (s.Length > 0)
                                            sb.Append(s);
                                    }
                                }
                                break;
                            }
                        }
                        catch { }
                    }
                }

                //BOTTOMSTUFF:
                string bottomStuff = "";
                ndText += bottomStuff;
                sb.Append(bottomStuff);
                sb.Append("</section>");

                sb.Append("<section col=\"right\">");
                string rightStuff = "";
                ndText += rightStuff;
                sb.Append(rightStuff);
                sb.Append("</section>");
                sb.Append("</searchResult>");

                if (ndText.Length > 0)
                {
                    count = orgLinks;
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;

        }

        private string GetRightStuff(HtmlDocument doc)
        {
            StringBuilder s = new StringBuilder();
            // product listed ads
            HtmlNode rcNode = doc.DocumentNode.SelectSingleNode("//div[@id='rhs_block']");
            if (rcNode == null)
                return string.Empty;

            HtmlNode sNode = rcNode.SelectSingleNode(".//div[@class='cu-container']");
            if (sNode != null)
            {
                //HtmlNodeCollection col = sNode.SelectNodes(".//a[@class='plantl pla-unit-title-link']|.//div[@class='mnr-c pla-unit']/a[2]|.//a[@class='plantl pla-unit-single-clickable-target clickable-card']");
                //if (col == null)
                HtmlNodeCollection col = sNode.SelectNodes(".//div[@class='_Ead']/a[2]");
                if (col != null)
                {
                    foreach (HtmlNode nd in col)
                    {
                        
                    }
                }
            }
            return s.ToString();
        }

        private string GetBottomStuff(HtmlDocument doc)
        {
            StringBuilder s = new StringBuilder();
            if (doc.DocumentNode.SelectSingleNode("//div[@class='mnr-c IGtt6d imgac']") != null || doc.DocumentNode.SelectSingleNode("//div[@class='IGtt6d imgac mnr-c']") != null) //22-06-2021 bottom PLAds 
            {
                HtmlNode pla = doc.DocumentNode.SelectSingleNode(".//div[contains(@class, 'commercial-unit-mobile-bottom')]");//18-01-2024
                if (pla != null)
                {

                }
            }//25-09-2019
            return s.ToString();
        }


        private string GetTopStuff(HtmlDocument doc)
        {
            StringBuilder s = new StringBuilder();

            // carousel
            HtmlNode crNode = doc.DocumentNode.SelectSingleNode("//div[@id='appbar']");
            if (crNode != null)
            {

            }

            crNode = doc.DocumentNode.SelectSingleNode("//div[@id='taw']");
            if (crNode != null)
            {
                //// apps
                HtmlNode App = crNode.SelectSingleNode(".//div[@class='kvsF2b']|.//div[@class='i5ai3 gsmt']"); //d5oMvf
                if (App != null)
                {
                    //HtmlNode App1 = App.SelectSingleNode(".//h3[@class='header-title yovt']");
                    
                }
                // mnr-c IGtt6d imgac qs-ic fp-w cTMkTb
                // product listed ads //start of change 11-01-2022 line number 409 to 498
                if (doc.DocumentNode.SelectSingleNode("//div[@class='mnr-c IGtt6d imgac cTMkTb']") != null
                   || doc.DocumentNode.SelectSingleNode("//div[@class='mnr-c IGtt6d imgac qs-ic fp-w cTMkTb']") != null
                   || doc.DocumentNode.SelectSingleNode("//div[contains(@class,'IGtt6d imgac mnr-c')]|//div[@id='tads']/g-card/div[contains(@class, 'mnr-c')]") != null //18-01-2024 //01-02-2022 //16-09-2021 missing ProductListAds
                   || doc.DocumentNode.SelectSingleNode("//div[@id='activities-carousel-container']") != null //11-01-2022
                   || doc.DocumentNode.SelectSingleNode("//div[@class='mnr-c Ioy6jb']") != null //07-02-2022
                   )
                {
                    
                }//end of change //11-01-2022

                HtmlNodeCollection col = crNode.SelectNodes(".//div[contains(@id,'tads')]/ol/li");
                if (col == null)
                    col = doc.DocumentNode.SelectNodes("//div[@id='tads']/div/ol/li|//div[@jsname='hWE2jd']|//div[@id='tads']/div[@class='uEierd']|.//div[@id='tads']/div/div[@class='uEierd']" +
                        "|//div[@id='tads']/div[@class='mnr-c O9g5cc uUPGi']|//div[contains(@class,'yDDB0e')]");//19-08-2022//28-03-2022 "/div" included//04-10-2021 updated selector for missing adwords
                if (col != null)
                {
                    
                }

                //// answercards
                HtmlNode answernode = crNode.SelectSingleNode(".//div[@class='xpdopen']|.//div[@class='xpdopen rYczAc']");
                if (answernode != null)
                {
                    bool video = false;
                    HtmlNode ndv = answernode.SelectSingleNode(".//div[@class='srg']");
                    if (ndv != null)
                    {
                        var vlnk = ndv.SelectSingleNode(".//a");
                        if (vlnk != null)
                            if (vlnk.Attributes["href"].Value.Contains("youtube."))
                            {
                                
                            }
                    }

                    if (!video)
                    {
                        
                    }
                }

                //starts 01-01-2021
                HtmlNode kgNode = crNode.SelectSingleNode(".//div[@class='p7xMX qs-ic fp-w']");//01-01-2021
                if (kgNode == null)
                    kgNode = crNode.SelectSingleNode(".//div[@class='c ptJHdc commercial-unit-mobile-top']|.//div[@class='SPZz6b']");
                // ends 01-01-2021


                if (kgNode != null)
                {
                    if (kgNode.SelectSingleNode(".//div[@class='navigation']|.//div[@class='kno-ecr-pt kno-fb-ctx HOpgu gsmt']") != null)     //'kp-blk knowledge-panel _Rqb _RJe']") != null)
                    {
                        
                    }
                }
            }
            return s.ToString();
        }

        private string ProcessNode(HtmlNode node)
        {
            try  //28-09-2020  try catch.
            {
                string sb = "";
                if (IsBlock(node))
                {
                    
                }
                else if (IsOrganic(node))
                {
                    sb = ProcessOrganic(node);
                }
                return sb;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string ProcessOrganic(HtmlNode node)
        {
            StringBuilder s = new StringBuilder();
            HtmlNodeCollection nds;

            if (node.SelectSingleNode(".//div[@jscontroller='iht5n']") != null)
                nds = node.SelectNodes(".//div[@jscontroller='iht5n']/div");
            else
            {
                //nds = node.SelectNodes(".//div[@class='mnr-c waTp2e xpd O9g5cc uUPGi']|.//div[@class='mnr-c luh4tb xpd O9g5cc uUPGi']|.//div[@class='mnr-c xpd O9g5cc uUPGi']|.//div[@class='g mnr-c']");//12=11-2021 CL//12-11-2021//24-03-2021//15-12-2020 removed selector//06-10-2020 classic link selector //19-06-2020   //20-01-2020 selector changed for two classic links
                nds = node.SelectNodes(".//div[@class='mnr-c waTp2e xpd O9g5cc uUPGi']|.//div[@class='mnr-c luh4tb xpd O9g5cc uUPGi']"); //15-11-2021
                nds = (node.SelectNodes(".//div[contains(@id,'tsuid')]//a[@class='Xhbgy']") != null && node.SelectNodes(".//div[contains(@class,'KJDcUb')]") != null) ? nds = node.SelectNodes(".//div[@class='mnr-c xpd O9g5cc uUPGi']") : nds = null; //21-02-2022
                //end 26-03-2021
                if (nds == null)
                    //nds = node.SelectNodes(".//div[@class='BYM4Nd']|.//div[@class='mnr-c OH1ZUd xpd O9g5cc uUPGi']");//27-01-2022 classic links //14-01-2022
                    nds = (node.SelectNodes(".//div[@class='BYM4Nd']|.//div[@class='mnr-c OH1ZUd xpd O9g5cc uUPGi']") != null) ? nds = node.SelectNodes(".//div[@class='UDZeY']/div/div") : nds = null;
                if (nds == null && node.SelectNodes(".//div[@class='BYM4Nd']|.//div[@class='mnr-c OH1ZUd xpd O9g5cc uUPGi']") != null) //26-09-2022//21-02-2022
                    nds = node.SelectNodes(".//div[@class='BYM4Nd']|.//div[@class='mnr-c OH1ZUd xpd O9g5cc uUPGi']|.//div[@class='Ww4FFb vt6azd xpd EtOod pkphOe']");//26-09-2022//21-02-2022
                if (nds == null)
                    if (node.SelectSingleNode(".//div[contains(@class,'YgXj7b')]|.//div[@class='Y37F6d Nn2Stf']/img") == null)//22-09-2021 missing video block//08-05-2021 applied contains //19-01-2021
                        nds = node.SelectNodes(".//div[contains(@class,'KJDcUb')]|.//div[@class='Lgnr0e J88qA vgnU9e BmP5tf']|.//g-card[@id='tscffb']|.//div[@class='mnr-c PHap3c']|.//div[contains(@class, 'Ww4FFb vt6azd PHap3c')]" + //14-12-2022
                         "|.//div[@jsname='wRSfy']|.//g-card[@class='g F6CFcc']|.//div[@class='mnr-c xpd O9g5cc uUPGi']|.//div[@class='urrG9 v5yQqb jqWpsc']|.//g-card[@class='T98FId']" + //11-07-2023 //31-05-2022
                        "|.//div[@class='mnr-c']/div/div[@class='P8ujBc v5yQqb jqWpsc']|.//div[contains(@class,'EtOod pkphOe')]|.//div[@class='mnr-c']/div/div[@class='P8ujBc jqWpsc']" +
                        //"|.//div[@class='fhQnRd']|.//div[@id='iur']|.//div[contains(@class,'cUnQKe wHYlTd Ww4FFb vt6azd')]|.//div[@class='uVMCKf Ww4FFb vt6azd']|.//div[@class='Ww4FFb vt6azd tRkSqb']|.//div[@class='xLjnU']");//28-08-2023 //18-08-2023
                        "|.//div[@class='fhQnRd']|.//div[@id='iur']|.//div[contains(@class,'Ww4FFb vt6azd')]");//01-09-2023//30-08-2023
                if (nds == null) //24-11-2022
                    nds = node.SelectNodes(".//div[@class='P8ujBc v5yQqb jqWpsc']|.//div[contains(@class,'Ww4FFb vt6azd')]|.//div[contains(@class,'EtOod pkphOe')]");//06-02-2023//16-01-2023 //24-11-2022//14-09-2022
                if (nds == null)
                    nds = node.SelectNodes(".//div[@class='mnr-c O9g5cc uUPGi']|.//div[@class='mnr-c xpd O9g5cc uUPGi']|.//div[@class='HD8Pae mnr-c xpd O9g5cc uUPGi']|.//div[@class='mnr-c']/div/div[contains(@class,'P8ujBc')]|.//div[@class='mnr-c P5XtRe']" + //25-03-2022 //22-02-2022
                        "|.//div/g-card[@class='XqIXXe']|.//g-card[@id='tscffb']|.//g-card[@class='g F6CFcc']|.//div[@class='khgTR lWEpfd']" +
                        "|.//div[@class='khgTR R5lVqb']|.//div[@class='mnr-c fp-w qs-ic aig-grd']|.//g-card[@class='URhAHe']" +
                        "|.//div[@class='mnr-c IcwJCe']|.//div[@class='g card-section svwwZ']|.//div[@class='g mnr-c']" + //15-11-2021 //03-11-2020//26-08-2020 incuded contains functions to the selector//29-07-2020 //20-05-2020 missing classic link //05-06-2020
                        "|.//div[contains(@class,'card-section')]|.//div[@class='wU9Tkd']|.//div[@jsname='wRSfy']|.//div[@class='tKdlvb jqWpsc']" +
                         //"|.//div[@class='mnr-c YibVsd']|.//div[@class='mnr-c xpd EtOod pkphOe']|.//div[contains(@class,'EtOod pkphOe')]");//30-06-2022//10-06-2022//16-12-2021 commented //29-06-2022
                         "|.//div[@class='mnr-c YibVsd']|.//div[contains(@class,'EtOod pkphOe')]|.//div[contains(@class,'Ww4FFb vt6azd')]");//02-09-2022 video block
                if (nds == null)//22-02-2022
                                // if (node.Attributes["class"]?.Value == "mnr-c" && node.SelectSingleNode(".//div/div[contains(@class,'P8ujBc')]") != null)//29-09-2022//22-02-2022
                    nds = node.SelectNodes(".//div/div[contains(@class,'P8ujBc')]");//22-02-2022
                if (nds == null)
                    if (node.Attributes["class"]?.Value == "mnr-c xpd O9g5cc uUPGi") //09-09-2021 applied ? condition
                        nds = node.SelectNodes(".//div[contains(@class,'KJDcUb')]"); //28-07-2020 //29-07-2020 included contains function
                if (nds == null)
                    nds = node.SelectNodes(".//div[@class='setTDc']|.//div[contains(@class, 'P8ujBc v5yQqb')]|.//div[@class='WFyfFf yOwOKc']|.//div[@class='kb0PBd cvP2Ce jGGQ5e']");//04-06-2024//14-02-2024
                if (nds == null && (node.Attributes["jsname"]?.Value == "pKB8Bc" || node.Attributes["class"]?.Value == "Ww4FFb vt6azd"))//1e-08-2024//14-02-2024
                    nds = node.SelectNodes(".");//14-02-2024//21-09-2023 //02-02-2022 moved from 687 line   // 25-10-2019
                if (nds == null)//30-09-2024
                    nds = node.SelectNodes(".//div[@class='AGopnf']");//30-09-2024
            }
            if (nds != null)
            {
                foreach (HtmlNode nd in nds)
                {
                    try
                    {
                        //12-11-2021 duplicates CLs
                        if (nd.Attributes["class"]?.Value != null)
                        {
                            if (nd.SelectSingleNode(".//div[@class='" + nd.Attributes["class"].Value + "']") != null)
                                continue;
                        }//12-11-2021
                        if (nd.SelectSingleNode(".//div[@jscontroller='i5z2Rc']") != null
                            || nd.SelectSingleNode(".//div[@class='Lgnr0e J88qA vgnU9e BmP5tf']") != null //13-12-2019
                             || nd.SelectSingleNode(".//div[@class='MUxGbd v0nnCb lyLwlc']") != null   //16-12-
                             || nd.SelectSingleNode(".//div[contains(@class,'E8hWLe SVMeif')]") != null //20-09-2024 //21-07-2022
                             || nd.SelectSingleNode(".//div[@class='GssVdc']") != null)//04-07-2024
                        {
                            s.Append(GetSiteLinks(nd));
                            continue;
                        }
                        //21-02-2020  included selector for the Apps Block
                        if (nd.SelectSingleNode(".//div[@class='ki5rnd']|.//div[@class='yR4jwc']|.//div[@class='UyqAp']|.//div[@class='qs-io aig-lst']") != null)//30-08-2023 //31-08-2020  //20-05-2020 included selector for app block
                        {
                            continue;
                        }
                        //27-09-2019
                        if (nd.HasClass("F6CFcc") || nd.SelectSingleNode(".//g-inner-card[@class='Bf5NPb']|.//g-tray-header[contains(@class, 'Bf5NPb')]") != null)//30-08-2023//twitter block //18-08-2023  
                        {
                            if (nd.SelectSingleNode(".//div[contains(@class,'qdrjAc Dwsemf')]|.//div[@class='Bv2VAe']") != null)//30-08-2023 //28-10-2021  //11-11-2019
                            {
                                s.Append(GetTwitterCards(nd));
                                continue;
                            }
                        }
                        if (nd.SelectSingleNode(".//g-inner-card[@class='zf84ud THG0oc VoEfsd']") != null)
                            continue;
                        //end 27-09-2019

                        try
                        {
                            // 17-10-2019
                            if (nd.Name == "g-card" || nd.Attributes["class"]?.Value == "HD8Pae mnr-c xpd O9g5cc uUPGi" || nd.Attributes["jsname"]?.Value == "wRSfy") //11-10-2021
                            {
                                if (nd.SelectNodes(".//div[@class='g card-section jiwmWe']") == null)  //27-12-2019
                                {
                                    
                                }
                            }
                            // end 17-10-2019
                        }
                        catch { }

                        if (nd.Attributes["data-hveid"] != null)    // || nd.SelectSingleNode(".//div[@class='U3THc']") != null)
                        {
                            if (!Regex.IsMatch(nd.Attributes["data-hveid"].Value, "C..QAA") && (!Regex.IsMatch(nd.Attributes["data-hveid"].Value, "C....")))

                                continue;


                            HtmlNode img = nd.SelectSingleNode(".//div[@class='G5NbBd']/div|.//g-img/img[@class='rISBZc zr758c']|.//span[@class='z1asCe UIgqBe']|.//div[contains(@class, 'i5w0Le')]|.//div[@class='Ylm8Fc']|.//div[@class='HaDSKc']");//10-06-2023//15-02-2023//26-05-2022//16-12-2021

                            if (img != null)
                            {
                                try
                                {
                                    if (Regex.IsMatch(img.OuterHtml, "id=\"vidthumb\\d*\"") || Regex.IsMatch(img.OuterHtml, "id=\"dimg_\\d*\"") || img.Attributes["class"].Value.Contains("__video-result") || nd.SelectSingleNode(".//div[contains(@class,'c8rnLc flgn0c')]")!=null || nd.SelectSingleNode(".//video-voyager") != null  || nd.SelectSingleNode(".//div[@class='J1mWY']")  != null)//10-06-2023//29-03-2023//27-03-2023 //08-04-2022 videos
                                    {
                                        HtmlNode vdo = nd.SelectSingleNode(".//a[contains(@class,'BmP5tf')]"); //11-11-2021
                                        // video block.
                                        if (vdo == null)
                                            vdo = nd.SelectSingleNode(".//div[@class='th N3nEGc']/a|.//a[@class='d3q37c']|.//div[@class='T61Aje v5yQqb']/a");//19-09-2024//14-02-2024
                                        if (vdo != null && nd.SelectNodes(".//g-scrolling-carousel[@class='kQ9KOd']") == null)//28-09-2022
                                        {
                                            string url = vdo.Attributes["href"].Value;
                                            string title = ""; // vdo.SelectSingleNode(".//div[contains(@class, 'MUxGbd v0nnCb')]").InnerText; //13-07-2022 //22-03-2021
                                            HtmlNode t = vdo.SelectSingleNode(".//div[contains(@class, 'MUxGbd v0nnCb')]");
                                            if (t == null)
                                                t = nd.SelectSingleNode(".//div[@role='heading']");//12-12-2023
                                            if (t != null)
                                                title = t.InnerText; //end 13-07-2022
                                            if (url.StartsWith("http") || url.StartsWith("https") || url.StartsWith("ftp")) //30-04-2020
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                                catch { }
                            }


                            HtmlNode n = nd.SelectSingleNode(".//div[@class='ZINbbc xpd']/div/a");
                            if (n == null)
                                n = nd.SelectSingleNode(".//div[@class='ZINbbc xpd']/div[1]/a");
                            if (n == null)
                                n = nd.SelectSingleNode(".//div[@class='NJo7tc Z26q7c']/div/a|.//div[@class='Z26q7c VGXe8']/div/a");//13-07-2022
                            if (n == null)
                                n = nd.SelectSingleNode(".//a[@class='C8nzq JTuIPc amp_r']");
                            if (n == null)
                                n = nd.SelectSingleNode(".//a[@class='C8nzq JTuIPc']");
                            if (n == null)
                                n = nd.SelectSingleNode(".//a[@class='C8nzq BmP5tf amp_r']");  // 10-06-2020 swapped from below
                            if (n == null)
                                n = nd.SelectSingleNode(".//a[@class='C8nzq Tj0U2 BmP5tf']");  // 27-11-2019
                            if (n == null)
                                n = nd.SelectSingleNode(".//a[@class='C8nzq BmP5tf']");   // 10-06-2020 swapped from above
                            if (n == null)
                                n = nd.SelectSingleNode(".//a[@class='C8nzq Tj0U2 BmP5tf amp_r']"); // 29-11-2019
                            if (n == null)
                                n = nd.SelectSingleNode(".//a[contains(@class,'cz3goc')]");//20-09-2024//03-02-2023 //31-05-2022
                            if (n == null)
                                n = nd.SelectSingleNode(".//a[contains(@class,'cz3goc BmP5tf')]");//03-02-2023 //31-05-2022
                            if (n == null)
                                n = nd.SelectSingleNode(".//a[contains(@class,'sXtWJb')]"); //16-12-2020
                            if (n == null)//13-08-2024
                                n = nd.SelectSingleNode(".//a[@class='rTyHce']");//13-08-2024
                            if (n == null)
                                n = nd.SelectSingleNode(".//g-link/a");
                            if (n == null)
                            {
                                n = nd.SelectSingleNode(".//div[@class='rc']");
                                if (n != null)
                                    n = nd.SelectSingleNode(".//h3[@class='r']/a|.//h3[@class='r']/div/a|.//h3[contains(@class,'yuRUbf JtG40d')]/a"); //04-11-2020 //09-06-2020 //16-09-2020 included selector for classic link
                            }
                            if (n != null)
                            {
                                string u = n.Attributes["href"].Value;
                                HtmlNode d = n.SelectSingleNode(".//div[@role='heading']");
                                string t = "";
                                if (d != null)
                                    t = d.InnerText;
                                else
                                    t = n.InnerText;

                                if (orgLinks < 100)
                                {
                                    if (u.StartsWith("http") || u.StartsWith("https") || u.StartsWith("ftp")) //30-04-2020
                                    {

                                        s.Append("<item url=\"" + SetUrl(u) + "\" />");   // 
                                        orgLinks++;
                                    }
                                }

                            }
                        }
                        else if (nd.Attributes["class"] != null || nd.Attributes.Count == 0)
                        {
                            if (nd.Attributes.Count == 0 || nd.Attributes["class"].Value == "mnr-c" || nd.Attributes["class"].Value == "mnr-c xpd O9g5cc uUPGi"
                             || nd.Attributes["class"].Value == "mnr-c O9g5cc uUPGi" || nd.Attributes["class"].Value == "mnr-c waTp2e xpd O9g5cc uUPGi"
                             || nd.Attributes["class"].Value == "MGqjK" || nd.Attributes["class"].Value == "setTDc" || nd.Attributes["class"].Value.Contains("khgTR") //26-08-2020    // 20-05-2020
                             || node.Attributes["class"]?.Value == "mnr-c xpd O9g5cc uUPGi" || nd.Attributes["class"].Value == "KJDcUb" // 14-12-2020  //20-01-2020 // selectors for two classic links block
                             || nd.Attributes["class"].Value == "mnr-c luh4tb xpd O9g5cc uUPGi" || nd.Attributes["class"].Value == "mnr-c PHap3c" //05-01-2021
                             || nd.Attributes["class"].Value == "g card-section svwwZ" || nd.Attributes["class"].Value == "card-section"  //15-12-2020//03-11-2020 //06-10-2020 classic type block type
                             || nd.Attributes["class"].Value == "d5oMvf KJDcUb" || nd.Attributes["class"].Value == "c6gxKe card-section" //20-05-2021) //09-02-2021
                             || nd.Attributes["class"].Value.Contains("KJDcUb") || nd.Attributes["class"].Value == "mnr-c OH1ZUd xpd O9g5cc uUPGi" //27-01-2022 classic links //09-09-2021 missing classic links
                            // || nd.Attributes["class"].Value.Contains("mnr-c xpd EtOod pkphOe")//30-09-2022 commented //10-06-2022
                             || nd.Attributes["class"].Value.Contains("EtOod pkphOe") || nd.Attributes["class"].Value.Contains("WFyfFf")//14-02-2024 //30-06-2022//29-06-2022
                             || nd.Attributes["class"].Value == "wU9Tkd" || nd.Attributes["class"].Value.Contains("P8ujBc") //22-02-2022 //01-02-2022 //10-07-2021
                             || nd.Attributes["class"].Value == "g card-section" || nd.Attributes["class"].Value == "card-section svwwZ" || nd.Attributes["class"].Value == "kb0PBd cvP2Ce jGGQ5e") //21-09-2023//30-08-2021 missing classic link
                            {
                                //17-10-2019
                                
                                // end 17-10-2019

                                HtmlNode img = nd.SelectSingleNode(".//g-img[contains(@class,'P64nJb')]//img|.//div[@class='BNeawe wyrwXc HrGdeb']|.//div[@class='Y37F6d Nn2Stf']/img"); //08-04-2022
                                if (img != null)
                                {
                                    try
                                    {
                                        //19-10-2021 commented
                                        //if (Regex.IsMatch(img.OuterHtml, "id=\"vidthumb\\d*\"") || nd.SelectSingleNode(".//div[contains(@class,'YgXj7b')]|.//div[contains(@class,'qW7zYd')]") != null || node.SelectSingleNode(".//div[@class='YgXj7b']|.//div[@class='Y37F6d Nn2Stf']") != null || nd.SelectSingleNode(".//div[@class='hq7Nmd']|.//div[@class='UT9Awd S3PB2d']") != null) //04-05-2022
                                        if (Regex.IsMatch(img.OuterHtml, "id=\"vidthumb\\d*\"") || nd.SelectSingleNode(".//div[contains(@class,'YgXj7b')]|.//div[contains(@class,'qW7zYd')]|.//div[@class='Y37F6d Nn2Stf']") != null || nd.SelectSingleNode(".//div[@class='hq7Nmd']") != null) //01-06-2022
                                            {
                                            HtmlNode n = nd.SelectSingleNode(".//h3[@class='r']/a");
                                            // video block.
                                            if (n == null)
                                                n = nd.SelectSingleNode(".//div[@class='th N3nEGc']/a");
                                            if (n == null) //11-11-2021
                                                n = nd.SelectSingleNode(".//a[contains(@class,'BmP5tf')]");  // 10-06-2020 swapped from below //08-01-2020 //included selector for video block
                                            //if (n == null)
                                            //    n = nd.SelectSingleNode(".//a[@class='C8nzq BmP5tf']");  // 10-06-2020 swapped from above
                                            //if (n == null)
                                            //    n = nd.SelectSingleNode(".//a[@class='C8nzq Tj0U2 BmP5tf']");    // 29-11-2019
                                            //end 11-11-2021
                                            if (n == null)
                                                n = nd.SelectSingleNode(".//div[@class='au0C1b u78HIe']/a"); //15-12-2020 video selector
                                            string url = n.Attributes["href"].Value;
                                            //string title = n.InnerText.Replace("'", "").Replace("\"", ""); //commented on 02-03-2021
                                            string title = nd.SelectSingleNode(".//div[@class='BNeawe UwRFLe']") != null ? nd.SelectSingleNode(".//div[@class='BNeawe UwRFLe']").InnerText : n.InnerText.Replace("'", "").Replace("\"", ""); //02-03-2021 title for video block item urls
                                            //28-10-2019
                                            if (n.SelectSingleNode(".//div[@role='heading']") != null)
                                                title = n.SelectSingleNode(".//div[@role='heading']").InnerText;
                                            if (url.StartsWith("http") || url.StartsWith("https") || url.StartsWith("ftp")) //30-04-2020
                                            {
                                                if (!s.ToString().Contains("<item url=\"" + SetUrl(url) + "\" />"))//16-02-2021 video block if condition to avoid duplicates with classic link
                                                {
                                                    
                                                }
                                                continue;
                                            }
                                        }
                                    }
                                    catch { }

                                }

                                HtmlNode nv = nd.SelectSingleNode(".//div[@class='ZINbbc xpd']/div/a");
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//div[@class='ZINbbc xpd']/div[1]/a");
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//div[contains(@class,'Z26q7c')]/div/a");////15-07-2022 13-07-2022
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//a[@class='C8nzq JTuIPc amp_r']");
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//a[@class='C8nzq JTuIPc']");
                                if (nv == null)//19-09-2024
                                    nv = nd.SelectSingleNode(".//a[contains(@class,'cz3goc OcpZAb')]");//19-09-2024
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//a[@class='C8nzq BmP5tf amp_r']");  // 10-06-2020 swapped from below
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//a[@class='C8nzq Tj0U2 BmP5tf']");  // 27-11-2019
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//a[@class='C8nzq BmP5tf']");  // 10-06-2020 swapped from above
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//a[@class='C8nzq Tj0U2 BmP5tf amp_r']"); // 29-11-2019
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//div[@class='P8ujBc v5yQqb jqWpsc']/a");//16-11-2021 TS Item urls
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//div[@class='P8ujBc jqWpsc']/a"); //22-11-2021 for bad classic links
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//g-link/a");
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//div[@class='fM8c FUksre']/a"); //22-06-2020
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//a[contains(@class,'sXtWJb')]"); //16-12-2020
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//a[contains(@class,'cz3goc BmP5tf')]");//06-10-2022
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//a[@class='d3q37c']");//14-02-2024
                                if (nv == null)
                                    nv = nd.SelectSingleNode(".//div/a");  //25-06-2020
                                if (nv == null)
                                {
                                    nv = nd.SelectSingleNode(".//div[@class='rc']|.//div[@class='ytwLQd']");//05-06-2020 missing classic links
                                    if (nv != null)
                                        nv = nd.SelectSingleNode(".//h3[@class='r']/a|.//h3[@class='r']/div/a|.//h3[contains(@class,'yuRUbf JtG40d')]/a"); //03-11-2020  // 09-06-2020
                                }
                                //if (nv == null)//06-10-2022 commented //01-02-2022 
                                  //  nv = nd.SelectSingleNode(".//a[contains(@class,'cz3goc BmP5tf')]");//10-06-2022 contains//01-02-2022
                                if (nv != null)
                                {
                                    string u = nv.Attributes["href"].Value;
                                    HtmlNode d = nv.SelectSingleNode(".//div[@role='heading']");
                                    if (d == null)
                                        d = nv.SelectSingleNode(".//div[@class='BNeawe vvjwJb AP7Wnd UwRFLe']"); //22-06-2020
                                    if (d == null)
                                        d = nd.SelectSingleNode(".//div[@class='bvTQqb']");//10-07-2021
                                    if (d == null)
                                        d = nd.SelectSingleNode(".//div[@role='heading']");//24-11-2022
                                    string t = "";
                                    if (d != null)
                                        t = d.InnerText;
                                    else
                                        t = nv.InnerText;
                                    //05-01-2023 adwords in middle page
                                    if (node.SelectSingleNode(".//div[@class='eMXfhf']") != null)
                                    {
                                        
                                    }//05-01-2023
                                    else if (orgLinks < 100)
                                    {
                                        u = SetUrl(u);
                                        if (u.StartsWith("http") || u.StartsWith("https") || u.StartsWith("ftp")) //30-04-2020
                                        {
                                            if (!s.ToString().Contains("<item url=\"" + SetUrl(u) + "\"  title=\"" + SetTitle(t) + "\" />"))//30-09-2022
                                            {
                                                s.Append("<item url=\"" + SetUrl(u) + "\" />");   //30-09-2022
                                                orgLinks++;//30-09-2022
                                            }
 
                                        }
                                    }
                                }

                            }
                            else if (nd.Attributes["class"].Value == "g mnr-c srg")
                            {
                                // if it is app link
                                HtmlNode n = node.SelectSingleNode(".//div[@class='g mnr-c srg']/a");
                                if (n != null)
                                {
                                    if (orgLinks < 100)
                                    {
                                        var u = n.Attributes["href"].Value;
                                        if (u.StartsWith("http") || u.StartsWith("https") || u.StartsWith("ftp")) //30-04-2020
                                        {
                                            // string links1 = HttpUtility.UrlDecode(u);
                                            if (!s.ToString().Contains("<item url=\"" + SetUrl(u) + "\"  title=\"" + SetTitle(n.InnerText) + "\" />"))//30-09-2022
                                            {
                                                s.Append("<item url=\"" + SetUrl(u) + "\" />");   //30-06-2022
                                                orgLinks++;//30-09-2022
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }

                    catch { }
                }
            }

            //}

            return s.ToString();
        }
        
        private string GetSiteLinks(HtmlNode node)
        {
            StringBuilder s = new StringBuilder();
            HtmlNode n = node.SelectSingleNode(".//h3[@class='r']/a");
            if (n == null)
                n = node.SelectSingleNode(".//a[@class='C8nzq JTuIPc']");
            if (n == null)
                n = node.SelectSingleNode(".//a[contains(@class,'BmP5tf')]");//14-01-2022 //14-09-2020 contains
            if (n == null)
                n = node.SelectSingleNode(".//a[contains(@class,'sXtWJb')]");//16-12-2020 //15-12-2020
            if (n == null)//20-09-2024
                n = node.SelectSingleNode(".//a[@class='cz3goc OcpZAb']");//20-09-2024
            if (n == null)//13-08-2024
                n = node.SelectSingleNode(".//a[@class='rTyHce jgWGIe']");//13-08-2024
            if (n == null)//19-09-2024
                n = node.SelectSingleNode(".//a[contains(@class,'cz3goc OcpZAb')]");//19-09-2024
            if (n != null)
            {
                if (orgLinks < 100)
                {
                    HtmlNode t = n.SelectSingleNode(".//div[@role='heading']");
                    if (t == null)
                        t = n.SelectSingleNode(".//span"); //15-12-2020
                    if (string.IsNullOrEmpty(t?.InnerText.Trim()))//13-07-2022
                    {
                        n = node.SelectSingleNode(".//div[@class='BmP5tf']/a");
                        t = n?.SelectSingleNode(".//div[@role='heading']");
                    }//end 13-07-2022
                    s.Append("<item url=\"" + SetUrl(n.Attributes["href"].Value) + "\" />");
                    orgLinks++;
                }
            }

            HtmlNodeCollection nds = node.SelectNodes(".//table[@class='nrg']/tr");

            if (nds != null)
            {
                s.Append("<block type=\"siteLinks\" url=\"\">");
                foreach (HtmlNode nd in nds)
                {
                    HtmlNodeCollection c = nd.SelectNodes(".//span[@class='cNifBc']/h3/a");
                    if (c == null) continue;
                    foreach (HtmlNode a in c)
                    {
                        s.Append("<item url=\"" + SetUrl(a.Attributes["href"].Value) + "\" />");
                    }
                }
                s.Append("</block>");
            }

            //28-10-2019
            nds = node.SelectNodes(".//div[@class='Lgnr0e J88qA BmP5tf']");
            if (nds == null)
                nds = node.SelectNodes(".//div[@class='pIpgAc KKgUze XO51F OAX6kd']/a");
            if (nds == null)
                nds = node.SelectNodes(".//div[@class='pIpgAc KKgUze XO51F']/a");
            if (nds == null)
                nds = node.SelectNodes(".//div[contains(@class,'MUxGbd v0nnCb lyLwlc')]/a"); //21-07-2022
            if (nds == null)
                nds = node.SelectNodes(".//div[contains(@class,'MUxGbd lyLwlc')]/a"); //01-11-2022
            if (nds == null)//11-12-2023
                nds = node.SelectNodes(".//div[contains(@class, 'Va3FIb EE3Upf lVm3ye')]/a");//11-12-2023
            if (nds == null)//08-12-2023
                nds = node.SelectNodes(".//div[@class='E8hWLe SVMeif BmP5tf']/div/a");//08-12-2023
            if (nds != null)
            {
                s.Append("<block type=\"siteLinks\" url=\"\">");
                foreach (HtmlNode nd in nds)
                {
                    //HtmlNodeCollection c = nd.SelectNodes(".//span[@class='cNifBc']/h3/a");
                    //if (c == null) continue;
                    //foreach (HtmlNode a in c)
                    //{
                    if (!nd.InnerHtml.Contains("span id"))//25-04-2020   
                        s.Append("<item url=\"" + SetUrl(nd.Attributes["href"].Value) + "\" />");
                    //    }
                }
                s.Append("</block>");
            }

            if (orgLinks < 100)//13-12-2019
            {
                nds = node.SelectNodes(".//div[@class='srg']");
                if (nds != null)
                    foreach (HtmlNode nd in nds)
                    {
                        n = nd.SelectSingleNode(".//div[@class='KJDcUb']/a");
                        if (n == null)
                            n = nd.SelectSingleNode(".//div[@class='d5oMvf KJDcUb']/a");

                        if (n != null)
                        {
                            HtmlNode t = nd.SelectSingleNode(".//div[@role='heading']");
                            s.Append("<item url=\"" + SetUrl(n.Attributes["href"].Value) + "\" />");
                            orgLinks++;
                        }
                    }

            }//13-12-2019

            return s.ToString();
        }

        private string GetTwitterCards(HtmlNode node)
        {
            StringBuilder s = new StringBuilder();
            HtmlNode hn = node.SelectSingleNode(".//div[@class='JVrfPc']/a");
            if (hn == null)
                hn = node.SelectSingleNode(".//g-link//a"); //included on 2019-06-24
            if (hn != null)
            {
                s.Append("<block type=\"twitterCards\" url=\"" + SetUrl(hn.Attributes["href"].Value) + "\">");

                HtmlNodeCollection nds = node.SelectNodes(".//div[@class='uR34qf oIY2kd JTuIPc']/a");

                if (nds == null)
                    nds = node.SelectNodes(".//div[@class='uR34qf dJMePd JTuIPc']/a");
                if (nds == null)
                    nds = node.SelectNodes(".//div[@class='uR34qf dJMePd BmP5tf']/a");
                if (nds == null)
                     nds = node.SelectNodes(".//g-card-section[contains(@class,'jDsVJf')]/a"); //27-10-2021
                if (nds == null)
                    nds = node.SelectNodes(".//div[@class='agqCtf tw-res']/g-image-section/a");//03-04-2024
                if (nds != null)
                    foreach (HtmlNode nd in nds)
                    {
                        s.Append("<item url=\"" + SetUrl(nd.Attributes["href"].Value) + "\" />");
                    }
                s.Append("</block>");
            }
            return s.ToString();
        }

        private bool IsBlock(HtmlNode node)
        {
            // changes on 05-07-2019
            HtmlNode nd = node.SelectSingleNode(".//div[@class='HnYYW']|.//g-tray-header[@role='heading']|.//div[@role='heading']");
            if (nd != null)
            {
                if (nd.InnerText.Trim() == "Top stories" || nd.InnerText.ToLower().Contains("noticias") || nd.InnerText.Trim() == "Notizie principali" || nd.InnerText.Trim() == "Interesting finds" //04-11-2020//16-09-2020
                     || nd.InnerText.ToLower().Contains("últimas noticias") || nd.InnerText.ToLower().Contains("det senaste")
                     || nd.InnerText.ToLower().StartsWith("latest") || nd.InnerText.ToLower().Contains("map")//07-08-2020  //23-06-2020 //22-06-2020
                     || nd.InnerText.ToLower().Contains("notícias")) //08-01-2021 top stories
                    if ((node.SelectSingleNode(".//div[@id='tscffb']") != null || node.SelectSingleNode(".//div[@class='KJDcUb']") == null)
                            && node.SelectSingleNode(".//div/a[contains(@class,'C8nzq BmP5tf')]|.//div/a[@class='rTyHce jgWGIe']|.//div[contains(@class,'kb0PBd cvP2Ce')]|.//div/a[contains(@class,'cz3goc')]") == null//20-09-2024
                             && node.SelectSingleNode(".//g-card[@id='tscffb']") == null) //19-05-2022//12-11-2021 //25-05-2021 //04-01-2021 video block
                        return true;
                if (node.SelectSingleNode(".//div[@class='ttfMne']|.//div[@class='N60dNb mfMhoc']|.//h2[@class='OEsCyf mfMhoc']|.//div[@class='kp-blk c2xzTb OJXvsb']|.//div[@class='WpKAof']|.//g-card/div[@class='mnr-c']|.//div[@class='FQrfLd']") != null)//04-11-2022 //23-11-2021 CB //27-10-2021 //01-09-2021 missing AC block//12-07-2021 job block //12-07-2021 carousel block //19-01-2021 missing top stories
                    if (node.SelectSingleNode(".//div[@class='WvKfwe a3spGf']|.//div[@class='V1nn0e wgFKp']|.//div[@jscontroller='rMVp5e']|.//div[@jscontroller='rQR4vd']|.//div[contains(@class,'P8ujBc')]|.//div[@class='v5yQqb jqWpsc']|.//div[contains(@class,'jqWpsc')]") != null && node.SelectSingleNode(".//div[@class='aJegcc']") == null)//09-12-2022//16-09-2022//09-09-2022//06-05-2022//21-01-2022//13-01-2022//17-12-2021 //12-10-2021
                        return false;
                    else if (node.SelectSingleNode(".//div[contains(@class,'BToiNc')]") == null)//11-07-2023
                        return true;//12-11-2021
                if (nd.InnerText == "More results" || nd.InnerText == "Top results" || nd.InnerText == "Toppresultater" || nd.InnerText == "También se buscó" //27-10-2021
                     || nd.InnerText == "Fler resultat" || nd.InnerText == "Flere resultater" || nd.InnerText == "Plus de résultats")    // 13-12-2019
                    return false;
            }
            if (node.SelectSingleNode(".//div[contains(@class, 'Z3ngN')]") != null) //22-03-2021
                return false;
            if (node.SelectSingleNode(".//video-voyager[@class='LnSx5b']|.//div[@class='lNvPub v5yQqb']" +
                "|.//div[@class='adXOEf v5yQqb']|.//div[@class='T61Aje v5yQqb']|.//div[contains(@class,'kb0PBd cvP2Ce')]") != null)//13-08-2024//23-08-2023//15-03-2023//16-12-2021
                return false;//16-12-2021
            //17-01-2020
            nd = node.SelectSingleNode(".//div[@class='Lgnr0e J88qA BmP5tf']");
            if (nd == null)
                nd = node.SelectSingleNode(".//div[@class='Lgnr0e J88qA vgnU9e BmP5tf']|.//div[@class='LEHmvf adDDi']|.//div[@class='V3FYCf']");//02-02-2024
            if (nd != null)
            {
                //30-03-2020
                if (node.SelectSingleNode(".//div[@class='g card-section jiwmWe']|.//div[contains(@class,'xpd EtOod pkphOe')]") != null)//30-08-2023
                    return false;
                if (node.SelectSingleNode(".//div[@class='MUxGbd v0nnCb lyLwlc']|.//div[@class='uVMCKf Ww4FFb vt6azd']") != null && node.SelectSingleNode(".//div[@class='LEHmvf adDDi']") == null)//29-09-2023//28-08-2023//25-04-2020
                    return false;
                return true;
            }

            // if (node.SelectSingleNode(".//div[@class='g card-section svwwZ']|.//div[@class='c6gxKe card-section']|.//div[@class='g card-section']") != null) //30-08-2021 //20-05-2021//03-11-2020
            if (node.SelectSingleNode(".//div[@class='g card-section svwwZ']|.//div[@class='c6gxKe card-section']|.//div[@class='g card-section']|.//div[@class='card-section svwwZ']") != null //10-11-2022
                 && node.SelectSingleNode(".//div[@data-tts='answers']|.//div[@class='ifM9O']") == null)//27-01-2022 answered card //07-09-2021
                return false;

            //22-11-2019
            //nd = node.SelectSingleNode(".//div[@class='g kno-result rQUFld mnr-c g-blk']|.//w-answer/div[@class='MUxGbd t51gnb lyLwlc lEBKkf']|.//div[@class='ifM9O']|.//div[@class='Q9mvUc']|.//div[contains(@class,'tkQJMd')]|.//div[contains(@class, 'RPdfze')]|.//div[contains(@class, 'Qkn3ie')]");//22-09-2023//07-07-2023 FindResultsOn block //03-07-2023//08-07-2022//16-02-2022 //01-10-2020 Answered Card selector included
            nd = node.SelectSingleNode(".//div[@class='g kno-result rQUFld mnr-c g-blk']|.//w-answer/div[@class='MUxGbd t51gnb lyLwlc lEBKkf']|.//div[@class='ifM9O']|.//div[@class='Q9mvUc']|.//div[contains(@class,'tkQJMd')]|.//div[contains(@class, 'RPdfze')]|.//div[contains(@class, 'Qkn3ie')]|.//div/span[@class='kL6Fzb']");//11-0-2024 DataSet Block or uncomment above line
            if (nd != null)
            {
                return true;
            }
            //29-11-2019
            nd = node.SelectSingleNode(".//div[@class='KJDcUb WzRKRb']|.//div[contains(@class,'P8ujBc')]" +
             "|.//div[@class='mnr-c P5XtRe']|.//div[@class='urrG9 v5yQqb jqWpsc']|.//div[@class='lNvPub cP7qLd v5yQqb']" +
             "|.//div[contains(@class,'EtOod pkphOe')]|.//div[@class='mnr-c Eiw3V']|.//div[contains(@class, 'WFyfFf')]|.//div[@class='kb0PBd cvP2Ce jGGQ5e']");//04-06-2024//14-02-2024//23-08-2023//17-05-2023//26-09-2022 //29-06-2022
            if (nd != null && nd.SelectSingleNode(".//div[contains(@class,'BNeawe')]") == null)//13-12-2023
            {
                return false;
            }
            /*nd = node.SelectSingleNode(".//g-tray-header[contains(@class,'kno-fb-ctx gsrt')]"); //23-12-2023//19-01-2023 new element top sights
            if (nd != null)
                return true;*///23-12-2023//19-01-2023
            nd = node.SelectSingleNode(".//div[@class='aJegcc']");//05-01-2023
            if (nd != null)
                return true;//05-01-2023
            //start 13-08-2019
            nd = node.SelectSingleNode(".//div[@id='sports-app']");
            if (nd != null)
            {
                return true;
            }
            nd = node.SelectSingleNode(".//nav[@class='baPFxb g kSMK2']");
            if (nd != null)
            {
                return true;
            }//end 13-08-2019

            nd = node.SelectSingleNode(".//div[@data-tts='answers']|.//div[@class='N6Sb2c i29hTd']|.//div[@class='kp-blk c2xzTb OJXvsb']|.//div[@class='answered-question']|.//div[@class='UDZeY fAgajc']"); //02-09-2020 included selector for (About)Answer Card
            if (nd != null)
            {
                return true;
            }
            nd = node.SelectSingleNode(".//div[@class='oLO3I']|.//div[contains(@class, 'tw-res')]"); //27-10-2021
            if (nd == null)
                nd = node.SelectSingleNode(".//div[@class='aZVgnb']/h2");  // 08-06-2020  
            if (nd != null)
                if (node.SelectSingleNode(".//div[@class='WvKfwe a3spGf']") != null)//28-10-2021
                    return false;//28-10-2021
                else//28-10-2021
                    return true;
            // Changes in Finance block on 25-06-2019
            nd = node.SelectSingleNode(".//div[@id='fac-tc']");
            if (nd != null)
                return true;

            nd = node.SelectSingleNode(".//div[@class='mnr-c']/g-link/a");
            if (nd != null)
                if (nd.Attributes["href"].Value.Contains("/maps/"))
                    return true;

            nd = node.SelectSingleNode(".//div[@class='srg']");
            if (nd != null)
                return false;
            nd = node.SelectSingleNode(".//g-card[@id='tscffb']");
            if (nd != null)
            {
                nd = node.SelectSingleNode(".//g-card[@class='XqIXXe']|.//div[@class='kGH5dd']");  //19-06-2020
                if (nd != null)
                {
                    nd = node.SelectSingleNode(".//div[@class='zK9jzc B3JUpd i8lZMc']"); //17-01-2020 selector changed videos block
                    if (nd == null)
                        //nd = node.SelectSingleNode(".//g-tray-header[@class='kno-fb-ctx lQckZe gsrt ieGFJe ndEm3b']"); //08-09-2020 commented //21-02-2020 included selector for videos card
                        nd = node.SelectSingleNode(".//g-tray-header[contains(@class,'kno-fb-ctx lQckZe')]");//26-02-2021 above line commented and included contains //08-09-2020 commented //21-02-2020 included selector for videos card
                    if (nd != null)
                    {
                        //if (nd.InnerText.Trim() == "Recipes" || nd.InnerText.Trim() == "Recept" || nd.InnerText.Trim() == "Ricette")// 21-11-2019
                        return true;
                    }//04-11-2019"
                    return false;
                }
                return true;
            }
            nd = node.SelectSingleNode(".//div[@jscontroller='iht5n']");
            //nd = node.SelectSingleNode(".//div[@jsmodel='uIhXXc']");
            if (nd != null)
                return false;
            //24-11-2020 updated selector for evenResults boolean
            nd = node.SelectSingleNode(".//div[@class='tsp-view']|.//div[@class='Y2NmGf']"); //25-10-2021
            if (nd != null)
            {
                return true;
            }
            //end 24-11-2020
            nd = node.SelectSingleNode(".//div[@jscontroller='UrRncd']/div/div/a[@class='C8nzq BmP5tf amp_r']");
            if (nd == null)
                nd = node.SelectSingleNode(".//div[@jscontroller='UrRncd']/div/div/a[@class='C8nzq BmP5tf']");  // 25-10-2019
            if (nd == null)
                nd = node.SelectSingleNode(".//div[@class='KJDcUb']/a[@class='C8nzq BmP5tf amp_r']");  // 25-10-2019
            if (nd == null)
                nd = node.SelectSingleNode(".//div[@class='KJDcUb']/a[@class='C8nzq BmP5tf']");  // 25-10-2019
            if (nd == null)
                nd = node.SelectSingleNode(".//div[@class='kp-blk Wnoohf OJXvsb']");
            if (nd == null)
                nd = node.SelectSingleNode(".//div[@class='mnr-c']/div"); //10-07-2021
            if (node.SelectNodes(".//div[contains(@class,'aD8dbe')]") != null)//14-09-2020 updated selector return true for empty block
            {
                return true;
            }//14-09-2020
            if (nd == null)
                nd = node.SelectSingleNode(".//div[contains(@class,'khgTR')]");  //16-09-2020 applied contains function
            if (nd != null)
            {
                if (node.SelectSingleNode(".//table[@class='std']|.//div[@class='di8g3 ChOqnd']") == null)//16-09-2021 ignore wrong url //15-09-2021 ignoring wrong classic links
                    return false;
            }
            //start 06-08-2019
            nd = node.SelectSingleNode(".//div[@class='f570C']|.//div[@class='Q9mvUc']");//07-01-2022
            if (nd != null && node.SelectSingleNode(".//div[@class='KoYIdc']") == null) //06-05-2022
            {
                return true;
            }
            //end 06-08-2019

            nd = node.SelectSingleNode(".//g-card[@class='XqIXXe']");//21-08-2019
            if (nd != null)
            {
                return true;
            }//21-08-2019"


            if (!node.HasClass("srg")) // 19-09-2019
            {
                nd = node.SelectSingleNode(".//div[@class='mnr-c xpd O9g5cc uUPGi']|.//div[@class='mnr-c OH1ZUd xpd O9g5cc uUPGi']|.//div[@class='ytwLQd']");//27-01-2022 classic links//05-06-2020 missing classic links
                if (nd != null && node.SelectSingleNode(".//div[@class='EDblX m8vZ3d']") == null || node.SelectSingleNode(".//div[@jscontroller='KP4k7d']") != null) //12-11-2021 CL  // 16-10-2019
                {
                    //if (node.SelectSingleNode(".//div[contains(@class,'BNeawe')]") != null && node.SelectSingleNode(".//div[contains(@class,'au0C1b')]") == null)//02-03-2021 included contains for classic links
                    if (node.SelectSingleNode(".//div[contains(@class,'BNeawe')]") != null && node.SelectSingleNode(".//div[contains(@class,'au0C1b')]") == null
                        && node.SelectSingleNode(".//div[contains(@class,'PUrSVb')]") == null && node.SelectSingleNode(".//div[@class='Q9mvUc']") == null
                        || node.SelectSingleNode(".//div[@class='pXvdUe']") != null && node.SelectSingleNode(".//div[@class='KoYIdc']") == null) //18-05-2022//11-05-2022//05-01-2022 carousel //25-10-2021
                        return true;
                    return false;
                }
                else if (nd != null && (node.SelectSingleNode(".//div[contains(@class,'KJDcUb')]") != null || node.SelectSingleNode(".//div[@class='U3THc']") != null)) //11-11-2021    //09-09-2021 applied contains // 22-01-2020 included selector for classic links
                {
                    return false;
                }
            }
            if (node.SelectSingleNode(".//div[@class='card-section']") != null && node.SelectSingleNode(".//div[@class='tF2Cxc']") != null) //23-04-2021
            {
                nd = node.SelectSingleNode(".//h3[@class='yuRUbf JtG40d V7Sr0']"); //22-04-2021
                if (nd != null) return false; //22-04-2021
            }
            return (!node.HasClass("srg")); // && node.SelectSingleNode(".//div[@class='ZINbbc xpd']") == null);   // block                
        }

        private bool IsOrganic(HtmlNode node)
        {
            return (node.HasClass("srg") || node.SelectSingleNode(".//div[@class='oITGTd aSYQ6c']") != null
                || node.SelectSingleNode(".//div[@class='ZINbbc xpd']") != null
                || node.SelectSingleNode(".//div[@class='mnr-c xpd O9g5cc uUPGi']") != null
                || node.SelectSingleNode(".//div[@class='mnr-c']") != null //05-06-2020
                || node.Attributes["class"]?.Value == "mnr-c xpd O9g5cc uUPGi"
                || node.SelectSingleNode(".//div[@class='vC5Ym']") != null
                || node.SelectSingleNode(".//div[@class='mnr-c OH1ZUd xpd O9g5cc uUPGi']") != null //27-01-2022 classic links
                || node.SelectSingleNode(".//div[@class='kp-blk Wnoohf OJXvsb']") != null
                || (node.SelectSingleNode(".//g-card[@class='XqIXXe']") != null && node.SelectSingleNode(".//g-card[@id='tscffb']") != null)
                || node.SelectSingleNode(".//div[@class='khgTR lWEpfd']") != null  //22-06-2020
                || node.SelectSingleNode(".//div[@class='ywTQJc']") != null //07-08-2020 
                || node.SelectSingleNode(".//div[@class='khgTR R5lVqb']") != null //26-08-2020 selector for missing classic link
                || node.SelectSingleNode(".//div[@class='V1nn0e R5lVqb']") != null //15-12-2020
                || node.SelectSingleNode(".//div[@class='card-section']") != null //15-12-2020
                || node.SelectSingleNode(".//div[contains(@class, 'Z3ngN')]") != null//22-03-2021
                || node.SelectSingleNode(".//div[@class='g card-section']") != null) && node.SelectSingleNode(".//div[@class='BNeawe']") == null //08-10-2021//30-08-2021
                || node.SelectSingleNode(".//video-voyager[@class='LnSx5b']|.//div[@class='lNvPub v5yQqb']") != null//15-03-2023//16-12-2021
                || (node.SelectSingleNode(".//div[contains(@class,'EtOod pkphOe')]") != null && node.SelectSingleNode(".//div[@class='TOQyFc U48fD']") == null)//04-07-2022 //29-06-2022
                || node.SelectSingleNode(".//div[@class='mnr-c xpd EtOod pkphOe']") != null //10-06-2022
                || node.SelectSingleNode(".//div[contains(@class,'P8ujBc')]") != null //22-02-2022 //01-02-2022
                || node.SelectSingleNode(".//div[@class='mnr-c P5XtRe']") != null //25-03-2022
                || node.SelectSingleNode(".//div[@class='urrG9 v5yQqb jqWpsc']|.//div[@class='lNvPub cP7qLd v5yQqb']|.//div[@class='adXOEf v5yQqb']" +
                "|.//div[@class='T61Aje v5yQqb']|.//div[contains(@class, 'WFyfFf')]") != null || node.SelectSingleNode(".//div[contains(@class,'kb0PBd cvP2Ce')]") != null;//13-08-2024//04-06-2024//14-02-2024//23-08-2023 != null//17-05-2023 //31-05-2022
        }

        internal object GetTop100GoogleUKMobileImages_PageURLs(string kw, string v1, string v2, string v3, string v4, string v5)
        {
            throw new NotImplementedException();
        }

        internal object GetTop100GoogleUKMobileImages_ImageURLs(string kw, string v1, string v2, string v3, string v4, string v5)
        {
            throw new NotImplementedException();
        }
        
        private string GetRedirectedUrl(string url)
        {
            try  //28-09-2020  try catch.
            {
                //21-11-2019
                url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");
                if (string.IsNullOrEmpty(url.Trim())) return string.Empty;

                //24-09-2020 changed LastIndexOf to IndexOf
                if (url.IndexOf("https://") == 0 || url.IndexOf("https://") >= 0)//01-10-2020
                    url = url.Remove(0, url.IndexOf("https://"));
                else if (url.IndexOf("http://") == 0 || url.IndexOf("http://") >= 0) //18-11-2020//14-10-2020 included indexof for http
                    url = url.Remove(0, url.IndexOf("http://"));


                //end 24-09-2020
                Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
                if (!rx.Match(url).Success && !url.Contains("/aclk?") && !url.Contains("/search?"))//related searches //21-1-2022 "/search" remove from &&
                                                                                                   //if (!url.StartsWith("/")) //11-09-2021 ignore url start with "/"
                    if (!url.Contains("://")) // 30-04-2020
                           url = "http://" + url;

                if (url.StartsWith("http:////") || url.StartsWith("https:////")) //18-09-2020 condition applied if appears http:////
                    url = url.Replace("////", "//"); //18-09-2020

                if (url.Contains("&amp;grqid="))
                    url = url.Remove(url.IndexOf("&amp;grqid="));

                //  13-12-2019
                if (url.Contains("&grqid="))
                    url = url.Remove(url.IndexOf("&grqid="));
                //23-09-2020
                if (url.Contains("&amp;gclid="))
                    url = url.Remove(url.IndexOf("&amp;gclid="));
                if (url.Contains("&gclid="))
                    url = url.Remove(url.IndexOf("&gclid="));
                //end 23-09-2020

                if (url.Contains("\0"))
                    url = url.Replace("\0", "%00");

                if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && (!url.Contains("/aclk?") && !url.Contains("search?num=100")))  // 30-04-2020 //18-02-2020 and 24-02-2020 included condition
                    return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;

        }
        /// <summary>
        /// peoplealsoask block url and youtube urls cleaning method
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string SetYTUrl(string url, char[] yt)//12-03-202
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;
            if (url.Contains(@"\x3d"))
                url = url.Replace(@"\x3d", "="); //10-03-2022
            if (url.Contains("youtube.com"))
            {
                int n = url.IndexOfAny(yt);
                if (n > 0)
                    url = url.Replace(url.Remove(0, n), "");//11-03-2022
            }
            if (url.Contains(@"\u003d"))
                url = url.Replace(@"\u003d", "="); //22-03-2022 \u003d

            if (url.Contains(@"\u0026"))
                url = url.Replace(@"\u0026", "&"); //22-03-2022

            if (url.Contains(@"\x26"))
                url = url.Replace(@"\x26amp;", "&"); //10-03-2022\x26#39;

            if (url.Contains(@"\x26#39;"))
                url = url.Replace(@"\x26#39;", "'"); //17-03-2022

            if (url.Contains(@"\x27"))
                url = url.Replace(@"\x27", "'"); //11-03-2022

            if (url.Contains(@"\x3cb\x3e"))
                url = url.Replace(@"\x3cb\x3e", ""); //15-03-2022 for text
            if (url.Contains(@"\x3cbr\x3e"))
                url = url.Replace(@"\x3cbr\x3e", ""); //21-03-2022 for text

            if (url.Contains(@"\x3c/b\x3e"))
                url = url.Replace(@"\x3c/b\x3e", ""); //17-03-2022 for text
            if (url.Contains(@"\x26quot;"))
                url = url.Replace(@"\x26quot;", "\"");//21-03-2022
            if (url.Contains(@"\u201c"))
                url = url.Replace(@"\u201c", "“"); //22-03-2022
            if (url.Contains(@"\u201d"))
                url = url.Replace(@"\u201d", "”"); //22-03-2022

            return WebUtility.HtmlEncode(WebUtility.HtmlDecode(url));
        }//12-03-2022 end

        // There are chances method was used for title contains in case any issues in xml applied decode/encode.
        public string SetTitle(string unicodestring)
        {
            //return WebUtility.HtmlEncode(WebUtility.HtmlDecode(unicodestring));
            return WebUtility.HtmlEncode(WebUtility.HtmlDecode(unicodestring)).Replace("\\x27", "'").Replace("\\\\u0026", "&amp;").Replace("\\\\\\x22", "&quot;").Replace("\\u2013", "–"); //17-03-2022

        }

        public string SetUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || url.StartsWith("#")) return string.Empty;//05-04-2024 //24-08-2020
            try  //28-09-2020  try catch.
            {
                //21-11-2019
                url = GetRedirectedUrl(WebUtility.HtmlDecode(url).Trim());
                //if (url.ToLower().Contains("%2f") || url.ToLower().Contains("%2e"))
                if (url.Contains("%")) //18-09-2020
                    url = GetRedirectedUrl(WebUtility.UrlDecode(WebUtility.HtmlDecode(url)).Trim());
                return WebUtility.HtmlEncode(SanitizeXmlString(url).Replace("\x00", "%00")).Replace("\\\\u003d", "=").Replace('\u0002', ' ').Replace('\u0018', ' ').Replace('\f', ' ').Trim(); //23-09-2020 applied method to URL//22-04-2020

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //27-08-2020
        private string GetRedirectedUrl_TextAds(string url)
        {
            if (string.IsNullOrEmpty(url) || url.StartsWith("#")) return string.Empty; //08-02-2022
            try  //28-09-2020  try catch.
            {
                url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");
                if (string.IsNullOrEmpty(url.Trim())) return string.Empty;

                Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
                if (!rx.Match(url).Success && !url.Contains("/aclk?") && !url.Contains("/localservices/")) //19-08-2022
                    if (!url.Contains("://"))
                        url = "http://" + url;

                if (url.StartsWith("http:////") || url.StartsWith("https:////")) //18-09-2020 condition applied if appears http:////
                    url = url.Replace("////", "//"); //18-09-2020

                if (url.Contains("adurl="))//28-03-2024
                {
                    int indx = url.LastIndexOf("http://");
                    if (indx < 0)
                    {
                        indx = url.LastIndexOf("https://");
                    }
                    url = url.Remove(0, indx);
                }//28-03-2024

                if (url.Contains("&amp;grqid="))
                    url = url.Remove(url.IndexOf("&amp;grqid="));

                if (url.Contains("&grqid="))
                    url = url.Remove(url.IndexOf("&grqid="));

                //23-09-2020
                if (url.Contains("&amp;gclid="))
                    url = url.Remove(url.IndexOf("&amp;gclid="));
                if (url.Contains("&gclid="))
                    url = url.Remove(url.IndexOf("&gclid="));
                //end 23-09-2020

                if (url.Contains("\0"))
                    url = url.Replace("\0", "%00");

                if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && (!url.StartsWith("/aclk?") && !url.Contains("search?num=100")))
                {
                    url = WebUtility.UrlDecode(WebUtility.HtmlDecode(url).Trim()); //10-01-2020
                    return WebUtility.HtmlEncode(SanitizeXmlString(url).Replace("\x00", "%00")).Replace("\\\\u003d", "=").Replace('\u0002', ' ').Replace('\u0018', ' ').Replace('\f', ' ').Trim();//23-09-2020 applied method to URL
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }
        //23-09-2020 included method to find and fix hexdecimal chars
        public string SanitizeXmlString(string xml)
        {
            if (xml == null)
            {
                throw new ArgumentNullException("xml");
            }

            StringBuilder buffer = new StringBuilder(xml.Length);

            foreach (char c in xml)
            {
                if (XmlSanitizingStream.IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }
            return buffer.ToString();
        }
    }


    public class XmlSanitizingStream : StreamReader
    {
        public XmlSanitizingStream(Stream streamToSanitize)
        : base(streamToSanitize, true)
        { }

        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        public static bool IsLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '\t' == 9   */          ||
                 character == 0xA /* == '\n' == 10  */          ||
                 character == 0xD /* == '\r' == 13  */          ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character < 0xFFFD) ||       //02-11-2020 changed <= 0xFFFD to < 0xFFFD
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }
        private const int EOF = -1;

        public override int Read()
        {
            // Read each char, skipping ones XML has prohibited

            int nextCharacter;

            do
            {
                // Read a character

                if ((nextCharacter = base.Read()) == EOF)
                {
                    // If the char denotes end of file, stop
                    break;
                }
            }

            // Skip char if it's illegal, and try the next

            while (!XmlSanitizingStream.
                    IsLegalXmlChar(nextCharacter));

            return nextCharacter;
        }

        public override int Peek()
        {
            // Return next legal XML char w/o reading it 

            int nextCharacter;

            do
            {
                // See what the next character is 
                nextCharacter = base.Peek();
            }
            while
            (
                // If it's illegal, skip over 
                // and try the next.

                !XmlSanitizingStream.IsLegalXmlChar(nextCharacter) &&
                (nextCharacter = base.Read()) != EOF
            );

            return nextCharacter;

        }
    }
}