using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleFirstPage.RapidTrackingSERPs
{
    public class GetParams
    {
        public string jobid { get; set; }
        public string domain { get; set; }
        public string query { get; set; }
        public string geo_location { get; set; }
        public string locale { get; set; }
        public string user_agent_type { get; set; }

        public string uule { get; set; }
        
    }
}