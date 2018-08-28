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

namespace App7.Droid
{
    public class NearItems : ListFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.AllLayout, container, false);
            var sampleTextView = view.FindViewById<TextView>(Resource.Id.textView1);
            sampleTextView.Text = "Near to Me";
            checkMode.mode = 0;
            var myActivity = (MainActivity)this.Activity;
            myActivity.clearList();
            myActivity.AddressButton_OnClick();
            myActivity.ListView.ItemClick += myActivity.ListView_ItemClick;
            Button aButton = new Button(myActivity);
            aButton.Text = "Κοντινές Παραλίες στον Χάρτη";

            LinearLayout ll = view.FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            ll.AddView(aButton);
            aButton.Click += (sender, e) =>
            {
                Intent intent = new Intent(this.Activity, typeof(ShowMap));
                StartActivity(intent);
            };
            return view;
        }

    }
    public class regionItems : ListFragment
    {
        public string[] regionArray = { "Περιφέρεια Πελοποννήσου", "Περιφέρεια Κρήτης", "Περιφέρεια Ιόνιων νησιών", "Περιφέρεια Νοτίου Αιγαίου", "Περιφέρεια Ανατολικής Μακεδονίας και Θράκης", "Περιφέρεια Στερεάς Ελλάδας", "Περιφέρεια Βόρειου Αιγαίου", "Περιφέρεια Αττικής", "Περιφέρεια Θεσσαλίας", "Περιφέρεια Κεντρικής Μακεδονίας", "Περιφέρεια Ηπείρου", "Περιφέρεια Δυτικής Ελλάδας" };
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.AllLayout, container, false);
            var sampleTextView = view.FindViewById<TextView>(Resource.Id.textView1);
            sampleTextView.Text = "List of Regions";
            var myActivity = (MainActivity)this.Activity;
            myActivity.clearList();
            myActivity.ListAdapter = new ArrayAdapter<String>((MainActivity)this.Activity, Android.Resource.Layout.SimpleListItem1, regionArray);
            checkMode.mode = 1;
            myActivity.ListView.ItemClick += myActivity.ListView_ItemClick;
            return view;
        }

        public   void ListView_ItemClick1(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(RegionActivity));
            var pointer = e.Position;
            fromRegionFrag.regionPointer = pointer;
            StartActivity(intent);
        }
    }



    public class AllItems : ListFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.AllLayout, container, false);
            var sampleTextView = view.FindViewById<TextView>(Resource.Id.textView1);
            checkMode.mode = 0;
            var myActivity = (MainActivity)this.Activity;
            myActivity.clearTextLoc();
            sampleTextView.Text = "List of all beaches";
            ListAdapter = null;
            MyClass myclass = new MyClass();
            myclass.readItems();
            myActivity.RunOnUiThread(() =>
            {
                myActivity.ListAdapter = new myCustomAdapter(myActivity, FromAllItemClass.allItems);
            });
            myActivity.ListView.ItemClick += myActivity.ListView_ItemClick;
            return view;
        }
    }
        public class SearchMe : Fragment
        {
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                // Create your fragment here
            }

            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                base.OnCreateView(inflater, container, savedInstanceState);

                var view = inflater.Inflate(Resource.Layout.SearchLayout, container, false);
                AutoCompleteTextView textView = view.FindViewById<AutoCompleteTextView>(Resource.Id.autocomplete_country);
                
                var adapter = new ArrayAdapter<String>(this.Activity, Resource.Layout.list_item, toNear.mynames);
                checkMode.mode = 0;
                textView.Adapter = adapter;
                var myActivity = (MainActivity)this.Activity;
                myActivity.clearList();
                textView.ItemClick += TextView_ItemClick;
                return view;
            }
        public void TextView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var activity2 = new Intent(this.Activity, typeof(ItemContext));
            AutoCompleteTextView autoText = (AutoCompleteTextView)sender;
            var myText = autoText.Text;
            int index = Array.IndexOf(toNear.mynames, myText);
            var beachName = toNear.mynames[index];
            var beachId = toNear.myids[index];
            SharedObjects.mainName = beachName;
            SharedObjects.mainId = beachId;
            Intent intent = new Intent(this.Activity, typeof(ItemContext)).SetFlags(ActivityFlags.ReorderToFront); 
            StartActivity(intent);
            
        }


    }

    public class FromAllItemClass {
        public static List<Item> allItems;
        public static List<myItem> nearItems;
        public static List<tItem> regionItems;
    }

    }
