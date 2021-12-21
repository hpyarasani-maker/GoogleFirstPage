using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace GoogleFirstPage.Internallink
{
    public struct LinkItemAgility
    {
        public string Href;
        public string Text;
        public override string ToString()
        {
            return "Href = " + Href + "\n\t" + "Text = " + Text;
        }
    }

    static class LinkFinderAgility
    {
        public static List<LinkItemAgility> Find(string htmlSource)
        {
            List<LinkItemAgility> list = new List<LinkItemAgility>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlSource);
            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a");

            foreach (HtmlNode link in links)
            {
                LinkItemAgility i = new LinkItemAgility();
                i.Href = link.GetAttributeValue("href", "");
                i.Text = link.InnerText;
                list.Add(i);
            }
            return list;
        }
    }
}
