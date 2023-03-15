using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace GoogleFirstPage.Xmltracker
{
    public partial class viewxml : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["xmltracker"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.QueryString["url"].ToString();
            string date = DateTime.Today.ToString("yyyy-MM-dd");

            processresults(url);
        }

        private CookieContainer _cookies = new CookieContainer();

        //In case you need to clear the cookies
        public void ClearCookies()
        {
            _cookies = new CookieContainer();
        }
        public string webSourceTracking(string url)
        {
            string html = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.CookieContainer = _cookies;
                request.AllowWriteStreamBuffering = true;
                //request.UserAgent = ".NET Framework Test Client";
                request.UserAgent = "Mozilla/5.0 (Macintosh; U; Intel Mac OS X 10_6_3; en-US) AppleWebKit/533.4 (KHTML, like Gecko) Chrome/5.0.375.70 Safari/533.4";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(response.StatusDescription);
                var stream = response.GetResponseStream();

                using (var reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
            }
            catch (WebException web)
            {
                Response.Write(web.Message);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return html;
        }

        /* public string webSourceTracking(string url)
         {
             string respHTML = string.Empty;
             try
             {
                 Uri u = new Uri(url);

                 using (var client = new WebClient())
                 {
                     try
                     {
                         client.Headers.Clear();
                         client.Headers["Content-Type"] = "text/xml";
                         respHTML = client.DownloadString(u);
                     }

                     catch (Exception ex)
                     {
                         Response.Write(ex.Message);
                     }
                 }
                 return respHTML;
             }
             catch (WebException web)
             {
                 Response.Write(web.Message);
             }
             catch (Exception ex)
             {
                 Response.Write(ex.Message);
             }

             return respHTML;

         }*/


        public void processresults(string url)
        {
            string urlSource = string.Empty;
            string urlData = string.Empty;
            string xmlData = string.Empty;
            string strIns = string.Empty;


            try
            {
                urlSource = webSourceTracking(url);
                List<string> unchar = new List<string>();
                unchar.Add("?qx?d");
                unchar.Add("'");
                unchar.Add("???x?2");
                unchar.Add("{?xw?h");
                unchar.Add("hi	?fe	h");
                unchar.Add("pi	?fe	h");
                unchar.Add("??jx?5");
                unchar.Add("p??x?2");
                unchar.Add("?8rx?d");
                unchar.Add("??q ?mh");
                unchar.Add("0?jx?5");
                unchar.Add("0?i	x??");
                unchar.Add("??q");
                unchar.Add("(?qx?d");
                unchar.Add("p?m??mh");
                unchar.Add("...");
                unchar.Add("&apos;s");
                unchar.Add("&apos;ll");
                unchar.Add("ll");
                unchar.Add("&#039;");
                unchar.Add("&#0...");
                unchar.Add("')");
                unchar.Add("..");
                unchar.Add("been 12 ");
                unchar.Add("UK");
                unchar.Add("...");
                unchar.Add("UK");
                unchar.Add("...");
                unchar.Add("s");
                unchar.Add("Jo..");
                unchar.Add("&amp;");
                unchar.Add("??`?&apos;-?~?");
                unchar.Add("..");
                unchar.Add("");
                unchar.Add("");
                unchar.Add("");
                unchar.Add("");
                unchar.Add("  ");
                unchar.Add("上");
                unchar.Add("  ");
                unchar.Add("Â");
                unchar.Add("Ã");
                unchar.Add("â€¢");
                unchar.Add("â€");
                foreach (string str in unchar)
                {
                    urlSource = urlSource.Replace(str, "");
                }


                Encoding utf8 = Encoding.UTF8;
                string Text1 = HttpUtility.UrlDecode(urlSource, utf8);
                StringBuilder sb = new StringBuilder();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(Text1);

                string htmlElements = "meta,title,dd,dl,dt,menu,pre,cite,code,data,time,h1,h2,h3,h4,h5,h6,p,span,a,ul,ol,li,b,i,u,hr,br,strong,em,table,tbody,tfoot,tr,th,thead,td,col,colgroup,img,picture,map,track,video,svg,button,form,input,label,option,select,textarea";
                var strElements = htmlElements.Split(',');

                sb.Append("<?xml version=\"1.0\" ?>");
                sb.Append("<XmlSource date=\"" + WebUtility.HtmlEncode(WebUtility.HtmlDecode(DateTime.Today.ToString("yyyy-MM-dd"))) + "\" url=\"" + WebUtility.HtmlEncode(WebUtility.HtmlDecode(url)) + "\">");

                foreach (string ele in strElements)
                {
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes($"//{ele}");
                    if (nodes != null)
                    {
                        string eleVal = string.Empty;

                        foreach (HtmlNode node in nodes)
                        {
                            try
                            {
                                string eVal = string.Empty;
                                string nodeText = WebUtility.HtmlEncode(WebUtility.HtmlDecode(node?.InnerText));

                                if (string.IsNullOrEmpty(nodeText.Trim()) && node?.Name != "img" && node?.Name != "meta") continue;

                                if (node?.Name == "a") //<anchor>
                                    eVal = $"<href>{WebUtility.HtmlEncode(WebUtility.HtmlDecode(node?.Attributes["href"]?.Value))}</href>";
                                else if (node?.Name == "img") //<image>
                                {
                                    string src = node.Attributes["src"]?.Value != null && (bool)!node.Attributes["src"]?.Value?.Trim().StartsWith("data:image")
                                        ? WebUtility.HtmlEncode(WebUtility.HtmlDecode(node?.Attributes["src"]?.Value)) : "";
                                    eVal = $"<alt>{WebUtility.HtmlEncode(WebUtility.HtmlDecode(node?.Attributes["alt"]?.Value))}</alt><src>{src}</src>";
                                }
                                else if (node?.Name == "meta")
                                {
                                    if (node?.Attributes["name"]?.Value == "keywords" || node?.Attributes["name"]?.Value == "description")
                                        eVal = $"<{node?.Attributes["name"]?.Value}>{WebUtility.HtmlEncode(WebUtility.HtmlDecode(node?.Attributes["content"]?.Value))}</{node?.Attributes["name"]?.Value}>";
                                }

                                if (node?.Name != "meta" && node?.Name != "img")
                                    eVal += $"<text>{nodeText}</text>";

                                if (string.IsNullOrEmpty(eVal)) continue;

                                eVal = Regex.Replace(eVal, @">\s+", ">");
                                eVal = Regex.Replace(eVal, @"\s+<", "<");
                                eVal = Regex.Replace(eVal, @"\s+", " ");

                                if (!eleVal.Contains(eVal))
                                {
                                    eleVal += eVal;
                                }
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex.Message);
                            }
                        }

                        sb.Append($"<tag type=\"{ele.ToUpper()}\">");

                        if (!string.IsNullOrEmpty(eleVal))
                        {
                            if (ele == "a")
                                eleVal = $"<anchor>{eleVal}</anchor>";
                            else if (ele == "img")
                                eleVal = $"<image>{eleVal}</image>";
                            else
                                eleVal = $"<{ele}>{eleVal}</{ele}>";

                            sb.Append(eleVal);
                        }

                        sb.Append("</tag>");
                    }
                }

                sb.Append("</XmlSource>");
                XmlDocument xd = new XmlDocument();
                xd.PreserveWhitespace = true;

                xd.LoadXml(SanitizeXmlString(sb.ToString().Replace("\n", "").Replace("\r\n", "")));
                //xd.PreserveWhitespace = true;
                Response.ContentType = "text/xml";
                Response.Write(xd.InnerXml);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }

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
}