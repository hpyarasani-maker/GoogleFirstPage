using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GoogleFirstPage
{
    public partial class Amazontrackingweekly : System.Web.UI.Page
    {
        static DataTable dt1;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;
        string connection = ConfigurationManager.ConnectionStrings["AmazonTracking"].ToString();
        string KName = string.Empty;
        int seid = 0;
        string date = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calendar.EndDate = DateTime.Now;
                txtDate.Attributes.Add("ReadOnly", "ReadOnly");
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");// AddDays(-1).ToString("yyyy-MM-dd");
                using (SqlConnection con = new SqlConnection(connection))
                {
                    //seid = Convert.ToInt32(ddlseid.SelectedValue);
                    cmd = new SqlCommand("UI_GetAmazonTrackedKeywords", con);
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    ddlkeyword.DataTextField = "KName";
                    //ddlseid.DataValueField = "seid";
                    ddlkeyword.DataSource = dt;
                    ddlkeyword.DataBind();
                    ddlkeyword.Items.Insert(0, "----Select----"); //UI_GetAmazonSeid
                    if (ddlkeyword.Items.Count > 1)
                    {
                        cmd = new SqlCommand("UI_GetAmazonSeid", con);
                        da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        ddlseid.DataTextField = "Name";
                        ddlseid.DataValueField = "Seid";
                        ddlseid.DataSource = dt;
                        ddlseid.DataBind();
                        ddlseid.Items.Insert(0, "----Select----");
                    }
                }
            }
        }

        protected void btnclickwkly_Click(object sender, EventArgs e)
        {
            DataSet ds1 = new DataSet();
            seid = Convert.ToInt32(ddlseid.SelectedValue);
            KName = ddlkeyword.SelectedItem.ToString();
            date = txtDate.Text;
            if (seid > 0 && !String.IsNullOrEmpty(KName) && !String.IsNullOrEmpty(date))
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    cmd = new SqlCommand("UI_GetUrlDataweekly", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@seid", SqlDbType.Int).Value = seid;
                    cmd.Parameters.AddWithValue("@KName", SqlDbType.NVarChar).Value = KName;
                    cmd.Parameters.AddWithValue("@date", SqlDbType.VarChar).Value = date;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);

                    //lbl2.Text = "Search Engine : " + ddlseid.SelectedItem.Text.ToString() + "<br/>" + "Keyword : " + ddlkeyword.SelectedItem.ToString() + "<br/>" + "No.of URLs : " + ds.Tables[0].Rows.Count.ToString();

                    lbl1.Text = "Data Not Available";
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvLocalData.DataSource = ds;
                        gvLocalData.DataBind();
                        lbl1.Text = "";
                    }
                    else
                    {
                        gvLocalData.DataSource = null;
                        gvLocalData.DataBind();

                    }
                    // location=ddlLocation.SelectedItem.ToString();
                    //lbl.Visible = true;
                    //lbl.Text = keyWord + "&nbsp;" + location;


                }
            }
        }

        protected void ddlseid_SelectedIndexChanged(object sender, EventArgs e)
        {
            seid = Convert.ToInt32(ddlseid.SelectedValue);
            //cid = Convert.ToInt32(ddlclient.SelectedValue);
            using (SqlConnection con = new SqlConnection(connection))
            {
                cmd = new SqlCommand("[UI_Keywordsamazontracking]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@seid", SqlDbType.Int).Value = seid;
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                // ddlkeyword.DataTextField = "keywords";
                // ddlkeyword.DataValueField = "seid";
                ddlkeyword.DataSource = dt;
                ddlkeyword.DataBind();
                ddlkeyword.Items.Insert(0, "----Select----");

            }

        }

        protected void gvLocalData_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}