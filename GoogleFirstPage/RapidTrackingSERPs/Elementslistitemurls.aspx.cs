using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GoogleFirstPage.RapidTrackingSERPs
{
    public partial class Elementlisturls : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;

        string connection = ConfigurationManager.ConnectionStrings["allelements"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblalllinks.Visible = false;
        }


        public void BindData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    cmd = new SqlCommand("[UI_GetElementsListUrls]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", SqlDbType.NVarChar).Value = ddltype1.SelectedValue;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    gvelementslisturls.DataSource = ds;
                    gvelementslisturls.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvelementslisturls.DataSource = ds;
                        gvelementslisturls.DataBind();
                        lblalllinks.Text = "Item Urls appeared for No. of keywords in : " + ddltype1.SelectedItem.Text;
                    }
                    else
                    {
                        gvelementslisturls.DataSource = null;
                        gvelementslisturls.DataBind();
                        lblalllinks.Text = "Data Not Available";
                    }
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }
        protected void btnelementslisturls_Click(object sender, EventArgs e)
        {
            lblalllinks.Visible = true;
            BindData();
        }

        protected void gvelementslisturls_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                tc.Attributes["style"] = "border-right:1px solid black";
            }
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
        }

        protected void gvelementslisturls_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            lblalllinks.Visible = true;
            gvelementslisturls.PageIndex = e.NewPageIndex;
            this.BindData();
        }
    }
}