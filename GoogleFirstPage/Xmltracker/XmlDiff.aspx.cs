using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Xml;

namespace GoogleFirstPage.Xmltracker
{
    public partial class XmlDiff : Page
    {
        string connection = ConfigurationManager.ConnectionStrings["xmltracker"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            string uid = Request.QueryString["uid"].ToString();
            string date = Request.QueryString["date"].ToString();

            string strQuery = "Exec [XmlTracker].[dbo].[GetXmlSourceChangeDate] " + uid + ", '" + date + "'";

            try
            {
                string curXml = string.Empty;
                var prevXml = string.Empty;

                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    SqlCommand comm = new SqlCommand(strQuery, con);
                    comm.CommandTimeout = 0;
                    SqlDataReader dr = comm.ExecuteReader();
                    if (dr.Read())
                    {
                        curXml = dr.GetSqlXml(0).Value;
                        prevXml = dr.GetSqlXml(1).Value;
                    }
                    dr.Close();
                    comm.Dispose();
                    con.Close();
                }

                string diffXml = XmlChanges(curXml, prevXml);

                Response.Clear();
                Response.ContentType = "text/xml";
                Response.Write(diffXml);
                //Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }            
        }

        private string XmlChanges(string curXml, string prevXml)
        {
            XmlDocument curXmlDoc = new XmlDocument();
            XmlDocument prevXmlDoc = new XmlDocument();

            curXmlDoc.LoadXml(curXml);
            prevXmlDoc.LoadXml(prevXml);

            string url = curXmlDoc.SelectSingleNode("XmlSource").Attributes["url"].Value;
            string curDate = curXmlDoc.SelectSingleNode("XmlSource").Attributes["date"].Value;
            string prevDate = prevXmlDoc.SelectSingleNode("XmlSource").Attributes["date"].Value;

            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.Append($"<XmlSource InDate=\"{curDate}\" OutDate=\"{prevDate}\" url=\"{url}\">");

            XmlNodeList curList = curXmlDoc.SelectNodes("XmlSource/tag");
            XmlNodeList prevList = prevXmlDoc.SelectNodes("XmlSource/tag");

            foreach (XmlNode curNode in curList)
            {
                XmlNode cNode = curNode.ChildNodes[0];
                if (cNode == null)
                    continue;
                XmlNodeList cNodes = cNode.ChildNodes;
                string strChanges = string.Empty;
                foreach (XmlNode prevNode in prevList)
                {
                    XmlNode pNode = prevNode.ChildNodes[0];
                    if (pNode != null && pNode.Name == cNode.Name)
                    {
                        XmlNodeList pNodes = pNode.ChildNodes;
                        string changes = GetChanges(cNodes, pNodes);
                        if (!string.IsNullOrEmpty(changes))
                        {
                            strChanges += $"<{cNode.Name}>";
                            strChanges += changes;
                            strChanges += $"</{cNode.Name}>";
                        }
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(strChanges))
                {
                    sb.Append($"<tag type=\"{curNode.Attributes["type"].Value}\">");
                    sb.Append(strChanges);
                    sb.Append("</tag>");
                }
            }

            sb.Append("</XmlSource>");
            return sb.ToString();
        }

        private string GetChanges(XmlNodeList cNodes, XmlNodeList pNodes)
        {
            StringBuilder sb = new StringBuilder();

            int curCount = cNodes.Count;
            int prevCount = pNodes.Count;

            for (int i = 0; i < curCount; i++)
            {
                try
                {
                    if (cNodes.Item(i).OuterXml != pNodes.Item(i).OuterXml)
                    {
                        sb.Append($"<in>{cNodes.Item(i).OuterXml}</in>");
                        sb.Append($"<out>{pNodes.Item(i).OuterXml}</out>");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(NullReferenceException))
                    {
                        sb.Append($"<in>{cNodes.Item(i).OuterXml}</in>");
                        sb.Append("<out></out>");
                    }
                }
            }

            if (prevCount > curCount)
                for (int i = curCount; i < prevCount; i++)
                {
                    sb.Append("<in></in>");
                    sb.Append($"<out>{pNodes.Item(i).OuterXml}</out>");
                }

            return sb.ToString();
        }
    }
}