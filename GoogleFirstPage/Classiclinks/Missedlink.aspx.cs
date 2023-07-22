using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace GoogleFirstPage.Classiclinks
{
    public partial class Missedlink : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string kwd = Request.QueryString["keyword"].ToString();
            GetProcessedLists();
        }

        public DataTable Table()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("URL");
            dt.Columns.Add("MissedIn");
            return dt;
        }
        List<ArrayList> GetProcessedLists()
        {
            string xml1 = Server.MapPath(@"\files\selector.xml");
            string xml2 = Server.MapPath(@"\files\regex.xml");

            List<ArrayList> al = new List<ArrayList>();
            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();

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

            ArrayList missedList = new ArrayList();
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
            BindData(missedList);
            al.Add(list1);
            al.Add(list2);
            return al;
        }

        public void BindData(ArrayList missedList)
        {
            DataTable dt = Table();
            foreach (string[] s in missedList)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(s[0], s[1]);
            }
            gvmissed.DataSource = dt;
            gvmissed.DataBind();
        }
    }
}