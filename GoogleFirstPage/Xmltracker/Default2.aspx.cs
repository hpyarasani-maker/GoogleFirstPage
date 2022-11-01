using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.Xmltracker
{
    public partial class Default2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connection = ConfigurationManager.ConnectionStrings["xmltracker"].ToString();

            string uid = Request.QueryString["uid"].ToString();
            string date = Request.QueryString["date"].ToString();


            SqlConnection con = new SqlConnection(connection);
            string strQuerry = "Select xmldata from xmlsource Where uid=" + uid + " and convert(varchar(10),date,127)='" +date  + "'";
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
            catch
            {
                Response.Write("Got an error!");
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
