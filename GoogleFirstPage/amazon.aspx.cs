using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.Web.Services;

namespace GoogleFirstPage
{
    public partial class amazon : System.Web.UI.Page
    {
        static DataTable dt1;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnamazzonresult_Click(object sender, EventArgs e)
        {
            string seid = DropDownList1.SelectedItem.Value.ToString();
            if (Page.IsValid)
            {
                switch (seid)
                {

                    case "1":
                        {
                            panelChart.Visible = false;
                            DataTable res = getTop100AmazonUK(kwd_txt.Text);
                            GridView1.DataSource = res;
                            GridView1.DataBind();
                            break;
                        }
                    case "2":
                        {

                            panelChart.Visible = false;
                            DataTable res = getTop100AmazonUKMobile(kwd_txt.Text);
                            GridView1.DataSource = res;
                            GridView1.DataBind();
                            break;
                        }
                    case "3":
                        {

                            panelChart.Visible = false;
                            DataTable res = getTop100AmazonFrance(kwd_txt.Text);
                            GridView1.DataSource = res;
                            GridView1.DataBind();
                            break;
                        }
                    case "4":
                        {

                            panelChart.Visible = false;
                            DataTable res = getTop100AmazonGermany(kwd_txt.Text);
                            GridView1.DataSource = res;
                            GridView1.DataBind();
                            break;
                        }
                    case "5":
                        {

                            panelChart.Visible = false;
                            DataTable res = getTop100AmazonItaly(kwd_txt.Text);
                            GridView1.DataSource = res;
                            GridView1.DataBind();
                            break;
                        }
                    case "6":
                        {

                            panelChart.Visible = false;
                            DataTable res = getTop100AmazonNetherlands(kwd_txt.Text);
                            GridView1.DataSource = res;
                            GridView1.DataBind();
                            break;
                        }
                    case "7":
                        {

                            panelChart.Visible = false;
                            DataTable res = getTop100AmazonSpain(kwd_txt.Text);
                            GridView1.DataSource = res;
                            GridView1.DataBind();
                            break;
                        }
                    case "8":
                        {
                            panelChart.Visible = false;
                            DataTable res = getTop100AmazonUS(kwd_txt.Text);
                            GridView1.DataSource = res;
                            GridView1.DataBind();
                            break;
                        }
                }
            }
        }


        ArrayList top100AmazonUK = new ArrayList();
        ArrayList brandList = new ArrayList();
        ArrayList reviewsList = new ArrayList();
        ArrayList costList = new ArrayList();
        string links = "";
        string brand = string.Empty;
        string reviews = "";
        string cost = "";
        int nCount = 0;
        int Pos = 0;
        string maxRepeated = string.Empty;
        ArrayList sample = new ArrayList();
        string url = string.Empty;
        string HTML = "";
        public DataTable getTop100AmazonUK(string keyword)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Position", typeof(int));
            dt.Columns.Add("Urls", typeof(string));
            dt.Columns.Add("By", typeof(string));
            dt.Columns.Add("Cost", typeof(string));
            dt.Columns.Add("Reviews", typeof(string));

            dt1 = new DataTable();
            dt1.Columns.Add("Brand", typeof(string));
            dt1.Columns.Add("Count", typeof(string));


            try
            {

                //url = "https://www.amazon.co.uk/s/page=1&keywords=<Q>&ie=UTF8";
                url = "https://www.amazon.co.uk/s?k=<Q>&crid=3N8YUAS5W58BH&sprefix=razor%2Caps%2C269&ref=nb_sb_ss_i_3_5";
                Regex regUrl = new Regex("<Q>", RegexOptions.IgnoreCase);
                string newURL = regUrl.Replace(url, keyword);
                HTML = getWebDataSource(newURL);
                string matchPattern = "<div class=\\Wa-row\\W><div[ aria-hidden=\\Wtrue\\W]* class=\\Wa-column a-span12 a-text-center\\W><a class=\\Wa-link-normal a-text-normal\\W href=\"(.*?)\"";
                string brandPattern = "by </span><span class=.a-size-small a-color-secondary.>(.*?)</span>";
                string reviewsPattern = "<i class=\\Wa-icon a-icon-star a-star-4-5\\W><span class=\\Wa-icon-alt\\W>(.*?)</span>";
                string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span>";
                //string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span><span class=\\Wa-letter-space\\W></span><span class=\\Wa-size-base a-color-price\\W>(.*?)</span>";
                Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(HTML);

                foreach (Match m in mc)
                {
                    nCount++;
                    links = HttpUtility.HtmlDecode(m.Groups[1].Value);
                    // HtmlText1 = HtmlText.Remove(0, 89);
                    if (!links.Contains("/gp/slredirect/"))
                        top100AmazonUK.Add(links);
                }
                re = new Regex(brandPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    brand = m.Groups[1].Value;
                    if (brandList.Count <= 19)
                        brandList.Add(brand);
                }
                re = new Regex(costPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    // nCount++;
                    cost = m.Groups[1].Value;//+ " " + m.Groups[2].Value
                    costList.Add(cost);
                }
                re = new Regex(reviewsPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    reviews = m.Groups[1].Value;
                    reviewsList.Add(reviews);
                }
                for (int i = 0; i < 20; i++)
                {
                    Pos++;

                    dt.Rows.Add(Pos, top100AmazonUK[i].ToString(), brandList.Count - 1 >= i ? brandList[i].ToString() : null, costList.Count - 1 >= i ? costList[i].ToString() : null, reviewsList.Count - 1 >= i ? reviewsList[i].ToString() : null);
                }
                List<string> results = brandList.Cast<string>().ToList();

                string[] myArray = (string[])brandList.ToArray(typeof(string));
                IGrouping<string, string> max = myArray.GroupBy(n => n).OrderByDescending(g => g.Count()).First();
                if (max.Count() > 0)
                {

                }
                //lblMaxPresence.CssClass(font)
                List<BrandValues> lst = new List<BrandValues>();
                foreach (string str in brandList)
                {
                    lst.Add(new BrandValues() { Name = str });
                }


                var ff = results.GroupBy(i => i);
                foreach (var grp in ff)
                {
                    dt1.Rows.Add(grp.Key, grp.Count());
                }


                GetPiechartData();
                if (dt1.Rows.Count > 1)
                {
                    panelChart.Visible = true;
                }

            }
            catch (Exception ex)
            {
                string errorAmazonUK = "e100" + " " + ex.Message.ToString();
                top100AmazonUK.Add(errorAmazonUK);

            }
            return dt;
        }

        public DataTable getTop100AmazonUKMobile(string keyword)
        {
            ArrayList top100AmazonUKMobile = new ArrayList();
            ArrayList alDup = new ArrayList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Position", typeof(int));
            dt.Columns.Add("Urls", typeof(string));
            dt.Columns.Add("By", typeof(string));
            dt.Columns.Add("Cost", typeof(string));
            dt.Columns.Add("Reviews", typeof(string));

            dt1 = new DataTable();
            dt1.Columns.Add("Brand", typeof(string));
            dt1.Columns.Add("Count", typeof(string));

            string url = string.Empty;
            string HTML = "";
            int newpos = 0;
            try
            {
                for (int t = 1; t <= 3; t++)
                {

                    url = "https://www.amazon.co.uk/gp/aw/s/ref=nb_sb_noss?page=" + t + "&k=<Q>";
                    Regex regUrl = new Regex("<Q>", RegexOptions.IgnoreCase);
                    string newURL = regUrl.Replace(url, keyword);
                    HTML = getWebDataMobileSource(newURL);
                    string matchPattern = "<a style=\\Wwidth:100\\W\\Wheight:100\\W\\W href=\"(.*?)\"";
                    string brandPattern = "<span class=\\Wauthor\\W>by (.*?)</span>";
                    string costPattern = "\\W<span class=\\WdpOurPrice\\W>\\W\\W(.*?)</span>";
                    string reviewsPattern = "\\s+<span class=\\Ws_ratingStar_45\\W title=\"(.*?)\"></span>";

                    Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase);
                    MatchCollection mc = re.Matches(HTML);

                    foreach (Match m in mc)
                    {
                        nCount++;
                        links = m.Groups[1].Value;
                        string HtmlText = "https://www.amazon.co.uk" + links;
                        top100AmazonUKMobile.Add(HttpUtility.HtmlDecode(HtmlText));
                    }


                    re = new Regex(brandPattern, RegexOptions.IgnoreCase);
                    mc = re.Matches(HTML);
                    foreach (Match m in mc)
                    {
                        //nCount++;
                        brand = m.Groups[1].Value;
                        //if (brandList.Count <= 19)
                        brandList.Add(brand);
                    }
                    re = new Regex(costPattern, RegexOptions.IgnoreCase);
                    mc = re.Matches(HTML);
                    foreach (Match m in mc)
                    {
                        // nCount++;
                        cost = m.Groups[1].Value;//+ " " + m.Groups[2].Value
                        costList.Add(cost);
                    }
                    re = new Regex(reviewsPattern, RegexOptions.IgnoreCase);
                    mc = re.Matches(HTML);
                    foreach (Match m in mc)
                    {
                        //nCount++;
                        reviews = m.Groups[1].Value.Substring(0, 3);
                        reviewsList.Add(reviews);
                    }
                    for (int i = 0; i < 20; i++)
                    {
                        Pos++;
                        if (Pos <= top100AmazonUKMobile.Count)
                        {
                            newpos++;
                            if (newpos <= 20)
                                dt.Rows.Add(newpos, top100AmazonUKMobile[i].ToString(), brandList.Count - 1 >= i ? brandList[i].ToString() : null, costList.Count - 1 >= i ? costList[i].ToString() : null, reviewsList.Count - 1 >= i ? reviewsList[i].ToString() : null);
                        }

                    }
                    top100AmazonUKMobile.Clear();
                    brandList.Clear();
                    costList.Clear();
                    reviewsList.Clear();
                    Pos = 0;
                    if (newpos == 20) break;
                }

                foreach (DataRow list in dt.Rows)
                {
                    brandList.Add(list["By"].ToString());
                }

                List<string> results = brandList.Cast<string>().ToList();

                string[] myArray = (string[])brandList.ToArray(typeof(string));
                IGrouping<string, string> max = myArray.GroupBy(n => n).OrderByDescending(g => g.Count()).First();
                if (max.Count() > 0)
                {

                }
                //lblMaxPresence.CssClass(font)
                List<BrandValues> lst = new List<BrandValues>();
                foreach (string str in brandList)
                {
                    lst.Add(new BrandValues() { Name = str });
                }


                var ff = results.GroupBy(i => i);
                foreach (var grp in ff)
                {
                    dt1.Rows.Add(grp.Key, grp.Count());
                }


                GetPiechartData();
                if (dt1.Rows.Count > 1)
                {
                    panelChart.Visible = true;
                }
            }
            catch (Exception ex)
            {
                string errorAmazonUK = "e100" + " " + ex.Message.ToString();
                top100AmazonUK.Add(errorAmazonUK);

            }
            return dt;
        }

        ArrayList top100AmazonFR = new ArrayList();

        public DataTable getTop100AmazonFrance(string keyword)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Position", typeof(int));
            dt.Columns.Add("Urls", typeof(string));
            dt.Columns.Add("By", typeof(string));
            dt.Columns.Add("Cost", typeof(string));
            dt.Columns.Add("Reviews", typeof(string));

            dt1 = new DataTable();
            dt1.Columns.Add("Brand", typeof(string));
            dt1.Columns.Add("Count", typeof(string));

            try
            {

                url = "https://www.amazon.fr/s/page=1&keywords=<Q>&ie=UTF8";
                Regex regUrl = new Regex("<Q>", RegexOptions.IgnoreCase);
                string newURL = regUrl.Replace(url, keyword);
                HTML = getWebDataSource(newURL);
                string matchPattern = "<div class=\\Wa-row\\W><div[ aria-hidden=\\Wtrue\\W]* class=\\Wa-column a-span12 a-text-center\\W><a class=\\Wa-link-normal a-text-normal\\W href=\"(.*?)\"";
                string brandPattern = "de </span><span class=.a-size-small a-color-secondary.>(.*?)</span>";
                //string reviewsPattern = "<i class=\\Wa-icon a-icon-star a-star-4-5\\W><span class=\\Wa-icon-alt\\W>(.*?)</span>";
                string reviewsPattern = "<span class=\\Wa-icon-alt\\W>(.*?)</span></i><i class=\\Wa-icon a-icon-popover\\W>";
                string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span>";
                //string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span><span class=\\Wa-letter-space\\W></span><span class=\\Wa-size-base a-color-price\\W>(.*?)</span>";
                Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(HTML);

                foreach (Match m in mc)
                {
                    nCount++;
                    links = HttpUtility.HtmlDecode(m.Groups[1].Value);
                    // HtmlText1 = HtmlText.Remove(0, 89);
                    if (!links.Contains("/gp/slredirect/"))
                        top100AmazonFR.Add(links);
                }
                re = new Regex(brandPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    if (m.Groups[1].Value.Contains("a-link-normal a-text-normal"))
                    {
                        string tempBrandPattern = "<a [^>]*>(.*?)</a>"; // "<[a|A][^>]*>|)";// "<[a|A][^>]*>|</[a|A]>";
                        Regex re1 = new Regex(tempBrandPattern, RegexOptions.IgnoreCase);
                        MatchCollection mc1 = re1.Matches(m.Groups[1].Value);
                        foreach (Match m1 in mc1)
                        {
                            if (brandList.Count <= 19)
                                brandList.Add(m1.Groups[1].Value);
                        }
                    }
                    else
                    {
                        brand = m.Groups[1].Value;
                        if (brandList.Count <= 19)
                            brandList.Add(brand);
                    }
                }
                re = new Regex(costPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    // nCount++;
                    cost = m.Groups[1].Value;//+ " " + m.Groups[2].Value
                    costList.Add(cost);
                }
                re = new Regex(reviewsPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    reviews = m.Groups[1].Value.Substring(0, 3).TrimEnd('v').TrimEnd('é');
                    reviewsList.Add(reviews);
                }
                for (int i = 0; i < 20; i++)
                {
                    Pos++;

                    dt.Rows.Add(Pos, top100AmazonFR[i].ToString(), brandList.Count - 1 >= i ? brandList[i].ToString() : null, costList.Count - 1 >= i ? costList[i].ToString() : null, reviewsList.Count - 1 >= i ? reviewsList[i].ToString() : null);
                }
                List<string> results = brandList.Cast<string>().ToList();


                string[] myArray = (string[])brandList.ToArray(typeof(string));
                IGrouping<string, string> max = myArray.GroupBy(n => n).OrderByDescending(g => g.Count()).First();
                if (max.Count() > 0)
                {

                }
                List<BrandValues> lst = new List<BrandValues>();
                foreach (string str in brandList)
                {
                    lst.Add(new BrandValues() { Name = str });
                }


                var ff = results.GroupBy(i => i);
                foreach (var grp in ff)
                {
                    dt1.Rows.Add(grp.Key, grp.Count());
                }


                GetPiechartData();
                if (dt1.Rows.Count > 1)
                {
                    panelChart.Visible = true;
                }

            }
            catch (Exception ex)
            {
                string errorAmazonFR = "e100" + " " + ex.Message.ToString();
                top100AmazonFR.Add(errorAmazonFR);

            }
            return dt;
        }


        ArrayList top100AmazonDE = new ArrayList();

        public DataTable getTop100AmazonGermany(string keyword)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Position", typeof(int));
            dt.Columns.Add("Urls", typeof(string));
            dt.Columns.Add("By", typeof(string));
            dt.Columns.Add("Cost", typeof(string));
            dt.Columns.Add("Reviews", typeof(string));

            dt1 = new DataTable();
            dt1.Columns.Add("Brand", typeof(string));
            dt1.Columns.Add("Count", typeof(string));

            try
            {

                url = "https://www.amazon.de/s/page=1&keywords=<Q>&ie=UTF8";
                Regex regUrl = new Regex("<Q>", RegexOptions.IgnoreCase);
                string newURL = regUrl.Replace(url, keyword);
                HTML = getWebDataSource(newURL);
                string matchPattern = "<div class=\\Wa-row\\W><div[ aria-hidden=\\Wtrue\\W]* class=\\Wa-column a-span12 a-text-center\\W><a class=\\Wa-link-normal a-text-normal\\W href=\"(.*?)\"";
                string brandPattern = "von </span><span class=.a-size-small a-color-secondary.>(.*?)</span>";
                //string reviewsPattern = "<i class=\\Wa-icon a-icon-star a-star-4-5\\W><span class=\\Wa-icon-alt\\W>(.*?)</span>";
                string reviewsPattern = "<span class=\\Wa-icon-alt\\W>(.*?)</span></i><i class=\\Wa-icon a-icon-popover\\W>";
                string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span>";
                //string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span><span class=\\Wa-letter-space\\W></span><span class=\\Wa-size-base a-color-price\\W>(.*?)</span>";
                Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(HTML);

                foreach (Match m in mc)
                {
                    nCount++;
                    links = HttpUtility.HtmlDecode(m.Groups[1].Value);
                    // HtmlText1 = HtmlText.Remove(0, 89);
                    if (!links.Contains("/gp/slredirect/"))
                        top100AmazonDE.Add(links);
                }
                re = new Regex(brandPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    if (m.Groups[1].Value.Contains("a-link-normal a-text-normal"))
                    {
                        string tempBrandPattern = "<a [^>]*>(.*?)</a>"; // "<[a|A][^>]*>|)";// "<[a|A][^>]*>|</[a|A]>";
                        Regex re1 = new Regex(tempBrandPattern, RegexOptions.IgnoreCase);
                        MatchCollection mc1 = re1.Matches(m.Groups[1].Value);
                        foreach (Match m1 in mc1)
                        {
                            if (brandList.Count <= 19)
                                brandList.Add(m1.Groups[1].Value);
                        }
                    }
                    else
                    {
                        brand = m.Groups[1].Value;
                        if (brandList.Count <= 19)
                            brandList.Add(brand);
                    }
                }
                re = new Regex(costPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    // nCount++;
                    cost = m.Groups[1].Value;//+ " " + m.Groups[2].Value
                    costList.Add(cost);
                }
                re = new Regex(reviewsPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    reviews = m.Groups[1].Value.Substring(0, 3).TrimEnd('v');
                    reviewsList.Add(reviews);
                }
                for (int i = 0; i < 20; i++)
                {
                    Pos++;

                    dt.Rows.Add(Pos, top100AmazonDE[i].ToString(), brandList.Count - 1 >= i ? brandList[i].ToString() : null, costList.Count - 1 >= i ? costList[i].ToString() : null, reviewsList.Count - 1 >= i ? reviewsList[i].ToString() : null);
                }
                List<string> results = brandList.Cast<string>().ToList();

                string[] myArray = (string[])brandList.ToArray(typeof(string));
                IGrouping<string, string> max = myArray.GroupBy(n => n).OrderByDescending(g => g.Count()).First();
                if (max.Count() > 0)
                {

                }
                //lblMaxPresence.CssClass(font)
                List<BrandValues> lst = new List<BrandValues>();
                foreach (string str in brandList)
                {
                    lst.Add(new BrandValues() { Name = str });
                }


                var ff = results.GroupBy(i => i);
                foreach (var grp in ff)
                {
                    dt1.Rows.Add(grp.Key, grp.Count());
                }


                GetPiechartData();
                if (dt1.Rows.Count > 1)
                {
                    panelChart.Visible = true;
                }

            }
            catch (Exception ex)
            {
                string errorAmazonDE = "e100" + " " + ex.Message.ToString();
                top100AmazonDE.Add(errorAmazonDE);

            }
            return dt;
        }


        ArrayList top100AmazonIT = new ArrayList();
        public DataTable getTop100AmazonItaly(string keyword)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Position", typeof(int));
            dt.Columns.Add("Urls", typeof(string));
            dt.Columns.Add("By", typeof(string));
            dt.Columns.Add("Cost", typeof(string));
            dt.Columns.Add("Reviews", typeof(string));

            dt1 = new DataTable();
            dt1.Columns.Add("Brand", typeof(string));
            dt1.Columns.Add("Count", typeof(string));

            try
            {

                url = "https://www.amazon.it/s/page=1&keywords=<Q>&ie=UTF8";
                Regex regUrl = new Regex("<Q>", RegexOptions.IgnoreCase);
                string newURL = regUrl.Replace(url, keyword);
                HTML = getWebDataSource(newURL);
                string matchPattern = "<div class=\\Wa-row\\W><div[ aria-hidden=\\Wtrue\\W]* class=\\Wa-column a-span12 a-text-center\\W><a class=\\Wa-link-normal a-text-normal\\W href=\"(.*?)\"";
                string brandPattern = "di </span><span class=.a-size-small a-color-secondary.>(.*?)</span>";
                //string reviewsPattern = "<i class=\\Wa-icon a-icon-star a-star-4-5\\W><span class=\\Wa-icon-alt\\W>(.*?)</span>";
                string reviewsPattern = "<span class=\\Wa-icon-alt\\W>(.*?)</span></i><i class=\\Wa-icon a-icon-popover\\W>";
                string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span>";
                //string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span><span class=\\Wa-letter-space\\W></span><span class=\\Wa-size-base a-color-price\\W>(.*?)</span>";
                Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(HTML);

                foreach (Match m in mc)
                {
                    nCount++;
                    links = HttpUtility.HtmlDecode(m.Groups[1].Value);
                    // HtmlText1 = HtmlText.Remove(0, 89);
                    if (!links.Contains("/gp/slredirect/"))
                        top100AmazonIT.Add(links);
                }
                re = new Regex(brandPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    if (m.Groups[1].Value.Contains("a-link-normal a-text-normal"))
                    {
                        string tempBrandPattern = "<a [^>]*>(.*?)</a>"; // "<[a|A][^>]*>|)";// "<[a|A][^>]*>|</[a|A]>";
                        Regex re1 = new Regex(tempBrandPattern, RegexOptions.IgnoreCase);
                        MatchCollection mc1 = re1.Matches(m.Groups[1].Value);
                        foreach (Match m1 in mc1)
                        {
                            if (brandList.Count <= 19)
                                brandList.Add(m1.Groups[1].Value);
                        }
                    }
                    else
                    {
                        brand = m.Groups[1].Value;
                        if (brandList.Count <= 19)
                            brandList.Add(brand);
                    }
                }
                re = new Regex(costPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    // nCount++;
                    cost = m.Groups[1].Value;//+ " " + m.Groups[2].Value
                    costList.Add(cost);
                }
                re = new Regex(reviewsPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    reviews = m.Groups[1].Value.Substring(0, 3).TrimEnd('v').TrimEnd('s');
                    reviewsList.Add(reviews);
                }
                for (int i = 0; i < 20; i++)
                {
                    Pos++;

                    dt.Rows.Add(Pos, top100AmazonIT[i].ToString(), brandList.Count - 1 >= i ? brandList[i].ToString() : null, costList.Count - 1 >= i ? costList[i].ToString() : null, reviewsList.Count - 1 >= i ? reviewsList[i].ToString() : null);
                }
                List<string> results = brandList.Cast<string>().ToList();


                string[] myArray = (string[])brandList.ToArray(typeof(string));
                IGrouping<string, string> max = myArray.GroupBy(n => n).OrderByDescending(g => g.Count()).First();
                if (max.Count() > 0)
                {

                }
                //lblMaxPresence.CssClass(font)
                List<BrandValues> lst = new List<BrandValues>();
                foreach (string str in brandList)
                {
                    lst.Add(new BrandValues() { Name = str });
                }


                var ff = results.GroupBy(i => i);
                foreach (var grp in ff)
                {
                    dt1.Rows.Add(grp.Key, grp.Count());

                }


                GetPiechartData();
                if (dt1.Rows.Count > 1)
                {
                    panelChart.Visible = true;
                }

            }
            catch (Exception ex)
            {
                string errorAmazonIT = "e100" + " " + ex.Message.ToString();
                top100AmazonIT.Add(errorAmazonIT);

            }
            return dt;
        }

        ArrayList top100AmazonNL = new ArrayList();
        public DataTable getTop100AmazonNetherlands(string keyword)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Position", typeof(int));
            dt.Columns.Add("Urls", typeof(string));
            dt.Columns.Add("By", typeof(string));
            dt.Columns.Add("Cost", typeof(string));
            dt.Columns.Add("Reviews", typeof(string));

            dt1 = new DataTable();
            dt1.Columns.Add("Brand", typeof(string));
            dt1.Columns.Add("Count", typeof(string));

            try
            {

                url = "https://www.amazon.nl/s/page=1&keywords=<Q>&ie=UTF8";
                Regex regUrl = new Regex("<Q>", RegexOptions.IgnoreCase);
                string newURL = regUrl.Replace(url, keyword);
                HTML = getWebDataSource(newURL);
                string matchPattern = "<div class=\\Wa-row\\W><div[ aria-hidden=\\Wtrue\\W]* class=\\Wa-column a-span12 a-text-center\\W><a class=\\Wa-link-normal a-text-normal\\W href=\"(.*?)\"";
                string brandPattern = "door </span><span class=.a-size-small a-color-secondary.>(.*?)</span>";
                //string reviewsPattern = "<i class=\\Wa-icon a-icon-star a-star-4-5\\W><span class=\\Wa-icon-alt\\W>(.*?)</span>";
                string reviewsPattern = "<span class=\\Wa-icon-alt\\W>(.*?)</span></i><i class=\\Wa-icon a-icon-popover\\W>";
                string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span>";
                //string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span><span class=\\Wa-letter-space\\W></span><span class=\\Wa-size-base a-color-price\\W>(.*?)</span>";
                Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(HTML);

                foreach (Match m in mc)
                {
                    nCount++;
                    links = HttpUtility.HtmlDecode(m.Groups[1].Value);
                    // HtmlText1 = HtmlText.Remove(0, 89);
                    if (!links.Contains("/gp/slredirect/"))
                        top100AmazonNL.Add(links);
                }
                re = new Regex(brandPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    if (m.Groups[1].Value.Contains("a-link-normal a-text-normal"))
                    {
                        string tempBrandPattern = "<a [^>]*>(.*?)</a>"; // "<[a|A][^>]*>|)";// "<[a|A][^>]*>|</[a|A]>";
                        Regex re1 = new Regex(tempBrandPattern, RegexOptions.IgnoreCase);
                        MatchCollection mc1 = re1.Matches(m.Groups[1].Value);
                        foreach (Match m1 in mc1)
                        {
                            if (brandList.Count <= 19)
                                brandList.Add(m1.Groups[1].Value);
                        }
                    }
                    else
                    {
                        brand = m.Groups[1].Value;
                        if (brandList.Count <= 19)
                            brandList.Add(brand);
                    }
                }
                re = new Regex(costPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    // nCount++;
                    cost = m.Groups[1].Value;//+ " " + m.Groups[2].Value
                    costList.Add(cost);
                }
                re = new Regex(reviewsPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    reviews = m.Groups[1].Value.Substring(0, 3).TrimEnd('s').TrimEnd('v');
                    reviewsList.Add(reviews);
                }
                for (int i = 0; i < 20; i++)
                {
                    Pos++;

                    dt.Rows.Add(Pos, top100AmazonNL[i].ToString(), brandList.Count - 1 >= i ? brandList[i].ToString() : null, costList.Count - 1 >= i ? costList[i].ToString() : null, reviewsList.Count - 1 >= i ? reviewsList[i].ToString() : null);
                }
                List<string> results = brandList.Cast<string>().ToList();


                string[] myArray = (string[])brandList.ToArray(typeof(string));
                IGrouping<string, string> max = myArray.GroupBy(n => n).OrderByDescending(g => g.Count()).First();
                if (max.Count() > 0)
                {

                }
                //lblMaxPresence.CssClass(font)
                List<BrandValues> lst = new List<BrandValues>();
                foreach (string str in brandList)
                {
                    lst.Add(new BrandValues() { Name = str });
                }


                var ff = results.GroupBy(i => i);
                foreach (var grp in ff)
                {
                    dt1.Rows.Add(grp.Key, grp.Count());

                }


                GetPiechartData();
                if (dt1.Rows.Count > 1)
                {
                    panelChart.Visible = true;
                }

            }
            catch (Exception ex)
            {
                string errorAmazonNL = "e100" + " " + ex.Message.ToString();
                top100AmazonNL.Add(errorAmazonNL);

            }
            return dt;
        }

        ArrayList top100AmazonES = new ArrayList();
        public DataTable getTop100AmazonSpain(string keyword)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Position", typeof(int));
            dt.Columns.Add("Urls", typeof(string));
            dt.Columns.Add("By", typeof(string));
            dt.Columns.Add("Cost", typeof(string));
            dt.Columns.Add("Reviews", typeof(string));

            dt1 = new DataTable();
            dt1.Columns.Add("Brand", typeof(string));
            dt1.Columns.Add("Count", typeof(string));

            try
            {

                url = "https://www.amazon.es/s/page=1&keywords=<Q>&ie=UTF8";
                Regex regUrl = new Regex("<Q>", RegexOptions.IgnoreCase);
                string newURL = regUrl.Replace(url, keyword);
                HTML = getWebDataSource(newURL);
                string matchPattern = "<div class=\\Wa-row\\W><div[ aria-hidden=\\Wtrue\\W]* class=\\Wa-column a-span12 a-text-center\\W><a class=\\Wa-link-normal a-text-normal\\W href=\"(.*?)\"";
                string brandPattern = "de </span><span class=.a-size-small a-color-secondary.>(.*?)</span>";
                //string reviewsPattern = "<i class=\\Wa-icon a-icon-star a-star-4-5\\W><span class=\\Wa-icon-alt\\W>(.*?)</span>";
                string reviewsPattern = "<span class=\\Wa-icon-alt\\W>(.*?)</span></i><i class=\\Wa-icon a-icon-popover\\W>";
                string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span>";
                //string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span><span class=\\Wa-letter-space\\W></span><span class=\\Wa-size-base a-color-price\\W>(.*?)</span>";
                Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(HTML);

                foreach (Match m in mc)
                {
                    nCount++;
                    links = HttpUtility.HtmlDecode(m.Groups[1].Value);
                    // HtmlText1 = HtmlText.Remove(0, 89);
                    if (!links.Contains("/gp/slredirect/"))
                        top100AmazonES.Add(links);
                }
                re = new Regex(brandPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    if (m.Groups[1].Value.Contains("a-link-normal a-text-normal"))
                    {
                        string tempBrandPattern = "<a [^>]*>(.*?)</a>"; // "<[a|A][^>]*>|)";// "<[a|A][^>]*>|</[a|A]>";
                        Regex re1 = new Regex(tempBrandPattern, RegexOptions.IgnoreCase);
                        MatchCollection mc1 = re1.Matches(m.Groups[1].Value);
                        foreach (Match m1 in mc1)
                        {
                            if (brandList.Count <= 19)
                                brandList.Add(m1.Groups[1].Value);
                        }
                    }
                    else
                    {
                        brand = m.Groups[1].Value;
                        if (brandList.Count <= 19)
                            brandList.Add(brand);
                    }
                }
                re = new Regex(costPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    // nCount++;
                    cost = m.Groups[1].Value;//+ " " + m.Groups[2].Value
                    costList.Add(cost);
                }
                re = new Regex(reviewsPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    reviews = m.Groups[1].Value.Substring(0, 3).TrimEnd('s');
                    reviewsList.Add(reviews);
                }
                for (int i = 0; i < 20; i++)
                {
                    Pos++;

                    dt.Rows.Add(Pos, top100AmazonES[i].ToString(), brandList.Count - 1 >= i ? brandList[i].ToString() : null, costList.Count - 1 >= i ? costList[i].ToString() : null, reviewsList.Count - 1 >= i ? reviewsList[i].ToString() : null);
                }
                List<string> results = brandList.Cast<string>().ToList();


                string[] myArray = (string[])brandList.ToArray(typeof(string));
                IGrouping<string, string> max = myArray.GroupBy(n => n).OrderByDescending(g => g.Count()).First();
                if (max.Count() > 0)
                {

                }
                //lblMaxPresence.CssClass(font)
                List<BrandValues> lst = new List<BrandValues>();
                foreach (string str in brandList)
                {
                    lst.Add(new BrandValues() { Name = str });
                }


                var ff = results.GroupBy(i => i);
                foreach (var grp in ff)
                {
                    dt1.Rows.Add(grp.Key, grp.Count());

                }


                GetPiechartData();
                if (dt1.Rows.Count > 1)
                {
                    panelChart.Visible = true;
                }

            }
            catch (Exception ex)
            {
                string errorAmazonES = "e100" + " " + ex.Message.ToString();
                top100AmazonES.Add(errorAmazonES);

            }
            return dt;
        }


        ArrayList top100AmazonUS = new ArrayList();


        public DataTable getTop100AmazonUS(string keyword)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Position", typeof(int));
            dt.Columns.Add("Urls", typeof(string));
            dt.Columns.Add("By", typeof(string));
            dt.Columns.Add("Cost", typeof(string));
            dt.Columns.Add("Reviews", typeof(string));

            dt1 = new DataTable();
            dt1.Columns.Add("Brand", typeof(string));
            dt1.Columns.Add("Count", typeof(string));

            //string url = string.Empty;
            //string HTML = "";

            try
            {

                url = "https://www.amazon.com/s/page=1&keywords=<Q>&ie=UTF8";
                //url = "https://www.amazon.com/s/page=" + i + "&keywords=<Q>&ie=UTF8";
                Regex regUrl = new Regex("<Q>", RegexOptions.IgnoreCase);
                string newURL = regUrl.Replace(url, keyword);
                //HTML = getWebDataSource(newURL);
                HTML = getWebDataSourceAmazonUS(newURL);
                //HTML = getWebDataSource_IP(newURL);
                string matchPattern = "<div class=\\Wa-row\\W><div[ aria-hidden=\\Wtrue\\W]* class=\\Wa-column a-span12 a-text-center\\W><a class=\\Wa-link-normal a-text-normal\\W href=\"(.*?)\"";
                string brandPattern = "by </span><span class=.a-size-small a-color-secondary.>(.*?)</span>";
                string reviewsPattern = "<i class=\\Wa-icon a-icon-star a-star-4-5\\W><span class=\\Wa-icon-alt\\W>(.*?)</span>";
                string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span>";
                //string costPattern = "<span class=\\Wa-size-base a-color-price s-price a-text-bold\\W>(.*?)</span><span class=\\Wa-letter-space\\W></span><span class=\\Wa-size-base a-color-price\\W>(.*?)</span>";
                Regex re = new Regex(matchPattern, RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(HTML);

                foreach (Match m in mc)
                {
                    nCount++;
                    links = HttpUtility.HtmlDecode(m.Groups[1].Value);
                    // HtmlText1 = HtmlText.Remove(0, 89);
                    if (!links.Contains("/gp/slredirect/"))
                        top100AmazonUS.Add(links);
                }
                re = new Regex(brandPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    brand = m.Groups[1].Value;
                    if (brandList.Count <= 19)
                        brandList.Add(brand);
                }
                re = new Regex(costPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    // nCount++;
                    cost = m.Groups[1].Value;//+ " " + m.Groups[2].Value
                    costList.Add(cost);
                }
                re = new Regex(reviewsPattern, RegexOptions.IgnoreCase);
                mc = re.Matches(HTML);
                foreach (Match m in mc)
                {
                    //nCount++;
                    reviews = m.Groups[1].Value;
                    reviewsList.Add(reviews);
                }
                for (int i = 0; i < 20; i++)
                {
                    Pos++;

                    dt.Rows.Add(Pos, top100AmazonUS[i].ToString(), brandList.Count - 1 >= i ? brandList[i].ToString() : null, costList.Count - 1 >= i ? costList[i].ToString() : null, reviewsList.Count - 1 >= i ? reviewsList[i].ToString() : null);
                }
                List<string> results = brandList.Cast<string>().ToList();

                string[] myArray = (string[])brandList.ToArray(typeof(string));
                IGrouping<string, string> max = myArray.GroupBy(n => n).OrderByDescending(g => g.Count()).First();
                if (max.Count() > 0)
                {
                    //lblMaxPresence.Text = "Max No.of Presence : " + max.Key + "  (" + max.Count() + ")";
                }
                //lblMaxPresence.CssClass(font)
                List<BrandValues> lst = new List<BrandValues>();
                foreach (string str in brandList)
                {
                    lst.Add(new BrandValues() { Name = str });
                }


                var ff = results.GroupBy(i => i);
                foreach (var grp in ff)
                {
                    dt1.Rows.Add(grp.Key, grp.Count());

                }

                GetPiechartData();

                if (dt1.Rows.Count > 1)
                {
                    panelChart.Visible = true;
                }

            }
            catch (Exception ex)
            {
                string errorAmazonUS = "e100" + " " + ex.Message.ToString();
                top100AmazonUS.Add(errorAmazonUS);

            }
            return dt;
        }

        public string getWebDataMobileSource(string url)
        {
            System.Threading.Thread.Sleep(10000);
            StringBuilder stringBuilder = new StringBuilder();
            string value = string.Empty;
            Uri uri = new Uri(url);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.UserAgent = @"Mozilla/5.0 (iPod; U; CPU iPhone OS 2_2_1 like Mac OS X; en-us) AppleWebKit/525.18.1 (KHTML, like Gecko) Mobile/5H11a";
            try
            {
                HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();
                if (res.StatusCode != HttpStatusCode.OK) throw new Exception(res.StatusDescription);
                value = new StreamReader(res.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                stringBuilder.Append(value);
            }
            catch (WebException ex)
            {
                value = ex.Message.ToString();
                stringBuilder.Append(value);
            }
            return stringBuilder.ToString();
        }


        public string getWebDataSource(string url)
        {
            // System.Threading.Thread.Sleep(GetTime());
            //  string sendingIp = GetIP();
            //   int sendingPort = 0;
            StringBuilder stringBuilder = new StringBuilder();
            string value = string.Empty;
            Uri uri = new Uri(url);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            // httpWebRequest.UserAgent = @"Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";
            try
            {
                //   ServicePoint servicePoint2 = ServicePointManager.FindServicePoint(uri);
                //   servicePoint2.BindIPEndPointDelegate = ((ServicePoint servicePoint, IPEndPoint remoteEp, int retryCount) => new IPEndPoint(IPAddress.Parse(sendingIp), sendingPort));                
                HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();
                if (res.StatusCode != HttpStatusCode.OK) throw new Exception(res.StatusDescription);
                value = new StreamReader(res.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                stringBuilder.Append(value);
            }
            catch (WebException ex)
            {
                Environment.Exit(Environment.ExitCode);
                //value = ex.Message.ToString();
                //stringBuilder.Append(value);
            }
            return stringBuilder.ToString();
        }
        public string getWebDataSourceAmazonUS(string url)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string value = string.Empty;
            Uri uri = new Uri(url);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            // httpWebRequest.UserAgent = @"Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";
            httpWebRequest.UserAgent = @"Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.93 Safari/537.36";

            try
            {
                //   ServicePoint servicePoint2 = ServicePointManager.FindServicePoint(uri);
                //   servicePoint2.BindIPEndPointDelegate = ((ServicePoint servicePoint, IPEndPoint remoteEp, int retryCount) => new IPEndPoint(IPAddress.Parse(sendingIp), sendingPort));                
                HttpWebResponse res = (HttpWebResponse)httpWebRequest.GetResponse();
                if (res.StatusCode != HttpStatusCode.OK) throw new Exception(res.StatusDescription);
                value = new StreamReader(res.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                stringBuilder.Append(value);
            }
            catch (WebException ex)
            {
                Environment.Exit(Environment.ExitCode);
                //value = ex.Message.ToString();
                //stringBuilder.Append(value);
            }
            return stringBuilder.ToString();
        }

        Random rnd;
        DataTable dtIPs; // to store IPs from database.
        string sIP; // for return to dashboard.

        string[] lastIP = new string[2];
        ArrayList al = new ArrayList();
        Dictionary<string, CookieCollection> cookies = new Dictionary<string, CookieCollection>();

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                tc.Attributes["style"] = "border-right:1px solid black";

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

            }
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (TableCell tc in e.Row.Cells)
            {
                tc.Attributes["style"] = "border-right:1px solid black";

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

            }
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
        }

        [WebMethod]
        public static List<ChartDetails> GetPiechartData()
        {

            List<ChartDetails> dataList = new List<ChartDetails>();

            foreach (DataRow dtrow in dt1.Rows)
            {
                ChartDetails details = new ChartDetails();
                details.Brand = dtrow[0].ToString();
                int Percentage = Convert.ToInt32(dtrow[1]);//* 100 / 20;
                details.Count = Percentage;
                dataList.Add(details);
            }

            return dataList;
        }

        public class BrandValues
        {
            public string Name { get; set; }
        }
        public class ChartDetails
        {
            public string Brand { get; set; }
            public int Count { get; set; }
        }

    }
}
