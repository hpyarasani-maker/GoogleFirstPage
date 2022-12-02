namespace MyLib
{
    using System;
    using System.Collections;

    public class MyCls
    {
        public bool blChng;
        public bool blColChng;

        public string[] diffInfo(IEnumerator n, IEnumerator ol, string s)
        {
            string[] strArray = new string[] { "", "" };
        Label_0154:
            if (!n.MoveNext())
            {
                ol.Reset();
                return strArray;
            }
            try
            {
                string[] strArray4;
                string[] strArray5;
                if (ol.MoveNext())
                {
                    try
                    {
                        if (!n.Current.ToString().Equals(ol.Current.ToString()))
                        {
                            string[] strArray2;
                            string[] strArray3;
                            (strArray2 = strArray)[0] = strArray2[0] + "<li>" + setDiff(n.Current.ToString().Replace("<", "&lt;"), ol.Current.ToString().Replace("<", "&lt;"), "green", s) + "</li>";
                            (strArray3 = strArray)[1] = strArray3[1] + "<li>" + setDiff(ol.Current.ToString().Replace("<", "&lt;"), n.Current.ToString().Replace("<", "&lt;"), "red", s) + "</li>";
                            this.blChng = true;
                            this.blColChng = true;
                        }
                        goto Label_0154;
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("at diff1 - " + exception.Message);
                    }
                }
                (strArray4 = strArray)[0] = strArray4[0] + "<li>" + setDiff(n.Current.ToString().Replace("<", "&lt;"), string.Empty, "green", s) + "</li>";
                (strArray5 = strArray)[1] = strArray5[1] + "<li> - - - ";
                this.blChng = true;
                this.blColChng = true;
            }
            catch (Exception exception2)
            {
                throw new Exception("at diff3 - " + exception2.Message);
            }
            goto Label_0154;
            //return strArray;
        }

        private string setDiff(string s1, string s2, string color, string s)
        {
            if (s == "A Href" || s == "Image Src")
            {
                return setUrlDiff(s1, s2, color);
            }

            var newStr = s1.Split(' ');
            var oldStr = s2.Split(' ');

            for (int i = 0; i < newStr.Length; i++)
            {
                try
                {
                    if (!newStr[i].Equals(oldStr[i]))
                        newStr[i] = "<span style=\"color:" + color + "\">" + newStr[i] + "</span>";
                }
                catch
                {
                    newStr[i] = "<span style=\"color:" + color + "\">" + newStr[i] + "</span>";
                }
            }

            return string.Join(" ", newStr);
        }

        private string setUrlDiff(string s1, string s2, string color)
        {
            var newChr = s1.ToCharArray();
            var oldChr = s2.ToCharArray();

            var diffStr = string.Empty;

            for (int i = 0; i < newChr.Length; i++)
            {
                try
                {
                    if (!newChr[i].Equals(oldChr[i]))
                        diffStr += "<span style=\"color:" + color + "\">" + newChr[i] + "</span>";
                    else
                        diffStr += newChr[i];
                }
                catch
                {
                    diffStr += "<span style=\"color:" + color + "\">" + newChr[i] + "</span>";
                }
            }

            return diffStr;
        }

        public string getColumn(IEnumerator nd)
        {

            string str = "";
            while (nd.MoveNext())
            {
                str = str + "<li>" + nd.Current.ToString().Replace("<", "&lt;") + "</li>";
            }
            return str;
        }
    }
}
