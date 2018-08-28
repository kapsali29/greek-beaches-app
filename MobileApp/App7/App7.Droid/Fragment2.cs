using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Macaw.UIComponents;
using Android.Graphics;
using RestSharp;
using Android.Gms.Maps;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Android.Gms.Maps.Model;
using Android.Locations;

namespace App7.Droid
{
    public class myMap : Fragment, IOnMapReadyCallback
    {
        LatLng location;
        CameraUpdate cameraUpdate;
        MapFragment mapFragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.basic, container, false);
            //var myActivity = (ItemContext)this.Activity;
            InitMap();
            return view;
        }
        public override void OnPause()
        {
            base.OnPause();
            if (mapFragment != null)
            {
                FragmentManager fragmentManager = FragmentManager;
                fragmentManager.BeginTransaction().Remove(mapFragment).Commit();
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            googleMap.MapType = GoogleMap.MapTypeNormal;
            googleMap.AddMarker(new MarkerOptions()
                .SetPosition(location)
                .SetTitle(SharedObjects.mainName));
            googleMap.MoveCamera(cameraUpdate);
        }

        public void InitMap() {
            var myActivity = (ItemContext)this.Activity;
            mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            //var mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map);
            mapFragment.GetMapAsync(this);
            var mylat = double.Parse(helloMap.helloLat);
            var mylong = double.Parse(helloMap.helloLng);
            var loc = new LatLng(mylat, mylong);
            location = loc;
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(16);
            //builder.Bearing(155);
            builder.Tilt(30);
            CameraPosition cameraPosition = builder.Build();
            var mycamera = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            cameraUpdate = mycamera;
        }
    }




    public class showPhotos : Fragment
    {
        public static MultiImageView image;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.ph, container, false);
            image = view.FindViewById<MultiImageView>(Resource.Id.imageView1);
            var myActivity = (ItemContext)this.Activity;
            MyClass mm = new MyClass();
            var myli = mm.getPhotos(SharedObjects.mainId);
            photocall(myli);
            return view;
        }

        //public void getPhotos(int idd) {
          //  var url = "http://192.168.17.1:5000/getPhotos/" + idd.ToString();
            //var client = new RestClient(url);
            //var request = new RestRequest(Method.GET);
            //client.ExecuteAsync<PhObject>(request, response =>
            //{
              //  var ph = response.Data.Items;
                //photocall(response.Data.Items);
           // });

        //}

        public void photocall(List<photoItem> li) {
            var photoarray = li.Select(item => item.photo).ToArray();
            image.LoadImageList(photoarray);
            image.SliderSelectedIcon = BitmapFactory.DecodeResource(Resources, Resource.Drawable.slider_blt_grn);
            image.SliderUnselectedIcon = BitmapFactory.DecodeResource(Resources, Resource.Drawable.slider_blt_trans);
            image.SetSliderIconDimensions(50, 50);
            image.DownloadedImageSampleSize = 1;
            var myActivity = (ItemContext)this.Activity;
            image.ImagesLoaded += (sender, e) =>
            {   // Loads the first image in the list
                myActivity.RunOnUiThread(image.LoadImage);
            };

        }



    }


    public class beachDesc :    ListFragment
    {
        TextView text;
        TextView adText;
        TextView namest;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.desc, container, false);
            namest = view.FindViewById<TextView>(Resource.Id.textView1);
            adText = view.FindViewById<TextView>(Resource.Id.sss);
            text = view.FindViewById<TextView>(Resource.Id.message_scroll);
            text.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            MyClass mmm = new MyClass();
            var mmml = mmm.readDesc(SharedObjects.mainId);
            desc(mmml);
            var jsm = mmm.showAddress(SharedObjects.mainId);
            getAd(jsm);

            var mynames = mmm.getAlter(SharedObjects.mainId);
            getNames(mynames);
            return view;
        }

        //public void readDesc(int idd)
        //{

          //  var url = "http://192.168.17.1:5000/getDetails/" + idd.ToString();
            //var client = new RestClient(url);
            //var request = new RestRequest(Method.GET);
            //client.ExecuteAsync<dRootObject>(request, response =>
            //{
              //  desc(response.Data.Items);
            //});

        //}
        public void desc(List<dItem> li)
        {
            var darray = li.Select(item => item.description).ToArray();
            var dsource = li.Select(item => item.source).ToArray();
            var perigrafh = "";
            var comments = "";
            var myActivity = (ItemContext)this.Activity;
            for (var i = 0; i < darray.Length; i++)
            {
                if (dsource[i] == "Foursquare")
                {
                    comments = comments + "\n" + "\n" + "\n" + darray[i];
                }
                else
                {
                    perigrafh = perigrafh + "\n" + "\n" + darray[i];
                }

            }
            myActivity.RunOnUiThread(() => {
            text.Text = perigrafh;
            });
        }

        //public void showAddress(int idd)
        //{
          //  var url = "http://192.168.17.1:5000/getAddress/" + idd.ToString();
            //var client = new RestClient(url);
            //var request = new RestRequest(Method.GET);
            //client.ExecuteAsync<adRootObject>(request, response =>
            //{
              //  var input = response.Content;
                //getAd(input);
            //});
        //}
        public void getAd(string jsonString)
        {
            var jsonItem = JObject.Parse(jsonString);
            var myAddress = jsonItem["Items"][0]["address"].ToString();
            var myActivity = (ItemContext)this.Activity;
            myActivity.RunOnUiThread(() => {
                adText.Text = myAddress;
            });
        }

        //public void getAlter(int idd)
        //{
          //  var url = "http://192.168.17.1:5000/getAlternateNames/" + idd.ToString();
            //var client = new RestClient(url);
            //var request = new RestRequest(Method.GET);
            //client.ExecuteAsync<nRootObject>(request, response =>
            //{
              //  getNames(response.Data.Items);
            //});
        //}
        public void getNames(List<nItem> d)
        {
            var namesArray = d.Select(item => item.Names).ToArray();
            var myActivity = (ItemContext)this.Activity;
            var myAdapter = new ArrayAdapter<String>(myActivity, Android.Resource.Layout.SimpleListItem1, namesArray);

            myActivity.RunOnUiThread(() =>
            {
                namest.Text = "Alternate Names";
                ListAdapter = myAdapter;
            });
        }



    }

    public class beachComments : Fragment
    {
        TextView comments;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.beachComments, container, false);
            comments = view.FindViewById<TextView>(Resource.Id.me_scroll);
            comments.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            MyClass mn = new MyClass();
            beachComm(mn.readComments(SharedObjects.mainId));
            return view;
        }

        //public void readComments(int idd)
        //{

          //  var url = "http://192.168.17.1:5000/getDetails/" + idd.ToString();
            //var client = new RestClient(url);
            //var request = new RestRequest(Method.GET);
            //client.ExecuteAsync<dRootObject>(request, response =>
            //{
              //  beachComm(response.Data.Items);
            //});

        //}
        public void beachComm(List<dItem> li)
        {
            var darray = li.Select(item => item.description).ToArray();
            var dsource = li.Select(item => item.source).ToArray();
            var perigrafh = "";
            var co = "";
            var myActivity = (ItemContext)this.Activity;
            for (var i = 0; i < darray.Length; i++)
            {
                if (dsource[i] == "Foursquare")
                {
                    co = co + "\n" + "\n" + "\n" + darray[i];
                }
                else
                {
                    perigrafh = perigrafh + "\n" + "\n" + darray[i];
                }

            }
            myActivity.RunOnUiThread(() => {
                comments.Text = co;
            });
        }


    }


    

   

}