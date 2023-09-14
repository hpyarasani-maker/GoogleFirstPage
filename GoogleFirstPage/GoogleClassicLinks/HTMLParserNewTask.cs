using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace GoogleFirstPage.GoogleClassicLinks
{
   public class HTMLParserNewTask
    {
        string sIP;
        public string jobid = string.Empty;
        int requestcount = 0;
        string connection1 = ConfigurationManager.ConnectionStrings["allelements"].ToString();

        private ArrayList GetOxylabsWebDataSources(string kwds, string domain, string location, string lang, string uule)
        {
            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwds.Split(','),
                limit = 100,
                pages = 1,
                locale = lang,
                geo_location = location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = "desktop",
                context = new List<Context> {
                    new Context("safe_search", 0)
                   // new Context("safe","off")
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
                        requestcount++;
                        SendToDBTableRequest(requestcount);
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();//chang

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
                            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();//chang

                            string doneresp = "";
                            using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                            {
                                doneresp = reader.ReadToEnd();
                            }
                            res.Close();

                            JObject job = JObject.Parse(doneresp);
                            string status = job["status"].Value<string>();
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

            return alResult;
        }
        private ArrayList GetOxylabsWebDataMobileSources(string kwds, string domain, string location, string lang, string uule)
        {
            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
            string[] kwd = { kwds };
            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwd,
                limit = 100,
                pages = 1,
                locale = lang,
                geo_location = location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = "mobile_android",
                context = new List<Context> {
                    new Context("safe_search", 0)
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
                        requestcount++;
                        SendToDBTableRequest(requestcount);
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();

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
                            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();

                            string doneresp = "";
                            using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                            {
                                doneresp = reader.ReadToEnd();
                            }
                            res.Close();

                            JObject job = JObject.Parse(doneresp);
                            string status = job["status"].Value<string>();
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
                }
                if (lst.Count == cnt) break;

            } while (true);

            return alResult;
        }

        public ArrayList GetOxylabsWebDataSources_Nws_Images(string kwds, string domain, string location, string lang, string uule, string value)
        {

            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
            string[] kwd = { kwds };
            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwd,
                limit = 100,
                pages = 1,
                locale = lang,
                geo_location = location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = "desktop",
                context = new List<Context> {
                    new Context("tbm", value),
                    new Context("safe_search", 0)
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
                        requestcount++;
                        SendToDBTableRequest(requestcount);
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();

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
                            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();

                            string doneresp = "";
                            using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                            {
                                doneresp = reader.ReadToEnd();
                            }
                            res.Close();

                            JObject job = JObject.Parse(doneresp);
                            string status = job["status"].Value<string>();
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
                    //Task.Delay(200).Wait();
                }
                if (lst.Count == cnt) break;

            } while (true);

            return alResult;
        }

        public ArrayList GetOxylabsWebDataSources_Nws_Images_Mobile(string kwds, string domain, string location, string lang, string uule, string value)
        {

            Uri queryUri = new Uri("https://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));
            string[] kwd = { kwds };
            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = domain,
                query = kwd,
                limit = 100,
                pages = 1,
                locale = lang,
                geo_location = location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = "mobile_android",
                context = new List<Context> {
                    new Context("tbm", value),
                    new Context("safe_search", 0)
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
                        requestcount++;
                        SendToDBTableRequest(requestcount);
                        try
                        {
                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                            httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();

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
                            HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();

                            string doneresp = "";
                            using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                            {
                                doneresp = reader.ReadToEnd();
                            }
                            res.Close();

                            JObject job = JObject.Parse(doneresp);
                            string status = job["status"].Value<string>();
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
                }
                if (lst.Count == cnt) break;

            } while (true);

            return alResult;
        }

        public void SendToDBTableRequest(int requestcount)
        {
            string query = "";
            string myDate = DateTime.Today.ToString("yyyy-MM-dd");
            SqlCommand comm;
            int count = 0;
            object live = "";
            try
            {
                using (SqlConnection con = new SqlConnection(connection1))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    comm = new SqlCommand("Select Liverequestcount from LiveToolRequests where Date=@date", con);
                    comm.CommandType = CommandType.Text;
                    comm.CommandTimeout = 0;
                    comm.Parameters.Add("@id", SqlDbType.Int).Value = requestcount;
                    comm.Parameters.Add("@date", SqlDbType.DateTime).Value = myDate;                    
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            live = reader["Liverequestcount"];
                        }
                    }

                    count = requestcount + Convert.ToInt32(live);                    
                    if (count > 0)
                    {
                        query = "UPDATE LiveToolRequests SET Liverequestcount  =" + count + " Where Date='" + myDate + "' ";
                        comm = new SqlCommand(query, con);
                        comm.ExecuteNonQuery();
                    }

                }

            }
            catch (SqlException ex)
            {
                string error = ex.Message;
            }
            catch (Exception ex)
            {
                using (SqlConnection con = new SqlConnection(connection1))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    query = "INSERT INTO dbo.LiveToolRequests (Date,Liverequestcount) VALUES (@date,@reqcnt)";
                    comm = new SqlCommand(query, con);
                    comm.Parameters.Add("@date", SqlDbType.DateTime).Value = myDate;
                    comm.Parameters.Add("@reqcnt", SqlDbType.Int).Value = requestcount;
                    count = comm.ExecuteNonQuery();
                }
            }
        }

        public Dictionary<string, ArrayList> getTop100(string keyword, string seid)
        {

            Dictionary<string, ArrayList> myArrayList = new Dictionary<string, ArrayList>();
            switch (seid)
            {

                case "1":
                    {
                        myArrayList = GetTop100GoogleUS(keyword);
                        break;
                    }
                case "2":
                    {
                        myArrayList = GetTop100GoogleUK(keyword);
                        break;
                    }
                case "7":
                    {
                        myArrayList = GetTop100GoogleAus(keyword);
                        break;
                    }
                case "13":
                    {
                        myArrayList = GetTop100GoogleNZ(keyword);
                        break;
                    }
                case "16":
                    {
                        myArrayList = GetTop100GoogleZA(keyword);
                        break;
                    }
                case "21":
                    {
                        myArrayList = GetTop100GoogleRussia(keyword);
                        break;
                    }
                //case "22":
                //    {
                //        myArrayList = GetTop100GoogleKorean(keyword);
                //        break;
                //    }
                case "27":
                    {
                        myArrayList = GetTop100GoogleIT(keyword);
                        break;
                    }

                case "28":
                    {
                        myArrayList = GetTop100GoogleSE(keyword);
                        break;
                    }


                case "31":
                    {
                        myArrayList = this.GetTop100GoogleCN(keyword);
                        break;
                    }
                case "32":
                    {
                        myArrayList = this.GetTop100GoogleFR(keyword);
                        break;
                    }
                case "33":
                    {
                        myArrayList = this.GetTop100GoogleHK(keyword);
                        break;
                    }


                case "39":
                    {
                        myArrayList = this.GetTop100GoogleES(keyword);
                        break;
                    }
                case "40":
                    {
                        myArrayList = this.GetTop100GoogleFRWeb(keyword);
                        break;
                    }
                case "41":
                    {
                        myArrayList = this.GetTop100GoogleSG(keyword);
                        break;
                    }

                case "44":
                    {
                        myArrayList = this.GetTop100GoogleBEInternet(keyword);
                        break;
                    }
                case "45":
                    {
                        myArrayList = this.GetTop100GoogleBENederlands(keyword);
                        break;
                    }
                case "46":
                    {
                        myArrayList = this.GetTop100GoogleBEBelgie(keyword);
                        break;
                    }

                case "47":
                    {
                        myArrayList = this.GetTop100GoogleJapan(keyword);
                        break;
                    }
                case "49":
                    {
                        myArrayList = this.GetTop100GoogleBrasil(keyword);
                        break;
                    }
                case "51":
                    {
                        myArrayList = this.GetTop100GoogleNederlands(keyword);
                        break;
                    }
                case "52":
                    {
                        myArrayList = this.GetTop100GoogleDE(keyword);
                        break;
                    }
                case "53":
                    {
                        myArrayList = this.GetTop100GoogleCH(keyword);
                        break;
                    }
                case "54":
                    {
                        myArrayList = this.GetTop100GoogleLU(keyword);
                        break;
                    }
                case "55":
                    {
                        myArrayList = this.GetTop100GoogleAT(keyword);
                        break;
                    }

                case "57":
                    {
                        myArrayList = this.GetTop100GoogleNederlandsInternet(keyword);
                        break;
                    }
                case "58":
                    {
                        myArrayList = this.GetTop100GoogleUKInternet(keyword);
                        break;
                    }
                case "59":
                    {
                        myArrayList = this.GetTop100GoogleESInternet(keyword);
                        break;
                    }
                case "60":
                    {
                        myArrayList = this.GetTop100GoogleITInternet(keyword);
                        break;
                    }
                case "61":
                    {
                        myArrayList = this.GetTop100GoogleSEInternet(keyword);
                        break;
                    }
                case "62":
                    {
                        myArrayList = this.GetTop100GoogleCNInternet(keyword);
                        break;
                    }
                case "63":
                    {
                        myArrayList = this.GetTop100GoogleHKInternet(keyword);
                        break;
                    }
                case "64":
                    {
                        myArrayList = this.GetTop100GoogleSGInternet(keyword);
                        break;
                    }
                case "65":
                    {
                        myArrayList = this.GetTop100GoogleJapanInternet(keyword);
                        break;
                    }
                case "66":
                    {
                        myArrayList = this.GetTop100GoogleBrasilInternet(keyword);
                        break;
                    }
                case "67":
                    {
                        myArrayList = this.GetTop100GoogleDEInternet(keyword);
                        break;
                    }
                case "68":
                    {
                        myArrayList = this.GetTop100GoogleCHInternet(keyword);
                        break;
                    }
                case "69":
                    {
                        myArrayList = this.GetTop100GoogleLUInternet(keyword);
                        break;
                    }
                case "70":
                    {
                        myArrayList = this.GetTop100GoogleATInternet(keyword);
                        break;
                    }
                case "74":
                    {
                        myArrayList = this.GetTop100GoogleUKImages_PageURLs(keyword); ;//desktop pattern
                        break;
                    }

                ////case "75":
                ////    {
                ////        myArrayList = this.GetTop100GoogleUKCaffeine(keyword);
                ////        break;
                ////    }

                case "77":
                    {
                        myArrayList = this.GetTop100GoogleAE(keyword);
                        break;
                    }

                case "79":
                    {
                        myArrayList = this.GetTop100GoogleTR(keyword);
                        break;
                    }
                case "80":
                    {
                        myArrayList = this.GetTop100GoogleMA(keyword);
                        break;
                    }
                case "81":
                    {
                        myArrayList = this.GetTop100GoogleLY(keyword);
                        break;
                    }
                case "82":
                    {
                        myArrayList = this.GetTop100GoogleDZ(keyword);
                        break;
                    }
                case "83":
                    {
                        myArrayList = this.GetTop100GoogleUA(keyword);
                        break;
                    }
                case "84":
                    {
                        myArrayList = this.GetTop100GoogleEgypt(keyword);
                        break;
                    }
                case "85":
                    {
                        myArrayList = this.GetTop100GoogleBH(keyword);
                        break;
                    }
                case "86":
                    {
                        myArrayList = this.GetTop100GoogleQA(keyword);
                        break;
                    }
                case "87":
                    {
                        myArrayList = this.GetTop100GoogleSaudi(keyword);
                        break;
                    }
                case "88":
                    {
                        myArrayList = this.GetTop100GoogleKW(keyword);
                        break;
                    }
                case "89":
                    {
                        myArrayList = this.GetTop100GoogleVN(keyword);
                        break;
                    }
                case "90":
                    {
                        myArrayList = this.GetTop100GoogleGabon(keyword);
                        break;
                    }

                case "93":
                    {
                        myArrayList = this.GetTop100GoogleIN(keyword);
                        break;
                    }

                case "95":
                    {
                        myArrayList = this.GetTop100GooglePT(keyword);
                        break;
                    }
                case "96":
                    {
                        myArrayList = this.GetTop100GoogleCZ(keyword);
                        break;
                    }
                case "97":
                    {
                        myArrayList = this.GetTop100GoogleSK(keyword);
                        break;
                    }
                case "98":
                    {
                        myArrayList = this.GetTop100GoogleIL(keyword);
                        break;
                    }
                case "99":
                    {
                        myArrayList = this.GetTop100GoogleUSLocal(keyword);
                        break;
                    }
                //case "100":
                //    {
                //        myArrayList = this.GetTop100GoogleAusLocal(keyword);
                //        break;
                //    }
                case "101":
                    {
                        myArrayList = this.GetTop100GoogleZALocal(keyword);
                        break;
                    }
                case "102":
                    {
                        myArrayList = this.GetTop100GoogleUSMobile(keyword);
                        break;
                    }
                case "104":
                    {
                        myArrayList = this.GetTop100GoogleITMobile(keyword);
                        break;
                    }
                case "105":
                    {
                        myArrayList = this.GetTop100GoogleIELocal(keyword);
                        break;
                    }
                case "106":
                    {
                        myArrayList = this.GetTop100GoogleUKMobileWeb(keyword);
                        break;
                    }
                case "107":
                    {
                        myArrayList = this.GetTop100GoogleCanada(keyword);
                        break;
                    }
                //case "108":
                //    {
                //        myArrayList = this.GetTop100GoogleMY(keyword);
                //        break;
                //    }
                case "109":
                    {
                        myArrayList = this.GetTop100GoogleMYWeb(keyword);
                        break;
                    }
                ////case "110":
                ////    {
                ////        myArrayList = this.GetTop100GoogleTH(keyword);
                ////        break;
                ////    }
                case "111":
                    {
                        myArrayList = this.GetTop100GoogleTHWeb(keyword);
                        break;
                    }
                ////case "112":
                ////    {
                ////        myArrayList = this.GetTop100GoogleNG(keyword);
                ////        break;
                ////    }
                case "113":
                    {
                        myArrayList = this.GetTop100GoogleNGWeb(keyword);
                        break;
                    }
                case "114":
                    {
                        myArrayList = this.GetTop100GoogleCHItalino(keyword);
                        break;
                    }
                case "115":
                    {
                        myArrayList = this.GetTop100GoogleCHFrancais(keyword);
                        break;
                    }
                case "116":
                    {
                        myArrayList = this.GetTop100GoogleCHDeutsch(keyword);
                        break;
                    }
                case "117":
                    {
                        myArrayList = this.GetTop100GooglePTMobile(keyword);
                        break;
                    }
                case "118":
                    {
                        myArrayList = this.GetTop100GoogleNederlandsMobile(keyword);
                        break;
                    }
                case "119":
                    {
                        myArrayList = this.GetTop100GoogleMexico(keyword);
                        break;
                    }
                case "120":
                    {
                        myArrayList = this.GetTop100GoogleESMobile(keyword);
                        break;
                    }

                case "121":
                    {
                        myArrayList = this.GetTop100GoogleAusInternet(keyword);
                        break;
                    }
                case "122":
                    {
                        myArrayList = this.GetTop100GooglePolskiInternet(keyword);
                        break;
                    }
                case "123":
                    {
                        myArrayList = this.GetTop100GoogleRomaniaInternet(keyword);
                        break;
                    }
                case "124":
                    {
                        myArrayList = this.GetTop100GoogleBulgariaInternet(keyword);
                        break;
                    }
                case "125":
                    {
                        myArrayList = this.GetTop100GoogleSlovenijaInternet(keyword);
                        break;
                    }
                case "126":
                    {
                        myArrayList = this.GetTop100GoogleMagyaInternet(keyword);
                        break;
                    }

                case "127":
                    {
                        myArrayList = this.GetTop100GoogleCzechInternet(keyword);
                        break;
                    }
                case "128":
                    {
                        myArrayList = this.GetTop100GoogleSlovakiaInternet(keyword);
                        break;
                    }

                case "129":
                    {
                        myArrayList = this.GetTop100GoogleArgentinaInternet(keyword);
                        break;
                    }
                case "130":
                    {
                        myArrayList = this.GetTop100GoogleLebanonInternet(keyword);
                        break;
                    }
                case "131":
                    {
                        myArrayList = this.GetTop100GoogleJordanInternet(keyword);
                        break;
                    }
                case "132":
                    {
                        myArrayList = this.GetTop100GoogleFinlandInternet(keyword);
                        break;
                    }
                case "133":
                    {
                        myArrayList = this.GetTop100GoogleGreeceInternet(keyword);
                        break;
                    }
                case "134":
                    {
                        myArrayList = this.GetTop100GoogleNorwayInternet(keyword);
                        break;
                    }
                case "135":
                    {
                        myArrayList = this.GetTop100GoogleBelgiumInternet(keyword);
                        break;
                    }
                case "136":
                    {
                        myArrayList = this.GetTop100GoogleDenmarkInternet(keyword);
                        break;
                    }
                case "137":
                    {
                        myArrayList = this.GetTop100GoogleChileInternet(keyword);
                        break;
                    }
                case "139":
                    {
                        myArrayList = this.GetTop100GoogleAusMobile(keyword);
                        break;
                    }

                //case "140":
                //    {
                //        myArrayList = this.GetTop100GoogleUKNews(keyword);
                //        break;
                //    }
                case "141":
                    {
                        myArrayList = this.GetTop100GoogleCHItalinoMobile(keyword);
                        break;
                    }
                case "142":
                    {
                        myArrayList = this.GetTop100GoogleCHFrancaisMobile(keyword);
                        break;
                    }
                case "143":
                    {
                        myArrayList = this.GetTop100GoogleCHDeutschMobile(keyword);
                        break;
                    }

                case "144":
                    {
                        myArrayList = this.GetTop100GooglePTInternet(keyword);
                        break;
                    }
                case "145":
                    {
                        myArrayList = this.GetTop100GoogleDEMobile(keyword);
                        break;
                    }
                case "146":
                    {
                        myArrayList = this.GetTop100GoogleAEMobile(keyword);
                        break;
                    }
                case "147":
                    {
                        myArrayList = this.GetTop100GoogleATMobile(keyword);
                        break;
                    }
                case "148":
                    {
                        myArrayList = this.GetTop100GoogleBEDutchMobile(keyword);
                        break;
                    }
                case "149":
                    {
                        myArrayList = this.GetTop100GoogleBEFrenchMobile(keyword);
                        break;
                    }
                case "150":
                    {
                        myArrayList = this.GetTop100GoogleBrasilMobile(keyword);
                        break;
                    }
                case "151":
                    {
                        myArrayList = this.GetTop100GoogleCanadaMobile(keyword);
                        break;
                    }
                case "152":
                    {
                        myArrayList = this.GetTop100GoogleDenmarkMobile(keyword);
                        break;
                    }

                case "153":
                    {
                        myArrayList = this.GetTop100GoogleFinlandMobile(keyword);
                        break;
                    }
                case "154":
                    {
                        myArrayList = this.GetTop100GoogleFRMobile(keyword);
                        break;
                    }
                case "155":
                    {
                        myArrayList = this.GetTop100GoogleGreeceMobile(keyword);
                        break;
                    }
                case "156":
                    {
                        myArrayList = this.GetTop100GoogleHKMobile(keyword);
                        break;
                    }
                case "157":
                    {
                        myArrayList = this.GetTop100GoogleIEMobile(keyword);
                        break;
                    }

                case "158":
                    {
                        myArrayList = this.GetTop100GoogleJapanMobile(keyword);
                        break;
                    }
                case "159":
                    {
                        myArrayList = this.GetTop100GoogleNorwayMobile(keyword);
                        break;
                    }
                case "160":
                    {
                        myArrayList = this.GetTop100GoogleSEMobile(keyword);
                        break;
                    }
                case "167":
                    {
                        myArrayList = this.GetTop100GoogleHungaryMobileWeb(keyword);
                        break;
                    }
                case "168":
                    {
                        myArrayList = this.GetTop100GooglePolandMobileWeb(keyword);
                        break;
                    }
                case "169":
                    {
                        myArrayList = this.GetTop100GoogleTurkeyMobileWeb(keyword);
                        break;
                    }
                case "172":
                    {
                        myArrayList = this.GetTop100GoogleRussiaMobile(keyword);
                        break;
                    }
                case "173":
                    {
                        myArrayList = this.GetTop100GoogleTWInternet(keyword);
                        break;
                    }
                case "174":
                    {
                        myArrayList = this.GetTop100GoogleILInternet(keyword);
                        break;
                    }

                case "176":
                    {
                        myArrayList = this.GetTop100GoogleSerbiaInternet(keyword);
                        break;
                    }
                case "177":
                    {
                        myArrayList = this.GetTop100GoogleUSNewYorkCity(keyword);
                        break;
                    }
                case "178":
                    {
                        myArrayList = this.GetTop100GoogleUSNewYorkCityMobile(keyword);
                        break;
                    }
                case "179":
                    {
                        myArrayList = this.GetTop100GoogleUSLA(keyword);
                        break;
                    }
                case "180":
                    {
                        myArrayList = this.GetTop100GoogleUSLAMobile(keyword);
                        break;
                    }
                case "181":
                    {
                        myArrayList = this.GetTop100GoogleZAMobile(keyword);
                        break;
                    }
                case "182":
                    {
                        myArrayList = this.GetTop100GoogleSaudiMobileInternet(keyword);
                        break;
                    }
                case "183":
                    {
                        myArrayList = this.GetTop100GoogleCanadaFrenchWeb(keyword);
                        break;
                    }

                case "184":
                    {
                        myArrayList = this.GetTop100GoogleUSChicago(keyword);
                        break;
                    }
                case "185":
                    {
                        myArrayList = this.GetTop100GoogleUSChicagoMobile(keyword);
                        break;
                    }
                case "186":
                    {
                        myArrayList = this.GetTop100GoogleUSSanFran(keyword);
                        break;
                    }
                case "187":
                    {
                        myArrayList = this.GetTop100GoogleUSSanFranMobile(keyword);
                        break;
                    }
                case "188":
                    {
                        myArrayList = this.GetTop100GooglePakistanInternet(keyword);
                        break;
                    }
                case "189":
                    {
                        myArrayList = this.GetTop100GooglePakistanMobileInternet(keyword);
                        break;
                    }

                case "204":
                    {
                        myArrayList = this.GetTop100GoogleINMobileWeb(keyword);
                        break;
                    }
                case "206":
                    {
                        myArrayList = this.GetTop100GoogleCanadaFrenchMobileInternet(keyword);
                        break;
                    }
                case "207":
                    {
                        myArrayList = this.GetTop100GoogleMYMobileInternet(keyword);
                        break;
                    }
                case "208":
                    {
                        myArrayList = this.GetTop100GoogleSGMobileInternet(keyword);
                        break;
                    }

                case "209":
                    {
                        myArrayList = this.GetTop100GoogleCanada1(keyword);
                        break;
                    }
                case "210":
                    {
                        myArrayList = this.GetTop100GoogleCanadaMobile1(keyword);
                        break;
                    }
                case "211":
                    {
                        myArrayList = this.GetTop100GoogleSrilankaMobileInternet(keyword);
                        break;
                    }
                case "212":
                    {
                        myArrayList = this.GetTop100GoogleSrilankaInternet(keyword);
                        break;
                    }
                case "213":
                    {
                        myArrayList = this.GetTop100GoogleHRInternet(keyword);
                        break;
                    }
                case "214":
                    {
                        myArrayList = this.GetTop100GoogleHRMobileInternet(keyword);
                        break;
                    }
                case "215":
                    {
                        myArrayList = this.GetTop100GoogleCZMobileInternet(keyword);
                        break;
                    }
                case "219":
                    {
                        myArrayList = this.GetTop100GoogleTHMobile(keyword);
                        break;
                    }
                case "223":
                    {
                        myArrayList = this.GetTop100GoogleUSHoustonCity(keyword);
                        break;
                    }
                case "224":
                    {
                        myArrayList = this.GetTop100GoogleUSHoustonCityMobile(keyword);
                        break;
                    }
                case "225":
                    {
                        myArrayList = this.GetTop100GoogleUSMiamiCity(keyword);
                        break;
                    }
                case "226":
                    {
                        myArrayList = this.GetTop100GoogleUSMiamiCityMobile(keyword);
                        break;
                    }
                case "233":
                    {
                        myArrayList = this.GetTop100GoogleAEEnglish(keyword);
                        break;
                    }
                case "234":
                    {
                        myArrayList = this.GetTop100GoogleHKInternetEnglish(keyword);
                        break;
                    }
                case "235":
                    {
                        myArrayList = this.GetTop100GoogleMexicoMobile(keyword);
                        break;
                    }
                case "236":
                    {
                        myArrayList = this.GetTop100GoogleMexicoCityMobile(keyword);
                        break;
                    }
                case "237":
                    {
                        myArrayList = this.GetTop100GoogleChileInternetMobile(keyword);
                        break;
                    }
                case "238":
                    {
                        myArrayList = this.GetTop100GooglePeruInternet(keyword);
                        break;
                    }
                case "239":
                    {
                        myArrayList = this.GetTop100GooglePeruInternetMobile(keyword);
                        break;
                    }
                case "240":
                    {
                        myArrayList = this.GetTop100GoogleColumbiaInternet(keyword);
                        break;
                    }
                case "241":
                    {
                        myArrayList = this.GetTop100GoogleColumbiaInternetMobile(keyword);
                        break;
                    }

                case "242":
                    {
                        myArrayList = this.GetTop100GoogleMexicoCity(keyword);
                        break;
                    }
                case "243":
                    {
                        myArrayList = this.GetTop100GoogleIndonesiaInternet(keyword);
                        break;
                    }
                case "244":
                    {
                        myArrayList = this.GetTop100GoogleIndonesiaInternetMobile(keyword);
                        break;
                    }
                case "245":
                    {
                        myArrayList = this.GetTop100GooglePhilippinesInternet(keyword);
                        break;
                    }
                case "246":
                    {
                        myArrayList = this.GetTop100GooglePhilippinesInternetMobile(keyword);
                        break;
                    }
                case "247":
                    {
                        myArrayList = this.GetTop100GoogleSGMobileInternetEnglish(keyword);
                        break;
                    }
                case "248":
                    {
                        myArrayList = this.GetTop100GoogleTHMobileInternetEnglish(keyword);
                        break;
                    }
                case "249":
                    {
                        myArrayList = this.GetTop100GoogleHKMobileInternetEnglish(keyword);
                        break;
                    }
                case "250":
                    {
                        myArrayList = this.GetTop100GoogleMYInternetLanguage(keyword);
                        break;
                    }
                case "251":
                    {
                        myArrayList = this.GetTop100GoogleMYMobileInternetLanguage(keyword);
                        break;
                    }
                case "252":
                    {
                        myArrayList = this.GetTop100GoogleTHInternetEnglish(keyword);
                        break;
                    }
                case "253":
                    {
                        myArrayList = this.GetTop100GoogleSGInternetLanguage(keyword);
                        break;
                    }
                case "254":
                    {
                        myArrayList = this.GetTop100GoogleHKInternetChinaLanguage(keyword);
                        break;
                    }
                case "255":
                    {
                        myArrayList = this.GetTop100GoogleHKMobileInternetChinaLanguage(keyword);
                        break;
                    }

                case "257":
                    {
                        myArrayList = this.GetTop100GoogleNZInternetMobile(keyword);
                        break;
                    }
                case "258":
                    {
                        myArrayList = this.GetTop100GoogleTWInternetMobile(keyword);
                        break;
                    }
                case "259":
                    {
                        myArrayList = this.GetTop100GoogleAEEnglishInternetMobile(keyword);
                        break;
                    }

                case "260":
                    {
                        myArrayList = this.GetTop100GoogleJapanInternetEnglish(keyword);
                        break;
                    }
                case "261":
                    {
                        myArrayList = this.GetTop100GoogleJapanMobileEnglish(keyword);
                        break;
                    }
                case "262":
                    {
                        myArrayList = this.GetTop100GoogleDenmarkInternetEnglish(keyword);
                        break;
                    }
                case "263":
                    {
                        myArrayList = this.GetTop100GoogleDenmarkMobileEnglish(keyword);
                        break;
                    }
                case "264":
                    {
                        myArrayList = this.GetTop100GoogleSEInternetEnglish(keyword);
                        break;
                    }
                case "265":
                    {
                        myArrayList = this.GetTop100GoogleSEMobileEnglish(keyword);
                        break;
                    }
                case "266":
                    {
                        myArrayList = this.GetTop100GoogleNorwayInternetEnglish(keyword);
                        break;
                    }
                case "267":
                    {
                        myArrayList = this.GetTop100GoogleNorwayMobileEnglish(keyword);
                        break;
                    }
                case "268":
                    {
                        myArrayList = this.GetTop100GoogleFinlandInternetEnglish(keyword);
                        break;
                    }
                case "269":
                    {
                        myArrayList = this.GetTop100GoogleFinlandMobileEnglish(keyword);
                        break;
                    }
                case "270":
                    {
                        myArrayList = this.GetTop100GoogleDEInternetEnglish(keyword);
                        break;
                    }
                case "271":
                    {
                        myArrayList = this.GetTop100GoogleDEMobileEnglish(keyword);
                        break;
                    }
                case "272":
                    {
                        myArrayList = this.GetTop100GoogleFRInternetEnglish(keyword);
                        break;
                    }
                case "273":
                    {
                        myArrayList = this.GetTop100GoogleFRMobileEnglish(keyword);
                        break;
                    }
                case "274":
                    {
                        myArrayList = this.GetTop100GoogleITInternetEnglish(keyword);
                        break;
                    }
                case "275":
                    {
                        myArrayList = this.GetTop100GoogleITMobileEnglish(keyword);
                        break;
                    }

                case "279":
                    {
                        myArrayList = this.GetTop100GoogleLondonCityDesktop(keyword);
                        break;
                    }
                case "280":
                    {
                        myArrayList = this.GetTop100GoogleLondonCityMobile(keyword);
                        break;
                    }
                case "281":
                    {
                        myArrayList = this.GetTop100GoogleBirminghamCityDesktop(keyword);
                        break;
                    }
                case "282":
                    {
                        myArrayList = this.GetTop100GoogleBirminghamCityMobile(keyword);
                        break;
                    }
                case "283":
                    {
                        myArrayList = this.GetTop100GoogleLeedsCityDesktop(keyword);
                        break;
                    }
                case "284":
                    {
                        myArrayList = this.GetTop100GoogleLeedsCityMobile(keyword);
                        break;
                    }

                case "285":
                    {
                        myArrayList = this.GetTop100GoogleSheffieldCityDesktop(keyword);
                        break;
                    }
                case "286":
                    {
                        myArrayList = this.GetTop100GoogleSheffieldCityMobile(keyword);
                        break;
                    }
                case "287":
                    {
                        myArrayList = this.GetTop100GoogleBradfordCityDesktop(keyword);
                        break;
                    }
                case "288":
                    {
                        myArrayList = this.GetTop100GoogleBradfordCityMobile(keyword);
                        break;
                    }
                case "289":
                    {
                        myArrayList = this.GetTop100GoogleManchesterCityDesktop(keyword);
                        break;
                    }
                case "290":
                    {
                        myArrayList = this.GetTop100GoogleManchesterCityMobile(keyword);
                        break;
                    }
                case "291":
                    {
                        myArrayList = this.GetTop100GoogleLiverpoolCityDesktop(keyword);
                        break;
                    }
                case "292":
                    {
                        myArrayList = this.GetTop100GoogleLiverpoolCityMobile(keyword);
                        break;
                    }
                case "293":
                    {
                        myArrayList = this.GetTop100GoogleBristolCityDesktop(keyword);
                        break;
                    }
                case "294":
                    {
                        myArrayList = this.GetTop100GoogleBristolCityMobile(keyword);
                        break;
                    }
                case "295":
                    {
                        myArrayList = this.GetTop100GoogleNewcastleCityDesktop(keyword);
                        break;
                    }
                case "296":
                    {
                        myArrayList = this.GetTop100GoogleNewcastleCityMobile(keyword);
                        break;
                    }

                case "297":
                    {
                        myArrayList = this.GetTop100GoogleSunderlandCityDesktop(keyword);
                        break;
                    }
                case "298":
                    {
                        myArrayList = this.GetTop100GoogleSunderlandCityMobile(keyword);
                        break;
                    }
                case "299":
                    {
                        myArrayList = this.GetTop100GoogleWolverhamptonCityDesktop(keyword);
                        break;
                    }
                case "300":
                    {
                        myArrayList = this.GetTop100GoogleWolverhamptonCityMobile(keyword);
                        break;
                    }
                case "301":
                    {
                        myArrayList = this.GetTop100GooglePlymouthCityDesktop(keyword);
                        break;
                    }
                case "302":
                    {
                        myArrayList = this.GetTop100GooglePlymouthCityMobile(keyword);
                        break;
                    }
                case "303":
                    {
                        myArrayList = this.GetTop100GoogleCardiffCityDesktop(keyword);
                        break;
                    }
                case "304":
                    {
                        myArrayList = this.GetTop100GoogleCardiffCityMobile(keyword);
                        break;
                    }
                case "305":
                    {
                        myArrayList = this.GetTop100GoogleOxfordCityDesktop(keyword);
                        break;
                    }
                case "306":
                    {
                        myArrayList = this.GetTop100GoogleOxfordCityMobile(keyword);
                        break;
                    }
                case "307":
                    {
                        myArrayList = this.GetTop100GoogleCambridgeCityDesktop(keyword);
                        break;
                    }
                case "308":
                    {
                        myArrayList = this.GetTop100GoogleCambridgeCityMobile(keyword);
                        break;
                    }
                case "309":
                    {
                        myArrayList = this.GetTop100GoogleBelfastCityDesktop(keyword);
                        break;
                    }
                case "310":
                    {
                        myArrayList = this.GetTop100GoogleBelfastCityMobile(keyword);
                        break;
                    }
                case "311":
                    {
                        myArrayList = this.GetTop100GoogleGlasgowCityDesktop(keyword);
                        break;
                    }
                case "312":
                    {
                        myArrayList = this.GetTop100GoogleGlasgowCityMobile(keyword);
                        break;
                    }
                case "313":
                    {
                        myArrayList = this.GetTop100GoogleEdinburghCityDesktop(keyword);
                        break;
                    }
                case "314":
                    {
                        myArrayList = this.GetTop100GoogleEdinburghCityMobile(keyword);
                        break;
                    }
                case "315":
                    {
                        myArrayList = this.GetTop100GoogleBrightonCityDesktop(keyword);
                        break;
                    }
                case "316":
                    {
                        myArrayList = this.GetTop100GoogleBrightonCityMobile(keyword);
                        break;
                    }
                case "317":
                    {
                        myArrayList = this.GetTop100GoogleHoveCityDesktop(keyword);
                        break;
                    }
                case "318":
                    {
                        myArrayList = this.GetTop100GoogleHoveCityMobile(keyword);
                        break;
                    }
                case "319":
                    {
                        myArrayList = this.GetTop100GoogleSouthamptonCityDesktop(keyword);
                        break;
                    }
                case "320":
                    {
                        myArrayList = this.GetTop100GoogleSouthamptonCityMobile(keyword);
                        break;
                    }
                case "321":
                    {
                        myArrayList = this.GetTop100GooglePhilippinesInternetEnglish(keyword);
                        break;
                    }
                case "322":
                    {
                        myArrayList = this.GetTop100GooglePhilippinesInternetMobileEnglish(keyword);
                        break;
                    }
                case "323":
                    {
                        myArrayList = this.GetTop100GoogleBahamasInternetEnglish(keyword);
                        break;
                    }
                case "324":
                    {
                        myArrayList = this.GetTop100GoogleBahamasInternetMobileEnglish(keyword);
                        break;
                    }
                case "325":
                    {
                        myArrayList = this.GetTop100GoogleJamaicaInternetEnglish(keyword);
                        break;
                    }
                case "326":
                    {
                        myArrayList = this.GetTop100GoogleJamaicaInternetMobileEnglish(keyword);
                        break;
                    }
                case "327":
                    {
                        myArrayList = this.GetTop100GoogleMexicoInternetEnglish(keyword);
                        break;
                    }
                case "328":
                    {
                        myArrayList = this.GetTop100GoogleMexicoInternetMobileEnglish(keyword);
                        break;
                    }
                case "329":
                    {
                        myArrayList = this.GetTop100GooglePuertoRicoInternetEnglish(keyword);
                        break;
                    }
                case "330":
                    {
                        myArrayList = this.GetTop100GooglePuertoRicoInternetMobileEnglish(keyword);
                        break;
                    }
                case "331":
                    {
                        myArrayList = this.GetTop100GooglePuertoRicoInternetSpanish(keyword);
                        break;
                    }
                case "332":
                    {
                        myArrayList = this.GetTop100GooglePuertoRicoInternetMobileSpanish(keyword);
                        break;
                    }
                case "333":
                    {
                        myArrayList = this.GetTop100GoogleCanadaTorontoCityDesktop(keyword);
                        break;
                    }
                case "334":
                    {
                        myArrayList = this.GetTop100GoogleCanadaTorontoCityMobile(keyword);
                        break;
                    }
                case "335":
                    {
                        myArrayList = this.GetTop100GoogleVenezuela(keyword);
                        break;
                    }
                case "336":
                    {
                        myArrayList = this.GetTop100GoogleVenezuelaMobile(keyword);
                        break;
                    }
                case "337":
                    {
                        myArrayList = this.GetTop100GoogleArgentinaInternetMobile(keyword);
                        break;
                    }
                case "338":
                    {
                        myArrayList = this.GetTop100GoogleDallasTexasCityDesktop(keyword);
                        break;
                    }
                case "339":
                    {
                        myArrayList = this.GetTop100GoogleDallasTexasCityMobile(keyword);
                        break;
                    }
                case "350":
                    {
                        myArrayList = this.GetTop100GoogleNGMobile(keyword);
                        break;
                    }
                case "351":
                    {
                        myArrayList = this.GetTop100GoogleKenya(keyword);
                        break;
                    }
                case "352":
                    {
                        myArrayList = this.GetTop100GoogleKenyaMobile(keyword);
                        break;
                    }
                case "353":
                    {
                        myArrayList = this.GetTop100GoogleSydneyCityDesktop(keyword);
                        break;
                    }
                case "354":
                    {
                        myArrayList = this.GetTop100GoogleMelbourneCityDesktop(keyword);
                        break;
                    }
                case "355":
                    {
                        myArrayList = this.GetTop100GoogleBrisbanCityDesktop(keyword);
                        break;
                    }
                case "356":
                    {
                        myArrayList = this.GetTop100GooglePerthCityDesktop(keyword);
                        break;
                    }
                case "357":
                    {
                        myArrayList = this.GetTop100GoogleAdelaideCityDesktop(keyword);
                        break;
                    }
                case "358":
                    {
                        myArrayList = this.GetTop100GoogleSydneyCityMobile(keyword);
                        break;
                    }
                case "359":
                    {
                        myArrayList = this.GetTop100GoogleMelbourneCityMobile(keyword);
                        break;
                    }
                case "360":
                    {
                        myArrayList = this.GetTop100GoogleBrisbanCityMobile(keyword);
                        break;
                    }
                case "361":
                    {
                        myArrayList = this.GetTop100GooglePerthCityMobile(keyword);
                        break;
                    }
                case "362":
                    {
                        myArrayList = this.GetTop100GoogleAdelaideCityMobile(keyword);
                        break;
                    }
                case "363":
                    {
                        myArrayList = this.GetTop100GoogleBangladeshInternet(keyword);
                        break;
                    }
                case "364":
                    {
                        myArrayList = this.GetTop100GoogleBangladeshMobile(keyword);
                        break;
                    }
                case "365":
                    {
                        myArrayList = this.GetTop100GoogleLatviaDesktop(keyword);
                        break;
                    }
                case "366":
                    {
                        myArrayList = this.GetTop100GoogleLatviaMobile(keyword);
                        break;
                    }
                case "367":
                    {
                        myArrayList = this.GetTop100GoogleAspenColoradoDesktop(keyword);
                        break;
                    }
                case "368":
                    {
                        myArrayList = this.GetTop100GoogleAspenColoradoMobile(keyword);
                        break;
                    }
                case "369":
                    {
                        myArrayList = this.GetTop100GoogleNapaCaliforniaDesktop(keyword);
                        break;
                    }
                case "370":
                    {
                        myArrayList = this.GetTop100GoogleNapaCaliforniaMobile(keyword);
                        break;
                    }
                case "371":
                    {
                        myArrayList = this.GetTop100GoogleOmanArabicDesktop(keyword);
                        break;
                    }
                case "372":
                    {
                        myArrayList = this.GetTop100GoogleOmanArabicMobile(keyword);
                        break;
                    }
                case "373":
                    {
                        myArrayList = this.GetTop100GoogleOmanEnglishDesktop(keyword);
                        break;
                    }
                case "374":
                    {
                        myArrayList = this.GetTop100GoogleOmanEnglishMobile(keyword);
                        break;
                    }
                /* case "375":
                     {
                         myArrayList = GetTop100GoogleUSDesktopAdult(keyword);
                         break;
                     }
                 case "376":
                     {
                         myArrayList = this.GetTop100GoogleUSMobileAdult(keyword);
                         break;
                     }
                 case "377":
                     {
                         myArrayList = this.GetTop100GoogleUKInternetDesktopAdult(keyword);
                         break;
                     }
                 case "378":
                     {
                         myArrayList = this.GetTop100GoogleUKMobileWebAdult(keyword);
                         break;
                     }
                 case "379":
                     {
                         myArrayList = this.GetTop100GoogleAusInternetDesktopAdult(keyword);
                         break;
                     }
                 case "380":
                     {
                         myArrayList = this.GetTop100GoogleAusMobileAdult(keyword);
                         break;
                     }*/

                case "381":
                    {
                        myArrayList = this.GetTop100GoogleUKImages_ImageURLs(keyword);//desktop pattern
                        break;
                    }
                case "382":
                    {
                        myArrayList = this.GetTop100GoogleUKMobileImages_PageURLs(keyword);//mobile pattern
                        break;
                    }

                case "383":
                    {
                        myArrayList = this.GetTop100GoogleUKMobileImages_ImageURLs(keyword);//mobile pattern
                        break;
                    }
                case "384":
                    {
                        myArrayList = this.GetTop100GoogleHemelHempsteadCityDesktop(keyword);
                        break;
                    }
                case "385":
                    {
                        myArrayList = this.GetTop100GoogleHemelHempsteadCityMobile(keyword);
                        break;
                    }
                case "386":
                    {
                        myArrayList = this.GetTop100GoogleLeicesterCityDesktop(keyword);
                        break;
                    }
                case "387":
                    {
                        myArrayList = this.GetTop100GoogleLeicesterCityMobile(keyword);
                        break;
                    }
                case "388":
                    {
                        myArrayList = this.GetTop100GoogleNottinghamCityDesktop(keyword);
                        break;
                    }
                case "389":
                    {
                        myArrayList = this.GetTop100GoogleNottinghamCityMobile(keyword);
                        break;
                    }
                case "390":
                    {
                        myArrayList = this.GetTop100GooglePortsmouthCityDesktop(keyword);
                        break;
                    }
                case "391":
                    {
                        myArrayList = this.GetTop100GooglePortsmouthCityMobile(keyword);
                        break;
                    }
                case "392":
                    {
                        myArrayList = this.GetTop100GoogleReadingCityDesktop(keyword);
                        break;
                    }
                case "393":
                    {
                        myArrayList = this.GetTop100GoogleReadingCityMobile(keyword);
                        break;
                    }
                case "394":
                    {
                        myArrayList = this.GetTop100GoogleStokeonTrentCityDesktop(keyword);
                        break;
                    }
                case "395":
                    {
                        myArrayList = this.GetTop100GoogleStokeonTrentCityMobile(keyword);
                        break;
                    }
                case "396":
                    {
                        myArrayList = this.GetTop100GoogleSwanseaCityDesktop(keyword);
                        break;
                    }
                case "397":
                    {
                        myArrayList = this.GetTop100GoogleSwanseaCityMobile(keyword);
                        break;
                    }
                case "398":
                    {
                        myArrayList = this.GetTop100GoogleVNMobile(keyword);
                        break;
                    }
                case "399":
                    {
                        myArrayList = this.GetTop100GoogleMyanmarInternet(keyword);
                        break;
                    }
                case "400":
                    {
                        myArrayList = this.GetTop100GoogleMyanmarInternetMobile(keyword);
                        break;
                    }
                case "401":
                    {
                        myArrayList = this.GetTop100GoogleUKDesktop_ImageURLs(keyword);//desktop pattern
                        break;
                    }

                case "402":
                    {
                        myArrayList = this.GetTop100GoogleUKMobile_ImageURLs(keyword);//mobile pattern
                        break;
                    }
                case "403":
                    {
                        myArrayList = this.GetTop100GoogleRomaniaInternetMobile(keyword);//mobile pattern
                        break;
                    }
                case "404":
                    {
                        myArrayList = this.GetTop100GoogleSwitzerlandEnglish(keyword);
                        break;
                    }
                case "405":
                    {
                        myArrayList = this.GetTop100GoogleSwitzerlandEnglishMobile(keyword);
                        break;
                    }
                case "406":
                    {
                        myArrayList = this.GetTop100GoogleBahrainEnglishMobile(keyword);
                        break;
                    }
                case "407":
                    {
                        myArrayList = this.GetTop100GoogleEgyptEnglishMobile(keyword);
                        break;
                    }
                case "408":
                    {
                        myArrayList = this.GetTop100GoogleJordanEnglishMobile(keyword);
                        break;
                    }
                case "409":
                    {
                        myArrayList = this.GetTop100GoogleKuwaitEnglishMobile(keyword);
                        break;
                    }
                case "410":
                    {
                        myArrayList = this.GetTop100GoogleLebanonEnglishMobile(keyword);
                        break;
                    }
                case "411":
                    {
                        myArrayList = this.GetTop100GoogleQatarEnglishMobile(keyword);
                        break;
                    }
                case "412":
                    {
                        myArrayList = this.GetTop100GoogleSaudiArabiaEnglishMobile(keyword);
                        break;
                    }
                case "413":
                    {
                        myArrayList = this.GetTop100GoogleBahrainEnglish(keyword);
                        break;
                    }
                case "414":
                    {
                        myArrayList = this.GetTop100GoogleEgyptEnglish(keyword);
                        break;
                    }
                case "415":
                    {
                        myArrayList = this.GetTop100GoogleJordanEnglish(keyword);
                        break;
                    }
                case "416":
                    {
                        myArrayList = this.GetTop100GoogleKuwaitEnglish(keyword);
                        break;
                    }
                case "417":
                    {
                        myArrayList = this.GetTop100GoogleLebanonEnglish(keyword);
                        break;
                    }
                case "418":
                    {
                        myArrayList = this.GetTop100GoogleQatarEnglish(keyword);
                        break;
                    }
                case "419":
                    {
                        myArrayList = this.GetTop100GoogleSaudiArabiaEnglish(keyword);
                        break;
                    }
                case "420":
                    {
                        myArrayList = this.GetTop100GoogleBahrainArabicMobile(keyword);
                        break;
                    }
                case "421":
                    {
                        myArrayList = this.GetTop100GoogleEgyptArabicMobile(keyword);
                        break;
                    }
                case "422":
                    {
                        myArrayList = this.GetTop100GoogleJordanArabicMobile(keyword);
                        break;
                    }
                case "423":
                    {
                        myArrayList = this.GetTop100GoogleKuwaitArabicMobile(keyword);
                        break;
                    }
                case "424":
                    {
                        myArrayList = this.GetTop100GoogleLebanonArabicMobile(keyword);
                        break;
                    }
                case "425":
                    {
                        myArrayList = this.GetTop100GoogleQatarArabicMobile(keyword);
                        break;
                    }
                case "426":
                    {
                        myArrayList = this.GetTop100GoogleKoreanMobile(keyword);
                        break;
                    }
                case "427":
                    {
                        myArrayList = GetTop100GoogleKorean(keyword);
                        break;
                    }

                case "428":
                    {
                        myArrayList = GetTop100GoogleCzechRepublicEnglish(keyword);
                        break;
                    }
                case "429":
                    {
                        myArrayList = GetTop100GoogleCzechRepublicEnglishMobile(keyword);
                        break;
                    }
                case "430":
                    {
                        myArrayList = GetTop100GoogleMaltaEnglish(keyword);
                        break;
                    }
                case "431":
                    {
                        myArrayList = GetTop100GoogleMaltaEnglishMobile(keyword);
                        break;
                    }
                case "432":
                    {
                        myArrayList = GetTop100GoogleIceland(keyword);
                        break;
                    }
                case "433":
                    {
                        myArrayList = GetTop100GoogleIcelandMobile(keyword);
                        break;
                    }
                case "434":
                    {
                        myArrayList = GetTop100GoogleIcelandEnglish(keyword);
                        break;
                    }
                case "435":
                    {
                        myArrayList = GetTop100GoogleIcelandEnglishMobile(keyword);
                        break;
                    }
                case "436":
                    {
                        myArrayList = GetTop100GoogleUKPeterboroughEnglish(keyword);
                        break;
                    }
                case "437":
                    {
                        myArrayList = GetTop100GoogleUKPeterboroughEnglishMobile(keyword);
                        break;
                    }
                case "438":
                    {
                        myArrayList = getTop100GoogleTWInternetEnglish(keyword);
                        break;
                    }
                case "439":
                    {
                        myArrayList = getTop100GoogleTWInternetEnglishMobile(keyword);
                        break;
                    }
                case "441":
                    {
                        myArrayList = GetTop100GoogleNewcastleCityEnglandDesktop(keyword);
                        break;
                    }
                case "442":
                    {
                        myArrayList = GetTop100GoogleNewcastleCityEnglandMobile(keyword);
                        break;
                    }
                case "443":
                    {
                        myArrayList = GetTop100GoogleUKLondonE1Desktop(keyword);
                        break;
                    }
                case "444":
                    {
                        myArrayList = GetTop100GoogleUKLondonE1Mobile(keyword);
                        break;
                    }
                case "445":
                    {
                        myArrayList = GetTop100GoogleUKLondonNW1Desktop(keyword);
                        break;
                    }
                case "446":
                    {
                        myArrayList = GetTop100GoogleUKLondonNW1Mobile(keyword);
                        break;
                    }
                case "447":
                    {
                        myArrayList = GetTop100GoogleUKLondonSE1Desktop(keyword);
                        break;
                    }
                case "448":
                    {
                        myArrayList = GetTop100GoogleUKLondonSE1Mobile(keyword);
                        break;
                    }
                case "449":
                    {
                        myArrayList = GetTop100GoogleUKAberdeenDesktop(keyword);
                        break;
                    }
                case "450":
                    {
                        myArrayList = GetTop100GoogleUKAberdeenMobile(keyword);
                        break;
                    }
                case "451":
                    {
                        myArrayList = GetTop100GoogleUKBoltonDesktop(keyword);
                        break;
                    }
                case "452":
                    {
                        myArrayList = GetTop100GoogleUKBoltonMobile(keyword);
                        break;
                    }
                case "453":
                    {
                        myArrayList = GetTop100GoogleUKBournemouthDesktop(keyword);
                        break;
                    }
                case "454":
                    {
                        myArrayList = GetTop100GoogleUKBournemouthMobile(keyword);
                        break;
                    }
                case "455":
                    {
                        myArrayList = GetTop100GoogleUKCoventryDesktop(keyword);
                        break;
                    }
                case "456":
                    {
                        myArrayList = GetTop100GoogleUKCoventryMobile(keyword);
                        break;
                    }
                case "457":
                    {
                        myArrayList = GetTop100GoogleUKCroydonDesktop(keyword);
                        break;
                    }
                case "458":
                    {
                        myArrayList = GetTop100GoogleUKCroydonMobile(keyword);
                        break;
                    }
                case "459":
                    {
                        myArrayList = GetTop100GoogleUKNorthamptonDesktop(keyword);
                        break;
                    }
                case "460":
                    {
                        myArrayList = GetTop100GoogleUKNorthamptonMobile(keyword);
                        break;
                    }
                case "461":
                    {
                        myArrayList = GetTop100GoogleUKNorwichDesktop(keyword);
                        break;
                    }
                case "462":
                    {
                        myArrayList = GetTop100GoogleUKNorwichMobile(keyword);
                        break;
                    }
                case "463":
                    {
                        myArrayList = GetTop100GoogleUKSloughDesktop(keyword);
                        break;
                    }
                case "464":
                    {
                        myArrayList = GetTop100GoogleUKSloughMobile(keyword);
                        break;
                    }
                case "465":
                    {
                        myArrayList = GetTop100GoogleUKWatfordDesktop(keyword);
                        break;
                    }
                case "466":
                    {
                        myArrayList = GetTop100GoogleUKWatfordMobile(keyword);
                        break;
                    }
                case "467":
                    {
                        myArrayList = GetTop100GoogleUKLondonSW1VDesktop(keyword);
                        break;
                    }
                case "468":
                    {
                        myArrayList = GetTop100GoogleUKLondonSW1VMobile(keyword);
                        break;
                    }
                case "469":
                    {
                        myArrayList = GetTop100GoogleCyprusDesktop(keyword);
                        break;
                    }
                case "470":
                    {
                        myArrayList = GetTop100GoogleCyprusMobile(keyword);
                        break;
                    }
                case "471":
                    {
                        myArrayList = GetTop100GoogleEstoniaDesktop(keyword);
                        break;
                    }
                case "472":
                    {
                        myArrayList = GetTop100GoogleEstoniaMobile(keyword);
                        break;
                    }
                case "473":
                    {
                        myArrayList = GetTop100GoogleLithuaniaDesktop(keyword);
                        break;
                    }
                case "474":
                    {
                        myArrayList = GetTop100GoogleLithuaniaMobile(keyword);
                        break;
                    }
                case "475":
                    {
                        myArrayList = GetTop100GoogleUSFloridaDesktop(keyword);
                        break;
                    }
                case "476":
                    {
                        myArrayList = GetTop100GoogleUSFloridaMobile(keyword);
                        break;
                    }
                case "477":
                    {
                        myArrayList = GetTop100GoogleUSPennsylvaniaDesktop(keyword);
                        break;
                    }
                case "478":
                    {
                        myArrayList = GetTop100GoogleUSPennsylvaniaMobile(keyword);
                        break;
                    }
                case "479":
                    {
                        myArrayList = GetTop100GoogleUSWisconsinDesktop(keyword);
                        break;
                    }
                case "480":
                    {
                        myArrayList = GetTop100GoogleUSWisconsinMobile(keyword);
                        break;
                    }
                case "481":
                    {
                        myArrayList = GetTop100GoogleUSArizonaDesktop(keyword);
                        break;
                    }
                case "482":
                    {
                        myArrayList = GetTop100GoogleUSWisconsinMobile(keyword);
                        break;
                    }
                case "483":
                    {
                        myArrayList = GetTop100GoogleUSMichiganDesktop(keyword);
                        break;
                    }
                case "484":
                    {
                        myArrayList = GetTop100GoogleUSMichiganMobile(keyword);
                        break;
                    }
                case "485":
                    {
                        myArrayList = GetTop100GoogleUSOhioDesktop(keyword);
                        break;
                    }
                case "486":
                    {
                        myArrayList = GetTop100GoogleUSOhioMobile(keyword);
                        break;
                    }
                case "487":
                    {
                        myArrayList = GetTop100GoogleUSNorthCarolinaDesktop(keyword);
                        break;
                    }
                case "488":
                    {
                        myArrayList = GetTop100GoogleUSNorthCarolinaMobile(keyword);
                        break;
                    }
                case "489":
                    {
                        myArrayList = GetTop100GoogleUSTallahasseeFloridaDesktop(keyword);
                        break;
                    }
                case "490":
                    {
                        myArrayList = GetTop100GoogleUSTallahasseeFloridaMobile(keyword);
                        break;
                    }
                case "491":
                    {
                        myArrayList = GetTop100GoogleUSHarrisburgPennsylvaniaDesktop(keyword);
                        break;
                    }
                case "492":
                    {
                        myArrayList = GetTop100GoogleUSHarrisburgPennsylvaniaMobile(keyword);
                        break;
                    }
                case "493":
                    {
                        myArrayList = GetTop100GoogleUSMadisonWisconsinDesktop(keyword);
                        break;
                    }
                case "494":
                    {
                        myArrayList = GetTop100GoogleUSMadisonWisconsinMobile(keyword);
                        break;
                    }
                case "495":
                    {
                        myArrayList = GetTop100GoogleUSPhoenixArizonaDesktop(keyword);
                        break;
                    }
                case "496":
                    {
                        myArrayList = GetTop100GoogleUSPhoenixArizonaMobile(keyword);
                        break;
                    }
                case "497":
                    {
                        myArrayList = GetTop100GoogleUSLansingMichiganDesktop(keyword);
                        break;
                    }
                case "498":
                    {
                        myArrayList = GetTop100GoogleUSLansingMichiganMobile(keyword);
                        break;
                    }
                case "499":
                    {
                        myArrayList = GetTop100GoogleUSColumbusOhioDesktop(keyword);
                        break;
                    }
                case "500":
                    {
                        myArrayList = GetTop100GoogleUSColumbusOhioMobile(keyword);
                        break;
                    }
                case "501":
                    {
                        myArrayList = GetTop100GoogleUSRaleighNorthCarolinaDesktop(keyword);
                        break;
                    }
                case "502":
                    {
                        myArrayList = GetTop100GoogleUSRaleighNorthCarolinaMobile(keyword);
                        break;
                    }
                case "504":
                    {
                        myArrayList = GetTop100GoogleMonacoEnglishDesktop(keyword);
                        break;
                    }
                case "505":
                    {
                        myArrayList = GetTop100GoogleMonacoEnglishMobile(keyword);
                        break;
                    }
                case "506":
                    {
                        myArrayList = GetTop100GoogleMonacoFrenchDesktop(keyword);
                        break;
                    }
                case "507":
                    {
                        myArrayList = GetTop100GoogleMonacoFrenchMobile(keyword);
                        break;
                    }
                case "508":
                    {
                        myArrayList = GetTop100GoogleUSMonacoSpanishDesktop(keyword);
                        break;
                    }
                case "509":
                    {
                        myArrayList = GetTop100GoogleUSMonacoSpanishMobile(keyword);
                        break;
                    }
                case "510":
                    {
                        myArrayList = GetTop100GoogleUSChineseDesktop(keyword);
                        break;
                    }
                case "511":
                    {
                        myArrayList = GetTop100GoogleUSChineseMobile(keyword);
                        break;
                    }
            }
            //oIP = sIP;
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

            //  13-12-2020
            if (url.Contains("&grqid="))
                url = url.Remove(url.IndexOf("&grqid="));

            if (url.Contains("\0"))
                url = url.Replace("\0", "%00");

            if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && (!url.Contains("/aclk?") && !url.Contains("search?num=100")))
                return url;

            return string.Empty;

        }
        private Dictionary<string, ArrayList> ImagesPattern(ArrayList htmlsource, string key, string urlType)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            var doc = new HtmlAgilityPack.HtmlDocument();
            string html;
            string keyword;

            foreach (string[] src in htmlsource)//chang
            {
                try
                {
                    keyword = src[0] + ":" + src[2];
                    html = src[1].Replace(@"\", "");
                    doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    ArrayList alDup = new ArrayList();
                    ArrayList googleList = new ArrayList();

                    //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//*[@id=\"rg_s\"]/div/div");
                    HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//div[@class=\"rg_meta notranslate\"]");


                    if (node != null)
                    {
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
                    }

                    else
                    {
                        if (urlType == "ImageLinks")
                        {
                            string pattern = @"\]n,\[""http(.*?)\"",";
                            Regex rx = new Regex(pattern, RegexOptions.IgnoreCase);
                            MatchCollection mc = rx.Matches(html);

                            foreach (Match m in mc)
                            {
                                string url = "http" + m.Groups[1].Value;
                                int indx = url.LastIndexOf("http://");
                                if (indx < 0)
                                {
                                    indx = url.LastIndexOf("https://");
                                }
                                url = url.Remove(0, indx);
                                alDup.Add(HttpUtility.HtmlDecode(url));
                            }
                        }

                        else  // 74
                        {
                            string pattern = "x22 targetx3dx22_blankx22 hrefx3dx22(.*?)x22 ";
                            Regex rx = new Regex(pattern, RegexOptions.IgnoreCase);
                            MatchCollection mc = rx.Matches(html);
                            foreach (Match m in mc)
                            {
                                string url = m.Groups[1].Value;

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
                    if (googleList.Count > 0)
                        dict.Add(keyword, googleList);
                }
                catch (Exception ex)
                {
                    throw new Exception("No pattern match,  " + ex.Message);
                }
            }
            return dict;
        }

        private Dictionary<string, ArrayList> ImagesPatternMobile(ArrayList htmlsource, string key, string urlType)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            var doc = new HtmlAgilityPack.HtmlDocument();
            string html;
            string keyword;

            foreach (string[] src in htmlsource)
            {
                try
                {
                    keyword = src[0] + ":" + src[2];
                    JObject obj = JObject.Parse(src[1]);
                    html = obj["results"][0]["content"].Value<string>();
                    doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    ArrayList alDup = new ArrayList();
                    ArrayList googleList = new ArrayList();

                    HtmlNodeCollection node;
                    if (urlType == "ImageLinks")
                        node = doc.DocumentNode.SelectNodes("//a[@jsname=\"m8x3S\"]/img");
                    else
                        //node = doc.DocumentNode.SelectNodes("//a[@class=\"VFACy\"]");
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
                                alDup.Add(WebUtility.HtmlDecode(url));
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
                    if (googleList.Count > 0)
                        dict.Add(keyword, googleList);
                }
                catch
                {
                    continue;
                }
            }

            return dict;
        }

        private Dictionary<string, ArrayList> MobilePattern(ArrayList htmlsource, string key)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            var doc = new HtmlAgilityPack.HtmlDocument();
            string html;
            string keyword;

            foreach (string[] src in htmlsource)
            {
                try
                {
                    keyword = src[0] + ":" + src[2];
                    JObject obj = JObject.Parse(src[1]);
                    html = obj["results"][0]["content"].Value<string>();
                    doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    //System.IO.File.WriteAllText(@"c:\inetput\wwwroot\donaldtrump.html", html);
                    //File.WriteAllText(@"C:\inetpub\wwwroot\html\" + jobid + "_" + src[0] + ".html", html, Encoding.UTF8);


                    ArrayList alDup = new ArrayList();
                    ArrayList googleList = new ArrayList();
                    //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//a[@class='C8nzq']|//a[@class='Rk4fgb']|//div[@id='rso']/div/div/div/a[1]|//a[@class='JTuIPc']|//a[@class='C8nzq BmP5tf']|//a[@class='BmP5tf']|//a[@class='sXtWJb']|//a[@class='C8nzq BmP5tf amp_r']|//div[@jsl='$t t-4cfX2GiP_Fk;$x 0;']/a|//g-link[not(contains(@class,'fl'))]/a");
                    HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//div[@class='ZINbbc xpd']/div/a|.//div[@class='ZINbbc xpd']/div[1]/a|.//a[@class='C8nzq JTuIPc amp_r']|.//a[@class='C8nzq JTuIPc']|.//a[@class='C8nzq BmP5tf amp_r']|.//a[@class='C8nzq Tj0U2 BmP5tf']|.//a[@class='C8nzq BmP5tf']|.//a[@class='C8nzq Tj0U2 BmP5tf amp_r']|.//a[@class='sXtWJb amp_r']|.//g-link/a|.//div[@class='fM8c FUksre']/a|.//div[@class='ytwLQd']|.//div[@class='rc']|.//h3[@class='r']/a|.//h3[@class='r']/div/a|.//h3[contains(@class,'yuRUbf JtG40d')]/a|.//a[class='cz3goc BmP5tf']");
                    //HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//div[@class='ZINbbc xpd']/div/a|.//div[@class='ZINbbc xpd']/div[1]/a|.//a[@class='C8nzq JTuIPc amp_r']|.//a[@class='C8nzq JTuIPc']|.//a[@class='C8nzq BmP5tf amp_r']|.//a[@class='C8nzq Tj0U2 BmP5tf']|.//a[@class='C8nzq BmP5tf']|.//a[@class='C8nzq Tj0U2 BmP5tf amp_r']|.//a[@class='sXtWJb amp_r']|.//g-link/a|.//div[@class='fM8c FUksre']/a|.//div/a|.//div[@class='rc']|.//div[@class='ytwLQd']|.//h3[@class='r']/a|.//h3[@class='r']/div/a|.//h3[contains(@class,'yuRUbf JtG40d')]/a");

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
                                //if (!urls.Contains("maps?uule=w+"))
                                if (!urls.Contains("https://www.google.com/maps"))
                                alDup.Add(HttpUtility.HtmlDecode(urls.Replace("\x00", "%00").Replace("|", "%7C").Replace("^", "%5E")));
                            }*/
                            string url = links.Attributes["href"].Value;
                            url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");
                            //if (string.IsNullOrEmpty(url.Trim())) return string.Empty;
                            //29-09-2020            
                            if (url.IndexOf("https://") == 0 || url.IndexOf("https://") >= 0) //01-10-2020
                                url = url.Remove(0, url.IndexOf("https://"));
                            else if (url.IndexOf("http://") == 0 || url.IndexOf("http://") >= 0) //14-10-2020 included indexof for http)
                                url = url.Remove(0, url.IndexOf("http://"));
                            //end 29-09-2020

                            Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
                            if (!rx.Match(url).Success && !url.Contains("/aclk?"))
                                if (!url.Contains("://")) // 30-04-2020
                                    url = "http://" + url;

                            if (url.StartsWith("http:////") || url.StartsWith("https:////")) //18-09-2020 condition applied if appears http:////
                                url = url.Replace("////", "//").Replace("///","//"); //18-09-2020

                            if (url.Contains("&amp;grqid="))
                                url = url.Remove(url.IndexOf("&amp;grqid="));
                            //13-12-2019
                            if (url.Contains("&grqid="))
                                url = url.Remove(url.IndexOf("&grqid="));
                            //23-09-2020
                            if (url.Contains("&amp;gclid="))
                                url = url.Remove(url.IndexOf("&amp;gclid="));
                            if (url.Contains("&gclid="))
                                url = url.Remove(url.IndexOf("&gclid="));
                            //end 23-09-2020

                            if (url.Contains("\0"))
                                url = url.Replace("\0", "%00");

                            if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && (!url.Contains("/aclk?") && !url.Contains("///search?") && !url.Contains("search?num=100") && !url.Contains("?sa=X") && !url.Contains("http://#") && !url.Contains("www.google.") && !url.Contains("maps.google.") && !url.Contains("https://www.google.com/maps")))

                                if (url.StartsWith("http") || url.StartsWith("https") || !url.Contains("https://www.google.com/maps"))
                                {
                                    url = GetRedirectedUrl(WebUtility.HtmlDecode(url).Trim());
                                    if (url.ToLower().Contains("%2f") || url.ToLower().Contains("%2e"))
                                        url = GetRedirectedUrl(WebUtility.UrlDecode(WebUtility.HtmlDecode(url)).Trim());
                                    alDup.Add(WebUtility.HtmlDecode(url.Replace("\x00", "%00").Replace("|", "%7C").Replace("^", "%5E").Replace("\\\\u003d", "=").Replace('\u0002', ' ').Replace('\u0018', ' ').Replace('\f', ' ').Trim()));
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
                    if (googleList.Count > 0)
                        dict.Add(keyword, googleList);
                }
                catch
                {
                    continue;
                }
            }

            return dict;
        }   //MobilePattern



        private Dictionary<string, ArrayList> DesktopPattern(ArrayList htmlsource, string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            var doc = new HtmlAgilityPack.HtmlDocument();
            string html = "";
            foreach (string[] src in htmlsource)
            {
                try
                {
                    keyword = src[0] + ":" + src[2];
                    JObject obj = JObject.Parse(src[1]);
                    html = obj["results"][0]["content"].Value<string>();
                    //html = src[1].Replace(@"\", "");
                    doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    //File.WriteAllText(@"C:\inetpub\wwwroot\html\" + jobid + "_" + src[0] + ".html", html, Encoding.UTF8);

                    ArrayList alDup = new ArrayList();
                    ArrayList googleList = new ArrayList();
                    //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//div[@class='r']/a[1]|//div[1]/div/h3/div/g-link/a|//div[1]/div/h3/g-link/a");
                    //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//div[(contains(@class, 'srg'))]//div[@class='r']/a[1]|//div[1]/div/h3/div/g-link/a|//div[1]/div/h3/g-link/a");
                    //HtmlNodeCollection node = doc.DocumentNode.SelectNodes("//div[@class='srg']//div[@class='r']/a[1]|//div[@class='bkWMgd']/div[@class='g']//div[@class='r']/a[1]|//div[1]/div/h3/div/g-link/a|//div[1]/div/h3/g-link/a");
                    //[not(contains(@class, 'fl'))]    
                    //HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//div[@class='r']/a|.//div[@class='rc']/div/a");
                    HtmlNodeCollection node = doc.DocumentNode.SelectNodes(".//h3[@class='r']/a|.//div[@class='r']/a|.//div[@class='r']/div/a|.//div[@class='yuRUbf']/div/span/a|.//g-link/a|.//h3[@class='r dO0Ag']/a|.//div[@class='yuRUbf']/a|.//div[@class='zTpPx']/g-link/a");


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
                                //if (!urls.Contains("//maps.google."))
                                alDup.Add(HttpUtility.HtmlDecode(urls.Replace("\x00", "%00").Replace("|", "%7C").Replace("^", "%5E")));
                            }*/

                            string url = links.Attributes["href"].Value;
                            url = url.Replace("HTTPS://", "https://").Replace("HTTP://", "http://");
                            //if (string.IsNullOrEmpty(url.Trim())) return string.Empty;
                            //29-09-2020            
                            if (url.IndexOf("https://") == 0 || url.IndexOf("https://") >= 0) //01-10-2020
                                url = url.Remove(0, url.IndexOf("https://"));
                            else if (url.IndexOf("http://") == 0 || url.IndexOf("http://") >= 0) //14-10-2020 included indexof for http)
                                url = url.Remove(0, url.IndexOf("http://"));
                            //end 29-09-2020

                            Regex rx = new Regex("http[\\w]?://(.*)", RegexOptions.Singleline);
                            if (!rx.Match(url).Success && !url.Contains("/aclk?"))
                                if (!url.Contains("://")) // 30-04-2020
                                    url = "http://" + url;

                            if (url.StartsWith("http:////") || url.StartsWith("https:////")) //18-09-2020 condition applied if appears http:////
                                url = url.Replace("////", "//").Replace("///", "//"); //18-09-2020

                            if (url.Contains("&amp;grqid="))
                                url = url.Remove(url.IndexOf("&amp;grqid="));
                            //13-12-2019
                            if (url.Contains("&grqid="))
                                url = url.Remove(url.IndexOf("&grqid="));
                            //23-09-2020
                            if (url.Contains("&amp;gclid="))
                                url = url.Remove(url.IndexOf("&amp;gclid="));
                            if (url.Contains("&gclid="))
                                url = url.Remove(url.IndexOf("&gclid="));
                            //end 23-09-2020

                            if (url.Contains("\0"))
                                url = url.Replace("\0", "%00");

                            if ((url.StartsWith("https://") || url.StartsWith("http://") || url.StartsWith("ftp://")) && (!url.Contains("/aclk?") && !url.Contains("///search?") && !url.Contains("search?num=100") && !url.Contains("?sa=X")))

                                if (url.StartsWith("http") || url.StartsWith("https"))
                                {
                                    url = GetRedirectedUrl(WebUtility.HtmlDecode(url).Trim());
                                    if (url.ToLower().Contains("%2f") || url.ToLower().Contains("%2e"))
                                        url = GetRedirectedUrl(WebUtility.UrlDecode(WebUtility.HtmlDecode(url)).Trim());
                                    alDup.Add(WebUtility.HtmlDecode(url.Replace("\x00", "%00").Replace("|", "%7C").Replace("^", "%5E").Replace("\\\\u003d", "=").Replace('\u0002', ' ').Replace('\u0018', ' ').Replace('\f', ' ').Trim()));
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
                    if (googleList.Count > 0)
                        dict.Add(keyword, googleList);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            return dict;
        }  //DesktopPattern

        public Dictionary<string, ArrayList> GetTop100GoogleNZ(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.nz", "New Zealand", "en-nz", "w+CAIQICILTmV3IFplYWxhbmQ=");
                dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleZA(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.za", "South Africa", "en-za", "w+CAIQICIPU291dGggQWZyaWNhCgoK");
                dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleRussia(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ru", "Russia", "ru-ru", "w+CAIQICIGUnVzc2lh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleIT(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "it", "Italy", "it-it", "w+CAIQICIFSXRhbHk=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSE(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "se", "Sweden", "sv-se", "w+CAIQICIGc3dlZGVu");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCN(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "zh-cn-hk", "w+CAIQICIJSG9uZyBLb25n");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleFR(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "fr", "France", "fr-fr", "w+CAIQICIGRnJhbmNl");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHK(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "en-hk", "w+CAIQICIJSG9uZyBLb25n");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleES(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "es", "Spain", "es-es", "w+CAIQICIFc3BhaW4=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleFRWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "fr", "France", "fr-fr", "w+CAIQICIGRnJhbmNl");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSG(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.sg", "Singapore", "en-sg", "w+CAIQICIJU2luZ2Fwb3Jl");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBEInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "be", "Belgium", "nl-be", "w+CAIQICIHQmVsZ2l1bQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBENederlands(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "be", "Belgium", "nl-be", "w+CAIQICIHQmVsZ2l1bQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBEBelgie(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "be", "Belgium", "nl-be", "w+CAIQICIHQmVsZ2l1bQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleJapan(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.jp", "Japan", "ja-jp", "w+CAIQICIFSmFwYW4=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBrasil(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.br", "Brazil", "pt-br", "w+CAIQICIGQnJhemls");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNederlands(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "nl", "Netherlands", "nl-nl", "w+CAIQICILTmV0aGVybGFuZHM=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDE(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "de", "Germany", "de-de", "w+CAIQICIHR2VybWFueQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUS(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "United States", "en-us", "w+CAIQICINVW5pdGVkIFN0YXRlcw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleCH(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "de-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLU(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "lu", "Luxembourg", "de-lu", "w+CAIQICILTHV4ZW1ib3VyZw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleAT(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "at", "Austria", "de-at", "w+CAIQICIHQXVzdHJpYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNederlandsInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "nl", "Netherlands", "nl-nl", "w+CAIQICILTmV0aGVybGFuZHM=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKInternet(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKMobileWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCanada(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ca", "Canada", "en-ca", "w+CAIQICIGQ2FuYWRh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMYWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.my", "Malaysia", "en-my", "w+CAIQICIITWFsYXlzaWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleTHWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.th", "Thailand", "th-th", "w+CAIQICIIVGhhaWxhbmQ=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNGWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.ng", "Nigeria", "en-ng", "w+CAIQICIHTmlnZXJpYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCHItalino(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "it-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCHFrancais(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "fr-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCHDeutsch(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "de-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePTMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "pt", "Portugal", "pt-pt", "w+CAIQICIIUG9ydHVnYWw=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNederlandsMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "nl", "Netherlands", "nl-nl", "w+CAIQICILTmV0aGVybGFuZHM=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleMexico(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.mx", "Mexico", "es-419-mx", "w+CAIQICIGTWV4aWNv");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleESMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "es", "Spain", "es-es", "w+CAIQICIFc3BhaW4=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }
        public Dictionary<string, ArrayList> GetTop100GoogleAusInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.au", "Australia", "en-au", "w+CAIQICIJQXVzdHJhbGlh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePolskiInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "pl", "Poland", "pl-pl", "w+CAIQICIGUG9sYW5k");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleRomaniaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ro", "Romania", "ro-ro", "w+CAIQICIHUm9tYW5pYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBulgariaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "bg", "Bulgaria", "bg-bg", "w+CAIQICIIQnVsZ2FyaWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSlovenijaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "si", "Slovenia", "sl-si", "w+CAIQICIIU2xvdmVuaWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMagyaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "hu", "Hungary", "hu-hu", "w+CAIQICIHSHVuZ2FyeQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCzechInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "cz", "Czech Republic", "cs-cz", "w+CAIQICIOQ3plY2ggUmVwdWJsaWM=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSlovakiaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "sk", "Slovakia", "sk-sk", "w+CAIQICIIU2xvdmFraWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleArgentinaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.ar", "Argentina", "es-419-ar", "w+CAIQICIJQXJnZW50aW5h");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLebanonInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.lb", "Lebanon", "ar-lb", "w+CAIQICIHTGViYW5vbg==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleJordanInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "jo", "Jordan", "ar-jo", "w+CAIQICIGSm9yZGFu");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleFinlandInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "fi", "Finland", "fi-fi", "w+CAIQICIHRmlubGFuZA==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleGreeceInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "gr", "Greece", "el-gr", "w+CAIQICIGR3JlZWNl");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNorwayInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "no", "Norway", "no-no", "w+CAIQICIGTm9yd2F5");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }
        public Dictionary<string, ArrayList> GetTop100GoogleBelgiumInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "be", "Belgium", "fr-be", "w+CAIQICIHQmVsZ2l1bQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDenmarkInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "dk", "Denmark", "da-dk", "w+CAIQICIHRGVubWFyaw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleChileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "cl", "Chile", "es-419-cl", "w+CAIQICIFQ2hpbGU=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleAusMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.au", "Australia", "en-au", "w+CAIQICIJQXVzdHJhbGlh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCHItalinoMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ch", "Switzerland", "it-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCHFrancaisMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ch", "Switzerland", "fr-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCHDeutschMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ch", "Switzerland", "de-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePTInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "pt", "Portugal", "pt-pt", "w+CAIQICIIUG9ydHVnYWw=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDEMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "de", "Germany", "de-de", "w+CAIQICIHR2VybWFueQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleAEMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ae", "United Arab Emirates", "ar-ae", "w+CAIQICIUVW5pdGVkIEFyYWIgRW1pcmF0ZXM=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleATMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "at", "Austria", "de-at", "w+CAIQICIHQXVzdHJpYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBEDutchMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "be", "Belgium", "nl-be", "w+CAIQICIHQmVsZ2l1bQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBEFrenchMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "be", "Belgium", "fr-be", "w+CAIQICIHQmVsZ2l1bQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBrasilMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.br", "Brazil", "pt-br", "w+CAIQICIGQnJhemls");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCanadaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ca", "Canada", "en-ca", "w+CAIQICIGQ2FuYWRh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDenmarkMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "dk", "Denmark", "da-dk", "w+CAIQICIHRGVubWFyaw==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleFinlandMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "fi", "Finland", "fi-fi", "w+CAIQICIHRmlubGFuZA==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleFRMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "fr", "France", "fr-fr", "w+CAIQICIGRnJhbmNl");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleGreeceMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "gr", "Greece", "el-gr", "w+CAIQICIGR3JlZWNl");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHKMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.hk", "Hong Kong", "zh-TW-HK", "w+CAIQICIJSG9uZyBLb25n");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleIEMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ie", "Ireland", "en-ie", "w+CAIQICIHSXJlbGFuZA==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleJapanMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.jp", "Japan", "ja-jp", "w+CAIQICIFSmFwYW4=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNorwayMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "no", "Norway", "no-no", "w+CAIQICIGTm9yd2F5");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleSEMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "se", "Sweden", "sv-se", "w+CAIQICIGU3dlZGVu");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHungaryMobileWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "hu", "Hungary", "hu-hu", "w+CAIQICIHSHVuZ2FyeQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePolandMobileWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "pl", "Poland", "pl-pl", "w+CAIQICIGUG9sYW5k");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleTurkeyMobileWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.tr", "Turkey", "tr-tr", "w+CAIQICIGVHVya2V5");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleRussiaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ru", "Russia", "ru-ru", "w+CAIQICIGUnVzc2lh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleTWInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.tw", "Taiwan", "zh-tw", "w+CAIQICIGVGFpd2Fu");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleILInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.il", "Israel", "en-il", "w+CAIQICIGSXNyYWVs");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSerbiaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "rs", "Serbia", "sr-rs", "w+CAIQICIGU2VyYmlh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSNewYorkCity(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "New York,United States", "en-us", "w+CAIQICIWTmV3IFlvcmssVW5pdGVkIFN0YXRlcw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSNewYorkCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "New York,United States", "en-us", "w+CAIQICIWTmV3IFlvcmssVW5pdGVkIFN0YXRlcw==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSLA(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Los Angeles,United States", "en-us", "w+CAIQICIkTG9zIEFuZ2VsZXMsQ2FsaWZvcm5pYSxVbml0ZWQgU3RhdGVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSLAMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Los Angeles,United States", "en-us", "w+CAIQICIkTG9zIEFuZ2VsZXMsQ2FsaWZvcm5pYSxVbml0ZWQgU3RhdGVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleZAMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.za", "South Africa", "en-za", "w+CAIQICIPU291dGggQWZyaWNhCgoK");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSaudiMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.sa", "Saudi Arabia", "ar-sa", "w+CAIQICIMU2F1ZGkgQXJhYmlh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCanadaFrenchWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ca", "Canada", "fr-ca", "w+CAIQICIGQ2FuYWRh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSChicago(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Chicago,United States", "en-us", "w+CAIQICIVQ2hpY2FnbyxVbml0ZWQgU3RhdGVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSChicagoMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Chicago,United States", "en-us", "w+CAIQICIeQ2hpY2FnbyxJbGxpbm9pcyxVbml0ZWQgU3RhdGVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSSanFran(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "San Francisco,United States", "en-us", "w+CAIQICImU2FuIEZyYW5jaXNjbyxDYWxpZm9ybmlhLFVuaXRlZCBTdGF0ZXM");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSSanFranMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "San Francisco,United States", "en-us", "w+CAIQICImU2FuIEZyYW5jaXNjbyxDYWxpZm9ybmlhLFVuaXRlZCBTdGF0ZXM");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePakistanInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.pk", "Pakistan", "en-pk", "w+CAIQICIIUGFraXN0YW4=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePakistanMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.pk", "Pakistan", "en-pk", "w+CAIQICIIUGFraXN0YW4=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleINMobileWeb(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.in", "India", "en-in", "w+CAIQICIFSW5kaWE=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCanadaFrenchMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ca", "Canada", "fr-ca", "w+CAIQICIGQ2FuYWRh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMYMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.my", "Malaysia", "en-my", "w+CAIQICIITWFsYXlzaWE=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSGMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.sg", "Singapore", "zh-sg", "w+CAIQICIJU2luZ2Fwb3J");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCanada1(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ca", "Canada", "en-ca", "w+CAIQICIGQ2FuYWRh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCanadaMobile1(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ca", "Canada", "en-ca", "w+CAIQICIGQ2FuYWRh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSrilankaMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "lk", "Sri Lanka", "en-lk", "w+CAIQICIJU3JpIExhbmth");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSrilankaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "lk", "Sri Lanka", "en-lk", "w+CAIQICIJU3JpIExhbmth");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHRInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "hr", "Croatia", "hr-hr", "w+CAIQICIHQ3JvYXRpYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHRMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "hr", "Croatia", "hr-hr", "w+CAIQICIHQ3JvYXRpYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCZMobileInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "cz", "Czech Republic", "cs-cz", "w+CAIQICIHQ3plY2hpYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleTHMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.th", "Thailand", "th-th", "w+CAIQICIIVGhhaWxhbmQ=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSHoustonCity(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Houston,United States", "en-us", "w+CAIQICIbSG91c3RvbixUZXhhcyxVbml0ZWQgU3RhdGVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSHoustonCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Houston,United States", "en-us", "w+CAIQICIbSG91c3RvbixUZXhhcyxVbml0ZWQgU3RhdGVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSMiamiCity(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Miami,United States", "en-us", "w+CAIQICIbTWlhbWksRmxvcmlkYSxVbml0ZWQgU3RhdGVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSMiamiCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Miami,United States", "en-us", "w+CAIQICIbTWlhbWksRmxvcmlkYSxVbml0ZWQgU3RhdGVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleAEEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ae", "United Arab Emirates", "en-ae", "w+CAIQICIUVW5pdGVkIEFyYWIgRW1pcmF0ZXM=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHKInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "en-hk", "w+CAIQICIJSG9uZyBLb25n");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMexicoMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.mx", "Mexico", "es-419-mx", "w+CAIQICIGTWV4aWNv");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMexicoCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.mx", "Mexico City,Mexico", "es-419-mx", "w+CAIQICIeTWV4aWNvIENpdHksTWV4aWNvIENpdHksTWV4aWNv");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleChileInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "cl", "Chile", "es-419-cl", "w+CAIQICIFQ2hpbGU=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePeruInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.pe", "Peru", "es-419-pe", "w+CAIQICIEUGVydQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePeruInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.pe", "Peru", "es-419-pe", "w+CAIQICIEUGVydQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleColumbiaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.co", "Columbia", "es-419-co", "w+CAIQICIIQ29sb21iaWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleColumbiaInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.co", "Columbia", "es-419-co", "w+CAIQICIIQ29sb21iaWE=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMexicoCity(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.mx", "Mexico City,Mexico", "es-419-mx", "w+CAIQICIeTWV4aWNvIENpdHksTWV4aWNvIENpdHksTWV4aWNv");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleIndonesiaInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.id", "Indonesia", "id-id", "w+CAIQICIJSW5kb25lc2lh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleIndonesiaInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.id", "Indonesia", "id-id", "w+CAIQICIJSW5kb25lc2lh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePhilippinesInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.ph", "Philippines", "fil-ph", "w+CAIQICILUGhpbGlwcGluZXM=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePhilippinesInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.ph", "Philippines", "fil-ph", "w+CAIQICILUGhpbGlwcGluZXM=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSGMobileInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.sg", "Singapore", "en-sg", "w+CAIQICIJU2luZ2Fwb3J");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleTHMobileInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.th", "Thailand", "en-th", "w+CAIQICIIVGhhaWxhbmQ=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHKMobileInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.hk", "Hong Kong", "en-hk", "w+CAIQICIJSG9uZyBLb25n");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMYInternetLanguage(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.my", "Malaysia", "ms-my", "w+CAIQICIITWFsYXlzaWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMYMobileInternetLanguage(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.my", "Malaysia", "ms-my", "w+CAIQICIITWFsYXlzaWE=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleTHInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.th", "Thailand", "en-th", "w+CAIQICIIVGhhaWxhbmQ=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSGInternetLanguage(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.sg", "Singapore", "zh-sg", "w+CAIQICIJU2luZ2Fwb3Jl");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHKInternetChinaLanguage(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "zh-cn-hk", "w+CAIQICIJSG9uZyBLb25n");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHKMobileInternetChinaLanguage(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.hk", "Hong Kong", "zh-cn-hk", "w+CAIQICIJSG9uZyBLb25n");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNZInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.nz", "New Zealand", "en-nz", "w+CAIQICILTmV3IFplYWxhbmQ=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleTWInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.tw", "Taiwan", "zh-tw", "w+CAIQICIGVGFpd2Fu");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleAEEnglishInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ae", "United Arab Emirates", "en-ae", "w+CAIQICIUVW5pdGVkIEFyYWIgRW1pcmF0ZXM=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleJapanInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.jp", "Japan", "en-jp", "w+CAIQICIFSmFwYW4=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleJapanMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.jp", "Japan", "en-jp", "w+CAIQICIFSmFwYW4=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDenmarkInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "dk", "Denmark", "en-dk", "w+CAIQICIHRGVubWFyaw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDenmarkMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "dk", "Denmark", "en-dk", "w+CAIQICIHRGVubWFyaw==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSEInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "se", "Sweden", "en-se", "w+CAIQICIGU3dlZGV");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSEMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "se", "Sweden", "en-se", "w+CAIQICIGU3dlZGVu");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNorwayInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "no", "Norway", "en-no", "w+CAIQICIGTm9yd2F5");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNorwayMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "no", "Norway", "en-no", "w+CAIQICIGTm9yd2F5");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleFinlandInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "fi", "Finland", "en-fi", "w+CAIQICIHRmlubGFuZA==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleFinlandMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "fi", "Finland", "en-fi", "w+CAIQICIHRmlubGFuZA==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDEInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "de", "Germany", "en-de", "w+CAIQICIHR2VybWFueQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDEMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "de", "Germany", "en-de", "w+CAIQICIHR2VybWFueQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleFRInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "fr", "France", "en-fr", "w+CAIQICIGRnJhbmNl");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }
        public Dictionary<string, ArrayList> GetTop100GoogleFRMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "fr", "France", "en-fr", "w+CAIQICIGRnJhbmNl");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleITInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "it", "Italy", "en-it", "w+CAIQICIFSXRhbHk=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleITMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "it", "Italy", "en-it", "w+CAIQICIFSXRhbHk=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLondonCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "London,England,United Kingdom", "en-gb", "w+CAIQICIdTG9uZG9uLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLondonCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "London,England,United Kingdom", "en-gb", "w+CAIQICIdTG9uZG9uLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBirminghamCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Birmingham,England,United Kingdom", "en-gb", "w+CAIQICIhQmlybWluZ2hhbSxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBirminghamCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Birmingham,England,United Kingdom", "en-gb", "w+CAIQICIhQmlybWluZ2hhbSxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLeedsCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Leeds,England,United Kingdom", "en-gb", "w+CAIQICIcTGVlZHMsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLeedsCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Leeds,England,United Kingdom", "en-gb", "w+CAIQICIcTGVlZHMsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSheffieldCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Sheffield,England,United Kingdom", "en-gb", "w+CAIQICIgU2hlZmZpZWxkLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSheffieldCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Sheffield,England,United Kingdom", "en-gb", "w+CAIQICIgU2hlZmZpZWxkLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBradfordCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Bradford,England,United Kingdom", "en-gb", "w+CAIQICIfQnJhZGZvcmQsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBradfordCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Bradford,England,United Kingdom", "en-gb", "w+CAIQICIfQnJhZGZvcmQsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleManchesterCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Manchester,England,United Kingdom", "en-gb", "w+CAIQICIhTWFuY2hlc3RlcixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleManchesterCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Manchester,England,United Kingdom", "en-gb", "w+CAIQICIhTWFuY2hlc3RlcixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLiverpoolCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Liverpool,England,United Kingdom", "en-gb", "w+CAIQICIgTGl2ZXJwb29sLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLiverpoolCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Liverpool,England,United Kingdom", "en-gb", "w+CAIQICIgTGl2ZXJwb29sLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBristolCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Bristol,England,United Kingdom", "en-gb", "w+CAIQICIeQnJpc3RvbCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBristolCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Bristol,England,United Kingdom", "en-gb", "w+CAIQICIeQnJpc3RvbCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNewcastleCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Newcastle,Northern Ireland,United Kingdom", "en-gb", "w+CAIQICIpTmV3Y2FzdGxlLE5vcnRoZXJuIElyZWxhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleNewcastleCityEnglandDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Newcastle upon Tyne,England,United Kingdom", "en-gb", "w+CAIQICIqTmV3Y2FzdGxlIHVwb24gVHluZSxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKLondonE1Desktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "E1,England,United Kingdom", "en-gb", "w+CAIQICIZRTEsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKLondonNW1Desktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "NW1,England,United Kingdom", "en-gb", "w+CAIQICIaTlcxLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKLondonSE1Desktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "SE1,England,United Kingdom", "en-gb", "w+CAIQICIaU0UxLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKAberdeenDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Aberdeen,Scotland,United Kingdom", "en-gb", "w+CAIQICIgQWJlcmRlZW4sU2NvdGxhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKBoltonDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Bolton,England,United Kingdom", "en-gb", "w+CAIQICIdQm9sdG9uLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKBournemouthDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Bournemouth,England,United Kingdom", "en-gb", "w+CAIQICIiQm91cm5lbW91dGgsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKCoventryDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Coventry,England,United Kingdom", "en-gb", "w+CAIQICIfQ292ZW50cnksRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKCroydonDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Croydon,England,United Kingdom", "en-gb", "w+CAIQICIeQ3JveWRvbixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKNorthamptonDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Northampton,England,United Kingdom", "en-gb", "w+CAIQICIiTm9ydGhhbXB0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKNorwichDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Norwich,England,United Kingdom", "en-gb", "w+CAIQICIeTm9yd2ljaCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKSloughDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Slough,England,United Kingdom", "en-gb", "w+CAIQICIdU2xvdWdoLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKWatfordDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Watford,England,United Kingdom", "en-gb", "w+CAIQICIeV2F0Zm9yZCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKLondonSW1VDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "SW1V,England,United Kingdom", "en-gb", "w+CAIQICIbU1cxVixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleCyprusDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.cy", "Cyprus", "EL-CY", "w+CAIQICIGQ3lwcnVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleEstoniaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ee", "Estonia", "ET-EE", "w+CAIQICIHRXN0b25pYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleLithuaniaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "lt", "Lithuania", "LT-LT", "w+CAIQICIJTGl0aHVhbmlh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSFloridaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Florida,United States", "en-us", "w+CAIQICIVRmxvcmlkYSxVbml0ZWQgU3RhdGVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleUSPennsylvaniaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Pennsylvania,United States", "en-us", "w+CAIQICIaUGVubnN5bHZhbmlhLFVuaXRlZCBTdGF0ZXM=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSWisconsinDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Wisconsin,United States", "en-us", "w+CAIQICIXV2lzY29uc2luLFVuaXRlZCBTdGF0ZXM=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSArizonaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Arizona,United States", "en-us", "w+CAIQICIVQXJpem9uYSxVbml0ZWQgU3RhdGVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSMichiganDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Michigan,United States", "en-us", "w+CAIQICIWTWljaGlnYW4sVW5pdGVkIFN0YXRlcw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSOhioDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Ohio,United States", "en-us", "w+CAIQICIST2hpbyxVbml0ZWQgU3RhdGVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSNorthCarolinaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "North Carolina,United States", "en-us", "w+CAIQICIcTm9ydGggQ2Fyb2xpbmEsVW5pdGVkIFN0YXRlcw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSTallahasseeFloridaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Tallahassee,Florida,United States", "en-us", "w+CAIQICIhVGFsbGFoYXNzZWUsRmxvcmlkYSxVbml0ZWQgU3RhdGVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSHarrisburgPennsylvaniaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Harrisburg,Pennsylvania,United States", "en-us", "w+CAIQICIlSGFycmlzYnVyZyxQZW5uc3lsdmFuaWEsVW5pdGVkIFN0YXRlcw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSMadisonWisconsinDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Madison,Wisconsin,United States", "en-us", "w+CAIQICIfTWFkaXNvbixXaXNjb25zaW4sVW5pdGVkIFN0YXRlcw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSPhoenixArizonaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Phoenix,Arizona,United States", "en-us", "w+CAIQICIdUGhvZW5peCxBcml6b25hLFVuaXRlZCBTdGF0ZXM=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSLansingMichiganDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Lansing,Michigan,United States", "en-us", "w+CAIQICIeTGFuc2luZyxNaWNoaWdhbixVbml0ZWQgU3RhdGVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSColumbusOhioDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Columbus,Ohio,United States", "en-us", "w+CAIQICIbQ29sdW1idXMsT2hpbyxVbml0ZWQgU3RhdGVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSRaleighNorthCarolinaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Raleigh,North Carolina,United States", "en-us", "w+CAIQICIkUmFsZWlnaCxOb3J0aCBDYXJvbGluYSxVbml0ZWQgU3RhdGVz");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleMonacoEnglishDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "mn", "Monaco", "en-mc", "w+CAIQICIGTW9uYWNv");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        
        public Dictionary<string, ArrayList> GetTop100GoogleMonacoFrenchDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "mn", "Monaco", "fr-mc", "w+CAIQICIGTW9uYWNv");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        
        public Dictionary<string, ArrayList> GetTop100GoogleUSMonacoSpanishDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "United States", "es-419-us", "w+CAIQICIWVG9yb250byxPbnRhcmlvLENhbmFkYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        
        public Dictionary<string, ArrayList> GetTop100GoogleUSChineseDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "United States", "zh-cn", "w+CAIQICIWVG9yb250byxPbnRhcmlvLENhbmFkYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        ///-------------------------------------------------------------------////


        public Dictionary<string, ArrayList> GetTop100GoogleUKAberdeenMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Aberdeen,Scotland,United Kingdom", "en-gb", "w+CAIQICIgQWJlcmRlZW4sU2NvdGxhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKBoltonMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Bolton,England,United Kingdom", "en-gb", "w+CAIQICIdQm9sdG9uLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKBournemouthMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Bournemouth,England,United Kingdom", "en-gb", "w+CAIQICIiQm91cm5lbW91dGgsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKCoventryMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Coventry,England,United Kingdom", "en-gb", "w+CAIQICIfQ292ZW50cnksRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKCroydonMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Croydon,England,United Kingdom", "en-gb", "w+CAIQICIeQ3JveWRvbixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKNorthamptonMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Northampton,England,United Kingdom", "en-gb", "w+CAIQICIiTm9ydGhhbXB0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKNorwichMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Norwich,England,United Kingdom", "en-gb", "w+CAIQICIeTm9yd2ljaCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKSloughMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Slough,England,United Kingdom", "en-gb", "w+CAIQICIdU2xvdWdoLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKWatfordMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Watford,England,United Kingdom", "en-gb", "w+CAIQICIeV2F0Zm9yZCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public Dictionary<string, ArrayList> GetTop100GoogleUKLondonSW1VMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "SW1V,England,United Kingdom", "en-gb", "w+CAIQICIbU1cxVixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public Dictionary<string, ArrayList> GetTop100GoogleCyprusMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.cy", "Cyprus", "EL-CY", "w+CAIQICIGQ3lwcnVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleEstoniaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ee", "Estonia", "ET-EE", "w+CAIQICIHRXN0b25pYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public Dictionary<string, ArrayList> GetTop100GoogleLithuaniaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "lt", "Lithuania", "LT-LT", "w+CAIQICIJTGl0aHVhbmlh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSFloridaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Florida,United States", "en-us", "w+CAIQICIVRmxvcmlkYSxVbml0ZWQgU3RhdGVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSPennsylvaniaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Pennsylvania,United States", "en-us", "w+CAIQICIaUGVubnN5bHZhbmlhLFVuaXRlZCBTdGF0ZXM=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSWisconsinMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Wisconsin,United States", "en-us", "w+CAIQICIXV2lzY29uc2luLFVuaXRlZCBTdGF0ZXM=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSMichiganMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Michigan,United States", "en-us", "w+CAIQICIWTWljaGlnYW4sVW5pdGVkIFN0YXRlcw==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSArizonaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Arizona,United States", "en-us", "w+CAIQICIVQXJpem9uYSxVbml0ZWQgU3RhdGVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSOhioMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Ohio,United States", "en-us", "w+CAIQICIST2hpbyxVbml0ZWQgU3RhdGVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSNorthCarolinaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "North Carolina,United States", "en-us", "w+CAIQICIcTm9ydGggQ2Fyb2xpbmEsVW5pdGVkIFN0YXRlcw==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSTallahasseeFloridaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Tallahassee,Florida,United States", "en-us", "w+CAIQICIhVGFsbGFoYXNzZWUsRmxvcmlkYSxVbml0ZWQgU3RhdGVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public Dictionary<string, ArrayList> GetTop100GoogleUSHarrisburgPennsylvaniaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Harrisburg,Pennsylvania,United States", "en-us", "w+CAIQICIlSGFycmlzYnVyZyxQZW5uc3lsdmFuaWEsVW5pdGVkIFN0YXRlcw==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSMadisonWisconsinMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Madison,Wisconsin,United States", "en-us", "w+CAIQICIfTWFkaXNvbixXaXNjb25zaW4sVW5pdGVkIFN0YXRlcw==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }


        public Dictionary<string, ArrayList> GetTop100GoogleUSPhoenixArizonaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Phoenix,Arizona,United States", "en-us", "w+CAIQICIdUGhvZW5peCxBcml6b25hLFVuaXRlZCBTdGF0ZXM=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSLansingMichiganMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Lansing,Michigan,United States", "en-us", "w+CAIQICIeTGFuc2luZyxNaWNoaWdhbixVbml0ZWQgU3RhdGVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSColumbusOhioMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Columbus,Ohio,United States", "en-us", "w+CAIQICIbQ29sdW1idXMsT2hpbyxVbml0ZWQgU3RhdGVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSRaleighNorthCarolinaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Raleigh,North Carolina,United States", "en-us", "w+CAIQICIkUmFsZWlnaCxOb3J0aCBDYXJvbGluYSxVbml0ZWQgU3RhdGVz");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleMonacoEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "mn", "Monaco", "en-mc", "w+CAIQICIGTW9uYWNv");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        
        public Dictionary<string, ArrayList> GetTop100GoogleMonacoFrenchMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "mn", "Monaco", "fr-mc", "w+CAIQICIGTW9uYWNv");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUSMonacoSpanishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "United States", "es-419-us", "w+CAIQICIWVG9yb250byxPbnRhcmlvLENhbmFkYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        
        public Dictionary<string, ArrayList> GetTop100GoogleUSChineseMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "United States", "zh-cn", "w+CAIQICIWVG9yb250byxPbnRhcmlvLENhbmFkYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        ///-----------------------/////////////

        public Dictionary<string, ArrayList> GetTop100GoogleNewcastleCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Newcastle,Northern Ireland,United Kingdom", "en-gb", "w+CAIQICIpTmV3Y2FzdGxlLE5vcnRoZXJuIElyZWxhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleNewcastleCityEnglandMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Newcastle upon Tyne,England,United Kingdom", "en-gb", "w+CAIQICIqTmV3Y2FzdGxlIHVwb24gVHluZSxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKLondonE1Mobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "E1,England,United Kingdom", "en-gb", "w+CAIQICIZRTEsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKLondonNW1Mobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "NW1,England,United Kingdom", "en-gb", "w+CAIQICIaTlcxLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKLondonSE1Mobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "SE1,England,United Kingdom", "en-gb", "w+CAIQICIaU0UxLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }

        public Dictionary<string, ArrayList> GetTop100GoogleSunderlandCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Sunderland,England,United Kingdom", "en-gb", "w+CAIQICIhU3VuZGVybGFuZCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSunderlandCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Sunderland,England,United Kingdom", "en-gb", "w+CAIQICIhU3VuZGVybGFuZCxFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleWolverhamptonCityDesktop(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Wolverhampton,England,United Kingdom", "en-gb", "w+CAIQICIkV29sdmVyaGFtcHRvbixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleWolverhamptonCityMobile(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Wolverhampton,England,United Kingdom", "en-gb", "w+CAIQICIkV29sdmVyaGFtcHRvbixFbmdsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePlymouthCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Plymouth,England,United Kingdom", "en-gb", "w+CAIQICIfUGx5bW91dGgsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePlymouthCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Plymouth,England,United Kingdom", "en-gb", "w+CAIQICIfUGx5bW91dGgsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCardiffCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Cardiff,Wales,United Kingdom", "en-gb", "w+CAIQICIcQ2FyZGlmZixXYWxlcyxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCardiffCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Cardiff,Wales,United Kingdom", "en-gb", "w+CAIQICIcQ2FyZGlmZixXYWxlcyxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleOxfordCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Oxford,England,United Kingdom", "en-gb", "w+CAIQICIdT3hmb3JkLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleOxfordCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Oxford,England,United Kingdom", "en-gb", "w+CAIQICIdT3hmb3JkLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCambridgeCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Cambridge,England,United Kingdom", "en-gb", "w+CAIQICIgQ2FtYnJpZGdlLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCambridgeCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Cambridge,England,United Kingdom", "en-gb", "w+CAIQICIgQ2FtYnJpZGdlLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBelfastCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Belfast,Northern Ireland,United Kingdom", "en-gb", "w+CAIQICInQmVsZmFzdCxOb3J0aGVybiBJcmVsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBelfastCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Belfast,Northern Ireland,United Kingdom", "en-gb", "w+CAIQICInQmVsZmFzdCxOb3J0aGVybiBJcmVsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleGlasgowCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Glasgow,Scotland,United Kingdom", "en-gb", "w+CAIQICIfR2xhc2dvdyxTY290bGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleGlasgowCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Glasgow,Scotland,United Kingdom", "en-gb", "w+CAIQICIfR2xhc2dvdyxTY290bGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleEdinburghCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Edinburgh,Scotland,United Kingdom", "en-gb", "w+CAIQICIhRWRpbmJ1cmdoLFNjb3RsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleEdinburghCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Edinburgh,Scotland,United Kingdom", "en-gb", "w+CAIQICIhRWRpbmJ1cmdoLFNjb3RsYW5kLFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBrightonCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Brighton,England,United Kingdom", "en-gb", "w+CAIQICIfQnJpZ2h0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBrightonCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Brighton,England,United Kingdom", "en-gb", "w+CAIQICIfQnJpZ2h0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleESInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "es", "Spain", "es-es", "w+CAIQICIFc3BhaW4=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleITInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "it", "Italy", "it-it", "w+CAIQICIFSXRhbHk=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSEInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "se", "Sweden", "sv-se", "w+CAIQICIGc3dlZGVu");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCNInternet(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "zh-cn-hk", "w+CAIQICIJSG9uZyBLb25n");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHKInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.hk", "Hong Kong", "zh-TW-HK", "w+CAIQICIJSG9uZyBLb25n");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSGInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.sg", "Singapore", "en-sg", "w+CAIQICIJU2luZ2Fwb3Jl");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleJapanInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.jp", "Japan", "ja-jp", "w+CAIQICIFSmFwYW4=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBrasilInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.br", "Brazil", "pt-br", "w+CAIQICIGQnJhemls");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDEInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "de", "Germany", "de-de", "w+CAIQICIHR2VybWFueQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCHInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "de-ch", "w+CAIQICIMU3dpdHplcmxhbmQK");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLUInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "lu", "Luxembourg", "de-lu", "w+CAIQICIKTHV4ZW1ib3VyZw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleATInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "at", "Austria", "de-at", "w+CAIQICIHQXVzdHJpYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleAE(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ae", "United Arab Emirates", "ar-ae", "w+CAIQICIUVW5pdGVkIEFyYWIgRW1pcmF0ZXM=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleTR(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.tr", "Turkey", "tr-tr", "w+CAIQICIGVHVya2V5");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMA(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.ma", "Morocco", "fr-ma", "w+CAIQICIHTW9yb2Njbw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLY(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.ly", "Libya", "ar-ly", "w+CAIQICIFTGlieWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDZ(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "dz", "Algeria", "fr-dz", "w+CAIQICIHQWxnZXJpYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUA(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.ua", "Ukraine", "uk-ua", "w+CAIQICIHVWtyYWluZQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleEgypt(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.eg", "Egypt", "ar-eg", "w+CAIQICIFRWd5cHQ=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBH(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.bh", "Bahrain", "ar-bh", "w+CAIQICIHQmFocmFpbg==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleQA(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.qa", "Qatar", "ar-qa", "w+CAIQICIFUWF0YXI=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSaudi(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.sa", "Saudi Arabia", "ar-sa", "w+CAIQICIMU2F1ZGkgQXJhYmlh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleKW(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.kw", "Kuwait", "ar-kw", "w+CAIQICIGS3V3YWl0");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleVN(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.vn", "Vietnam", "vi-vn", "w+CAIQICIHVmlldG5hbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleGabon(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ga", "Gabon", "fr-ga", "w+CAIQICIFR2Fib24=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleIN(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.in", "India", "en-in", "w+CAIQICIFSW5kaWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePT(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "pt", "Portugal", "pt-pt", "w+CAIQICIIUG9ydHVnYWw=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCZ(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "cz", "Czech Republic", "cs-cz", "w+CAIQICIHQ3plY2hpYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSK(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "sk", "Slovakia", "sk-sk", "w+CAIQICIIU2xvdmFraWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleIL(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.il", "Israel", "iw-il", "w+CAIQICIGSXNyYWVs");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSLocal(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "United States", "en-us", "w+CAIQICINVW5pdGVkIFN0YXRlcw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleZALocal(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.za", "South Africa", "en-za", "w+CAIQICIPU291dGggQWZyaWNhCgoK");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "United States", "en-us", "w+CAIQICINVW5pdGVkIFN0YXRlcw==");
                dict = MobilePattern(htmlsource, "");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleITMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "it", "Italy", "it-IT", "w+CAIQICIFSXRhbHk=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleIELocal(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ie", "Ireland", "en-ie", "w+CAIQICIHSXJlbGFuZA==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHoveCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Hove,England,United Kingdom", "en-gb", "w+CAIQICIoQnJpZ2h0b24gYW5kIEhvdmUsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHoveCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Hove,England,United Kingdom", "en-gb", "w+CAIQICIoQnJpZ2h0b24gYW5kIEhvdmUsRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSouthamptonCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Southampton,England,United Kingdom", "en-gb", "w+CAIQICIiU291dGhhbXB0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSouthamptonCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Southampton,England,United Kingdom", "en-gb", "w+CAIQICIiU291dGhhbXB0b24sRW5nbGFuZCxVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePhilippinesInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.ph", "Philippines", "en-ph", "w+CAIQICILUGhpbGlwcGluZXM="); ;
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePhilippinesInternetMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.ph", "Philippines", "en-ph", "w+CAIQICILUGhpbGlwcGluZXM=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBahamasInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "bs", "Bahamas", "en-bs", "w+CAIQICIHQmFoYW1hcw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBahamasInternetMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "bs", "Bahamas", "en-bs", "w+CAIQICIHQmFoYW1hcw==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleJamaicaInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.jm", "Jamaica", "en-jm", "w+CAIQICIHSmFtYWljYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleJamaicaInternetMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.jm", "Jamaica", "en-jm", "w+CAIQICIHSmFtYWljYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleAus(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.au", "Australia", "en-au", "w+CAIQICIJQXVzdHJhbGlh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMexicoInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.mx", "Mexico", "en-mx", "w+CAIQICIGTWV4aWNv");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMexicoInternetMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.mx", "Mexico", "en-mx", "w+CAIQICIGTWV4aWNv");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePuertoRicoInternetEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.pr", "Puerto Rico", "en-pr", "w+CAIQICILUHVlcnRvIFJpY28=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePuertoRicoInternetMobileEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.pr", "Puerto Rico", "en-pr", "w+CAIQICILUHVlcnRvIFJpY28=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePuertoRicoInternetSpanish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.pr", "Puerto Rico", "es-419-pr", "w+CAIQICILUHVlcnRvIFJpY28=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePuertoRicoInternetMobileSpanish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.pr", "Puerto Rico", "es-419-pr", "w+CAIQICILUHVlcnRvIFJpY28=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCanadaTorontoCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ca", "Toronto,Canada", "en-ca", "w+CAIQICIWVG9yb250byxPbnRhcmlvLENhbmFkYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCanadaTorontoCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ca", "Toronto,Canada", "en-ca", "w+CAIQICIWVG9yb250byxPbnRhcmlvLENhbmFkYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleVenezuela(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.ve", "Venezuela", "es-419-ve", "w+CAIQICIJVmVuZXp1ZWxh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleVenezuelaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.ve", "Venezuela", "es-419-ve", "w+CAIQICIJVmVuZXp1ZWxh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleArgentinaInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.ar", "Argentina", "es-419-ar", "w+CAIQICIJQXJnZW50aW5h");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDallasTexasCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Dallas,Texas,United States", "en-us", "w+CAIQICIaRGFsbGFzLFRleGFzLFVuaXRlZCBTdGF0ZXM=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleDallasTexasCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Dallas,Texas,United States", "en-us", "w+CAIQICIaRGFsbGFzLFRleGFzLFVuaXRlZCBTdGF0ZXM=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNGMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.ng", "Nigeria", "en-ng", "w+CAIQICIHTmlnZXJpYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleKenya(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.ke", "Kenya", "en-ke", "w+CAIQICIFS2VueWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleKenyaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.ke", "Kenya", "en-ke", "w+CAIQICIFS2VueWE=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSydneyCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.au", "Sydney,Australia", "en-au", "w+CAIQICIgU3lkbmV5LE5ldyBTb3V0aCBXYWxlcyxBdXN0cmFsaWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMelbourneCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.au", "Melbourne,Australia", "en-au", "w+CAIQICIcTWVsYm91cm5lLFZpY3RvcmlhLEF1c3RyYWxpYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;

        }
        public Dictionary<string, ArrayList> GetTop100GoogleBrisbanCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.au", "Brisbane,Australia", "en-au", "w+CAIQICIdQnJpc2JhbmUsUXVlZW5zbGFuZCxBdXN0cmFsaWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePerthCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.au", "Perth,Australia", "en-au", "w+CAIQICIhUGVydGgsV2VzdGVybiBBdXN0cmFsaWEsQXVzdHJhbGlh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleAdelaideCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.au", "Adelaide,Australia", "en-au", "w+CAIQICIiQWRlbGFpZGUsU291dGggQXVzdHJhbGlhLEF1c3RyYWxpYQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSydneyCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.au", "Sydney,Australia", "en-au", "w+CAIQICIgU3lkbmV5LE5ldyBTb3V0aCBXYWxlcyxBdXN0cmFsaWE=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMelbourneCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.au", "Melbourne,Australia", "en-au", "w+CAIQICIcTWVsYm91cm5lLFZpY3RvcmlhLEF1c3RyYWxpYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBrisbanCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.au", "Brisbane,Australia", "en-au", "w+CAIQICIdQnJpc2JhbmUsUXVlZW5zbGFuZCxBdXN0cmFsaWE=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePerthCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.au", "Perth,Australia", "en-au", "w+CAIQICIhUGVydGgsV2VzdGVybiBBdXN0cmFsaWEsQXVzdHJhbGlh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleAdelaideCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.au", "Adelaide,Australia", "en-au", "w+CAIQICIiQWRlbGFpZGUsU291dGggQXVzdHJhbGlhLEF1c3RyYWxpYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBangladeshInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.bd", "Bangladesh", "en-bd", "w+CAIQICIKQmFuZ2xhZGVzaA");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleBangladeshMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.bd", "Bangladesh", "en-bd", "w+CAIQICIKQmFuZ2xhZGVzaA");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUK(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleLatviaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "lv", "Latvia", "lv-lv", "w+CAIQICIGTGF0dmlh");
                dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLatviaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "lv", "Latvia", "lv-lv", "w+CAIQICIGTGF0dmlh");
                dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        private Dictionary<string, ArrayList> GetTop100GoogleAspenColoradoDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Aspen,Colorado,United States", "en-us", "w+CAIQICIcQXNwZW4sQ29sb3JhZG8sVW5pdGVkIFN0YXRlcw==");

                dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        private Dictionary<string, ArrayList> GetTop100GoogleAspenColoradoMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Aspen,Colorado,United States", "en-us", "w+CAIQICIcQXNwZW4sQ29sb3JhZG8sVW5pdGVkIFN0YXRlcw==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        private Dictionary<string, ArrayList> GetTop100GoogleNapaCaliforniaDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "Napa,California,United States", "en-us", "w+CAIQICIfbmFwYSwgY2FsaWZvcm5pYSwgdW5pdGVkIHN0YXRlcw==");
                dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        private Dictionary<string, ArrayList> GetTop100GoogleNapaCaliforniaMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "Napa,California,United States", "en-us", "w+CAIQICIfbmFwYSwgY2FsaWZvcm5pYSwgdW5pdGVkIHN0YXRlcw==");
                dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        private Dictionary<string, ArrayList> GetTop100GoogleOmanArabicDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.om", "Oman", "ar-om", "w+CAIQICIET21hbg");
                dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        private Dictionary<string, ArrayList> GetTop100GoogleOmanArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.om", "Oman", "ar-om", "w+CAIQICIET21hbg");
                dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        private Dictionary<string, ArrayList> GetTop100GoogleOmanEnglishDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.om", "Oman", "en-om", "w+CAIQICIET21hbg");
                dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        private Dictionary<string, ArrayList> GetTop100GoogleOmanEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.om", "Oman", "en-om", "w+CAIQICIET21hbg");
                dict = MobilePattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSDesktopAdult(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com", "United States", "en-us", "w+CAIQICINVW5pdGVkIFN0YXRlcw==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUSMobileAdult(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com", "United States", "en-us", "w+CAIQICINVW5pdGVkIFN0YXRlcw==");
                dict = MobilePattern(htmlsource, "");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUKInternetDesktopAdult(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleUKMobileWebAdult(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleAusInternetDesktopAdult(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.au", "Australia", "en-au", "w+CAIQICIJQXVzdHJhbGlh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleAusMobileAdult(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.au", "Australia", "en-au", "w+CAIQICIJQXVzdHJhbGlh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        private Dictionary<string, ArrayList> GetTop100GoogleUKImages_PageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources_Nws_Images(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                dict = ImagesPattern(htmlsource, keyword, "PageLinks");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        /*private Dictionary<string, ArrayList> GetTop100GoogleUKImages_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource =  GetOxylabsWebDataSources_Nws_Images(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                dict = ImagesPattern(htmlsource, keyword, "ImageLinks");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return  dict;
        }*/

        private Dictionary<string, ArrayList> GetTop100GoogleUKImages_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources_Nws_Images(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");

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
        private Dictionary<string, ArrayList> GetTop100GoogleUKMobileImages_PageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources_Nws_Images_Mobile(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                dict = ImagesPatternMobile(htmlsource, keyword, "PageLinks");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        /* private Dictionary<string, ArrayList> GetTop100GoogleUKMobileImages_ImageURLs(string keyword)
         {
             Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
             try
             {
                 ArrayList htmlsource =  GetOxylabsWebDataSources_Nws_Images_Mobile(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                 dict = ImagesPatternMobile(htmlsource, keyword, "ImageLinks");
             }
             catch (Exception ex)
             {
                 string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
             }
             return  dict;
         }*/

        private Dictionary<string, ArrayList> GetTop100GoogleUKMobileImages_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources_Nws_Images_Mobile(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");

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


        public Dictionary<string, ArrayList> GetTop100GoogleHemelHempsteadCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Hemel Hempstead,England,United Kingdom", "en-gb", "w+CAIQICIfSGVtZWwgSGVtcHN0ZWFkLCBVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleHemelHempsteadCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Hemel Hempstead,England,United Kingdom", "en-gb", "w+CAIQICIfSGVtZWwgSGVtcHN0ZWFkLCBVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLeicesterCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Leicester,England,United Kingdom", "en-gb", "w+CAIQICIZTGVpY2VzdGVyLCBVbml0ZWQgS2luZ2RvbQ==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLeicesterCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Leicester,England,United Kingdom", "en-gb", "w+CAIQICIZTGVpY2VzdGVyLCBVbml0ZWQgS2luZ2RvbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNottinghamCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Nottingham,England,United Kingdom", "en-gb", "w+CAIQICIaTm90dGluZ2hhbSwgVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleNottinghamCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Nottingham,England,United Kingdom", "en-gb", "w+CAIQICIaTm90dGluZ2hhbSwgVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePortsmouthCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Portsmouth,England,United Kingdom", "en-gb", "w+CAIQICIaUG9ydHNtb3V0aCwgVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GooglePortsmouthCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Portsmouth,England,United Kingdom", "en-gb", "w+CAIQICIaUG9ydHNtb3V0aCwgVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleReadingCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Reading,England,United Kingdom", "en-gb", "w+CAIQICIXUmVhZGluZywgVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleReadingCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Reading,England,United Kingdom", "en-gb", "w+CAIQICIXUmVhZGluZywgVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleStokeonTrentCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Stoke-on-Trent,England,United Kingdom", "en-gb", "w+CAIQICIeU3Rva2Utb24tVHJlbnQsIFVuaXRlZCBLaW5nZG9t");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleStokeonTrentCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Stoke-on-Trent,England,United Kingdom", "en-gb", "w+CAIQICIeU3Rva2Utb24tVHJlbnQsIFVuaXRlZCBLaW5nZG9t");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSwanseaCityDesktop(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Swansea,Wales,United Kingdom", "en-gb", "w+CAIQICIXU3dhbnNlYSwgVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSwanseaCityMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Swansea,Wales,United Kingdom", "en-gb", "w+CAIQICIXU3dhbnNlYSwgVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleVNMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.vn", "Vietnam", "vi-vn", "w+CAIQICIHVmlldG5hbQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMyanmarInternet(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.mm", "Myanmar", "my-mm", "w+CAIQICIHTXlhbm1hcg==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMyanmarInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.mm", "Myanmar", "my-mm", "w+CAIQICIHTXlhbm1hcg==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        /*private Dictionary<string, ArrayList> GetTop100GoogleUKDesktop_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource =  GetOxylabsWebDataSources_Nws_Images(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                dict = ImagesPattern(htmlsource, keyword, "ImageLinks");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return  dict;
        }*/


        //private Dictionary<string, ArrayList> GetTop100GoogleUKDesktop_ImageURLs(string keyword)
        //{
        //    Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
        //    try
        //    {
        //        ArrayList htmlsource = GetOxylabsWebDataSources_Nws_Images(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");

        //        ArrayList myList = new ArrayList();
        //        ArrayList alDup = new ArrayList();

        //        foreach (string[] src in htmlsource)
        //        {

        //            string html = src[1].ToString().Replace(@"\", "");
        //            string matchPattern = "\"ou\":\"(.*?)\",";
        //            Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        //            MatchCollection mc = re.Matches(html);

        //            foreach (Match m in mc)
        //            {

        //                string HtmlText = HttpUtility.HtmlDecode(m.Groups[1].Value);
        //                if (HtmlText.StartsWith("http") || HtmlText.StartsWith("https"))
        //                {
        //                    int n = HtmlText.IndexOf("?");
        //                    if (n > 0)
        //                        HtmlText = HtmlText.Remove(n);
        //                    HtmlText = checkurls(HtmlText);
        //                    alDup.Add(HtmlText);
        //                }
        //            }
        //            foreach (string s in alDup)
        //            {
        //                if (myList.Contains(s) || string.IsNullOrEmpty(s)) continue;
        //                myList.Add(s);
        //            }

        //            if (myList.Count > 100)
        //            {
        //                myList.RemoveRange(100, myList.Count - 100);
        //            }

        //            if (myList.Count > 0)
        //                dict.Add(src[0] + ":" + src[2], myList);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
        //    }
        //    return dict;
        //}

        private Dictionary<string, ArrayList> GetTop100GoogleUKDesktop_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources_Nws_Images(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");
                dict = ImagesPattern(htmlsource, keyword, "ImageLinks");
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        private Dictionary<string, ArrayList> GetTop100GoogleUKMobile_ImageURLs(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources_Nws_Images_Mobile(keyword, "co.uk", "United Kingdom", "en-gb", "w+CAIQICIOVW5pdGVkIEtpbmdkb20=", "isch");

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
        public Dictionary<string, ArrayList> GetTop100GoogleRomaniaInternetMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ro", "Romania", "ro-ro", "w+CAIQICIHUm9tYW5pYQ==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleSwitzerlandEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "ch", "Switzerland", "en-ch", "w+CAIQICILU3dpdHplcmxhbmQ=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleSwitzerlandEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "ch", "Switzerland", "en-ch", "w+CAIQICILU3dpdHplcmxhbmQ=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleBahrainEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.bh", "Bahrain", "en-bh", "w+CAIQICIHQmFocmFpbg==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleEgyptEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.eg", "Egypt", "en-eg", "w+CAIQICIFRWd5cHQ=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleJordanEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "jo", "Jordan", "en-jo", "w+CAIQICIGSm9yZGFu");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleKuwaitEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.kw", "Kuwait", "en-kw", "w+CAIQICIGS3V3YWl0");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleLebanonEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.lb", "Lebanon", "en-lb", "w+CAIQICIHTGViYW5vbg==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleQatarEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.qa", "Qatar", "en-qa", "w+CAIQICIFUWF0YXI=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleSaudiArabiaEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.sa", "Saudi Arabia", "en-sa", "w+CAIQICIMU2F1ZGkgQXJhYmlh");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleBahrainEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.bh", "Bahrain", "en-bh", "w+CAIQICIHQmFocmFpbg==");
                dict = DesktopPattern(htmlsource, keyword);

            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleEgyptEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.eg", "Egypt", "en-eg", "w+CAIQICIFRWd5cHQ=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleJordanEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "jo", "Jordan", "en-jo", "w+CAIQICIGSm9yZGFu");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleKuwaitEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.kw", "Kuwait", "en-kw", "w+CAIQICIGS3V3YWl0");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleLebanonEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.lb", "Lebanon", "en-lb", "w+CAIQICIHTGViYW5vbg==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleQatarEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.qa", "Qatar", "en-qa", "w+CAIQICIFUWF0YXI=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleSaudiArabiaEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.sa", "Saudi Arabia", "en-sa", "w+CAIQICIMU2F1ZGkgQXJhYmlh");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }


        public Dictionary<string, ArrayList> GetTop100GoogleBahrainArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.bh", "Bahrain", "ar-bh", "w+CAIQICIHQmFocmFpbg==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleEgyptArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.eg", "Egypt", "ar-eg", "w+CAIQICIFRWd5cHQ=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleJordanArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "jo", "Jordan", "ar-jo", "w+CAIQICIGSm9yZGFu");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleKuwaitArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.kw", "Kuwait", "ar-kw", "w+CAIQICIGS3V3YWl0");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleLebanonArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.lb", "Lebanon", "ar-lb", "w+CAIQICIHTGViYW5vbg==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleQatarArabicMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.qa", "Qatar", "ar-qa", "w+CAIQICIFUWF0YXI=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleKoreanMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.kr", "South Korea", "ko-kr", "w+CAIQICILU291dGggS29yZWE=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleKorean(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.kr", "South Korea", "ko-kr", "w+CAIQICILU291dGggS29yZWE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCzechRepublicEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "cz", "Czech Republic", "en-cz", "w+CAIQICIOQ3plY2ggUmVwdWJsaWM=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleCzechRepublicEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "cz", "Czech Republic", "en-cz", "w+CAIQICIOQ3plY2ggUmVwdWJsaWM=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }
        public Dictionary<string, ArrayList> GetTop100GoogleMaltaEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.mt", "Malta", "en-mt", "w+CAIQICIFbWFsdGE=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleIceland(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "is", "Iceland", "is-is", "w+CAIQICIHSWNlbGFuZA==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleIcelandEnglish(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "is", "Iceland", "en-is", "w+CAIQICIHSWNlbGFuZA==");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKPeterboroughEnglish(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "co.uk", "Peterborough,England,United Kingdom", "en-gb", "w+CAIQICIjUGV0ZXJib3JvdWdoLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleMaltaEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.mt", "Malta", "en-mt", "w+CAIQICIFbWFsdGE=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleIcelandMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "is", "Iceland", "is-is", "w+CAIQICIHSWNlbGFuZA==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleIcelandEnglishMobile(string keyword)
        {
            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "is", "Iceland", "en-is", "w+CAIQICIHSWNlbGFuZA==");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> GetTop100GoogleUKPeterboroughEnglishMobile(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "co.uk", "Peterborough,England,United Kingdom", "en-gb", "w+CAIQICIjUGV0ZXJib3JvdWdoLEVuZ2xhbmQsVW5pdGVkIEtpbmdkb20=");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> getTop100GoogleTWInternetEnglish(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataSources(keyword, "com.tw", "Taiwan", "en-tw", "w+CAIQICIGVGFpd2Fu");
                dict = DesktopPattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

        public Dictionary<string, ArrayList> getTop100GoogleTWInternetEnglishMobile(string keyword)
        {

            Dictionary<string, ArrayList> dict = new Dictionary<string, ArrayList>();
            try
            {
                ArrayList htmlsource = GetOxylabsWebDataMobileSources(keyword, "com.tw", "Taiwan", "en-tw", "w+CAIQICIGVGFpd2Fu");
                dict = MobilePattern(htmlsource, keyword);
            }
            catch (Exception ex)
            {
                string errorGoogleUKInternet = "e100" + " " + ex.Message.ToString();
            }
            return dict;
        }

    }
}