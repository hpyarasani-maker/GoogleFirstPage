using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace GoogleFirstPage.RapidTrackingSERPs
{
    public class Oxylabsresponse
    {
        public string GetJobidsource1(string jobId, string seid, string device, out string uule)
        {
            string username = "gpidatametrics";
            string password = "sdV5X3fcX6";
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
                if(obj["results"][0]["url"].Value<string>().Contains("uule"))
                {
                    if (device == "desktop")
                        uule = obj["results"][0]["url"].Value<string>().Split('&')[3].Replace("uule=", "");
                    else uule = obj["results"][0]["url"].Value<string>().Split('&')[4].Replace("uule=", "");
                }
                else
                {
                    uule = sp.uule;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}