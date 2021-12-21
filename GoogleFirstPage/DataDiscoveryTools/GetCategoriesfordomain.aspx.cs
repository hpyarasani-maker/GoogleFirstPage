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
    public partial class GetCategoriesfordomain : System.Web.UI.Page
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

        public async Task kwrd_finder_categories_for_domain_get()
        {
            var rnd = new Random();
            Dictionary<int, object> postObject = null;
            string z1 = string.Empty;
            string orderby1 = "";
            //bool tuple = false;
            string Type = "organic";
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
            if (ddlType.SelectedItem.Value == "paid")
                Type = "paid";


            if (ddlorderby.SelectedItem.Value != "0")
            {

                postObject = new Dictionary<int, object>
                {
                    [rnd.Next(1, 30000000)] = new
                    {
                        domain = txtdomain.Text,
                        country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                        language = ddllanguage.SelectedItem.Text,
                        limit = int.Parse(txtlimit.Text),
                        type = Type,
                        orderby = orderby1
                        //full_tuple = tuple
                    }
                };

            }
            if (ddlorderby.SelectedItem.Value == "0")
            {

                postObject = new Dictionary<int, object>
                {
                    [rnd.Next(1, 30000000)] = new
                    {
                        domain = txtdomain.Text,
                        country_code = ddlcountrycode.SelectedItem.Text.ToUpper(),
                        language = ddllanguage.SelectedItem.Text,
                        limit = int.Parse(txtlimit.Text),
                        type = Type
                        //full_tuple = tuple
                        //orderby= "organic_count,asc"
                    }
                };
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


                var pagePostResponse = await httpClient.PostAsync("v2/kwrd_finder_categories_for_domain_get", new StringContent(JsonConvert.SerializeObject(new { data = postObject })));
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
                        err = Convert.ToString(taskState.error.code) + ":" + Convert.ToString(taskState.error.message);
                        //throw new Exception(qq);
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
            int cat1;
            string ccc = "";
            DataTable dt = new DataTable();
            JObject jo = JObject.Parse(taskState);
            dt.Columns.Add("Category");
            dt.Columns.Add("country_code");
            dt.Columns.Add("language");
            dt.Columns.Add("organic_count");
            dt.Columns.Add("paid_count");
            dt.Columns.Add("organic_traffic_cost");
            dt.Columns.Add("paid_traffic_cost");
            dt.Columns.Add("etv");
            dt.Columns.Add("impressions_etv");
            dt.Columns.Add("date");
            dt.Columns.Add("pos1");
            dt.Columns.Add("pos2_3");
            dt.Columns.Add("pos4_10");
            dt.Columns.Add("pos11_20");
            dt.Columns.Add("pos21_30");
            dt.Columns.Add("pos31_40");
            dt.Columns.Add("pos41_50");
            dt.Columns.Add("pos51_60");
            dt.Columns.Add("pos61_70");
            dt.Columns.Add("pos71_80");
            dt.Columns.Add("pos81_90");
            dt.Columns.Add("pos91_100");
            dt.Columns.Add("paid_pos1");
            dt.Columns.Add("paid_pos2_3");
            dt.Columns.Add("paid_pos4_10");
            dt.Columns.Add("paid_pos11_20");
            dt.Columns.Add("paid_pos21_100");
            foreach (JProperty x in (JToken)jo)
            {
                string name = x.Name;
                JToken value = x.Value;

                if (name == "categories")
                {
                    string catagory = "";
                    foreach (var item in value)
                    {
                        catagory = "";
                        ArrayList a = new ArrayList();
                        DataRow dr = dt.NewRow();
                        foreach (var item1 in item["category"])
                        {
                            //catagory = "";
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


                        string country_code = item["country_code"].Value<string>();
                        string language = item["language"].Value<string>();
                        string organic_count = item["organic_count"].Value<string>();
                        string paid_count = item["paid_count"].Value<string>();
                        string organic_traffic_cost = item["organic_traffic_cost"].Value<string>();
                        string paid_traffic_cost = item["paid_traffic_cost"].Value<string>();
                        string etv = item["etv"].Value<string>();
                        string impressions_etv = item["impressions_etv"].Value<string>();
                        string date = item["date"].Value<string>();
                        string pos1 = item["pos1"].Value<string>();
                        string pos2_3 = item["pos2_3"].Value<string>();
                        string pos4_10 = item["pos4_10"].Value<string>();
                        string pos11_20 = item["pos11_20"].Value<string>();
                        string pos21_30 = item["pos21_30"].Value<string>();
                        string pos31_40 = item["pos31_40"].Value<string>();
                        string pos41_50 = item["pos41_50"].Value<string>();
                        string pos51_60 = item["pos51_60"].Value<string>();
                        string pos61_70 = item["pos61_70"].Value<string>();
                        string pos71_80 = item["pos71_80"].Value<string>();
                        string pos81_90 = item["pos81_90"].Value<string>();
                        string pos91_100 = item["pos91_100"].Value<string>();
                        string paid_pos1 = item["paid_pos1"].Value<string>();
                        string paid_pos2_3 = item["paid_pos2_3"].Value<string>();
                        string paid_pos4_10 = item["paid_pos4_10"].Value<string>();
                        string paid_pos11_20 = item["paid_pos11_20"].Value<string>();
                        string paid_pos21_100 = item["paid_pos21_100"].Value<string>();
                        a.Add(catagory);
                        a.Add(country_code);
                        a.Add(language);
                        a.Add(organic_count);
                        a.Add(paid_count);
                        a.Add(organic_traffic_cost);
                        a.Add(paid_traffic_cost);
                        a.Add(etv);
                        a.Add(impressions_etv);
                        a.Add(date);
                        a.Add(pos1);
                        a.Add(pos2_3);
                        a.Add(pos4_10);
                        a.Add(pos11_20);
                        a.Add(pos21_30);
                        a.Add(pos31_40);
                        a.Add(pos41_50);
                        a.Add(pos51_60);
                        a.Add(pos61_70);
                        a.Add(pos71_80);
                        a.Add(pos81_90);
                        a.Add(pos91_100);
                        a.Add(paid_pos1);
                        a.Add(paid_pos2_3);
                        a.Add(paid_pos4_10);
                        a.Add(paid_pos11_20);
                        a.Add(paid_pos21_100);
                        try
                        {
                            for (int s = 0; s < a.Count; s++)
                            {
                                dr[s] = a[s];
                            }
                        }
                        catch (Exception e)
                        {

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


            csv += "\r\n";

            foreach (DataRow row in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    csv += row[i].ToString().Replace(",", ";") + ',';
                }


                csv += "\r\n";
            }

            //Download the CSV file.
            Response.Clear();
            Response.Buffer = true;

            string a1 = "Get Categories For Domain" + "_" + ddlcountrycode.SelectedItem.Text + "_" + ddllanguage.SelectedItem.Text + "_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:tt") + ".csv";
            Response.AddHeader("content-disposition", "attachment;filename=" + a1);
            Response.Charset = "";
            Response.ContentType = "application /text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        }

        protected void btnGetCategoriesForDomain_Click(object sender, EventArgs e)
        {
            lblnodata.Text = string.Empty;
            RegisterAsyncTask(new PageAsyncTask(kwrd_finder_categories_for_domain_get));
        }
    }
}