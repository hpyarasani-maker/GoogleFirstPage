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
    public partial class GetSerpcompetitors : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["KeywordFinderAPI"].ToString();
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblnodata.Text = string.Empty;
            kwderrorlbl.Text = string.Empty;
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

        string[] kws;
        protected void btnserpgetcompetitors_Click(object sender, EventArgs e)
        {
            if (Page.ModelState.IsValid)
            {
                RegisterAsyncTask(new PageAsyncTask(kwrds_finder_related_keywords_get));
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
        public async Task kwrds_finder_related_keywords_get()
        {
            var rnd = new Random();
            //Dictionary<int, object> postObject = new Dictionary<int, object>();
            Dictionary<int, object> postObject = null;
            string z1 = string.Empty;
            string orderby1 = "";
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

                postObject = new Dictionary<int, object>
                {
                    [rnd.Next(1, 30000000)] = new
                    {
                        //loc_name_canonical = "United States",
                        country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                        language = ddllanguage.SelectedItem.Text,
                        limit = int.Parse(txtlimit.Text),
                        keywords = kws,
                        orderby = orderby1
                    }
                };
            }
            if (ddlorderby.SelectedItem.Value == "0")
            {
                postObject = new Dictionary<int, object>
                {
                    [rnd.Next(1, 30000000)] = new
                    {
                        //loc_name_canonical = "United States",
                        country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                        language = ddllanguage.SelectedItem.Text,
                        limit = int.Parse(txtlimit.Text),
                        keywords = kws
                    }
                };
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


                var pagePostResponse = await httpClient.PostAsync("v2/kwrd_finder_serp_competitors", new StringContent(JsonConvert.SerializeObject(new { data = postObject })));
                pagePostResponse.EnsureSuccessStatusCode();
                //Response.Write(pagePostResponse.StatusCode + " " + pagePostResponse.ReasonPhrase);
                var obj = JsonConvert.DeserializeObject<dynamic>(await pagePostResponse.Content.ReadAsStringAsync());

                string err1 = Convert.ToString(obj);
                //err1 = "";
                if (err1.Contains("not enough credits"))
                {
                    lblerror.Text = "not enough credits";
                    return;
                }
                foreach (var result in obj.results)
                {
                    taskState = ((IEnumerable<dynamic>)result).First();
                    if (taskState.status == "error")
                    {
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

                lblnodata.Text = ex.Message.ToString();
                return;
            }
        }

        public static DataTable GetdatatablefromJson(string taskState) //string JSON as in paramater
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("domain");
            dt.Columns.Add("avg_position");
            dt.Columns.Add("median_position");
            dt.Columns.Add("keywords_count");
            dt.Columns.Add("rating");
            dt.Columns.Add("etv");
            dt.Columns.Add("visibility");

            JObject jo = JObject.Parse(taskState);
            var csv = new StringBuilder();
            foreach (JProperty x in (JToken)jo)
            {
                string name = x.Name;
                JToken value = x.Value;
                if (name == "domains")
                {
                    foreach (var item in value)
                    {
                        ArrayList a = new ArrayList();
                        ArrayList a1 = new ArrayList();
                        DataRow dr = dt.NewRow();
                        string domain = item["domain"].Value<string>();
                        string avg_position = item["avg_position"].Value<string>();
                        string median_position = item["median_position"].Value<string>();
                        string keywords_count = item["keywords_count"].Value<string>();
                        string rating = item["rating"].Value<string>();
                        string etv = item["etv"].Value<string>();
                        string visibility = item["visibility"].Value<string>();

                        a.Add(domain);
                        a.Add(avg_position);
                        a.Add(median_position);
                        a.Add(keywords_count);
                        a.Add(rating);
                        a.Add(etv);
                        a.Add(visibility);

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

            string a1 = "Get SERP Competitors_" + ddlcountrycode.SelectedItem.Text + "_" + ddllanguage.SelectedItem.Text + "_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:tt") + ".csv";
            Response.AddHeader("content-disposition", "attachment;filename=" + a1);
            Response.Charset = "";
            Response.ContentType = "application /text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        }

        protected void cstsc_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var sText = args.Value;
            string t200 = "Keywords must be below 200.";
            string errorMessage = "There are duplicates keywords found in the list. Please remove them.";
            kws = sText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (kws.Count() > 200)
            {
                cstsc.ErrorMessage = t200;
                args.IsValid = false;
                Page.ModelState.AddModelError("Error", errorMessage);
            }
            else if (HasDuplicates(kws))
            {
                cstsc.ErrorMessage = errorMessage;
                args.IsValid = false;
                Page.ModelState.AddModelError("Error", errorMessage);
            }
            else
                args.IsValid = true;
        }
    }
}