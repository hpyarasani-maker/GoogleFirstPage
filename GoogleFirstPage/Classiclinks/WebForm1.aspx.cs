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

namespace GoogleFirstPage.Classiclinks
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt111_Click(object sender, EventArgs e)
        {
           GetProcessedLists();
           
        }

        List<ArrayList> GetProcessedLists()
        {
            string xml1 = Server.MapPath(@"\files\selector.xml");
            string xml2 = Server.MapPath(@"\files\regex.xml");

            List<ArrayList> al = new List<ArrayList>();
            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();
            ArrayList missedList = new ArrayList();


            XmlDocument xml = new XmlDocument();
            xml.Load(xml1);
            XmlNodeList xnList1 = xml.SelectNodes("/searchResult/section/item/@url");
            foreach (XmlNode xn1 in xnList1)
            {
                list1.Add(xn1.InnerText);
            }

            xml.Load(xml2);
            XmlNodeList xnList2 = xml.SelectNodes("/searchResult/section/item/@url");
            foreach (XmlNode xn2 in xnList2)
            {
                list2.Add(xn2.InnerText);
            }

            string[] st = { "", "" };

            foreach (string s in list1)
            {
                if (!list2.Contains(s))
                {
                    st[0] = s;
                    st[1] = "RegEx";
                    missedList.Add(st);
                }
            }
            foreach (string s in list2)
            {
                if (!list1.Contains(s))
                {
                    st[0] = s;
                    st[1] = "Selector";
                    missedList.Add(st);
                }
            }
            if (missedList.Count > 0)
                SaveToXml(missedList);
                Binddata(missedList);
            al.Add(list1);
            al.Add(list2);
            //Dictionary<ArrayList, ArrayList> dict = new Dictionary<ArrayList,ArrayList>();

            //dict.Add(list2,missedList);
            //if (Page.IsValid)
            //{
            //    int count = 1;
            //    ArrayList myList = new ArrayList();
            //    foreach (KeyValuePair<ArrayList, ArrayList> kvp in dict)
            //    {
            //        ArrayList alRes = kvp.Value;
            //        for (int i = 0; i < alRes.Count; i++)
            //        {
            //            myList.Add(new mURL(alRes[i].ToString(),count++));
            //        }
            //    }

            //    if (list1.Count < list2.Count)
            //    {
            //        lblclcount.Text = "Missing classic links in selector";
            //    }
            //    else if(list2.Count < list1.Count)
            //    {
            //        lblclcount.Text = "Missing classic links in Regex";
            //    }
            //    grd11.DataSource = myList;
            //    grd11.DataBind();
            //}
            return al;
        }

        public void Binddata(ArrayList missedList)
        {
            DataTable dt = Table();

            foreach (string[] s in missedList)
            {
                //sb.Append("<item url=\"" + s[0] + "\" missedIn=\"" + s[1] + "\" />");
                DataRow row = dt.NewRow();
                dt.Rows.Add(s[0],s[1]);

            }
            grd11.DataSource = dt;
            grd11.DataBind();
        }

        public DataTable Table()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("URL");
            dt.Columns.Add("Missing from");
            return dt;
        }

        private void SaveToXml(ArrayList missedList)
        {
            string kwd = "jabra wireless earphones";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.Append("<searchResult searchEngine=\"" + 1 + "\" keyword=\"" + System.Net.WebUtility.HtmlEncode(kwd) + "\" date=\"" + DateTime.Today.ToString("yyyy-MM-dd") + "\" >");
            sb.Append("<section col=\"missedLinks\">");

            foreach (string[] s in missedList)
            {
                sb.Append("<item url=\"" + s[0] + "\" missedIn=\"" + s[1] + "\" />");
            }

            sb.Append("</section>");
            sb.Append("</searchResult>");

            string xmlPath = @"C:\inetpub\wwwroot\";
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sb.ToString());
            xd.Save(xmlPath + "missing.xml");
            //File.WriteAllText(Server.MapPath("~/files/" + "missing.xml"), sb.ToString());
        }
        public class mURL
        {
            private string url;
            private int position;
            public mURL(string url, int position)
            {
                this.url = url;
                this.position = position;
            }
            public string URL { get { return url; } }
            public int Position { get { return position; } }
        }

        

    }
}