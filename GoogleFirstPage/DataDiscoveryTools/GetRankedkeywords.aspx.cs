using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections;


namespace GoogleFirstPage.DataDiscoveryTools
{
    public partial class GetRankedkeywords : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["KeywordFinderAPI"].ToString();
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblnodata.Text = string.Empty;
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    cmd = new SqlCommand("select distinct(market) from SearchEnginesParametersTest ", con);
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);

                    ddlcountrycode.DataValueField = "market";
                    ddlcountrycode.DataSource = dt;
                    ddlcountrycode.DataBind();
                    ddlcountrycode.Items.Insert(0, "Drop down");
                    ddlcountrycode.SelectedIndex = 0;
                }

                using (SqlConnection con = new SqlConnection(connection))
                {
                    cmd = new SqlCommand("select distinct([language]) from SearchEnginesParametersTest", con);
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    ddllanguage.DataValueField = "language";
                    ddllanguage.DataSource = dt;
                    ddllanguage.DataBind();
                    ddllanguage.Items.Insert(0, "Drop down");
                    ddllanguage.SelectedIndex = 0;
                }
            }
        }

        protected void btngetrankedkeywords_Click(object sender, EventArgs e)
        {
            lblnodata.Text = string.Empty;
            RegisterAsyncTask(new PageAsyncTask(kwrd_finder_related_keywords_get));
        }


        public async Task kwrd_finder_related_keywords_get()
        {
            var rnd = new Random();
            Dictionary<int, object> postObject = null;
            string z1 = string.Empty;
            string a2 = "";
            string a3 = "";
            int a4 = 0;
            string a = "";
            string b = "";
            int c = 0;
            string dd = "";
            string e = "";
            int f = 0;
            string k = "";
            string l = "";
            string s = "";
            string text = "";
            string a1 = "";
            string b1 = "";
            int c1 = 0;
            string dd1 = "";
            string e1 = "";
            int f1 = 0;
            string orderby1 = "";
            string type1 = "";
            ArrayList z2 = new ArrayList();
            if (rb1orderby.Checked == true)
            {
                z1 = "asc";
            }
            else if (rb2orderby.Checked == true)
            {
                z1 = "desc";
            }

            if (ddlorderby.SelectedItem.Value != "0")
            {
                orderby1 = ddlorderby.SelectedItem.Value.ToString().ToLower() + "," + z1;
            }
            if (ddltype.SelectedItem.Value != "0")
            {
                type1 = ddltype.SelectedItem.Value.ToString().ToLower();
            }
            if (ddltype.SelectedItem.Value == "0")
            {
                type1 = "organic";
            }
            if (chkpositionhigherthan.Checked == true)
            {
                a2 = "position";
                a3 = ">";
                a4 = int.Parse(txtpostionhigerthan.Text);
                z2.Add(a2);
                z2.Add(a3);
                z2.Add(a4);

            }
            if (chkpositionlowerthan.Checked == true)
            {
                a2 = "position";
                a3 = "<";
                a4 = int.Parse(txtpostionlowerthan.Text);

                z2.Add(a2);
                z2.Add(a3);
                z2.Add(a4);
            }



            if (chkcpchigherthan.Checked == true)
            {
                a = "cpc";
                b = ">";
                c = int.Parse(txtcpchigherthan.Text);
                z2.Add(a);
                z2.Add(b);
                z2.Add(c);
                text = "a";
            }
            if (chkcpclowerthan.Checked == true)
            {
                a1 = "cpc";
                b1 = "<";
                c1 = int.Parse(txtcpclowerthan.Text);
                text += "b";
                z2.Add(a1);
                z2.Add(b1);
                z2.Add(c1);
            }

            if (chk5svhigher.Checked == true)
            {
                dd = "search_volume";
                e = ">";
                f = int.Parse(txtsvhigherthan.Text);
                text += "c";
                z2.Add(dd);
                z2.Add(e);
                z2.Add(f);
            }
            if (chk6svlower.Checked == true)
            {
                dd1 = "search_volume";
                e1 = "<";
                f1 = int.Parse(txtsvlowerthan.Text);
                text += "d";
                z2.Add(dd1);
                z2.Add(e1);
                z2.Add(f1);
            }
            if (ddlfilter.SelectedIndex != 0 && txtfilter.Text != "")
            {
                if (ddlfilter.SelectedItem.Value == "1")
                {
                    k = "key";
                    l = "like";
                    s = txtfilter.Text + "%";
                    z2.Add(k);
                    z2.Add(l);
                    z2.Add(s);
                }
                else if (ddlfilter.SelectedItem.Value == "2")
                {
                    k = "key";
                    l = "like";
                    s = "%" + txtfilter.Text;
                    z2.Add(k);
                    z2.Add(l);
                    z2.Add(s);
                }
                else if (ddlfilter.SelectedItem.Value == "3")
                {
                    k = "key";
                    l = "like";
                    s = "%" + txtfilter.Text + "%";
                    z2.Add(k);
                    z2.Add(l);
                    z2.Add(s);
                }
            }

            if (ddlorderby.SelectedItem.Value != "0")
            {
                switch (z2.Count)
                {
                    case 3:

                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
                                type = type1,
                                filters = new object[]
                                {
                        new object[] { z2[0], z2[1], z2[2] }
                                }
                            }
                        };
                        break;
                    case 6:

                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
                                type = type1,
                                filters = new object[]
                                {

                        new object[] { z2[0], z2[1], z2[2] },
                        "and",
                        new object[] { z2[3], z2[4], z2[5] }
                                }
                            }
                        };
                        break;
                    case 9:

                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
                                type = type1,
                                filters = new object[]
                                {
                        new object[] { z2[0], z2[1], z2[2] },
                        "and",
                        new object[] { z2[3], z2[4], z2[5] },
                        "and",
                        new object[] { z2[6], z2[7], z2[8] }
                                }
                            }
                        };
                        break;
                    case 12:
                        //Dictionary<int, object> postObject = null;

                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
                                type = type1,
                                filters = new object[]
                                {
                        new object[] { z2[0], z2[1], z2[2] },
                        "and",
                        new object[] { z2[3], z2[4], z2[5] },
                        "and",
                        new object[] { z2[6], z2[7], z2[8] },
                        "and",
                        new object[] { z2[9], z2[10], z2[11] }
                                }
                            }
                        };
                        //var aaa = postObject;
                        break;
                    case 15:

                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
                                type = type1,
                                filters = new object[]
                                {
                        new object[] { z2[0], z2[1], z2[2] },
                        "and",
                        new object[] { z2[3], z2[4], z2[5] },
                        "and",
                        new object[] { z2[6], z2[7], z2[8] },
                        "and",
                        new object[] { z2[9], z2[10], z2[11] },
                        "or",
                        new object[] { z2[12], z2[13], z2[14] }
                                }
                            }
                        };
                        break;
                    case 18:

                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
                                type = type1,
                                filters = new object[]
                                {
                        new object[] { z2[0], z2[1], z2[2] },
                        "and",
                        new object[] { z2[3], z2[4], z2[5] },
                        "and",
                        new object[] { z2[6], z2[7], z2[8] },
                        "and",
                        new object[] { z2[9], z2[10], z2[11] },
                        "and",
                        new object[] { z2[12], z2[13], z2[14] },
                        "and",
                        new object[] { z2[15], z2[16], z2[17] }
                                }
                            }
                        };
                        break;
                    case 21:

                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
                                type = type1,
                                filters = new object[]
                                {
                        new object[] { z2[0], z2[1], z2[2] },
                        "and",
                        new object[] { z2[3], z2[4], z2[5] },
                        "and",
                        new object[] { z2[6], z2[7], z2[8] },
                        "and",
                        new object[] { z2[9], z2[10], z2[11] },
                        "and",
                        new object[] { z2[12], z2[13], z2[14] },
                        "and",
                        new object[] { z2[15], z2[16], z2[17] },
                        "and",
                        new object[] { z2[18], z2[19], z2[20] }
                                }
                            }
                        };
                        break;
                    case 0:
                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
                                type = type1,
                            }
                        };
                        break;
                }
            }
            if (ddlorderby.SelectedItem.Value == "0")
            {
                switch (z2.Count)
                {
                    case 3:
                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                type = type1,
                                filters = new object[]
                                {

                        new object[] { z2[0], z2[1], z2[2] }
                                }
                            }
                        };
                        break;
                    case 6:
                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                type = type1,
                                filters = new object[]
                                {

                        new object[] { z2[0], z2[1], z2[2] },
                        "and",
                        new object[] { z2[3], z2[4], z2[5] }
                                }
                            }
                        };
                        break;
                    case 9:

                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                type = type1,
                                filters = new object[]
                                {
                        new object[] { z2[0], z2[1], z2[2] },
                        "and",
                        new object[] { z2[3], z2[4], z2[5] },
                        "and",
                        new object[] { z2[6], z2[7], z2[8] }
                                }
                            }
                        };
                        break;
                    case 12:
                        //Dictionary<int, object> postObject = null;

                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                type = type1,
                                filters = new object[]
                                {
                        new object[] { z2[0], z2[1], z2[2] },
                        "and",
                        new object[] { z2[3], z2[4], z2[5] },
                        "and",
                        new object[] { z2[6], z2[7], z2[8] },
                        "and",
                        new object[] { z2[9], z2[10], z2[11] }
                                }
                            }
                        };
                        //var aaa = postObject;
                        break;
                    case 15:

                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                type = type1,
                                filters = new object[]
                                {
                        new object[] { z2[0], z2[1], z2[2] },
                        "and",
                        new object[] { z2[3], z2[4], z2[5] },
                        "and",
                        new object[] { z2[6], z2[7], z2[8] },
                        "and",
                        new object[] { z2[9], z2[10], z2[11] },
                        "and",
                        new object[] { z2[12], z2[13], z2[14] }
                                }
                            }
                        };
                        break;
                    case 18:

                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                type = type1,
                                filters = new object[]
                                {
                        new object[] { z2[0], z2[1], z2[2] },
                        "and",
                        new object[] { z2[3], z2[4], z2[5] },
                        "and",
                        new object[] { z2[6], z2[7], z2[8] },
                        "and",
                        new object[] { z2[9], z2[10], z2[11] },
                        "and",
                        new object[] { z2[12], z2[13], z2[14] },
                        "and",
                        new object[] { z2[15], z2[16], z2[17] }
                                }
                            }
                        };
                        break;
                    case 0:
                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                domain = txtdomain.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                limit = int.Parse(txtlimit.Text),
                                type = type1,
                                offset = 0
                            }
                        };
                        break;
                }
            }
            dynamic taskState = "";
            string err = string.Empty;
            //lblnodata.Text = string.Empty;
            try
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://api.dataforseo.com/"),
                    DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("corey@intelligentpositioning.com:87q5W7H7KeMwZdm2"))) }
                };


                var pagePostResponse = await httpClient.PostAsync("v2/kwrd_finder_ranked_keywords_get", new StringContent(JsonConvert.SerializeObject(new { data = postObject })));
                pagePostResponse.EnsureSuccessStatusCode();
                //Response.Write(pagePostResponse.StatusCode + " " + pagePostResponse.ReasonPhrase);
                var obj = JsonConvert.DeserializeObject<dynamic>(await pagePostResponse.Content.ReadAsStringAsync());

                string err1 = Convert.ToString(obj);
                if (err1.Contains("not enough credits"))
                {
                    lblerror.Text = "Not enough credits";
                    return;
                }

                foreach (var result in obj.results)
                {
                    taskState = ((IEnumerable<dynamic>)result).First();
                    if (taskState.status == "error")
                    {
                        //Response.Write($"\nError in task with post_id {taskState.post_id}. Code: {taskState.error.code} Message: {taskState.error.message}");
                        //throw new Exception(taskState.error.message);
                        err = Convert.ToString(taskState.error.code) + ":" + Convert.ToString(taskState.error.message);
                        //throw new Exception(qq);
                        lblerror.Text = err;
                        return;
                    }
                    //Response.Write(taskState);
                    if (taskState.task_id == null)
                    {
                        lblnodata.Text = "No data found please check input values";
                        return;
                    }
                    else
                    {
                        lblnodata.Text = string.Empty;
                    }
                }

                string value = Convert.ToString(taskState);
                DataTable d = GetdatatablefromJson(value);
                //ToCSV(d, FilePath);
                ToCSV(d);
            }
            catch (HttpRequestException ex)
            {
                //throw ex;
                lblnodata.Text = ex.Message;
                return;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot find column"))
                {
                    lblnodata.Text = "Search volume is missing for columns and rows because of depth input value, please select depth value as 1";
                    lblnodata.Visible = true;
                }
                else
                {
                    lblnodata.Text = ex.Message.ToString();
                    return;
                }
            }
        }


        public static DataTable GetdatatablefromJson(string taskState) //string JSON as in paramater
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("key");
            dt.Columns.Add("exact_domain");
            dt.Columns.Add("country_code");
            dt.Columns.Add("language");
            dt.Columns.Add("position");
            dt.Columns.Add("url");
            dt.Columns.Add("relative_url");
            dt.Columns.Add("results_count");
            dt.Columns.Add("etv");
            dt.Columns.Add("traffic_cost");
            dt.Columns.Add("competition");
            dt.Columns.Add("cpc");
            dt.Columns.Add("date");
            dt.Columns.Add("extra");
            dt.Columns.Add("search_volume");
            dt.Columns.Add("spell");
            dt.Columns.Add("title");
            dt.Columns.Add("snippet");
            dt.Columns.Add("categories");
            dt.Columns.Add("impressions_etv");
            dt.Columns.Add("daily_impressions_avg");
            dt.Columns.Add("ads_pos1_cpc");
            //dt.Columns.Add("ads_pos1_daily_clicks");
            dt.Columns.Add("ads_pos1_daily_cost");



            int cat1 = 0;


            JObject jo = JObject.Parse(taskState);
            var csv = new StringBuilder();
            foreach (JProperty x in (JToken)jo)
            {
                string name = x.Name;
                JToken value = x.Value;
                if (name == "ranked")
                {
                    foreach (var item in value)
                    {
                        ArrayList a = new ArrayList();
                        ArrayList a1 = new ArrayList();
                        DataRow dr = dt.NewRow();
                        string key = item["key"].Value<string>();
                        string exact_domain = item["exact_domain"].Value<string>();
                        string country_code = item["country_code"].Value<string>();
                        string language = item["language"].Value<string>();
                        string position = item["position"].Value<string>();
                        string url = item["url"].Value<string>();
                        string relative_url = item["relative_url"].Value<string>();
                        string results_count = item["results_count"].Value<string>();
                        string etv = item["etv"].Value<string>();
                        string traffic_cost = item["traffic_cost"].Value<string>();
                        object competition1 = item["competition"].Value<string>();
                        string cpc = item["cpc"].Value<string>();
                        string date = item["date"].Value<string>();
                        string extra = item["extra"].Value<string>();
                        string search_volume = item["search_volume"].Value<string>();
                        string spell = item["spell"].Value<string>();
                        object title = item["title"].Value<object>();
                        object snippet = item["snippet"].Value<object>();

                        a.Add(key);
                        a.Add(exact_domain);
                        a.Add(country_code);
                        a.Add(language);
                        a.Add(position);
                        a.Add(url);
                        a.Add(relative_url);
                        a.Add(results_count);
                        a.Add(etv);
                        a.Add(traffic_cost);
                        a.Add(competition1);
                        a.Add(cpc);
                        a.Add(date);
                        a.Add(extra);
                        a.Add(search_volume);
                        a.Add(spell);
                        a.Add(title);
                        a.Add(snippet);


                        string ccc = "";
                        string catagory = "";
                        foreach (var item1 in item["categories"])
                        {
                            try
                            {
                                ccc = item1.ToString();
                                searchProperties sk = SearchCategories.searches.Where(s => s.id == int.Parse(ccc)).SingleOrDefault();
                                catagory += sk.category + "$";
                            }
                            catch (Exception)
                            {
                                //catagory += "/" + ccc + "/";
                            }
                        }
                        if (catagory != "")
                        {
                            cat1 = catagory.LastIndexOf("$");
                            if (cat1 > 0)
                            {
                                catagory = catagory.Remove(cat1);
                            }
                        }

                        a.Add(catagory);

                        string impressions_etv = item["impressions_etv"].Value<string>();
                        string daily_impressions_avg = item["daily_impressions_avg"].Value<string>();
                        string ads_pos1_cpc = item["ads_pos1_cpc"].Value<string>();
                        //string ads_pos1_daily_clicks = item["ads_pos1_daily_clicks"].Value<string>();
                        string ads_pos1_daily_cost = item["ads_pos1_daily_cost"].Value<string>();
                        a.Add(impressions_etv);
                        a.Add(daily_impressions_avg);
                        a.Add(ads_pos1_cpc);
                        //a.Add(ads_pos1_daily_clicks);
                        a.Add(ads_pos1_daily_cost);
                        for (int s = 0; s < a.Count; s++)
                        {
                            dr[s] = a[s];
                        }

                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }



        public void ToCSV(DataTable dtDataTable)
        {
            string csv = string.Empty;

            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                csv += dtDataTable.Columns[i].ToString() + ',';
            }

            //Add new line.
            csv += "\r\n";

            foreach (DataRow row in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    csv += row[i].ToString().Replace(",", ";") + ',';
                }

                //Add new line.
                csv += "\r\n";
            }

            //Download the CSV file.
            Response.Clear();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv");
            //string aaaa= " + txtkeyword.Text + " " + DateTime.Now.ToString("yyyy -MM-dd HH:mm") +".csv";

            string a1 = "Get Ranked Keywords" + "_" + ddlcountrycode.SelectedItem.Text + "_" + ddllanguage.SelectedItem.Text + "_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:tt") + ".csv";
            Response.AddHeader("content-disposition", "attachment;filename=" + a1);
            Response.Charset = "";
            Response.ContentType = "application /text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        }

    }
}