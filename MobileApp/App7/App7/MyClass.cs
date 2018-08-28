using App7.Droid;
using App7.SQLite;
using Newtonsoft.Json.Linq;
using RestSharp;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace App7
{
	public class MyClass
	{
        public string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allbeach15.db3");
        public string dbPhPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "photosbyid2.db3");
        public string dbdesc = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "descbyid12.db3");
        public string dbalter = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "alternamesbyid12.db3");
        public string dbnear = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "neardb2.db3");
        public string dbreg = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "region11.db3");
        public string dbad = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ad1.db3");
        public string dblatlng = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "latlng1.db3");
        public string dbcom = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "comments1.db3");
        public MyClass ()
		{
		}

        public void readItems()
        {
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.CreateTable<beach>();
            var table = db.Table<beach>();
            if (table.Count() == 0)
            {
                var client = new RestClient("http://passenger.dream.mmlab.gr/GetAllItems");
                var request = new RestRequest(Method.GET);
                var response2 = client.Execute<RootObject>(request);
                callback(response2.Data.Items, 0);
            }
            else {
                var mylist = new List<Item>();
                foreach (var s in table) {
                    var data = new Item
                    {
                        id = s.beach_id,
                        name = s.name,
                        photo = s.photo
                    };
                    
                    mylist.Add(data);
                }
                callback(mylist, 1);
               
            }
        }
        public void callback(List<Item> d, int checkStock)
        {
            
            FromAllItemClass.allItems = d;
            MainActivity.names = d.Select(item => item.name).ToArray();
            MainActivity.idds = d.Select(item => item.id).ToArray();
            MainActivity.phs = d.Select(item => item.photo).ToArray();
            toNear.mynames = MainActivity.names;
            toNear.myids = MainActivity.idds;
            if (checkStock == 0)
            {
                ToDoAllItems myallItems = new ToDoAllItems();
                myallItems.DoGetAllBeaches(dbPath, MainActivity.names, MainActivity.idds, MainActivity.phs);
            }

        }


        public void getNearBeaches()
        {
            SQLiteConnection db = new SQLiteConnection(dbnear);
            db.CreateTable<near>();
            var table = db.Table<near>();
            var nnn = new getNearFSQL();
            var string1 = toNear.mylat.ToString().Replace(',', '.');
            var string2 = toNear.mylon.ToString().Replace(',', '.');
            var nearList = nnn.reachNear(dbnear, string1, string2);
            if (nearList.Count == 0)
            {
                var c = toNear.mylon;
                
                var url = "http://passenger.dream.mmlab.gr/getNearBeaches/" + string1 + "/" + string2;
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var response3 = client.Execute<myRootObject>(request);
                if (response3.Data == null || response3.Data.Items.Count ==0)
                {
                    var para_data = new List<myItem>();
                    var data1 = new myItem
                    {
                        id = 0,
                        distance = 0.0,
                        latitude = 0.0,
                        longitude = 0.0,
                        name = "unknown",
                        photo = ""
                    };
                    para_data.Add(data1);
                    near(para_data);
                }
                else
                {
                    nnn.fillTableNear(dbnear, response3.Data.Items, table, string1, string2);
                    near(response3.Data.Items);
                }
            }
            else {
                var result = new List<myItem>();
                foreach (var s in nearList) {
                    
                    var data = new myItem
                    {
                        id = s.beach_id,
                        distance = s.distance,
                        latitude = s.lat,
                        longitude = s.lon,
                        name = s.name,
                        photo = s.photo
                    };
                    result.Add(data);
                }
                near(result);
            }

        }

        public void near(List<myItem> li)
        {
            FromAllItemClass.nearItems = li;
            var myArray2 = li.Select(item => item.name).ToArray();
            var myArray3 = li.Select(item => item.id).ToArray();
            var mylats = li.Select(item => item.latitude).ToArray();
            var mylons = li.Select(item => item.longitude).ToArray();
            MainActivity.lats = mylats;
            MainActivity.lons = mylons;
            MainActivity.names = myArray2;
            MainActivity.idds = myArray3;
            helloMap.hnames = MainActivity.names;
            helloMap.hellolons = MainActivity.lons;
            helloMap.hellolats = MainActivity.lats;


        }



        public void getRegionBeaches()
        {
            SQLiteConnection db = new SQLiteConnection(dbreg);
            db.CreateTable<region>();
            var table = db.Table<region>();
            var rrr = new getRegionItems();
            var regList = rrr.getRegionFSQL(dbreg, fromRegionFrag.regionPointer);
            if (regList.Count == 0)
            {
                var url = "http://passenger.dream.mmlab.gr/getRegionBeaches/" + fromRegionFrag.regionPointer.ToString();
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var response4 = client.Execute<tRootObject>(request);
                rrr.fillTableregion(dbreg, response4.Data.Items, table, fromRegionFrag.regionPointer);
                regionCall(response4.Data.Items);
            }
            else {
                var result = new List<tItem>();
                foreach(var s in regList)
                {
                    var data = new tItem
                    {
                        id = s.beach_id,
                        name = s.name,
                        photo = s.photo
                    };
                    result.Add(data);
                }
                regionCall(result);
            }
        }
        public void regionCall(List<tItem> d)
        {
            FromAllItemClass.regionItems = d;
            var beachArray = d.Select(item => item.name).ToArray();
            RegionActivity.regAr = beachArray;
            var beachIdsArray = d.Select(item => item.id).ToArray();
            RegionActivity.regId = beachIdsArray;
        }

        public void readLatLng(int idd)
        {
            SQLiteConnection db = new SQLiteConnection(dblatlng);
            db.CreateTable<latlng>();
            var table = db.Table<latlng>();
            var ltln = new getCoordsFSQL();
            var latlngList = ltln.getlatlngFromTable(idd, dblatlng);
            if (latlngList.Count() == 0)
            {
                var url = "http://passenger.dream.mmlab.gr/getCoordinates/" + idd.ToString();
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var response5 = client.Execute<RootLatLong>(request);
                ltln.fillTablelatlng(dblatlng, response5.Content, table);
                DrawMap(response5.Content);
            }
            else
            {
                var list = new List<LatlongItem>();
                foreach (var s in latlngList)
                {
                    var data = new LatlongItem
                    {
                        id = s.beach_id,
                        latitude = s.lat,
                        longitude = s.lon,
                        name = s.name
                    };
                    list.Add(data);
                }
                var item = new RootLatLong
                {
                    Items = list
                };
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                DrawMap(json);
            }

        }

        public void DrawMap(string jsonString)
        {
            var jsonItem = JObject.Parse(jsonString);
            var mylat = jsonItem["Items"][0]["latitude"].ToString();
            ItemContext.lat = mylat;
            var mylon = jsonItem["Items"][0]["longitude"].ToString();
            ItemContext.lon = mylon;
            var myuri = "geo:" + ItemContext.lat + "," + ItemContext.lon;
            helloMap.helloLat = ItemContext.lat;
            helloMap.helloLng = ItemContext.lon;
            helloMap.hellobeachId = SharedObjects.mainId;
            //mapsButton.Click += delegate {
            //StartActivity(typeof(ShowMap));
            // };


        }

        public List<photoItem>  getPhotos(int idd)
        {
            SQLiteConnection dbph = new SQLiteConnection(dbPhPath);
            dbph.CreateTable<photos>();
            var table = dbph.Table<photos>();
            var gg = new getPhotos();
            var phlist = gg.getPhotoById(idd, dbPhPath);
            if (phlist.Count() == 0)
            {
                var url = "http://passenger.dream.mmlab.gr/getPhotos/" + idd.ToString();
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var response6 = client.Execute<PhObject>(request);
                gg.accessPhoto(dbPhPath, response6.Data.Items, table);
                return response6.Data.Items;
            }
            else {
                var result = new List<photoItem>();
                foreach (var s in phlist) {
                    var data = new photoItem
                    {
                        id = s.ph_id,
                        photo = s.photo
                    };
                    result.Add(data);
                }
                return result;
            }
            
        }

        public List<dItem>  readDesc(int idd)
        {
            SQLiteConnection db = new SQLiteConnection(dbdesc);
            db.CreateTable<desc>();
            var table = db.Table<desc>();
            var ddd = new getDescription();
            var desclist = ddd.getDescById(idd, dbdesc);
            if (desclist.Count() == 0)
            {
                var url = "http://passenger.dream.mmlab.gr/getDetails/" + idd.ToString();
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var response7 = client.Execute<dRootObject>(request);
                if ( response7.Data == null || response7.Data.Items.Count == 0)
                {
                    var list = new List<dItem>();
                    var data = new dItem
                    {
                        description = "Δεν υπάρχει περιγραφή",
                        id = 0,
                        source = "no"
                    };
                    list.Add(data);
                    ddd.fillTable(dbdesc, list, table);
                    return list;
                }
                else
                {
                    ddd.fillTable(dbdesc, response7.Data.Items, table);
                    return response7.Data.Items;
                }
            }
            else
            {
                var result = new List<dItem>();
                foreach (var s in desclist)
                {
                    var data = new dItem
                    {
                        id = s.beach_id,
                        description = s.description,
                        source = s.source
                    };
                    result.Add(data);
                }
                return result;

            }
        }


        public string showAddress(int idd)
        {
            SQLiteConnection db = new SQLiteConnection(dbad);
            db.CreateTable<address>();
            var table = db.Table<address>();
            var aaaa = new getAdSQL();
            var adList = aaaa.getaddressFromTable(idd, dbad);
            if (adList.Count() == 0)
            {
                var url = "http://passenger.dream.mmlab.gr/getAddress/" + idd.ToString();
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var response8 = client.Execute<adRootObject>(request);
                aaaa.fillTableAd(dbad, response8.Content, table);
                return response8.Content;
            }
            else {
                
                var items = new List<adItem>();
                
                foreach (var s in adList)
                {
                    var data = new adItem
                    {
                        Id = s.beach_id,
                        Names = s.names,
                        address = s.ad
                    };
                    items.Add(data);
                }
                var item = new adRootObject
                {
                    Items = items
                };
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                return json;


            }
        }

        public List<nItem> getAlter(int idd)
        {
            SQLiteConnection db = new SQLiteConnection(dbalter);
            db.CreateTable<alter>();
            var table = db.Table<alter>();
            var alll = new getAlterNames();
            var alterlist = alll.getAltById(idd, dbalter);
            if (alterlist.Count() == 0)
            {
                var url = "http://passenger.dream.mmlab.gr/getAlternateNames/" + idd.ToString();
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var response9 = client.Execute<nRootObject>(request);
                alll.fillTableNames(dbalter, response9.Data.Items, table);
                return response9.Data.Items;
            }
            else
            {
                var result = new List<nItem>();
                foreach (var s in alterlist) {
                    var data = new nItem
                    {
                        Id = s.beach_id,
                        Names = s.names,
                        Source = s.source

                    };
                    result.Add(data);
                }
                return result;
            }
        }

        public List<dItem> readComments(int idd)
        {
            SQLiteConnection db = new SQLiteConnection(dbcom);
            db.CreateTable<comments>();
            var table = db.Table<comments>();
            var cocc = new getComments();
            var commentsList = cocc.getComById(idd, dbcom);
            if (commentsList.Count == 0)
            {
                var url = "http://passenger.dream.mmlab.gr/getDetails/" + idd.ToString();
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var response10 = client.Execute<dRootObject>(request);
                if ( response10.Data == null|| response10.Data.Items.Count == 0)
                {
                    var list = new List<dItem>();
                    var data = new dItem
                    {
                        description = "Δεν υπάρχουν σχόλια",
                        id = 0,
                        source = "no"
                    };
                    list.Add(data);
                    cocc.fillComments(dbcom, list, table);
                    return list;
                }
                else
                {
                    cocc.fillComments(dbcom, response10.Data.Items, table);
                    return response10.Data.Items;
                }
            }
            else
            {
                var result = new List<dItem>();
                foreach (var s in commentsList)
                {
                    var data = new dItem
                    {
                        id = s.beach_id,
                        description = s.description,
                        source = s.source
                    };
                    result.Add(data);
                }
                return result;

            }
        }







    }

    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
    }

    public class RootObject
    {
        public List<Item> Items { get; set; }
    }

    public class myItem
    {
        public double distance { get; set; }
        public int id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
    }

    public class myRootObject
    {
        public List<myItem> Items { get; set; }
    }

    public class tItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
    }

    public class tRootObject
    {
        public List<tItem> Items { get; set; }
    }


    public class RootLatLong
    {
        public List<LatlongItem> Items { get; set; }
    }

    public class LatlongItem
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }


    public class photoItem
    {
        public string photo { get; set; }
        public int id { get; set; }
    }

    public class PhObject
    {
        public List<photoItem> Items { get; set; }
    }

    public class dItem
    {
        public string description { get; set; }
        public string source { get; set; }
        public int id { get; set; }
    }

    public class dRootObject
    {
        public List<dItem> Items { get; set; }
    }

    public class adItem
    {
        public string Names { get; set; }
        public string address { get; set; }
        public int Id { get; set; }
    }

    public class adRootObject
    {
        public List<adItem> Items { get; set; }
    }

    public class nItem
    {
        public string Names { get; set; }
        public string Source { get; set; }
        public int Id { get; set; }
    }

    public class nRootObject
    {
        public List<nItem> Items { get; set; }
    }


}

