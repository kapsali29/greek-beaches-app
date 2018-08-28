using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App7.SQLite
{
    [Table("near")]
    public class near
    {
        public string name { get; set; }
        public string photo { get; set; }
        public int beach_id { get; set; }
        public double distance { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public string mylat { get; set; } 
        public string mylon { get; set; }

    }

    public class getNearFSQL
    {
        public void fillTableNear(string dbnear, List<myItem> nl, TableQuery<near> table, string mla, string ml)
        {
            var db = new SQLiteConnection(dbnear);
            var names = nl.Select(item => item.name).ToArray();
            var photos = nl.Select(item => item.photo).ToArray();
            var ids = nl.Select(item => item.id).ToArray();
            var lons = nl.Select(item => item.longitude).ToArray();
            var lats = nl.Select(item => item.latitude).ToArray();
            var all_dist = nl.Select(item => item.distance).ToArray();
            for (var i = 0; i < ids.Length; i++) {
                near nn = new near();
                nn.beach_id = ids[i];
                nn.distance = all_dist[i];
                nn.lat = lats[i];
                nn.lon = lons[i];
                nn.name = names[i];
                nn.photo = photos[i];
                nn.mylat = mla;
                nn.mylon = ml;
                db.Insert(nn);
            }
        }

        public List<near> reachNear( string dbnear, string mla, string ml) {
            var db = new SQLiteConnection(dbnear);
            return db.Query<near>("select * from near where mylat = ? and mylon = ?", mla, ml);
        }
    }
}
