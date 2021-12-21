using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.TrendingTwoHoursResultsFromDB
{
    public partial class Viewxml : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["WorkspaceLive"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WorkspaceLive"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            string seid = Request.QueryString["seid"].ToString();
            string keyword = Request.QueryString["keyword"].ToString();
            string date = Request.QueryString["date"].ToString();
            string hour = Request.QueryString["hour"].ToString();
            string strQuerry = "exec [dbo].[UI_GetXMLData] '" + date + "',N'" + keyword.Replace("'", "''") + "'," + seid + "," + hour + "";

            SqlCommand comm = new SqlCommand(strQuerry, con);
            try
            {
                con.Open();
                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    Response.ContentType = "text/xml";
                    Response.Write(dr.GetValue(0).ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
            finally
            {
                comm.Dispose();
                con.Close();
                con.Dispose();
            }
        }
    }
}