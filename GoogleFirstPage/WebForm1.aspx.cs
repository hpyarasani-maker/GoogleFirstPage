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
                    category_code = 1,
                    //keywords = "car insurance"
                    keywords = new[]
                    {
                    "mercedes"
                     }
                });
                var taskPostResponse = await httpClient.PostAsync("/v3/keywords_data/google_trends/explore/live", new StringContent(JsonConvert.SerializeObject(postData)));
                var result = JsonConvert.DeserializeObject<dynamic>(await taskPostResponse.Content.ReadAsStringAsync());
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

        }

    }
}