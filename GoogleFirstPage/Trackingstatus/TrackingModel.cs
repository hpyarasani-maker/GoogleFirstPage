using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Net.NetworkInformation;

namespace GoogleFirstPage.Trackingstatus
{
    public class TrackingModel
    {
        public int ID { get; set; }
        public string Seid { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }
        public string Received { get; set; }
        public string Remaining { get; set; }

        public override string ToString()
        {
            return $"{ID}{Seid}{Name}{Received}{Remaining}";
        }

        
        public List<TrackingModel> GetTrackingAll()
        {
            var connection = ConfigurationManager.ConnectionStrings["Trackingpipeline"].ToString();
            var dt = new List<TrackingModel>();
            using (IDbConnection con = new SqlConnection(connection))
            {
                con.Open();
                dt = con.Query<TrackingModel>("UI_Dashboard").ToList();
            }
            return dt;
        }

        public bool PingServer(byte[] AddrBytes)
        {
            bool Active = false;
            //10.2.0.4 Server1
            // 10.4.0.5 Server2
            //10.2.0.6 Server3
            // 10.4.0.7 Server4
            //10.2.0.8 Server5
            // 10.4.0.9 Server6
            // if ping is working then Server "Online" elseo "Offline"
            //byte[] AddrBytes1 = new byte[] { 10, 2, 0, 4 }; // byte array for server address.



            using (System.Net.NetworkInformation.Ping png = new System.Net.NetworkInformation.Ping())
            {
                System.Net.IPAddress addr;
                // Sending ping to a numeric byte address has the best change of
                // never causing en exception, whether network connected or not.
                addr = new System.Net.IPAddress(AddrBytes);
                try
                {
                    Active = (png.Send(addr, 1500, new byte[] { 0, 1, 2, 3 }).Status == IPStatus.Success);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    Console.WriteLine(ex.ToString());
                    Active = false;
                }
                return Active;
            }
        }
        public ArrayList servers()
        {
            ArrayList al = new ArrayList();
            byte[] AddrBytes = new byte[4];
            string[] arr = new string[6] { "10.2.0.4", "10.2.0.5", "10.2.0.6", "10.2.0.7", "10.2.0.8", "10.2.0.9" };
            foreach (string item in arr)
            {
                for (int i = 0; i < item.Split('.').Length; i++)
                {
                    byte a = Convert.ToByte(item.Split('.')[i]);
                    AddrBytes[i] = a;
                }
                al.Add(AddrBytes);
                AddrBytes = new byte[4];
            }
            return al;
        }

    }
}