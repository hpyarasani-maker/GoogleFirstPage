using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Threading;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using Formatting = Newtonsoft.Json.Formatting;
using System.Text.RegularExpressions;

namespace GoogleFirstPage.GoogleClassicLinksCallback
{
    public class HTMLParserNewTask_Callbackurl
    {
        public string jobid = string.Empty;
        public string callback_url = string.Empty;
        public string resURL = string.Empty;
        public string urlType = string.Empty;
        public string htmlsource = string.Empty;

        public ArrayList DoProcessDesktop(string resp)
        {
            JObject job = JObject.Parse(resp);
            string status = job["status"].Value<string>();
            string kw = job["query"].Value<string>();
            string device = job["user_agent_type"].Value<string>();
            string hl = job["locale"].Value<string>();
            string gl = job["geo_location"].Value<string>();
            string domain = job["domain"].Value<string>();
            jobid = job["id"].Value<string>();
            ArrayList arRes = new ArrayList();
            try
            {
                //string username = "gpidatametrics";
                //string password = "sdV5X3fcX6";
                string username = "piapp";
                string password = "b5FCvgkjxx";

                if (status == "done")
                {
                    resURL = job["results_url"].Value<string>();
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

                        arRes = DesktopPattern(response);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //if (seid > 0) 
                    //    ProcessResults(arRes, kw, seid, jobid );

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Result Request: " + ex.Message);

            }
            return arRes;
        }

        public ArrayList DoProcessMobile(string resp)
        {
            JObject job = JObject.Parse(resp);
            string status = job["status"].Value<string>();
            string kw = job["query"].Value<string>();
            string device = job["user_agent_type"].Value<string>();
            string hl = job["locale"].Value<string>();
            string gl = job["geo_location"].Value<string>();
            string domain = job["domain"].Value<string>();
            jobid = job["id"].Value<string>();
            ArrayList arRes = new ArrayList();
            try
            {
                //string username = "gpidatametrics";
                //string password = "sdV5X3fcX6";
                string username = "piapp";
                string password = "b5FCvgkjxx";

                if (status == "done")
                {
                    resURL = job["results_url"].Value<string>();
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
                        arRes = MobilePattern(response);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //if (seid > 0) 
                    //    ProcessResults(arRes, kw, seid, jobid );

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Result Request: " + ex.Message);

            }
            return arRes;
        }

        public ArrayList DoProcessImagesDesktop(string resp)
        {
            JObject job = JObject.Parse(resp);
            string status = job["status"].Value<string>();
            string kw = job["query"].Value<string>();
            string device = job["user_agent_type"].Value<string>();
            string hl = job["locale"].Value<string>();
            string gl = job["geo_location"].Value<string>();
            string domain = job["domain"].Value<string>();
            jobid = job["id"].Value<string>();
            ArrayList arRes = new ArrayList();
            try
            {
                //string username = "gpidatametrics";
                //string password = "sdV5X3fcX6";
                string username = "piapp";
                string password = "b5FCvgkjxx";

                if (status == "done")
                {
                    resURL = job["results_url"].Value<string>();
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
                        //arRes = ImagesPattern(response,urlType);
                        arRes = ImagesPattern(response, "ImageLinks");

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //if (seid > 0) 
                    //    ProcessResults(arRes, kw, seid, jobid );

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Result Request: " + ex.Message);

            }
            return arRes;
        }

        public ArrayList DoProcessImagesDesktop1(string resp)
        {
            JObject job = JObject.Parse(resp);
            string status = job["status"].Value<string>();
            string kw = job["query"].Value<string>();
            string device = job["user_agent_type"].Value<string>();
            string hl = job["locale"].Value<string>();
            string gl = job["geo_location"].Value<string>();
            string domain = job["domain"].Value<string>();
            jobid = job["id"].Value<string>();
            ArrayList arRes = new ArrayList();
            try
            {
                //string username = "gpidatametrics";
                //string password = "sdV5X3fcX6";
                string username = "piapp";
                string password = "b5FCvgkjxx";

                if (status == "done")
                {
                    resURL = job["results_url"].Value<string>();
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
                        //arRes = ImagesPattern(response,urlType);
                        arRes = ImagesPattern(response, "PageLinks");

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //if (seid > 0) 
                    //    ProcessResults(arRes, kw, seid, jobid );

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Result Request: " + ex.Message);

            }
            return arRes;
        }

        public ArrayList DoProcessImagesMobile(string resp)
        {
            JObject job = JObject.Parse(resp);
            string status = job["status"].Value<string>();
            string kw = job["query"].Value<string>();
            string device = job["user_agent_type"].Value<string>();
            string hl = job["locale"].Value<string>();
            string gl = job["geo_location"].Value<string>();
            string domain = job["domain"].Value<string>();
            jobid = job["id"].Value<string>();
            ArrayList arRes = new ArrayList();
            try
            {
                //string username = "gpidatametrics";
                //string password = "sdV5X3fcX6";
                string username = "piapp";
                string password = "b5FCvgkjxx";

                if (status == "done")
                {
                    resURL = job["results_url"].Value<string>();
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
                        //arRes = ImagesPattern(response,urlType);
                        arRes = ImagesPatternMobile(response, "PageLinks");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //if (seid > 0) 
                    //    ProcessResults(arRes, kw, seid, jobid );

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Result Request: " + ex.Message);

            }
            return arRes;
        }

        public ArrayList DoProcessPageResultsMobile(string resp)
        {
            JObject job = JObject.Parse(resp);
            string status = job["status"].Value<string>();
            string kw = job["query"].Value<string>();
            string device = job["user_agent_type"].Value<string>();
            string hl = job["locale"].Value<string>();
            string gl = job["geo_location"].Value<string>();
            string domain = job["domain"].Value<string>();
            jobid = job["id"].Value<string>();
            ArrayList arRes = new ArrayList();
            try
            {
                //string username = "gpidatametrics";
                //string password = "sdV5X3fcX6";
                string username = "piapp";
                string password = "b5FCvgkjxx";

                if (status == "done")
                {
                    resURL = job["results_url"].Value<string>();
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
                        //arRes = ImagesPattern(response,urlType);
                        arRes = PageResultsPatternMobile(response, "PageLinks");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //if (seid > 0) 
                    //    ProcessResults(arRes, kw, seid, jobid );

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Result Request: " + ex.Message);

            }
            return arRes;
        }

        public ArrayList DoProcessImageResultsMobile(string resp)
        {
            JObject job = JObject.Parse(resp);
            string status = job["status"].Value<string>();
            string kw = job["query"].Value<string>();
            string device = job["user_agent_type"].Value<string>();
            string hl = job["locale"].Value<string>();
            string gl = job["geo_location"].Value<string>();
            string domain = job["domain"].Value<string>();
            jobid = job["id"].Value<string>();
            ArrayList arRes = new ArrayList();
            try
            {
                //string username = "gpidatametrics";
                //string password = "sdV5X3fcX6";
                string username = "piapp";
                string password = "b5FCvgkjxx";

                if (status == "done")
                {
                    resURL = job["results_url"].Value<string>();
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
                        //arRes = ImagesPattern(response,urlType);
                        arRes = PageResultsPatternMobile(response, "ImageLinks");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //if (seid > 0) 
                    //    ProcessResults(arRes, kw, seid, jobid );

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Result Request: " + ex.Message);

            }
            return arRes;
        }



        //public string callbackURL = "http://seresults.azurewebsites.net/api/callback/";

        private async Task<Dictionary<string, ArrayList>> GetOxylabsWebDataSources(string kwds, string domain, string location, string lang, string uule)
        {
            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            //string callbackURL = "http://seresults.azurewebsites.net/api/callback/";              
            string callbackURL = "http://callbacklivetest.azurewebsites.net/api/callback";


            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwds.Split(','),
                //query = kwds,
                limit = 100,
                pages = 1,
                locale = lang,
                callback_url = callbackURL,
                geo_location = location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = "desktop",
                context = new List<Context1> {
                    new Context1("safe_search", 0)
                }
            };

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();

            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authInfo);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(op, new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                });

                streamWriter.Write(json);
            }

            string response;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                res.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //string url = "";
            string responsedata = "";
            WebClient clnt = new WebClient();
            while (true)
            {
                clnt.Headers.Add("Content-Type", "Application/Json");
                responsedata = clnt.DownloadString(callbackURL);
                if (responsedata != "null" && !string.IsNullOrEmpty(responsedata))
                {
                    JObject job = JObject.Parse(responsedata);
                    callback_url = job["callback_url"].Value<string>();
                    break;
                }
            }

            ArrayList alresult = DoProcessDesktop(responsedata);
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            dict.Add(responsedata, alresult);
            return await Task.FromResult(dict);  // (alResult);//chang
        }

        private async Task<Dictionary<string, ArrayList>> GetOxylabsWebDataMobileSources(string kwds, string domain, string location, string lang, string uule)
        {
            //string kws = "[\"" + kwds.Replace(",", "\", \"") + "\"]";
            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            //string callbackURL = "http://seresults.azurewebsites.net/api/callback/";
            string callbackURL = "http://callbacklivetest.azurewebsites.net/api/callback";

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwds.Split(','),
                limit = 100,
                pages = 1,
                locale = lang,
                callback_url = callbackURL,
                geo_location = location,
                // uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = "mobile_android",
                context = new List<Context1> {
                    new Context1("safe_search", 0)
                }
            };

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();

            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authInfo);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(op, new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                });

                streamWriter.Write(json);
            }

            string response = string.Empty;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                res.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //string url = "";
            string responsedata;
            WebClient clnt = new WebClient();
            while (true)
            {
                clnt.Headers.Add("Content-Type", "Application/Json");
                responsedata = clnt.DownloadString(callbackURL);
                if (responsedata != "null" && !string.IsNullOrEmpty(responsedata))
                {
                    JObject job = JObject.Parse(responsedata);
                    callback_url = job["callback_url"].Value<string>();
                    break;
                }
            }

            ArrayList alresult = DoProcessMobile(responsedata);
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            dict.Add(responsedata, alresult);
            return await Task.FromResult(dict);
        }

        public async Task<Dictionary<string, ArrayList>> GetOxylabsWebDataSources_Nws_Images(string kwds, string domain, string location, string lang, string uule, string value)
        {
            //string kws = "[\"" + kwds.Replace(",", "\", \"") + "\"]";

            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            string callbackURL = "http://callbacklivetest.azurewebsites.net/api/callback";

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwds.Split(','),
                limit = 100,
                pages = 1,
                locale = lang,
                callback_url = callbackURL,
                geo_location = location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = "desktop",
                context = new List<Context1> {
                    new Context1("tbm", value),
                    new Context1("safe_search", 0)
                }
            };

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();

            req.Method = "POST";
            req.ContentType = "application/json";

            req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authInfo);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(op, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                });

                streamWriter.Write(json);
            }

            string response;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                res.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            string responsedata = string.Empty;
            WebClient clnt = new WebClient();
            while (true)
            {
                clnt.Headers.Add("Content-Type", "Application/Json");
                responsedata = clnt.DownloadString(callbackURL);
                if (responsedata != "null" && !string.IsNullOrEmpty(responsedata))
                {
                    JObject job = JObject.Parse(responsedata);
                    callback_url = job["callback_url"].Value<string>();
                    break;
                }
            }

            ArrayList alresult = DoProcessImagesDesktop(responsedata);
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            dict.Add(responsedata, alresult);
            return await Task.FromResult(dict);
        }

        public async Task<Dictionary<string, ArrayList>> GetOxylabsWebDataSources_Nws_Images1(string kwds, string domain, string location, string lang, string uule, string value)
        {
            //string kws = "[\"" + kwds.Replace(",", "\", \"") + "\"]";

            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            string callbackURL = "http://callbacklivetest.azurewebsites.net/api/callback";

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwds.Split(','),
                limit = 100,
                pages = 1,
                locale = lang,
                callback_url = callbackURL,
                geo_location = location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = "desktop",
                context = new List<Context1> {
                    new Context1("tbm", value),
                    new Context1("safe_search", 0)
                }
            };

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();

            req.Method = "POST";
            req.ContentType = "application/json";

            req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authInfo);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(op, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                });

                streamWriter.Write(json);
            }

            string response;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                res.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            string responsedata = string.Empty;
            WebClient clnt = new WebClient();
            while (true)
            {
                clnt.Headers.Add("Content-Type", "Application/Json");
                responsedata = clnt.DownloadString(callbackURL);
                if (responsedata != "null" && !string.IsNullOrEmpty(responsedata))
                {
                    JObject job = JObject.Parse(responsedata);
                    callback_url = job["callback_url"].Value<string>();
                    break;
                }
            }

            ArrayList alresult = DoProcessImagesDesktop1(responsedata);
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            dict.Add(responsedata, alresult);
            return await Task.FromResult(dict);
        }
        public async Task<Dictionary<string, ArrayList>> GetOxylabsWebDataSources_Nws_Images_Mobile(string kwds, string domain, string location, string lang, string uule, string value)
        {
            //string kws = "[\"" + kwds.Replace(",", "\", \"") + "\"]";

            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            string callbackURL = "http://callbacklivetest.azurewebsites.net/api/callback";

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwds.Split(','),
                limit = 100,
                pages = 1,
                locale = lang,
                callback_url = callbackURL,
                geo_location = location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = "mobile_android",
                context = new List<Context1> {
                    new Context1("tbm", value),
                    new Context1("safe_search", 0)
                }
            };

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();

            req.Method = "POST";
            req.ContentType = "application/json";

            req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authInfo);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(op, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                });

                streamWriter.Write(json);
            }

            string response;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                res.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            string responsedata = string.Empty;
            WebClient clnt = new WebClient();
            while (true)
            {
                clnt.Headers.Add("Content-Type", "Application/Json");
                responsedata = clnt.DownloadString(callbackURL);
                if (responsedata != "null" && !string.IsNullOrEmpty(responsedata))
                {
                    JObject job = JObject.Parse(responsedata);
                    callback_url = job["callback_url"].Value<string>();
                    break;
                }
            }

            //ArrayList alresult = DoProcessImagesMobile(responsedata);
            ArrayList alresult = DoProcessPageResultsMobile(responsedata);
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            dict.Add(responsedata, alresult);
            return await Task.FromResult(dict);
        }


        public async Task<Dictionary<string, ArrayList>> GetOxylabsWebDataSources_Nws_Images_Mobile1(string kwds, string domain, string location, string lang, string uule, string value)
        {
            //string kws = "[\"" + kwds.Replace(",", "\", \"") + "\"]";

            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            string callbackURL = "http://callbacklivetest.azurewebsites.net/api/callback";

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwds.Split(','),
                limit = 100,
                pages = 1,
                locale = lang,
                callback_url = callbackURL,
                geo_location = location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = "mobile_android",
                context = new List<Context1> {
                    new Context1("tbm", value),
                    new Context1("safe_search", 0)
                }
            };

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();

            req.Method = "POST";
            req.ContentType = "application/json";

            req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authInfo);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(op, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                });

                streamWriter.Write(json);
            }

            string response;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                res.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            string responsedata = string.Empty;
            WebClient clnt = new WebClient();
            while (true)
            {
                clnt.Headers.Add("Content-Type", "Application/Json");
                responsedata = clnt.DownloadString(callbackURL);
                if (responsedata != "null" && !string.IsNullOrEmpty(responsedata))
                {
                    JObject job = JObject.Parse(responsedata);
                    callback_url = job["callback_url"].Value<string>();
                    break;
                }
            }

            //ArrayList alresult = DoProcessImagesMobile(responsedata);
            ArrayList alresult = DoProcessImageResultsMobile(responsedata);
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            dict.Add(responsedata, alresult);
            return await Task.FromResult(dict);
        }

        public async Task<ArrayList> GetOxylabsWebDataSources_Nws_Images_Mobile2(string kwds, string domain, string location, string lang, string uule, string value)
        {
            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            string callbackURL = "http://callbacklivetest.azurewebsites.net/api/callback";

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwds.Split(','),
                limit = 100,
                pages = 1,
                locale = lang,
                callback_url = callbackURL,
                geo_location = location,
                //uule = uule,
                parse = false,
                user_agent_type = "mobile_android",
                context = new List<Context1> {
                    new Context1("tbm", value),
                    new Context1("safe_search", 0)
                }
            };

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();

            req.Method = "POST";
            req.ContentType = "application/json";

            req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authInfo);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(op, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                });

                streamWriter.Write(json);
            }

            string response;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                res.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            /* string responsedata = string.Empty;
             WebClient clnt = new WebClient();
             while (true)
             {
                 clnt.Headers.Add("Content-Type", "Application/Json");
                 responsedata = clnt.DownloadString(callbackURL);
                 if (responsedata != "null" && !string.IsNullOrEmpty(responsedata))
                 {
                     JObject job = JObject.Parse(responsedata);
                     callback_url = job["callback_url"].Value<string>();
                     break;
                 }
             }

             //ArrayList alresult = DoProcessImagesMobile(responsedata);
             //ArrayList alresult = DoProcessPageResultsMobile(responsedata);
             ArrayList alresult = new ArrayList();
             //Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
             //dict.Add(responsedata, alresult);
             //return await Task.FromResult(dict);*/

            JObject jo = JObject.Parse(response);
            var links = from p in jo["queries"] select p;
            ArrayList lst = new ArrayList();
            foreach (JToken link in links)
            {
                string kw = link["query"].Value<string>();
                string href = link["_links"][1]["href"].Value<string>();
                string status = link["status"].Value<string>();
                jobid = link["id"].Value<string>();

                string[] s = { kw, href, status, "no", jobid };    // keyword, url, status, isdownloaded.
                lst.Add(s);
            }

            if (lst.Count <= 0) return lst;
            ArrayList alResult = new ArrayList();
            do
            {
                int cnt = 0;
                foreach (string[] cbUrl in lst)
                {
                    string[] reslt = { "", "", "" };
                    response = "";

                    Uri uri = new Uri(cbUrl[1]);
                    if (cbUrl[2] == "done" && cbUrl[3] == "no")
                    {
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

                            Stream resVal = res.GetResponseStream();
                            StreamReader reader = new StreamReader(resVal, Encoding.UTF8);
                            //** Store all the contents
                            response = reader.ReadToEnd();
                            resVal.Close();
                            res.Close();

                            cbUrl[3] = "yes";
                            cnt++;

                            if (!string.IsNullOrEmpty(response))
                            {
                                reslt[0] = cbUrl[0];
                                reslt[1] = response;
                                reslt[2] = cbUrl[4];
                                alResult.Add(reslt);
                                resURL = cbUrl[1];
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Result Request: " + ex.Message);
                        }
                    }
                    else if (cbUrl[2] == "faulted" && cbUrl[3] == "no")
                    {
                        cbUrl[3] = "yes";
                        cnt++;
                    }
                    else if (cbUrl[2] == "pending" && cbUrl[3] == "no")
                    {
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri.ToString().Replace("/results", ""));
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

                            string doneresp = "";
                            using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                            {
                                doneresp = reader.ReadToEnd();
                            }
                            res.Close();

                            JObject job = JObject.Parse(doneresp);
                            string status = job["status"].Value<string>();
                            callback_url = job["callback_url"].Value<string>();
                            //resURL = job["results_url"].Value<string>();
                            cbUrl[2] = status;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Status Request: " + ex.Message);
                        }
                    }
                    else
                        cnt++;
                    //Thread.Sleep(1000);
                    Task.Delay(200).Wait();//chang
                }
                if (lst.Count == cnt) break;

            } while (true);


            return await Task.FromResult<ArrayList>(alResult);//chang
        }



        public async Task<ArrayList> GetOxylabsWebDataSources_Nws_Images2(string kwds, string domain, string location, string lang, string uule, string value)
        {
            //string kws = "[\"" + kwds.Replace(",", "\", \"") + "\"]";

            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            string callbackURL = "http://callbacklivetest.azurewebsites.net/api/callback";

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwds.Split(','),
                limit = 100,
                pages = 1,
                locale = lang,
                callback_url = callbackURL,
                geo_location = location,
                //uule = uule,
                parse = false,
                user_agent_type = "desktop",
                context = new List<Context1> {
                    new Context1("tbm", value),
                    new Context1("safe_search", 0)
                }
            };

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();

            req.Method = "POST";
            req.ContentType = "application/json";

            req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authInfo);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(op, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                });

                streamWriter.Write(json);
            }

            string response;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                res.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            JObject jo = JObject.Parse(response);
            var links = from p in jo["queries"] select p;
            ArrayList lst = new ArrayList();
            foreach (JToken link in links)
            {
                string kw = link["query"].Value<string>();
                string href = link["_links"][1]["href"].Value<string>();
                string status = link["status"].Value<string>();
                jobid = link["id"].Value<string>();

                string[] s = { kw, href, status, "no", jobid };    // keyword, url, status, isdownloaded.
                lst.Add(s);
            }

            if (lst.Count <= 0) return lst;
            ArrayList alResult = new ArrayList();
            do
            {
                int cnt = 0;
                foreach (string[] cbUrl in lst)
                {
                    string[] reslt = { "", "", "" };
                    response = "";

                    Uri uri = new Uri(cbUrl[1]);
                    if (cbUrl[2] == "done" && cbUrl[3] == "no")
                    {
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

                            Stream resVal = res.GetResponseStream();
                            StreamReader reader = new StreamReader(resVal, Encoding.UTF8);
                            //** Store all the contents
                            response = reader.ReadToEnd();
                            resVal.Close();
                            res.Close();

                            cbUrl[3] = "yes";
                            cnt++;

                            if (!string.IsNullOrEmpty(response))
                            {
                                reslt[0] = cbUrl[0];
                                reslt[1] = response;
                                reslt[2] = cbUrl[4];
                                alResult.Add(reslt);
                                resURL = cbUrl[1];
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Result Request: " + ex.Message);
                        }
                    }
                    else if (cbUrl[2] == "faulted" && cbUrl[3] == "no")
                    {
                        cbUrl[3] = "yes";
                        cnt++;
                    }
                    else if (cbUrl[2] == "pending" && cbUrl[3] == "no")
                    {
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri.ToString().Replace("/results", ""));
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

                            string doneresp = "";
                            using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                            {
                                doneresp = reader.ReadToEnd();
                            }
                            res.Close();

                            JObject job = JObject.Parse(doneresp);
                            string status = job["status"].Value<string>();
                            callback_url = job["callback_url"].Value<string>();
                            //resURL = job["results_url"].Value<string>();
                            cbUrl[2] = status;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Status Request: " + ex.Message);
                        }
                    }
                    else
                        cnt++;
                    //Thread.Sleep(1000);
                    Task.Delay(200).Wait();//chang
                }
                if (lst.Count == cnt) break;

            } while (true);


            return await Task.FromResult<ArrayList>(alResult);//chang
        }


        public async Task<ArrayList> GetOxylabsWebDataSources_Nws_ImagesDesktop(string kwds, string domain, string location, string lang, string uule, string value)
        {
            //string kws = "[\"" + kwds.Replace(",", "\", \"") + "\"]";

            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            string callbackURL = "http://callbacklivetest.azurewebsites.net/api/callback";

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwds.Split(','),
                limit = 100,
                pages = 1,
                locale = lang,
                callback_url = callbackURL,
                geo_location = location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = "desktop",
                context = new List<Context1> {
                    new Context1("tbm", value),
                    new Context1("safe_search", 0)
                }
            };

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(queryUri);
            req.Headers.Clear();

            req.Method = "POST";
            req.ContentType = "application/json";

            req.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authInfo);

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(op, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                });

                streamWriter.Write(json);
            }

            string response;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
                res.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            JObject jo = JObject.Parse(response);
            var links = from p in jo["queries"] select p;
            ArrayList lst = new ArrayList();
            foreach (JToken link in links)
            {
                string kw = link["query"].Value<string>();
                string href = link["_links"][1]["href"].Value<string>();
                string status = link["status"].Value<string>();
                jobid = link["id"].Value<string>();

                string[] s = { kw, href, status, "no", jobid };    // keyword, url, status, isdownloaded.
                lst.Add(s);
            }

            if (lst.Count <= 0) return lst;
            ArrayList alResult = new ArrayList();
            do
            {
                int cnt = 0;
                foreach (string[] cbUrl in lst)
                {
                    string[] reslt = { "", "", "" };
                    response = "";

                    Uri uri = new Uri(cbUrl[1]);
                    if (cbUrl[2] == "done" && cbUrl[3] == "no")
                    {
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

                            Stream resVal = res.GetResponseStream();
                            StreamReader reader = new StreamReader(resVal, Encoding.UTF8);
                            //** Store all the contents
                            response = reader.ReadToEnd();
                            resVal.Close();
                            res.Close();

                            cbUrl[3] = "yes";
                            cnt++;

                            if (!string.IsNullOrEmpty(response))
                            {
                                reslt[0] = cbUrl[0];
                                reslt[1] = response;
                                reslt[2] = cbUrl[4];
                                alResult.Add(reslt);
                                resURL = cbUrl[1];
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Result Request: " + ex.Message);
                        }
                    }
                    else if (cbUrl[2] == "faulted" && cbUrl[3] == "no")
                    {
                        cbUrl[3] = "yes";
                        cnt++;
                    }
                    else if (cbUrl[2] == "pending" && cbUrl[3] == "no")
                    {
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri.ToString().Replace("/results", ""));
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

                            string doneresp = "";
                            using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                            {
                                doneresp = reader.ReadToEnd();
                            }
                            res.Close();

                            JObject job = JObject.Parse(doneresp);
                            string status = job["status"].Value<string>();
                            callback_url = job["callback_url"].Value<string>();
                            //resURL = job["results_url"].Value<string>();
                            cbUrl[2] = status;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Status Request: " + ex.Message);
                        }
                    }
                    else
                        cnt++;
                    //Thread.Sleep(1000);
                    Task.Delay(200).Wait();//chang
                }
                if (lst.Count == cnt) break;

            } while (true);


            return await Task.FromResult<ArrayList>(alResult);//chang
        }




        public Dictionary<string, ArrayList> getTop100(string keyword, string seid)
        {

            Dictionary<string, ArrayList> myArrayList = new Dictionary<string, ArrayList>();
            switch (seid)
            {

                case "1":
                    {
                        myArrayList = getTop100GoogleUS(keyword).Result;
                        break;
                    }
                case "2":
                    {
                        myArrayList = getTop100GoogleUK(keyword).Result;
                        break;
                    }
                case "7":
                    {
                        myArrayList = getTop100GoogleAus(keyword).Result;
                        break;
                    }
                case "13":
                    {
                        myArrayList = getTop100GoogleNZ(keyword).Result;
                        break;
                    }
                case "16":
                    {
                        myArrayList = getTop100GoogleZA(keyword).Result;
                        break;
                    }
                case "21":
                    {
                        myArrayList = getTop100GoogleRussia(keyword).Result;
                        break;
                    }
                //case "22":
                //    {
                //        myArrayList = getTop100GoogleKorean(keyword).Result;
                //        break;
                //    }
                case "27":
                    {
                        myArrayList = getTop100GoogleIT(keyword).Result;
                        break;
                    }

                case "28":
                    {
                        myArrayList = getTop100GoogleSE(keyword).Result;
                        break;
                    }


                case "31":
                    {
                        myArrayList = this.getTop100GoogleCN(keyword).Result;
                        break;
                    }
                case "32":
                    {
                        myArrayList = this.getTop100GoogleFR(keyword).Result;
                        break;
                    }
                case "33":
                    {
                        myArrayList = this.getTop100GoogleHK(keyword).Result;
                        break;
                    }


                case "39":
                    {
                        myArrayList = this.getTop100GoogleES(keyword).Result;
                        break;
                    }
                case "40":
                    {
                        myArrayList = this.getTop100GoogleFRWeb(keyword).Result;
                        break;
                    }
                case "41":
                    {
                        myArrayList = this.getTop100GoogleSG(keyword).Result;
                        break;
                    }

                case "44":
                    {
                        myArrayList = this.getTop100GoogleBEInternet(keyword).Result;
                        break;
                    }
                case "45":
                    {
                        myArrayList = this.getTop100GoogleBENederlands(keyword).Result;
                        break;
                    }
                case "46":
                    {
                        myArrayList = this.getTop100GoogleBEBelgie(keyword).Result;
                        break;
                    }

                case "47":
                    {
                        myArrayList = this.getTop100GoogleJapan(keyword).Result;
                        break;
                    }
                case "49":
                    {
                        myArrayList = this.getTop100GoogleBrasil(keyword).Result;
                        break;
                    }
                case "51":
                    {
                        myArrayList = this.getTop100GoogleNederlands(keyword).Result;
                        break;
                    }
                case "52":
                    {
                        myArrayList = this.getTop100GoogleDE(keyword).Result;
                        break;
                    }
                case "53":
                    {
                        myArrayList = this.getTop100GoogleCH(keyword).Result;
                        break;
                    }
                case "54":
                    {
                        myArrayList = this.getTop100GoogleLU(keyword).Result;
                        break;
                    }
                case "55":
                    {
                        myArrayList = this.getTop100GoogleAT(keyword).Result;
                        break;
                    }

                case "57":
                    {
                        myArrayList = this.getTop100GoogleNederlandsInternet(keyword).Result;
                        break;
                    }
                case "58":
                    {
                        myArrayList = this.getTop100GoogleUKInternet(keyword).Result;
                        break;
                    }
                case "59":
                    {
                        myArrayList = this.getTop100GoogleESInternet(keyword).Result;
                        break;
                    }
                case "60":
                    {
                        myArrayList = this.getTop100GoogleITInternet(keyword).Result;
                        break;
                    }
                case "61":
                    {
                        myArrayList = this.getTop100GoogleSEInternet(keyword).Result;
                        break;
                    }
                case "62":
                    {
                        myArrayList = this.getTop100GoogleCNInternet(keyword).Result;
                        break;
                    }
                case "63":
                    {
                        myArrayList = this.getTop100GoogleHKInternet(keyword).Result;
                        break;
                    }
                case "64":
                    {
                        myArrayList = this.getTop100GoogleSGInternet(keyword).Result;
                        break;
                    }
                case "65":
                    {
                        myArrayList = this.getTop100GoogleJapanInternet(keyword).Result;
                        break;
                    }
                case "66":
                    {
                        myArrayList = this.getTop100GoogleBrasilInternet(keyword).Result;
                        break;
                    }
                case "67":
                    {
                        myArrayList = this.getTop100GoogleDEInternet(keyword).Result;
                        break;
                    }
                case "68":
                    {
                        myArrayList = this.getTop100GoogleCHInternet(keyword).Result;
                        break;
                    }
                case "69":
                    {
                        myArrayList = this.getTop100GoogleLUInternet(keyword).Result;
                        break;
                    }
                case "70":
                    {
                        myArrayList = this.getTop100GoogleATInternet(keyword).Result;
                        break;
                    }
                case "74":
                    {
                        myArrayList = this.GetTop100GoogleUKImages_PageURLs(keyword).Result; ;//desktop pattern
                        break;
                    }

                ////case "75":
                ////    {
                ////        myArrayList = this.getTop100GoogleUKCaffeine(keyword).Result;
                ////        break;
                ////    }

                case "77":
                    {
                        myArrayList = this.getTop100GoogleAE(keyword).Result;
                        break;
                    }

                case "79":
                    {
                        myArrayList = this.getTop100GoogleTR(keyword).Result;
                        break;
                    }
                case "80":
                    {
                        myArrayList = this.getTop100GoogleMA(keyword).Result;
                        break;
                    }
                case "81":
                    {
                        myArrayList = this.getTop100GoogleLY(keyword).Result;
                        break;
                    }
                case "82":
                    {
                        myArrayList = this.getTop100GoogleDZ(keyword).Result;
                        break;
                    }
                case "83":
                    {
                        myArrayList = this.getTop100GoogleUA(keyword).Result;
                        break;
                    }
                case "84":
                    {
                        myArrayList = this.getTop100GoogleEgypt(keyword).Result;
                        break;
                    }
                case "85":
                    {
                        myArrayList = this.getTop100GoogleBH(keyword).Result;
                        break;
                    }
                case "86":
                    {
                        myArrayList = this.getTop100GoogleQA(keyword).Result;
                        break;
                    }
                case "87":
                    {
                        myArrayList = this.getTop100GoogleSaudi(keyword).Result;
                        break;
                    }
                case "88":
                    {
                        myArrayList = this.getTop100GoogleKW(keyword).Result;
                        break;
                    }
                case "89":
                    {
                        myArrayList = this.getTop100GoogleVN(keyword).Result;
                        break;
                    }
                case "90":
                    {
                        myArrayList = this.getTop100GoogleGabon(keyword).Result;
                        break;
                    }

                case "93":
                    {
                        myArrayList = this.getTop100GoogleIN(keyword).Result;
                        break;
                    }

                case "95":
                    {
                        myArrayList = this.getTop100GooglePT(keyword).Result;
                        break;
                    }
                case "96":
                    {
                        myArrayList = this.getTop100GoogleCZ(keyword).Result;
                        break;
                    }
                case "97":
                    {
                        myArrayList = this.getTop100GoogleSK(keyword).Result;
                        break;
                    }
                case "98":
                    {
                        myArrayList = this.getTop100GoogleIL(keyword).Result;
                        break;
                    }
                case "99":
                    {
                        myArrayList = this.getTop100GoogleUSLocal(keyword).Result;
                        break;
                    }
                //case "100":
                //    {
                //        myArrayList = this.getTop100GoogleAusLocal(keyword).Result;
                //        break;
                //    }
                case "101":
                    {
                        myArrayList = this.getTop100GoogleZALocal(keyword).Result;
                        break;
                    }
                case "102":
                    {
                        myArrayList = this.getTop100GoogleUSMobile(keyword).Result;
                        break;
                    }
                case "104":
                    {
                        myArrayList = this.getTop100GoogleITMobile(keyword).Result;
                        break;
                    }
                case "105":
                    {
                        myArrayList = this.getTop100GoogleIELocal(keyword).Result;
                        break;
                    }
                case "106":
                    {
                        myArrayList = this.getTop100GoogleUKMobileWeb(keyword).Result;
                        break;
                    }
                case "107":
                    {
                        myArrayList = this.getTop100GoogleCanada(keyword).Result;
                        break;
                    }
                //case "108":
                //    {
                //        myArrayList = this.getTop100GoogleMY(keyword).Result;
                //        break;
                //    }
                case "109":
                    {
                        myArrayList = this.getTop100GoogleMYWeb(keyword).Result;
                        break;
                    }
                ////case "110":
                ////    {
                ////        myArrayList = this.getTop100GoogleTH(keyword).Result;
                ////        break;
                ////    }
                case "111":
                    {
                        myArrayList = this.getTop100GoogleTHWeb(keyword).Result;
                        break;
                    }
                ////case "112":
                ////    {
                ////        myArrayList = this.getTop100GoogleNG(keyword).Result;
                ////        break;
                ////    }
                case "113":
                    {
                        myArrayList = this.getTop100GoogleNGWeb(keyword).Result;
                        break;
                    }
                case "114":
                    {
                        myArrayList = this.getTop100GoogleCHItalino(keyword).Result;
                        break;
                    }
                case "115":
                    {
                        myArrayList = this.getTop100GoogleCHFrancais(keyword).Result;
                        break;
                    }
                case "116":
                    {
                        myArrayList = this.getTop100GoogleCHDeutsch(keyword).Result;
                        break;
                    }
                case "117":
                    {
                        myArrayList = this.getTop100GooglePTMobile(keyword).Result;
                        break;
                    }
                case "118":
                    {
                        myArrayList = this.getTop100GoogleNederlandsMobile(keyword).Result;
                        break;
                    }
                case "119":
                    {
                        myArrayList = this.getTop100GoogleMexico(keyword).Result;
                        break;
                    }
                case "120":
                    {
                        myArrayList = this.getTop100GoogleESMobile(keyword).Result;
                        break;
                    }

                case "121":
                    {
                        myArrayList = this.getTop100GoogleAusInternet(keyword).Result;
                        break;
                    }
                case "122":
                    {
                        myArrayList = this.getTop100GooglePolskiInternet(keyword).Result;
                        break;
                    }
                case "123":
                    {
                        myArrayList = this.getTop100GoogleRomaniaInternet(keyword).Result;
                        break;
                    }
                case "124":
                    {
                        myArrayList = this.getTop100GoogleBulgariaInternet(keyword).Result;
                        break;
                    }
                case "125":
                    {
                        myArrayList = this.getTop100GoogleSlovenijaInternet(keyword).Result;
                        break;
                    }
                case "126":
                    {
                        myArrayList = this.getTop100GoogleMagyaInternet(keyword).Result;
                        break;
                    }

                case "127":
                    {
                        myArrayList = this.getTop100GoogleCzechInternet(keyword).Result;
                        break;
                    }
                case "128":
                    {
                        myArrayList = this.getTop100GoogleSlovakiaInternet(keyword).Result;
                        break;
                    }

                case "129":
                    {
                        myArrayList = this.getTop100GoogleArgentinaInternet(keyword).Result;
                        break;
                    }
                case "130":
                    {
                        myArrayList = this.getTop100GoogleLebanonInternet(keyword).Result;
                        break;
                    }
                case "131":
                    {
                        myArrayList = this.getTop100GoogleJordanInternet(keyword).Result;
                        break;
                    }
                case "132":
                    {
                        myArrayList = this.getTop100GoogleFinlandInternet(keyword).Result;
                        break;
                    }
                case "133":
                    {
                        myArrayList = this.getTop100GoogleGreeceInternet(keyword).Result;
                        break;
                    }
                case "134":
                    {
                        myArrayList = this.getTop100GoogleNorwayInternet(keyword).Result;
                        break;
                    }
                case "135":
                    {
                        myArrayList = this.getTop100GoogleBelgiumInternet(keyword).Result;
                        break;
                    }
                case "136":
                    {
                        myArrayList = this.getTop100GoogleDenmarkInternet(keyword).Result;
                        break;
                    }
                case "137":
                    {
                        myArrayList = this.getTop100GoogleChileInternet(keyword).Result;
                        break;
                    }
                case "139":
                    {
                        myArrayList = this.getTop100GoogleAusMobile(keyword).Result;
                        break;
                    }

                //case "140":
                //    {
                //        myArrayList = this.getTop100GoogleUKNews(keyword).Result;
                //        break;
                //    }
                case "141":
                    {
                        myArrayList = this.getTop100GoogleCHItalinoMobile(keyword).Result;
                        break;
                    }
                case "142":
                    {
                        myArrayList = this.getTop100GoogleCHFrancaisMobile(keyword).Result;
                        break;
                    }
                case "143":
                    {
                        myArrayList = this.getTop100GoogleCHDeutschMobile(keyword).Result;
                        break;
                    }

                case "144":
                    {
                        myArrayList = this.getTop100GooglePTInternet(keyword).Result;
                        break;
                    }
                case "145":
                    {
                        myArrayList = this.getTop100GoogleDEMobile(keyword).Result;
                        break;
                    }
                case "146":
                    {
                        myArrayList = this.getTop100GoogleAEMobile(keyword).Result;
                        break;
                    }
                case "147":
                    {
                        myArrayList = this.getTop100GoogleATMobile(keyword).Result;
                        break;
                    }
                case "148":
                    {
                        myArrayList = this.getTop100GoogleBEDutchMobile(keyword).Result;
                        break;
                    }
                case "149":
                    {
                        myArrayList = this.getTop100GoogleBEFrenchMobile(keyword).Result;
                        break;
                    }
                case "150":
                    {
                        myArrayList = this.getTop100GoogleBrasilMobile(keyword).Result;
                        break;
                    }
                case "151":
                    {
                        myArrayList = this.getTop100GoogleCanadaMobile(keyword).Result;
                        break;
                    }
                case "152":
                    {
                        myArrayList = this.getTop100GoogleDenmarkMobile(keyword).Result;
                        break;
                    }

                case "153":
                    {
                        myArrayList = this.getTop100GoogleFinlandMobile(keyword).Result;
                        break;
                    }
                case "154":
                    {
                        myArrayList = this.getTop100GoogleFRMobile(keyword).Result;
                        break;
                    }
                case "155":
                    {
                        myArrayList = this.getTop100GoogleGreeceMobile(keyword).Result;
                        break;
                    }
                case "156":
                    {
                        myArrayList = this.getTop100GoogleHKMobile(keyword).Result;
                        break;
                    }
                case "157":
                    {
                        myArrayList = this.getTop100GoogleIEMobile(keyword).Result;
                        break;
                    }

                case "158":
                    {
                        myArrayList = this.getTop100GoogleJapanMobile(keyword).Result;
                        break;
                    }
                case "159":
                    {
                        myArrayList = this.getTop100GoogleNorwayMobile(keyword).Result;
                        break;
                    }
                case "160":
                    {
                        myArrayList = this.getTop100GoogleSEMobile(keyword).Result;
                        break;
                    }
                case "167":
                    {
                        myArrayList = this.getTop100GoogleHungaryMobileWeb(keyword).Result;
                        break;
                    }
                case "168":
                    {
                        myArrayList = this.getTop100GooglePolandMobileWeb(keyword).Result;
                        break;
                    }
                case "169":
                    {
                        myArrayList = this.getTop100GoogleTurkeyMobileWeb(keyword).Result;
                        break;
                    }
                case "172":
                    {
                        myArrayList = this.getTop100GoogleRussiaMobile(keyword).Result;
                        break;
                    }
                case "173":
                    {
                        myArrayList = this.getTop100GoogleTWInternet(keyword).Result;
                        break;
                    }
                case "174":
                    {
                        myArrayList = this.getTop100GoogleILInternet(keyword).Result;
                        break;
                    }

                case "176":
                    {
                        myArrayList = this.getTop100GoogleSerbiaInternet(keyword).Result;
                        break;
                    }
                case "177":
                    {
                        myArrayList = this.getTop100GoogleUSNewYorkCity(keyword).Result;
                        break;
                    }
                case "178":
                    {
                        myArrayList = this.getTop100GoogleUSNewYorkCityMobile(keyword).Result;
                        break;
                    }
                case "179":
                    {
                        myArrayList = this.getTop100GoogleUSLA(keyword).Result;
                        break;
                    }
                case "180":
                    {
                        myArrayList = this.getTop100GoogleUSLAMobile(keyword).Result;
                        break;
                    }
                case "181":
                    {
                        myArrayList = this.getTop100GoogleZAMobile(keyword).Result;
                        break;
                    }
                case "182":
                    {
                        myArrayList = this.getTop100GoogleSaudiMobileInternet(keyword).Result;
                        break;
                    }
                case "183":
                    {
                        myArrayList = this.getTop100GoogleCanadaFrenchWeb(keyword).Result;
                        break;
                    }

                case "184":
                    {
                        myArrayList = this.getTop100GoogleUSChicago(keyword).Result;
                        break;
                    }
                case "185":
                    {
                        myArrayList = this.getTop100GoogleUSChicagoMobile(keyword).Result;
                        break;
                    }
                case "186":
                    {
                        myArrayList = this.getTop100GoogleUSSanFran(keyword).Result;
                        break;
                    }
                case "187":
                    {
                        myArrayList = this.getTop100GoogleUSSanFranMobile(keyword).Result;
                        break;
                    }
                case "188":
                    {
                        myArrayList = this.getTop100GooglePakistanInternet(keyword).Result;
                        break;
                    }
                case "189":
                    {
                        myArrayList = this.getTop100GooglePakistanMobileInternet(keyword).Result;
                        break;
                    }

                case "204":
                    {
                        myArrayList = this.getTop100GoogleINMobileWeb(keyword).Result;
                        break;
                    }
                case "206":
                    {
                        myArrayList = this.getTop100GoogleCanadaFrenchMobileInternet(keyword).Result;
                        break;
                    }
                case "207":
                    {
                        myArrayList = this.getTop100GoogleMYMobileInternet(keyword).Result;
                        break;
                    }
                case "208":
                    {
                        myArrayList = this.getTop100GoogleSGMobileInternet(keyword).Result;
                        break;
                    }

                case "209":
                    {
                        myArrayList = this.getTop100GoogleCanada1(keyword).Result;
                        break;
                    }
                case "210":
                    {
                        myArrayList = this.getTop100GoogleCanadaMobile1(keyword).Result;
                        break;
                    }
                case "211":
                    {
                        myArrayList = this.getTop100GoogleSrilankaMobileInternet(keyword).Result;
                        break;
                    }
                case "212":
                    {
                        myArrayList = this.getTop100GoogleSrilankaInternet(keyword).Result;
                        break;
                    }
                case "213":
                    {
                        myArrayList = this.getTop100GoogleHRInternet(keyword).Result;
                        break;
                    }
                case "214":
                    {
                        myArrayList = this.getTop100GoogleHRMobileInternet(keyword).Result;
                        break;
                    }
                case "215":
                    {
                        myArrayList = this.getTop100GoogleCZMobileInternet(keyword).Result;
                        break;
                    }
                case "219":
                    {
                        myArrayList = this.getTop100GoogleTHMobile(keyword).Result;
                        break;
                    }
                case "223":
                    {
                        myArrayList = this.getTop100GoogleUSHoustonCity(keyword).Result;
                        break;
                    }
                case "224":
                    {
                        myArrayList = this.getTop100GoogleUSHoustonCityMobile(keyword).Result;
                        break;
                    }
                case "225":
                    {
                        myArrayList = this.getTop100GoogleUSMiamiCity(keyword).Result;
                        break;
                    }
                case "226":
                    {
                        myArrayList = this.getTop100GoogleUSMiamiCityMobile(keyword).Result;
                        break;
                    }
                case "233":
                    {
                        myArrayList = this.getTop100GoogleAEEnglish(keyword).Result;
                        break;
                    }
                case "234":
                    {
                        myArrayList = this.getTop100GoogleHKInternetEnglish(keyword).Result;
                        break;
                    }
                case "235":
                    {
                        myArrayList = this.getTop100GoogleMexicoMobile(keyword).Result;
                        break;
                    }
                case "236":
                    {
                        myArrayList = this.getTop100GoogleMexicoCityMobile(keyword).Result;
                        break;
                    }
                case "237":
                    {
                        myArrayList = this.getTop100GoogleChileInternetMobile(keyword).Result;
                        break;
                    }
                case "238":
                    {
                        myArrayList = this.getTop100GooglePeruInternet(keyword).Result;
                        break;
                    }
                case "239":
                    {
                        myArrayList = this.getTop100GooglePeruInternetMobile(keyword).Result;
                        break;
                    }
                case "240":
                    {
                        myArrayList = this.getTop100GoogleColumbiaInternet(keyword).Result;
                        break;
                    }
                case "241":
                    {
                        myArrayList = this.getTop100GoogleColumbiaInternetMobile(keyword).Result;
                        break;
                    }

                case "242":
                    {
                        myArrayList = this.getTop100GoogleMexicoCity(keyword).Result;
                        break;
                    }
                case "243":
                    {
                        myArrayList = this.getTop100GoogleIndonesiaInternet(keyword).Result;
                        break;
                    }
                case "244":
                    {
                        myArrayList = this.getTop100GoogleIndonesiaInternetMobile(keyword).Result;
                        break;
                    }
                case "245":
                    {
                        myArrayList = this.getTop100GooglePhilippinesInternet(keyword).Result;
                        break;
                    }
                case "246":
                    {
                        myArrayList = this.getTop100GooglePhilippinesInternetMobile(keyword).Result;
                        break;
                    }
                case "247":
                    {
                        myArrayList = this.getTop100GoogleSGMobileInternetEnglish(keyword).Result;
                        break;
                    }
                case "248":
                    {
                        myArrayList = this.getTop100GoogleTHMobileInternetEnglish(keyword).Result;
                        break;
                    }
                case "249":
                    {
                        myArrayList = this.getTop100GoogleHKMobileInternetEnglish(keyword).Result;
                        break;
                    }
                case "250":
                    {
                        myArrayList = this.getTop100GoogleMYInternetLanguage(keyword).Result;
                        break;
                    }
                case "251":
                    {
                        myArrayList = this.getTop100GoogleMYMobileInternetLanguage(keyword).Result;
                        break;
                    }
                case "252":
                    {
                        myArrayList = this.getTop100GoogleTHInternetEnglish(keyword).Result;
                        break;
                    }
                case "253":
                    {
                        myArrayList = this.getTop100GoogleSGInternetLanguage(keyword).Result;
                        break;
                    }
                case "254":
                    {
                        myArrayList = this.getTop100GoogleHKInternetChinaLanguage(keyword).Result;
                        break;
                    }
                case "255":
                    {
                        myArrayList = this.getTop100GoogleHKMobileInternetChinaLanguage(keyword).Result;
                        break;
                    }

                case "257":
                    {
                        myArrayList = this.getTop100GoogleNZInternetMobile(keyword).Result;
                        break;
                    }
                case "258":
                    {
                        myArrayList = this.getTop100GoogleTWInternetMobile(keyword).Result;
                        break;
                    }
                case "259":
                    {
                        myArrayList = this.getTop100GoogleAEEnglishInternetMobile(keyword).Result;
                        break;
                    }

                case "260":
                    {
                        myArrayList = this.getTop100GoogleJapanInternetEnglish(keyword).Result;
                        break;
                    }
                case "261":
                    {
                        myArrayList = this.getTop100GoogleJapanMobileEnglish(keyword).Result;
                        break;
                    }
                case "262":
                    {
                        myArrayList = this.getTop100GoogleDenmarkInternetEnglish(keyword).Result;
                        break;
                    }
                case "263":
                    {
                        myArrayList = this.getTop100GoogleDenmarkMobileEnglish(keyword).Result;
                        break;
                    }
                case "264":
                    {
                        myArrayList = this.getTop100GoogleSEInternetEnglish(keyword).Result;
                        break;
                    }
                case "265":
                    {
                        myArrayList = this.getTop100GoogleSEMobileEnglish(keyword).Result;
                        break;
                    }
                case "266":
                    {
                        myArrayList = this.getTop100GoogleNorwayInternetEnglish(keyword).Result;
                        break;
                    }
                case "267":
                    {
                        myArrayList = this.getTop100GoogleNorwayMobileEnglish(keyword).Result;
                        break;
                    }
                case "268":
                    {
                        myArrayList = this.getTop100GoogleFinlandInternetEnglish(keyword).Result;
                        break;
                    }
                case "269":
                    {
                        myArrayList = this.getTop100GoogleFinlandMobileEnglish(keyword).Result;
                        break;
                    }
                case "270":
                    {
                        myArrayList = this.getTop100GoogleDEInternetEnglish(keyword).Result;
                        break;
                    }
                case "271":
                    {
                        myArrayList = this.getTop100GoogleDEMobileEnglish(keyword).Result;
                        break;
                    }
                case "272":
                    {
                        myArrayList = this.getTop100GoogleFRInternetEnglish(keyword).Result;
                        break;
                    }
                case "273":
                    {
                        myArrayList = this.getTop100GoogleFRMobileEnglish(keyword).Result;
                        break;
                    }
                case "274":
                    {
                        myArrayList = this.getTop100GoogleITInternetEnglish(keyword).Result;
                        break;
                    }
                case "275":
                    {
                        myArrayList = this.getTop100GoogleITMobileEnglish(keyword).Result;
                        break;
                    }

                case "279":
                    {
                        myArrayList = this.getTop100GoogleLondonCityDesktop(keyword).Result;
                        break;
                    }
                case "280":
                    {
                        myArrayList = this.getTop100GoogleLondonCityMobile(keyword).Result;
                        break;
                    }
                case "281":
                    {
                        myArrayList = this.getTop100GoogleBirminghamCityDesktop(keyword).Result;
                        break;
                    }
                case "282":
                    {
                        myArrayList = this.getTop100GoogleBirminghamCityMobile(keyword).Result;
                        break;
                    }
                case "283":
                    {
                        myArrayList = this.getTop100GoogleLeedsCityDesktop(keyword).Result;
                        break;
                    }
                case "284":
                    {
                        myArrayList = this.getTop100GoogleLeedsCityMobile(keyword).Result;
                        break;
                    }

                case "285":
                    {
                        myArrayList = this.getTop100GoogleSheffieldCityDesktop(keyword).Result;
                        break;
                    }
                case "286":
                    {
                        myArrayList = this.getTop100GoogleSheffieldCityMobile(keyword).Result;
                        break;
                    }
                case "287":
                    {
                        myArrayList = this.getTop100GoogleBradfordCityDesktop(keyword).Result;
                        break;
                    }
                case "288":
                    {
                        myArrayList = this.getTop100GoogleBradfordCityMobile(keyword).Result;
                        break;
                    }
                case "289":
                    {
                        myArrayList = this.getTop100GoogleManchesterCityDesktop(keyword).Result;
                        break;
                    }
                case "290":
                    {
                        myArrayList = this.getTop100GoogleManchesterCityMobile(keyword).Result;
                        break;
                    }
                case "291":
                    {
                        myArrayList = this.getTop100GoogleLiverpoolCityDesktop(keyword).Result;
                        break;
                    }
                case "292":
                    {
                        myArrayList = this.getTop100GoogleLiverpoolCityMobile(keyword).Result;
                        break;
                    }
                case "293":
                    {
                        myArrayList = this.getTop100GoogleBristolCityDesktop(keyword).Result;
                        break;
                    }
                case "294":
                    {
                        myArrayList = this.getTop100GoogleBristolCityMobile(keyword).Result;
                        break;
                    }
                case "295":
                    {
                        myArrayList = this.getTop100GoogleNewcastleCityDesktop(keyword).Result;
                        break;
                    }
                case "296":
                    {
                        myArrayList = this.getTop100GoogleNewcastleCityMobile(keyword).Result;
                        break;
                    }

                case "297":
                    {
                        myArrayList = this.getTop100GoogleSunderlandCityDesktop(keyword).Result;
                        break;
                    }
                case "298":
                    {
                        myArrayList = this.getTop100GoogleSunderlandCityMobile(keyword).Result;
                        break;
                    }
                case "299":
                    {
                        myArrayList = this.getTop100GoogleWolverhamptonCityDesktop(keyword).Result;
                        break;
                    }
                case "300":
                    {
                        myArrayList = this.getTop100GoogleWolverhamptonCityMobile(keyword).Result;
                        break;
                    }
                case "301":
                    {
                        myArrayList = this.getTop100GooglePlymouthCityDesktop(keyword).Result;
                        break;
                    }
                case "302":
                    {
                        myArrayList = this.getTop100GooglePlymouthCityMobile(keyword).Result;
                        break;
                    }
                case "303":
                    {
                        myArrayList = this.getTop100GoogleCardiffCityDesktop(keyword).Result;
                        break;
                    }
                case "304":
                    {
                        myArrayList = this.getTop100GoogleCardiffCityMobile(keyword).Result;
                        break;
                    }
                case "305":
                    {
                        myArrayList = this.getTop100GoogleOxfordCityDesktop(keyword).Result;
                        break;
                    }
                case "306":
                    {
                        myArrayList = this.getTop100GoogleOxfordCityMobile(keyword).Result;
                        break;
                    }
                case "307":
                    {
                        myArrayList = this.getTop100GoogleCambridgeCityDesktop(keyword).Result;
                        break;
                    }
                case "308":
                    {
                        myArrayList = this.getTop100GoogleCambridgeCityMobile(keyword).Result;
                        break;
                    }
                case "309":
                    {
                        myArrayList = this.getTop100GoogleBelfastCityDesktop(keyword).Result;
                        break;
                    }
                case "310":
                    {
                        myArrayList = this.getTop100GoogleBelfastCityMobile(keyword).Result;
                        break;
                    }
                case "311":
                    {
                        myArrayList = this.getTop100GoogleGlasgowCityDesktop(keyword).Result;
                        break;
                    }
                case "312":
                    {
                        myArrayList = this.getTop100GoogleGlasgowCityMobile(keyword).Result;
                        break;
                    }
                case "313":
                    {
                        myArrayList = this.getTop100GoogleEdinburghCityDesktop(keyword).Result;
                        break;
                    }
                case "314":
                    {
                        myArrayList = this.getTop100GoogleEdinburghCityMobile(keyword).Result;
                        break;
                    }
                case "315":
                    {
                        myArrayList = this.getTop100GoogleBrightonCityDesktop(keyword).Result;
                        break;
                    }
                case "316":
                    {
                        myArrayList = this.getTop100GoogleBrightonCityMobile(keyword).Result;
                        break;
                    }
                case "317":
                    {
                        myArrayList = this.getTop100GoogleHoveCityDesktop(keyword).Result;
                        break;
                    }
                case "318":
                    {
                        myArrayList = this.getTop100GoogleHoveCityMobile(keyword).Result;
                        break;
                    }
                case "319":
                    {
                        myArrayList = this.getTop100GoogleSouthamptonCityDesktop(keyword).Result;
                        break;
                    }
                case "320":
                    {
                        myArrayList = this.getTop100GoogleSouthamptonCityMobile(keyword).Result;
                        break;
                    }
                case "321":
                    {
                        myArrayList = this.getTop100GooglePhilippinesInternetEnglish(keyword).Result;
                        break;
                    }
                case "322":
                    {
                        myArrayList = this.getTop100GooglePhilippinesInternetMobileEnglish(keyword).Result;
                        break;
                    }
                case "323":
                    {
                        myArrayList = this.getTop100GoogleBahamasInternetEnglish(keyword).Result;
                        break;
                    }
                case "324":
                    {
                        myArrayList = this.getTop100GoogleBahamasInternetMobileEnglish(keyword).Result;
                        break;
                    }
                case "325":
                    {
                        myArrayList = this.getTop100GoogleJamaicaInternetEnglish(keyword).Result;
                        break;
                    }
                case "326":
                    {
                        myArrayList = this.getTop100GoogleJamaicaInternetMobileEnglish(keyword).Result;
                        break;
                    }
                case "327":
                    {
                        myArrayList = this.getTop100GoogleMexicoInternetEnglish(keyword).Result;
                        break;
                    }
                case "328":
                    {
                        myArrayList = this.getTop100GoogleMexicoInternetMobileEnglish(keyword).Result;
                        break;
                    }
                case "329":
                    {
                        myArrayList = this.getTop100GooglePuertoRicoInternetEnglish(keyword).Result;
                        break;
                    }
                case "330":
                    {
                        myArrayList = this.getTop100GooglePuertoRicoInternetMobileEnglish(keyword).Result;
                        break;
                    }
                case "331":
                    {
                        myArrayList = this.getTop100GooglePuertoRicoInternetSpanish(keyword).Result;
                        break;
                    }
                case "332":
                    {
                        myArrayList = this.getTop100GooglePuertoRicoInternetMobileSpanish(keyword).Result;
                        break;
                    }
                case "333":
                    {
                        myArrayList = this.getTop100GoogleCanadaTorontoCityDesktop(keyword).Result;
                        break;
                    }
                case "334":
                    {
                        myArrayList = this.getTop100GoogleCanadaTorontoCityMobile(keyword).Result;
                        break;
                    }
                case "335":
                    {
                        myArrayList = this.getTop100GoogleVenezuela(keyword).Result;
                        break;
                    }
                case "336":
                    {
                        myArrayList = this.getTop100GoogleVenezuelaMobile(keyword).Result;
                        break;
                    }
                case "337":
                    {
                        myArrayList = this.getTop100GoogleArgentinaInternetMobile(keyword).Result;
                        break;
                    }
                case "338":
                    {
                        myArrayList = this.getTop100GoogleDallasTexasCityDesktop(keyword).Result;
                        break;
                    }
                case "339":
                    {
                        myArrayList = this.getTop100GoogleDallasTexasCityMobile(keyword).Result;
                        break;
                    }
                case "350":
                    {
                        myArrayList = this.getTop100GoogleNGMobile(keyword).Result;
                        break;
                    }
                case "351":
                    {
                        myArrayList = this.getTop100GoogleKenya(keyword).Result;
                        break;
                    }
                case "352":
                    {
                        myArrayList = this.getTop100GoogleKenyaMobile(keyword).Result;
                        break;
                    }
                case "353":
                    {
                        myArrayList = this.getTop100GoogleSydneyCityDesktop(keyword).Result;
                        break;
                    }
                case "354":
                    {
                        myArrayList = this.getTop100GoogleMelbourneCityDesktop(keyword).Result;
                        break;
                    }
                case "355":
                    {
                        myArrayList = this.getTop100GoogleBrisbanCityDesktop(keyword).Result;
                        break;
                    }
                case "356":
                    {
                        myArrayList = this.getTop100GooglePerthCityDesktop(keyword).Result;
                        break;
                    }
                case "357":
                    {
                        myArrayList = this.getTop100GoogleAdelaideCityDesktop(keyword).Result;
                        break;
                    }
                case "358":
                    {
                        myArrayList = this.getTop100GoogleSydneyCityMobile(keyword).Result;
                        break;
                    }
                case "359":
                    {
                        myArrayList = this.getTop100GoogleMelbourneCityMobile(keyword).Result;
                        break;
                    }
                case "360":
                    {
                        myArrayList = this.getTop100GoogleBrisbanCityMobile(keyword).Result;
                        break;
                    }
                case "361":
                    {
                        myArrayList = this.getTop100GooglePerthCityMobile(keyword).Result;
                        break;
                    }
                case "362":
                    {
                        myArrayList = this.getTop100GoogleAdelaideCityMobile(keyword).Result;
                        break;
                    }
                case "363":
                    {
                        myArrayList = this.GetTop100GoogleBangladeshInternet(keyword).Result;
                        break;
                    }
                case "364":
                    {
                        myArrayList = this.GetTop100GoogleBangladeshMobile(keyword).Result;
                        break;
                    }
                case "365":
                    {
                        myArrayList = this.getTop100GoogleLatviaDesktop(keyword).Result;
                        break;
                    }
                case "366":
                    {
                        myArrayList = this.getTop100GoogleLatviaMobile(keyword).Result;
                        break;
                    }
                case "367":
                    {
                        myArrayList = this.getTop100GoogleAspenColoradoDesktop(keyword).Result;
                        break;
                    }
                case "368":
                    {
                        myArrayList = this.getTop100GoogleAspenColoradoMobile(keyword).Result;
                        break;
                    }
                case "369":
                    {
                        myArrayList = this.getTop100GoogleNapaCaliforniaDesktop(keyword).Result;
                        break;
                    }
                case "370":
                    {
                        myArrayList = this.getTop100GoogleNapaCaliforniaMobile(keyword).Result;
                        break;
                    }
                case "371":
                    {
                        myArrayList = this.getTop100GoogleOmanArabicDesktop(keyword).Result;
                        break;
                    }
                case "372":
                    {
                        myArrayList = this.getTop100GoogleOmanArabicMobile(keyword).Result;
                        break;
                    }
                case "373":
                    {
                        myArrayList = this.getTop100GoogleOmanEnglishDesktop(keyword).Result;
                        break;
                    }
                case "374":
                    {
                        myArrayList = this.getTop100GoogleOmanEnglishMobile(keyword).Result;
                        break;
                    }
                case "381":
                    {
                        myArrayList = this.GetTop100GoogleUKImages_ImageURLs(keyword).Result;//desktop pattern
                        break;
                    }
                case "382":
                    {
                        myArrayList = this.GetTop100GoogleUKMobileImages_PageURLs(keyword).Result;//mobile pattern
                        break;
                    }

                case "383":
                    {
                        myArrayList = this.GetTop100GoogleUKMobileImages_ImageURLs(keyword).Result;//mobile pattern
                        break;
                    }
                case "384":
                    {
                        myArrayList = this.GetTop100GoogleHemelHempsteadCityDesktop(keyword).Result;
                        break;
                    }
                case "385":
                    {
                        myArrayList = this.GetTop100GoogleHemelHempsteadCityMobile(keyword).Result;
                        break;
                    }
                case "386":
                    {
                        myArrayList = this.GetTop100GoogleLeicesterCityDesktop(keyword).Result;
                        break;
                    }
                case "387":
                    {
                        myArrayList = this.GetTop100GoogleLeicesterCityMobile(keyword).Result;
                        break;
                    }
                case "388":
                    {
                        myArrayList = this.GetTop100GoogleNottinghamCityDesktop(keyword).Result;
                        break;
                    }
                case "389":
                    {
                        myArrayList = this.GetTop100GoogleNottinghamCityMobile(keyword).Result;
                        break;
                    }
                case "390":
                    {
                        myArrayList = this.GetTop100GooglePortsmouthCityDesktop(keyword).Result;
                        break;
                    }
                case "391":
                    {
                        myArrayList = this.GetTop100GooglePortsmouthCityMobile(keyword).Result;
                        break;
                    }
                case "392":
                    {
                        myArrayList = this.GetTop100GoogleReadingCityDesktop(keyword).Result;
                        break;
                    }
                case "393":
                    {
                        myArrayList = this.GetTop100GoogleReadingCityMobile(keyword).Result;
                        break;
                    }
                case "394":
                    {
                        myArrayList = this.GetTop100GoogleStokeonTrentCityDesktop(keyword).Result;
                        break;
                    }
                case "395":
                    {
                        myArrayList = this.GetTop100GoogleStokeonTrentCityMobile(keyword).Result;
                        break;
                    }
                case "396":
                    {
                        myArrayList = this.GetTop100GoogleSwanseaCityDesktop(keyword).Result;
                        break;
                    }
                case "397":
                    {
                        myArrayList = this.GetTop100GoogleSwanseaCityMobile(keyword).Result;
                        break;
                    }
                case "398":
                    {
                        myArrayList = this.GetTop100GoogleVNMobile(keyword).Result;
                        break;
                    }
                case "399":
                    {
                        myArrayList = this.GetTop100GoogleMyanmarInternet(keyword).Result;
                        break;
                    }
                case "400":
                    {
                        myArrayList = this.GetTop100GoogleMyanmarInternetMobile(keyword).Result;
                        break;
                    }
                case "401":
                    {
                        myArrayList = this.GetTop100GoogleUKDesktop_ImageURLs(keyword).Result;//desktop pattern
                        break;
                    }

                case "402":
                    {
                        myArrayList = this.GetTop100GoogleUKMobile_ImageURLs(keyword).Result;//mobile pattern
                        break;
                    }
                case "403":
                    {
                        myArrayList = this.GetTop100GoogleRomaniaInternetMobile(keyword).Result;//mobile pattern
                        break;
                    }
                case "404":
                    {
                        myArrayList = this.GetTop100GoogleSwitzerlandEnglish(keyword).Result;
                        break;
                    }
                case "405":
                    {
                        myArrayList = this.GetTop100GoogleSwitzerlandEnglishMobile(keyword).Result;
                        break;
                    }
                case "406":
                    {
                        myArrayList = this.GetTop100GoogleBahrainEnglishMobile(keyword).Result;
                        break;
                    }
                case "407":
                    {
                        myArrayList = this.GetTop100GoogleEgyptEnglishMobile(keyword).Result;
                        break;
                    }
                case "408":
                    {
                        myArrayList = this.GetTop100GoogleJordanEnglishMobile(keyword).Result;
                        break;
                    }
                case "409":
                    {
                        myArrayList = this.GetTop100GoogleKuwaitEnglishMobile(keyword).Result;
                        break;
                    }
                case "410":
                    {
                        myArrayList = this.GetTop100GoogleLebanonEnglishMobile(keyword).Result;
                        break;
                    }
                case "411":
                    {
                        myArrayList = this.GetTop100GoogleQatarEnglishMobile(keyword).Result;
                        break;
                    }
                case "412":
                    {
                        myArrayList = this.GetTop100GoogleSaudiArabiaEnglishMobile(keyword).Result;
                        break;
                    }
                case "413":
                    {
                        myArrayList = this.GetTop100GoogleBahrainEnglish(keyword).Result;
                        break;
                    }
                case "414":
                    {
                        myArrayList = this.GetTop100GoogleEgyptEnglish(keyword).Result;
                        break;
                    }
                case "415":
                    {
                        myArrayList = this.GetTop100GoogleJordanEnglish(keyword).Result;
                        break;
                    }
                case "416":
                    {
                        myArrayList = this.GetTop100GoogleKuwaitEnglish(keyword).Result;
                        break;
                    }
                case "417":
                    {
                        myArrayList = this.GetTop100GoogleLebanonEnglish(keyword).Result;
                        break;
                    }
                case "418":
                    {
                        myArrayList = this.GetTop100GoogleQatarEnglish(keyword).Result;
                        break;
                    }
                case "419":
                    {
                        myArrayList = this.GetTop100GoogleSaudiArabiaEnglish(keyword).Result;
                        break;
                    }
                case "420":
                    {
                        myArrayList = this.GetTop100GoogleBahrainArabicMobile(keyword).Result;
                        break;
                    }
                case "421":
                    {
                        myArrayList = this.GetTop100GoogleEgyptArabicMobile(keyword).Result;
                        break;
                    }
                case "422":
                    {
                        myArrayList = this.GetTop100GoogleJordanArabicMobile(keyword).Result;
                        break;
                    }
                case "423":
                    {
                        myArrayList = this.GetTop100GoogleKuwaitArabicMobile(keyword).Result;
                        break;
                    }
                case "424":
                    {
                        myArrayList = this.GetTop100GoogleLebanonArabicMobile(keyword).Result;
                        break;
                    }
                case "425":
                    {
                        myArrayList = this.GetTop100GoogleQatarArabicMobile(keyword).Result;
                        break;
                    }
                case "426":
                    {
                        myArrayList = this.GetTop100GoogleKoreanMobile(keyword).Result;
                        break;
                    }
                case "427":
                    {
                        myArrayList = getTop100GoogleKorean(keyword).Result;
                        break;
                    }
                case "428":
                    {
                        myArrayList = GetTop100GoogleCzechRepublicEnglish(keyword).Result;
                        break;
                    }
                case "429":
                    {
                        myArrayList = GetTop100GoogleCzechRepublicEnglishMobile(keyword).Result;
                        break;
                    }
                case "430":
                    {
                        myArrayList = GetTop100GoogleMaltaEnglish(keyword).Result;
                        break;
                    }
                case "431":
                    {
                        myArrayList = GetTop100GoogleMaltaEnglishMobile(keyword).Result;
                        break;
                    }
                case "432":
                    {
                        myArrayList = GetTop100GoogleIceland(keyword).Result;
                        break;
                    }
                case "433":
                    {
                        myArrayList = GetTop100GoogleIcelandMobile(keyword).Result;
                        break;
                    }
                case "434":
                    {
                        myArrayList = GetTop100GoogleIcelandEnglish(keyword).Result;
                        break;
                    }
                case "435":
                    {
                        myArrayList = GetTop100GoogleIcelandEnglishMobile(keyword).Result;
                        break;
                    }
                case "436":
                    {
                        myArrayList = GetTop100GoogleUKPeterboroughEnglish(keyword).Result;
                        break;
                    }
                case "437":
                    {
                        myArrayList = GetTop100GoogleUKPeterboroughEnglishMobile(keyword).Result;
                        break;
                    }
                case "438":
                    {
                        myArrayList = GetTop100GoogleTWIEnglish(keyword).Result;
                        break;
                    }
                case "439":
                    {
                        myArrayList = getTop100GoogleTWInternetEnglishMobile(keyword).Result;
                        break;
                    }
                case "441":
                    {
                        myArrayList = GetTop100GoogleNewcastleCityEnglandDesktop(keyword).Result;
                        break;
                    }
                case "442":
                    {
                        myArrayList = GetTop100GoogleNewcastleCityEnglandMobile(keyword).Result;
                        break;
                    }
                case "443":
                    {
                        myArrayList = GetTop100GoogleUKLondonE1Desktop(keyword).Result;
                        break;
                    }
                case "444":
                    {
                        myArrayList = GetTop100GoogleUKLondonE1Mobile(keyword).Result;
                        break;
                    }
                case "445":
                    {
                        myArrayList = GetTop100GoogleUKLondonNW1Desktop(keyword).Result;
                        break;
                    }
                case "446":
                    {
                        myArrayList = GetTop100GoogleUKLondonNW1Mobile(keyword).Result;
                        break;
                    }
                case "447":
                    {
                        myArrayList = GetTop100GoogleUKLondonSE1Desktop(keyword).Result;
                        break;
                    }
                case "448":
                    {
                        myArrayList = GetTop100GoogleUKLondonSE1Mobile(keyword).Result;
                        break;
                    }
                case "449":
                    {
                        myArrayList = GetTop100GoogleUKAberdeenDesktop(keyword).Result;
                        break;
                    }
                case "450":
                    {
                        myArrayList = GetTop100GoogleUKAberdeenMobile(keyword).Result;
                        break;
                    }
                case "451":
                    {
                        myArrayList = GetTop100GoogleUKBoltonDesktop(keyword).Result;
                        break;
                    }
                case "452":
                    {
                        myArrayList = GetTop100GoogleUKBoltonMobile(keyword).Result;
                        break;
                    }
                case "453":
                    {
                        myArrayList = GetTop100GoogleUKBournemouthDesktop(keyword).Result;
                        break;
                    }
                case "454":
                    {
                        myArrayList = GetTop100GoogleUKBournemouthMobile(keyword).Result;
                        break;
                    }
                case "455":
                    {
                        myArrayList = GetTop100GoogleUKCoventryDesktop(keyword).Result;
                        break;
                    }
                case "456":
                    {
                        myArrayList = GetTop100GoogleUKCoventryMobile(keyword).Result;
                        break;
                    }
                case "457":
                    {
                        myArrayList = GetTop100GoogleUKCroydonDesktop(keyword).Result;
                        break;
                    }
                case "458":
                    {
                        myArrayList = GetTop100GoogleUKCroydonMobile(keyword).Result;
                        break;
                    }
                case "459":
                    {
                        myArrayList = GetTop100GoogleUKNorthamptonDesktop(keyword).Result;
                        break;
                    }
                case "460":
                    {
                        myArrayList = GetTop100GoogleUKNorthamptonMobile(keyword).Result;
                        break;
                    }
                case "461":
                    {
                        myArrayList = GetTop100GoogleUKNorwichDesktop(keyword).Result;
                        break;
                    }
                case "462":
                    {
                        myArrayList = GetTop100GoogleUKNorwichMobile(keyword).Result;
                        break;
                    }
                case "463":
                    {
                        myArrayList = GetTop100GoogleUKSloughDesktop(keyword).Result;
                        break;
                    }
                case "464":
                    {
                        myArrayList = GetTop100GoogleUKSloughMobile(keyword).Result;
                        break;
                    }
                case "465":
                    {
                        myArrayList = GetTop100GoogleUKWatfordDesktop(keyword).Result;
                        break;
                    }
                case "466":
                    {
                        myArrayList = GetTop100GoogleUKWatfordMobile(keyword).Result;
                        break;
                    }
                case "467":
                    {
                        myArrayList = GetTop100GoogleUKLondonSW1VDesktop(keyword).Result;
                        break;
                    }
                case "468":
                    {
                        myArrayList = GetTop100GoogleUKLondonSW1VMobile(keyword).Result;
                        break;
                    }
                case "469":
                    {
                        myArrayList = GetTop100GoogleCyprusDesktop(keyword).Result; ;
                        break;
                    }
                case "470":
                    {
                        myArrayList = GetTop100GoogleCyprusMobile(keyword).Result;
                        break;
                    }
                case "471":
                    {
                        myArrayList = GetTop100GoogleEstoniaDesktop(keyword).Result; 
                        break;
                    }
                case "472":
                    {
                        myArrayList = GetTop100GoogleEstoniaMobile(keyword).Result;
                        break;
                    }
                case "473":
                    {
                        myArrayList = GetTop100GoogleLithuaniaDesktop(keyword).Result;
                        break;
                    }
                case "474":
                    {
                        myArrayList = GetTop100GoogleLithuaniaMobile(keyword).Result;
                        break;
                    }
            }
            return myArrayList;
        }


        private string GetRedirectedUrl(string url)
        {
            //21-11-2020
            url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");
            if (string.IsNullOrEmpty(url.Trim())) return string.Empty;

            if (url.LastIndexOf("https://") > 0)
                url = url.Remove(0, url.LastIndexOf("https://"));
            if (url.LastIndexOf("http://") > 0)
                url = url.Remove(0, url.LastIndexOf("http://"));

            Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
            if (!rx.Match(url).Success && !url.Contains("/aclk?"))
                if (!url.Contains("://"))
                    url = "http://" + url;

            if (url.Contains("&amp;grqid="))
                url = url.Remove(url.IndexOf("&amp;grqid="));

            if (url.Contains("\0"))
                url = url.Replace("\0", "%00");

            if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && (!url.Contains("/aclk?") && !url.Contains("search?num=100")))

                return url;

            return string.Empty;

        }

        public ArrayList MobilePattern(string htmlsource)
        {

            var doc = new HtmlAgilityPack.HtmlDocument();
            string html = htmlsource.Replace(@"\", "");
            doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            ArrayList alDup = new ArrayList();
            ArrayList googleList = new ArrayList();
            //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//a[@class='C8nzq']|//a[@class='Rk4fgb']|//div[@id='rso']/div/div/div/a[1]|//a[@class='JTuIPc']|//a[@class='C8nzq BmP5tf']|//a[@class='BmP5tf']|//a[@class='sXtWJb']|//a[@class='C8nzq BmP5tf amp_r']|//div[@jsl='$t t-4cfX2GiP_Fk;$x 0;']/a|//g-link[not(contains(@class,'fl'))]/a");
            HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//a[@class='C8nzq']|//a[@class='Rk4fgb']|//div[@id='rso']/div/div/div/a[1]|//a[@class='JTuIPc']|//a[@class='C8nzq BmP5tf']|//a[@class='BmP5tf']|//a[@class='sXtWJb']|//a[@class='C8nzq BmP5tf amp_r']|//div[@jsl='$t t-4cfX2GiP_Fk;$x 0;']/a|//g-link[not(contains(@class,'fl'))]/a");


            foreach (HtmlNode links in node)
            {
                try
                {
                    /*string urls = links.Attributes["href"].Value;
                    if (urls.StartsWith("http") || urls.StartsWith("https"))
                    {
                        int indx = urls.LastIndexOf("http://");
                        if (indx < 0)
                        {
                            indx = urls.LastIndexOf("https://");
                        }
                        urls = urls.Remove(0, indx);

                        //string links1 = HttpUtility.UrlDecode(urls);

                        alDup.Add(HttpUtility.HtmlDecode(url.Replace("\x00", "%00").Replace("|", "%7C").Replace("^", "%5E")));
                    }*/

                    string url = links.Attributes["href"].Value;
                    url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");

                    if (url.LastIndexOf("https://") > 0)
                        url = url.Remove(0, url.LastIndexOf("https://"));
                    if (url.LastIndexOf("http://") > 0)
                        url = url.Remove(0, url.LastIndexOf("http://"));

                    Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
                    if (!rx.Match(url).Success && !url.Contains("/aclk?"))
                        if (!url.Contains("://"))
                            url = "http://" + url;

                    if (url.Contains("&amp;grqid="))
                        url = url.Remove(url.IndexOf("&amp;grqid="));

                    if (url.Contains("\0"))
                        url = url.Replace("\0", "%00");

                    if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && (!url.Contains("/aclk?") && !url.Contains("///search?") && !url.Contains("search?num=100")))

                        if (url.StartsWith("http") || url.StartsWith("https") || !url.Contains("https://www.google.com/maps"))
                        {
                            url = GetRedirectedUrl(WebUtility.HtmlDecode(url).Trim());
                            if (url.ToLower().Contains("%2f") || url.ToLower().Contains("%2e"))
                                url = GetRedirectedUrl(WebUtility.UrlDecode(WebUtility.HtmlDecode(url)).Trim());
                            alDup.Add(WebUtility.HtmlDecode(url.Replace("\x00", "%00").Replace("|", "%7C").Replace("^", "%5E")));
                        }
                }
                catch { continue; }
            }
            foreach (string s in alDup)
            {
                if (googleList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                googleList.Add(s);
            }

            if (googleList.Count > 100)
            {
                googleList.RemoveRange(100, googleList.Count - 100);
            }
            return googleList;
        }   //MobilePattern



        public ArrayList DesktopPattern(string htmlsource)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            string html = htmlsource.Replace(@"\", "");
            doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            ArrayList alDup = new ArrayList();
            ArrayList googleList = new ArrayList();
            //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//div[@class='srg']//div[@class='r']/a[1]|//div[@class='bkWMgd']/div[@class='g']//div[@class='r']/a[1]|//div[1]/div/h3/div/g-link/a");
            //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//div[@class='srg']//div[@class='r']/a[1]|//div[@class='bkWMgd']/div[@class='g']//div[@class='r']/a[1]|//div[1]/div/h3/div/g-link/a|//div[1]/div/h3/g-link/a");
            HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//div[@class='r']/a");


            foreach (HtmlNode links in node)
            {
                try
                {
                    /*string url = links.Attributes["href"].Value;
                    if (url.StartsWith("http") || url.StartsWith("https"))
                    {
                        int indx = url.LastIndexOf("http://");
                        if (indx < 0)
                        {
                            indx = url.LastIndexOf("https://");
                        }
                        url = url.Remove(0, indx);

                        //string links1 = HttpUtility.UrlDecode(urls);

                        alDup.Add(HttpUtility.HtmlDecode(url.Replace("\x00", "%00").Replace("|", "%7C").Replace("^", "%5E")));
                    }*/

                    string url = links.Attributes["href"].Value;
                    url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");

                    if (url.LastIndexOf("https://") > 0)
                        url = url.Remove(0, url.LastIndexOf("https://"));
                    if (url.LastIndexOf("http://") > 0)
                        url = url.Remove(0, url.LastIndexOf("http://"));

                    Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
                    if (!rx.Match(url).Success && !url.Contains("/aclk?"))
                        if (!url.Contains("://"))
                            url = "http://" + url;

                    if (url.Contains("&amp;grqid="))
                        url = url.Remove(url.IndexOf("&amp;grqid="));

                    if (url.Contains("\0&"))
                        url = url.Replace("\0&", "%00&");

                    if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && (!url.Contains("/aclk?") && !url.Contains("///search?") && !url.Contains("search?num=100")))


                        if (url.StartsWith("http") || url.StartsWith("https"))
                        {
                            url = GetRedirectedUrl(WebUtility.HtmlDecode(url).Trim());
                            if (url.ToLower().Contains("%2f") || url.ToLower().Contains("%2e"))
                                url = GetRedirectedUrl(WebUtility.UrlDecode(WebUtility.HtmlDecode(url)).Trim());
                            alDup.Add(WebUtility.HtmlDecode(url.Replace("\x00", "%00").Replace("|", "%7C").Replace("^", "%5E")));
                        }


                }
                catch { continue; }
            }
            foreach (string s in alDup)
            {
                if (googleList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                googleList.Add(s);
            }

            if (googleList.Count > 100)
            {
                googleList.RemoveRange(100, googleList.Count - 100);
            }

            return googleList;
        }  //DesktopPattern

        private ArrayList ImagesPattern(string htmlsource, string urlType)
        {
            //string urlType = string.Empty;
            var doc = new HtmlAgilityPack.HtmlDocument();
            string html = htmlsource.Replace(@"\", "");
            doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            ArrayList alDup = new ArrayList();
            ArrayList googleList = new ArrayList();
            HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//div[@class=\"rg_meta notranslate\"]");

            foreach (HtmlNode links in node)
            {
                try
                {
                    string url = "";

                    if (urlType == "ImageLinks")

                        url = JObject.Parse(links.InnerText)["ou"].Value<string>();
                    else
                        url = JObject.Parse(links.InnerText)["ru"].Value<string>();

                    if (url.StartsWith("http") || url.StartsWith("https"))
                    {
                        int indx = url.LastIndexOf("http://");
                        if (indx < 0)
                        {
                            indx = url.LastIndexOf("https://");
                        }
                        url = url.Remove(0, indx);
                        alDup.Add(HttpUtility.HtmlDecode(url));
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            foreach (string s in alDup)
            {
                if (googleList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                googleList.Add(s);
            }

            if (googleList.Count > 100)
            {
                googleList.RemoveRange(100, googleList.Count - 100);
            }
            //if (googleList.Count > 0)
            //dict.Add(keyword, googleList);
            //}
            return googleList;
        }

        private ArrayList ImagesPatternMobile(string htmlsource, string urlType)
        {
            //string urlType = string.Empty;
            var doc = new HtmlAgilityPack.HtmlDocument();
            string html = htmlsource.Replace(@"\", "");
            doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            ArrayList alDup = new ArrayList();
            ArrayList googleList = new ArrayList();

            //HtmlNodeCollection node;
            //if (urlType == "ImageLinks")
            //    node = doc.DocumentNode.SelectNodes("//a[@jsname=\"m8x3S\"]/img");
            //else
            //    node = doc.DocumentNode.SelectNodes("//a[@class=\"VFACy\"]");

            HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//a[@jsname=\"m8x3S\"]/img|//a[@class=\"VFACy\"]");


            foreach (HtmlNode links in node)
            {
                try
                {
                    string url = "";

                    if (urlType == "ImageLinks")
                    {
                        try
                        {
                            url = links.Attributes["data-iurl"].Value;
                        }
                        catch
                        {
                            url = links.Attributes["data-src"].Value;
                        }
                    }
                    else
                        url = links.Attributes["href"].Value;

                    if (url.StartsWith("http") || url.StartsWith("https"))
                    {
                        int indx = url.LastIndexOf("http://");
                        if (indx < 0)
                        {
                            indx = url.LastIndexOf("https://");
                        }
                        url = url.Remove(0, indx);
                        alDup.Add(HttpUtility.HtmlDecode(url));
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            foreach (string s in alDup)
            {
                if (googleList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                googleList.Add(s);
            }

            if (googleList.Count > 100)
            {
                googleList.RemoveRange(100, googleList.Count - 100);
            }
            return googleList;
        }


        private ArrayList PageResultsPatternMobile(string htmlsource, string urlType)
        {
            //string urlType = string.Empty;
            var doc = new HtmlAgilityPack.HtmlDocument();
            string html = htmlsource.Replace(@"\", "");
            doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            ArrayList alDup = new ArrayList();
            ArrayList googleList = new ArrayList();

            HtmlNodeCollection node;
            if (urlType == "ImageLinks")
                node = doc.DocumentNode.SelectNodes("//a[@jsname=\"m8x3S\"]/img");
            else
                //node = doc.DocumentNode.SelectNodes("//a[@class=\"VFACy\"]");//VFACy kGQAp
                node = doc.DocumentNode.SelectNodes("//a[@class=\"VFACy kGQAp\"]");

            foreach (HtmlNode links in node)
            {
                try
                {
                    string url = "";

                    if (urlType == "ImageLinks")
                    {
                        try
                        {
                            url = links.Attributes["data-iurl"].Value;
                        }
                        catch
                        {
                            url = links.Attributes["data-src"].Value;
                        }
                    }
                    else
                        url = links.Attributes["href"].Value;

                    if (url.StartsWith("http") || url.StartsWith("https"))
                    {
                        int indx = url.LastIndexOf("http://");
                        if (indx < 0)
                        {
                            indx = url.LastIndexOf("https://");
                        }
                        url = url.Remove(0, indx);
                        alDup.Add(HttpUtility.HtmlDecode(url));
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            foreach (string s in alDup)
            {
                if (googleList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                googleList.Add(s);
            }

            if (googleList.Count > 100)
            {
                googleList.RemoveRange(100, googleList.Count - 100);
            }
            return googleList;
        }

        /*private ArrayList PageResultsPatternMobile2(string htmlsource)
        {
            //string urlType = string.Empty;
            //var doc = new HtmlAgilityPack.HtmlDocument();
            //string html = htmlsource.Replace(@"\", "");
            //doc = new HtmlAgilityPack.HtmlDocument();
            //doc.LoadHtml(html);
            ArrayList alDup = new ArrayList();
            ArrayList myList = new ArrayList();

            foreach (string[] src in htmlsource)
            {
                string html = src[1].ToString().Replace(@"\", "");

                //string matchPattern = "n,[\"http(.*?)\",";
                string matchPattern = "n,\\[\"(http.*?)\",";

                //string matchPattern = "data-iurl=\"";
                //string URLpattern = "([\\w-]+\\.)+[\\w-]+(/[\\w-./+?()~'%!,$=;:]*)?";

                Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                MatchCollection mc = re.Matches(html);

                foreach (Match m in mc)
                {
                    string HtmlText = HttpUtility.HtmlDecode(m.Groups[1].Value);
                    int n = HtmlText.IndexOf("?");
                    if (n > 0)
                        HtmlText = HtmlText.Remove(n);
                    alDup.Add(HtmlText);
                }
                foreach (string s in alDup)
                {
                    if (myList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                    myList.Add(s);
                }

                if (myList.Count > 100)
                {
                    myList.RemoveRange(100, myList.Count - 100);
                }
                return myList;
            }
        }*/

        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUS(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com", "United States", "en-us", "w+CAIQICINVW5pdGVkIFN0YXRlcw==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNZ(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.nz", "New Zealand", "en-nz", "w+CAIQICILTmV3IFplYWxhbmQ=");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public async Task<Dictionary<string, ArrayList>> getTop100GoogleZA(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.za", "South Africa", "en-za", "w+CAIQICIPU291dGggQWZyaWNhCgoK");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public async Task<Dictionary<string, ArrayList>> getTop100GoogleRussia(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ru", "Russia", "ru-ru", "w+CAIQICIGUnVzc2lh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleKorean(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.kr", "South Korea", "ko-kr", "w+CAIQICILU291dGggS29yZWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleIT(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "it", "Italy", "it-it", "w+CAIQICIFSXRhbHk=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSE(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "se", "Sweden", "sv-se", "w+CAIQICIGc3dlZGVu");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCN(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "zh-cn-hk", "w+CAIQICIJSG9uZyBLb25n");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleFR(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "fr", "France", "fr-fr", "w+CAIQICIGRnJhbmNl");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHK(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "en-hk", "w+CAIQICIJSG9uZyBLb25n");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleES(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "es", "Spain", "es-es", "w+CAIQICIFc3BhaW4=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleFRWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "fr", "France", "fr-fr", "w+CAIQICIGRnJhbmNl");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSG(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.sg", "Singapore", "en-sg", "w+CAIQICIJU2luZ2Fwb3Jl");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBEInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "be", "Belgium", "nl-be", "w+CAIQICIHQmVsZ2l1bQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBENederlands(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "be", "Belgium", "nl-be", "w+CAIQICIHQmVsZ2l1bQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBEBelgie(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "be", "Belgium", "nl-be", "w+CAIQICIHQmVsZ2l1bQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleJapan(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.jp", "Japan", "ja-jp", "w+CAIQICIFSmFwYW4=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBrasil(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.br", "Brazil", "pt-br", "w+CAIQICIGQnJhemls");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNederlands(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "nl", "Netherlands", "nl-nl", "w+CAIQICILTmV0aGVybGFuZHM=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDE(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "de", "Germany", "de-de", "w+CAIQICIHR2VybWFueQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCH(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "de-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLU(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "lu", "Luxembourg", "de-lu", "w+CAIQICILTHV4ZW1ib3VyZw==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleAT(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "at", "Austria", "de-at", "w+CAIQICIHQXVzdHJpYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNederlandsInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "nl", "Netherlands", "nl-nl", "w+CAIQICILTmV0aGVybGFuZHM=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUKInternet(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUKMobileWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCanada(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ca", "Canada", "en-ca", "w+CAIQICIGQ2FuYWRh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMYWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.my", "Malaysia", "en-my", "w+CAIQICIITWFsYXlzaWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleTHWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.th", "Thailand", "th-th", "w+CAIQICIIVGhhaWxhbmQ=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNGWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.ng", "Nigeria", "en-ng", "w+CAIQICIHTmlnZXJpYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCHItalino(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "it-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCHFrancais(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "fr-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCHDeutsch(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "de-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePTMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "pt", "Portugal", "pt-pt", "w+CAIQICIIUG9ydHVnYWw=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNederlandsMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "nl", "Netherlands", "nl-nl", "w+CAIQICILTmV0aGVybGFuZHM=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMexico(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.mx", "Mexico", "es-419-mx", "w+CAIQICIGTWV4aWNv");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleESMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "es", "Spain", "es-es", "w+CAIQICIFc3BhaW4=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;

        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleAusInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.au", "Australia", "en-au", "w+CAIQICIJQXVzdHJhbGlh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePolskiInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "pl", "Poland", "pl-pl", "w+CAIQICIGUG9sYW5k");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleRomaniaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ro", "Romania", "ro-ro", "w+CAIQICIHUm9tYW5pYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBulgariaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "bg", "Bulgaria", "bg-bg", "w+CAIQICIIQnVsZ2FyaWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSlovenijaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "si", "Slovenia", "sl-si", "w+CAIQICIIU2xvdmVuaWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMagyaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "hu", "Hungary", "hu-hu", "w+CAIQICIHSHVuZ2FyeQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCzechInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "cz", "Czech Republic", "cs-cz", "w+CAIQICIOQ3plY2ggUmVwdWJsaWM=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSlovakiaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "sk", "Slovakia", "sk-sk", "w+CAIQICIIU2xvdmFraWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleArgentinaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.ar", "Argentina", "es-419-ar", "w+CAIQICIJQXJnZW50aW5h");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLebanonInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.lb", "Lebanon", "ar-lb", "w+CAIQICIHTGViYW5vbg==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleJordanInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "jo", "Jordan", "ar-jo", "w+CAIQICIGSm9yZGFu");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleFinlandInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "fi", "Finland", "fi-fi", "w+CAIQICIHRmlubGFuZA==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleGreeceInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "gr", "Greece", "el-gr", "w+CAIQICIGR3JlZWNl");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNorwayInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "no", "Norway", "no-no", "w+CAIQICIGTm9yd2F5");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;

        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBelgiumInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "be", "Belgium", "fr-be", "w+CAIQICIHQmVsZ2l1bQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDenmarkInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "dk", "Denmark", "da-dk", "w+CAIQICIHRGVubWFyaw==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> getTop100GoogleChileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "cl", "Chile", "es-419-cl", "w+CAIQICIFQ2hpbGU=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleAusMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.au", "Australia", "en-au", "w+CAIQICIJQXVzdHJhbGlh");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCHItalinoMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ch", "Switzerland", "it-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCHFrancaisMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ch", "Switzerland", "fr-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCHDeutschMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ch", "Switzerland", "de-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePTInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "pt", "Portugal", "pt-pt", "w+CAIQICIIUG9ydHVnYWw=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDEMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "de", "Germany", "de-de", "w+CAIQICIHR2VybWFueQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleAEMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ae", "United Arab Emirates", "ar-ae", "w+CAIQICIUVW5pdGVkIEFyYWIgRW1pcmF0ZXM=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleATMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "at", "Austria", "de-at", "w+CAIQICIHQXVzdHJpYQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBEDutchMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "be", "Belgium", "nl-be", "w+CAIQICIHQmVsZ2l1bQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBEFrenchMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "be", "Belgium", "fr-be", "w+CAIQICIHQmVsZ2l1bQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBrasilMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.br", "Brazil", "pt-br", "w+CAIQICIGQnJhemls");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCanadaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ca", "Canada", "en-ca", "w+CAIQICIGQ2FuYWRh");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDenmarkMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "dk", "Denmark", "da-dk", "w+CAIQICIHRGVubWFyaw==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleFinlandMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "fi", "Finland", "fi-fi", "w+CAIQICIHRmlubGFuZA==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleFRMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "fr", "France", "fr-fr", "w+CAIQICIGRnJhbmNl");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleGreeceMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "gr", "Greece", "el-gr", "w+CAIQICIGR3JlZWNl");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHKMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.hk", "Hong Kong", "zh-TW-HK", "w+CAIQICIJSG9uZyBLb25n");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleIEMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ie", "Ireland", "en-ie", "w+CAIQICIHSXJlbGFuZA==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleJapanMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.jp", "Japan", "ja-jp", "w+CAIQICIFSmFwYW4=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNorwayMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "no", "Norway", "no-no", "w+CAIQICIGTm9yd2F5");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSEMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "se", "Sweden", "sv-se", "w+CAIQICIGU3dlZGVu");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHungaryMobileWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "hu", "Hungary", "hu-hu", "w+CAIQICIHSHVuZ2FyeQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePolandMobileWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "pl", "Poland", "pl-pl", "w+CAIQICIGUG9sYW5k");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleTurkeyMobileWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.tr", "Turkey", "tr-tr", "w+CAIQICIGVHVya2V5");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleRussiaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ru", "Russia", "ru-ru", "w+CAIQICIGUnVzc2lh");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleTWInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.tw", "Taiwan", "zh-tw", "w+CAIQICIGVGFpd2Fu");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleILInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.il", "Israel", "en-il", "w+CAIQICIGSXNyYWVs");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSerbiaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "rs", "Serbia", "sr-rs", "w+CAIQICIGU2VyYmlh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSNewYorkCity(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com", "New York, United States", "en-us", "w+CAIQICIWTmV3IFlvcmssVW5pdGVkIFN0YXRlcw==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSNewYorkCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com", "New York, United States", "en-us", "w+CAIQICIWTmV3IFlvcmssVW5pdGVkIFN0YXRlcw==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSLA(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com", "Los Angeles, United States", "en-us", "w+CAIQICIkTG9zIEFuZ2VsZXMsQ2FsaWZvcm5pYSxVbml0ZWQgU3RhdGVz");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSLAMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com", "Los Angeles, United States", "en-us", "w+CAIQICIkTG9zIEFuZ2VsZXMsQ2FsaWZvcm5pYSxVbml0ZWQgU3RhdGVz");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleZAMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.za", "South Africa", "en-za", "w+CAIQICIPU291dGggQWZyaWNhCgoK");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSaudiMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.sa", "Saudia Arabia", "ar-sa", "w+CAIQICIMU2F1ZGkgQXJhYmlh");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCanadaFrenchWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ca", "Canada", "fr-ca", "w+CAIQICIGQ2FuYWRh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSChicago(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com", "Chicago, United States", "en-us", "w+CAIQICIVQ2hpY2FnbyxVbml0ZWQgU3RhdGVz");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSChicagoMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com", "Chicago, United States", "en-us", "w+CAIQICIeQ2hpY2FnbyxJbGxpbm9pcyxVbml0ZWQgU3RhdGVz");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSSanFran(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com", "San Francisco, United States", "en-us", "w+CAIQICImU2FuIEZyYW5jaXNjbyxDYWxpZm9ybmlhLFVuaXRlZCBTdGF0ZXM");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSSanFranMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com", "San Francisco, United States", "en-us", "w+CAIQICImU2FuIEZyYW5jaXNjbyxDYWxpZm9ybmlhLFVuaXRlZCBTdGF0ZXM");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePakistanInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.pk", "Pakistan", "en-pk", "w+CAIQICIIUGFraXN0YW4=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePakistanMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.pk", "Pakistan", "en-pk", "w+CAIQICIIUGFraXN0YW4=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleINMobileWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.in", "India", "en-in", "w+CAIQICIFSW5kaWE=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCanadaFrenchMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ca", "Canada", "fr-ca", "w+CAIQICIGQ2FuYWRh");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMYMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.my", "Malaysia", "en-my", "w+CAIQICIITWFsYXlzaWE=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSGMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.sg", "Singapore", "zh-sg", "w+CAIQICIJU2luZ2Fwb3J");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCanada1(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ca", "Canada", "en-ca", "w+CAIQICIGQ2FuYWRh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCanadaMobile1(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ca", "Canada", "en-ca", "w+CAIQICIGQ2FuYWRh");
                // dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSrilankaMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "lk", "Sri Lanka", "en-lk", "w+CAIQICIJU3JpIExhbmth");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSrilankaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "lk", "Sri Lanka", "en-lk", "w+CAIQICIJU3JpIExhbmth");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHRInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "hr", "Croatia", "hr-hr", "w+CAIQICIHQ3JvYXRpYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHRMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "hr", "Croatia", "hr-hr", "w+CAIQICIHQ3JvYXRpYQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCZMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "cz", "Czech Republic", "cs-cz", "w+CAIQICIHQ3plY2hpYQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleTHMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.th", "Thailand", "th-th", "w+CAIQICIIVGhhaWxhbmQ=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSHoustonCity(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com", "Houston, United States", "en-us", "w+CAIQICIbSG91c3RvbixUZXhhcyxVbml0ZWQgU3RhdGVz");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSHoustonCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com", "United States", "en-us", "w+CAIQICIbSG91c3RvbixUZXhhcyxVbml0ZWQgU3RhdGVz");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSMiamiCity(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com", "Miami, United States", "en-us", "w+CAIQICIbTWlhbWksRmxvcmlkYSxVbml0ZWQgU3RhdGVz");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSMiamiCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com", "Miami, United States", "en-us", "w+CAIQICIbTWlhbWksRmxvcmlkYSxVbml0ZWQgU3RhdGVz");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleAEEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ae", "United Arab Emirates", "en-ae", "w+CAIQICIUVW5pdGVkIEFyYWIgRW1pcmF0ZXM=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHKInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "en-hk", "w+CAIQICIJSG9uZyBLb25n");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMexicoMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.mx", "Mexico", "es-419-mx", "w+CAIQICIGTWV4aWNv");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMexicoCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.mx", "Mexico", "es-419-mx", "w+CAIQICIeTWV4aWNvIENpdHksTWV4aWNvIENpdHksTWV4aWNv");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleChileInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "cl", "Chile", "es-419-cl", "w+CAIQICIFQ2hpbGU=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePeruInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.pe", "Peru", "es-419-pe", "w+CAIQICIEUGVydQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePeruInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.pe", "Peru", "es-419-pe", "w+CAIQICIEUGVydQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleColumbiaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.co", "Columbia", "es-419-co", "w+CAIQICIIQ29sb21iaWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleColumbiaInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.co", "Columbia", "es-419-co", "w+CAIQICIIQ29sb21iaWE=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMexicoCity(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.mx", "Mexico City, Mexico", "es-419-mx", "w+CAIQICIeTWV4aWNvIENpdHksTWV4aWNvIENpdHksTWV4aWNv");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleIndonesiaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.id", "Indonesia", "id-id", "w+CAIQICIJSW5kb25lc2lh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleIndonesiaInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.id", "Indonesia", "id-id", "w+CAIQICIJSW5kb25lc2lh");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePhilippinesInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.ph", "Philippines", "fil-ph", "w+CAIQICILUGhpbGlwcGluZXM=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePhilippinesInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.ph", "Philippines", "fil-ph", "w+CAIQICILUGhpbGlwcGluZXM=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSGMobileInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.sg", "Singapore", "en-sg", "w+CAIQICIJU2luZ2Fwb3J");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleTHMobileInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.th", "Thailand", "en-th", "w+CAIQICIIVGhhaWxhbmQ=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHKMobileInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.hk", "Hong Kong", "en-hk", "w+CAIQICIJSG9uZyBLb25n");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMYInternetLanguage(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.my", "Malaysia", "ms-my", "w+CAIQICIITWFsYXlzaWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMYMobileInternetLanguage(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.my", "Malaysia", "ms-my", "w+CAIQICIITWFsYXlzaWE=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleTHInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.th", "Thailand", "en-th", "w+CAIQICIIVGhhaWxhbmQ=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSGInternetLanguage(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.sg", "Singapore", "zh-sg", "w+CAIQICIJU2luZ2Fwb3Jl");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHKInternetChinaLanguage(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "zh-cn-hk", "w+CAIQICIJSG9uZyBLb25n");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHKMobileInternetChinaLanguage(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.hk", "Hong Kong", "zh-cn-hk", "w+CAIQICIJSG9uZyBLb25n");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNZInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.nz", "New Zealand", "en-nz", "w+CAIQICILTmV3IFplYWxhbmQ=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleTWInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.tw", "Taiwan", "zh-tw", "w+CAIQICIGVGFpd2Fu");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleAEEnglishInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ae", "United Arab Emirates", "en-ae", "w+CAIQICIUVW5pdGVkIEFyYWIgRW1pcmF0ZXM=");
                // dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleJapanInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.jp", "Japan", "en-jp", "w+CAIQICIFSmFwYW4=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleJapanMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.jp", "Japan", "en-jp", "w+CAIQICIFSmFwYW4=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDenmarkInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "dk", "Denmark", "en-dk", "w+CAIQICIHRGVubWFyaw==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDenmarkMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "dk", "Denmark", "en-dk", "w+CAIQICIHRGVubWFyaw==");
                // dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSEInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "se", "Sweden", "en-se", "w+CAIQICIGU3dlZGV");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSEMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "se", "Sweden", "en-se", "w+CAIQICIGU3dlZGVu");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNorwayInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "no", "Norway", "en-no", "w+CAIQICIGTm9yd2F5");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNorwayMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "no", "Norway", "en-no", "w+CAIQICIGTm9yd2F5");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleFinlandInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "fi", "Finland", "en-fi", "w+CAIQICIHRmlubGFuZA==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleFinlandMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "fi", "Finland", "en-fi", "w+CAIQICIHRmlubGFuZA==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDEInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "de", "Germany", "en-de", "w+CAIQICIHR2VybWFueQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDEMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "de", "Germany", "en-de", "w+CAIQICIHR2VybWFueQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleFRInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "fr", "France", "en-fr", "w+CAIQICIGRnJhbmNl");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;

        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleFRMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "fr", "France", "en-fr", "w+CAIQICIGRnJhbmNl");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleITInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "it", "Italy", "en-it", "w+CAIQICIFSXRhbHk=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleITMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "it", "Italy", "en-it", "w+CAIQICIFSXRhbHk=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLondonCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                //dict = await GetOxylabsWebDataSources(keyword, "co.uk", "London, United Kingdom", "en-gb", "w+CAIQICIdTG9uZG9uLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "London,England,United Kingdom", "en-gb", "w+CAIQICIdTG9uZG9uLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLondonCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "London,England,United Kingdom", "en-gb", "w+CAIQICIdTG9uZG9uLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                // dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBirminghamCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Birmingham,England,United Kingdom", "en-gb", "w+CAIQICIhQmlybWluZ2hhbSxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBirminghamCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Birmingham,England,United Kingdom", "en-gb", "w+CAIQICIhQmlybWluZ2hhbSxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLeedsCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Leeds,England,United Kingdom", "en-gb", "w+CAIQICIcTGVlZHMsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLeedsCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Leeds,England,United Kingdom", "en-gb", "w+CAIQICIcTGVlZHMsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSheffieldCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Sheffield,England,United Kingdom", "en-gb", "w+CAIQICIgU2hlZmZpZWxkLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSheffieldCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Sheffield,England,United Kingdom", "en-gb", "w+CAIQICIgU2hlZmZpZWxkLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBradfordCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Bradford,England,United Kingdom", "en-gb", "w+CAIQICIfQnJhZGZvcmQsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBradfordCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Bradford,England,United Kingdom", "en-gb", "w+CAIQICIfQnJhZGZvcmQsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleManchesterCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Manchester,England,United Kingdom", "en-gb", "w+CAIQICIhTWFuY2hlc3RlcixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleManchesterCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Manchester,England,United Kingdom", "en-gb", "w+CAIQICIhTWFuY2hlc3RlcixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLiverpoolCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Liverpool,England,United Kingdom", "en-gb", "w+CAIQICIgTGl2ZXJwb29sLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLiverpoolCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Liverpool,England,United Kingdom", "en-gb", "w+CAIQICIgTGl2ZXJwb29sLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBristolCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Bristol,England,United Kingdom", "en-gb", "w+CAIQICIeQnJpc3RvbCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBristolCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Bristol,England,United Kingdom", "en-gb", "w+CAIQICIeQnJpc3RvbCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNewcastleCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Newcastle,Northern Ireland,United Kingdom", "en-gb", "w+CAIQICIpTmV3Y2FzdGxlLE5vcnRoZXJuIElyZWxhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNewcastleCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Newcastle,Northern Ireland,United Kingdom", "en-gb", "w+CAIQICIpTmV3Y2FzdGxlLE5vcnRoZXJuIElyZWxhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;

        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSunderlandCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Sunderland,England,United Kingdom", "en-gb", "w+CAIQICIhU3VuZGVybGFuZCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSunderlandCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Sunderland,England,United Kingdom", "en-gb", "w+CAIQICIhU3VuZGVybGFuZCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleWolverhamptonCityDesktop(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Wolverhampton,England,United Kingdom", "en-gb", "w+CAIQICIkV29sdmVyaGFtcHRvbixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleWolverhamptonCityMobile(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Wolverhampton,England,United Kingdom", "en-gb", "w+CAIQICIkV29sdmVyaGFtcHRvbixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePlymouthCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Plymouth,England,United Kingdom", "en-gb", "w+CAIQICIfUGx5bW91dGgsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePlymouthCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Plymouth,England,United Kingdom", "en-gb", "w+CAIQICIfUGx5bW91dGgsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCardiffCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Cardiff,England,United Kingdom", "en-gb", "w+CAIQICIcQ2FyZGlmZixXYWxlcyxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCardiffCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Cardiff,Wales,United Kingdom", "en-gb", "w+CAIQICIcQ2FyZGlmZixXYWxlcyxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleOxfordCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Oxford,England,United Kingdom", "en-gb", "w+CAIQICIdT3hmb3JkLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleOxfordCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Oxford,England,United Kingdom", "en-gb", "w+CAIQICIdT3hmb3JkLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCambridgeCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Cambridge,England,United Kingdom", "en-gb", "w+CAIQICIgQ2FtYnJpZGdlLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCambridgeCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Cambridge,England,United Kingdom", "en-gb", "w+CAIQICIgQ2FtYnJpZGdlLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBelfastCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Belfast, United Kingdom", "en-gb", "w+CAIQICInQmVsZmFzdCxOb3J0aGVybiBJcmVsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBelfastCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Belfast, United Kingdom", "en-gb", "w+CAIQICInQmVsZmFzdCxOb3J0aGVybiBJcmVsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleGlasgowCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Glasgow, United Kingdom", "en-gb", "w+CAIQICIfR2xhc2dvdyxTY290bGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleGlasgowCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Glasgow, United Kingdom", "en-gb", "w+CAIQICIfR2xhc2dvdyxTY290bGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleEdinburghCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Edinburgh, United Kingdom", "en-gb", "w+CAIQICIhRWRpbmJ1cmdoLFNjb3RsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleEdinburghCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Edinburgh, United Kingdom", "en-gb", "w+CAIQICIhRWRpbmJ1cmdoLFNjb3RsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBrightonCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Brighton, United Kingdom", "en-gb", "w+CAIQICIfQnJpZ2h0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBrightonCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Brighton, United Kingdom", "en-gb", "w+CAIQICIfQnJpZ2h0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleESInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "es", "Spain", "es-es", "w+CAIQICIFc3BhaW4=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleITInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "it", "Italy", "it-it", "w+CAIQICIFSXRhbHk=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSEInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "se", "Sweden", "sv-se", "w+CAIQICIGc3dlZGVu");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCNInternet(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "zh-cn-hk", "w+CAIQICIJSG9uZyBLb25n");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHKInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "zh-TW-HK", "w+CAIQICIJSG9uZyBLb25n");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSGInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.sg", "Singapore", "en-sg", "w+CAIQICIJU2luZ2Fwb3Jl");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleJapanInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.jp", "Japan", "ja-jp", "w+CAIQICIFSmFwYW4=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBrasilInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.br", "Brazil", "pt-br", "w+CAIQICIGQnJhemls");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDEInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "de", "Germany", "de-de", "w+CAIQICIHR2VybWFueQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCHInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "de-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLUInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "lu", "Luxembourg", "de-lu", "w+CAIQICIKTHV4ZW1ib3VyZw==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleATInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "at", "Austria", "de-at", "w+CAIQICIHQXVzdHJpYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        private async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKImages_PageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources_Nws_Images1(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                ArrayList A = ImagesPattern(htmlsource, "PageLinks");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleAE(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ae", "United Arab Emirates", "ar-ae", "w+CAIQICIUVW5pdGVkIEFyYWIgRW1pcmF0ZXM=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleTR(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.tr", "Turkey", "tr-tr", "w+CAIQICIGVHVya2V5");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMA(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.ma", "Morocco", "fr-ma", "w+CAIQICIHTW9yb2Njbw==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLY(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.ly", "Libya", "ar-ly", "w+CAIQICIFTGlieWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDZ(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "dz", "Algeria", "fr-dz", "w+CAIQICIHQWxnZXJpYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUA(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.ua", "Ukraine", "uk-ua", "w+CAIQICIHVWtyYWluZQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleEgypt(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.eg", "Egypt", "ar-eg", "w+CAIQICIFRWd5cHQ=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBH(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.bh", "Bahrain", "ar-bh", "w+CAIQICIHQmFocmFpbg==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleQA(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.qa", "Qatar", "ar-qa", "w+CAIQICIFUWF0YXI=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSaudi(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.sa", "Saudia Arabia", "ar-sa", "w+CAIQICIMU2F1ZGkgQXJhYmlh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleKW(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.kw", "Kuwait", "ar-kw", "w+CAIQICIGS3V3YWl0");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleVN(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.vn", "Vietnam", "vi-vn", "w+CAIQICIHVmlldG5hbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleGabon(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ga", "Gabon", "fr-ga", "w+CAIQICIFR2Fib24=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleIN(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.in", "India", "en-in", "w+CAIQICIFSW5kaWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePT(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "pt", "Portugal", "pt-pt", "w+CAIQICIIUG9ydHVnYWw=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCZ(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "cz", "Czech Republic", "cs-cz", "w+CAIQICIHQ3plY2hpYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSK(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "sk", "Slovakia", "sk-sk", "w+CAIQICIIU2xvdmFraWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleIL(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.il", "Israel", "iw-il", "w+CAIQICIGSXNyYWVs");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSLocal(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com", "United States", "en-us", "w+CAIQICINVW5pdGVkIFN0YXRlcw==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleZALocal(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.za", "South Africa", "en-za", "w+CAIQICIPU291dGggQWZyaWNhCgoK");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUSMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com", "United States", "en-us", "w+CAIQICINVW5pdGVkIFN0YXRlcw==");
                // dict = MobilePattern(htmlsource, "");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> getTop100GoogleITMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "it", "Italy", "it-IT", "w+CAIQICIFSXRhbHk=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleIELocal(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ie", "Ireland", "en-ie", "w+CAIQICIHSXJlbGFuZA==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHoveCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Hove, United Kingdom", "en-gb", "w+CAIQICIoQnJpZ2h0b24gYW5kIEhvdmUsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleHoveCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Hove, United Kingdom", "en-gb", "w+CAIQICIoQnJpZ2h0b24gYW5kIEhvdmUsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSouthamptonCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Southampton, United Kingdom", "en-gb", "w+CAIQICIiU291dGhhbXB0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSouthamptonCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Southampton, United Kingdom", "en-gb", "w+CAIQICIiU291dGhhbXB0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePhilippinesInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.ph", "Philippines", "en-ph", "w+CAIQICILUGhpbGlwcGluZXM="); ;
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePhilippinesInternetMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.ph", "Philippines", "en-ph", "w+CAIQICILUGhpbGlwcGluZXM=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBahamasInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "bs", "Bahamas", "en-bs", "w+CAIQICIHQmFoYW1hcw==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBahamasInternetMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "bs", "Bahamas", "en-bs", "w+CAIQICIHQmFoYW1hcw==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleJamaicaInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.jm", "Jamaica", "en-jm", "w+CAIQICIHSmFtYWljYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleJamaicaInternetMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.jm", "Jamaica", "en-jm", "w+CAIQICIHSmFtYWljYQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleAus(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.au", "Australia", "en-au", "w+CAIQICIJQXVzdHJhbGlh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMexicoInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.mx", "Mexico", "en-mx", "w+CAIQICIGTWV4aWNv");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMexicoInternetMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.mx", "Mexico", "en-mx", "w+CAIQICIGTWV4aWNv");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePuertoRicoInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.pr", "Puerto Rico", "en-pr", "w+CAIQICILUHVlcnRvIFJpY28=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePuertoRicoInternetMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.pr", "Puerto Rico", "en-pr", "w+CAIQICILUHVlcnRvIFJpY28=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePuertoRicoInternetSpanish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.pr", "Puerto Rico", "es-419-pr", "w+CAIQICILUHVlcnRvIFJpY28=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePuertoRicoInternetMobileSpanish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.pr", "Puerto Rico", "es-419-pr", "w+CAIQICILUHVlcnRvIFJpY28=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCanadaTorontoCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ca", "Toronto, Canada", "en-ca", "w+CAIQICIWVG9yb250byxPbnRhcmlvLENhbmFkYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleCanadaTorontoCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ca", "Toronto, Canada", "en-ca", "w+CAIQICIWVG9yb250byxPbnRhcmlvLENhbmFkYQ==");
                // dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleVenezuela(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.ve", "Venezuela", "es-419-ve", "w+CAIQICIJVmVuZXp1ZWxh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleVenezuelaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.ve", "Venezuela", "es-419-ve", "w+CAIQICIJVmVuZXp1ZWxh");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleArgentinaInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.ar", "Argentina", "es-419-ar", "w+CAIQICIJQXJnZW50aW5h");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDallasTexasCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com", "Dallas, Texas, United States", "en-us", "w+CAIQICIaRGFsbGFzLFRleGFzLFVuaXRlZCBTdGF0ZXM=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleDallasTexasCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com", "Dallas, Texas, United States", "en-us", "w+CAIQICIaRGFsbGFzLFRleGFzLFVuaXRlZCBTdGF0ZXM=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleNGMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.ng", "Nigeria", "en-ng", "w+CAIQICIHTmlnZXJpYQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleKenya(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.ke", "Kenya", "en-ke", "w+CAIQICIFS2VueWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleKenyaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.ke", "Kenya", "en-ke", "w+CAIQICIFS2VueWE=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSydneyCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.au", "Sydney, Australia", "en-au", "w+CAIQICIgU3lkbmV5LE5ldyBTb3V0aCBXYWxlcyxBdXN0cmFsaWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMelbourneCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.au", "Melbourne, Australia", "en-au", "w+CAIQICIcTWVsYm91cm5lLFZpY3RvcmlhLEF1c3RyYWxpYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;

        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBrisbanCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.au", "Brisban, Australia", "en-au", "w+CAIQICIdQnJpc2JhbmUsUXVlZW5zbGFuZCxBdXN0cmFsaWE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePerthCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.au", "Perth, Australia", "en-au", "w+CAIQICIhUGVydGgsV2VzdGVybiBBdXN0cmFsaWEsQXVzdHJhbGlh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleAdelaideCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.au", "Adelaide, Australia", "en-au", "w+CAIQICIiQWRlbGFpZGUsU291dGggQXVzdHJhbGlhLEF1c3RyYWxpYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleSydneyCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.au", "Sydney, Australia", "en-au", "w+CAIQICIgU3lkbmV5LE5ldyBTb3V0aCBXYWxlcyxBdXN0cmFsaWE=");
                // dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleMelbourneCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.au", "Melbourne, Australia", "en-au", "w+CAIQICIcTWVsYm91cm5lLFZpY3RvcmlhLEF1c3RyYWxpYQ==");
                // dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleBrisbanCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.au", "Brisban, Australia", "en-au", "w+CAIQICIdQnJpc2JhbmUsUXVlZW5zbGFuZCxBdXN0cmFsaWE=");
                // dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GooglePerthCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.au", "Perth, Australia", "en-au", "w+CAIQICIhUGVydGgsV2VzdGVybiBBdXN0cmFsaWEsQXVzdHJhbGlh");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleAdelaideCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.au", "Adelaide, Australia", "en-au", "w+CAIQICIiQWRlbGFpZGUsU291dGggQXVzdHJhbGlhLEF1c3RyYWxpYQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleBangladeshInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.bd", "Bangladesh", "en-bd", "w+CAIQICIKQmFuZ2xhZGVzaA");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleBangladeshMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.bd", "Bangladesh", "en-bd", "w+CAIQICIKQmFuZ2xhZGVzaA");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleUK(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLatviaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "lv", "Latvia", "lv-lv", "w+CAIQICIGTGF0dmlh");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleLatviaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "lv", "Latvia", "lv-lv", "w+CAIQICIGTGF0dmlh");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        private async Task<Dictionary<string, ArrayList>> getTop100GoogleAspenColoradoDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com", "Aspen,Colorado,United States", "en-us", "w+CAIQICIcQXNwZW4sQ29sb3JhZG8sVW5pdGVkIFN0YXRlcw==");

                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        private async Task<Dictionary<string, ArrayList>> getTop100GoogleAspenColoradoMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com", "Aspen,Colorado,United States", "en-us", "w+CAIQICIcQXNwZW4sQ29sb3JhZG8sVW5pdGVkIFN0YXRlcw==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        private async Task<Dictionary<string, ArrayList>> getTop100GoogleNapaCaliforniaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com", "Napa,California,United States", "en-us", "w+CAIQICIfbmFwYSwgY2FsaWZvcm5pYSwgdW5pdGVkIHN0YXRlcw==");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        private async Task<Dictionary<string, ArrayList>> getTop100GoogleNapaCaliforniaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com", "Napa,California,United States", "en-us", "w+CAIQICIfbmFwYSwgY2FsaWZvcm5pYSwgdW5pdGVkIHN0YXRlcw==");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        private async Task<Dictionary<string, ArrayList>> getTop100GoogleOmanArabicDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.om", "Oman", "ar-om", "w+CAIQICIET21hbg");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        private async Task<Dictionary<string, ArrayList>> getTop100GoogleOmanArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.om", "Oman", "ar-om", "w+CAIQICIET21hbg");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        private async Task<Dictionary<string, ArrayList>> getTop100GoogleOmanEnglishDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.om", "Oman", "en-om", "w+CAIQICIET21hbg");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        private async Task<Dictionary<string, ArrayList>> getTop100GoogleOmanEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.om", "Oman", "en-om", "w+CAIQICIET21hbg");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        /*private async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKImages_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources_Nws_Images(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                ArrayList A1 = ImagesPattern(htmlsource, "ImageLinks");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }*/

        private async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKImages_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = await GetOxylabsWebDataSources_Nws_ImagesDesktop(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");

                ArrayList myList = new ArrayList();
                ArrayList alDup = new ArrayList();

                foreach (string[] src in htmlsource)
                {

                    string html = src[1].ToString().Replace(@"\", "");
                    string matchPattern = "\"ou\":\"(.*?)\",";
                    Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    MatchCollection mc = re.Matches(html);

                    foreach (Match m in mc)
                    {

                        string HtmlText = HttpUtility.HtmlDecode(m.Groups[1].Value);
                        if (HtmlText.StartsWith("http") || HtmlText.StartsWith("https"))
                        {
                            int n = HtmlText.IndexOf("?");
                            if (n > 0)
                                HtmlText = HtmlText.Remove(n);
                            HtmlText = checkurls(HtmlText);
                            alDup.Add(HtmlText);
                        }
                    }
                    foreach (string s in alDup)
                    {
                        if (myList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                        myList.Add(s);
                    }

                    if (myList.Count > 100)
                    {
                        myList.RemoveRange(100, myList.Count - 100);
                    }

                    if (myList.Count > 0)
                        dict.Add(src[0] + ":" + src[2], myList);
                }

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        private async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKMobileImages_PageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources_Nws_Images_Mobile(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                ArrayList A2 = PageResultsPatternMobile(htmlsource, "PageLinks");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        /*private async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKMobileImages_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = await GetOxylabsWebDataSources_Nws_Images_Mobile2(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                ArrayList A3 = PageResultsPatternMobile2("htmlsource");

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }*/

        private async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKMobileImages_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = await GetOxylabsWebDataSources_Nws_Images_Mobile2(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                //dict = ImagesPatternMobile(htmlsource, keyword, "ImageLinks");

                ArrayList myList = new ArrayList();
                ArrayList alDup = new ArrayList();

                foreach (string[] src in htmlsource)
                {
                    string html = src[1].ToString().Replace(@"\", "");
                    string matchPattern = "n,\\[\"(http.*?)\"";
                    string matchPattern1 = "\"ou\":\"(.*?)\",";
                    Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    MatchCollection mc = re.Matches(html);

                    foreach (Match m in mc)
                    {

                        string HtmlText = HttpUtility.HtmlDecode(m.Groups[1].Value);
                        if (HtmlText.StartsWith("http") || HtmlText.StartsWith("https"))
                        {
                            int n = HtmlText.IndexOf("?");
                            if (n > 0)
                                HtmlText = HtmlText.Remove(n);
                            HtmlText = checkurls(HtmlText);
                            alDup.Add(HtmlText);
                        }
                    }
                    re = new Regex(matchPattern1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    mc = re.Matches(html);

                    foreach (Match m in mc)
                    {
                        string HtmlText = HttpUtility.HtmlDecode(m.Groups[1].Value);
                        if (HtmlText.StartsWith("http") || HtmlText.StartsWith("https"))
                        {
                            int n = HtmlText.IndexOf("?");
                            if (n > 0)
                                HtmlText = HtmlText.Remove(n);
                            HtmlText = checkurls(HtmlText);
                            alDup.Add(HtmlText);
                        }
                    }
                    foreach (string s in alDup)
                    {
                        if (myList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                        myList.Add(s);
                    }

                    if (myList.Count > 100)
                    {
                        myList.RemoveRange(100, myList.Count - 100);
                    }

                    if (myList.Count > 0)
                        dict.Add(src[0] + ":" + src[2], myList);
                }

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleHemelHempsteadCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Hemel Hempstead,England,United Kingdom", "en-gb", "w+CAIQICIfSGVtZWwgSGVtcHN0ZWFkLCBVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleHemelHempsteadCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Hemel Hempstead,England,United Kingdom", "en-gb", "w+CAIQICIfSGVtZWwgSGVtcHN0ZWFkLCBVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleLeicesterCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Leicester,England,United Kingdom", "en-gb", "w+CAIQICIZTGVpY2VzdGVyLCBVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleLeicesterCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Leicester,England,United Kingdom", "en-gb", "w+CAIQICIZTGVpY2VzdGVyLCBVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleNottinghamCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Nottingham,England,United Kingdom", "en-gb", "w+CAIQICIaTm90dGluZ2hhbSwgVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleNottinghamCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Nottingham,England,United Kingdom", "en-gb", "w+CAIQICIaTm90dGluZ2hhbSwgVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GooglePortsmouthCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Portsmouth,England,United Kingdom", "en-gb", "w+CAIQICIaUG9ydHNtb3V0aCwgVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GooglePortsmouthCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Portsmouth,England,United Kingdom", "en-gb", "w+CAIQICIaUG9ydHNtb3V0aCwgVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleReadingCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Reading,England,United Kingdom", "en-gb", "w+CAIQICIXUmVhZGluZywgVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleReadingCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Reading,England,United Kingdom", "en-gb", "w+CAIQICIXUmVhZGluZywgVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleStokeonTrentCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Stoke-on-Trent,England,United Kingdom", "en-gb", "w+CAIQICIeU3Rva2Utb24tVHJlbnQsIFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleStokeonTrentCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Stoke-on-Trent,England,United Kingdom", "en-gb", "w+CAIQICIeU3Rva2Utb24tVHJlbnQsIFVuaXRlZCBLaW5nZG9t");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleSwanseaCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Swansea,Wales,United Kingdom", "en-gb", "w+CAIQICIXU3dhbnNlYSwgVW5pdGVkIEtpbmdkb20=");
                //                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleSwanseaCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Swansea,Wales,United Kingdom", "en-gb", "w+CAIQICIXU3dhbnNlYSwgVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleVNMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.vn", "Vietnam", "vi-vn", "w+CAIQICIHVmlldG5hbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleMyanmarInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.mm", "Myanmar", "my-mm", "w+CAIQICIHTXlhbm1hcg==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleMyanmarInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.mm", "Myanmar", "my-mm", "w+CAIQICIHTXlhbm1hcg==");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        /*private async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKDesktop_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources_Nws_Images(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                ArrayList A1 = ImagesPattern(htmlsource, "ImageLinks");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }*/


        private async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKDesktop_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = await GetOxylabsWebDataSources_Nws_Images2(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");

                ArrayList myList = new ArrayList();
                ArrayList alDup = new ArrayList();

                foreach (string[] src in htmlsource)
                {

                    string html = src[1].ToString().Replace(@"\", "");
                    string matchPattern = "\"ou\":\"(.*?)\",";
                    Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    MatchCollection mc = re.Matches(html);

                    foreach (Match m in mc)
                    {

                        string HtmlText = HttpUtility.HtmlDecode(m.Groups[1].Value);
                        if (HtmlText.StartsWith("http") || HtmlText.StartsWith("https"))
                        {
                            int n = HtmlText.IndexOf("?");
                            if (n > 0)
                                HtmlText = HtmlText.Remove(n);
                            HtmlText = checkurls(HtmlText);
                            alDup.Add(HtmlText);
                        }
                    }
                    foreach (string s in alDup)
                    {
                        if (myList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                        myList.Add(s);
                    }

                    if (myList.Count > 100)
                    {
                        myList.RemoveRange(100, myList.Count - 100);
                    }

                    if (myList.Count > 0)
                        dict.Add(src[0] + ":" + src[2], myList);
                }

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        private async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKMobile_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = await GetOxylabsWebDataSources_Nws_Images_Mobile2(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");

                ArrayList myList = new ArrayList();
                ArrayList alDup = new ArrayList();

                foreach (string[] src in htmlsource)
                {
                    string html = src[1].ToString().Replace(@"\", "");
                    string matchPattern = "n,\\[\"(http.*?)\"";
                    string matchPattern1 = "\"ou\":\"(.*?)\",";
                    Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    MatchCollection mc = re.Matches(html);

                    foreach (Match m in mc)
                    {

                        string HtmlText = HttpUtility.HtmlDecode(m.Groups[1].Value);
                        if (HtmlText.StartsWith("http") || HtmlText.StartsWith("https"))
                        {
                            int n = HtmlText.IndexOf("?");
                            if (n > 0)
                                HtmlText = HtmlText.Remove(n);
                            HtmlText = checkurls(HtmlText);
                            alDup.Add(HtmlText);
                        }
                    }
                    re = new Regex(matchPattern1, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    mc = re.Matches(html);

                    foreach (Match m in mc)
                    {
                        string HtmlText = HttpUtility.HtmlDecode(m.Groups[1].Value);
                        if (HtmlText.StartsWith("http") || HtmlText.StartsWith("https"))
                        {
                            int n = HtmlText.IndexOf("?");
                            if (n > 0)
                                HtmlText = HtmlText.Remove(n);
                            HtmlText = checkurls(HtmlText);
                            alDup.Add(HtmlText);
                        }
                    }
                    foreach (string s in alDup)
                    {
                        if (myList.Contains(s) || string.IsNullOrEmpty(s)) continue;
                        myList.Add(s);
                    }

                    if (myList.Count > 100)
                    {
                        myList.RemoveRange(100, myList.Count - 100);
                    }

                    if (myList.Count > 0)
                        dict.Add(src[0] + ":" + src[2], myList);
                }

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public static string checkurls(string unicodestring)
        {
            string str = "";

            try
            {
                if (unicodestring.Contains("u0"))
                {
                    //unicodestring = unicodestring.Insert(unicodestring.IndexOf("u0"), @"\");
                    unicodestring = unicodestring.Replace("u0", @"\u0");
                    int n = unicodestring.IndexOf(@"\u0");
                    if (n > 0)
                    {
                        str = Regex.Unescape(unicodestring);
                    }
                    return str;
                }
                else
                {
                    return unicodestring;
                }
            }
            catch (Exception)
            {
                return unicodestring;
            }
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleRomaniaInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ro", "Romania", "ro-ro", "w+CAIQICIHUm9tYW5pYQ==");

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleSwitzerlandEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "en-ch", "w+CAIQICILU3dpdHplcmxhbmQ=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleSwitzerlandEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ch", "Switzerland", "en-ch", "w+CAIQICILU3dpdHplcmxhbmQ=");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleBahrainEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.bh", "Bahrain", "en-bh", "w+CAIQICIHQmFocmFpbg==");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleEgyptEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.eg", "Egypt", "en-eg", "w+CAIQICIFRWd5cHQ=");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleJordanEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "jo", "Jordan", "en-jo", "w+CAIQICIGSm9yZGFu");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleKuwaitEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.kw", "Kuwait", "en-kw", "w+CAIQICIGS3V3YWl0");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleLebanonEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.lb", "Lebanon", "en-lb", "w+CAIQICIHTGViYW5vbg==");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleQatarEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.qa", "Qatar", "en-qa", "w+CAIQICIFUWF0YXI=");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleSaudiArabiaEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.sa", "Saudi Arabia", "en-sa", "w+CAIQICIMU2F1ZGkgQXJhYmlh");
                //dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleBahrainEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.bh", "Bahrain", "en-bh", "w+CAIQICIHQmFocmFpbg==");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleEgyptEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.eg", "Egypt", "en-eg", "w+CAIQICIFRWd5cHQ=");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleJordanEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "jo", "Jordan", "en-jo", "w+CAIQICIGSm9yZGFu");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleKuwaitEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.kw", "Kuwait", "en-kw", "w+CAIQICIGS3V3YWl0");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleLebanonEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.lb", "Lebanon", "en-lb", "w+CAIQICIHTGViYW5vbg==");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleQatarEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.qa", "Qatar", "en-qa", "w+CAIQICIFUWF0YXI=");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleSaudiArabiaEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.sa", "Saudi Arabia", "en-sa", "w+CAIQICIMU2F1ZGkgQXJhYmlh");
                //dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleBahrainArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.bh", "Bahrain", "ar-bh", "w+CAIQICIHQmFocmFpbg==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleEgyptArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.eg", "Egypt", "ar-eg", "w+CAIQICIFRWd5cHQ=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleJordanArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "jo", "Jordan", "ar-jo", "w+CAIQICIGSm9yZGFu");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleKuwaitArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.kw", "Kuwait", "ar-kw", "w+CAIQICIGS3V3YWl0");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleLebanonArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.lb", "Lebanon", "ar-lb", "w+CAIQICIHTGViYW5vbg==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleQatarArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.qa", "Qatar", "ar-qa", "w+CAIQICIFUWF0YXI=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleKoreanMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.kr", "South Korea", "ko-kr", "w+CAIQICILU291dGggS29yZWE=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleCzechRepublicEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "cz", "Czech Republic", "en-cz", "w+CAIQICIOQ3plY2ggUmVwdWJsaWM=");
                // dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleCzechRepublicEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "cz", "Czech Republic", "en-cz", "w+CAIQICIOQ3plY2ggUmVwdWJsaWM=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleMaltaEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.mt", "Malta", "en-mt", "w+CAIQICIFbWFsdGE=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleIceland(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "is", "Iceland", "is-is", "w+CAIQICIHSWNlbGFuZA==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleIcelandEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "is", "Iceland", "en-is", "w+CAIQICIHSWNlbGFuZA==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleMaltaEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.mt", "Malta", "en-mt", "w+CAIQICIFbWFsdGE=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleIcelandMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "is", "Iceland", "is-is", "w+CAIQICIHSWNlbGFuZA==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleIcelandEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "is", "Iceland", "en-is", "w+CAIQICIHSWNlbGFuZA==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKPeterboroughEnglish(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Peterborough,England,United Kingdom", "en-gb", "w+CAIQICIjUGV0ZXJib3JvdWdoLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleTWIEnglish(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.tw", "Taiwan", "en-tw", "w+CAIQICIGVGFpd2Fu");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleNewcastleCityEnglandDesktop(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Newcastle upon Tyne,England,United Kingdom", "en-gb", "w+CAIQICIqTmV3Y2FzdGxlIHVwb24gVHluZSxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKPeterboroughEnglishMobile(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Peterborough,England,United Kingdom", "en-gb", "w+CAIQICIjUGV0ZXJib3JvdWdoLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }
        public async Task<Dictionary<string, ArrayList>> getTop100GoogleTWInternetEnglishMobile(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.tw", "Taiwan", "en-tw", "w+CAIQICIGVGFpd2Fu");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleNewcastleCityEnglandMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Newcastle,Northern Ireland,United Kingdom", "en-gb", "w+CAIQICIpTmV3Y2FzdGxlLE5vcnRoZXJuIElyZWxhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKLondonE1Desktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "E1,England,United Kingdom", "en-gb", "w+CAIQICIZRTEsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKLondonNW1Desktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "NW1,England,United Kingdom", "en-gb", "w+CAIQICIaTlcxLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKLondonSE1Desktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "SE1,England,United Kingdom", "en-gb", "w+CAIQICIaU0UxLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKLondonE1Mobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "E1,England,United Kingdom", "en-gb", "w+CAIQICIZRTEsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;

        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKLondonNW1Mobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "NW1,England,United Kingdom", "en-gb", "w+CAIQICIaTlcxLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;

        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKLondonSE1Mobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "SE1,England,United Kingdom", "en-gb", "w+CAIQICIaU0UxLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;

        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKAberdeenDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Aberdeen,Scotland,United Kingdom", "en-gb", "w+CAIQICIgQWJlcmRlZW4sU2NvdGxhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKBoltonDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Bolton,England,United Kingdom", "en-gb", "w+CAIQICIdQm9sdG9uLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKBournemouthDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Bournemouth,England,United Kingdom", "en-gb", "w+CAIQICIiQm91cm5lbW91dGgsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKCoventryDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Coventry,England,United Kingdom", "en-gb", "w+CAIQICIfQ292ZW50cnksRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKCroydonDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Croydon,England,United Kingdom", "en-gb", "w+CAIQICIeQ3JveWRvbixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKNorthamptonDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Northampton,England,United Kingdom", "en-gb", "w+CAIQICIiTm9ydGhhbXB0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKNorwichDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Norwich,England,United Kingdom", "en-gb", "w+CAIQICIeTm9yd2ljaCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKSloughDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Slough,England,United Kingdom", "en-gb", "w+CAIQICIdU2xvdWdoLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKWatfordDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "Watford,England,United Kingdom", "en-gb", "w+CAIQICIeV2F0Zm9yZCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;

        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKLondonSW1VDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "co.uk", "SW1V,England,United Kingdom", "en-gb", "w+CAIQICIbU1cxVixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;

        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleCyprusDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "com.cy", "Cyprus", "EL-CY", "w+CAIQICIGQ3lwcnVz");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleEstoniaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "ee", "Estonia", "ET-EE", "w+CAIQICIHRXN0b25pYQ==");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
          return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleLithuaniaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataSources(keyword, "lt", "Lithuania", "LT-LT", "w+CAIQICIJTGl0aHVhbmlh");
                //dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        ///-------------------------------------------------------------------////

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleCyprusMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "com.cy", "Cyprus", "EL-CY", "w+CAIQICIGQ3lwcnVz");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleEstoniaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "ee", "Estonia", "ET-EE", "w+CAIQICIHRXN0b25pYQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleLithuaniaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {

                dict = await GetOxylabsWebDataMobileSources(keyword, "lt", "Lithuania", "LT-LT", "w+CAIQICIJTGl0aHVhbmlh");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKLondonSW1VMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "SW1V,England,United Kingdom", "en-gb", "w+CAIQICIbU1cxVixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKAberdeenMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict  = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Aberdeen,Scotland,United Kingdom", "en-gb", "w+CAIQICIgQWJlcmRlZW4sU2NvdGxhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKBoltonMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Bolton,England,United Kingdom", "en-gb", "w+CAIQICIdQm9sdG9uLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKBournemouthMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Bournemouth,England,United Kingdom", "en-gb", "w+CAIQICIiQm91cm5lbW91dGgsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKCoventryMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Coventry,England,United Kingdom", "en-gb", "w+CAIQICIfQ292ZW50cnksRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;


        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKCroydonMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Croydon,England,United Kingdom", "en-gb", "w+CAIQICIeQ3JveWRvbixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKNorthamptonMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Northampton,England,United Kingdom", "en-gb", "w+CAIQICIiTm9ydGhhbXB0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKNorwichMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Norwich,England,United Kingdom", "en-gb", "w+CAIQICIeTm9yd2ljaCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKSloughMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Slough,England,United Kingdom", "en-gb", "w+CAIQICIdU2xvdWdoLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                //dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;


        }


        public async Task<Dictionary<string, ArrayList>> GetTop100GoogleUKWatfordMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                dict = await GetOxylabsWebDataMobileSources(keyword, "co.uk", "Watford,England,United Kingdom", "en-gb", "w+CAIQICIeV2F0Zm9yZCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


    }
}