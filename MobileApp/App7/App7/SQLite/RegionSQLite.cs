using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App7.SQLite
{
    [Table("region")]
    public class region
    {
        public int beach_id { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
        public int regid { get; set; }
    }

    public class getRegionItems {


        public void fillTableregion(string dbreg, List<tItem> dr, TableQuery<region> table, int point)
        {
            var db = new SQLiteConnection(dbreg);
            var ids = dr.Select(item => item.id).ToArray();
            var names = dr.Select(item => item.name).ToArray();
            var photos = dr.Select(item => item.photo).ToArray();
            for (var i = 0; i < ids.Length; i++) {
                var item = new region();
                item.beach_id = ids[i];
                item.name = names[i];
                item.photo = photos[i];
                item.regid = point;
                db.Insert(item);
            }
        }

        public List<region> getRegionFSQL(string dbreg, int point) {
            var db = new SQLiteConnection(dbreg);
            return db.Query<region>("select * from region where regid = ?", point);
        }

    }
}
