using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App7.SQLite
{
    [Table("address")]
    public class address
    {
        public string names { get; set; }
        public string ad { get; set; }
        public int beach_id { get; set; }
    }

    public class getAdSQL
    {
        public void fillTableAd(string dbad, string json, TableQuery<address> table)
        {
            var db = new SQLiteConnection(dbad);
            var addd = new address();
            var jsonItem = JObject.Parse(json);

            addd.names = jsonItem["Items"][0]["Names"].ToString();
            addd.ad = jsonItem["Items"][0]["address"].ToString();
            addd.beach_id = (int)jsonItem["Items"][0]["Id"];
            db.Insert(addd);
        }

        public List<address> getaddressFromTable(int idd, string dbad)
        {
            var db = new SQLiteConnection(dbad);
            return db.Query<address>("select * from address where beach_id = ?", idd);
        }
    }
}
