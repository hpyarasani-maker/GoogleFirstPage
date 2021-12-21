using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleFirstPage.GoogleClassicLinksCallback
{
    class OxyParams
    {
        public string source { get; set; }
        public string domain { get; set; }
        public string[] query { get; set; }
        public int limit { get; set; }
        public int pages { get; set; }
        public string locale { get; set; }
        public string geo_location { get; set; }
        public string uule { get; set; }
        public bool parse { get; set; } //23-09-2021 changed datatype into "int to bool"
        public string callback_url { get; set; }
        public int seid { get; set; }
        public string user_agent_type { get; set; }
        public List<Context1> context { get; set; }
    }

    class Context1
    {
        public string key { get; set; }
        public dynamic value { get; set; }

        public Context1(string key, dynamic value)
        {
            this.key = key;
            this.value = value;
        }
    }
}