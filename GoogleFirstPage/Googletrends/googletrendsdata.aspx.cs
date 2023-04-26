using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.Googletrends
{
    public partial class keywordsdata : System.Web.UI.Page
    {
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
                //string kid = ddllocation.SelectedValue.ToString();
                if (locations != null)
                {
                    await trends.ProcessData(txtkeyword.Text, locations);
                    SaveOverTime();
                    SaveBySubregion();
                    SaveRelatedTopics();
                    SaveRelatedQueries();
                }
                btnCSV.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }               
        
        public void SaveOverTime() 
        {
            try
            {
                DataTable dt1 = trends.GetOverTime(); 

                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    gvinterestot.DataSource = dt1;
                    gvinterestot.DataBind();

                    gvinterestot.FooterRow.Cells[1].Text = "Total Volume = ";
                    gvinterestot.FooterRow.Cells[1].Font.Bold = true;
                    gvinterestot.FooterRow.Cells[2].Text = trends.iot_total.ToString();
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

        public void SaveBySubregion() 
        {
            try
            {
                DataTable dt2 = trends.GetBySubregion();

                if (dt2 != null && dt2.Rows.Count > 0)
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

        public void SaveRelatedTopics() 
        {
            try
            {
                DataTable dt3 = trends.GetRelatedTopics();

                if (dt3 != null && dt3.Rows.Count > 0)
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

        public void SaveRelatedQueries()
        {
            try
            {
                DataTable dt4 = trends.GetRelatedQueries();

                if (dt4 != null && dt4.Rows.Count > 0)
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

       
    }
}