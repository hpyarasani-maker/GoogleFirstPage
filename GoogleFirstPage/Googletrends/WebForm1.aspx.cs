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
        List<string> allDates;
        List<string> volume;
        List<string> lDates;


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Dictionary<List<string>, List<string>> GetDicVolumeData(List<string> date, List<string> volume)
        {
            Dictionary<List<string>, List<string>> dict = new Dictionary<List<string>, List<string>>();
            dict.Add(date, volume);
            return dict;
        }


        protected void btnsvd_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Volume", typeof(string)));
            DataRow dr;
            allDates = GetAllDates().ToList();
            volume = GetVolumeData().ToList();
            var resVolume = from v in volume select v;
            lDates = GetLastDates(allDates).ToList();
            Dictionary<List<string>, List<string>> res = GetDicVolumeData(lDates, volume);
            dr = dt.NewRow();
            foreach (KeyValuePair<List<string>, List<string>> a in res.ToList())
            {
                string lastmdate = "";
                string volumedata = "";
                for (int i = 0; i < lDates.Count; i++)
                {
                    lastmdate = a.Key[i];
                    volumedata = a.Value[i];
                    dr = dt.NewRow();
                    dr["Date"] = lastmdate;
                    dr["Volume"] = volumedata;
                    dt.Rows.Add(dr);
                }
            }
            //dr = dt.NewRow();
            //dr["Date"] = res.Keys;
            //dr["Volume"] = res.Values;
            //dt.Rows.Add(dr);


            //foreach (var ad in allDates)
            //{
            //    dr = dt.NewRow();
            //    dr["Date"] = ad.ToString();

            //    foreach(var r in res)
            //    {
            //        if(ad.ToString() == r.Key.ToString())
            //        dr["Volume"] = r.Value;

            //    }
            //    //foreach (var ld in lDates)
            //    //{
            //    //    foreach (var vol in resVolume)
            //    //    {
            //    //        dict.Add(ld.ToString(),vol.ToString());

            //    //        //if (ad.ToString() == ld.ToString())
            //    //        foreach(KeyValuePair<string,string> volu in dict)
            //    //            dr["Volume"] = volu.Value;
            //    //    }
            //    //}
            //    dt.Rows.Add(dr);
            //}
            grdsvm.DataSource = dt;
            grdsvm.DataBind();
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
            List<string> myList = new List<string>();
            List<DateTime> dates = mydate1.Select(date => DateTime.Parse(date)).ToList();
            dates.Sort();

            var groupdates = dates.GroupBy(x => new { MatchDates = x.Month + "-" + x.Year }).Select(x => x.Max(s => s.Date));

            foreach (var items in groupdates)
            {
                //Console.WriteLine(items.ToString("yyyy-MM-dd"));
                myList.Add(items.ToString("yyyy-MM-dd"));
            }
            //dateitm = Items.ToString();
            return myList;
        }
       

        public List<string> GetVolumeData()
        {
            
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
            //li.Add("823000");
            //string val = li[0];
            return li.ToList();
            
        }


        public class UniqueDates
        {
            public string LDate { get; set; }

            public string Volume { get; set; }

        }

    }
}