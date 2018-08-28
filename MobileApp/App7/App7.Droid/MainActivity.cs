using System;
using Android.Locations;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using RestSharp;
using System.Linq;
using Android.Util;

namespace App7.Droid
{
    [Activity(Label = "App7.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity, ILocationListener
    {
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        Location _currentLocation;
    
        LocationManager _locationManager;
        string _locationProvider;
        TextView _locationText;
        public static string[] names;
        public static int[] idds;
        public static string[] phs;
        public static double[] lats;
        public static double[] lons;
        public void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation == null)
            {
                _locationText.Text = "Unable to determine your location. Try again in a short while.";
            }
            else
            {
                toNear.mylat = _currentLocation.Latitude.ToString();
                toNear.mylon = _currentLocation.Longitude.ToString();

                _locationText.Text = string.Format("{0:f6},{1:f6}", _currentLocation.Latitude, _currentLocation.Longitude);
            }
        }
        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            Log.Debug(TAG, "{0}, {1}", provider, status);
        }



        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            _locationText = FindViewById<TextView>(Resource.Id.location_text);
             //textView = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);
            AddTab("Beaches", new AllItems());
            AddTab("Near To Me", new NearItems());
            AddTab("Search", new SearchMe());
            AddTab("Regions", new regionItems());
            InitializeLocationManager();

        }
        public void clearTextLoc() {
            _locationText.Text = "";
        }

        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + _locationProvider + ".");
        }

        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
            Log.Debug(TAG, "Listening for location updates using " + _locationProvider + ".");
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
            Log.Debug(TAG, "No longer listening for location updates.");
        }

        public void AddressButton_OnClick()
        {
            if (_currentLocation == null)
            {
                _locationText.Text = "Can't determine the current location. Try again in a few minutes.";
                return;
            }
            toNear.mylat = _currentLocation.Latitude.ToString();
            toNear.mylon = _currentLocation.Longitude.ToString();
            MyClass myclass = new MyClass();
            myclass.getNearBeaches();
            RunOnUiThread(() =>
            {
                ListAdapter = new NearAdapter(this, FromAllItemClass.nearItems);
            });
        }
        void AddTab(string tabText, Fragment fragment)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);

            // must set event handler for replacing tabs tab
            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e) {
                e.FragmentTransaction.Replace(Resource.Id.frameLayout1, fragment);
            };

            this.ActionBar.AddTab(tab);
        }

        public void TextView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var activity2 = new Intent(this, typeof(ItemContext));
            var beachName = names[e.Position];
            var beachId = idds[e.Position];
            SharedObjects.mainName = beachName;
            SharedObjects.mainId = beachId;
            StartActivity(typeof(ItemContext));
        }

        public void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (checkMode.mode == 1)
            {
                var pointer = e.Position;
                fromRegionFrag.regionPointer = pointer;
                var intent = new Intent(this, typeof(RegionActivity))
                         .SetFlags(ActivityFlags.ReorderToFront);
                StartActivity(intent);
            }
            else {
                var activity2 = new Intent(this, typeof(ItemContext));
                var beachName = names[e.Position];
                var beachId = idds[e.Position];
                SharedObjects.mainName = beachName;
                SharedObjects.mainId = beachId;
                var intent = new Intent(this, typeof(ItemContext))
                        .SetFlags(ActivityFlags.ReorderToFront);
                StartActivity(intent);
            }
        }

        public void clearList()
        {
            ListAdapter = null;

        }

        //public void getNearBeaches()
        //{
        //  var c = toNear.mylon;
        // var string1 = toNear.mylat.ToString().Replace(',', '.');
        //var string2 = toNear.mylon.ToString().Replace(',', '.');
        //var url = "http://192.168.17.1:5000/getNearBeaches/" + string1 + "/" + string2;
        //var client = new RestClient(url);
        //var request = new RestRequest(Method.GET);
        //client.ExecuteAsync<myRootObject>(request, response =>
        //{
        //   near(response.Data.Items);
        //});

        //        }

        //      public void near(List<myItem> li)
        //    {
        //      var myArray2 = li.Select(item => item.name).ToArray();
        //    var myArray3 = li.Select(item => item.id).ToArray();
        //  var mylats = li.Select(item => item.latitude).ToArray();
        //var mylons = li.Select(item => item.longitude).ToArray();
        //lats = mylats;
        //lons = mylons;
        //names = myArray2;
        //idds = myArray3;
        //helloMap.hnames = names;
        //helloMap.hellolons = lons;
        //helloMap.hellolats = lats;
        //var myAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, myArray2);

        //RunOnUiThread(() => {
        //  ListAdapter = new NearAdapter(this, li);
        //});


    }

       // public void readItems()
        //{
          //  var client = new RestClient("http://192.168.1.2:5000/GetAllItems");
            //var request = new RestRequest(Method.GET);


           // client.ExecuteAsync<RootObject>(request, response =>
            //{
              //  callback(response.Data.Items);
            //});

        //}

//        public void callback(List<Item> d)
  //      {


    //        var nlist = new List<Item>();
            //foreach (var name in d) {
            //  nlist.Add(name);
            //}
      //      var myArray = d.Select(item => item.name).ToArray();
        //    var myArray2 = d.Select(item => item.id).ToArray();
          //  names = myArray;
            //idds = myArray2;
            //toNear.mynames = myArray;
            //toNear.myids = myArray2;
            //var myAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, myArray);
            //var adapter2 = new ArrayAdapter<String>(this, Resource.Layout.list_item, names);

            
            //RunOnUiThread(() => {
              //  ListAdapter = new myCustomAdapter(this, d);
                //textView.Adapter = adapter2;
            //});

            //        li.Adapter = listadapter;

        //




    public class toNear
    {
        public static string mylat;
        public static string mylon;
        public static string[] mynames;
        public static int[] myids;
    }


    

    public class checkMode {
        public static int mode;
    }

}