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
    public partial class xmlchanges : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["xmltracker"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {


            //string uid = Request.QueryString["uid"].ToString();
            //string name = Request.QueryString["name"].ToString();
            //string date = Request.QueryString["date"].ToString();
            //string kid = Request.QueryString["kid"].ToString();

            string previousdate = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");

            if(!IsPostBack)
            {
                //calendar.StartDate = DateTime.Now.AddDays(-1);
                calendar.EndDate = DateTime.Now;
                txtdate.Attributes.Add("ReadOnly", "ReadOnly");
                txtdate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                
            }

        }

        protected void btnchanges_Click(object sender, EventArgs e)
        {
            string url = Request.QueryString["url"].ToString();

            SqlConnection con = new SqlConnection(connection);
            //string strQuerry = "exec [dbo].[GetXMLData] " + uid + ",'" + date + "','" + name + "'";
            string strQuerry = "exec [dbo].[GetXMLData] " + url + ",'" + txtdate.Text + "'";

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