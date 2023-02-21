using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoogleFirstPage.Googletrends
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        List<string> dateitm;
        List<string> svdata;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnsvd_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Volume", typeof(string)));
            DataRow dr;

            List<string> allDates = GetAllDates();
            foreach (var ad in allDates)
            {
                dr = dt.NewRow();
                dr["Date"] = ad.ToString();
                dt.Rows.Add(dr);
            }

            dateitm = GetLastDates(allDates);
            svdata = GetVolumeData();
            Dictionary<List<string>, List<string>> result = new Dictionary<List<string>, List<string>>();
            //result = GetAllDatesData(dateitm, svdata);
            result.Add(dateitm, svdata);
            List<string> dataa = new List<string> ();
         
            foreach (KeyValuePair<List<string>, List<string>> kvp in result)
            {
                dt.NewRow();
                List<string> lstk = kvp.Key;
                List<string> lstv = kvp.Value;
                for (int i = 0; i < lstk.Count; i++)
                {
                    //dataa.add(lstk[i].ToString(), lstv[i].ToString());
                    //dataa.Add(lstk[i].ToString());
                    //row["volume"] = lstv[i].ToString();
                    for (int x = 0; x < lstv.Count; x++)
                    {
                        dataa.Add(lstv[x].ToString());
                    }

                }
                
            }

            //grdsvm.DataSource = allDates;
            //grdsvm.DataBind();

        }
        public List<string> GetAllDates()
        {
            List<string> mydate = new List<string>();
            mydate.Add("2022-02-26");
            mydate.Add("2022-03-05");
            mydate.Add("2022-03-12");
            mydate.Add("2022-03-19");
            mydate.Add("2022-03-26");
            mydate.Add("2022-04-02");
            mydate.Add("2022-04-09");
            mydate.Add("2022-04-16");
            mydate.Add("2022-04-23");
            mydate.Add("2022-04-30");
            mydate.Add("2022-05-07");
            mydate.Add("2022-05-14");
            mydate.Add("2022-05-21");
            mydate.Add("2022-05-28");
            mydate.Add("2022-06-04");
            mydate.Add("2022-06-11");
            mydate.Add("2022-06-18");
            mydate.Add("2022-06-25");
            mydate.Add("2022-07-02");
            mydate.Add("2022-07-09");
            mydate.Add("2022-07-16");
            mydate.Add("2022-07-23");
            mydate.Add("2022-07-30");
            mydate.Add("2022-08-06");
            mydate.Add("2022-08-13");
            mydate.Add("2022-08-20");
            mydate.Add("2022-08-27");
            mydate.Add("2022-09-03");
            mydate.Add("2022-09-10");
            mydate.Add("2022-09-17");
            mydate.Add("2022-09-24");
            mydate.Add("2022-10-01");
            mydate.Add("2022-10-08");
            mydate.Add("2022-10-15");
            mydate.Add("2022-10-22");
            mydate.Add("2022-10-29");
            mydate.Add("2022-11-05");
            mydate.Add("2022-11-12");
            mydate.Add("2022-11-19");
            mydate.Add("2022-11-26");
            mydate.Add("2022-12-03");
            mydate.Add("2022-12-10");
            mydate.Add("2022-12-17");
            mydate.Add("2022-12-24");
            mydate.Add("2022-12-31");
            mydate.Add("2023-01-07");
            mydate.Add("2023-01-14");
            mydate.Add("2023-01-21");
            mydate.Add("2023-01-28");
            mydate.Add("2023-02-04");
            mydate.Add("2023-02-11");
            mydate.Add("2023-02-21");
            return mydate;
        }
        public List<string> GetLastDates(List<string> mydate1)
        {
            
            List<DateTime> dates = mydate1.Select(date => DateTime.Parse(date)).ToList();
            dates.Sort();

            var groupdates = dates.GroupBy(x => new { MatchDates = x.Month + "-" + x.Year }).Select(x => x.Max(s => s.Date));

            foreach (var items in groupdates)
            {
                //Console.WriteLine(items.ToString("yyyy-MM-dd"));
                mydate1.Add(items.ToString("yyyy-MM-dd"));
            }
          //dateitm = Items.ToString();
            return mydate1;
        }


        public List<string> GetVolumeData()
        {
            List<string> lsvm = new List<string>();
            List<string> li = new List<string>();
            li.Add("823000");
            li.Add("673000");
            li.Add("673000");
            li.Add("673000");
            li.Add("673000");
            li.Add("823000");
            li.Add("823000");
            li.Add("823000");
            li.Add("1000000");
            li.Add("823000");
            li.Add("823000");
            li.Add("673000");
            li.Add("673000");
            foreach (var items in li)
            {
                lsvm.Add(items.ToString());
            }
            return lsvm;
        }

    }
}