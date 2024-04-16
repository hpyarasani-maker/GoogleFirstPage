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
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string jobid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt111_Click(object sender, EventArgs e)
        {
            string seid = txtseids.Text;
            string keyword = txtkwd.Text;
            ArrayList list1 = new ArrayList();
            //GetProcessedLists();
            bool result = false;
            if (Page.IsValid)
            {
                try
                {
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    ArrayList alresult = GetHTML(keyword, Convert.ToInt32(seid));

                    foreach (string[] src in alresult)
                    {
                        JObject obj = JObject.Parse(src[1]);
                        string html = obj["results"][0]["content"].Value<string>();
                        string kwds = src[0];
                        //string html = src[1];
                        jobid = src[2];
                        string device = src[3];
                        //SendToDatabase(Convert.ToInt32(seid), kwds, jobid);
                        result = true;
                        doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(html);
                        string res = string.Empty;
                        int count = 0;
                        try
                        {
                            if (device == "desktop")
                            {
                                Desktop clsDesktop = new Desktop();
                                res = clsDesktop.ProcessDocument(seid, keyword, doc, out count);
                                XmlDocument xml = new XmlDocument();
                                xml.LoadXml(res);
                                XmlNodeList xnList1 = xml.SelectNodes("/searchResult/section/item/@url");
                                int itemcount = 1;
                                foreach (XmlNode xn1 in xnList1)
                                {
                                    list1.Add(xn1.InnerText);
                                }
                                ArrayList alRes = new ArrayList();
                                ArrayList myList = new ArrayList();
                                alRes = list1;
                                for (int i = 0; i < alRes.Count; i++)
                                {
                                    myList.Add(new mURL(itemcount++, alRes[i].ToString()));
                                }
                                gvtracking.DataSource = myList;
                                gvtracking.DataBind();
                            }
                            else
                            {
                                iOS clsiOS = new iOS();
                                res = clsiOS.ProcessDocument(seid, keyword, doc, out count);
                                XmlDocument xml = new XmlDocument();
                                xml.LoadXml(res);
                                XmlNodeList xnList1 = xml.SelectNodes("/searchResult/section/item/@url");
                                foreach (XmlNode xn1 in xnList1)
                                {
                                    list1.Add(xn1.InnerText);
                                }
                                int itemcount = 1;
                                ArrayList alRes = new ArrayList();
                                ArrayList myList = new ArrayList();
                                alRes = list1;
                                for (int i = 0; i < alRes.Count; i++)
                                {
                                    myList.Add(new mURL(itemcount++, alRes[i].ToString()));
                                }
                                gvtracking.DataSource = myList;
                                gvtracking.DataBind();
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }

                    //if (Page.IsValid)
                    //{
                    //    int count = 1;
                    //    ArrayList myList = new ArrayList();
                    //    //foreach (var a in list1)
                    //    //{
                    //        DataTable dt = Table();

                    //        foreach (string[] s in list1)
                    //        {
                    //            //sb.Append("<item url=\"" + s[0] + "\" missedIn=\"" + s[1] + "\" />");
                    //            DataRow row = dt.NewRow();
                    //            dt.Rows.Add(s[0]);
                    //        }
                    //        //grd11.DataSource = dt;
                    //        //grd11.DataBind();
                    //        //ArrayList alRes = a.Value;
                    //        //for (int i = 0; i < alRes.Count; i++)
                    //        //{
                    //        //    myList.Add(new mURL(alRes[i].ToString(), count++));
                    //        //}
                    //        //oxylabsjobid.Text = "" + jobid.Trim();
                    //        //resultscnt.Text = "" + alRes.Count;
                    //    //}
                    //    gvtracking.DataSource = dt;
                    //    gvtracking.DataBind();
                    //}
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }

        }

        List<ArrayList> GetProcessedLists()
        {
            string xml1 = Server.MapPath(@"\files\selector.xml");
            string xml2 = Server.MapPath(@"\files\regex.xml");

            List<ArrayList> al = new List<ArrayList>();
            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();
            ArrayList missedList = new ArrayList();


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
                Binddata(missedList);
            al.Add(list1);
            al.Add(list2);
            //Dictionary<ArrayList, ArrayList> dict = new Dictionary<ArrayList,ArrayList>();

            //dict.Add(list2,missedList);
            //if (Page.IsValid)
            //{
            //    int count = 1;
            //    ArrayList myList = new ArrayList();
            //    foreach (KeyValuePair<ArrayList, ArrayList> kvp in dict)
            //    {
            //        ArrayList alRes = kvp.Value;
            //        for (int i = 0; i < alRes.Count; i++)
            //        {
            //            myList.Add(new mURL(alRes[i].ToString(),count++));
            //        }
            //    }

            //    if (list1.Count < list2.Count)
            //    {
            //        lblclcount.Text = "Missing classic links in selector";
            //    }
            //    else if(list2.Count < list1.Count)
            //    {
            //        lblclcount.Text = "Missing classic links in Regex";
            //    }
            //    grd11.DataSource = myList;
            //    grd11.DataBind();
            //}
            return al;
        }

        public void Binddata(ArrayList missedList)
        {
            DataTable dt = Table();

            foreach (string[] s in missedList)
            {
                //sb.Append("<item url=\"" + s[0] + "\" missedIn=\"" + s[1] + "\" />");
                DataRow row = dt.NewRow();
                dt.Rows.Add(s[0],s[1]);

            }
            //grd11.DataSource = dt;
            //grd11.DataBind();
        }

        public DataTable Table()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("URL");
            //dt.Columns.Add("Missing from");
            return dt;
        }

        private void SaveToXml(ArrayList missedList)
        {
            string kwd = "jabra wireless earphones";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.Append("<searchResult searchEngine=\"" + 1 + "\" keyword=\"" + System.Net.WebUtility.HtmlEncode(kwd) + "\" date=\"" + DateTime.Today.ToString("yyyy-MM-dd") + "\" >");
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
        public class mURL
        {
            private string url;
            private int position;
            public mURL(int position, string url)
            {
                this.position = position;

                this.url = url;
            }
            public int Position { get { return position; } }

            public string URL { get { return url; } }
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
            string[] keyword = { sp.query };

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = sp.domain,
                //query = sp.query.Split(','),
                query = keyword,
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
                using (StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
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
                string jobid = link["id"].Value<string>();
                string device = link["user_agent_type"].Value<string>();
                string[] s = { kw, href, status, "no", jobid, device };    // keyword, url, status, isdownloaded, jobid, device.
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
                    //Uri uri = new Uri("http://data.oxylabs.io/v1/queries/6707077494169666561/results");
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
                            using (StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
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


    }
}