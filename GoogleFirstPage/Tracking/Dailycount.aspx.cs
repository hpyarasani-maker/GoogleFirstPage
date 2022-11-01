using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GoogleFirstPage.OxylabsRequestCount
{
    public partial class Dailycount : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;
        string Date;

        string connection = ConfigurationManager.ConnectionStrings["allelements"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            GetDataFromDB();
            GetDataFromDBForMonthCount();
        }

        public void GetDataFromDB()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    cmd = new SqlCommand("[UI_GetOxylabsRequestCount]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    gvrequestcount.DataSource = ds;
                    gvrequestcount.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvrequestcount.DataSource = ds;
                        gvrequestcount.DataBind();
                        //lblalllinks.Text = "Elements appeared for No. of keywords in " + ddltype.SelectedItem.Text;
                    }
                    else
                    {
                        gvrequestcount.DataSource = null;
                        gvrequestcount.DataBind();
                        lblalllinks.Text = "Data Not Available";
                    }
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }


        public void GetDataFromDBForMonthCount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    cmd = new SqlCommand("[UI_GetOxyMonthCount]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    grdmonth.DataSource = ds;
                    grdmonth.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdmonth.DataSource = ds;
                        grdmonth.DataBind();
                        //lblalllinks.Text = "Elements appeared for No. of keywords in " + ddltype.SelectedItem.Text;
                    }
                    else
                    {
                        grdmonth.DataSource = null;
                        grdmonth.DataBind();
                        lblalllinks.Text = "Data Not Available";
                    }
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }


        protected void gvrequestcount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvrequestcount.PageIndex = e.NewPageIndex;
            GetDataFromDB();

        }

        protected void gvrequestcount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Date = gvrequestcount.DataKeys[e.Row.RowIndex].Value.ToString();

                    //int count = Convert.ToInt32(((HyperLink)e.Row.FindControl("lbllessthan20")).Text);
                    //if (count > 0)
                    //{
                    //    HyperLink lbllessthan20 = (HyperLink)e.Row.FindControl("lbllessthan20");
                    //    lbllessthan20.Text = count.ToString();
                    //}
                    //else
                    //{
                    //    HyperLink lbllessthan20 = (HyperLink)e.Row.FindControl("lbllessthan20");
                    //    lbllessthan20.Text = count.ToString();
                    //    lbllessthan20.Enabled = false;
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void ExportCSV()
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("select [Date],[Pi_DailyCount],[Google],[Bing],[Yahoo],[Yandex],[Sogou],[PriceSearcher],[Baidu],[Haosou],[Naver],[Sending],[Receiving],[Trending],[Re-TrackedKeywords],[Error_Keywords],[Lessthen20],[TotalSending],[Message] from [TrackingCountTable] order by date desc"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            
                            string csv = string.Empty;

                            foreach (DataColumn column in dt.Columns)
                            {
                                
                                csv += column.ColumnName + ',';
                            }

                            
                            csv += "\r\n";

                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    
                                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                }
                                csv += "\r\n";
                            }
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment;filename=TrackingCount.csv");
                            Response.Charset = "";
                            Response.ContentType = "application/text";
                            Response.Output.Write(csv);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
        }

        protected void lnkdownloadreport_Click(object sender, EventArgs e)
        {
            ExportCSV();
        }
    }
}