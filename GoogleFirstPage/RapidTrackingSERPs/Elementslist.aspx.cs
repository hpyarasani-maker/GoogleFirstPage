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
    public partial class ElementsList : System.Web.UI.Page
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
                    cmd = new SqlCommand("[UI_GetElementsList]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", SqlDbType.NVarChar).Value = ddltype.SelectedValue;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    gvelements.DataSource = ds;
                    gvelements.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvelements.DataSource = ds;
                        gvelements.DataBind();
                        lblalllinks.Text = "Elements appeared for No. of keywords in : " + ddltype.SelectedItem.Text;
                    }
                    else
                    {
                        if(ds.Tables[0].Rows.Count < 0 || ds.Tables[0].Rows.Count == 0)
                        {
                            lblalllinks.Text = "Data Not Available";
                        }
                        //gvelements.DataSource = null;
                        //gvelements.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }

        protected void btnelementslist_Click(object sender, EventArgs e)
        {
            lblalllinks.Visible = true;
            BindData();
        }

        protected void gvelements_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            lblalllinks.Visible = true;
            gvelements.PageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void gvelements_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }
}