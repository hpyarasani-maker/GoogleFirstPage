using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using Ionic.Zip;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.Configuration;
using System.Data.SqlClient;

namespace GoogleFirstPage.Classiclinks
{
    public partial class missinglinks : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string connection = ConfigurationManager.ConnectionStrings["allsearchengines"].ToString();
        string jobid1 = string.Empty;
        string device = string.Empty;
        string res = string.Empty;
        string resRx = string.Empty;
        string keyword = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //oxydiv1.Visible = false;
            Label1.Visible = false;
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    cmd = new SqlCommand("GetSearchEngines", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    ddlseid.DataTextField = "name";
                    ddlseid.DataValueField = "seid";
                    ddlseid.DataSource = dt;
                    ddlseid.DataBind();
                    ddlseid.SelectedItem.Value.ToString();
                    ddlseid.SelectedIndex = 0;
                }
            }
        }

        protected void btnmsg_Click(object sender, EventArgs e)
        {
            //oxydiv1.Visible = true;
            Label1.Visible = true;
            try
            {
                ArrayList data = new ArrayList();
                DataTable dt = Table();
                DataSet ds = new DataSet();
                var doc = new HtmlAgilityPack.HtmlDocument();

                string seid = ddlseid.SelectedValue;
                string kewrd = txtmsgsearch.Text;
                ArrayList alresult = GetHTML(kewrd, Convert.ToInt32(seid));
                foreach (string[] src in alresult)
                {
                    keyword = src[0];
                    JObject obj = JObject.Parse(src[1]);
                    string html = obj["results"][0]["content"].Value<string>();
                    string jobid = src[2];
                    device = src[3];
                    doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);

                    int count = 0;
                    if (device == "desktop")
                    {
                        Desktop clsDesktop = new Desktop();
                        res = clsDesktop.ProcessDocument(seid, keyword, doc, out count);
                        //resRx = clsDesktop.ProcessClassicLinks(seid, keyword, doc);
                    }
                    else
                    {
                        iOS1 clsiOS = new iOS1();
                        res = clsiOS.ProcessDocument(seid, keyword, doc, out count);
                        resRx = clsiOS.ProcessClassicLinks(seid, keyword, doc);
                    }
                    if (!string.IsNullOrEmpty(res))
                    {
                        if (count > 20)
                        {
                            CreateXml(res, resRx);
                        }
                    }
                }
                GetHtmlFiles();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                Response.Write(error);
            }
        }

        private void CreateXml(string res, string resRx)
        {
            XmlDocument xd = new XmlDocument();
            res = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + res;
            xd.LoadXml(res);
            File.WriteAllText(Server.MapPath("~/files/" + "selector.xml"), res);

            resRx = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + resRx;
            xd.LoadXml(resRx);
            File.WriteAllText(Server.MapPath("~/files/" + "regex.xml"), resRx);
        }

        //public string GetMissedlinkdata(string keyword)
        //{
        //    string kwd1 = "Missedlink.aspx?keyword=" + txtmsgsearch.Text.Replace("'", "%27").Trim();
        //    ClientScript.RegisterStartupScript(this.GetType(), "Popup_File", "window.open('" + kwd1 + "');", true);
        //    return keyword;
        //}

        protected void txtmsgsearch_TextChanged(object sender, EventArgs e)
        {
            gridviewmsg.DataSource = null;
            gridviewmsg.DataBind();
        }

        public void GetHtmlFiles()
        {
            string filePath = string.Empty;
            string filepath1 = string.Empty;
            string[] filePaths1 = Directory.GetFiles(Server.MapPath("~/files/"));
            foreach (string file in filePaths1)
            {
                if ((System.IO.File.Exists(file)))
                {
                    System.IO.File.Delete(file);
                }
            }
            try
            {
                ArrayList data = new ArrayList();
                DataTable dt = Table();
                DataSet ds = new DataSet();

                string jobid = jobid1;
                string seid = ddlseid.SelectedValue.Trim();

                string html = GetHtmlSource(jobid, seid, out string device, out string url, out string gl, out string keyword);
                string html1 = "";

                File.WriteAllText(Server.MapPath("~/files/" + "Oxylabs_" + device + "_" + gl + "_" + jobid + "_" + keyword + ".html"), html);
                if (device == "desktop")
                    html1 = getWebDataSource(url);
                else
                    html1 = getWebDataMobileSource(url);

                File.WriteAllText(Server.MapPath("~/files/" + "Google_" + device + "_" + gl + "_" + keyword + ".html"), html1);
                if (device == "desktop")
                {
                    CreateXml(res, resRx);
                    GetProcessedLists();
                    try
                    {
                        //GetMissedlinkdata(keyword);
                        string[] filePaths = Directory.GetFiles(Server.MapPath("~/files/"));
                        List<ListItem> zipFiles = new List<ListItem>();
                        foreach (string file in filePaths)
                        {
                            zipFiles.Add(new ListItem(Path.GetFileName(file), file));
                        }
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                            foreach (ListItem s in zipFiles)
                            {
                                string filePath2 = Server.MapPath("~/files/" + s);
                                zip.AddFile(filePath2, "" + device + "_" + jobid + "_" + keyword);
                            }
                            Response.Clear();
                            GetProcessedLists();
                            Response.BufferOutput = false;
                            string zipName = String.Format("Zip_{0}.zip", device + "_" + gl + "_" + keyword);
                            Response.ContentType = "application/zip";
                            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                            zip.Save(Response.OutputStream);
                            Response.End();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    CreateXml(res, resRx);

                    try
                    {
                        string[] filePaths = Directory.GetFiles(Server.MapPath("~/files/"));
                        List<ListItem> zipFiles = new List<ListItem>();
                        foreach (string file in filePaths)
                        {
                            zipFiles.Add(new ListItem(Path.GetFileName(file), file));
                        }
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                            foreach (ListItem s in zipFiles)
                            {
                                string filePath2 = Server.MapPath("~/files/" + s);
                                zip.AddFile(filePath2, "" + device + "_" + jobid + "_" + keyword);
                            }
                            Response.Clear();
                            Response.BufferOutput = false;
                            string zipName = String.Format("Zip_{0}.zip", device + "_" + gl + "_" + keyword);
                            Response.ContentType = "application/zip";
                            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                            zip.Save(Response.OutputStream);
                            Response.End();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
        }

        private string GetHtmlSource(string jobId, string seid, out string device, out string url, out string gl, out string keyword)
        {

            string username = "piapp";
            string password = "b5FCvgkjxx";

            string resURL = "http://data.oxylabs.io/v1/queries/" + jobId + "/results";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(resURL);
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream resStream = res.GetResponseStream();
            StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
            string response = reader.ReadToEnd();
            resStream.Close();
            res.Close();

            try
            {
                JObject obj = JObject.Parse(response);
                response = obj["results"][0]["content"].Value<string>();

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(response);
                HtmlNode htmlNode = doc.DocumentNode.SelectSingleNode("//title");
                keyword = htmlNode.InnerText;
                int pos = keyword.IndexOf("-");
                keyword = keyword.Remove(pos);

                SearchProperties sp = SearchParams.searches.Where(s => s.seid == Convert.ToInt32(seid)).SingleOrDefault();
                device = sp.device;
                int lan = sp.locale.IndexOf("-");
                string lan1 = sp.locale.Remove(lan);
                string s1 = sp.locale.Remove(0, 3);
                //seid = sp.seid.ToString();

                if (device == "desktop")
                    url = "https://www.google." + sp.domain + "/search?q=" + keyword + "&gl=" + s1 + "&hl=" + lan1 + "&uule=" + sp.uule + "&num=100&aomd=1&safe=off&safe_search=0&gs_l=desktop&gws_rd=ssl,cr";
                else
                    url = "https://www.google." + sp.domain + "/search?q=" + keyword + "&gl=" + s1 + "&hl=" + lan1 + "&uule=" + sp.uule + "&num=100&aomd=1&safe=off&safe_search=0&gs_l=mobile-gws-serp.3.0&gws_rd=ssl,cr";

                gl = sp.geo_location;
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getWebDataSource(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                req.Headers.Clear();
                //req.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.110 Safari/537.36";
                req.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.90 Safari/537.36";
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                if (res.StatusCode != HttpStatusCode.OK) throw new Exception(res.StatusDescription);
                // replace the cookie ...
                //** get the stream of data and read into a string
                Stream respStream = res.GetResponseStream();

                //** Contents of HTML in the Response object to a Stream reader
                StreamReader reader = new StreamReader(respStream, Encoding.UTF8); //windows default code page
                //** Store all the contents
                String respHTML = reader.ReadToEnd();

                respStream.Close();
                res.Close();

                // respHTML = obj["results"][0]["content"].Value<string>();

                // device = sp.device;

                // if (device == "desktop")
                // googleurl = obj[0]["url"].Value<string>();
                //else
                //    url = obj["results"][0]["url"].Value<string>() + "&gs_l=mobile-gws-serp.3.0";


                return respHTML;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getWebDataMobileSource(string url)
        {
            try
            {
                Uri uri = new Uri(url);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                req.Headers.Clear();

                req.UserAgent = @"Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1";

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                if (res.StatusCode != HttpStatusCode.OK) throw new Exception(res.StatusDescription);

                //** get the stream of data and read into a string
                Stream respStream = res.GetResponseStream();
                //** Contents of HTML in the Response object to a Stream reader
                StreamReader reader = new StreamReader(respStream, Encoding.UTF8); //windows default code page
                //** Store all the contents
                String respHTML = reader.ReadToEnd();

                respStream.Close();
                res.Close();
                return respHTML;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        List<ArrayList> GetProcessedLists()
        {
            string xml1 = Server.MapPath(@"\files\selector.xml");
            string xml2 = Server.MapPath(@"\files\regex.xml");

            List<ArrayList> al = new List<ArrayList>();
            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();

            XmlDocument xml = new XmlDocument();
            xml.Load(xml1);
            XmlNodeList xnList1 = xml.SelectNodes("/searchResult/section/item/@url");
            foreach (XmlNode xn1 in xnList1)
            {
                list1.Add(xn1.InnerText);
            }

            xml.Load(xml2);
            XmlNodeList xnList2 = xml.SelectNodes("/searchResult/section/item/@url");
            foreach (XmlNode xn2 in xnList2)
            {
                list2.Add(xn2.InnerText);
            }

            ArrayList missedList = new ArrayList();
            string[] st = { "", "" };

            foreach (string s in list1)
            {
                if (!list2.Contains(s))
                {
                    st[0] = s;
                    st[1] = "RegEx";
                    missedList.Add(st);
                }
            }
            foreach (string s in list2)
            {
                if (!list1.Contains(s))
                {
                    st[0] = s;
                    st[1] = "Selector";
                    missedList.Add(st);
                }
            }
            if (missedList.Count > 0)
                SaveToXml(missedList);
                BindData(missedList);
            al.Add(list1);
            al.Add(list2);
            return al;
        }

        public void BindData(ArrayList missedList)
        {
            DataTable dt = Table();
            foreach (string[] s in missedList)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(s[0], s[1]);
            }
            gridviewmsg.DataSource = dt;
            gridviewmsg.DataBind();
        }

        private void SaveToXml(ArrayList missedList)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.Append("<searchResult searchEngine=\"" + ddlseid.SelectedValue.ToString() + "\" keyword=\"" + System.Net.WebUtility.HtmlEncode(txtmsgsearch.Text) + "\" date=\"" + DateTime.Today.ToString("yyyy-MM-dd") + "\" >");
            sb.Append("<section col=\"missedLinks\">");

            foreach (string[] s in missedList)
            {
                sb.Append("<item url=\"" + s[0] + "\" missedIn=\"" + s[1] + "\" />");
            }

            sb.Append("</section>");
            sb.Append("</searchResult>");

            string xmlPath = @"C:\inetpub\wwwroot\";
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sb.ToString());
            xd.Save(xmlPath + "missing.xml");
            //File.WriteAllText(Server.MapPath("~/files/" + "missing.xml"), sb.ToString());
        }

        public ArrayList GetHTML(string keyword, int seid)
        {
            ArrayList alResult = new ArrayList();
            try
            {
                SearchProperties sp = SearchParams.searches.Where(s => s.seid == seid).SingleOrDefault();
                sp.query = keyword;
                if (sp != null)
                    alResult = GetOxylabsWebDataSources(sp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return alResult;
        }

        ArrayList GetOxylabsWebDataSources(SearchProperties sp)
        {
            Uri queryUri = new Uri("http://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = sp.domain,
                query = sp.query.Split(','),
                limit = 100,
                pages = 1,
                //start_page = 1,
                locale = sp.locale,
                geo_location = sp.geo_location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = sp.device,
                context = new List<Context> {
                    new Context("safe_search", 0)
                }
            };

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();

            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authInfo);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(op, new Newtonsoft.Json.JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                });

                streamWriter.Write(json);
            }

            string response;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                res.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            JObject jo = JObject.Parse(response);
            var links = from p in jo["queries"] select p;
            ArrayList lst = new ArrayList();
            foreach (JToken link in links)
            {
                string kw = link["query"].Value<string>();
                string href = link["_links"][1]["href"].Value<string>();
                string status = link["status"].Value<string>();
                jobid1 = link["id"].Value<string>();
                string device = link["user_agent_type"].Value<string>();
                string[] s = { kw, href, status, "no", jobid1, device };    // keyword, url, status, isdownloaded, jobid, device.
                lst.Add(s);
            }

            if (lst.Count <= 0) return lst;
            ArrayList alResult = new ArrayList();
            do
            {
                int cnt = 0;
                foreach (string[] cbUrl in lst)
                {
                    string[] reslt = { "", "", "", "" };
                    response = "";

                    Uri uri = new Uri(cbUrl[1]);
                    if (cbUrl[2] == "done" && cbUrl[3] == "no")
                    {
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();

                            Stream resVal = res.GetResponseStream();
                            StreamReader reader = new StreamReader(resVal, Encoding.UTF8);
                            //** Store all the contents
                            response = reader.ReadToEnd();
                            resVal.Close();
                            res.Close();

                            cbUrl[3] = "yes";
                            cnt++;

                            if (!string.IsNullOrEmpty(response))
                            {
                                reslt[0] = cbUrl[0];
                                reslt[1] = response;
                                reslt[2] = cbUrl[4];
                                reslt[3] = cbUrl[5];
                                alResult.Add(reslt);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Result Request: " + ex.Message);
                        }
                    }
                    else if (cbUrl[2] == "faulted" && cbUrl[3] == "no")
                    {
                        cbUrl[3] = "yes";
                        cnt++;
                    }
                    else if (cbUrl[2] == "pending" && cbUrl[3] == "no")
                    {
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri.ToString().Replace("/results", ""));
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();

                            string doneresp = "";
                            using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                            {
                                doneresp = reader.ReadToEnd();
                            }
                            res.Close();

                            JObject job = JObject.Parse(doneresp);
                            string status = job["status"].Value<string>();
                            cbUrl[2] = status;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Status Request: " + ex.Message);
                        }
                    }
                    else
                        cnt++;
                    Task.Delay(200).Wait();
                }
                if (lst.Count == cnt) break;

            } while (true);

            return alResult;
        }


        protected void gridviewmsg_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        public DataTable Table()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("URL");
            dt.Columns.Add("MissedIn");
            return dt;
        }

    }
}