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

namespace GoogleFirstPage.Googletrends
{
    public class TrendsData
    {
        public async Task<string> keywords_data_trends_explore_live(string keyword, string stDate, string endDate, string location)
        {
            string[] kwds = { keyword };
            Uri queryUri = new Uri("https://api.dataforseo.com/v3/keywords_data/google_trends/explore/live");
            string username = "hemachander@intelligentpositioning.com";
            string password = "19a90cf9a3f8a1e1";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            string fromDate = stDate;
            string toDate = endDate;

            var postData = new List<object>{
                new {
                    location_name = location,
                    date_from = fromDate,
                    date_to = toDate,
                    type = "web",
                    category_code = 0,
                    keywords = kwds
                }
            };
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
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(response);
        }

        public async Task<string> GetSearchVolumeResponse(string keyword, string location)
        {
            string[] kwds = { keyword };
            Uri queryUri = new Uri("https://api.dataforseo.com/v3/keywords_data/google_ads/search_volume/live");
            string username = "hemachander@intelligentpositioning.com";
            string password = "19a90cf9a3f8a1e1";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            var postData = new List<object>{
                new {
                    location_name = location,
                    keywords = kwds
                }
            };
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
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult(response);
        }

        public DataTable GetOverTime(JToken iot, string searches)
        {
            DataTable dt1 = new DataTable("OverTime");

            try
            {
                ArrayList a = new ArrayList();

                dt1.Columns.Add(new DataColumn("DateFrom", typeof(string)));
                dt1.Columns.Add(new DataColumn("DateTo", typeof(string)));
                dt1.Columns.Add(new DataColumn("MontlyVolume", typeof(string)));
                dt1.Columns.Add(new DataColumn("WeeklyVolume", typeof(string)));
                dt1.Columns.Add(new DataColumn("Value", typeof(string)));
                dt1.Columns.Add(new DataColumn("WeeklyVolProp", typeof(string)));
                dt1.Columns.Add(new DataColumn("WeeklyVolDiff", typeof(string)));

                var date_from = "";
                var date_to = "";
                var value = "";
                DataRow dr;

                List<string> allDates = GetAlldates(iot);
                List<string> lDates = GetLastDates(allDates).ToList();
                List<string> myVolume = GetSearchVolume(searches);
                int total = myVolume.Sum(x => Convert.ToInt32(x));
                Dictionary<string, string> res = GetDicVolumeData(lDates, myVolume);
                var weeklyVol = total / 52;
                Dictionary<string, string> weeklyVolRes = GetWeeklyVolume(res, allDates, iot, weeklyVol);

                double prevVal = -1;

                foreach (var item in iot)
                {
                    dr = dt1.NewRow();

                    date_from = item["date_from"].Value<string>();
                    var days = (Convert.ToDateTime(item["date_to"].Value<string>()) - Convert.ToDateTime(item["date_from"].Value<DateTime>())).Days;
                    if (days > 6)
                        date_to = Convert.ToDateTime(item["date_to"].Value<string>()).AddDays(6 - days).ToString("yyyy-MM-dd");
                    else
                        date_to = item["date_to"].Value<string>();

                    value = item["values"][0].Value<string>();
                    dr["DateFrom"] = date_from;
                    dr["DateTo"] = date_to;
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

                    var duration = 52;
                    var volDiff = GetWeeklyVolumeDiff(Convert.ToDouble(weeklyVol), Convert.ToDouble(value), prevVal, duration);
                    prevVal = Convert.ToDouble(value);

                    dr["WeeklyVolume"] = weeklyVol;
                    dr["Value"] = value;
                    if (volDiff != null)
                    {
                        dr["WeeklyVolProp"] = volDiff[0];
                        dr["WeeklyVolDiff"] = volDiff[1];
                    }
                    dt1.Rows.Add(dr);
                }

                return dt1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetBySubregion(JToken ibs)
        {
            try
            {
                DataTable dt2 = new DataTable("Subregion");
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
                }

                return dt2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRelatedTopics(JToken rt)
        {
            try
            {
                DataTable dt3 = new DataTable("RelatedTopics");
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
                }

                return dt3;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRelatedQueries(JToken rq)
        {
            try
            {
                DataTable dt4 = new DataTable("RelatedQueries");
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
                }
                return dt4;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetAlldates(JToken jt)
        {
            List<string> dts = new List<string>();

            foreach (var item in jt)
            {
                string dt;
                var days = (Convert.ToDateTime(item["date_to"].Value<string>()) - Convert.ToDateTime(item["date_from"].Value<DateTime>())).Days;
                if (days > 6)
                    dt = Convert.ToDateTime(item["date_to"].Value<string>()).AddDays(6 - days).ToString("yyyy-MM-dd");
                else
                    dt = item["date_to"].Value<string>();

                dts.Add(dt);
            }
            return dts;
        }

        public List<string> GetLastDates(List<string> mydate1)
        {
            List<string> myList = new List<string>();
            List<DateTime> dates = mydate1.Select(date => DateTime.Parse(date)).ToList();
            dates.Sort();

            var groupdates = dates.GroupBy(x => new { MatchDates = x.Month + "-" + x.Year }).Select(x => x.Min(s => s.Date));

            foreach (var items in groupdates)
            {
                myList.Add(items.ToString("yyyy-MM-dd"));
            }
            return myList;
        }

        public List<string> GetSearchVolume(string searches)
        {
            List<string> myList = new List<string>();
            JObject jo = JObject.Parse(searches);
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

        public Dictionary<string, string> GetDicVolumeData(List<string> date, List<string> volume)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < date.Count; i++)
                dict.Add(date[i], volume[i]);
            return dict;
        }

        private Dictionary<string, string> GetWeeklyVolume(Dictionary<string, string> res, List<string> allDates, JToken iot, int weeklyVol)
        {
            Dictionary<string, string> wRes = new Dictionary<string, string>();

            foreach (var r in res)
            {
                string mDate = r.Key;
                var weeks = allDates.Where(d => DateTime.Parse(d).ToString("yyyy-MM") == DateTime.Parse(mDate).ToString("yyyy-MM"));
                var swVol = 0.0;

                foreach (var w in weeks)
                {
                    string value = string.Empty;
                    foreach (var item in iot)
                    {
                        string date_to;
                        var days = (Convert.ToDateTime(item["date_to"].Value<string>()) - Convert.ToDateTime(item["date_from"].Value<DateTime>())).Days;
                        if (days > 6)
                            date_to = Convert.ToDateTime(item["date_to"].Value<string>()).AddDays(6 - days).ToString("yyyy-MM-dd");
                        else
                            date_to = item["date_to"].Value<string>();
                        if (w != date_to)
                            continue;
                        value = item["values"][0].Value<string>();
                        break;
                    }
                    string wr = Math.Round(Convert.ToDouble(weeklyVol) * Convert.ToDouble(value) / 100 / 7, 0).ToString();
                    swVol += Convert.ToDouble(wr);
                    wRes.Add(w, wr);
                }

                var remVol = Convert.ToDouble(weeklyVol) - swVol;
                if (remVol > 0)
                {
                    remVol /= weeks.Count();

                    foreach (var w in weeks)
                    {
                        foreach (var rs in wRes)
                        {
                            string date = rs.Key;
                            string volume = rs.Value;
                            if (date == w)
                            {
                                wRes[date] = Math.Round(Convert.ToDouble(volume) + remVol, 0).ToString();
                                break;
                            }
                        }
                    }
                }
            }
            return wRes;
        }

        private string[] GetWeeklyVolumeDiff(double vol, double curVal, double prevVal, int duration)
        {
            if (prevVal == -1)
            {
                return null;
            }

            string[] res = { string.Empty, string.Empty };

            var cVol = Math.Round(vol * (curVal / 100f), 3);
            var pVol = Math.Round(vol * (prevVal / 100f), 3);

            var prop = Math.Round(((cVol - pVol) / pVol) * duration, 3);

            var volDiff = Math.Round(vol * (prop / 100f), 0);

            res[0] = prop.ToString();
            res[1] = volDiff.ToString();
            return res;

        }

        public string GetNumberofDays(int vm)
        {
            int d = vm / DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) * DateTime.Now.Day;
            return d.ToString();
        }
        private void CreateXml(DataTable dt, int total, string keyword, string location, string prevYear, string curYear)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            builder.AppendLine("<GoogletrendsOverTime>");
            builder.AppendLine("<OverTimes keyword =\"" + keyword + "\" country=\"" + location + "\" previousYear=\"" + prevYear + "\" currentYear=\"" + curYear + "\" >");
            foreach (DataRow row in dt.Rows)
            {
                builder.AppendLine("<OverTime>");
                foreach (DataColumn col in dt.Columns)
                {
                    if (string.IsNullOrEmpty(row[col].ToString()))
                        builder.AppendLine("<" + col.ColumnName + " />");
                    else
                        builder.AppendLine("<" + col.ColumnName + ">" + row[col].ToString() + "</" + col.ColumnName + ">");
                }
                builder.AppendLine("</OverTime>");
            }
            builder.AppendLine("</OverTimes>");
            builder.AppendLine("<TotalVolume>" + total + "</TotalVolume>");
            builder.AppendLine("</GoogletrendsOverTime>");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(builder.ToString());
            doc.Save("C:\\inetpub\\wwwroot\\trends\\TrendsOverTime.xml");
        }

    }
}