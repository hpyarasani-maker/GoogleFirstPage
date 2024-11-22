using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleFirstPage.RapidTrackingSERPs
{
    public class Seforaiblock
    {
        public static IList<Sesearchprop> seforai = new List<Sesearchprop>()
        {
            new Sesearchprop()//duplicate Seid's for AI overview testing  1026 to 1029
            {
                seid =1026, domain ="co.uk",geo_location="United Kingdom",locale= "en-gb",uule = "w+CAIQICIOVW5pdGVkIEtpbmdkb20=",device="desktop", sename="Google UK (AIO)"
            },
            new Sesearchprop()
            {
                seid =1027, domain ="co.uk", geo_location="United Kingdom", locale = "en-gb", uule = "w+CAIQICIOVW5pdGVkIEtpbmdkb20=",device="mobile_android", sename="Google UK (AIO, Mobile)"
            },
            new Sesearchprop()
            {
                seid =1028, domain ="com", geo_location="United States", locale="en-us", uule="w+CAIQICINVW5pdGVkIFN0YXRlcw==", device="desktop", sename="Google US (AIO)"
            },
            new Sesearchprop()
            {
                seid =1029, domain ="com", geo_location="United States", locale = "en-us", uule = "w+CAIQICINVW5pdGVkIFN0YXRlcw==",device="mobile_android", sename="Google US (AIO, Mobile)"
            },//duplicate Seid's for AI overview testing 1026 to 1029
        };
    }

    public class Sesearchprop
    {
        public int seid { get; set; }
        public string domain { get; set; }
        public string query { get; set; }
        public string geo_location { get; set; }
        public string locale { get; set; }
        public string uule { get; set; }
        public string tbm { get; set; }
        public string device { get; set; }
        public string sename { get; set; }
    }
}