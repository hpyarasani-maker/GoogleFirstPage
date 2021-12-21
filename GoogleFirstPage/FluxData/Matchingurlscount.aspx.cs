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
    public partial class Matchingurlscount : System.Web.UI.Page
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["Fluxresults"].ToString());

        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //gvalllinks.Columns.Clear();
                calender.EndDate = DateTime.Now;
                txtDate.Attributes.Add("ReadOnly", "ReadOnly");
                txtDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }
        }

        protected void btnfluxmatching_Click(object sender, EventArgs e)
        {
            gvmatching.Visible = true;
            this.GetDataFromDB();
        }

        public void GetDataFromDB()
        {
            string date = txtDate.Text;
            cmd = new SqlCommand("[UI_GetMatchingurlscount=0]", con1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = date;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);

            lbl1.Text = "Data Not Available";
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvmatching.DataSource = ds;
                gvmatching.DataBind();
                lbl1.Text = "";
            }
            else
            {
                gvmatching.DataSource = null;
                gvmatching.DataBind();
            }
        }
        protected void gvmatching_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
        }

        protected void gvmatching_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvmatching.PageIndex = e.NewPageIndex;
            this.GetDataFromDB();
        }
    }
}