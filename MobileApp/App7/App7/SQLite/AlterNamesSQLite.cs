using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App7.SQLite
{
    [Table("alttable")]
    public class alter
    {
        public string names { get; set; }
        public string source { get; set; }
        public int beach_id { get; set; }
    }

    public class getAlterNames
    {
        public void fillTableNames(string dbalter, List<nItem> alt, TableQuery<alter> table)
        {
            var db = new SQLiteConnection(dbalter);
            var names_id = alt.Select(item => item.Id).ToArray()[0];
            var altnames = alt.Select(item => item.Names).ToArray();
            var source = alt.Select(item => item.Source).ToArray();
            for (var i = 0; i < altnames.Length; i++)
            {
                alter al = new alter();
                al.beach_id = names_id;
                al.names = altnames[i];
                al.source = source[i];
                db.Insert(al);
            }

        }

        public List<alter> getAltById(int idd, string dbalter)
        {
            var db = new SQLiteConnection(dbalter);
            return db.Query<alter>("select * from alttable where beach_id = ?", idd);

        }
    }
}
