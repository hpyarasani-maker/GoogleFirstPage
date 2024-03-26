using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.RapidTrackingSERPs
{
    public partial class Missingelements : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["firstserver"].ToString();
        string connection1 = ConfigurationManager.ConnectionStrings["allsearchengines"].ToString();
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(connection1))
                {
                    //cmd = new SqlCommand("GetSearchEngines", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //da = new SqlDataAdapter(cmd);
                    //dt = new DataTable();
                    //da.Fill(dt);
                    //ddltype.DataTextField = "name";
                    //ddltype.DataValueField = "seid";
                    //ddltype.DataSource = dt;
                    //ddltype.DataBind();
                    //ddltype.SelectedItem.Value.ToString();
                    //ddltype.SelectedIndex = 0;


                    cmd = new SqlCommand("GetSearchEngines", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    ddlseid.DataTextField = "name";
                    ddlseid.DataValueField = "seid";
                    ddlseid.DataSource = dt;
                    ddlseid.DataBind();
                    ddlseid.SelectedItem.Value.ToString();
                    ddlseid.SelectedIndex = 0;
                }
            }
        }

        protected void btndata_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string type = ddltype.SelectedValue;
                    string seid = ddlseid.SelectedValue;

                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetSearchMissingElementKeywords";
                    cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = ddltype.SelectedValue;
                    cmd.Parameters.Add("@Seid", SqlDbType.Int).Value = ddlseid.SelectedValue;
                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        grmissing.EmptyDataText = "No Records Found";
                        grmissing.DataSource = cmd.ExecuteReader();
                        grmissing.DataBind();
                        //con.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void grmissing_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}