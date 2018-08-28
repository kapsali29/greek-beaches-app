using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App7.SQLite
{
    [Table("comments")]
    public class comments
    {
        public string description { get; set; }
        public string source { get; set; }
        public int beach_id { get; set; }
    }
    public class getComments {
        public void fillComments(string dbcom, List<dItem> dc, TableQuery<comments> table)
        {
            var db = new SQLiteConnection(dbcom);
            var com = dc.Select(item => item.description).ToArray();
            var com_id = dc.Select(item => item.id).ToArray()[0];
            var source = dc.Select(item => item.source).ToArray();
            for (var i = 0; i < com.Length; i++) {
                comments ccc = new comments();
                ccc.beach_id = com_id;
                ccc.description = com[i];
                ccc.source = source[i];
                db.Insert(ccc);
            }

        }

        public List<comments> getComById(int idd, string dbcom)
        {
            var db = new SQLiteConnection(dbcom);
            return db.Query<comments>("select * from comments where beach_id = ?", idd);

        }
    }
}
