using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace GoogleFirstPage.GoogleClassicLinks
{
    public class Desktop
    {
        int orgLinks;
        string html;
        string seid = string.Empty;//23-06-2023
        public string ProcessDocument(string seid, string keyword, HtmlDocument doc, out int count)
        {
            this.seid = seid;//23-06-2023
            count = 0;
            if (doc == null) throw new Exception("No source found.");
            HtmlNode htmlNode = doc.DocumentNode.SelectSingleNode("//table[@id='mn']");
            if (htmlNode != null)
            {
                throw new Exception("Old page found.");
            }
            orgLinks = 0;
            string ndText = "";
            try //28-09-2020  try catch.
            {
                html = doc.DocumentNode.OuterHtml;
                StringBuilder sb = new StringBuilder();
                sb.Append("<searchResult searchEngine=\"" + seid + "\" keyword=\"" + WebUtility.HtmlEncode(keyword) + "\" date=\"" + DateTime.Today.ToString("yyyy-MM-dd") + "\" >");
                sb.Append("<section col=\"main\">");
                string topStuff = GetTopStuff(doc);
                ndText = topStuff;
                sb.Append(topStuff);
                //01-11-2023
                HtmlNode rso = doc.DocumentNode.SelectSingleNode("//div[@id='rso']");
                HtmlNodeCollection nodeCol = rso.SelectNodes(".//div[contains(@class,'MjjYud')]");//09-12-2024
                if (nodeCol == null || (nodeCol.Count <= 1))
                    nodeCol = doc.DocumentNode.SelectNodes(".//div[contains(@class,'TzHB6b cLjAic')]|.//div[contains(@id, 'arc-srp')]/div/div[@class='MjjYud']|.//div[contains(@id, 'arc-srp')]/div/div/div[@class='MjjYud']");
                if (nodeCol == null || nodeCol.Count <= 4)
                    nodeCol = rso.SelectNodes(".//div[contains(@class,'TzHB6b cLjAic')]");//02-11-2023
                //if (nodeCol == null || nodeCol.Count <= 3)//02-11-2023
                //    nodeCol = rso.SelectNodes(".//div[@class='WvKfwe a3spGf']/div|.//div[@class='aviV4d']");//01-08-2024//01-02-2024//02-11-2023
                if (nodeCol == null || (nodeCol.Count >= 1 && nodeCol.Count <= 5))//09-02-2024
                {//08-02-2024
                    if (rso.SelectNodes("//div[contains(@id,'kp-wp-tab-')]") != null)
                        nodeCol = rso.SelectNodes(".//div[contains(@class,'TzHB6b cLjAic')]|.//div[contains(@class,'TzHB6b j8lBAb p7kDMc cLjAic')]|.//div[contains(@class,'g Ww4FFb')]|.//div[@class='g PmEWq']|.//div[@class='Wt5Tfe']|.//div[@class='g']/div[@class='BYM4Nd']|.//div[@class='Lv2Cle']|.//g-section-with-header[@class='yG4QQe TBC9ub']|.//div[@class='uVMCKf']");//09-10-2024
                    else
                        nodeCol = rso.SelectNodes(".//div[contains(@class,'WvKfwe')]/div|.//div[@class='UDZeY OTFaAf']/div|.//div[@class='MjjYud']|.//div[contains(@class,'g Ww4FFb')]");
                }//08-02-2024
                if (nodeCol != null)  //11-08-2022 //end 01-11-2023
                    foreach (HtmlNode node in nodeCol)
                    {
                        if (node.HasClass("kp-wholepage"))
                        {
                            continue;
                        }
                        if (node.HasClass("TzHB6b") && node.SelectSingleNode(".//div[contains(@class,'g Ww4FFb')]|.//div[@class='g PmEWq']|.//div[@class='Wt5Tfe']|.//div[@class='g']/div[@class='BYM4Nd']|.//div[@class='Lv2Cle']|.//g-section-with-header[@class='yG4QQe TBC9ub']|.//div[@class='uVMCKf']") != null)//09-10-2024
                        {
                            continue;
                        }//09-10-2024
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
                        catch { }
                    }

                // 23-03-2020
                if (string.IsNullOrEmpty(ndText) || orgLinks == 0)//08-04-2020
                {
                    nodeCol = doc.DocumentNode.SelectNodes("//div[@class='xVtsMb i6u2Cc']|//div[@class='xVtsMb']/div/div");//swapped 08-04-2020
                    if (nodeCol == null)
                        nodeCol = doc.DocumentNode.SelectNodes("//div[@class='vC5Ym DhKAUb']/div");  // 03-04-2020
                    if (nodeCol == null)
                        nodeCol = doc.DocumentNode.SelectNodes(".//div[contains(@class,'WvKfwe')]/div|.//div[contains(@class,'WvKfwe')]/g-section-with-header|.//div[@class='UDZeY OTFaAf']");//09-12-2020
                    if (nodeCol != null) //11-08-2022
                        foreach (HtmlNode node in nodeCol)
                        {
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
                            catch { }
                        }
                }
                // 23-03-2020

                //if (nodeCol == null) throw new Exception("No block found.");
                if (nodeCol == null & string.IsNullOrEmpty(ndText)) throw new Exception("No block found."); // 03-06-2020
                //if (nodeCol == null) return string.Empty;                      
                //if (nodeCol == null) goto BOTTOMSTUFF; 


                //if (orgLinks < count)
                //    return string.Empty;

                //BOTTOMSTUFF:
                string bottomStuff = "";
                sb.Append(bottomStuff);
                sb.Append("</section>");

                sb.Append("<section col=\"right\">");
                string rightStuff = "";
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

        private string GetTopStuff(HtmlDocument doc)
        {
            StringBuilder s = new StringBuilder();

            // for carousel
            HtmlNode crNode = doc.DocumentNode.SelectSingleNode("//div[@id='extabar']|//div[@id='appbar']");  //29-06-2020
            if (crNode != null)
            {
                HtmlNode carousel = crNode.SelectSingleNode(".//div[@id='kx']|.//g-scrolling-carousel|.//div[@jscontroller='envtD']");//07-12-2023 start
                if (carousel == null)
                    carousel = doc.DocumentNode.SelectSingleNode("//div[@id='Odp5De']")?.SelectSingleNode(".//g-scrolling-carousel");
                if (carousel != null)//07-12-2023 end
                {

                }
            }

            // product listed ads
            HtmlNode pla = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'cu-container')]"); //25-09-2020 included contains for existing selector
            if (pla != null)
            {
                HtmlNode h3 = pla.SelectSingleNode(".//div[@class='dxR8gf']/h3");
                if (h3 != null)
                {
                    if (pla.SelectSingleNode(".//div[contains(@class, 'commercial-unit-desktop-top')]") != null
                        || pla.SelectSingleNode(".//div[contains(@class, 'top-pla-group-inner')]") != null)  // 13-02-2020 
                    {
                        HtmlNodeCollection cl = pla.SelectNodes(".//a[@class='plantl pla-unit-title-link']");
                        if (cl != null)
                        {
                            foreach (HtmlNode nd in cl)
                            {

                            }
                        }

                    }
                }
            }

            // text ads
            HtmlNode colt = doc.DocumentNode.SelectSingleNode("//div[@id='tvcap']");  //20-01-2020
            if (colt != null)
            {
                HtmlNode pNode = colt.SelectSingleNode(".//div[@jscontroller='vWOOIe']|.//div[@id='tads']");//02-10-2023//30-06-2023
                if (pNode != null && pla == null)//26-10-2023
                {
                    HtmlNodeCollection pNodes = pNode.SelectNodes(".//div[@class='ZPze1e']/a|.//g-inner-card[contains(@class,'B5kg8b')]/a");//02-10-2023
                    if (pNodes != null)
                    {
                        foreach (var nd in pNodes)
                        {
                            var url = nd.Attributes["href"].Value;
                            url = GetRedirectedUrl(url);
                            var title = nd.SelectSingleNode(".//div[@class='e7SMre']|.//div[@class='gCv54b']")?.InnerText;//02-10-2023

                        }
                    }

                }//30-06-2023
                HtmlNodeCollection col = colt.SelectNodes(".//div[@id='tads']/ol/li|.//div[@id='tads']/div/ol/li|.//div[@id='tadsb']/ol/li|.//div[@id='tads']/div[@class='uEierd']|.//div[@id='tads']/div/div[@class='uEierd']");//28-03-2022 "/div/"included //21-09-2020 adwords selector//20-01-2020 //08-04-2020

                if (col != null) //return s.ToString();  //20-01-2020
                {
                    foreach (HtmlNode nd in col)
                    {
                        //HtmlNode n = nd.SelectSingleNode(".//h3/a[2]");
                        HtmlNode n = nd.SelectSingleNode(".//div[@class='ad_cclk']/a[2]|.//div[contains(@class,'d5oMvf')]/a" +
                            "|.//div[contains(@class,'v5yQqb')]/a|.//div[@class='pPjAYc']/text-ad-link/a");//09-02-2024//12-11-2021 //29-08-2020 included contains fucntions //23-07-2020 included missing item urls selectors
                        if (n != null)
                        {

                        }
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
            if ((node.HasClass("_NId") || node.HasClass("bkWMgd") || node.HasClass("srg") //10-10-2022
                 || node.HasClass("g") || node.SelectNodes(".//div[@class='g']") != null
                 || node.SelectNodes(".//div[@class='g GjRtuc']|.//div[@class='g zXItKe']|.//div[contains(@class,'g PmEWq')]") != null//14-09-2023//23-08-2023
                 || node.SelectNodes(".//div[contains(@class,'g card-section')]|.//div[@class='N3nEGc']") != null //14-09-2023
                 || node.SelectNodes(".//div[@class='g tF2Cxc']|.//div[contains(@class,'g dFd2Tb')]|.//div[contains(@class,'g Ww4FFb')]|.//div[@class='g ZYT4Gf']") != null
                || node.SelectNodes(".//div[@class='d3zsgb']/div[@class='yuRUbf']|.//div[@class='tF2Cxc']/div[@class='yuRUbf']|.//div[contains(@class,'g Ww4FFb')]|.//div[@class='g eejeod up9jud']|.//div[contains(@class,'Ww4FFb vt6azd')]") != null)//25-04-2024//25-01-2023
                   && (node.SelectSingleNode(".//div[@class='MjjYud']") != null || node.SelectSingleNode(".//div[@id='rhs']") == null))//25-01-2023//10-01-2023//12-10-2022//end of 10-10-2022
            {
                HtmlNodeCollection nds = node.SelectNodes(".//div[contains(@class,'g tF2Cxc')]|.//div[contains(@class,'g dFd2Tb')]|.//div[contains(@class,'g Ww4FFb')]|.//div[contains(@class,'g wF4fFd')]|.//div[@class='g zXItKe']|.//div[@class='BYM4Nd']|.//div[@class='AuVD cUnQKe']|.//div[@class='cUnQKe']|.//div[@class='cUnQKe vt6azd']|.//div[@class='uVMCKf']|.//g-card[@class='tkfIqc g']|.//div[contains(@class, 'g PmEWq')]|.//g-scrolling-carousel[@class='arDHIe']|.//g-section-with-header[@class='yG4QQe TBC9ub']");//29-09-2023
                if (nds == null && (node.Attributes["class"]?.Value == "g tF2Cxc" || node.Attributes["class"]?.Value == "g Ww4FFb vt6azd tF2Cxc asEBEc") || node.Attributes["class"]?.Value == "g PmEWq")//08-02-2024//17-11-2022//20-04-2022
                    nds = node.SelectNodes(".");//20-04-2022 
                if (nds == null)
                    nds = node.SelectNodes(".//div[contains(@class,'tF2Cxc')]");
                if (nds == null)
                    nds = node.SelectNodes(".//div[@class='yuRUbf']");//11-10-2022 //31-05-2021
                if (nds == null) //25-01-2022
                    nds = node.SelectNodes(".//div[@class='g']|.//div[@class='HD8Pae luh4tb cUezCb xpd O9g5cc uUPGi']|.//div[contains(@class,'g card-section')]");//25-01-2022
                if (nds == null)
                    nds = node.SelectNodes(".//div[@class='rc']");
                if (nds == null)
                    nds = node.SelectNodes(".//div[@class='gG0TJc']");  //29-05-2020
                if (nds == null)
                    nds = node.SelectNodes(".//div[contains(@class,'dFd2Tb')]|.//div[@class='g ZYT4Gf']");//07-04-2022 //24-08-2021 video block
                if (node.SelectNodes(".//div/div[@class='g jNVrwc Y4pkMc']") != null) //24-08-2021 collecting sub classic links
                    nds = node.SelectNodes(".//div/div[@class='g jNVrwc Y4pkMc']"); //24-08-2021 collecting sub classic links
                if (nds == null)
                    nds = node.SelectNodes(".//div[@class='g eejeod up9jud']");//12-10-2022
                if (nds != null)
                    foreach (HtmlNode nd in nds)
                    {
                        // 02-06-2020
                        if (nd.SelectSingleNode(".//table[@class='nrgt']") != null || node.SelectSingleNode(".//table[@class='jmjoTe']") != null) //22-08-2020 included dor site links
                        {
                            s.Append(GetSiteLinks(nd));
                            continue;
                        }

                        HtmlNode title = null;  // 18-11-2019
                        HtmlNode n = nd.SelectSingleNode(".//h3[@class='r']/a");
                        if (n == null)
                            n = nd.SelectSingleNode(".//div[@class='r']/a");
                        if (n == null)
                            n = nd.SelectSingleNode(".//div[@class='r']/div/a"); //28-05-2020
                        if (n == null)
                            n = nd.SelectSingleNode(".//div[@class='yuRUbf']/a"); //02-10-2020
                        if (n == null)
                            n = nd.SelectSingleNode(".//div[@class='yuRUbf']/div/a");//28-07-2023
                        if (n == null)
                            n = nd.SelectSingleNode(".//div[@class='ARVUmc']/div/span/a");//07-09-2023
                        if (n == null)
                            n = nd.SelectSingleNode(".//div[@class='nhaZ2c']/div/span/a");//08-09-2023
                        if (n != null)
                            title = n.SelectSingleNode(".//h3");

                        HtmlNode img = nd.SelectSingleNode(".//img");
                        if (img != null)
                        {
                            if ((Regex.IsMatch(nd.OuterHtml, "id=\"vidthumb\\d*\"") && (nd.SelectSingleNode(".//div[@class='ij69rd UHe5G']") != null || nd.SelectSingleNode(".//div[@class='ij69rd TUOsUe UHe5G']") != null)) || nd.SelectSingleNode(".//div[contains(@class,'U1TUId')]|.//div[@class='J1mWY']|.//div[contains(@class,'c8rnLc flgn0c')]|.//div[@class='Ylm8Fc']|.//div[@class='Ttm4Le']|.//div[@class='TdIFfc']") != null)//02-09-2023//23-08-2023//10-06-2023//07-06-2023//02-05-2023//07-04-2022 //18-10-2021 video block selector
                            {
                                //24-08-2021 video item urls
                                var urls = string.Empty;
                                if (n != null)
                                    urls = n.Attributes["href"].Value;
                                else
                                {
                                    var a = nd.SelectSingleNode(".//div[@class='ct3b9e']/a|.//div[@class='IAZbGe']/a|.//div[@class='DhN8Cf']/a" +
                                        "|.//div[@class='E74pWd']/a|.//div[@class='DhN8Cf']/div/a|.//div[@class='IAZbGe']/div/a" +
                                        "|.//div[@class='nhaZ2c']/div/a|.//div[@class='ARVUmc']/div/a|.//div[@class='nhaZ2c']/div/span/a" +
                                        "|.//div[@class='xe8e1b']/div/div/span/a");//05-12-2024//07-09-2023//23-08-2023//28-07-2023//18-04-2023//11-02-2023 //07-04-2022 urls = a.Attributes["href"].Value;
                                    if (a != null)//14-09-2023
                                    {
                                        urls = a.Attributes["href"].Value;
                                        title = a.SelectSingleNode(".//h3");
                                    }//14-09-2023

                                } //24-08-2021 video block item urls
                                if (urls.StartsWith("http") || urls.StartsWith("https") || urls.StartsWith("ftp")) //30-04-2020
                                {
                                    // video block.
                                }
                            }

                            //Changes - Included else if condition which was missing.....
                            else if (orgLinks < 100)
                            {
                                if (n == null)
                                    n = nd.SelectSingleNode(".//div[@class='yuRUbf']/a|.//div[@class='yuRUbf']/div/div[@class='E74pWd']/a|.//div[@class='yuRUbf']/div/span/a|.//div[@class='IAZbGe']/a|.//div[@class='IAZbGe']/div/a");//07-09-2023//28-07-2023//18-04-2023
                                if (n == null)//18-04-2023
                                    n = nd.SelectSingleNode(".//div[@class='E74pWd']/a|.//g-link[@class='WYrxZc']/a");//05-12-2023//18-04-2023//18-10-2022 //04-09-2020 included selector for classic links
                                if (n == null && nd.Attributes["class"]?.Value == "yuRUbf")//17-11-2022
                                    n = nd.SelectSingleNode(".//a");//17-11-2022
                                if (n != null)
                                    title = n.SelectSingleNode(".//h3"); //04-09-2020 included selector for classic links
                                var urls = n.Attributes["href"].Value;
                                urls = SetUrl(urls);    // 20-12-2019
                                if (urls.StartsWith("http") || urls.StartsWith("https") || urls.StartsWith("ftp")) //30-04-2020
                                {
                                    //end 25-09-2020
                                    // string links1= HttpUtility.UrlDecode(urls);
                                    s.Append("<item url=\"" + SetUrl(urls) + "\" />");
                                    orgLinks++;
                                }
                            }
                        }
                        else
                        {
                            if (orgLinks < 100)
                            {
                                if (n == null)
                                    n = nd.SelectSingleNode(".//g-link/a");
                                if (n == null)
                                    n = nd.SelectSingleNode(".//div[@class='r']/div/a");  // 28-05-2020 twitter class link included selector
                                if (n == null)
                                    n = nd.SelectSingleNode(".//h3[@class='r dO0Ag']/a");  //29-05-2020
                                if (n == null)
                                    n = nd.SelectSingleNode(".//div[@class='yuRUbf']/a"); //03-09-2020  included selector for classic links                         
                                if (n == null)
                                    n = nd.SelectSingleNode(".//div[@class='yuRUbf']/div/a");//28-07-2023
                                if (n == null)
                                    n = nd.SelectSingleNode(".//div[@class='E74pWd']/a");//18-04-2023
                                if (n == null)
                                    n = nd.SelectSingleNode(".//a");//31-05-2021
                                if (n != null)
                                    title = n.SelectSingleNode(".//h3"); //03-09-2020 included selector for classic links
                                var urls = n.Attributes["href"].Value;
                                string t;
                                if (title != null)
                                    t = title.InnerText;
                                else
                                    t = n.InnerText;

                                urls = SetUrl(urls);    // 20-12-2019
                                if (urls.StartsWith("http") || urls.StartsWith("https") || urls.StartsWith("ftp")) //30-04-2020
                                {
                                    // string links1= HttpUtility.UrlDecode(urls);
                                    s.Append("<item url=\"" + SetUrl(urls) + "\" />");
                                    orgLinks++;
                                }
                            }
                        }
                    }
            }
            else
            {
                HtmlNodeCollection nc = null; //10-10-2022
                if (node.SelectSingleNode(".//div[@id='rhs']") != null || node.Attributes["id"]?.Value == "rhs")
                {
                    nc = node.SelectNodes(".//div[@class='g']|.//div[contains(@class,'g Ww4FFb')]|.//div[@class='g eejeod up9jud']|.//div[contains(@class,'g dFd2Tb')]" +
                        "|.//div[contains(@class,'g ZYT4Gf')]|.//div[@class='g PmEWq']");//21-11-2023//07-08-2023//26-04-2023//12-10-2022
                    if (nc == null) return string.Empty;
                }
                foreach (HtmlNode n in nc)
                {
                    if (n.SelectSingleNode(".//table[@class='nrgt']") != null || node.SelectSingleNode(".//table[@class='jmjoTe']") != null)//14-12-2022
                    {
                        if (n.SelectSingleNode(".//h2") == null) continue;
                        s.Append(GetSiteLinks(n));
                        continue;
                    }//14-12-2022
                    //12-10-2022
                    if ((Regex.IsMatch(n.OuterHtml, "id=\"vidthumb\\d*\"") && (n.SelectSingleNode(".//div[@class='ij69rd UHe5G']") != null || n.SelectSingleNode(".//div[@class='ij69rd TUOsUe UHe5G']") != null)) || n.SelectSingleNode(".//div[contains(@class,'U1TUId')]|.//div[@class='J1mWY']|.//div[contains(@class,'c8rnLc flgn0c')]") != null)//21-11-2023//30-05-2023
                    {
                        var a = n.SelectSingleNode(".//div[@class='ct3b9e']/a|.//div[@class='IAZbGe']/a|.//div[@class='DhN8Cf']/a|.//div[@class='E74pWd']/a" +
                            "|.//div[@class='IAZbGe']/div/a|.//div[@class='DhN8Cf']/div/a|.//div[@class='nhaZ2c']/div/span/a|.//div[@class='xe8e1b']/div/div/span/a");//09-12-2024//21-11-2023//28-07-2023//18-04-2023//13-02-2023
                        var url = a.Attributes["href"].Value;
                        var title = a.SelectSingleNode(".//h3");
                        if (url.StartsWith("http") || url.StartsWith("https") || url.StartsWith("ftp"))
                        {
                            continue;
                        }
                    }//end 12-10-2022
                    HtmlNodeCollection col = n.SelectNodes(".//h3[@class='r']/a");
                    if (col == null)
                        col = n.SelectNodes(".//div[@class='r']/a");
                    if (col == null)
                        col = n.SelectNodes(".//h3[@class='r dO0Ag']/a");
                    if (col == null)
                        col = n.SelectNodes(".//div[@class='zTpPx']/g-link/a");
                    if (col == null)
                        col = n.SelectNodes(".//div[@class='DOqJne']/g-link/a|.//div[@class='M42dy']/g-link/a");
                    if (col == null)
                        col = n.SelectNodes(".//div[@class='yuRUbf']/a|.//div[@class='yuRUbf']/div/div[@class='E74pWd']/a|.//div[@class='yuRUbf']/div/a|.//div[@class='yuRUbf']/div/span/a");//07-09-2023//28-07-2023//18-04-2023 //08-10-2021 for missing classic links
                    foreach (HtmlNode nd in col)
                    {
                        string u = nd.Attributes["href"].Value.Replace("/url?q=", "").Replace("&amp;", "&").Replace("&", "&#38;");
                        //u = nd.Attributes["href"].Value.StartsWith("http").ToString();
                        if (u.Contains("&sa="))
                            u = u.Substring(0, u.IndexOf("&sa="));
                        if (orgLinks < 100)
                        {
                            if (u.StartsWith("http") || u.StartsWith("https") || u.StartsWith("ftp"))
                            {
                                var title = nd.SelectSingleNode(".//h3")?.InnerText ?? nd.InnerText;
                                s.Append("<item url=\"" + SetUrl(u) + "\" />");
                                orgLinks++;
                            }
                        }
                    }
                }//end of 10-10-2022
            }
            return s.ToString();
        }

        //20-11-2020

        private string GetSiteLinks(HtmlNode node)
        {
            StringBuilder s = new StringBuilder();
            HtmlNode n = node.SelectSingleNode(".//div[@class='r']/a");
            if (n == null)
                n = node.SelectSingleNode(".//div[@class='yuRUbf']/a|.//div[@class='yuRUbf']/div/a|.//div[@class='yuRUbf']/div/span/a"); //08-09-2023//07-08-2023 //03-09-2020 included classic link selector
            if (n != null)
            {
                if (orgLinks < 100)
                {
                    HtmlNode t = n.SelectSingleNode(".//h3");
                    s.Append("<item url=\"" + SetUrl(n.Attributes["href"].Value) + "\" />");
                    orgLinks++;
                }
            }
            return s.ToString();
        }

        private string GetTwitterCards(HtmlNode node)
        {
            StringBuilder s = new StringBuilder();
            HtmlNode hn = node.SelectSingleNode(".//h3[@class='r']/div/g-link/a");
            if (hn == null)
                hn = node.SelectSingleNode(".//h3/g-link/a");
            if (hn == null)
                hn = node.SelectSingleNode(".//g-link/a"); //included on 2019-06-24
            if (hn != null)
            {
                string url = hn.Attributes["href"].Value; //10-01-2022
                if (url.Contains("/search?num=100")) url = string.Empty; //10-01-2022
                s.Append("<block type=\"twitterCards\" url=\"" + SetUrl(url) + "\">"); //10-01-2022
                HtmlNodeCollection nds = node.SelectNodes(".//g-inner-card/div/div[2]/div/g-link/a");
                if (nds == null)
                    nds = node.SelectNodes(".//g-inner-card/div/a"); //21-10-2021 twitter item urls
                if (nds == null)
                    nds = node.SelectNodes(".//g-inner-card/div/div[1]/a");
                if (nds == null)
                    nds = node.SelectNodes(".//g-inner-card/div/div/div[1]/a[1]");  //02-06-2020
                if (nds == null)
                    nds = node.SelectNodes(".//a[@class='h4kbcd']"); //27-05-2020 twitterCard item URLs included selector
                if (nds != null)
                    foreach (HtmlNode nd in nds)
                    {
                        s.Append("<item url=\"" + SetUrl(nd.Attributes["href"].Value) + "\" title=\"\" />");
                    }
                s.Append("</block>");
            }
            return s.ToString();
        }

        private bool IsBlock(HtmlNode node)
        {
            bool bVal = (node.SelectSingleNode(".//h3[@class='zQlLed']") != null  // top stories       
                || node.SelectSingleNode(".//div[@class='wXlZre B03h3d V14nKc ptcLIOszQJu__wholepage-card wp-msss']") != null//topstories 08-04-2020
                || node.SelectSingleNode(".//div[contains(@class, 'e2BEnf U7izfe')]") != null //28-07-2021 images selectors
                || node.SelectSingleNode(".//table[@class='nrgt']") != null || node.SelectSingleNode(".//table[@class='jmjoTe']") != null      // site links  22-08-2020 included block type selector
                || node.SelectSingleNode(".//img[@id='lu_map']|.//div[@id='lu_map']") != null //02-11-2-23     // maps
                || node.SelectSingleNode(".//div[@class='xERobd']") != null //  maps    //changed on 26-06-2019
                || node.SelectSingleNode(".//div[@id='kx']") != null      // carousel
                || node.SelectSingleNode(".//div[@id='fac-ut']") != null      // finance
                || node.SelectSingleNode(".//div[@class='_Zfh']") != null   // twitters
                || node.SelectSingleNode(".//div[@class='Brgz0 tw-res']") != null   // twitters                
                || node.SelectSingleNode(".//div[@class='_OKe']") != null   // answer card / people also ask
                || node.SelectSingleNode(".//div[@class='vkc_np kkww4d']") != null   // 23-03-2020
                || node.SelectSingleNode(".//div[@class='k9uN1c kfn9hb']") != null//24-10-2019
                || node.SelectSingleNode(".//div[@class='HaXvv kfn9hb']") != null//07-02-2020
                || (node.SelectSingleNode(".//div[@class='ifM9O']") != null && node.SelectSingleNode(".//div[@class='Wnoohf OJXvsb']") == null)   // answer card  
               || node.SelectSingleNode(".//div[@id='cwmcwd']|.//div[@class='wDYxhc']") != null //21-04-2023  // answer card 
                                                                                                //|| node.SelectSingleNode(".//div[@class='vk_c card-section']") != null // answer card    
                || node.SelectSingleNode(".//div[@class='d7sCQ kp-header']") != null  //03-06-2020                                                      
                                                                                      //|| node.SelectSingleNode(".//div[@class='vk_c card-section']") != null // answer card                
                || node.SelectSingleNode(".//div[@class='pcCUmf vCOSGb']") != null  //03-06-2020
                                                                                    //|| node.SelectSingleNode(".//div[@class='kp-blk cUnQKe Wnoohf OJXvsb']") != null  // people also ask // 11-02-2020
                || node.SelectSingleNode(".//div[contains(@class,'cUnQKe')]") != null //08-07-2021
                                                                                      //|| node.SelectSingleNode(".//span[@data-original-name='People also ask']") != null  // people also ask
                || node.SelectSingleNode(".//h3[@class='_DM']") != null || node.SelectSingleNode(".//div[@id='imagebox_bigimages']") != null || node.SelectSingleNode(".//div[@class='mR2gOd']") != null //27-06-2020   // images
                || node.SelectSingleNode(".//div[@class='e2BEnf']/h3") != null // videos
                || node.SelectSingleNode(".//div[@class='e2BEnf U7izfe']/h3") != null  // videos
                || node.SelectSingleNode(".//div[@class='mod NFQFxe oHglmf xzPb7d']") != null//images//05-08-2020
                || node.SelectSingleNode(".//div[@id='knowledge-finance-wholepage__entity-summary']") != null // 18-03-2020
                || node.SelectSingleNode(".//div[@class='I6TXqe osrp-blk']") != null //12-08-2020 included selector for video card
                || node.SelectSingleNode(".//div[@class='WcS13d']") != null //02-10-2020 maps selectors
                || node.SelectSingleNode(".//h3[@class='GmE3X']") != null //16-10-2020 updated selector for videos
                || node.SelectSingleNode(".//div[@class='twQ0Be']") != null //03-12-2020 updated selector for videocard
                || node.SelectSingleNode(".//div[@class='vwfsqc']") != null //07-12-2020
                || node.SelectSingleNode(".//div[@class='setTDc']") != null //07-12-2020
                || node.SelectSingleNode(".//div[@class='HnYYW']/div") != null //23-07-2021
                || node.SelectSingleNode(".//div[@class='e2BEnf mfMhoc']") != null //25-09-2021 missing top stories
                || node.SelectSingleNode(".//div[@class='e2BEnf']") != null //04-10-2021 top stories
                || node.SelectSingleNode(".//div[@jscontroller='hFvNdd']") != null//13-10-2021
                || node.SelectSingleNode(".//div[@class='g jNVrwc Y4pkMc']") != null //07-12-2021
                || node.SelectSingleNode(".//div[@class='e2BEnf q8U8x']") != null //07-12-2021
                || node.SelectSingleNode(".//div[@jsname='wRSfy']") != null) //07-12-2021
                || node.SelectSingleNode(".//div[@class='e2BEnf axf3qc q8U8x']") != null//29-12-2021 top stories
                || node.SelectSingleNode(".//div[@class='WlTAzf mnr-c']") != null //23-03-2022
                || node.SelectSingleNode(".//div[@class='AxJnmb Wdsnue']") != null //05-01-2023
                || node.SelectSingleNode(".//div[@class='CH6Bmd']") != null//27-02-2023
                || node.SelectSingleNode(".//div[@class='oCLR8']") != null//02-06-2023
                || node.SelectSingleNode(".//div[@class='fPmcEc']") != null//19-09-2023
                || node.SelectSingleNode(".//div[@class='qkC4td']") != null//21-09-2023
                || node.SelectNodes(".//div[@class='xSoq1']") != null//10-10-2023
                || node.SelectNodes(".//div[@class='udVt6e']") != null;//02-11-2023
            if (bVal == true)//2019-09-11
            {
                try
                {
                    if (node.SelectSingleNode(".//div[@class='Brgz0 tw-res']|.//div[@class='k9uN1c kfn9hb']|.//div[@class='baPFxb g kSMK2']") != null
                        && node.SelectNodes(".//div[contains(@class,'g Ww4FFb')]") == null) return true;//24-11-2023//26-10-2023//04-01-2023//16-12-2022
                    if (node.SelectSingleNode(".//div[@class='g jNVrwc Y4pkMc']|.//div[@class='g tF2Cxc']|.//div[@class='g eejeod up9jud']" +
                    //"|.//div[@class='g Ww4FFb tF2Cxc']") != null) return false; //21-07-2022//15-02-2022//02-02-2022//31-12-2021 missing CLinks
                    "|.//div[contains(@class,'g Ww4FFb')]|.//div[@class='BYM4Nd']|.//div[@class='rULfzc']|.//div[@class='g PmEWq']" +//07-12-2023
                    "|.//div[@class='g ZYT4Gf']") != null || (node.Attributes["class"]?.Value?.Contains("g Ww4FFb") ?? false)) return false;//07-08-2023//31-10-2022//11-10-2022
                    if (node.SelectSingleNode(".//div[@class='twQ0Be']|.//div[@jsname='N760b']|.//div[@jsname='wRSfy']|.//div[contains(@class,'e2BEnf U7izfe')]" + //03-11-2023//05-12-2022//26-09-2022//13-08-2022 maps//08-03-2022//07-03-2022//28-12-2021//10-12-2021//09-12-2021 //08-12-2021 PAlsoB   //30-08-2021 video card
                        "|.//div[@jsname='A6RGif']|.//div[@class='P9Jfrb']|.//div[@class='ntKMYc']|.//div[@class='T6zPgb gduDCb']|.//div[@class='M0XuFe mnr-c vk_c']" +
                        "|.//g-section-with-header[@class='yG4QQe TBC9ub']|.//div[contains(@class,'knowledge-finance')]") != null) return true; //05-12-2023
                    if (node.SelectSingleNode(".//div[@class='osrp-blk']|.//div[@class='tpa-cc']|.//div[@class='pKv8Zb fm06If']") != null && node.SelectSingleNode(".//div[@class='l44Vof']") == null && node.SelectSingleNode(".//div[@class='H93uF']") == null)//25-04-2024 //17-05-2022//31-12-2021
                        return false; //20-08-2021
                    if (node.Attributes["id"]?.Value == "rhs") return false;//03-03-2022
                    //02-12-2020
                    HtmlNode nd = node.SelectSingleNode(".//div[@role='heading']|.//div[@class='UDZeY OTFaAf']"); //02-07-2021

                    if (nd != null && (nd.InnerText == "More results" || nd.InnerText == "Top results" || nd.InnerText.Contains("Web results"))) //02-07-2021 //03-12-2020
                        return false;
                    //end 02-12-2020

                    if (node.InnerText.Contains("Podcast") || node.InnerText.Contains("播客") || node.InnerText.Contains("Podcaster")
                        || node.InnerText.Contains("ملفات البودكاست") || node.InnerText.Contains("พอดแคสต์"))   // 19-09-2019
                    {
                        if (node.SelectSingleNode(".//div[@class='Brgz0 tw-res']") == null)
                            bVal = false;
                    }
                    // 02-06-2020
                    if (node.SelectSingleNode(".//div[@class='a3spGf WvKfwe']|.//div[@class='HnYYW i8lZMc']") != null
                        && node.SelectSingleNode(".//div[@class='Brgz0 tw-res']|.//div[@class='kp-blk cUnQKe Wnoohf OJXvsb']") == null)
                        bVal = false;

                    if (node.SelectSingleNode(".//div[contains(@class,'kp-blk')]" +
                        "|.//div[contains(@class,'c2xzTb')]|.//div[@class='lu_map_section']") != null //07-10-2022 missing maps block
                        || node.Attributes["class"]?.Value == "kp-blk c2xzTb")
                        return true;
                    //28-05-2021
                    if (node.SelectSingleNode(".//div[@class='g']") != null)
                        if (node.SelectSingleNode(".//table[@class='nrgt']") != null || node.SelectSingleNode(".//table[@class='jmjoTe']") != null)  // 28-05-2021
                            return true;
                    //28-05-2021 ends

                    if (node.SelectSingleNode(".//div[@class='g']") != null) //04-12-2020 select for class links
                        return false;
                    if (node.SelectSingleNode(".//div[@class='d3zsgb']|.//div[@class='lMMUFc']") != null) //18-07-2022 wrong answer card //13-10-2021
                        return false;
                }
                catch { }
            }

            if (!bVal)
            {
                HtmlNode nd = node.SelectSingleNode(".//h3|.//div[contains(@class,'HnYYW')]|.//div[@class='LMMXP i8lZMc']|.//div[@class='e2BEnf U7izfe']/div|.//div[@class='LMMXP mfMhoc']"); //05-08-2020 included contains function  //17-07-2020 //03-06-2020  // 02-06-2020    //01-05-2020
                if (nd != null)
                    if ((nd.InnerText == "Top stories" || nd.InnerText == "Huvudnyheter" || nd.InnerText == "Videos" || nd.InnerText == "Video" || nd.InnerText == "Tin bài hàng đầu" || nd.InnerText == "Voorpaginanieuws" || nd.InnerText == "Vertaalresultaat" || nd.InnerText == "Recipes" || nd.InnerText == "Vidéos") && node.SelectSingleNode(".//div[contains(@class,'g Ww4FFb')]") == null)//30-10-2023//02-12-2020 videos//05-08-2020 //29-06-2020//03-06-2020 // 02-06-2020  // 08-04-2020
                        return true;

                //enable below line without new block "popularProducts"
                //if (node.SelectSingleNode(".//div[contains(@class,'kp-blk')]") != null || node.SelectSingleNode(".//div[@class='dzpFPb']") != null)//28-05-2022//06-04-2022 //13-10-2021
                if (node.SelectSingleNode(".//div[contains(@class,'kp-blk')]") != null || node.SelectSingleNode(".//div[@class='dzpFPb']") != null || node.SelectSingleNode(".//div[@jscontroller='Yma7vd']") != null || node.SelectSingleNode(".//div[@class='aJegcc']") != null || node.SelectSingleNode(".//div[@class='IbDT9d']") != null)//24-05-2023//09-12-2022//09-11-2022 shopping
                    if (node.SelectSingleNode(".//div[contains(@class,'g Ww4FFb')]") == null)//21-07-2023
                        return true;
                if (node.SelectSingleNode(".//div[contains(@class, 'RPdfze')]|.//div[contains(@class, 'nJMOzb')]" +
                    "|.//div[contains(@class, 'Qkn3ie')]|.//div[contains(@class, 'vdQmEd')]|.//div[@class='lMMUFc']" +
                    "|.//div[@class='wH6SXe']|.//div[contains(@class,'WlTAzf')]|.//div[@class='cj1ht QkBAO oYQBg']" +
                    "|.//div[@class='oIk2Cb']|.//div[@jsmodel='Wn3aEc']") != null && node.SelectSingleNode(".//div[contains(@class,'g')]") == null)//10-12-2024//09-12-2024//04-12-2024//14-10-2024
                    return true;//29-06-2023
                // changes in map block on 19-06-2019.
                nd = node.SelectSingleNode(".//g-img/img");
                if (nd != null)
                {
                    if (nd.Attributes["alt"].Value.StartsWith("Map of ") || node.SelectSingleNode(".//div[@class='H93uF']" + //24-11-2023//06-12-2022//21-04-2022
                        "|.//div[@class='uaxL4e ef0Hld']") != null || node.SelectSingleNode(".//img[contains(@alt,'Map of ')]" +
                        "|.//img[contains(@alt,'Karte von ')]|.//img[contains(@alt,'karte')]|.//div[contains(@class,'o8ebK')]") != null)//08-02-2024//22-01-2024//05-12-2023
                        return true;
                    if (node.SelectSingleNode(".//div[@class='U1TUId LYh3vc']") != null) //16-12-2021
                        return false; //16-12-2021
                }
                // changes on 08-07-2019
                if (node.SelectSingleNode(".//img[@alt='map image']") != null || node.SelectSingleNode(".//div[@jsname='N760b']|.//div[@class='kno-mrg kno-swp']|.//div[@class='e4xoPb']|.//div[@class='H93uF']") != null || node.SelectSingleNode(".//img[contains(@data-bsrc,'/maps/')]") != null)//22-01-2022//23-08-2021 map selector//02-08-2021
                    if (node.SelectSingleNode(".//div[@class='tF2Cxc']|.//div[@class='jtfYYd']") != null || node.Attributes["id"]?.Value == "rhs" || node.SelectSingleNode(".//div[contains(@class, 'rhsg')]") != null)//07-04-2022 //17-02-2022//16-12-2021 //10-12-2021
                        return false;//10-12-2021
                    else //10-12-2021
                        return true;
                if (node.SelectSingleNode(".//div[contains(@class,'WlTAzf')]") != null)
                    return true;
                HtmlNodeCollection nds = node.SelectNodes(".//div");
                if (nds != null)
                    foreach (HtmlNode n in nds)
                    {
                        try
                        {
                            if (n.Attributes["class"] != null)   //01-06-2020
                                if (n.Attributes["class"].Value.Contains("obcontainer"))  //node.SelectSingleNode(".//div[@id='cwmcwd']") != null)
                                {
                                    bVal = true;
                                    break;
                                }
                        }
                        catch
                        { }
                    }
            }
            return bVal;
        }

        private bool IsOrganic(HtmlNode node)
        {
            return (node.SelectSingleNode(".//h3[@class='r']") != null || node.SelectSingleNode(".//div[@class='r']") != null
                || node.SelectSingleNode(".//div[@class='zTpPx']") != null || node.SelectSingleNode(".//div[@class='zTpPx']/g-link/a") != null    // 28-05-2020   // 13-03-2020
                || node.SelectSingleNode(".//h3[@class='r dO0Ag']") != null || node.SelectSingleNode(".//div[@class='DOqJne']") != null //27-06-2020    //29-05-2020
                || node.SelectSingleNode(".//div[@class='rc']") != null // 03-09-2020 missing classic links selector included
                || node.SelectSingleNode(".//div[@class='DOqJne']/g-link/a") != null //twitter classic link selector
                || node.SelectSingleNode(".//div[contains(@class,'tF2Cxc')]/div/a") != null //07-01-2021 missing classic link //18-02-2021 included contains fucntions
                || node.SelectSingleNode(".//div[@class='yuRUbf']") != null //31-05-2021
                || node.SelectSingleNode(".//div/div[@class='g tF2Cxc']|.//div[contains(@class,'g Ww4FFb')]|.//div[contains(@class,'g dFd2Tb')]|.//div[@class='g ZYT4Gf']") != null//10-10-2022//13-07-2022 //07-04-2022//24-08-2021 video block //01-06-2021
                || node.SelectSingleNode(".//div[@class='M42dy']/g-link/a") != null //02-02-2022 twitter link
                || node.SelectSingleNode(".//div[contains(@class,'g PmEWq')]|.//div[@class='g zXItKe']") != null //14-09-2023//23-08-2023
                || node.Attributes["class"]?.Value == "g PmEWq");//08-02-2024
        }

        private string GetRedirectedUrl(string url)
        {
            try //28-09-2020  try catch.
            {
                //21-11-2019
                url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");
                if (string.IsNullOrEmpty(url.Trim()) || url.StartsWith("#")) return string.Empty; //08-08-2022
                //29-09-2020            
                if (url.IndexOf("https://") == 0 || url.IndexOf("https://") >= 0) //01-10-2020
                    url = url.Remove(0, url.IndexOf("https://"));
                else if (url.IndexOf("http://") == 0 || url.IndexOf("http://") >= 0) //14-10-2020 included indexof for http)
                    url = url.Remove(0, url.IndexOf("http://"));
                //end 29-09-2020

                Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
                if (!rx.Match(url).Success && !url.Contains("/aclk?") && !url.Contains("/search?"))//related searches //21-1-2022 "/search" remove from &&
                    // if (!url.StartsWith("/")) //11-09-2021 ignore url start with "/"
                    if (!url.Contains("://")) // 30-04-2020
                        url = "http://" + url;

                if (url.StartsWith("http:////") || url.StartsWith("https:////")) //18-09-2020 condition applied if appears http:////
                    url = url.Replace("////", "//"); //18-09-2020

                if (url.Contains("&amp;grqid="))
                    url = url.Remove(url.IndexOf("&amp;grqid="));
                //13-12-2019
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

                if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && !url.Contains("/aclk?"))// 30-04-2020 
                    return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }

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

        public string SetTitle(string unicodestring)
        {
            //return WebUtility.HtmlEncode(WebUtility.HtmlDecode(unicodestring));
            return WebUtility.HtmlEncode(WebUtility.HtmlDecode(unicodestring)).Replace("\\x27", "'").Replace("\\\\u0026", "&amp;").Replace("\\\\\\x22", "&quot;").Replace("\\u2013", "–"); //17-03-2022
        }

        public string SetUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || url.StartsWith("#")) return string.Empty;//05-04-2024
            try  //28-09-2020  try catch.
            {
                //21-11-2019
                url = GetRedirectedUrl(WebUtility.HtmlDecode(url).Trim());
                //if (url.ToLower().Contains("%2f") || url.ToLower().Contains("%2e"))//18-09-2020 commented
                if (url.Contains("%")) //18-09-2020
                    url = GetRedirectedUrl(WebUtility.UrlDecode(WebUtility.HtmlDecode(url)).Trim());
                ////SanitizeXmlString(url);
                return WebUtility.HtmlEncode(SanitizeXmlString(url).Replace("\x00", "%00")).Replace("\\\\u003d", "=").Replace('\u0002', ' ').Replace('\u0018', ' ').Replace('\f', ' ').Trim();//23-09-2020 applied method to URL  //26-03-2020 updated converting hexadecimal codes
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //27-08-2020
        private string GetRedirectedUrl_TextAds(string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;
            try  //28-09-2020  try catch.
            {
                url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");
                if (string.IsNullOrEmpty(url.Trim())) return string.Empty;
                Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
                if (!rx.Match(url).Success && !url.Contains("/aclk?"))
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
                    url = WebUtility.UrlDecode(WebUtility.HtmlDecode(url)).Trim();//01-10-2020
                    return WebUtility.HtmlEncode(SanitizeXmlString(url).Replace("\x00", "%00")).Replace("\\\\u003d", "=").Replace('\u0002', ' ').Replace('\u0018', ' ').Replace('\f', ' ').Trim(); //23-09-2020 applied method to URL
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
        //end 23-09-2020

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
                    (character >= 0xE000 && character < 0xFFFD) ||  //02-11-2020 changed <= 0xFFFD to < 0xFFFD
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

}


