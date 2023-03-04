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
            List<string> myDateFromList = GetDateFrom();
            List<DateTime> myFromDates = GetFromDates(myDateFromList);
            List<string> myDateToList = GetDateTo();
            List<DateTime> myToDates = GetToDates(myDateToList);

            var weeks = GetNumberofWeeks(DateTime.Parse("06-03-2023 00:00:00",null), DateTime.Parse("26-03-2023 00:00:00", null));
            Response.Write(weeks);
            //foreach (var myfrom in myFromDates)
            //{
            //    Response.Write(myfrom + "<br />");
            //}
            //string dates = GetNumberofWeeks(myFirstDates, myToDates);
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
            dt.Columns.Add(new DataColumn("AllDates", typeof(string)));
            //dt.Columns.Add(new DataColumn("Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Volume", typeof(string)));
            //dt.Columns.Add(new DataColumn("Total", typeof(string)));
            DataRow dr;
            allDates = GetDateTo().ToList();
            volume = GetVolumeData().ToList();
            var resVolume = from v in volume select v;
            lDates = GetLastDates(allDates).ToList();
            Dictionary<List<string>, List<string>> res = GetDicVolumeData(lDates, volume);
            //int total = 0;
            int total = volume.Sum(x => Convert.ToInt32(x));
            foreach (var ad in allDates)
            {
                dr = dt.NewRow();
                foreach (KeyValuePair<List<string>, List<string>> a in res.ToList())
                {
                    string lastmdate = "";
                    string volumedata = "";
                    for (int i = 0; i < lDates.Count; i++)
                    {
                        lastmdate = a.Key[i];
                        volumedata = a.Value[i];
                        
                        dr["AllDates"] = ad;
                        if (ad.ToString() == lastmdate.ToString())
                        {
                            //dr["Date"] = lastmdate;
                            dr["Volume"] = volumedata;

                            //for (int z = 0; z < lDates.Count; z++)
                            //{
                            //    total = dt.AsEnumerable().Sum(row => row.Field<Int32>(volumedata));
                            //    //dr["Total"] = total.ToString();
                            //}
                        }
                    }
                }
                dt.Rows.Add(dr);
            }

           
            grdsvm.DataSource = dt;
            grdsvm.DataBind();

            //int total = 0; 
            grdsvm.FooterRow.Cells[0].Text = "Total Volume";
            grdsvm.FooterRow.Cells[1].Text = total.ToString();
           
        }
     

        public List<string> GetDateTo()
        {
            List<string> datetoList = new List<string>();
            datetoList.Add("2022-03-12");
            datetoList.Add("2022-03-19");
            datetoList.Add("2022-03-26");
            datetoList.Add("2022-04-02");
            datetoList.Add("2022-04-09");
            datetoList.Add("2022-04-16");
            datetoList.Add("2022-04-23");
            datetoList.Add("2022-04-30");
            datetoList.Add("2022-05-07");
            datetoList.Add("2022-05-14");
            datetoList.Add("2022-05-21");
            datetoList.Add("2022-05-28");
            datetoList.Add("2022-06-04");
            datetoList.Add("2022-06-11");
            datetoList.Add("2022-06-18");
            datetoList.Add("2022-06-25");
            datetoList.Add("2022-07-02");
            datetoList.Add("2022-07-09");
            datetoList.Add("2022-07-16");
            datetoList.Add("2022-07-23");
            datetoList.Add("2022-07-30");
            datetoList.Add("2022-08-06");
            datetoList.Add("2022-08-13");
            datetoList.Add("2022-08-20");
            datetoList.Add("2022-08-27");
            datetoList.Add("2022-09-03");
            datetoList.Add("2022-09-10");
            datetoList.Add("2022-09-17");
            datetoList.Add("2022-09-24");
            datetoList.Add("2022-10-01");
            datetoList.Add("2022-10-08");
            datetoList.Add("2022-10-15");
            datetoList.Add("2022-10-22");
            datetoList.Add("2022-10-29");
            datetoList.Add("2022-11-05");
            datetoList.Add("2022-11-12");
            datetoList.Add("2022-11-19");
            datetoList.Add("2022-11-26");
            datetoList.Add("2022-12-03");
            datetoList.Add("2022-12-10");
            datetoList.Add("2022-12-17");
            datetoList.Add("2022-12-24");
            datetoList.Add("2022-12-31");
            datetoList.Add("2023-01-07");
            datetoList.Add("2023-01-14");
            datetoList.Add("2023-01-21");
            datetoList.Add("2023-01-28");
            datetoList.Add("2023-02-04");
            datetoList.Add("2023-02-11");
            datetoList.Add("2023-02-18");
            datetoList.Add("2023-02-25");
            datetoList.Add("2023-03-04");
            return datetoList;
        }
        public List<string> GetDateFrom()
        {
            List<string> datefromList = new List<string>();
            datefromList.Add("2022-03-06");
            datefromList.Add("2022-03-13");
            datefromList.Add("2022-03-20");
            datefromList.Add("2022-03-27");
            datefromList.Add("2022-04-03");
            datefromList.Add("2022-04-10");
            datefromList.Add("2022-04-17");
            datefromList.Add("2022-04-24");
            datefromList.Add("2022-05-01");
            datefromList.Add("2022-05-08");
            datefromList.Add("2022-05-15");
            datefromList.Add("2022-05-22");
            datefromList.Add("2022-05-29");
            datefromList.Add("2022-06-05");
            datefromList.Add("2022-06-12");
            datefromList.Add("2022-06-19");
            datefromList.Add("2022-06-26");
            datefromList.Add("2022-07-03");
            datefromList.Add("2022-07-10");
            datefromList.Add("2022-07-17");
            datefromList.Add("2022-07-24");
            datefromList.Add("2022-07-31");
            datefromList.Add("2022-08-07");
            datefromList.Add("2022-08-14");
            datefromList.Add("2022-08-21");
            datefromList.Add("2022-08-28");
            datefromList.Add("2022-09-04");
            datefromList.Add("2022-09-11");
            datefromList.Add("2022-09-18");
            datefromList.Add("2022-09-25");
            datefromList.Add("2022-10-02");
            datefromList.Add("2022-10-09");
            datefromList.Add("2022-10-16");
            datefromList.Add("2022-10-23");
            datefromList.Add("2022-10-30");
            datefromList.Add("2022-11-06");
            datefromList.Add("2022-11-13");
            datefromList.Add("2022-11-20");
            datefromList.Add("2022-11-27");
            datefromList.Add("2022-12-04");
            datefromList.Add("2022-12-11");
            datefromList.Add("2022-12-18");
            datefromList.Add("2022-12-25");
            datefromList.Add("2023-01-01");
            datefromList.Add("2023-01-08");
            datefromList.Add("2023-01-15");
            datefromList.Add("2023-01-22");
            datefromList.Add("2023-01-29");
            datefromList.Add("2023-02-05");
            datefromList.Add("2023-02-12");
            datefromList.Add("2023-02-19");
            datefromList.Add("2023-02-26");
            return datefromList;
        }
        public List<string> GetLastDates(List<string> myDateFrom)
        {
            List<string> myList = new List<string>();
            List<DateTime> dates = myDateFrom.Select(date => DateTime.Parse(date)).ToList();
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
        public List<DateTime> GetFromDates(List<string> myDateFrom)
        {
            List<DateTime> myList = new List<DateTime>();
            List<DateTime> dates = myDateFrom.Select(date => DateTime.Parse(date)).ToList();
            dates.Sort();

            var groupdates = dates.GroupBy(x => new { MatchDates = x.Month + "-" + x.Year }).Select(x => x.Min(s => s.Date));

            foreach (var items in groupdates)
            {
                //Console.WriteLine(items.ToString("yyyy-MM-dd"));
                myList.Add(items);
            }
            //dateitm = Items.ToString();
            return myList;
        }
        public List<DateTime> GetToDates(List<string> myDateTo)
        {
            List<DateTime> myList = new List<DateTime>();
            List<DateTime> dates = myDateTo.Select(date => DateTime.Parse(date)).ToList();
            dates.Sort();

            var groupdates = dates.GroupBy(x => new { MatchDates = x.Month + "-" + x.Year }).Select(x => x.Max(s => s.Date));

            foreach (var items in groupdates)
            {
                //Console.WriteLine(items.ToString("yyyy-MM-dd"));
                myList.Add(items);
            }
            //dateitm = Items.ToString();
            return myList;
        }


        public List<string> GetVolumeData()
        {
            
            List<string> li = new List<string>();
            li.Add("673000");
            li.Add("823000");
            li.Add("823000");
            li.Add("673000");
            li.Add("673000");
            li.Add("823000");
            li.Add("823000");
            li.Add("823000");
            li.Add("1000000");
            li.Add("823000");
            li.Add("823000");
            li.Add("673000");
            li.Add(GetNumberofDays(int.Parse(li[0])));
            //string val = li[0];
            return li.ToList();
            
        }

        public string GetNumberofDays(int vm)
        {
            
            int d = vm/DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month)*DateTime.Now.Day;
            return d.ToString();
        }
        public string GetNumberofWeeks(DateTime fromdate, DateTime dateto)
        {
            TimeSpan ts = new TimeSpan();
            if (fromdate < dateto)
            {
                 ts = dateto.AddDays(1).Subtract(fromdate);
            }
            else
            {
                 ts = fromdate.AddDays(-1).Subtract(dateto);
            }
            int totalWeeks = ts.Days / 7;
            return totalWeeks.ToString();
        }

        public class UniqueDates
        {
            public string LDate { get; set; }

            public string Volume { get; set; }

        }

    }
}