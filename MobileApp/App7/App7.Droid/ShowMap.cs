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
using Android.Support.V4.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;


namespace App7.Droid
{
    [Activity(Label = "App7.Droid", Icon = "@drawable/icon")]
    public class ShowMap : FragmentActivity, IOnMapReadyCallback
    {
        CameraUpdate cu;
        LatLng location;
        LatLngBounds bounds;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.showMap);
            var mapFragment =(SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
            LatLngBounds.Builder b = new LatLngBounds.Builder();
            for (var j=0; j< helloMap.hellolats.Length; j++)
            {
                b.Include(new LatLng(helloMap.hellolats[j], helloMap.hellolons[j]));
            }
            bounds = b.Build();

            cu = CameraUpdateFactory.NewLatLngBounds(bounds, 50, 50, 10);
        }

        public void OnMapReady(GoogleMap googleMap)
        {


            googleMap.MapType = GoogleMap.MapTypeNormal;

            if (helloMap.hellolats.Length == 0)
            {
                googleMap.AddMarker(new MarkerOptions()
                    .SetPosition(new LatLng(50.897778, 3.013333)));
            }
            else
            {

                for (var i = 0; i < helloMap.hellolats.Length; i++)
                {
                    googleMap.AddMarker(new MarkerOptions()
                        .SetPosition(new LatLng(helloMap.hellolats[i], helloMap.hellolons[i]))
                        .SetTitle(helloMap.hnames[i]));

                }
                googleMap.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(bounds.Center, 11));

            }
        }

        


    }

    public class helloMap {
        public static string helloLat;
        public static string helloLng;
        public static int hellobeachId;
        public static double[] hellolats;
        public static double[] hellolons;
        public static string[] hnames;
    }

    
}