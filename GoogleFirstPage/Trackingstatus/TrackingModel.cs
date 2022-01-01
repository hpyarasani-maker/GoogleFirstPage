using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

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
    }

    



}