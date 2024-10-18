using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using Ionic.Zip;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace GoogleFirstPage.RapidTrackingSERPs
{
    public partial class jobidsource : System.Web.UI.Page
    {
        Oxylabsresponse oxyresponse = new Oxylabsresponse();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetParams gp = new GetParams();
            string jobid = Request.QueryString["jobid"].ToString();

            string seid = string.Empty;
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string resURL = "http://data.oxylabs.io/v1/queries/" + jobid;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(resURL);
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
            HttpWebResponse res1 = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream resStream = res1.GetResponseStream();
            StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
            string response = reader.ReadToEnd();
            resStream.Close();
            res1.Close();
            JObject obj = JObject.Parse(response);
            gp.geo_location = obj["geo_location"].Value<string>();
            gp.user_agent_type = obj["user_agent_type"].Value<string>();
            gp.locale = obj["locale"].Value<string>();
            gp.query = obj["query"].Value<string>();
            gp.domain = obj["domain"].Value<string>();

            var sp = SearchParams.searches.FirstOrDefault(s =>
                    s.device == gp.user_agent_type &&
                    s.domain == gp.domain &&
                    s.geo_location == gp.geo_location &&
                    s.locale == gp.locale
                    );
            seid = sp.seid.ToString();

            string device = obj["user_agent_type"].Value<string>();
            string keyword = obj["query"].Value<string>();
            string geol = obj["geo_location"].Value<string>();
            string locale = obj["locale"].Value<string>();

            string html1 = GetHtmlSource(jobid, seid, device);

            if (!string.IsNullOrEmpty(html1))
            {
                Response.ContentType = "text/html";
                Response.Write(html1.ToString());
                //Response.Write("<script>window.open('xmldata.aspx?jobid=" + jobid + "&seid=" + seid + "&keyword=" + keyword.Replace("%20"," ") + "','_blank');</script>");
                //ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + html1 + "');", true);
            }
        }

        private string GetHtmlSource(string jobId, string seid, string device)
        {
            
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string resURL = "http://data.oxylabs.io/v1/queries/" + jobId + "/results";
            //string resURL = "https://data.oxylabs.io/v1/queries/" + jobId;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(resURL);
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream resStream = res.GetResponseStream();
            StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
            string response = reader.ReadToEnd();
            resStream.Close();
            res.Close();
            try
            {
                JObject obj = JObject.Parse(response);
                response = obj["results"][0]["content"].Value<string>();
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(response);

                var sp = SearchParams.searches.Where(s => s.seid == Convert.ToInt32(seid)).SingleOrDefault();
                device = sp.device;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}