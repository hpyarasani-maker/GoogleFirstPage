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

namespace GoogleFirstPage.RapidTrackingSERPs
{
    public partial class Oxylabsjobid : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;

        string connection = ConfigurationManager.ConnectionStrings["allsearchengines"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    cmd = new SqlCommand("GetSearchEngines", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    ddlseids.DataTextField = "name";
                    ddlseids.DataValueField = "seid";
                    ddlseids.DataSource = dt;
                    ddlseids.DataBind();
                    ddlseids.SelectedItem.Value.ToString();
                    ddlseids.SelectedIndex = 0;
                }
            }
        }

        protected void btndata_Click(object sender, EventArgs e)
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

                string jobid = txtjobid.Text.Trim();
                string seid = ddlseids.SelectedValue.Trim();

                int count = 0;
                string html = GetHtmlSource(jobid, seid, out string device, out string url, out string gl, out string keyword);

                //File.WriteAllText(Server.MapPath("~/files/Urllink.txt"), url);

                //  System.IO.File.WriteAllText()

                string html1 = "";
                //string keyword = GetKeywordFromUrl(url);
                File.WriteAllText(Server.MapPath("~/files/" + "Oxylabs_" + device + "_" + gl + "_" + jobid + "_" + keyword + ".html"), html);
                if (device == "desktop")
                    html1 = getWebDataSource(url);
                else
                    html1 = getWebDataMobileSource(url);

                var doc = new HtmlAgilityPack.HtmlDocument();

                File.WriteAllText(Server.MapPath("~/files/" + "Google_" + device + "_" + gl + "_" + keyword + ".html"), html1);
                //File.WriteAllText(Server.MapPath("~/files/" + seid + "_" + jobid + "_" + keyword + "_1.html"), html1);
                //File.WriteAllText(Server.MapPath("~/files/" + url + ".txt"), url);

                doc.LoadHtml(html);
                if (device == "desktop")
                {
                    Desktop clsdesktop = new Desktop();
                    string res = clsdesktop.ProcessDocument(seid, keyword, doc, out count);
                    XmlDocument doc1 = new XmlDocument();
                    doc1.LoadXml(res);
                    GenerateXml(seid, keyword, res, jobid, device, gl);
                    int Position = 0;

                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/files/"));
                    //string[] filePaths = Directory.GetFiles(Server.MapPath("~/RapidTracking/files/"));
                    List<ListItem> zipFiles = new List<ListItem>();
                    foreach (string file in filePaths)
                    {
                        zipFiles.Add(new ListItem(Path.GetFileName(file), file));
                    }
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                        //zip.AddDirectoryByName("zipFile");
                        foreach (ListItem s in zipFiles)
                        {
                            string filePath2 = Server.MapPath("~/files/" + s);
                            //zip.AddFile(filePath2,"" + device + "_" + jobid + "_" + keyword + "ZipFile");
                            zip.AddFile(filePath2, "" + device + "_" + jobid + "_" + keyword);
                        }
                        Response.Clear();
                        Response.BufferOutput = false;
                        //string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MM-dd-HHmmss"));
                        string zipName = String.Format("Zip_{0}.zip", device + "_" + gl + "_" + keyword); //string zipName = String.Format("Zip_{0}.zip", device + gl + "_" + keyword);
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                        zip.Save(Response.OutputStream);
                        Response.End();
                    }

                    XmlElement root = doc1.DocumentElement;
                    foreach (XmlNode n in root)
                    {
                        foreach (XmlNode node in n)
                        {
                            if (node.Name != "block")
                            {
                                Position = Position + 1;
                                dt.Rows.Add("", node.Attributes[0].Value, node.Attributes[1].Value, Position);
                            }
                            else
                            {
                                DataRow row11 = dt.NewRow();
                                dt.Rows.Add(node.Attributes[0].Value);
                                foreach (XmlNode nodee in node)
                                {
                                    dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                }
                            }
                            //resultscnt.Text = "" + Position.ToString();
                        }
                    }
                }
                else
                {
                    iOS clsios = new iOS();
                    string res = clsios.ProcessDocument(seid, keyword, doc, out count);
                    XmlDocument doc1 = new XmlDocument();
                    doc1.LoadXml(res);
                    GenerateXml(seid, keyword, res, jobid, device, gl);

                    string[] filePaths = Directory.GetFiles(Server.MapPath("~/files/"));
                    List<ListItem> zipFiles = new List<ListItem>();
                    foreach (string file in filePaths)
                    {
                        zipFiles.Add(new ListItem(Path.GetFileName(file), file));
                    }
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                        // zip.AddDirectoryByName("zipFile");

                        // List<ListItem> zipdata = new List<ListItem>();

                        foreach (ListItem s in zipFiles)
                        {
                            string filePath2 = Server.MapPath("~/files/" + s);
                            //zip.AddFile(filePath2,"" + device + "_" + jobid + "_" + keyword + "ZipFile");
                            zip.AddFile(filePath2, "" + device + "_" + jobid + "_" + keyword);
                            // zip.AddFile(filepath1, "files");
                            // zip.AddFile();
                        }
                        Response.Clear();
                        Response.BufferOutput = false;
                        string zipName = String.Format("Zip_{0}.zip", device + "_" + gl + "_" + keyword);
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                        zip.Save(Response.OutputStream);
                        Response.End();
                    }

                    XmlElement root = doc1.DocumentElement;
                    int Pos = 0;
                    foreach (XmlNode n in root)
                    {
                        foreach (XmlNode node in n)
                        {
                            if (node.Name != "block")
                            {
                                Pos = Pos + 1;
                                dt.Rows.Add("", node.Attributes[0].Value, node.Attributes[1].Value, Pos);
                            }
                            else
                            {
                                DataRow row11 = dt.NewRow();
                                dt.Rows.Add(node.Attributes[0].Value);
                                foreach (XmlNode nodee in node)
                                {
                                    dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                }
                            }
                        }
                        //resultscnt.Text = "" + Pos.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                //string yourValue = "Data not available";
                //Response.Write(String.Format("<script>alert('Data not available.Please enter Details');</script>", yourValue));
            }
        }

        private string GetHtmlSource(string jobId, string seid, out string device, out string url, out string gl, out string keyword)
        {
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";

            string resURL = "http://data.oxylabs.io/v1/queries/" + jobId + "/results";
            //string resURL = "https://data.oxylabs.io/v1/queries/" + jobId;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(resURL);
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream resStream = res.GetResponseStream();
            StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
            string response = reader.ReadToEnd();
            resStream.Close();
            res.Close();
            //string result = string.Empty;
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

                //if (device == "desktop")
                //    url = obj["results"][0]["url"].Value<string>();
                //else
                //    url = obj["results"][0]["url"].Value<string>() + "&gs_l=mobile-gws-serp.3.0";

                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Table()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Block Type");
            dt.Columns.Add("URL");
            dt.Columns.Add("Title");
            dt.Columns.Add("Position");
            return dt;
        }
        public void GenerateXml(string seid, string kw, string res, string jobid, string device, string gl)
        {
            string filepath = string.Empty;
            if (res == string.Empty)
            {
                XmlDocument xd = new XmlDocument();
                res = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                res += "<searchResult searchEngine =\"" + seid + "\" keyword=\"" + kw + "\" date =\"" + DateTime.Today.ToString("yyyy-MM-dd") + "\">";
                res += "<section col = \"main\" /> <section col=\"right\" /> </searchResult> ";
                xd.LoadXml(res);
            }
            else
            {
                XmlDocument xd = new XmlDocument();
                res = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + res;
                xd.LoadXml(res);
                //File.WriteAllText(Server.MapPath("~/files/" + seid + "_" + jobid + "_" + kw + ".xml"), res);
                File.WriteAllText(Server.MapPath("~/files/" + "Oxylabs_" + device + "_" + gl + "_" + jobid + "_" + kw + ".xml"), res);
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


    }
}