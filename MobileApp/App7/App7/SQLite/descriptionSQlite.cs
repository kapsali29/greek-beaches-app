using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App7.SQLite
{
    [Table("desc")]
    public class desc
    {
        public string description { get; set; }
        public string source { get; set; }
        public int beach_id { get; set; }
    }

    public class getDescription
    {
        public void fillTable(string dbdesc, List<dItem> details, TableQuery<desc> table)
        {
            var db = new SQLiteConnection(dbdesc);
            var desc_id = details.Select(item => item.id).ToArray()[0];
            var info = details.Select(item => item.description).ToArray();
            var src = details.Select(item => item.source).ToArray();
           for (var i =0; i<info.Length; i++)
            {
                desc d = new desc();
                d.description = info[i];
                d.beach_id = desc_id;
                d.source = src[i];
                db.Insert(d);
            }

        }

        public List<desc> getDescById(int idd, string dbdesc)
        {
            var db = new SQLiteConnection(dbdesc);
            return db.Query<desc>("select * from desc where beach_id = ?", idd);

        }

    }
}
