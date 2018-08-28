using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App7.SQLite
{
    [Table("latlng")]
    public class latlng
    {
        public float lat { get; set; }
        public float lon { get; set; }
        public string name { get; set; }
        public int beach_id { get; set; }
    }

    public class getCoordsFSQL {

        public void fillTablelatlng(string dblatlng, string json, TableQuery<latlng> table) {
            var db = new SQLiteConnection(dblatlng);
            var lll = new latlng();
            var jsonItem = JObject.Parse(json);
            lll.lat = (float)jsonItem["Items"][0]["latitude"];
            lll.lon = (float)jsonItem["Items"][0]["longitude"];
            lll.name = jsonItem["Items"][0]["name"].ToString();
            lll.beach_id = (int)jsonItem["Items"][0]["id"];
            db.Insert(lll);
        }

        public List<latlng> getlatlngFromTable(int idd, string dblatlng)
        {
            var db = new SQLiteConnection(dblatlng);
            return db.Query<latlng>("select * from latlng where beach_id = ?", idd);
        }

    }
}
