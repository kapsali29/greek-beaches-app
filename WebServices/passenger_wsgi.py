#!/usr/bin/env python
# -*- coding: utf-8 -*-
import sys, os
import MySQLdb
INTERP = os.path.join(os.environ['HOME'], 'bin', 'python')
if sys.executable != INTERP:
    os.execl(INTERP, INTERP, *sys.argv)
sys.path.append(os.getcwd())

from flask import Flask, jsonify
from distance import haversine

application = Flask(__name__)

@application.route('/GetAllItems', methods=['GET'])
def getAll():
    servername = "mysql.dream.mmlab.gr"
    username = "mmlab_user"
    password = "mml@b#student"
    dbname = "dreammmlab_kapsali"
    queryAll = "SELECT name, id, photo FROM beach"
    db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
    cur = db.cursor()
    json_return = {}
    try:
        cur.execute(queryAll)
        listall = cur.fetchall()
        items = []
        for data in listall:
            j={'name':data[0], 'id':data[1], 'photo':data[2]}
            items.append(j)
    except Exception as error:
        return {'error':str(error)}
    finally:
        cur.close()
        db.close()
    return jsonify(Items = items)
@application.route('/get_beach/<idd>', methods=['GET'])
def get(idd):
    servername = "mysql.dream.mmlab.gr"
    username = "mmlab_user"
    password = "mml@b#student"
    dbname = "dreammmlab_kapsali"
    q_list_one = "SELECT name FROM beach WHERE id = %s"
    db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
    cur = db.cursor()
    json_return = {}
    try:
        cur.execute(q_list_one, (idd,))
        r_list_one = cur.fetchall()
        items = []
        for data in r_list_one:
            i = {'name':data[0]}
            items.append(i)
    except Exception as error:
        return {'error':str(error)}
    finally:
        cur.close()
        db.close()
    return jsonify(Items = items)
@application.route('/getCoordinates/<idd>', methods=['GET'])
def getlatlon(idd):
    servername = "mysql.dream.mmlab.gr"
    username = "mmlab_user"
    password = "mml@b#student"
    dbname = "dreammmlab_kapsali"
    querylatlon = "SELECT lat, lon, name, id FROM beach WHERE id = %s"
    db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
    cur = db.cursor()
    json_return = {}
    try:
        cur.execute(querylatlon, (idd,))
        listcoords = cur.fetchall()
        items = []
        for data in listcoords:
            jc = {'latitude':float(data[0]), 'longitude':float(data[1]), 'name':data[2], 'id': data[3]}
            items.append(jc)
    except Exception as error:
        return {'Error':str(error)}
    finally:
        cur.close()
        db.close()
    return jsonify(Items = items)
@application.route('/getAlternateNames/<idd>', methods = ['GET'])
def getAltNames(idd):
    servername = "mysql.dream.mmlab.gr"
    username = "mmlab_user"
    password = "mml@b#student"
    dbname = "dreammmlab_kapsali"
    queryalt = "SELECT alternate, source, beach_id FROM beachplus WHERE beach_id = %s"
    db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
    cur = db.cursor()
    json_return = {}
    try:
        cur.execute(queryalt, (idd,))
        listalt = cur.fetchall()
        items = []
        for data in listalt:
            jalt = {'Names':data[0], 'Source': data[1], 'Id': data[2]}
            items.append(jalt)
    except Exception as error:
        return {'Error':str(error)}
    finally:
        cur.close()
        db.close()
    return jsonify(Items = items)
@application.route('/getAddress/<idd>', methods = ['GET'])
def getAd(idd):
    servername = "mysql.dream.mmlab.gr"
    username = "mmlab_user"
    password = "mml@b#student"
    dbname = "dreammmlab_kapsali"
    queryforadd = "SELECT address, alternate, beach_id FROM beachplus WHERE beach_id = %s"
    db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
    cur = db.cursor()
    json_return = {}
    try:
        cur.execute(queryforadd, (idd,))
        listad = cur.fetchone()
        items = []
        #for data in listad:
        jad = {'address':listad[0], 'Names':listad[1], 'Id':listad[2]}
        items.append(jad)
    except Exception as error:
        return {'Error':str(error)}
    finally:
        cur.close()
        db.close()
    return jsonify(Items = items)
@application.route('/getDetails/<idd>', methods = ['GET'])
def getD(idd):
    servername = "mysql.dream.mmlab.gr"
    username = "mmlab_user"
    password = "mml@b#student"
    dbname = "dreammmlab_kapsali"
    queryfordesc = "SELECT text, source, beach_id FROM description WHERE beach_id = %s"
    db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
    cur = db.cursor()
    json_return = {}
    try:
        cur.execute(queryfordesc, (idd,))
        listdesc = cur.fetchall()
        items = []
        for data in listdesc:
            jdesc = {'description':data[0], 'source':data[1], 'id':data[2]}
            items.append(jdesc)
    except Exception as error:
        return {'Error':str(error)}
    finally:
        cur.close()
        db.close()
    return jsonify(Items = items)
@application.route('/getPhotos/<idd>', methods = ['GET'])
def getPh(idd):
    servername = 'mysql.dream.mmlab.gr'
    username = 'mmlab_user'
    password = 'mml@b#student'
    dbname = 'dreammmlab_kapsali'
    queryPhoto = "SELECT ph, beach_id FROM photos WHERE beach_id = %s"
    db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
    cur = db.cursor()
    json_return = {}
    try:
        cur.execute(queryPhoto, (idd,))
        listph = cur.fetchall()
        items = []
        for data in listph:
            jph = {'photo':data[0], 'id':data[1]}
            items.append(jph)
    except Exception as error:
        return {'Error':str(error)}
    finally:
        cur.close()
        db.close()
    return jsonify(Items = items)
@application.route('/getRegionBeaches/<pointer>', methods=['GET'])
def getRbeaches(pointer):
    servername = "mysql.dream.mmlab.gr"
    username = "mmlab_user"
    password = "mml@b#student"
    dbname = "dreammmlab_kapsali"
    region = ["Περιφέρεια Πελοποννήσου", "Περιφέρεια Κρήτης", "Περιφέρεια Ιόνιων νησιών", "Περιφέρεια Νοτίου Αιγαίου", "Περιφέρεια Ανατολικής Μακεδονίας και Θράκης", "Περιφέρεια Στερεάς Ελλάδας", "Περιφέρεια Βόρειου Αιγαίου", "Περιφέρεια Αττικής", "Περιφέρεια Θεσσαλίας", "Περιφέρεια Κεντρικής Μακεδονίας", "Περιφέρεια Ηπείρου", "Περιφέρεια Δυτικής Ελλάδας" ]
    queryAll = "SELECT name, id, photo FROM beach WHERE region = %s"
    db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
    cur = db.cursor()
    i = int(pointer)
    json_return = {}
    try:
        cur.execute(queryAll, (region[i],))
        listall = cur.fetchall()
        items = []
        for data in listall:
            j={'name':data[0], 'id':data[1], 'photo':data[2]}
            items.append(j)
    except Exception as error:
        return {'error':str(error)}
    finally:
        cur.close()
        db.close()
    return jsonify(Items = items)
@application.route('/getNearBeaches/<mylat>/<mylong>', methods = ['GET'])
def getNear(mylat, mylong):
    servername = "mysql.dream.mmlab.gr"
    username = "mmlab_user"
    password = "mml@b#student"
    dbname = "dreammmlab_kapsali"
    myquery = "SELECT name, id, lat, lon, photo FROM beach"
    db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
    cur = db.cursor()
    json_return = {}
    try:
        cur.execute(myquery)
        items = []
        names = []
        lats = []
        lons = []
        ids = []
        havs = []
        phs = []
        results = cur.fetchall()
        for item in results:
            if (haversine(float(item[3]), float(item[2]), float(mylong), float(mylat)) <= 15):
                names.append(item[0])
                ids.append(int(item[1]))
                lats.append(float(item[2]))
                lons.append(float(item[3]))
                havs.append(haversine(float(item[3]), float(item[2]), float(mylong), float(mylat)))
                phs.append(item[4])
        i = len(names) - 1
        while i >=0:
            myjson = {'name':names[i], 'id':ids[i], 'latitude':lats[i], 'longitude':lons[i], 'distance':float(havs[i]), 'photo':phs[i]}
            items.append(myjson)
            i = i -1;
    except Exception as error:
        return {'Error':str(error)}
    finally:
        cur.close()
        db.close()
    return jsonify(Items = items)
if __name__ == '__main__':
    application.debug = True
    application.run(host='0.0.0.0')