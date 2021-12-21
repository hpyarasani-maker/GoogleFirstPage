using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleFirstPage.GoogleClassicLinksCallback
{
   public class SearchParams
    {
        public string source;
        public string domain;
        public string query;
        public int limit;
        public int pages;
        public string locale;
        public string geo_location;
        public string uule;
        public int parse;
        public string user_agent;
        public string context { get; set; }
    }
}