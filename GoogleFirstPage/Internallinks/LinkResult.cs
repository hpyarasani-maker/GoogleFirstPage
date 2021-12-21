using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleFirstPage.Internallink
{
    public class LinkResult
    {
        public int SlNo { get; set; }
        public string URL { get; set; }
        public string Text { get; set; }
        public string TextType { get; set; }  // Text or Image etc...
        public string LinkType { get; set; }  // internal / external ect...
    }
}
