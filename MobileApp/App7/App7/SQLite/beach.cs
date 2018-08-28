using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;

namespace App7.SQLite
{
    [Table("beach")]
    public class beach
    {
        public int beach_id { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
    }

    public class ToDoAllItems
    {

        public void DoGetAllBeaches(string dbPath, string[] names,int[] idds, string[] phs) {
            var db = new SQLiteConnection(dbPath);
            for (var i = 0; i < names.Length; i++) {
                var newBeach = new beach();
                newBeach.beach_id = idds[i];
                newBeach.name = names[i];
                newBeach.photo = phs[i];
                db.Insert(newBeach);
            }
            var table = db.Table<beach>();

        }

    
    }
}
