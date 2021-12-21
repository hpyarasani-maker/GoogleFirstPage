using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.TrendingTwoHoursResultsFromDB
{
    public partial class TrendingworkspaceLive : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;
        string connection = ConfigurationManager.ConnectionStrings["WorkspaceLive"].ToString();
        string KName = string.Empty;
        string hour;
        string hour1;
        string date = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            calendar.StartDate = DateTime.Now.AddDays(-1);
            calendar.EndDate = DateTime.Now;
            //lblnodata.Visible = false;
            if (!IsPostBack)
            {
                DisplayRecord();
                calendar.StartDate = DateTime.Now.AddDays(-1);
                calendar.EndDate = DateTime.Now;
                txtDate.Attributes.Add("ReadOnly", "ReadOnly");
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");// AddDays(-1).ToString("yyyy-MM-dd");
                using (SqlConnection con = new SqlConnection(connection))
                {
                    lblnodata.Text = "";
                    cmd = new SqlCommand("UI_GetKeywords", con);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    ddlkeywordLive.DataTextField = "KName";
                    ddlkeywordLive.DataSource = dt;
                    ddlkeywordLive.DataBind();
                    ddlkeywordLive.Items.Insert(0, "----Select----");
                    ddlkeywordLive.SelectedIndex = 1;
                    string kname = ddlkeywordLive.SelectedItem.Text;
                    cmd = new SqlCommand("UI_GetSearchEngines", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@KName", SqlDbType.NVarChar).Value = kname;
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    ddlseid.DataTextField = "name";
                    ddlseid.DataValueField = "seid";
                    ddlseid.DataSource = dt;
                    ddlseid.DataBind();
                    ddlseid.Items.Insert(0, "----Select----");
                    ddlseid.SelectedIndex = 1;
                }
            }

        }

        public DataTable DisplayRecord()
        {
            SqlDataAdapter Adp = new SqlDataAdapter("select keyword from keywords", connection);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            lbl1.Text = Dt.Rows.Count.ToString();
            return Dt;
        }
        protected void btnworklive_Click(object sender, EventArgs e)
        {
            lblnodata.Text = "";
            string keyword1 = ddlkeywordLive.SelectedItem.Text;
            string seid1 = ddlseid.SelectedItem.Value;
            string date1 = txtDate.Text;
            hour1 = ddlhr.SelectedItem.Value;
            string strQuerry = "exec [dbo].[UI_GetXMLData] '" + date1 + "',N'" + keyword1.Replace("'", "''") + "'," + seid1 + "," + hour1 + "";


            SqlDataAdapter Adp = new SqlDataAdapter(strQuerry, connection);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            if (Dt.Rows.Count == 0)
            {
                lblnodata.Text = "No data to display";
            }
            else
            {
                string keyword = ddlkeywordLive.SelectedItem.Text;
                string seid = ddlseid.SelectedItem.Value;
                string date = txtDate.Text;
                hour = ddlhr.SelectedItem.Value;
                string url = "Viewxml.aspx?date=" + date + "&keyword=" + keyword.Replace("'", "%27") + "&seid=" + seid + "&hour=" + hour;    //Replace("'", "''")
                ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "');", true);
            }
        }

        protected void ddlkeywordLive_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblnodata.Text = "";
            string kname = ddlkeywordLive.SelectedItem.Text;
            using (SqlConnection con = new SqlConnection(connection))
            {
                cmd = new SqlCommand("UI_GetSearchEngines", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@KName", SqlDbType.NVarChar).Value = kname;
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                ddlseid.DataTextField = "name";
                ddlseid.DataValueField = "seid";
                ddlseid.DataSource = dt;
                ddlseid.DataBind();
                ddlseid.Items.Insert(0, "----Select----");
                ddlseid.SelectedIndex = 1;
            }
        }
    }
}