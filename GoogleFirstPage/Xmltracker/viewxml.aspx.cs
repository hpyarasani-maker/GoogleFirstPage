using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            //string name = Request.QueryString["KName"].ToString();
            //string date = Request.QueryString["date"].ToString();
            //string kid = Request.QueryString["kid"].ToString();
            //string uid = Request.QueryString["uid"].ToString();
            string url = Request.QueryString["url"].ToString();
            string date = DateTime.Today.ToString("yyyy-MM-dd");



            processresults(url);


            //SqlConnection con = new SqlConnection(connection);
            ////string strQuerry = "exec [dbo].[GetXMLData] " + uid + ",'" + date + "','" + name + "'";
            //string strQuerry = "exec [dbo].[GetXMLData] " + uid + ",'" + date + "'";

            //SqlCommand comm = new SqlCommand(strQuerry, con);
            //try
            //{
            //    con.Open();
            //    SqlDataReader dr = comm.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        Response.ContentType = "text/xml";
            //        Response.Write(dr.GetValue(0).ToString());
            //    }
            //    dr.Close();
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message.ToString());
            //}
            //finally
            //{
            //    comm.Dispose();
            //    con.Close();
            //    con.Dispose();
            //}
        }

        public string webSourceTracking(string url)
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

                    }
                }

                return respHTML;

            }
            catch (WebException we)
            {


            }
            catch (Exception ex)
            {

            }

            return respHTML;

        }


        public void processresults(string url)
        {
            string urlSource = string.Empty;
            string urlData = string.Empty;
            string xmlData = string.Empty;
            string strIns = string.Empty;

            //string xmlPath = "C:\\inetpub\\wwwroot\\xmlResults.xml";


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
                unchar.Add("â€¢");
                foreach (String str in unchar)
                {
                    urlData = urlSource.Replace(str, "");
                }
                //urlData = urlSource.Replace("'", "''");

                Encoding utf8 = Encoding.UTF8;
                //resultList.Text = HttpUtility.UrlDecode(urlData, utf8);
                string Text1 = HttpUtility.UrlDecode(urlData, utf8);
                StringBuilder sb = new StringBuilder();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(Text1);

                string htmlElements = "meta,title,dd,dl,dt,menu,pre,cite,code,data,time,div,h1,h2,h3,h4,h5,h6,p,span,a,ul,ol,li,b,i,u,hr,br,strong,em,table,tbody,tfoot,tr,th,thead,td,col,colgroup,img,picture,map,track,video,svg,button,form,input,label,option,select,textarea";
                var strElements = htmlElements.Split(',');

                sb.Append("<?xml version=\"1.0\" ?>");
                sb.Append("<XmlSource date=\"" + WebUtility.HtmlEncode(WebUtility.HtmlDecode(DateTime.Today.ToString("yyyy-MM-dd"))) + "\" url=\"" + WebUtility.HtmlEncode(WebUtility.HtmlDecode(url)) + "\">");

                foreach (string ele in strElements)
                {
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes($"//{ele}");
                    if (nodes != null)
                    {
                        sb.Append($"<tag type=\"{ele.ToUpper()}\">");

                        foreach (HtmlNode node in nodes)
                        {
                            try
                            {
                                string eleVal = string.Empty;
                                string nodeText = WebUtility.HtmlEncode(WebUtility.HtmlDecode(node?.InnerText));

                                if (string.IsNullOrEmpty(nodeText) && node?.Name != "meta") continue;
                                if (node?.Name == "a")
                                    eleVal = $"<{node?.Name} href=\"{WebUtility.HtmlEncode(WebUtility.HtmlDecode(node?.Attributes["href"]?.Value))}\">";
                                else if (node?.Name == "img")
                                    eleVal = $"<{node?.Name} alt=\"{WebUtility.HtmlEncode(WebUtility.HtmlDecode(node?.Attributes["alt"]?.Value))}\" title=\"{WebUtility.HtmlEncode(WebUtility.HtmlDecode(node?.Attributes["title"]?.Value))}\">";
                                else if (node?.Name == "meta")
                                {
                                    if (node?.Attributes["name"]?.Value == "keywords" || node?.Attributes["name"]?.Value == "description")
                                        eleVal = $"<{node?.Name} name=\"{node?.Attributes["name"]?.Value}\" content=\"{WebUtility.HtmlEncode(WebUtility.HtmlDecode(node?.Attributes["content"]?.Value))}\">";
                                }
                                else
                                    eleVal = $"<{node?.Name}>";

                                eleVal += nodeText;
                                if (string.IsNullOrEmpty(eleVal)) continue;

                                eleVal += $"</{node?.Name}>";

                                eleVal = Regex.Replace(eleVal, @">\s+", ">");
                                eleVal = Regex.Replace(eleVal, @"\s+<", "<");
                                eleVal = Regex.Replace(eleVal, @"\s+", " ");

                                if (!sb.ToString().Contains(eleVal))
                                {
                                    sb.Append(eleVal);
                                }
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex.Message);
                            }
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

                //xmlTxt.Refresh();
            }
            catch (Exception ex)
            {

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
