using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;

namespace GoogleFirstPage.Xmltracker
{
    public partial class datewise : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["xmltracker"].ToString();
        private string st;
        private string stCol;
        private bool blChng;
        private bool blColChng;
        private MyLib.MyCls cls;
        public string yearValue = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtenddate.Attributes.Add("ReadOnly", "ReadOnly");
                txtenddate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                endDate.EndDate = DateTime.Now;

                string uid = (string)Request.QueryString["uid"];


                DateTime dy = DateTime.Now;
                txtenddate.Text = dy.ToString("yyy-MM-dd");

            }
            DateTime dd = DateTime.Now;
            yearValue = dd.Year.ToString();
        }

        private void Show(string curDate)
        {
            string uid = Request.QueryString["uid"].ToString();

            string strQuery = "Exec [XmlTracker].[dbo].[GetViewSourceChangeDate] '" + uid + "','" + curDate + "'";
            SqlConnection con;
            SqlCommand comm;

            int x = 0;
            try
            {
                con = new SqlConnection(connection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                comm = new SqlCommand(strQuery, con);
                comm.CommandTimeout = 0;
            }
            catch (Exception ex)
            {
                con.Close();
                con.Dispose();
                throw ex;
            }
            try
            {
                con.Open();
                SqlDataReader dr = comm.ExecuteReader();
                XPathNodeIterator old_title = null;
                XPathNodeIterator old_description = null;
                XPathNodeIterator old_keywords = null;
                XPathNodeIterator old_paragraph = null;
                XPathNodeIterator old_h1 = null;
                XPathNodeIterator old_h2 = null;
                XPathNodeIterator old_h3 = null;
                XPathNodeIterator old_em = null;
                XPathNodeIterator old_strong = null;
                XPathNodeIterator old_link_text = null;
                XPathNodeIterator old_link_url = null;
                XPathNodeIterator old_img_alt = null;
                XPathNodeIterator old_img_src = null;
                XPathNodeIterator old_td = null;
                XPathNodeIterator old_li = null;
                //XPathNodeIterator old_div = null;
                XPathNodeIterator old_time = null;
                string stDate = "";
                string xmlID = "0";
                while (dr.Read())
                {
                    SqlXml sx = dr.GetSqlXml(0);
                    XmlReader xr = sx.CreateReader();
                    XPathDocument xpd = new XPathDocument(xr);
                    XPathNavigator xpn = xpd.CreateNavigator();
                    stDate = dr.GetDateTime(1).Date.ToString("yyyy-MM-dd");
                    xmlID = dr.GetValue(2).ToString();

                    XPathNodeIterator canonical = xpn.Select("XmlSource");
                    st = "";
                    blChng = false;
                    x++;
                    string anc = "";
                    if (canonical.MoveNext())
                        st += "<br />Results for : <a href=\"" + canonical.Current.GetAttribute("url", "") + "\" target=\"_blank\" >" + canonical.Current.GetAttribute("url", "") + "</a>";
                    anc = "&nbsp; &nbsp; &nbsp; <a href=\"Default2.aspx?date=" + stDate + "&uid=" + xmlID + "\" target=\"=_blank\">[Elementary Data]</a>";
                    st += "<p /><table width=\"50%\" bordercolor=\"Blue\" cellpadding=\"0\" cellspacing=\"0\" border=\"1\">";
                    st += "<col width=\"10%\"> <col width=\"45%\"> <col width=\"45%\"> ";
                    st += "<tr ><th valign=\"top\">Element</th><th >&nbsp; &nbsp; In</th><th >&nbsp; &nbsp; Out</th></tr>";

                    XPathNodeIterator new_description = xpn.Select("XmlSource/tag/meta/description");
                    this.display_changes(old_description, new_description, "Meta");
                    old_description = new_description;

                    XPathNodeIterator new_keywords = xpn.Select("XmlSource/tag/meta/keywords");
                    this.display_changes(old_keywords, new_keywords, "Meta");
                    old_keywords = new_keywords;

                    XPathNodeIterator new_title = xpn.Select("XmlSource/tag/title/text");
                    this.display_changes(old_title, new_title, "Title");
                    old_title = new_title;


                    XPathNodeIterator new_time = xpn.Select("XmlSource/tag/time/text");
                    display_changes(old_time, new_time, "Time");
                    old_time = new_time;

                    XPathNodeIterator new_strong = xpn.Select("XmlSource/tag/strong/text");
                    display_changes(old_strong, new_strong, "Strong");
                    old_strong = new_strong;

                    XPathNodeIterator new_paragraph = xpn.Select("XmlSource/tag/p/text");
                    display_changes(old_paragraph, new_paragraph, "P");
                    old_paragraph = new_paragraph;

                    XPathNodeIterator new_h1 = xpn.Select("XmlSource/tag/h1/text");
                    display_changes(old_h1, new_h1, "H1");
                    old_h1 = new_h1;

                    XPathNodeIterator new_h2 = xpn.Select("XmlSource/tag/h2/text");
                    display_changes(old_h2, new_h2, "H2");
                    old_h2 = new_h2;

                    XPathNodeIterator new_h3 = xpn.Select("XmlSource/tag/h3/text");
                    display_changes(old_h3, new_h3, "H3");
                    old_h3 = new_h3;

                    XPathNodeIterator new_em = xpn.Select("XmlSource/tag/em/text");
                    display_changes(old_em, new_em, "EM");
                    old_em = new_em;


                    XPathNodeIterator new_linkurl = xpn.Select("XmlSource/tag/anchor/href");
                    display_changes(old_link_url, new_linkurl, "A Href");
                    old_link_url = new_linkurl;

                    XPathNodeIterator new_linktext = xpn.Select("XmlSource/tag/anchor/text");
                    display_changes(old_link_text, new_linktext, "A Text");
                    old_link_text = new_linktext;

                    XPathNodeIterator new_imagealt = xpn.Select("XmlSource/tag/image/alt");
                    display_changes(old_img_alt, new_imagealt, "Image Alt");
                    old_img_alt = new_imagealt;

                    XPathNodeIterator new_imagesrc = xpn.Select("XmlSource/tag/image/src");
                    display_changes(old_img_src, new_imagesrc, "Image Src");
                    old_img_src = new_imagesrc;

                    XPathNodeIterator new_td = xpn.Select("XmlSource/tag/td/text");
                    display_changes(old_td, new_td, "TD");
                    old_td = new_td;

                    XPathNodeIterator new_li = xpn.Select("XmlSource/tag/li/text");
                    display_changes(old_li, new_li, "LI");
                    old_li = new_li;


                    //XPathNodeIterator new_div = xpn.Select("XmlSource/tag/div");
                    //display_changes(old_div, new_div, "Div");
                    //old_div = new_div;

                    st += "</table>";

                    xr.Close();
                    Label lbldt = new Label();
                    lbldt.ID = "lbldt" + x.ToString();
                    lbldt.Text = "<b>[" + stDate + "] </b>";
                    if (!blChng) { st = ""; }
                    phr.Controls.Add(lbldt);
                    phr.Controls.Add(new LiteralControl(anc + " <br />"));
                    
                    if(!string.IsNullOrEmpty(st))
                        phr.Controls.Add(new LiteralControl("<br /><a href=\"XmlDiff.aspx?date=" + stDate + "&uid=" + xmlID + "\" target=\"=_blank\">[View Xml Format]</a> <br />"));

                    Label lbl = new Label();
                    lbl.ID = "spn" + x.ToString();

                    lbl.Text = st;

                    phr.Controls.Add(lbl);
                    phr.Controls.Add(new LiteralControl("<br />"));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error - " + ex.Message);
            }
            finally
            {
                comm.Dispose();
                con.Close();
                con.Dispose();
            }
        }

        private void display_changes(XPathNodeIterator oldnode, XPathNodeIterator newnode, string s)
        {
            stCol = "";
            blColChng = false;
            if (oldnode != newnode && oldnode != null)
            {
                stCol += "<tr >";
                stCol += "<td valign='top' word-wrap='break-word'><b>" + s + "</b></td>";
                stCol += "<td ><ul>";

                IEnumerator nd = newnode.GetEnumerator();
                IEnumerator od = oldnode.GetEnumerator();
                if (nd != null)
                {
                    if (od != null)
                    {
                        cls.blChng = blChng;
                        cls.blColChng = blColChng;
                        string[] sdif = cls.diffInfo(nd, od, s);
                        blChng = cls.blChng;
                        blColChng = cls.blColChng;
                        stCol += sdif[0];
                        stCol += "</ul></td>";
                        stCol += "<td ><ul>";
                        stCol += sdif[1];
                    }
                    else
                    {
                        string stC = cls.getColumn(nd);
                        if (stC != "") stCol += stC;
                    }
                }
                stCol += "</ul></td></tr>";
            }
            else
            {
                blColChng = true;

                stCol += "<tr >";
                stCol += "<td word-wrap='break-word'><b>" + s + "</b></td>";
                stCol += "<td ><ul>";
                IEnumerator nd = newnode.GetEnumerator();
                if (nd != null)
                {
                    string stC = cls.getColumn(nd);
                    if (stC != "") stCol += stC;
                }
                stCol += "</ul></td> </tr>";
            }
            if (blColChng) { st += stCol; }
        }

        protected void btndatewise_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string eDate = txtenddate.Text;
                    cls = new MyLib.MyCls();
                    this.Show(eDate);
                }
                catch (Exception ex)
                {
                    Label lbl = new Label();
                    lbl.Text = ex.Message;
                    phr.Controls.Add(lbl);
                }
            }
        }
    }
}