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
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace GoogleFirstPage.RapidTrackingSERPs
{
    public partial class Live : System.Web.UI.Page
    {
        public string jobid = string.Empty;
        SqlConnection con = new SqlConnection();
        string s = string.Empty;
        string houre = string.Empty;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;
        int count;
        int requestcount = 0;
        string connection = ConfigurationManager.ConnectionStrings["allsearchengines"].ToString();
        string connection1 = ConfigurationManager.ConnectionStrings["allelements"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            oxydiv1.Visible = false;
            Label1.Visible = false;
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    cmd = new SqlCommand("GetSearchEngines", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    ddlseid.DataTextField = "name";
                    ddlseid.DataValueField = "seid";
                    ddlseid.DataSource = dt;
                    ddlseid.DataBind();
                    ddlseid.SelectedItem.Value.ToString();
                    ddlseid.SelectedIndex = 0;
                }
            }
        }

        protected void btngetdata_Click(object sender, EventArgs e)
        {
            oxydiv1.Visible = true;
            Label1.Visible = true;
            try
            {
                ArrayList data = new ArrayList();
                DataTable dt = Table();
                DataSet ds = new DataSet();
                var doc = new HtmlAgilityPack.HtmlDocument();

                string seid = ddlseid.SelectedValue;
                string kewrd = txtlivesearch.Text;
                ArrayList alresult = GetHTML(kewrd, Convert.ToInt32(seid));
                foreach (string[] src in alresult)
                {
                    string keyword = src[0];
                    JObject obj = JObject.Parse(src[1]);
                    string html = obj["results"][0]["content"].Value<string>();
                    string jobid = src[2];
                    string device = src[3];
                    SendToDatabase(Convert.ToInt32(seid), keyword, jobid);
                    doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    oxylabsjobid.Text = jobid.Trim();
                    if (device == "desktop")
                    {
                        Desktop clsdesktop = new Desktop();
                        string res = clsdesktop.ProcessDocument(seid, kewrd, doc, out count);
                        XmlDocument doc1 = new XmlDocument();
                        doc1.LoadXml(res);
                        XmlElement root = doc1.DocumentElement;
                        int Pos = 0;
                        int clscnt = 0;
                        int blkcnt = 0;
                        try
                        {
                            foreach (XmlNode n in root)
                            {
                                foreach (XmlNode node in n)
                                {
                                    if (node.Name != "block")
                                    {
                                        clscnt++;
                                        //Pos = Pos + 1;
                                        Pos++;
                                        dt.Rows.Add("", node.Attributes[0].Value, node.Attributes[1].Value, Pos, "CL");
                                        blkcnt = 0;
                                    }
                                    else if (node.Attributes[0].Value.ToString() == "classicLinkCarousel")
                                    {
                                        Pos++;
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">", node.Attributes[1].Value, node.Attributes[2].Value, Pos, "CL");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    else if (node.Attributes[0].Value.ToString() == "classicLinkSiteLinks")
                                    {
                                        Pos++;
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">", node.Attributes[1].Value, node.Attributes[2].Value, Pos, "CL");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            blkcnt++;
                                            //if (dt.Rows.Count != 0 && blkcnt == 0)
                                            //    dt.Rows.Add("", "", "");
                                            //if (clscnt >= 1)
                                            //    dt.Rows.Add("", "", "");
                                            DataRow row11 = dt.NewRow();
                                            //if (node.Attributes[0].Value.ToString() == "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            //{
                                            //    dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                            //    dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            //}
                                            if (node.Attributes[0].Value.ToString() == "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    dt.Rows.Add("", nodee.Attributes[0].Value, " Content = " + nodee.Attributes[1].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "peopleAlsoSearch")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    dt.Rows.Add("", "", nodee.Attributes[0].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    //dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value, " Source = " + nodee.Attributes[2].Value);
                                                    dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value + "  -----  " + " Source = " + nodee.Attributes[2].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "findResultsOn" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    dt.Rows.Add("", nodee.Attributes[1].Value, " Title = " + "" + " ,  " + " source = " + nodee.Attributes[0].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "videoCard" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "dataset")
                                            {
                                                dt.Rows.Add("", node.Attributes[1].Value);
                                            }
                                            if (node.Attributes[0].Value.ToString() == "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {                                                    
                                                    dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value + " --- " + " description = " + nodee.Attributes[2].Value + " --- " + " cardType = " + nodee.Attributes[3].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "dataset" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "hotelPack" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value + " --- " + " publishDate = " + nodee.Attributes[2].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() != "hotelPack" && node.Attributes[0].Value.ToString() != "flightPack" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                if (node.Attributes[0].Value.ToString() != "findResultsOn")
                                                {
                                                    dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                    foreach (XmlNode nodee in node)
                                                    {
                                                        if (node.Attributes[0].Value.ToString() == "popularProducts")
                                                        {
                                                            //dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value + " --- " + " Price = " + nodee.Attributes[2].Value + " --- " + " Site = " + nodee.Attributes[3].Value);
                                                            dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value + " --- " + " Price = " + nodee.Attributes[2].Value + " --- " + " PriceValue = " + nodee.Attributes[3].Value + " --- " + " Site = " + nodee.Attributes[4].Value);
                                                        }
                                                        else if (node.Attributes[0].Value.ToString() == "videos")
                                                        {
                                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value + " ------ " + " creatorName = " + nodee.Attributes[2].Value);
                                                        }
                                                        else
                                                        {
                                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                                        }
                                                    }
                                                    dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                                }
                                            }
                                            if (node.Attributes[0].Value.ToString() == "hotelPack" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    if (nodee.Attributes.Count == 2)
                                                    {
                                                        dt.Rows.Add("", "", " AdditionalInfo= " + nodee.Attributes[0].Value + " , " + " Title = " + nodee.Attributes[1].Value);
                                                    }
                                                    else if (nodee.Attributes.Count == 4)
                                                    {
                                                        dt.Rows.Add("", "", " Rating = " + nodee.Attributes[0].Value + " , " + " TotalReviews = " + nodee.Attributes[1].Value + " , " + " AdditionalInfo = " + nodee.Attributes[2].Value + " , " + " Title = " + nodee.Attributes[3].Value);
                                                    }
                                                    else
                                                    {
                                                        dt.Rows.Add("", "", " Price = " + nodee.Attributes[0].Value + " , " + " PriceValue = " + nodee.Attributes[1].Value + " , " + " Rating = " + nodee.Attributes[2].Value + " , " + " TotalReviews = " + nodee.Attributes[3].Value + " , " + " AdditionalInfo = " + nodee.Attributes[4].Value + " , " + " Title = " + nodee.Attributes[5].Value);
                                                    }
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "flightPack" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                if (node.Attributes.Count == 3)
                                                {
                                                    dt.Rows.Add("", "", " Origin = " + node.Attributes[1].Value + " , " + " Destination= " + node.Attributes[2].Value);
                                                }
                                                foreach (XmlNode nodee in node)
                                                {
                                                    dt.Rows.Add("", "", " Airline = " + nodee.Attributes[0].Value + "  , " + " Duration = " + nodee.Attributes[1].Value + " , " + " DurationValue = " + nodee.Attributes[2].Value + " , " + " Connections = " + nodee.Attributes[3].Value + " , " + " Price = " + nodee.Attributes[4].Value + " , " + " PriceValue = " + nodee.Attributes[5].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            clscnt = 0;
                                            //dt.Rows.Add("", "", "");
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }
                                    }
                                }
                                resultscnt.Text = "" + Pos.ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    else
                    {
                        iOS clsios = new iOS();
                        string res = clsios.ProcessDocument(seid, kewrd, doc, out count);
                        XmlDocument doc1 = new XmlDocument();
                        doc1.LoadXml(res);
                        XmlElement root = doc1.DocumentElement;
                        int Pos = 0;
                        int clscnt = 0;
                        int blkcnt = 0;
                        try
                        {
                            foreach (XmlNode n in root)
                            {
                                foreach (XmlNode node in n)
                                {
                                    if (node.Name != "block")
                                    {
                                        clscnt++;
                                        //Pos = Pos + 1;
                                        Pos++;
                                        dt.Rows.Add("", node.Attributes[0].Value, node.Attributes[1].Value, Pos, "CL");
                                        blkcnt = 0;
                                    }
                                    else if (node.Attributes[0].Value.ToString() == "classicLinkCarousel")
                                    {
                                        Pos++;
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">", node.Attributes[1].Value, node.Attributes[2].Value, Pos, "CL");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    else if (node.Attributes[0].Value.ToString() == "classicLinkSiteLinks")
                                    {
                                        Pos++;
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">", node.Attributes[1].Value, node.Attributes[2].Value, Pos, "CL");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            blkcnt++;
                                            //if (dt.Rows.Count != 0 && blkcnt == 0)
                                            //    dt.Rows.Add("", "", "");
                                            //if (clscnt >= 1)
                                            //    dt.Rows.Add("", "", "");
                                            DataRow row11 = dt.NewRow();
                                            //if (node.Attributes[0].Value.ToString() == "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            //{
                                            //    dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                            //    dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            //}
                                            if (node.Attributes[0].Value.ToString() == "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    dt.Rows.Add("", nodee.Attributes[0].Value, " Content = " + nodee.Attributes[1].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "peopleAlsoSearch")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    dt.Rows.Add("", "", nodee.Attributes[0].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    //dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value, " Source = " + nodee.Attributes[2].Value);
                                                    dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value + "  -----  " + " Source = " + nodee.Attributes[2].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "findResultsOn")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    dt.Rows.Add("", nodee.Attributes[1].Value, " Title = " + "" + " ,  " + " source = " + nodee.Attributes[0].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "videoCard" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview")
                                            {
                                                dt.Rows.Add("", node.Attributes[1].Value);
                                            }
                                            if (node.Attributes[0].Value.ToString() == "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value + " --- " + " Description = " + nodee.Attributes[2].Value + " --- " + " CardType = " + nodee.Attributes[3].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "dataset" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "hotelPack" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value + " ------ " + " publishDate = " + nodee.Attributes[2].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() != "hotelPack" && node.Attributes[0].Value.ToString() != "flightPack" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                if (node.Attributes[0].Value.ToString() != "findResultsOn")
                                                {
                                                    dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                    foreach (XmlNode nodee in node)
                                                    {
                                                        if (node.Attributes[0].Value.ToString() == "popularProducts")
                                                        {
                                                            dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value + " --- " + " Price = " + nodee.Attributes[2].Value + " --- " + " PriceValue = " + nodee.Attributes[3].Value + " --- " + " Site = " + nodee.Attributes[4].Value);
                                                        }
                                                        else if (node.Attributes[0].Value.ToString() == "videos")
                                                        {
                                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value + " ------ " + " creatorName = " + nodee.Attributes[2].Value);
                                                        }
                                                        else
                                                        {
                                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                                        }
                                                    }
                                                    dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                                }
                                            }
                                            if (node.Attributes[0].Value.ToString() == "hotelPack" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                foreach (XmlNode nodee in node)
                                                {
                                                    if (nodee.Attributes.Count == 2)
                                                    {
                                                        dt.Rows.Add("", "", " AdditionalInfo= " + nodee.Attributes[0].Value + " , " + " Title = " + nodee.Attributes[1].Value);
                                                    }
                                                    else if (nodee.Attributes.Count == 4)
                                                    {
                                                        dt.Rows.Add("", "", " Rating = " + nodee.Attributes[0].Value + " , " + " TotalReviews = " + nodee.Attributes[1].Value + " , " + " AdditionalInfo = " + nodee.Attributes[2].Value + " , " + " Title = " + nodee.Attributes[3].Value);
                                                    }
                                                    else
                                                    {
                                                        dt.Rows.Add("", "", " Price = " + nodee.Attributes[0].Value + " , " + " PriceValue = " + nodee.Attributes[1].Value + " , " + " Rating = " + nodee.Attributes[2].Value + " , " + " TotalReviews = " + nodee.Attributes[3].Value + " , " + " AdditionalInfo = " + nodee.Attributes[4].Value + " , " + " Title = " + nodee.Attributes[5].Value);
                                                    }
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            if (node.Attributes[0].Value.ToString() == "flightPack" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel")
                                            {
                                                dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                                if (node.Attributes.Count == 3)
                                                {
                                                    dt.Rows.Add("", "", " Origin = " + node.Attributes[1].Value + " , " + " Destination= " + node.Attributes[2].Value);
                                                }
                                                foreach (XmlNode nodee in node)
                                                {
                                                    dt.Rows.Add("", "", " Airline = " + nodee.Attributes[0].Value + "  , " + " Duration = " + nodee.Attributes[1].Value + " , " + " DurationValue = " + nodee.Attributes[2].Value + " , " + " Connections = " + nodee.Attributes[3].Value + " , " + " Price = " + nodee.Attributes[4].Value + " , " + " PriceValue = " + nodee.Attributes[5].Value);
                                                }
                                                dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                            }
                                            clscnt = 0;
                                            //dt.Rows.Add("", "", "");
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }
                                    }
                                }
                                resultscnt.Text = "" + Pos.ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                gridviewlive.DataSource = dt;
                gridviewlive.DataBind();
            }
            catch (Exception ex)
            {
                string yourValue = "Data not available";
                string error = ex.Message;
                Response.Write(String.Format("<script>alert('Data not available.');</script>", yourValue));
            }
        }

        public DataTable Table()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Block Type");
            dt.Columns.Add("URL");
            dt.Columns.Add("Title");
            dt.Columns.Add("Position");
            dt.Columns.Add("CL");
            return dt;
        }
        private void SendToDatabase(int seid, string keyword, string jobid)
        {
            string query = "";
            string myDate = DateTime.Today.ToString("yyyy-MM-dd");
            SqlCommand comm;
            try
            {
                using (SqlConnection con = new SqlConnection(connection1))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    query = "INSERT INTO [dbo].[LiveSearchCount] (Date,seid,keyword,jobid) VALUES (@date,@seid,@keyword,@jobid)";
                    comm = new SqlCommand(query, con);
                    comm.Parameters.Add("@date", SqlDbType.DateTime).Value = myDate;
                    comm.Parameters.Add("@seid", SqlDbType.Int).Value = seid;
                    comm.Parameters.Add("@keyword", SqlDbType.NVarChar).Value = keyword;
                    comm.Parameters.Add("@jobid", SqlDbType.NVarChar).Value = jobid.Trim();
                    comm.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                string error = ex.Message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ArrayList GetHTML(string keyword, int seid)
        {
            ArrayList alResult = new ArrayList();
            try
            {
                SearchProperties sp = SearchParams.searches.Where(s => s.seid == seid).SingleOrDefault();
                sp.query = keyword;
                if (sp != null)
                    alResult = GetOxylabsWebDataSources(sp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return alResult;
        }

        ArrayList GetOxylabsWebDataSources(SearchProperties sp)
        {
            Uri queryUri = new Uri("http://data.oxylabs.io/v1/queries/batch");
            //string username = "gpidatametrics";
            //string password = "sdV5X3fcX6";
            string username = "piapp";
            string password = "b5FCvgkjxx";
            string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password));

            OxyParams op = new OxyParams()
            {
                source = "google_search",
                domain = sp.domain,
                query = sp.query.Split(','),
                limit = 100,
                pages = 1,
                //start_page = 1,
                locale = sp.locale,
                geo_location = sp.geo_location,
                //uule = uule,
                parse = false, //23-09-2021 changed datatype into "int to bool"
                user_agent_type = sp.device,
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
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(op, new Newtonsoft.Json.JsonSerializerSettings
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

            JObject jo = JObject.Parse(response);
            var links = from p in jo["queries"] select p;
            ArrayList lst = new ArrayList();
            foreach (JToken link in links)
            {
                string kw = link["query"].Value<string>();
                string href = link["_links"][1]["href"].Value<string>();
                string status = link["status"].Value<string>();
                jobid = link["id"].Value<string>();
                string device = link["user_agent_type"].Value<string>();
                string[] s = { kw, href, status, "no", jobid, device };    // keyword, url, status, isdownloaded, jobid, device.
                lst.Add(s);
            }

            if (lst.Count <= 0) return lst;
            ArrayList alResult = new ArrayList();
            do
            {
                int cnt = 0;
                foreach (string[] cbUrl in lst)
                {
                    string[] reslt = { "", "", "", "" };
                    response = "";

                    Uri uri = new Uri(cbUrl[1]);
                    if (cbUrl[2] == "done" && cbUrl[3] == "no")
                    {
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
                                reslt[3] = cbUrl[5];
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
                    Task.Delay(200).Wait();
                }
                if (lst.Count == cnt) break;

            } while (true);

            return alResult;
        }


        protected void gridviewlive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                tc.Attributes["style"] = "border-right:1.5px solid black";
                tc.Attributes["style"] = "border-left:1.5px solid black";
                tc.Attributes["style"] = "border-top:1.5px solid black";
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text.Replace("&lt;", "").Replace("&gt;", "").Replace("/", "");
                //e.Row.Cells[i].ForeColor = Color.Blue;
            }
        }

        protected void txtlivesearch_TextChanged(object sender, EventArgs e)
        {
            gridviewlive.DataSource = null;
            gridviewlive.DataBind();
        }
    }
}