using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.DataDiscoveryTools
{
    public partial class GetKeywordsforterms : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["KeywordFinderAPI"].ToString();
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                lblnodata.Text = string.Empty;
                //kwderrorlbl.Text = string.Empty;
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

        string[] kws;

        private async Task kwrd_finder_related_keywords_get()
        {
            lblnodata.Text = string.Empty;
            // kwderrorlbl.Text = string.Empty;
            var rnd = new Random();
            //Dictionary<int, object> postObject = new Dictionary<int, object>();
            Dictionary<int, object> postObject = null;
            string z1 = string.Empty;
            Boolean z2 = false;
            string orderby1 = "";
            if (rb1orderby.Checked == true)
            {
                z1 = "asc";
            }
            else if (rb2orderby.Checked == true)
            {
                z1 = "desc";
            }

            if (rbcvtrue.Checked == true)
            {
                z2 = true;
            }
            else if (rbcvfalse.Checked == true)
            {
                z2 = false;
            }

            if (txtlimit.Text != "")
            {

                if (ddlorderby.SelectedItem.Value != "0")
                {
                    //orderby1 = ddlorderby.SelectedItem.Value.ToString().ToLower() + "," + z1;
                    orderby1 = ddlorderby.SelectedItem.Value.ToString().ToLower();

                    postObject = new Dictionary<int, object>
                    {
                        [rnd.Next(1, 30000000)] = new
                        {
                            country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                            language = ddllanguage.SelectedItem.Text,
                            limit = int.Parse(txtlimit.Text),
                            keywords = kws,
                            sort_by = orderby1,
                            close_variants = z2

                        }
                    };
                }

                if (ddlorderby.SelectedItem.Value == "0")
                {
                    postObject = new Dictionary<int, object>
                    {
                        [rnd.Next(1, 30000000)] = new
                        {
                            country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                            language = ddllanguage.SelectedItem.Text,
                            limit = int.Parse(txtlimit.Text),
                            keywords = kws,
                            close_variants = z2
                        }
                    };
                }
            }
            if (txtlimit.Text == "")
            {
                if (ddlorderby.SelectedItem.Value != "0")
                {
                    //orderby1 = ddlorderby.SelectedItem.Value.ToString().ToLower() + "," + z1;
                    orderby1 = ddlorderby.SelectedItem.Value.ToString().ToLower();

                    postObject = new Dictionary<int, object>
                    {
                        [rnd.Next(1, 30000000)] = new
                        {
                            country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                            language = ddllanguage.SelectedItem.Text,
                            keywords = kws,
                            sort_by = orderby1,
                            close_variants = z2
                        }
                    };
                }

                if (ddlorderby.SelectedItem.Value == "0")
                {
                    postObject = new Dictionary<int, object>
                    {
                        [rnd.Next(1, 30000000)] = new
                        {
                            country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                            language = ddllanguage.SelectedItem.Text,
                            keywords = kws,
                            close_variants = z2
                        }
                    };
                }
            }
            dynamic taskState = "";
            string err = string.Empty;
            //lblnodata.Text = string.Empty;
            lblerror.Text = "";

            try
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://api.dataforseo.com/"),
                    DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("corey@intelligentpositioning.com:87q5W7H7KeMwZdm2"))) }
                };
                var taskPostResponse = await httpClient.PostAsync("v2/kwrd_finder_kwrd_for_terms", new StringContent(JsonConvert.SerializeObject(new { data = postObject })));
                taskPostResponse.EnsureSuccessStatusCode();
                //Response.Write(taskPostResponse.StatusCode + " " + taskPostResponse.ReasonPhrase);
                var obj = JsonConvert.DeserializeObject<dynamic>(await taskPostResponse.Content.ReadAsStringAsync());

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

                        err = Convert.ToString(taskState.error.code) + ":" + Convert.ToString(taskState.error.message);

                        lblerror.Text = err;
                        return;
                    }

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

                //kwderrorlbl.Text = string.Empty;
                string value = Convert.ToString(taskState);
                DataTable d = GetdatatablefromJson(value);
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
                lblnodata.Text = ex.Message.ToString();
                return;
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
            dt.Columns.Add("language");
            dt.Columns.Add("search_volume");
            dt.Columns.Add("competition");
            dt.Columns.Add("cpc");




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

            foreach (string item in az)
            {
                dt.Columns.Add(item);

            }

            dt.Columns.Add("categories");

            int cat1 = 0;
            //JObject jo = JObject.Parse(System.IO.File.ReadAllText(@"C:\Users\Indianuser-5\Desktop\json.txt"));
            JObject jo = JObject.Parse(taskState);
            var csv = new StringBuilder();
            foreach (JProperty x in (JToken)jo)
            {
                string name = x.Name;
                JToken value = x.Value;
                if (name == "result")
                {
                    foreach (var item in value)
                    {
                        ArrayList a = new ArrayList();
                        ArrayList a1 = new ArrayList();
                        DataRow dr = dt.NewRow();
                        string kw = item["key"].Value<string>();
                        string country_code = item["country_code"].Value<string>();
                        string language = item["language"].Value<string>();
                        string search_volume = item["search_volume"].Value<string>();
                        object competition1 = item["competition"].Value<string>();
                        string cpc = item["cpc"].Value<string>();
                        a.Add(kw);
                        a.Add(country_code);
                        a.Add(language);
                        a.Add(search_volume);
                        a.Add(competition1);
                        a.Add(cpc);

                        string sv = "";
                        foreach (var item2 in item["history"])
                        {
                            string month = item2["month"].Value<string>();
                            string year = item2["year"].Value<string>();
                            string s = item2["search_volume"].Value<string>();
                            sv = month + "_" + year + "$" + s;
                            a1.Add(sv);

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
            //kwderrorlbl.Text = string.Empty;
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

            string a1 = "Get Keywords for Terms_" + ddlcountrycode.SelectedItem.Text + "_" + ddllanguage.SelectedItem.Text + "_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:tt") + ".csv";
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
                }

                return ddate;
            }
        }

        private static bool HasDuplicates(string[] kwd)
        {
            HashSet<string> stringSet = new HashSet<string>();
            bool hasDups = false;
            foreach (var s in kwd)
            {
                if (stringSet.Contains(s))
                {
                    hasDups = true;
                    break;
                }
                else
                {
                    stringSet.Add(s);
                }
            }
            return hasDups;
        }

        protected void cstlabel_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var sText = args.Value;
            string t200 = "Keywords must be below 200.";
            string errorMessage = "There are duplicates keywords found in the list. Please remove them.";
            kws = sText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (kws.Count() > 200)
            {
                cstlabel.ErrorMessage = t200;
                args.IsValid = false;
                Page.ModelState.AddModelError("Error", errorMessage);
            }
            else if (HasDuplicates(kws))
            {
                cstlabel.ErrorMessage = errorMessage;
                args.IsValid = false;
                Page.ModelState.AddModelError("Error", errorMessage);
            }
            else
                args.IsValid = true;
        }

        protected void btngetkeywordsforterms_Click(object sender, EventArgs e)
        {
            if (Page.ModelState.IsValid)
            {
                RegisterAsyncTask(new PageAsyncTask(kwrd_finder_related_keywords_get));
            }
        }
    }
}