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
using Newtonsoft.Json.Linq;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using  Macaw.UIComponents;
using Android.Graphics;
using Android.Support.V4.App;


namespace App7.Droid
{
    [Activity(Label = "App7.Droid", Icon = "@drawable/icon")]
    public class ItemContext :   FragmentActivity
    {
        public static string lat;
        public static string lon;
        //Button mapsButton;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {


            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ItemContext);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            //imageView = FindViewById<MultiImageView>(Resource.Id.imageView1);
            //mapsButton = FindViewById<Button>(Resource.Id.button1);
            var myTextView = FindViewById<TextView>(Resource.Id.textView1);
            var mybeachId = SharedObjects.mainId;
            MyClass mycc = new MyClass();
            mycc.readLatLng(SharedObjects.mainId);
            myTextView.Text = SharedObjects.mainName;
            AddTab("Photos", new showPhotos());
            AddTab("Description", new beachDesc());
            AddTab("Comments", new beachComments());
            AddTab("Map", new myMap());
            // readLatLng(mybeachId);
            //getPhotos(mybeachId);
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.AddMarker(new MarkerOptions()
                .SetPosition(new LatLng(50.897778, 3.013333))
                .SetTitle("Marker"));
        }
        void AddTab(string tabText, Android.App.Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);

            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e) {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.frameLayout1);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.frameLayout1, view);
                
            };
            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e) {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
        }


        //public void readLatLng(int idd)
        //{
        //  var url = "http://192.168.17.1:5000/getCoordinates/" + idd.ToString();
        //var client = new RestClient(url);
        //var request = new RestRequest(Method.GET);


        //client.ExecuteAsync<RootLatLong>(request, response =>
        //{
        //  var input = response.Content;
        //DrawMap(input);
        //});

        //        }

        //      public void DrawMap(string jsonString)
        //    {
        //      var jsonItem = JObject.Parse(jsonString);
        //    var mylat = jsonItem["Items"][0]["latitude"].ToString();
        //  lat = mylat;
        //var mylon = jsonItem["Items"][0]["longitude"].ToString();
        //lon = mylon;
        //var myuri = "geo:" + lat + "," + lon;
        //helloMap.helloLat = lat;
        //helloMap.helloLng = lon;
        //helloMap.hellobeachId = SharedObjects.mainId;
        //mapsButton.Click += delegate {
        //StartActivity(typeof(ShowMap));
        // };


        //}



    }


   


    




    public class SharedObjects
    {
        public static string mainName;
        public static int mainId;
    }


}