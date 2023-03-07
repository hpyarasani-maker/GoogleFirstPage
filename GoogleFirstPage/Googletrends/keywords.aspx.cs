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
    public partial class keywords : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["Trackingdata"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            GetClientKeywords();
        }

      
        public void GetClientKeywords()
        {
            string strSql = "exec [dbo].[GetKeywords]";

            SqlConnection objCon = null;
            try
            {
                objCon = new SqlConnection(connection);
                objCon.Open();
                SqlCommand objCmd = new SqlCommand(strSql, objCon);
                gvserchvolme.DataSource = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                gvserchvolme.DataBind();

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

    }
}