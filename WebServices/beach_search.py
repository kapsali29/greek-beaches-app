def extract(p_city):
    # !/usr/bin/python
    # -*- coding: utf-8 -*-
    import MySQLdb
    import foursquare
    import json
    import time
    import os
    import sys
    import codecs, locale
    from tips import tip_details
    import unicodedata
    import sys, nltk, shutil
    import string
    import unicodedata
    import chardet
    from listing_ad import it_li
    from unidecode import unidecode
    from venue_info import get_venue_info
    from photo_info import get_venue_photo
    from check_entry import check_double_entry
    from ids import idd
    from geopy.geocoders import Nominatim
    reload(sys)
    sys.setdefaultencoding('utf-8')
    import sys
    sys.path.insert(0, 'C:\Users\home\greekbeachesapp')
    from sunthesis import elegxos_paralias
    #import greek_alphabet
    client = foursquare.Foursquare(client_id='0NKJU5YU3205AZXBGQWLDWRLREZRNBGWPDHYR2PXNFOFOGIY',
                                   client_secret='DIWIFQPLO0HLBBKOCYLAO1FRK5KA0DDGS1ZTVLN2RBSQST3E')
    plaz = client.venues.search(params={'query': 'Beach', 'near': p_city, 'categoryId': '4bf58dd8d48988d1e2941735'})
    servername = "localhost"
    username = "root"
    password = ""
    dbname = "database"

    for venues in plaz["venues"]:
        plaz_id = str(venues["id"])
        print type(plaz_id)
        plaz_name = venues["name"]
        plaz_lat = venues["location"]["lat"]
        print plaz_lat
        plaz_lng = venues["location"]["lng"]
        print plaz_lng
        print "Rating & Url\n"
        get_venue_info(plaz_id)
        print "Location\n"
        plaz_lat = float(venues["location"]["lat"])
        print plaz_lat
        plaz_lng = float(venues["location"]["lng"])
        print plaz_lng
        plaz_cc = str(venues["location"]["cc"])
        print plaz_cc
        print type(plaz_cc)
        ccs = ['gr','GR']
        ap = plaz_cc in ccs
        if ap == True:
            geolocator = Nominatim()
            location = geolocator.reverse([plaz_lat, plaz_lng], timeout=10)
            if 'state_district' in location.raw['address'].keys():
                state_district = location.raw['address']['state_district']
            else:
                state_district = ' '
            reg = state_district
            db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
            cursor = db.cursor()
            source = "Foursquare"
            re = idd(plaz_id)
            print re
            if re == True:
                print "this entry exists"
                continue
            else:
                if elegxos_paralias(plaz_name, plaz_lat, plaz_lng, location.address, plaz_id, 'Foursquare', plaz_id) !=0:
                    print 'Inserted to beachplus'
                    sql0 = """INSERT INTO tsek(pid) VALUES(%s)"""
                    args0 = ([plaz_id])
                    cursor.execute(sql0, args0)
                    db.commit()
                    
                    

                else:
                    sql0 = """INSERT INTO tsek(pid) VALUES(%s)"""
                    args0 = ([plaz_id])
                    cursor.execute(sql0, args0)
                    db.commit()
                    sql1 = """INSERT INTO beach(name, lat, lon, region) VALUES(%s, %s, %s, %s) """
                    region_b = reg 
                    args1 = (plaz_name, plaz_lat, plaz_lng, region_b)
                    cursor.execute(sql1, args1)
                    db.commit()
                    sql2 = """SELECT MAX(id) FROM beach"""
                    cursor.execute(sql2)
                    db.commit()
                    result = cursor.fetchone()
                    sql3 = """INSERT INTO beachplus(address ,source, beach_id, alternate) VALUES (%s, %s, %s, %s)"""
                    args3 = (location.address, "Foursquare", result, plaz_name)
                    cursor.execute(sql3, args3)
                    db.commit()
                    #print 'Tips\n'
                    if venues["stats"]["tipCount"] != 0:
                        #tip_details(venues["id"],plaz_name,result)
                        tip = client.venues.tips(venues["id"])
                        for item in tip["tips"]["items"]:
                            tip_text = item["text"]
                            sql6 = """INSERT INTO description(beach_id, text, source) VALUES(%s, %s, %s)"""
                            args6 = (result, tip_text, source)
                            cursor.execute(sql6, args6)
                        db.commit()
                        print 'End\n'
                    ph=client.venues.photos(plaz_id, params={'group': 'venue'})
                    for photo in ph["photos"]["items"]:
                        photo_url=photo["prefix"]+"width500"+photo["suffix"]
                        sqlph = """INSERT INTO photos (beach_id, ph) VALUES (%s, %s)"""
                        cursor.execute(sqlph, (result, photo_url))
                    db.commit()
                    # disconnect from server

            db.close()