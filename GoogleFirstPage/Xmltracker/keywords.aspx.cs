using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.Xmltracker
{
    public partial class keywords : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["xmltracker"].ToString();
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    SqlCommand cmd = new SqlCommand("[GetClients]", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddlclients.DataTextField = "name";
                    ddlclients.DataValueField = "id";
                    ddlclients.DataSource = dt;
                    ddlclients.DataBind();
                }
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtsearch.Text))
            {
                gridkeywords.DataSource = null;
                gridkeywords.DataBind();
                string strSql = "exec [dbo].[GetSearchKeyword]'" + txtsearch.Text + "'";
                //SqlConnection objCon = null;
                //try
                //{
                //    objCon = new SqlConnection(connection);
                //    objCon.Open();
                //    SqlCommand objCmd = new SqlCommand(strSql, objCon);
                //    gvsearch.DataSource = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                //    gvsearch.DataBind();
                //}
                //catch (Exception ex)
                //{
                //    Response.Write(ex.Message);
                //}
                using (var con = new SqlConnection(connection))
                {
                    cmd = new SqlCommand("[GetSearchKeyword]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@keyword", SqlDbType.NVarChar).Value = txtsearch.Text; ;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    gvsearch.DataSource = ds;
                    gvsearch.DataBind();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvsearch.DataSource = ds;
                        gvsearch.DataBind();
                    }
                    else
                    {
                        lblnodata.Text = "Keyword not found: " + txtsearch.Text;
                        //if (ds.Tables[0].Rows.Count < 0 || ds.Tables[0].Rows.Count == 0)
                        //{
                        //    lblalllinks.Text = "Data Not Available";
                        //}
                        //gvelements.DataSource = null;
                        //gvelements.DataBind();
                    }
                }
            }
            else
            {
                Response.Write("please enter keyword");
            }
        }

        protected void btnkeywords_Click(object sender, EventArgs e)
        {
            gvsearch.DataSource = null;
            gvsearch.DataBind();
            GetClientKeywords();
        }

        public void GetClientKeywords()
        {            
            string clientid = ddlclients.SelectedValue;
            string strSql = "exec [dbo].[GetClKeywords]'" + clientid + "'";

            SqlConnection objCon = null;
            try
            {
                objCon = new SqlConnection(connection);
                objCon.Open();
                SqlCommand objCmd = new SqlCommand(strSql, objCon);
                gridkeywords.DataSource = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                gridkeywords.DataBind();

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