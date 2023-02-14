using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.Googletrends
{
    public partial class keywordsdata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                txtstartdate.Attributes.Add("ReadOnly", "ReadOnly");
                txtstartdate.Text = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                stdate.StartDate = DateTime.Now.AddYears(-1);
                txtenddate.Attributes.Add("ReadOnly", "ReadOnly");
                txtenddate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                endt.EndDate = DateTime.Now;
            }
            Label6.Visible = false;
            Label7.Visible = false;
            Label8.Visible = false;
            Label9.Visible = false;
        }

        public async Task<string> keywords_data_trends_explore_live(string[] keyword, string location)
        {
            string[] kwds = { txtkeyword.Text };
            Uri queryUri = new Uri("https://api.dataforseo.com/v3/keywords_data/google_trends/explore/live");
            string username = "hemachander@intelligentpositioning.com";
            string password = "19a90cf9a3f8a1e1";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            //string fromDate = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
            //string toDate = DateTime.Today.ToString("yyyy-MM-dd");

            string fromDate = txtstartdate.Text;
            string toDate = txtenddate.Text;

            var postData = new List<object>();
            postData.Add(new
            {
                location_name = ddllocation.SelectedItem,
                date_from = fromDate,
                date_to = toDate,
                type = "web",
                category_code = 0,
                keywords = kwds
            });
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();

            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authInfo);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(postData, new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                });

                streamWriter.Write(json);
            }
            string response = "";
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
                {
                    response = reader.ReadToEnd();
                    res.Close();
                    //return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(response);
        }

        protected void btngoogletrends_Click(object sender, EventArgs e)
        {
            //RegisterAsyncTask(new PageAsyncTask(keywords_data_trends_explore_live));

            try
            {
                string locations = ddllocation.SelectedItem.ToString();
                string kid = ddllocation.SelectedValue.ToString();
                string[] kwds = { txtkeyword.Text };
                if (locations != null)
                {
                    string res = keywords_data_trends_explore_live(kwds, ddllocation.SelectedValue.ToString()).Result;
                    ProcessData(res, kid, locations);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcessData(string result, string seid,string location)
        {
            try
            {
                JObject jo = JObject.Parse(result);
                var tasks = from p in jo["tasks"] select p;
                var res = tasks.FirstOrDefault()["result"];
                var items = res.FirstOrDefault()["items"];

                foreach (var item in items)
                {
                    var title = item["title"].Value<string>();
                    if (title == "Interest over time")
                    {
                        var iot = item["data"];
                        SaveOverTime(iot, seid, location);

                    }
                    else if (title == "Interest by subregion")
                    {
                        var ibs = item["data"];
                        SaveBySubregion(ibs, seid, location);
                    }
                    else if (title == "Related topics")
                    {
                        var rt = item["data"];
                        SaveRelatedTopics(rt, seid, location);
                    }
                    else if (title == "Related queries")
                    {
                        var rq = item["data"];
                        SaveRelatedQueries(rq, seid, location);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public string TimestampToDate(long timestamp)
        {
            DateTime tsDate = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
            return tsDate.ToString();
        }
        public void SaveOverTime(JToken iot, string kid, string seid)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("date_from");
                dt1.Columns.Add("date_to");
                //dt1.Columns.Add("timestamp");
                dt1.Columns.Add("missing_data");
                dt1.Columns.Add("value");

                foreach (var item in iot)
                {
                    ArrayList a = new ArrayList();
                    DataRow dr = dt1.NewRow();

                    var date_from = item["date_from"].Value<string>();
                    var date_to = item["date_to"].Value<string>();
                    //var date_from = txtstartdate.Text;
                    //var date_to = txtenddate.Text;
                    //var timestamp = item["timestamp"].Value<long>();
                    var missing_data = item["missing_data"].Value<bool>();
                    var value = item["values"][0].Value<string>();

                    //var tsDate = TimestampToDate(timestamp);

                    a.Add(date_from);
                    a.Add(date_to);
                    //a.Add(tsDate);
                    a.Add(missing_data);
                    a.Add(value);

                    for (int s = 0; s < a.Count; s++)
                    {
                        dr[s] = a[s];
                    }

                    dt1.Rows.Add(dr);

                    if (dt1.Rows.Count > 0)
                    {
                        Label6.Visible = true;
                        gvinterestot.DataSource = dt1;
                        gvinterestot.DataBind();
                    }
                    else
                    {
                        gvinterestot.DataSource = null;
                        gvinterestot.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SaveBySubregion(JToken ibs, string kid, string seid)
        {
            try
            {
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("geo_id");
                dt2.Columns.Add("geo_name");
                dt2.Columns.Add("value");
                dt2.Columns.Add("max_value_index");

                foreach (var item in ibs)
                {
                    ArrayList a = new ArrayList();
                    DataRow dr = dt2.NewRow();

                    var geo_id = item["geo_id"].Value<string>();
                    var geo_name = item["geo_name"].Value<string>();
                    var value = item["values"][0].Value<string>();
                    var max_value_index = item["max_value_index"].Value<int>();

                    a.Add(geo_id);
                    a.Add(geo_name);
                    a.Add(value);
                    a.Add(max_value_index);

                    for (int s = 0; s < a.Count; s++)
                    {
                        dr[s] = a[s];
                    }

                    dt2.Rows.Add(dr);
                    if (dt2.Rows.Count > 0)
                    {
                        Label7.Visible = true;
                        gvsubregion.DataSource = dt2;
                        gvsubregion.DataBind();
                    }
                    else
                    {
                        gvsubregion.DataSource = null;
                        gvsubregion.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveRelatedTopics(JToken rt, string kid, string seid)
        {
            try
            {
                DataTable dt3 = new DataTable();
                dt3.Columns.Add("topic_id");
                dt3.Columns.Add("topic_title");
                dt3.Columns.Add("topic_type");
                dt3.Columns.Add("type");
                dt3.Columns.Add("value");

                var top = rt["top"];
                var rising = rt["rising"];

                foreach (var item in top)
                {
                    ArrayList a = new ArrayList();
                    ArrayList a1 = new ArrayList();
                    DataRow dr = dt3.NewRow();

                    var topic_id = item["topic_id"].Value<string>();
                    var topic_title = item["topic_title"].Value<string>();
                    var topic_type = item["topic_type"].Value<string>();
                    var value = item["value"].Value<string>();
                    var type = "top";


                    a.Add(topic_id);
                    a.Add(topic_title);
                    a.Add(topic_type);
                    a.Add(type);
                    a.Add(value);

                    

                    for (int s = 0; s < a.Count; s++)
                    {
                        dr[s] = a[s];
                    }

                    dt3.Rows.Add(dr);
                    if (dt3.Rows.Count > 0)
                    {
                        Label8.Visible = true;
                        gvrelatedtopics.DataSource = dt3;
                        gvrelatedtopics.DataBind();
                    }
                    else
                    {
                        gvrelatedtopics.DataSource = null;
                        gvrelatedtopics.DataBind();
                    }
                }

                foreach (var item in rising)
                {
                    var topic_id = item["topic_id"].Value<string>();
                    var topic_title = item["topic_title"].Value<string>();
                    var topic_type = item["topic_type"].Value<string>();
                    var value = item["value"].Value<int>();
                    var type = "rising";

                    ArrayList a1 = new ArrayList();
                    DataRow dr1 = dt3.NewRow();

                    a1.Add(topic_id);
                    a1.Add(topic_title);
                    a1.Add(topic_type);
                    a1.Add(type);
                    a1.Add(value);
                    

                    for (int s = 0; s < a1.Count; s++)
                    {
                        dr1[s] = a1[s];
                    }

                    dt3.Rows.Add(dr1);
                    if (dt3.Rows.Count > 0)
                    {
                        Label8.Visible = true;
                        gvrelatedtopics.DataSource = dt3;
                        gvrelatedtopics.DataBind();
                    }
                    else
                    {
                        gvrelatedtopics.DataSource = null;
                        gvrelatedtopics.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveRelatedQueries(JToken rq, string kid, string seid)
        {
            try
            {
                DataTable dt4 = new DataTable();
                dt4.Columns.Add("query");
                dt4.Columns.Add("type");
                dt4.Columns.Add("value");
               

                var top = rq["top"];
                var rising = rq["rising"];

                foreach (var item in top)
                {
                    ArrayList a = new ArrayList();
                    DataRow dr = dt4.NewRow();

                    var query = item["query"].Value<string>();
                    var value = item["value"].Value<int>();
                    var type = "top";


                    a.Add(query);
                    a.Add(type);
                    a.Add(value);

                    for (int s = 0; s < a.Count; s++)
                    {
                        dr[s] = a[s];
                    }

                    dt4.Rows.Add(dr);
                    if (dt4.Rows.Count > 0)
                    {
                        Label9.Visible = true;
                        gvrelatedqueries.DataSource = dt4;
                        gvrelatedqueries.DataBind();
                    }
                    else
                    {
                        gvrelatedqueries.DataSource = null;
                        gvrelatedqueries.DataBind();
                    }
                }

                foreach (var item in rising)
                {
                    var query = item["query"].Value<string>();
                    var value = item["value"].Value<int>();
                    var type = "rising";

                    ArrayList a1 = new ArrayList();
                    DataRow dr1 = dt4.NewRow();

                    a1.Add(query);
                    a1.Add(type);
                    a1.Add(value);
                    

                    for (int s = 0; s < a1.Count; s++)
                    {
                        dr1[s] = a1[s];
                    }

                    dt4.Rows.Add(dr1);
                    if (dt4.Rows.Count > 0)
                    {
                        Label9.Visible = true;
                        gvrelatedqueries.DataSource = dt4;
                        gvrelatedqueries.DataBind();
                    }
                    else
                    {
                        gvrelatedqueries.DataSource = null;
                        gvrelatedqueries.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}