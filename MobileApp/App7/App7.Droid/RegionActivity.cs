using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App7.Droid
{
    [Activity(Label = "App7.Droid", Icon = "@drawable/icon")]
    public class RegionActivity : ListActivity
    {
        public static string[] regAr;
        public static int[] regId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.region);
            MyClass my = new MyClass();
            my.getRegionBeaches();
            RunOnUiThread(() =>
            {
                ListAdapter = new RegionAdapter(this, FromAllItemClass.regionItems);
            });
           ListView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var beachName = regAr[e.Position];
            var beachId = regId[e.Position];
            SharedObjects.mainName = beachName;
            SharedObjects.mainId = beachId;
            var intent = new Intent(this, typeof(ItemContext))
                    .SetFlags(ActivityFlags.ReorderToFront);
            StartActivity(intent);

        }

        //public void getRegionBeaches() {
          //  var url = "http://192.168.17.1:5000/getRegionBeaches/" + fromRegionFrag.regionPointer.ToString();
           // var client = new RestClient(url);
            //var request = new RestRequest(Method.GET);
            //client.ExecuteAsync<tRootObject>(request, response =>
            //{
              //  regionCall(response.Data.Items);
            //});
        //}
        //public void regionCall(List<tItem> d) {
          //  var beachArray = d.Select(item => item.name).ToArray();
            //regAr = beachArray;
            //var beachIdsArray = d.Select(item => item.id).ToArray();
            //regId = beachIdsArray;
            //var myAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, beachArray);
            //RunOnUiThread(() => {
              //  ListAdapter = new RegionAdapter(this, d);
                //textView.Adapter = adapter2;
            //});
        //}


    }


   

    public class fromRegionFrag
    {
        public static int regionPointer;
    }
}