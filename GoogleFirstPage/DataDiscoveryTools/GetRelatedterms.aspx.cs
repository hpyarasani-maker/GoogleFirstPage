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
    public partial class GetRelatedterms : System.Web.UI.Page
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

        protected void btngetrelatedterms_Click(object sender, EventArgs e)
        {
            lblnodata.Text = string.Empty;

            RegisterAsyncTask(new PageAsyncTask(kwrd_finder_related_keywords_get));
        }


        public async Task kwrd_finder_related_keywords_get()
        {
            var rnd = new Random();
            //Dictionary<int, object> postObject = new Dictionary<int, object>();
            Dictionary<int, object> postObject = null;
            string z1 = string.Empty;
            //string cpcsymbol = "";
            //string svsymbol = "";
            //string[] str = { "cpc", ">", "1" };
            string a = "";
            string b = "";
            int c = 0;
            string dd = "";
            string e = "";
            int f = 0;//"key", "like", "%dvd"
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
            ArrayList z2 = new ArrayList();
            //string rrrw = ddlorderby.SelectedItem.ToString();
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
                orderby1 = ddlorderby.SelectedItem.ToString().ToLower() + "," + z1;
            }

            if (chk3cpchigh.Checked == true)
            {
                a = "cpc";
                b = ">";
                c = int.Parse(txtcpchigh.Text);
                z2.Add(a);
                z2.Add(b);
                z2.Add(c);
                text = "a";
            }
            if (chk4cpclower.Checked == true)
            {

                a1 = "cpc";
                b1 = "<";
                c1 = int.Parse(txtcpclower.Text);
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
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
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
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
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
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
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
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
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
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1,
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
                    case 0:
                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
                                orderby = orderby1
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
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
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
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
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
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
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
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
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
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
                                offset = 0,
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
                    case 0:
                        postObject = new Dictionary<int, object>
                        {
                            [rnd.Next(1, 30000000)] = new
                            {
                                keyword = txtkeyword.Text,
                                country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                                language = ddllanguage.SelectedItem.Text,
                                depth = int.Parse(ddldepth.SelectedItem.Value),
                                limit = int.Parse(txtlimit.Text),
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


                var pagePostResponse = await httpClient.PostAsync("v2/kwrd_finder_related_keywords_get", new StringContent(JsonConvert.SerializeObject(new { data = postObject })));
                pagePostResponse.EnsureSuccessStatusCode();
                //Response.Write(pagePostResponse.StatusCode + " " + pagePostResponse.ReasonPhrase);
                var obj = JsonConvert.DeserializeObject<dynamic>(await pagePostResponse.Content.ReadAsStringAsync());

                string err1 = Convert.ToString(obj);
                //err1 = "";
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




        public DataTable GetdatatablefromJson(string taskState) //string JSON as in paramater
        {
            string cdate = retrivedate().Split(' ')[0];
            DateTime oDate = DateTime.Parse(cdate);
            var lastMonths = Enumerable.Range(0, 12).Select(i => oDate.AddMonths(-i).ToString("MM_yyyy"));
            DataTable dt = new DataTable();
            dt.Columns.Add("key");
            dt.Columns.Add("country_code");
            dt.Columns.Add("search_volume");
            dt.Columns.Add("cpc");
            dt.Columns.Add("competition1");
            dt.Columns.Add("categories");

            ArrayList az = new ArrayList();
            //az.Add("7_2020");
            foreach (var monthAndYear in lastMonths)
            {
                if (monthAndYear.StartsWith("0"))
                {
                    az.Add(monthAndYear.Substring(1));
                }
                else
                {
                    az.Add(monthAndYear);
                }
            }
            //az.Add("6_2020");
            //az.Add("5_2020");
            //az.Add("4_2020");
            //az.Add("3_2020");
            //az.Add("2_2020");
            //az.Add("1_2020");
            //az.Add("12_2018");
            //az.Add("11_2018");
            //az.Add("10_2018");
            //az.Add("9_2018");
            //az.Add("8_2018");
            foreach (string item in az)
            {
                dt.Columns.Add(item);
            }

            int count = 0;
            int cat1 = 0;

            //JObject jo = JObject.Parse(System.IO.File.ReadAllText(@"C:\Users\Indianuser-5\Desktop\json.txt"));
            JObject jo = JObject.Parse(taskState);
            var csv = new StringBuilder();
            foreach (JProperty x in (JToken)jo)
            {
                string name = x.Name;
                JToken value = x.Value;
                if (name == "related")
                {
                    foreach (var item in value)
                    {
                        ArrayList a = new ArrayList();
                        ArrayList a1 = new ArrayList();
                        DataRow dr = dt.NewRow();
                        string kw = item["key"].Value<string>();
                        string country_code = item["country_code"].Value<string>();
                        string search_volume = item["search_volume"].Value<string>();
                        string cpc = item["cpc"].Value<string>();
                        object competition1 = item["competition"].Value<string>();
                        a.Add(kw);
                        a.Add(country_code);
                        a.Add(search_volume);
                        a.Add(cpc);
                        a.Add(competition1);
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
                        //catagory = catagory.Remove(catagory.Length - 1);
                        a.Add(catagory);
                        //string sv = "";
                        //foreach (var item2 in item["history"])
                        //{
                        //    string month = item2["month"].Value<string>();
                        //    string year = item2["year"].Value<string>();
                        //    string s = item2["search_volume"].Value<string>();
                        //    sv = month + "_" + year;

                        //    a.Add(s);
                        //    a1.Add(sv);
                        //}

                        //foreach (string i in a1)
                        //{
                        //    if (count == 0)
                        //        dt.Columns.Add(i);
                        //}
                        //count++;
                        string sv = "";
                        foreach (var item2 in item["history"])
                        {
                            string month = item2["month"].Value<string>();
                            string year = item2["year"].Value<string>();
                            string s = item2["search_volume"].Value<string>();
                            sv = month + "_" + year + "$" + s;
                            a1.Add(sv);
                            //a.Add(s);


                        }
                        int j = 0;
                        //foreach (string mon in az)
                        //{
                        //    for (int i = j; i < a1.Count; i++)
                        //    {
                        //        if (mon == a1[i].ToString().Split('$')[0])
                        //        {
                        //            a.Add(a1[i].ToString().Split('$')[1]);
                        //            break;
                        //        }
                        //        else
                        //        {
                        //            a.Add("");
                        //            j--;
                        //            break;
                        //        }
                        //    }
                        //    j++;

                        //}

                        foreach (string mon in az)
                        {
                            ArrayList an = new ArrayList();
                            for (int i = j; i < a1.Count; i++)
                            {
                                if (mon == a1[i].ToString().Split('$')[0])
                                {
                                    a.Add(a1[i].ToString().Split('$')[1]);
                                    an.Add(a1[i].ToString().Split('$')[1]);
                                    break;
                                }
                                //else
                                //{
                                //    a.Add("");
                                //    j--;
                                //    break;
                                //}
                            }
                            if (an.Count == 0)
                            {
                                a.Add("");
                            }
                            //j++;

                        }


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

            string a1 = txtkeyword.Text + "_" + ddlcountrycode.SelectedItem.Text + "_" + ddllanguage.SelectedItem.Text + "_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:tt") + ".csv";
            Response.AddHeader("content-disposition", "attachment;filename=" + a1);
            Response.Charset = "";
            Response.ContentType = "application /text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        }

        public string retrivedate()
        {
            string ddate = "";
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection("Data Source=googlefirstpage.database.windows.net;Initial Catalog=TrackingKeywords;User ID=hemachander@googlefirstpage;Password=Brisbane007"))
            {
                //Data Source=googlefirstpage.database.windows.net;Initial Catalog=SearchEngines;User ID=hemachander@googlefirstpage;password=Brisbane007
                con.Open();
                cmd = new SqlCommand("select date from EmptyValuesMonth", con);
                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {
                    ddate = rdr["date"].ToString();
                    //object myString = rdr.GetString(0);
                    //aaa = myString.ToString();

                }

                return ddate;
            }
        }
    }
}