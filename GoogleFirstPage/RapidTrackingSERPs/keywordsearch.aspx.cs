using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.RapidTrackingSERPs
{
    public partial class keywords : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["firstserver"].ToString();
        
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        

        protected void Page_Load(object sender, EventArgs e)
        {
                lblalllinks.Visible = false;
        }

        protected void btnsearchkwd_Click(object sender, EventArgs e)
        {
            
          lblalllinks.Visible = false;
            try
            {
                lblalllinks.Visible = true;
                using (SqlConnection con = new SqlConnection(conn))
                {
                    cmd = new SqlCommand("[GetSearchKeywords]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    StreamWriter sw = new StreamWriter(@"C:\inetpub\wwwroot\html\keywordslist.txt", true);
                    int i;
                    foreach (DataRow row in dt.Rows)
                    {
                        object[] array = row.ItemArray;
                        for (i = 0; i < array.Length - 1; i++)
                        {
                            sw.Write(array[i].ToString() + " : ");
                        }
                        sw.WriteLine(array[i].ToString());
                    }
                    sw.Flush();
                    sw.Close();
                }
                lblalllinks.Text = "TextFile download is completed";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                
            }
        }
    }
}