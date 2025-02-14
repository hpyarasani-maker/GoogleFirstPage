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
using System.Drawing;

namespace GoogleFirstPage.RapidTrackingSERPs
{
    public partial class Jobid : System.Web.UI.Page
    {
        Oxylabsresponse oxyresponse = new Oxylabsresponse();
        protected void Page_Load(object sender, EventArgs e)
        {
            oxydiv1.Visible = false;
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

        public string GetJobid(string jobid)
        {
            string jobid1 = "jobidsource.aspx?jobid=" + jobid.Replace("'", "%27").Trim();
            ClientScript.RegisterStartupScript(this.GetType(), "Popup_File" + 1, "window.open('" + jobid1 + "');", true);
            return jobid;
        }

        public void DisplayDatainPage(string seid, string kw, string res, string jobid, string device)
        {
            try
            {
                ArrayList data = new ArrayList();
                DataTable dt = Table();
                DataSet ds = new DataSet();
                var doc = new HtmlAgilityPack.HtmlDocument();

                if (device == "desktop")
                {
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
                                    if (node.Attributes[0].Value.ToString() == "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", nodee.Attributes[0].Value, " Content = " + nodee.Attributes[1].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    //if (node.Attributes[0].Value.ToString() == "classicLinkSiteLinks")
                                    //{
                                    //    dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">", node.Attributes[1].Value, node.Attributes[2].Value);
                                    //    //dt.Rows.Add("", node.Attributes[1].Value, node.Attributes[2].Value);
                                    //    foreach (XmlNode nodee in node)
                                    //    {
                                    //        dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                    //    }
                                    //    dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    //}
                                    if (node.Attributes[0].Value.ToString() == "peopleAlsoSearch")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", "", nodee.Attributes[0].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    //if (node.Attributes[0].Value.ToString() == "classicLinkCarousel")
                                    //{
                                    //    dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">", node.Attributes[1].Value, node.Attributes[2].Value);
                                    //    //dt.Rows.Add("", node.Attributes[1].Value, node.Attributes[2].Value);
                                    //    foreach (XmlNode nodee in node)
                                    //    {
                                    //        dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                    //    }
                                    //    dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    //}
                                    if (node.Attributes[0].Value.ToString() == "sitesCarousel")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value + "  ----- " + " Source = " + nodee.Attributes[2].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    if (node.Attributes[0].Value.ToString() == "peopleAlsoBuyFrom")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    if (node.Attributes[0].Value.ToString() == "findResultsOn" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
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
                                    if (node.Attributes[0].Value.ToString() == "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                            //dt.Rows.Add("", nodee.Attributes[1].Value, " featureTitle = " + nodee.Attributes[0].Value + " --- " + " Title = " + nodee.Attributes[2].Value + " --- " + " description = " + nodee.Attributes[3].Value + " --- " + " isTable = " + nodee.Attributes[4].Value + " --- " + " isList = " + nodee.Attributes[5].Value + " --- " + " isChart = " + nodee.Attributes[6].Value + " --- " + " isVideo = " + nodee.Attributes[7].Value); //+ " --- " + " Table = " + nodee.LastChild.Attributes[8].Value
                                            //dt.Rows.Add("", nodee.Attributes[1].Value, " featureTitle = " + nodee.Attributes[0].Value + " --- " + " Title = " + nodee.Attributes[2].Value + " --- " + " description = " + nodee.Attributes[3].Value + " --- " + " cardType = " + nodee.Attributes[4].Value); //+ " --- " + " Table = " + nodee.LastChild.Attributes[8].Value
                                            dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value + " --- " + " description = " + nodee.Attributes[2].Value + " --- " + " cardType = " + nodee.Attributes[3].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    if (node.Attributes[0].Value.ToString() == "dataset" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "hotelPack" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value + " --- " + " publishDate = " + nodee.Attributes[2].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    if (node.Attributes[0].Value.ToString() != "hotelPack" && node.Attributes[0].Value.ToString() != "flightPack" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
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
                                    if (node.Attributes[0].Value.ToString() == "hotelPack" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
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
                                            //else if (nodee.Attributes.Count == 5)
                                            //{
                                            //    //dt.Rows.Add("", nodee.Attributes[0].Value, " Rating= " + nodee.Attributes[1].Value + " --- " + " TotalReviews = " + nodee.Attributes[2].Value + " --- " + " AdditionalInfo = " + nodee.Attributes[3].Value + " --- " + " Title = " + nodee.Attributes[4].Value);
                                            //    if (nodee.Attributes[0].Name == "price")
                                            //    {
                                            //        dt.Rows.Add("", "", " Price = " + nodee.Attributes[0].Value + " --- " + " Pricevalue = " + nodee.Attributes[1].Value + " --- " + " AdditionalInfo = " + nodee.Attributes[2].Value + " --- " + " Title = " + nodee.Attributes[3].Value);
                                            //    }
                                            //    else if (nodee.Attributes[1].Name != "price")
                                            //    {
                                            //        dt.Rows.Add("", nodee.Attributes[0].Value, " Rating = " + nodee.Attributes[1].Value + " --- " + " TotalReviews = " + nodee.Attributes[2].Value + " --- " + " AdditionalInfo = " + nodee.Attributes[3].Value + " --- " + " Title = " + nodee.Attributes[4].Value);
                                            //    }
                                            //}
                                            else
                                            {
                                                dt.Rows.Add("", "", " Price = " + nodee.Attributes[0].Value + " , " + " PriceValue = " + nodee.Attributes[1].Value + " , " + " Rating = " + nodee.Attributes[2].Value + " , " + " TotalReviews = " + nodee.Attributes[3].Value + " , " + " AdditionalInfo = " + nodee.Attributes[4].Value + " , " + " Title = " + nodee.Attributes[5].Value);
                                            }
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    if (node.Attributes[0].Value.ToString() == "flightPack" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        if (node.Attributes.Count == 3)
                                        {
                                            dt.Rows.Add("", "", " Origin = " + node.Attributes[1].Value + " , " + " Destination= " + node.Attributes[2].Value);
                                        }
                                        foreach (XmlNode nodee in node)
                                        {
                                            //dt.Rows.Add("", "", " url= " + "" + " , " + " title= " + "" + "  ,  " + " airline= " + nodee.Attributes[1].Value + "  ,  " + " duration= " + nodee.Attributes[3].Value + " , " + " durationValue= " + nodee.Attributes[4].Value + " , " + " connections= " + nodee.Attributes[5].Value + " , " + " price= " + nodee.Attributes[6].Value + " , " + " priceValue= " + nodee.Attributes[7].Value);
                                            dt.Rows.Add("", "", " Airline = " + nodee.Attributes[0].Value + "  , " + " Duration = " + nodee.Attributes[1].Value + " , " + " DurationValue = " + nodee.Attributes[2].Value + " , " + " Connections = " + nodee.Attributes[3].Value + " , " + " Price = " + nodee.Attributes[4].Value + " , " + " PriceValue = " + nodee.Attributes[5].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    clscnt = 0;
                                    //dt.Rows.Add("", "", "");
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
                                    dt.Rows.Add("", node.Attributes[0].Value, node.Attributes[1].Value, Pos,"CL");
                                    blkcnt = 0;
                                }
                                //else if(node.Attributes[0].Value.ToString() == "classicLinkSiteLinks" || node.Attributes[0].Value.ToString() == "classickLinkCarousel")
                                //{
                                //    Pos = Pos + 1;
                                //    dt.Rows.Add("", node.Attributes[1].Value, node.Attributes[2].Value, Pos);
                                //}
                                else if (node.Attributes[0].Value.ToString() == "classicLinkCarousel")
                                {
                                    Pos++;
                                    dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">", node.Attributes[1].Value, node.Attributes[2].Value, Pos,"CL");
                                    foreach (XmlNode nodee in node)
                                    {
                                        dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                    }
                                    dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                }
                                else if (node.Attributes[0].Value.ToString() == "classicLinkSiteLinks")
                                {
                                    Pos++;
                                    dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">", node.Attributes[1].Value, node.Attributes[2].Value, Pos,"CL");
                                    foreach (XmlNode nodee in node)
                                    {
                                        dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                    }
                                    dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                }
                                else
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
                                    //if (node.Attributes[0].Value.ToString() == "classicLinkSiteLinks")
                                    //{
                                    //    dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                    //    //dt.Rows.Add("", node.Attributes[1].Value, node.Attributes[2].Value);
                                    //    foreach (XmlNode nodee in node)
                                    //    {
                                    //        dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                    //    }
                                    //    dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    //}
                                    if (node.Attributes[0].Value.ToString() == "peopleAlsoSearch")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", "", nodee.Attributes[0].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    //if (node.Attributes[0].Value.ToString() == "classicLinkCarousel")
                                    //{
                                    //    dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                    //    //dt.Rows.Add("", node.Attributes[1].Value, node.Attributes[2].Value);
                                    //    foreach (XmlNode nodee in node)
                                    //    {
                                    //        dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                    //    }
                                    //    dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    //}
                                    if (node.Attributes[0].Value.ToString() == "sitesCarousel" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                            //dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value, " Source = " + nodee.Attributes[2].Value);
                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value + "  ----- " + " Source = " + nodee.Attributes[2].Value);

                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    if (node.Attributes[0].Value.ToString() == "peopleAlsoBuyFrom" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", nodee.Attributes[0].Value, nodee.Attributes[1].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    if (node.Attributes[0].Value.ToString() == "findResultsOn" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", nodee.Attributes[1].Value, " Title = " + "" + " ,  " + " source = " + nodee.Attributes[0].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    if (node.Attributes[0].Value.ToString() == "videoCard" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
                                    {
                                        dt.Rows.Add("", node.Attributes[1].Value);
                                    }
                                    if (node.Attributes[0].Value.ToString() == "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                           dt.Rows.Add("", nodee.Attributes[0].Value, "Title = " + nodee.Attributes[1].Value + " --- " + " Description = " + nodee.Attributes[2].Value + " --- " + " CardType = " + nodee.Attributes[3].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    if (node.Attributes[0].Value.ToString() == "dataset" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "hotelPack" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        foreach (XmlNode nodee in node)
                                        {
                                            dt.Rows.Add("", nodee.Attributes[0].Value, " Title = " + nodee.Attributes[1].Value + " --- " + " publishDate = " + nodee.Attributes[2].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    if (node.Attributes[0].Value.ToString() != "hotelPack" && node.Attributes[0].Value.ToString() != "flightPack" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
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
                                    if (node.Attributes[0].Value.ToString() == "hotelPack" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
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
                                            //else if (nodee.Attributes.Count == 5)
                                            //{
                                            //    //dt.Rows.Add("", nodee.Attributes[0].Value, " Rating= " + nodee.Attributes[1].Value + " --- " + " TotalReviews = " + nodee.Attributes[2].Value + " --- " + " AdditionalInfo = " + nodee.Attributes[3].Value + " --- " + " Title = " + nodee.Attributes[4].Value);
                                            //    if (nodee.Attributes[0].Name == "price")
                                            //    {
                                            //        dt.Rows.Add("", "", " Price = " + nodee.Attributes[0].Value + " --- " + " Pricevalue = " + nodee.Attributes[1].Value + " --- " + " AdditionalInfo = " + nodee.Attributes[2].Value + " --- " + " Title = " + nodee.Attributes[3].Value);
                                            //    }
                                            //    else if (nodee.Attributes[1].Name != "price")
                                            //    {
                                            //        dt.Rows.Add("", nodee.Attributes[0].Value, " Rating = " + nodee.Attributes[1].Value + " --- " + " TotalReviews = " + nodee.Attributes[2].Value + " --- " + " AdditionalInfo = " + nodee.Attributes[3].Value + " --- " + " Title = " + nodee.Attributes[4].Value);
                                            //    }
                                            //}
                                            else
                                            {
                                                dt.Rows.Add("", "", " Price = " + nodee.Attributes[0].Value + " , " + " PriceValue = " + nodee.Attributes[1].Value + " , " + " Rating = " + nodee.Attributes[2].Value + " , " + " TotalReviews = " + nodee.Attributes[3].Value + " , " + " AdditionalInfo = " + nodee.Attributes[4].Value + " , " + " Title = " + nodee.Attributes[5].Value);
                                            }
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    if (node.Attributes[0].Value.ToString() == "flightPack" && node.Attributes[0].Value.ToString() != "findResultsOn" && node.Attributes[0].Value.ToString() != "answerCard" && node.Attributes[0].Value.ToString() != "dataset" && node.Attributes[0].Value.ToString() != "aiOverview" && node.Attributes[0].Value.ToString() != "classickLinkCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoSearch" && node.Attributes[0].Value.ToString() != "classicLinkSiteLinks" && node.Attributes[0].Value.ToString() != "sitesCarousel" && node.Attributes[0].Value.ToString() != "peopleAlsoBuyFrom")
                                    {
                                        dt.Rows.Add("<" + node.Attributes[0].Value.ToString() + ">");
                                        if (node.Attributes.Count == 3)
                                        {
                                            dt.Rows.Add("", "", " Origin = " + node.Attributes[1].Value + " , " + " Destination= " + node.Attributes[2].Value);
                                        }
                                        foreach (XmlNode nodee in node)
                                        {
                                            //dt.Rows.Add("", "", " url= " + "" + " , " + " title= " + "" + "  ,  " + " airline= " + nodee.Attributes[1].Value + "  ,  " + " duration= " + nodee.Attributes[3].Value + " , " + " durationValue= " + nodee.Attributes[4].Value + " , " + " connections= " + nodee.Attributes[5].Value + " , " + " price= " + nodee.Attributes[6].Value + " , " + " priceValue= " + nodee.Attributes[7].Value);
                                            //dt.Rows.Add("", "", " airline= " + nodee.Attributes[0].Value + "  ,  " + " Title= " + nodee.Attributes[1].Value + " , " + " Duration= " + nodee.Attributes[2].Value + " , " + " DurationValue= " + nodee.Attributes[3].Value + " , " + " Connections= " + nodee.Attributes[4].Value + " , " + " Price= " + nodee.Attributes[5].Value + " , " + " PriceValue= " + nodee.Attributes[6].Value);
                                            dt.Rows.Add("", "", " Airline = " + nodee.Attributes[0].Value + "  , " + " Duration = " + nodee.Attributes[1].Value + " , " + " DurationValue = " + nodee.Attributes[2].Value + " , " + " Connections = " + nodee.Attributes[3].Value + " , " + " Price = " + nodee.Attributes[4].Value + " , " + " PriceValue = " + nodee.Attributes[5].Value);
                                        }
                                        dt.Rows.Add("</" + node.Attributes[0].Value.ToString() + ">", "", "");
                                    }
                                    clscnt = 0;
                                    //dt.Rows.Add("", "", "");
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
                gridviewjobid.DataSource = dt;
                gridviewjobid.DataBind();
            }
            catch (Exception ex)
            {
                string yourValue = "Data not available";
                string error = ex.Message;
                Response.Write(String.Format("<script>alert('Data not available.');</script>", yourValue));
            }
        }


        protected void gridviewjobid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                tc.Attributes["style"] = "border-right:1.5px solid black";
                tc.Attributes["style"] = "border-left:1.5px solid black";
                tc.Attributes["style"] = "border-top:1.5px solid black";
            }

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text.Replace("&lt;","").Replace("&gt;","").Replace("/","");
                //e.Row.Cells[i].ForeColor = Color.Blue;
            }
        }

        protected void btnjobid_Click(object sender, EventArgs e)
        {
            string url = "";
            lbloxykwd.Text = string.Empty;
            lbldevice.Text = string.Empty;
            lblseid1.Text = string.Empty;
            lblsename.Text = string.Empty;
            lbllocale.Text = string.Empty;
            lbldomain.Text = string.Empty;
            lblsename1.Text = string.Empty;
            lbljobiddate.Text = string.Empty; ;

            GetParams gp = new GetParams();

            try
            {
                ArrayList data = new ArrayList();
                ArrayList allparmas = new ArrayList();
                DataTable dt = Table();
                DataSet ds = new DataSet();
                string jobid = txtjobid.Text.Trim();
                string seid = "";
                string sename1 = "";
                int count = 0;
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
                string status = obj["status"].Value<string>();
                if (status == "done")
                {
                    oxydiv1.Visible = true;
                    lblalllinks.Text = "";
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
                    sename1 = sp.sename.ToString();


                    string device = obj["user_agent_type"].Value<string>();
                    string keyword = obj["query"].Value<string>();
                    string geol = obj["geo_location"].Value<string>();
                    string locale = obj["locale"].Value<string>();
                    string domain = obj["domain"].Value<string>();
                    string updatedate = obj["updated_at"].Value<string>();

                    lbloxykwd.Text = keyword;
                    lbldevice.Text = device;
                    lblseid1.Text = seid;
                    lblsename.Text = geol;
                    lbllocale.Text = locale;
                    lbldomain.Text = domain;
                    lblsename1.Text = sename1;
                    lbljobiddate.Text = updatedate;
                    lblalllinks.Visible = false;
                    lblalllinks1.Visible = false;

                    //string html = GetHtmlSource(jobid, seid, device);
                    int lan = gp.locale.IndexOf("-");
                    string lan1 = gp.locale.Remove(lan);
                    string s1 = gp.locale.Remove(0, 3);
                    string html = oxyresponse.GetJobidsource1(jobid, seid, device, out string uule);
                    GetJobid(jobid);
                    if (device == "desktop")
                        //url = "https://www.google." + sp.domain + "/search?q=" + keyword + "&gl=" + s1 + "&hl=" + lan1 + "&uule=" + sp.uule + "&num=100&aomd=1&safe=off&safe_search=0&gs_l=desktop&gws_rd=ssl,cr";                        
                        //url = "https://www.google." + gp.domain + "/search?q=" + keyword.Replace("'", "%27") + "&gl=" + s1 + "&hl=" + lan1 + "&uule=" + uule + "&num=100&aomd=1&safe=off&safe_search=0&gs_l=desktop&gws_rd=ssl,cr";
                        url = "https://www.google." + gp.domain + "/search?q=" + keyword.Replace("'", "%27").Replace("&", "%26") + "&gl=" + s1 + "&hl=" + lan1 + "&uule=" + uule + "&num=100&aomd=1&safe=off&safe_search=0";
                    else
                        url = "https://www.google." + gp.domain + "/search?q=" + keyword.Replace("'", "%27").Replace("&", "%26") + "&gl=" + s1 + "&hl=" + lan1 + "&uule=" + uule + "&num=100&glp=1&adtest=on&tci=g:2752&safe=images&safe=high&adtest-useragent=Mozilla/5.0 (iPhone; CPU iPhone OS 17_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) CriOS/128.0.6613.98 Mobile/15E148 Safari/604.1";


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup_File" + 2, "window.open('" + url.Replace("'", "%27") + "','Window_X')", true);

                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(html);
                    if (device == "desktop")
                    {
                        Desktop clsdesktop = new Desktop();
                        string res = clsdesktop.ProcessDocument(seid, keyword, doc, out count);
                        XmlDocument doc1 = new XmlDocument();
                        doc1.LoadXml(res);
                        DisplayDatainPage(seid, keyword, res, jobid, device);
                    }
                    else
                    {
                        iOS clsios = new iOS();
                        string res = clsios.ProcessDocument(seid, keyword, doc, out count);
                        XmlDocument doc1 = new XmlDocument();
                        doc1.LoadXml(res);
                        DisplayDatainPage(seid, keyword, res, jobid, device);
                    }
                }
                else
                {
                    lblalllinks.Text = "Oxylabs Jobid  is " + status + " : " + jobid;
                }
            }
            catch (Exception ex)
            {
                oxydiv1.Visible = false;
                if (ex.Message.Contains("Root element is missing.") || ex.Message.Contains("No block found.") || ex.Message.Contains("Object reference not set to an instance of an object."))
                {
                    lblalllinks1.Text = "No source found for provided jobid : " + txtjobid.Text;
                    lblalllinks1.Visible = true;
                }
                else if (string.IsNullOrEmpty("") || ex.Message.Contains("Error reading JObject from JsonReader. Path '', line 0, position 0."))
                {
                    lblalllinks.Text = "Provided jobid has expired or Status is faulted : " + txtjobid.Text;
                    lblalllinks.Visible = true;
                }
                gridviewjobid.DataSource = null;
                gridviewjobid.DataBind();
            }
        }
    }
}