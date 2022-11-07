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
            lblmessage.Text = "";
            if (!Page.IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    SqlCommand cmd = new SqlCommand("[GetClients]", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddlclient.DataTextField = "name";
                    ddlclient.DataValueField = "id";
                    ddlclient.DataSource = dt;
                    //ddlclient.Items.Insert(0, "---Select any Client---");
                    //ddlclient.SelectedIndex = 0;
                    ddlclient.DataBind();
                }
                
            }
        }

        public void GetClientKeywords()
        {
            //string strSql = "exec [dbo].[GetClientsKeywords]";
            string clientid = ddlclient.SelectedValue;
            string strSql = "exec [dbo].[GetClKeywords]'" + clientid + "'";

            SqlConnection objCon = null;
            try
            {
                objCon = new SqlConnection(connection);
                objCon.Open();
                SqlCommand objCmd = new SqlCommand(strSql, objCon);
                gvkeywords.DataSource = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                gvkeywords.DataBind();

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
            lblmessage.Text = "";
            gvsearch.DataSource = null;
            gvsearch.DataBind();
            GetClientKeywords();
        }

     
        protected void btnsearchkwd_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(txtsearchkwds.Text))
            {
                gvkeywords.DataSource = null;
                gvkeywords.DataBind();
                string strSql = "exec [dbo].[GetSearchKeyword]'" + txtsearchkwds.Text + "'";
                SqlConnection objCon = null;
                try
                {
                    objCon = new SqlConnection(connection);
                    objCon.Open();
                    SqlCommand objCmd = new SqlCommand(strSql, objCon);
                    gvsearch.DataSource = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    gvsearch.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else
            {
                lblmessage.Text = "please enter keyword";
            }

        }

        //protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    using (SqlConnection con = new SqlConnection(connection))
        //    {
        //        SqlCommand cmd = new SqlCommand("[GetClients]", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        ddlclient.DataTextField = "name";
        //        ddlclient.DataValueField = "id";
        //        ddlclient.DataSource = dt;
        //        ddlclient.Items.Insert(0, "---All keywords---");
        //        ddlclient.DataBind();
        //    }
        //}
    }
}