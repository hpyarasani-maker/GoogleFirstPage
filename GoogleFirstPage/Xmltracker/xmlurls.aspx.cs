using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
namespace GoogleFirstPage.Xmltracker
{
    public partial class xmlurls : System.Web.UI.Page
    {

        string connection = ConfigurationManager.ConnectionStrings["xmltracker"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            string kid = Request.QueryString["kid"].ToString();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd;
                SqlDataAdapter da;
                DataSet ds;
                cmd = new SqlCommand("[GetKeywordUrls]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@kid", SqlDbType.Int).Value = kid;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvxmlurls.DataSource = ds;
                    gvxmlurls.DataBind();
                }
                else
                {
                    gvxmlurls.DataSource = null;
                    gvxmlurls.DataBind();
                }
            }
        }
    }
}