using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web.Services;
using GoogleFirstPage.Internallink;

namespace GoogleFirstPage.Internallinks
{
    public partial class internallinks : System.Web.UI.Page
    {
        public string lnk = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtinternallinks_TextChanged(object sender, EventArgs e)
        {
            lbl2.Text = "";
            gridlinkfinder.DataSource = null;
            gridlinkfinder.DataBind();
        }
        List<LinkResult> linkResults = new List<LinkResult>();

        protected void btnsearchlinks_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                System.Threading.Thread.Sleep(1000);

                //Uri url = new Uri("https://en.wikipedia.org/wiki/Main_Page");
                GETDATAURLS(txtinternallinks.Text.Trim());
                gridlinkfinder.DataSource = linkResults;
                gridlinkfinder.DataBind();

                if (lbl2.Text == "")
                {
                    lbl2.Text = "Your search Domain is : " + txtinternallinks.Text + "<br/>" + "Total internal Links Found : " + linkResults.Count;
                }
            }
        }

        public void GETDATAURLS(string urlString)
        {
            //string urlString = search_txt.Text.Trim();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                Uri url = new Uri(urlString);
                string host = url.GetLeftPart(UriPartial.Authority);
                string domain = GetDomain(url);
                WebClient wb = new WebClient();
                //wb.Headers.Add("user-agent", " Mozilla/5.0 (Windows NT 6.1; WOW64; rv:25.0) Gecko/20100101 Firefox/25.0");
                wb.Encoding = Encoding.UTF8;
                string s = wb.DownloadString(url);


                int cnt = 0;

                foreach (LinkItemAgility links in LinkFinderAgility.Find(s))
                {
                    try
                    {
                        bool flag = false;
                        lnk = WebUtility.UrlDecode(links.Href);
                        if (lnk != null)
                        {

                            if (lnk.StartsWith("#") || lnk.StartsWith("//")) continue;
                            if (lnk.StartsWith("/"))
                                lnk = url.GetLeftPart(UriPartial.Authority) + lnk.Replace("//", "/");
                            else if (!lnk.StartsWith("http"))
                                lnk = url.GetLeftPart(UriPartial.Authority) + "/" + lnk.Replace("//", "/");
                            if (!lnk.Contains(domain)) continue;
                            foreach (LinkResult ldup in linkResults)
                            {
                                if (ldup.URL == lnk && ldup.Text == (string.IsNullOrEmpty(links.Text.Trim()) ? string.Empty : WebUtility.UrlDecode(links.Text)) && ldup.TextType == ((links.Text.Contains("<img ") || string.IsNullOrEmpty(links.Text.Trim())) ? "Image" : "Text"))
                                { flag = true; break; }
                            }
                            if (flag)
                            {
                                continue;
                            }
                            linkResults.Add(new LinkResult
                            {
                                SlNo = ++cnt,
                                URL = lnk,
                                Text = string.IsNullOrEmpty(links.Text.Trim()) ? string.Empty : WebUtility.UrlDecode(links.Text),
                                LinkType = "Internal",
                                TextType = (links.Text.Contains("<img ") || string.IsNullOrEmpty(links.Text.Trim())) ? "Image" : "Text"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        /*if (ex.Response != null)
                        {
                            var response = ex.Response;
                            var dataStream = response.GetResponseStream();
                            var reader = new StreamReader(dataStream);
                            var details = reader.ReadToEnd();
                            //lbl2.Text = "Domain cannot be resolved : " + "''" + search_txt.Text + "''";
                            lbl2.Text = details + "''" + search_txt.Text + "''";

                        }*/
                        //lbl2.Text = ex.Message.ToString() + "''" + search_txt.Text + "''";
                        lbl2.Text = "Domain cannot be resolved : " + "''" + txtinternallinks.Text + "''";

                    }
                }
            }
            catch (WebException ex)
            {
                /*if (ex.Response != null)
                {
                    var response = ex.Response;
                    var dataStream = response.GetResponseStream();
                    var reader = new StreamReader(dataStream);
                    var details = reader.ReadToEnd();
                    //lbl2.Text = "Domain cannot be resolved : " + "''" + search_txt.Text + "''";
                    lbl2.Text = details + "''" + search_txt.Text + "''";

                }*/
                //lbl2.Text = ex.Message.ToString() + "''" + search_txt.Text + "''";
                lbl2.Text = "Domain cannot be resolved : " + "''" + txtinternallinks.Text + "''";
            }
        }

        private static string GetDomain(Uri url)
        {
            string[] split = url.Host.Split('.');
            if (split.Length > 2)
                return split[split.Length - 2] + "." + split[split.Length - 1];
            else
                return url.Host;
        }
        protected void ImageExcel_Click(object sender, ImageClickEventArgs e)
        {
            GETDATAURLS(txtinternallinks.Text.Trim());

            if (linkResults.Count > 0)
            {
                try
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append(string.Format("{0},{1},{2},{3},{4}", "SlNo", "URL", "Text", "TextType", "LinkType") + Environment.NewLine);

                    foreach (LinkResult objlnks in linkResults)
                    {
                        sb.Append(string.Format("{0},{1},{2},{3},{4}", objlnks.SlNo, objlnks.URL.ToString(), ReplaceHTML(objlnks.Text.ToString().Replace(",", "")), objlnks.TextType.ToString(), objlnks.LinkType.ToString()) + Environment.NewLine);
                    }

                    byte[] bytes = Encoding.ASCII.GetBytes(sb.ToString());
                    if (bytes != null)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("Content-Length", bytes.Length.ToString());
                        Response.AddHeader("Content-disposition", "attachment; filename=\"internetlinks.csv" + "\"");
                        //Response.Charset = "";
                        Response.ContentType = "application/text;charset=utf-8";
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                    }
                }
                catch (Exception exImg)
                {
                    exImg.Message.ToString();
                }
            }

        }

        protected void gridlinkfinder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
        }

        public string ReplaceHTML(string input)
        {
            input = input.Replace("''", "'");
            input = input.Replace("&#x27;", "'");
            input = input.Replace("&quot;", "\"" + "\"");
            input = input.Replace("&nbsp;", string.Empty);
            input = input.Replace("&#x2716;", string.Empty);
            input = input.Replace("&#8220;", string.Empty);
            input = input.Replace("&#8221;", string.Empty);
            input = input.Replace("&#9658;", string.Empty);
            input = input.Replace("&#233;", string.Empty);
            input = input.Replace("&#39;", string.Empty);
            input = input.Replace("&#x25;", string.Empty);
            input = input.Replace("&#42;", string.Empty);
            input = input.Replace("10&#x25;", string.Empty);
            input = input.Replace("Off&#42;", string.Empty);
            input = input.Replace("&#x27;", string.Empty);
            input = input.Replace("&#8226;", string.Empty);
            input = input.Replace("&#038;", string.Empty);
            input = input.Replace("&#8230;", string.Empty);
            input = input.Replace("&#160;", string.Empty);
            input = input.Replace("&#x1F50E;", string.Empty);
            input = input.Replace("&#187;", string.Empty);
            input = input.Replace("&#039;", string.Empty);
            if (input.Contains("&#"))
                input = input.Substring(0, input.Length);
            return input.Trim();
        }

    }
}