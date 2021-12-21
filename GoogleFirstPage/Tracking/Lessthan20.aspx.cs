using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GoogleFirstPage.Oxylabs
{
    public partial class Lessthan20threads : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;
        string Date;

        string connection = ConfigurationManager.ConnectionStrings["allelements"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
                Date = HttpUtility.UrlDecode(Request.QueryString["Date"]);
                GetDataFromDB();
        }

        public void GetDataFromDB()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    cmd = new SqlCommand("[GetLessthen20Threads]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = Date;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    gvlessthan20.DataSource = ds;
                    gvlessthan20.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvlessthan20.DataSource = ds;
                        gvlessthan20.DataBind();
                        //lblalllinks.Text = "Elements appeared for No. of keywords in " + ddltype.SelectedItem.Text;
                    }
                    else
                    {
                        gvlessthan20.DataSource = null;
                        gvlessthan20.DataBind();
                        lblalllinks.Text = "Data Not Available";
                    }
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        protected void gvlessthan20_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
        }

        protected void gvlessthan20_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvlessthan20.PageIndex = e.NewPageIndex;
            this.GetDataFromDB();
        }

        

        public void ExportCSV()
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("[GetLessthen20Threads_D]"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = Date;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        using (DataTable dt = new DataTable())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
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
                            string a1 = "Lessthan20" + "_" + Date + ".csv";
                            Response.AddHeader("content-disposition", "attachment;filename=" + a1);
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

        protected void lnklessthan20_Click(object sender, EventArgs e)
        {
            ExportCSV();
        }
    }
}