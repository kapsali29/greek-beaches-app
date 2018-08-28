using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Square.Picasso;

namespace App7.Droid
{
    public class NearAdapter: BaseAdapter<myItem>
    {
        ImageView image;
        List<myItem> items;
        Activity context;

        public override int Count
        {
            get { return items.Count; }
        }

        public override myItem this[int position]
        {
            get { return items[position]; }
        }

        public NearAdapter(Activity context, List<myItem> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            var idds = items.Select(data => data.id).ToArray();
            for (var i = 0; i < idds.Length; i++)
            {
                //getPhotoForListview(idds[i]);

            }
            var dds = items;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomView, null);
            image = view.FindViewById<ImageView>(Resource.Id.Image);

            //getPhotoForListview(item.id);
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.name;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.id.ToString();
            if (item.photo != "")
            {
                Picasso.With(context).Load(item.photo).Into(image);
            }
            else
            {
                image.SetImageResource(Resource.Drawable.Icon);
            }
            return view;
        }
    }
}