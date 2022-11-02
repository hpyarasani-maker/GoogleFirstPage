using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace GoogleFirstPage.Xmltracker
{
    public partial class keywords : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["xmltracker"].ToString();

       
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            //{
            //    using (SqlConnection con = new SqlConnection(connection))
            //    {
            //        SqlCommand cmd = new SqlCommand("[GetClients]", con);
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        DataTable dt = new DataTable();
            //        da.Fill(dt);
            //        ddlclient.DataTextField = "name";
            //        ddlclient.DataSource = dt;
            //        ddlclient.DataBind();
            //        ddlclient.Items.Insert(0, "---------------Select Client---------------");
            //        ddlclient.SelectedIndex = 1;
            //    }
            //}
        }

        public void GetClientKeywords()
        {
            string strSql = "exec [dbo].[GetClientsKeywords]";
            SqlConnection objCon = null;
            try
            {
                objCon = new SqlConnection(connection);
                objCon.Open();
                SqlCommand objCmd = new SqlCommand(strSql, objCon);
                gvurls.DataSource = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                gvurls.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                if (objCon.State == ConnectionState.Open) objCon.Close();
            }
        }

        protected void btnclient_Click(object sender, EventArgs e)
        {
            GetClientKeywords();
            
        }

    }
}