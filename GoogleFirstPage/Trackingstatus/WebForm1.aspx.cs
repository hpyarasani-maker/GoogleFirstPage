using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Dapper;

namespace GoogleFirstPage.Trackingstatus
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;


        string connection = ConfigurationManager.ConnectionStrings["Trackingpipeline"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("Name");
                    dt1.Columns.Add("Seid");
                    dt1.Columns.Add("Total");
                    dt1.Columns.Add("Received");
                    dt1.Columns.Add("Remaining");

                    cmd = new SqlCommand("[UI_Dashboard]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    //dt1 = ds.Tables[0];

                    var a1 = ds.Tables[0].Rows[0].ItemArray;
                    object[] rowArray1 = new object[6];
                    rowArray1 = a1;
                    DataRow row1 = dt1.NewRow();
                    row1["Total"] = rowArray1[3];
                    row1["Received"] = rowArray1[4];
                    row1["Remaining"] = rowArray1[5];

                    dt1.Rows.Add(row1);
                    

                }
            }
        }
    }
}