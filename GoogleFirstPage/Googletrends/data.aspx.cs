using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.Googletrends
{
    public partial class data : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Trackingdata"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"].ToString();
            string name = Request.QueryString["name"].ToString();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd;
                SqlDataAdapter da;
                DataSet ds;
                cmd = new SqlCommand("[GetSearchvolumeData]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@kid", SqlDbType.Int).Value = id; 
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