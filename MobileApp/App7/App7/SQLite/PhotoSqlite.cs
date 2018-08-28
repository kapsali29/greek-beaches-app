using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App7.SQLite
{
    [Table("photos")]
    public class photos
    {
        public string photo { get; set; }
        public int ph_id { get; set; }
        public int flag { get; set; }
    }
    public class getPhotos
    {
        public void accessPhoto(string dbPhPath, List<photoItem> dph, TableQuery<photos> table)
        {
            var dbph = new SQLiteConnection(dbPhPath);
            if (dph.Count == 0)
            {
                var new1 = new photos();
                new1.flag = 1;
                dbph.Insert(new1);
            }
            else
            {
                var photo_id = dph.Select(item => item.id).ToArray()[0];
                var photos = dph.Select(item => item.photo).ToArray();
                var newphoto = new photos();
                foreach (var i in photos)
                {
                    newphoto.photo = i;
                    newphoto.ph_id = photo_id;
                    newphoto.flag = 0;
                    dbph.Insert(newphoto);
                }
            }

        }

        public List<photos> getPhotoById(int idd, string dbPhPath)
        {
            var dbph = new SQLiteConnection(dbPhPath);
            return dbph.Query<photos>("select * from photos where ph_id = ? and flag = ?", idd, 0);

        }
    }
}
