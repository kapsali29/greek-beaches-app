#!/usr/bin/python
#-*- coding: iso-8859-7 -*-
import json
import urllib2
import urllib
import string
import sys
import MySQLdb
from geopy.geocoders import Nominatim
reload(sys)
sys.setdefaultencoding('utf8')
from converting import remove_accents
from geopy.geocoders import Nominatim
response = urllib2.urlopen(' http://api.geonames.org/searchJSON?q=beach&country=GR&maxRows=1000&lang=el&username=kapsali')
json_object = json.load(response)
servername = "localhost"
username = "root"
password = ""
dbname = "database"
db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
cursor = db.cursor()
#print json_object
#print type(json_object)
#print json_object[ u'geonames']
for item in json_object[ u'geonames']:
	if item[u'countryCode'] == 'GR':
		print 'BEGIN'
		try:
			print item[ u'toponymName']
		except:
			print remove_accents(item[ u'toponymName'])
		print item[u'lat']
		print item[u'lng']
		print item[u'adminName1']
		try:
			name = item[u'name']
			print name
		except:
			name = remove_accents(item[u'name'])
			print name
		sql1 = """INSERT INTO beach(name, lat, lon, region) VALUES(%s, %s, %s, %s) """
		args1 = (name, item[u'lat'], item[u'lng'], item[u'adminName1'])
		cursor.execute(sql1, args1)
		geolocator = Nominatim()
		coord=(float(item[u'lat']),float(item[u'lng']))
		location = geolocator.reverse(coord)
		sql3 = """SELECT MAX(id) FROM beach"""
		cursor.execute(sql3)
		result = cursor.fetchone()
		sql2 = """INSERT INTO beachplus(address ,source, beach_id, alternate) VALUES (%s, %s, %s, %s)"""
		cursor.execute(sql2, (location.address, 'geonames.org', result, item[u'name']))
		db.commit()

	