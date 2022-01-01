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
    public partial class pipeline : System.Web.UI.Page
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

                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("Name");
                    dt2.Columns.Add("Seid");
                    dt2.Columns.Add("Total");
                    dt2.Columns.Add("Received");
                    dt2.Columns.Add("Remaining");

                    DataTable dt3 = new DataTable();
                    dt3.Columns.Add("Name");
                    dt3.Columns.Add("Seid");
                    dt3.Columns.Add("Total");
                    dt3.Columns.Add("Received");
                    dt3.Columns.Add("Remaining");

                    DataTable dt4 = new DataTable();
                    dt4.Columns.Add("Name");
                    dt4.Columns.Add("Seid");
                    dt4.Columns.Add("Total");
                    dt4.Columns.Add("Received");
                    dt4.Columns.Add("Remaining");

                    DataTable dt5 = new DataTable();
                    dt5.Columns.Add("Name");
                    dt5.Columns.Add("Seid");
                    dt5.Columns.Add("Total");
                    dt5.Columns.Add("Received");
                    dt5.Columns.Add("Remaining");


                    DataTable dt6 = new DataTable();
                    dt6.Columns.Add("Name");
                    dt6.Columns.Add("Seid");
                    dt6.Columns.Add("Total");
                    dt6.Columns.Add("Received");
                    dt6.Columns.Add("Remaining");

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
                    row1["Name"] = rowArray1[1];
                    row1["Seid"] = rowArray1[2];
                    row1["Total"] = rowArray1[3];
                    row1["Received"] = rowArray1[4];
                    row1["Remaining"] = rowArray1[5];
                    
                    dt1.Rows.Add(row1);
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                    lbl1.Text = "";

                    var a2 = ds.Tables[0].Rows[1].ItemArray;
                    object[] rowArray2 = new object[6];
                    rowArray2 = a2;
                    DataRow row2 = dt2.NewRow();
                    row2["Name"] = rowArray2[1];
                    row2["Seid"] = rowArray2[2];
                    row2["Total"] = rowArray2[3];
                    row2["Received"] = rowArray2[4];
                    row2["Remaining"] = rowArray2[5];

                    dt2.Rows.Add(row2);
                    GridView2.DataSource = dt2;
                    GridView2.DataBind();
                    lbl1.Text = "";


                    var a3 = ds.Tables[0].Rows[2].ItemArray;
                    object[] rowArray3 = new object[6];
                    rowArray3 = a3;
                    DataRow row3 = dt3.NewRow();
                    row3["Name"] = rowArray3[1];
                    row3["Seid"] = rowArray3[2];
                    row3["Total"] = rowArray3[3];
                    row3["Received"] = rowArray3[4];
                    row3["Remaining"] = rowArray3[5];

                    dt3.Rows.Add(row3);
                    GridView3.DataSource = dt3;
                    GridView3.DataBind();
                    lbl1.Text = "";

                    var a4 = ds.Tables[0].Rows[3].ItemArray;
                    object[] rowArray4 = new object[6];
                    rowArray4 = a4;
                    DataRow row4 = dt4.NewRow();
                    row4["Name"] = rowArray4[1];
                    row4["Seid"] = rowArray4[2];
                    row4["Total"] = rowArray4[3];
                    row4["Received"] = rowArray4[4];
                    row4["Remaining"] = rowArray4[5];

                    dt4.Rows.Add(row4);
                    GridView4.DataSource = dt4;
                    GridView4.DataBind();
                    lbl1.Text = "";


                    var a5 = ds.Tables[0].Rows[4].ItemArray;
                    object[] rowArray5 = new object[6];
                    rowArray5 = a5;
                    DataRow row5 = dt5.NewRow();
                    row5["Name"] = rowArray5[1];
                    row5["Seid"] = rowArray5[2];
                    row5["Total"] = rowArray5[3];
                    row5["Received"] = rowArray5[4];
                    row5["Remaining"] = rowArray5[5];

                    dt5.Rows.Add(row5);
                    GridView5.DataSource = dt5;
                    GridView5.DataBind();
                    lbl1.Text = "";

                    var a6 = ds.Tables[0].Rows[5].ItemArray;
                    object[] rowArray6 = new object[6];
                    rowArray6 = a6;
                    DataRow row6 = dt6.NewRow();
                    row6["Name"] = rowArray6[1];
                    row6["Seid"] = rowArray6[2];
                    row6["Total"] = rowArray6[3];
                    row6["Received"] = rowArray6[4];
                    row6["Remaining"] = rowArray6[5];

                    dt6.Rows.Add(row6);
                    GridView6.DataSource = dt6;
                    GridView6.DataBind();
                    lbl1.Text = "";


                }
            }
        }

        public DataTable GetFieldNames()
        {
            DataTable dt = new DataTable();
            return dt;
        }
        protected void grd1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}