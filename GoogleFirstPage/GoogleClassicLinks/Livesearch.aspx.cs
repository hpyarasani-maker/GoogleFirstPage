using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.GoogleClassicLinks
{
    public partial class Livesearch : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;

        public string resp1 = string.Empty;
        public string result1 = string.Empty;
        public string jobid = string.Empty;

        //HTMLParserNewTask WOWS = new HTMLParserNewTask();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["allsearchengines"].ToString());
        string connection1 = ConfigurationManager.ConnectionStrings["allelements"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            error_lbl.Text = "";
            //if (!Page.IsPostBack) { }


            if (!Page.IsPostBack)
            {
                oxydiv1.Visible = false;
                Label1.Visible = false;

                cmd = new SqlCommand("GetSearchEngines", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Kname", SqlDbType.NVarChar).Value = kname;
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                searchEngines.DataTextField = "name";
                searchEngines.DataValueField = "seid";
                searchEngines.DataSource = dt;
                searchEngines.DataBind();
                //searchEngines.Items.Insert(0, "----Select----");
                searchEngines.SelectedItem.Value.ToString();
                //ddlkeyword.SelectedValue = "2";
                searchEngines.SelectedIndex = 0;
            }
        }

        protected void btnlivesearch_Click(object sender, EventArgs e)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            oxydiv1.Visible = true;
            Label1.Visible = true;

            string sIP = string.Empty;

            string seid = searchEngines.SelectedItem.Value;
            string keyword = textsearchbox.Text;
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
                        SendToDatabase(Convert.ToInt32(seid), kwds, jobid);
                        result = true;
                        doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(html);
                        string res = string.Empty;
                        if (device == "desktop")
                        {
                            dict = DesktopPattern(html, keyword);
                        }
                        else
                        {
                            dict = MobilePattern(html, keyword);
                        }
                    }

                    if (Page.IsValid)
                    {
                        int count = 1;
                        ArrayList myList = new ArrayList();
                        foreach (KeyValuePair<string, ArrayList> kvp in dict)
                        {
                            ArrayList alRes = kvp.Value;
                            for (int i = 0; i < alRes.Count; i++)
                            {
                                myList.Add(new mURL(alRes[i].ToString(), count++));
                            }
                            oxylabsjobid.Text = "" + jobid.Trim();
                            resultscnt.Text = "" + alRes.Count;
                        }
                        gvtracking.DataSource = myList;
                        gvtracking.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }

        }

        protected void textsearchbox_TextChanged(object sender, EventArgs e)
        {
            gvtracking.DataSource = null;
            gvtracking.DataBind();
        }

        private void SendToDatabase(int seid, string keyword, string jobid)
        {
            string query = "";
            string myDate = DateTime.Today.ToString("yyyy-MM-dd");
            SqlCommand comm;
            try
            {
                using (SqlConnection con = new SqlConnection(connection1))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    query = "INSERT INTO dbo.LiveSearchCount (Date,seid,keyword,jobid) VALUES (@date,@seid,@keyword,@jobid)";
                    comm = new SqlCommand(query, con);
                    comm.Parameters.Add("@date", SqlDbType.DateTime).Value = myDate;
                    comm.Parameters.Add("@seid", SqlDbType.Int).Value = seid;
                    comm.Parameters.Add("@keyword", SqlDbType.NVarChar).Value = keyword;
                    comm.Parameters.Add("@jobid", SqlDbType.NVarChar).Value = jobid.Trim();
                    comm.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                string error = ex.Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvtracking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
        }

        public class mURL
        {
            private string url;
            private int position;
            public mURL(string url, int position)
            {
                this.url = url;
                this.position = position;
            }
            public string URL { get { return url; } }
            public int Position { get { return position; } }
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
                HttpWebResponse res = (HttpWebResponse) req.GetResponse();
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

        private Dictionary<string, ArrayList> MobilePattern(string htmlsource, string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            var doc = new HtmlAgilityPack.HtmlDocument();
            string html;

            //foreach (string[] src in htmlsource)
            //{
            try
            {
                //keyword = src[0] + ":" + src[2];

                html = htmlsource.Replace(@"\", "");
                doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                ArrayList alDup = new ArrayList();
                ArrayList googleList = new ArrayList();
                //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//a[@class='C8nzq']|//a[@class='Rk4fgb']|//div[@id='rso']/div/div/div/a[1]|//a[@class='JTuIPc']|//a[@class='C8nzq BmP5tf']|//a[@class='BmP5tf']|//a[@class='sXtWJb']|//a[@class='C8nzq BmP5tf amp_r']|//div[@jsl='$t t-4cfX2GiP_Fk;$x 0;']/a|//g-link[not(contains(@class,'fl'))]/a");
                //HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//div[@class='ZINbbc xpd']/div/a|.//div[@class='ZINbbc xpd']/div[1]/a|.//a[@class='C8nzq JTuIPc amp_r']|.//a[@class='C8nzq JTuIPc']|.//a[@class='C8nzq BmP5tf amp_r']|.//a[@class='C8nzq Tj0U2 BmP5tf']|.//a[@class='C8nzq BmP5tf']|.//a[@class='C8nzq Tj0U2 BmP5tf amp_r']|.//a[@class='sXtWJb amp_r']|.//g-link/a|.//div[@class='fM8c FUksre']/a|.//div[@class='ytwLQd']|.//div[@class='rc']|.//h3[@class='r']/a|.//h3[@class='r']/div/a|.//h3[contains(@class,'yuRUbf JtG40d')]/a");
                //HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//div[@class='ZINbbc xpd']/div/a|.//div[@class='ZINbbc xpd']/div[1]/a|.//a[@class='C8nzq JTuIPc amp_r']|.//a[@class='C8nzq JTuIPc']|.//a[@class='C8nzq BmP5tf amp_r']|.//a[@class='C8nzq Tj0U2 BmP5tf']|.//a[@class='C8nzq BmP5tf']|.//a[@class='C8nzq Tj0U2 BmP5tf amp_r']|.//a[@class='sXtWJb amp_r']|.//g-link/a|.//div[@class='fM8c FUksre']/a|.//div/a|.//div[@class='rc']|.//div[@class='ytwLQd']|.//h3[@class='r']/a|.//h3[@class='r']/div/a|.//h3[contains(@class,'yuRUbf JtG40d')]/a");
                HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//a[@class='C8nzq JTuIPc amp_r']|.//a[@class='C8nzq JTuIPc']|.//a[@class='C8nzq BmP5tf']|.//a[@class='C8nzq Tj0U2 BmP5tf']|.//a[@class='C8nzq BmP5tf amp_r']|.//a[@class='C8nzq Tj0U2 BmP5tf amp_r']|.//a[@class='sXtWJb amp_r']|.//div[@class='ytwLQd']|.//div[@class='rc']|.//h3[@class='r']/a|.//h3[@class='r']/div/a|.//h3[contains(@class,'yuRUbf JtG40d')]/a|.//a[@class='cz3goc BmP5tf']|.//div[@class='tKdlvb jqWpsc']|.//div[@class='mnr-c luh4tb xpd O9g5cc uUPGi']|.//div[@class='g mnr-c']|.//a[@class='cz3goc BmP5tf amp_r']");
                //HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//div[@class='mnr-c xpd O9g5cc uUPGi']");



                //System.IO.File.WriteAllText(@"c:\inetput\wwwroot\carleasing.html", html);

                foreach (HtmlNode links in node)
                {
                    try
                    {
                        string url = links.Attributes["href"].Value;
                        url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");
                        //if (string.IsNullOrEmpty(url.Trim())) return string.Empty;
                        //29-09-2020            
                        if (url.IndexOf("https://") == 0 || url.IndexOf("https://") >= 0) //01-10-2020
                            url = url.Remove(0, url.IndexOf("https://"));
                        else if (url.IndexOf("http://") == 0 || url.IndexOf("http://") >= 0) //14-10-2020 included indexof for http)
                            url = url.Remove(0, url.IndexOf("http://"));
                        //end 29-09-2020

                        Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
                        if (!rx.Match(url).Success && !url.Contains("/aclk?"))
                            if (!url.Contains("://")) // 30-04-2020
                                url = "http://" + url;

                        if (url.StartsWith("http:////") || url.StartsWith("https:////")) //18-09-2020 condition applied if appears http:////
                            url = url.Replace("////", "//").Replace("///", "//"); //18-09-2020

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

                        if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && (!url.Contains("/aclk?") && !url.Contains("///search?") && !url.Contains("search?num=100") && !url.Contains("?sa=X") && !url.Contains("http://#") && !url.Contains("www.google.") && !url.Contains("maps.google.") && !url.Contains("https://www.google.com/maps")))

                            if (url.StartsWith("http") || url.StartsWith("https") || !url.Contains("https://www.google.com/maps"))
                            {
                                url = GetRedirectedUrl(WebUtility.HtmlDecode(url).Trim());
                                if (url.ToLower().Contains("%2f") || url.ToLower().Contains("%2e"))
                                    url = GetRedirectedUrl(WebUtility.UrlDecode(WebUtility.HtmlDecode(url)).Trim());
                                alDup.Add(WebUtility.HtmlDecode(url.Replace("\x00", "%00").Replace("|", "%7C").Replace("^", "%5E").Replace("\\\\u003d", "=").Replace('\u0002', ' ').Replace('\u0018', ' ').Replace('\f', ' ').Trim()));
                            }

                    }
                    catch { continue; }
                }

                foreach (string s in alDup)
                {
                    if (googleList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                    googleList.Add(s);
                }

                if (googleList.Count > 100)
                {
                    googleList.RemoveRange(100, googleList.Count - 100);
                }
                if (googleList.Count > 0)
                    dict.Add(keyword, googleList);
            }
            catch
            {
                //continue;
            }
            //}

            return dict;
        }


        public Dictionary<string, ArrayList> DesktopPattern(string htmlsource, string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            var doc = new HtmlAgilityPack.HtmlDocument();
            string html = "";
            //foreach (string[] src in htmlsource)
            // {
            try
            {
                //keyword = src[0] + ":" + src[2];
                html = htmlsource.Replace(@"\", "");
                doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                ArrayList alDup = new ArrayList();
                ArrayList googleList = new ArrayList();
                //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//div[@class='r']/a[1]|//div[1]/div/h3/div/g-link/a|//div[1]/div/h3/g-link/a");
                //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//div[(contains(@class, 'srg'))]//div[@class='r']/a[1]|//div[1]/div/h3/div/g-link/a|//div[1]/div/h3/g-link/a");
                //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//div[@class='srg']//div[@class='r']/a[1]|//div[@class='bkWMgd']/div[@class='g']//div[@class='r']/a[1]|//div[1]/div/h3/div/g-link/a|//div[1]/div/h3/g-link/a");
                //[not(contains(@class, 'fl'))]    
                //HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//div[@class='r']/a|.//div[@class='rc']/div/a");
                //HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//h3[@class='r']/a|.//div[@class='r']/a|.//div[@class='r']/div/a|.//div[@class='yuRUbf']/a|.//g-link/a|.//h3[@class='r dO0Ag']/a|.//div[@class='yuRUbf']/a|.//div[@class='zTpPx']/g-link/a");
                HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//div[@class='yuRUbf']/a|.//div[@class='yuRUbf']/div/a|.//div/div[@class='g jNVrwc Y4pkMc']");

                foreach (HtmlNode links in node)
                {
                    try
                    {
                        string url = links.Attributes["href"].Value;
                        url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");
                        //if (string.IsNullOrEmpty(url.Trim())) return string.Empty;
                        //29-09-2020            
                        if (url.IndexOf("https://") == 0 || url.IndexOf("https://") >= 0) //01-10-2020
                            url = url.Remove(0, url.IndexOf("https://"));
                        else if (url.IndexOf("http://") == 0 || url.IndexOf("http://") >= 0) //14-10-2020 included indexof for http)
                            url = url.Remove(0, url.IndexOf("http://"));
                        //end 29-09-2020

                        Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
                        if (!rx.Match(url).Success && !url.Contains("/aclk?"))
                            if (!url.Contains("://")) // 30-04-2020
                                url = "http://" + url;

                        if (url.StartsWith("http:////") || url.StartsWith("https:////")) //18-09-2020 condition applied if appears http:////
                            url = url.Replace("////", "//").Replace("///", "//"); //18-09-2020

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

                        if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && (!url.Contains("/aclk?") && !url.Contains("///search?") && !url.Contains("search?num=100") && !url.Contains("?sa=X")))

                            if (url.StartsWith("http") || url.StartsWith("https"))
                            {
                                url = GetRedirectedUrl(WebUtility.HtmlDecode(url).Trim());
                                if (url.ToLower().Contains("%2f") || url.ToLower().Contains("%2e"))
                                    url = GetRedirectedUrl(WebUtility.UrlDecode(WebUtility.HtmlDecode(url)).Trim());
                                alDup.Add(WebUtility.HtmlDecode(url.Replace("\x00", "%00").Replace("|", "%7C").Replace("^", "%5E").Replace("\\\\u003d", "=").Replace('\u0002', ' ').Replace('\u0018', ' ').Replace('\f', ' ').Trim()));
                            }

                    }
                    catch { continue; }
                }
                foreach (string s in alDup)
                {
                    if (googleList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                    googleList.Add(s);
                }

                if (googleList.Count > 100)
                {
                    googleList.RemoveRange(100, googleList.Count - 100);
                }
                if (googleList.Count > 0)
                    dict.Add(keyword, googleList);
            }
            catch
            {
                //continue;
            }
            //}
            return dict;
        }  //DesktopPattern


        private string GetRedirectedUrl(string url)
        {
            //21-11-2020
            url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");
            if (string.IsNullOrEmpty(url.Trim())) return string.Empty;

            if (url.LastIndexOf("https://") > 0)
                url = url.Remove(0, url.LastIndexOf("https://"));
            if (url.LastIndexOf("http://") > 0)
                url = url.Remove(0, url.LastIndexOf("http://"));

            Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
            if (!rx.Match(url).Success && !url.Contains("/aclk?"))
                if (!url.Contains("://"))
                    url = "http://" + url;

            if (url.Contains("&amp;grqid="))
                url = url.Remove(url.IndexOf("&amp;grqid="));

            //  13-12-2020
            if (url.Contains("&grqid="))
                url = url.Remove(url.IndexOf("&grqid="));

            if (url.Contains("\0"))
                url = url.Replace("\0", "%00");

            if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && (!url.Contains("/aclk?") && !url.Contains("search?num=100")))
                return url;

            return string.Empty;

        }


    }
}