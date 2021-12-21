using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.GoogleClassicLinksCallback
{
    public partial class Livesearch : System.Web.UI.Page
    {
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataSet ds;

        public string resp1 = string.Empty;
        public string result1 = string.Empty;

        HTMLParserNewTask_Callbackurl WOWS = new HTMLParserNewTask_Callbackurl();
        

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["allsearchengines"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                oxydiv.Visible = false;

                cmd = new SqlCommand("GetSearchEngines", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Kname", SqlDbType.NVarChar).Value = kname;
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                searchEngines.DataTextField = "name";
                searchEngines.DataValueField = "seid";
                searchEngines.DataSource = dt;
                searchEngines.DataBind();
                //searchEngines.Items.Insert(0, "----Select----");
                searchEngines.SelectedItem.Value.ToString();
                //ddlkeyword.SelectedValue = "2";
                searchEngines.SelectedIndex = 0;
            }

        }

        protected void btncallbackurl_Click(object sender, EventArgs e)
        {
            string seid = searchEngines.SelectedItem.Value;
            string keyword = txtsearch.Text;

            if (Page.IsValid)
            {
                oxydiv.Visible = true;
                Dictionary<string, ArrayList> results = WOWS.getTop100(keyword, seid);
                
                string callback_url = WOWS.callback_url;
                string returnurl = WOWS.resURL;

                if (results.Count < 1)
                {
                    error_lbl.Text = "no results";
                }
                else if (results.ToString().Contains("e100") && results.ToString().Trim().StartsWith("e100"))
                {
                    error_lbl.Text = "e100 results";
                }
                else
                {
                    int count = 1;
                    ArrayList myList = new ArrayList();
                    foreach (KeyValuePair<string, ArrayList> kvp in results)
                    {
                        //string kwd = kvp.Key.Split(',')[10].Replace("query", "");
                        //string jobid = kvp.Key.Split(',')[12];
                        ArrayList alRes = kvp.Value;
                        for (int i = 0; i < alRes.Count; i++)
                        {
                            myList.Add(new mURL(alRes[i].ToString(), count++));
                        }
                        //lbl2.Text = "Keyword : " + search_txt.Text + "<br/>" + "Search Engine : " + searchEngines.SelectedItem.ToString() + "<br/>" + "Job ID : " + result1 + "<br/>" + "No.of URLs : " + alRes.Count + "<br/>" + "Callbackurl : " + callback_url + "<br/>" + "CallbackURL Return Result_URL : " + returnurl;
                        lbl2.Text = "" + returnurl;
                        Label1.Text = "" + callback_url;
                    }
                    gvtrackingcallback.DataSource = myList;
                    gvtrackingcallback.DataBind();
                }
            }
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            lbl2.Text = "";
            gvtrackingcallback.DataSource = null;
            gvtrackingcallback.DataBind();
        }

        protected void gvtrackingcallback_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
        }

        public class mURL
        {
            private string url;
            private int position;
            public mURL(string url, int position)
            {
                this.url = url;
                this.position = position;
            }
            public string URL { get { return url; } }
            public int Position { get { return position; } }
        }
    }
}