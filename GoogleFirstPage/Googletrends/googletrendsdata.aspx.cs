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
using System.Drawing;
using System.Globalization;

namespace GoogleFirstPage.Googletrends
{
    public partial class keywordsdata : System.Web.UI.Page
    {
        
        string volumedata = string.Empty;
        string searchres = string.Empty;
        List<string> allDates;
        List<string> lDates;
        List<string> myVolume;
        


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtstartdate.Attributes.Add("ReadOnly", "ReadOnly");
                txtstartdate.Text = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                //stdate.StartDate = DateTime.Now.AddYears(-1);
                txtenddate.Attributes.Add("ReadOnly", "ReadOnly");
                txtenddate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                endt.EndDate = DateTime.Now;
            }
            Label6.Visible = false;
            Label7.Visible = false;
            Label8.Visible = false;
            Label9.Visible = false;
            btnCSV.Enabled = false;
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

        public async Task<string> GetSearchVolumeResponse(string[] keyword, string location)
        {
            string[] kwds = { txtkeyword.Text };
            Uri queryUri = new Uri("https://api.dataforseo.com/v3/keywords_data/google_ads/search_volume/live");
            string username = "hemachander@intelligentpositioning.com";
            string password = "19a90cf9a3f8a1e1";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            var postData = new List<object>();
            postData.Add(new
            {
                location_name = ddllocation.SelectedItem,
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
                    searchres = GetSearchVolumeResponse(kwds, ddllocation.SelectedValue.ToString()).Result;
                    ProcessData(res, kid, locations);
                    //ProcessSVData(searchres, kid, locations);
                }
                btnCSV.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcessData(string result, string seid, string location)
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
            DataTable dt1 = new DataTable();

            try
            {
                ArrayList a = new ArrayList();

                dt1.Columns.Add(new DataColumn("date_from", typeof(string)));
                dt1.Columns.Add(new DataColumn("date_to", typeof(string)));
                dt1.Columns.Add(new DataColumn("MontlyVolume", typeof(string)));
                dt1.Columns.Add(new DataColumn("WeeklyVolume", typeof(string)));
                dt1.Columns.Add(new DataColumn("value", typeof(string)));
                dt1.Columns.Add(new DataColumn("WeeklyVolProp", typeof(string)));
                dt1.Columns.Add(new DataColumn("WeeklyVolDiff", typeof(string)));

                var date_from = "";
                var date_to = "";
                var value = "";
                DataRow dr;

                allDates = GetAlldates(iot);
                lDates = GetLastDates(allDates).ToList();
                myVolume = GetSearchVolume(searchres);
                int total = myVolume.Sum(x => Convert.ToInt32(x));
                Dictionary<string, string> res = GetDicVolumeData(lDates, myVolume);
                Dictionary<string, string> weeklyVolRes = GetWeeklyVolume(res, allDates);

                double prevVal = -1;

                foreach (var item in iot)
                {
                    dr = dt1.NewRow();

                    date_from = item["date_from"].Value<string>();
                    date_to = item["date_to"].Value<string>();
                    value = item["values"][0].Value<string>();
                    dr["date_from"] = date_from;
                    dr["date_to"] = date_to;
                    dr["MontlyVolume"] = null;

                    foreach (var x in res)
                    {
                        string lastmdate = "";
                        string volumedata = "";

                        for (int i = 0; i < lDates.Count; i++)
                        {
                            lastmdate = x.Key;
                            volumedata = x.Value;

                            if (date_to == lastmdate.ToString())
                            {
                                dr["MontlyVolume"] = volumedata;
                            }
                        }
                    }

                    string weeklyVol = string.Empty;
                    weeklyVolRes.TryGetValue(date_to, out weeklyVol);
                    var duration = weeklyVolRes.Count;
                    var volDiff = GetWeeklyVolumeDiff(Convert.ToDouble(weeklyVol), Convert.ToDouble(value), prevVal, duration);
                    prevVal = Convert.ToDouble(value);

                    dr["WeeklyVolume"] = weeklyVol;
                    dr["value"] = value;
                    if (volDiff != null)
                    {
                        dr["WeeklyVolProp"] = volDiff[0];
                        dr["WeeklyVolDiff"] = volDiff[1];
                    }
                    dt1.Rows.Add(dr);
                }

                if (dt1.Rows.Count > 0)
                {
                    gvinterestot.DataSource = dt1;
                    gvinterestot.DataBind();

                    gvinterestot.FooterRow.Cells[1].Text = "Total Volume = ";
                    gvinterestot.FooterRow.Cells[1].Font.Bold = true;
                    gvinterestot.FooterRow.Cells[2].Text = total.ToString();
                }
                else
                {
                    gvinterestot.DataSource = null;
                    gvinterestot.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        private string[] GetWeeklyVolumeDiff(double vol, double curVal, double prevVal, int duration )
        {
            if(prevVal == -1)
            {
                return null;
            }

            string[] res = { string.Empty, string.Empty };            
                       
            var cVol = Math.Round(vol * (curVal / 100f), 3);
            var pVol = Math.Round(vol * (prevVal / 100f), 3);

            var prop = Math.Round(((cVol - pVol) / pVol) * duration, 3);
            //var prop = prevVal - curVal;

            var volDiff = Math.Round(vol * (prop / 100f),0);

            res[0] = prop.ToString();
            res[1] = volDiff.ToString();
            return res;

        }      

        private Dictionary<string, string> GetWeeklyVolume(Dictionary<string, string> res, List<string> allDates)
        {
            Dictionary<string, string> wRes = new Dictionary<string, string>();

            foreach (var r in res)
            {
                string mDate = r.Key;
                string rvolume = r.Value;
                var weeks = allDates.Where(d => DateTime.Parse(d).ToString("yyyy-MM") == DateTime.Parse(mDate).ToString("yyyy-MM"));
                foreach (var w in weeks)
                {
                    string wr = Math.Round(Convert.ToDouble(rvolume) / weeks.Count(), 0).ToString();
                    wRes.Add(w, wr);
                }
            }
            return wRes;
        }

        public List<string> GetSearchVolume(string json)
        {
            List<string> myList = new List<string>();
            JObject jo = JObject.Parse(searchres);
            var tasks = from p in jo["tasks"] select p;
            var res = tasks.FirstOrDefault()["result"];
            var monthly = res.FirstOrDefault()["monthly_searches"];
            foreach (var mm in monthly)
            {
                myList.Add(mm["search_volume"].Value<string>());
            }
            myList.Add(GetNumberofDays(int.Parse(myList[0])));
            return myList;
        }

        public List<string> GetAlldates(JToken jt)
        {
            List<string> dts = new List<string>();

            foreach (var item in jt)
            {
                //dts.Add(item["date_from"].Value<string>());
                dts.Add(item["date_to"].Value<string>());
            }
            return dts;
        }

        public Dictionary<string, string> GetDicVolumeData(List<string> date, List<string> volume)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < date.Count; i++)
                dict.Add(date[i], volume[i]);
            return dict;
        }

        public List<string> GetLastDates(List<string> mydate1)
        {
            List<string> myList = new List<string>();
            List<DateTime> dates = mydate1.Select(date => DateTime.Parse(date)).ToList();
            dates.Sort();

            var groupdates = dates.GroupBy(x => new { MatchDates = x.Month + "-" + x.Year }).Select(x => x.Max(s => s.Date));

            foreach (var items in groupdates)
            {
                myList.Add(items.ToString("yyyy-MM-dd"));
            }
            return myList;
        }


        public string GetNumberofDays(int vm)
        {
            int d = vm / DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) * DateTime.Now.Day;
            return d.ToString();
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

        
        protected void gvinterestot_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        DateTime dtRow = DateTime.ParseExact(e.Row.Cells[1].Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //        string curDate = DateTime.Now.ToString("yyyy-MM-dd");
            //        int result = DateTime.Compare(dtRow, DateTime.Parse(curDate));


            //        if (result < 0)
            //        {
            //            e.Row.Cells[1].BackColor = Color.Red;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void btnCSV_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                string a = "GoogletrendsData" + "_" + txtkeyword.Text + "_" + ddllocation.SelectedItem.Text + ".csv";
                Response.AddHeader("content-disposition", "attachment;filename=" + a);
                Response.Charset = "";
                Response.ContentType = "text/csv";

                gvinterestot.AllowPaging = false;                

                StringBuilder sb = new StringBuilder();

                foreach (TableCell cell in gvinterestot.HeaderRow.Cells)
                {
                    sb.Append(cell.Text + ',').Replace("&nbsp;", " ");
                }
                sb.Append("\r\n");

                foreach (GridViewRow row in gvinterestot.Rows)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        sb.Append(cell.Text + ',').Replace("&nbsp;", " ");
                    }
                    sb.Append("\r\n");
                }

                foreach (TableCell cell in gvinterestot.FooterRow.Cells)
                {
                    sb.Append(cell.Text + ',').Replace("&nbsp;", " ");
                }
                sb.Append("\r\n");

                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}