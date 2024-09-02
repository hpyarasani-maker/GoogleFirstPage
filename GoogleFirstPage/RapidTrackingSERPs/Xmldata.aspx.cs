using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace GoogleFirstPage.RapidTrackingSERPs
{
    public partial class Xmldata : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            GetParams gp = new GetParams();
            string jobid = Request.QueryString["jobid"].ToString();
            string seid = Request.QueryString["seid"].ToString();
            
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
            GetSource(jobid, seid, keyword, device);
        }

        public string GetSource(string jobId, string seid,string keyword, string device)
        {
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
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
            string response1 = reader.ReadToEnd();
            resStream.Close();
            res.Close();
            try
            {
                JObject obj = JObject.Parse(response1);
                response1 = obj["results"][0]["content"].Value<string>();
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(response1);
                GetXmlDataView(seid, keyword, jobId, device,response1);
                //var sp = SearchParams.searches.Where(s => s.seid == Convert.ToInt32(seid)).SingleOrDefault();
                //device = sp.device;
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetXmlDataView(string seid, string kw, string jobid, string device,string resp)
        {
            try
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(resp);
                int count = 0;
                if (device == "desktop")
                {
                    Desktop clsdesktop = new Desktop();
                    string res = clsdesktop.ProcessDocument(seid, kw, doc, out count);
                    XmlDocument doc1 = new XmlDocument();
                    doc1.LoadXml(res);
                    Response.ContentType = "text/xml";
                    Response.Write(res);
                    Response.End();
                }
                else
                {
                    iOS clsios = new iOS();
                    string res = clsios.ProcessDocument(seid, kw, doc, out count);
                    XmlDocument doc1 = new XmlDocument();
                    doc1.LoadXml(res);
                    Response.ContentType = "text/xml";
                    Response.Write(res);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}