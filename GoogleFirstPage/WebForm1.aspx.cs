using Newtonsoft.Json;
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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnq1_Click(object sender, EventArgs e)
        {
            if (Page.ModelState.IsValid)
            {
                RegisterAsyncTask(new PageAsyncTask(keywords_data_trends_explore_live));
            }
        }

        public async Task keywords_data_trends_explore_live()
        {
            dynamic result = "";
            try
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("https://api.dataforseo.com/"),
                    DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("hemachander@intelligentpositioning.com:19a90cf9a3f8a1e1"))) }
                };
                var postData = new List<object>();
                postData.Add(new
                {
                    location_name = "United States",
                    date_from = "2023-01-01",
                    date_to = "2023-01-31",
                    type = "web",
                    category_code = 0,
                    keywords = new[]
                    {
                    //"seo api",
                    //"rank api"
                    txtkwds1.Text
                }
                });
                var taskPostResponse = await httpClient.PostAsync("/v3/keywords_data/google_trends/explore/live", new StringContent(JsonConvert.SerializeObject(postData)));
                result = JsonConvert.DeserializeObject<dynamic>(await taskPostResponse.Content.ReadAsStringAsync());
                if (result.status_code == 20000)
                {
                    Response.Write(result);
                }
                else
                    Response.Write($"error. Code: {result.status_code} Message: {result.status_message}");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string value = Convert.ToString(result);
            DataTable d = GetdatatablefromJson(value);
        }

        public static DataTable GetdatatablefromJson(string taskState) //string JSON as in paramater
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("date_from");
            dt.Columns.Add("date_to");
            dt.Columns.Add("values");
          
            JObject jo = JObject.Parse(taskState);
            var csv = new StringBuilder();
            foreach (JProperty x in (JToken)jo)
            {
                string name = x.Type.ToString();
                JToken value = x.Value;
                if (name == "trends")
                {
                    foreach (var item in value)
                    {
                        ArrayList a = new ArrayList();
                        ArrayList a1 = new ArrayList();
                        DataRow dr = dt.NewRow();
                        string datefrm = item["date_from"].Value<string>();
                        string dateto = item["date_to"].Value<string>();
                        string values1 = item["values"].Value<string>();
                        
                        a.Add(datefrm);
                        a.Add(dateto);
                        a.Add(values1);
                        
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

    }
}