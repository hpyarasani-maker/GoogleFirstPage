using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;

namespace GoogleFirstPage.FluxData
{
    public partial class Fluxresults : System.Web.UI.Page
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Fluxresults"].ToString());
        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["allsearchengines"].ToString());
        
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader rdr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                calender.EndDate = DateTime.Now.AddDays(-1);
                txtDate.Attributes.Add("ReadOnly", "ReadOnly");
                txtDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                cmd = new SqlCommand("GetSearchEngines", con2);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                SEList.DataTextField = "name";
                SEList.DataValueField = "seid";
                SEList.DataSource = dt;
                SEList.DataBind();
                SEList.SelectedItem.Value.ToString();
                SEList.SelectedIndex = 0;
            }
        }

        protected void btnfluxresults_Click(object sender, EventArgs e)
        {
            gvflux.Visible = true;
            BindDataToGrid();
        }

        public void BindDataToGrid()
        {
            try
            {
                DataSet1TableAdapters.UI_GetFluxresultsdata__50TableAdapter ds = new DataSet1TableAdapters.UI_GetFluxresultsdata__50TableAdapter();
                DataSet1._UI_GetFluxresultsdata__50DataTable fluxTable = new DataSet1._UI_GetFluxresultsdata__50DataTable();
                ds.Fill(fluxTable, Convert.ToInt32(SEList.SelectedItem.Value), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlfluxaverage.SelectedItem.Value));
                Session["FluxResults"] = fluxTable;

                if (fluxTable.Rows.Count == 0)
                {
                    //lblalllinks.Text = "This seid not yet tracked. Please try another seid...";
                    lblalllinks.Visible = true;
                }
                else if (fluxTable.Rows.Count > 0)
                {
                    lblalllinks.Text = "";
                    lblalllinks.Visible = false;
                }

                gvflux.DataSource = ds.GetData(Convert.ToInt32(SEList.SelectedItem.Value), Convert.ToDateTime(txtDate.Text), Convert.ToInt32(ddlfluxaverage.SelectedItem.Value));
                gvflux.DataBind();


            }
            catch (SqlException ex)
            {
                lbl.Text = ex.Message.ToString();
            }
        }
        protected void gvflux_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
        }

        protected void gvflux_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvflux.PageIndex = e.NewPageIndex;
            this.BindDataToGrid();
        }
    }
}