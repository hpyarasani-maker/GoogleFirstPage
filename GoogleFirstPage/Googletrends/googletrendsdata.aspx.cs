using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace GoogleFirstPage.Googletrends
{
    public partial class keywordsdata : System.Web.UI.Page
    {

        string searchres = string.Empty;

        TrendsData trends = new TrendsData();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtstartdate.Attributes.Add("ReadOnly", "ReadOnly");
                txtstartdate.Text = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                //stdate.StartDate = DateTime.Now.AddYears(-1);
                txtenddate.Attributes.Add("ReadOnly", "ReadOnly");
                txtenddate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                endt.EndDate = DateTime.Now;
            }
            Label6.Visible = false;
            Label7.Visible = false;
            Label8.Visible = false;
            Label9.Visible = false;
            btnCSV.Enabled = false;
        }

        protected async void btngoogletrends_Click(object sender, EventArgs e)
        {
            try
            {
                string locations = ddllocation.SelectedItem.ToString();
                string kid = ddllocation.SelectedValue.ToString();
                if (locations != null)
                {
                    string res = await trends.keywords_data_trends_explore_live(txtkeyword.Text, txtstartdate.Text, txtenddate.Text, ddllocation.SelectedItem.Text); //.Result;
                    searchres = await trends.GetSearchVolumeResponse(txtkeyword.Text, ddllocation.SelectedItem.Text);
                    ProcessData(res, kid, locations);
                }
                btnCSV.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ProcessData(string result, string seid, string location)
        {
            try
            {
                JObject jo = JObject.Parse(result);
                var tasks = from p in jo["tasks"] select p;
                var res = tasks.FirstOrDefault()["result"];
                var items = res.FirstOrDefault()["items"];

                foreach (var item in items)
                {
                    var title = item["title"].Value<string>();
                    if (title == "Interest over time")
                    {
                        var iot = item["data"];
                        SaveOverTime(iot, seid, location);
                    }
                    else if (title == "Interest by subregion")
                    {
                        var ibs = item["data"];
                        SaveBySubregion(ibs, seid, location);
                    }
                    else if (title == "Related topics")
                    {
                        var rt = item["data"];
                        SaveRelatedTopics(rt, seid, location);
                    }
                    else if (title == "Related queries")
                    {
                        var rq = item["data"];
                        SaveRelatedQueries(rq, seid, location);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void SaveOverTime(JToken iot, string kid, string location)
        {
            try
            {
                DataTable dt1 = trends.GetOverTime(iot, searchres);

                if (dt1.Rows.Count > 0)
                {
                    int total = trends.GetSearchVolume(searchres).Sum(x => Convert.ToInt32(x));
                    CreateXml(dt1, total, txtkeyword.Text, location, txtstartdate.Text, txtenddate.Text);

                    gvinterestot.DataSource = dt1;
                    gvinterestot.DataBind();

                    gvinterestot.FooterRow.Cells[1].Text = "Total Volume = ";
                    gvinterestot.FooterRow.Cells[1].Font.Bold = true;
                    gvinterestot.FooterRow.Cells[2].Text = total.ToString();
                }
                else
                {
                    gvinterestot.DataSource = null;
                    gvinterestot.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveBySubregion(JToken ibs, string kid, string location)
        {
            try
            {
                DataTable dt2 = trends.GetBySubregion(ibs);

                if (dt2.Rows.Count > 0)
                {
                    Label7.Visible = true;
                    gvsubregion.DataSource = dt2;
                    gvsubregion.DataBind();
                }
                else
                {
                    gvsubregion.DataSource = null;
                    gvsubregion.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveRelatedTopics(JToken rt, string kid, string location)
        {
            try
            {
                DataTable dt3 = trends.GetRelatedTopics(rt);

                if (dt3.Rows.Count > 0)
                {
                    Label8.Visible = true;
                    gvrelatedtopics.DataSource = dt3;
                    gvrelatedtopics.DataBind();
                }
                else
                {
                    gvrelatedtopics.DataSource = null;
                    gvrelatedtopics.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveRelatedQueries(JToken rq, string kid, string location)
        {
            try
            {
                DataTable dt4 = trends.GetRelatedQueries(rq);

                if (dt4.Rows.Count > 0)
                {
                    Label9.Visible = true;
                    gvrelatedqueries.DataSource = dt4;
                    gvrelatedqueries.DataBind();
                }
                else
                {
                    gvrelatedqueries.DataSource = null;
                    gvrelatedqueries.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void gvinterestot_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        DateTime dtRow = DateTime.ParseExact(e.Row.Cells[1].Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //        string curDate = DateTime.Now.ToString("yyyy-MM-dd");
            //        int result = DateTime.Compare(dtRow, DateTime.Parse(curDate));


            //        if (result < 0)
            //        {
            //            e.Row.Cells[1].BackColor = Color.Red;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void btnCSV_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                string a = "GoogletrendsData" + "_" + txtkeyword.Text + "_" + ddllocation.SelectedItem.Text + ".csv";
                Response.AddHeader("content-disposition", "attachment;filename=" + a);
                Response.Charset = "";
                Response.ContentType = "text/csv";

                gvinterestot.AllowPaging = false;

                StringBuilder sb = new StringBuilder();

                foreach (TableCell cell in gvinterestot.HeaderRow.Cells)
                {
                    sb.Append(cell.Text + ',').Replace("&nbsp;", " ");
                }
                sb.Append("\r\n");

                foreach (GridViewRow row in gvinterestot.Rows)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        sb.Append(cell.Text + ',').Replace("&nbsp;", " ");
                    }
                    sb.Append("\r\n");
                }

                foreach (TableCell cell in gvinterestot.FooterRow.Cells)
                {
                    sb.Append(cell.Text + ',').Replace("&nbsp;", " ");
                }
                sb.Append("\r\n");

                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CreateXml(DataTable dt, int total, string keyword, string location, string prevYear, string curYear)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            builder.AppendLine("<GoogletrendsOverTime>");
            builder.AppendLine("<OverTimes keyword =\"" + keyword + "\" country=\"" + location + "\" previousYear=\"" + prevYear + "\" currentYear=\"" + curYear + "\" >");
            foreach (DataRow row in dt.Rows)
            {
                builder.AppendLine("<OverTime>");
                foreach (DataColumn col in dt.Columns)
                {
                    if (string.IsNullOrEmpty(row[col].ToString()))
                        builder.AppendLine("<" + col.ColumnName + " />");
                    else
                        builder.AppendLine("<" + col.ColumnName + ">" + row[col].ToString() + "</" + col.ColumnName + ">");
                }
                builder.AppendLine("</OverTime>");
            }
            builder.AppendLine("</OverTimes>");
            builder.AppendLine("<TotalVolume>" + total + "</TotalVolume>");
            builder.AppendLine("</GoogletrendsOverTime>");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(builder.ToString());
            doc.Save("C:\\inetpub\\wwwroot\\trends\\TrendsOverTime.xml");
        }
    }
}