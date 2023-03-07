using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.Googletrends
{
    public partial class SearchVolume : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Trackingdata"].ToString();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!Page.IsPostBack)
            {
                GetCountryList();
            }
        }

        protected void GetCountryList()
        {
           
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd;
                SqlDataAdapter da;
                DataSet ds;
                cmd = new SqlCommand("[GetSearchEngines]", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add("@keyword", SqlDbType.NVarChar).Value = name.Replace(" ","%20");
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                country_list.DataTextField = "name";
                country_list.DataValueField = "seid";
                country_list.DataSource = dt;
                country_list.DataBind();
                //searchEngines.Items.Insert(0, "----Select----");
                country_list.SelectedItem.Value.ToString();
                //ddlkeyword.SelectedValue = "2";
                country_list.SelectedIndex = 0;
                ds = new DataSet();
                da.Fill(ds);
                
            }
        }

        protected void sub_btn_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"].ToString();

            string seid = country_list.SelectedValue.ToString();
            name_lbl.Text = Request.QueryString["name"].ToString();
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd;
                SqlDataAdapter da;
                DataSet ds;
                cmd = new SqlCommand("[GetSearchvolumeData]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@kid", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@seid", SqlDbType.Int).Value = seid;
                //cmd.Parameters.Add("@keyword", SqlDbType.NVarChar).Value = name.Replace(" ","%20");
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvdata.DataSource = ds;
                    gvdata.DataBind();
                }
                else
                {
                    gvdata.DataSource = null;
                    gvdata.DataBind();
                }
            }
        }
    }
}