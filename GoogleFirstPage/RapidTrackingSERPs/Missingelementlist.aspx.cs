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
    public partial class Missingelementlist : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;

        string connection = ConfigurationManager.ConnectionStrings["allelements"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblalllinks.Visible = false;
            //calendar.StartDate = DateTime.Now.AddDays(-1);
            calendar.EndDate = DateTime.Now;
            if(!IsPostBack)
            {
                //calendar.StartDate = DateTime.Now.AddDays(-1);
                calendar.EndDate = DateTime.Now;
                txtDate.Attributes.Add("ReadOnly", "ReadOnly");
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        public void BindData()
        {
            try
            {
                using (var con = new SqlConnection(connection))
                {
                    string date = txtDate.Text;
                    cmd = new SqlCommand("[UI_GetElementsListDiff]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", SqlDbType.NVarChar).Value = ddlse.SelectedValue;
                    cmd.Parameters.AddWithValue("@Date", SqlDbType.Date).Value = date;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    //gvelements.DataSource = ds;
                    //gvelements.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvmissingelements.DataSource = ds;
                        gvmissingelements.DataBind();
                        lblalllinks.Text = "Missing Elements appeared for No. of keywords in : " + ddlse.SelectedItem.Text;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count < 0 || ds.Tables[0].Rows.Count == 0)
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
                lblalllinks.Text = ex.StackTrace.ToString();
            }

        }
        protected void btnmisselementslist_Click(object sender, EventArgs e)
        {
            lblalllinks.Visible = true;
            BindData();
        }

        protected void gvmissingelements_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            lblalllinks.Visible = true;
            gvmissingelements.PageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void gvmissingelements_RowDataBound(object sender, GridViewRowEventArgs e)
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