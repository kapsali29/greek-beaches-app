# !/usr/bin/python
# -*- coding: utf-8 -*-
import MySQLdb
import sys
from geopy.exc import GeocoderTimedOut
sys.path.insert(0, 'C:\Users\home\greekbeachesapp')
from sunthesis import elegxos_paralias
from scrapinghub import Connection
from geopy.geocoders import Nominatim
import time
conn = Connection('57c54e0d05fb44439a9fc0887fb6ab3e')
print conn
print conn.project_ids()
project = conn[77669]
jobs = project.jobs(state='finished')
jobs_id = [x.id for x in jobs]
print jobs_id
job = project.job(u'77669/1/1')
servername = "localhost"
username = "root"
password = ""
dbname = "database"
source = 'cleanbeaches.gr'
db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
cursor = db.cursor()
for item in job.items():
	if item[u'url'] == 'http://www.cleanbeaches.gr/':
		continue
	else:
		inp = str(item[u'latlon'][0])
		startlat = inp.find('@')
		endlat = inp.find(',')
		lat = float(inp[startlat+1:endlat])
		print lat
		endlng = inp.find('z')
		lng = float(inp[endlat+1:endlng].replace(',16',''))
		print lng
		photos = []
		#r = u"Αττική"
		geolocator = Nominatim()
		location = geolocator.reverse([lat, lng], timeout=10)
		ad = location.address
		if 'state_district' in location.raw['address'].keys():
			state_district = location.raw['address']['state_district']
		else:
			state_district = ' '
		r = state_district
		time.sleep(1)
		if u'sugkrish' in item.keys():
			desc = item[u'katalhlothta'][0] + ' ' + item[u'metrhseis'][0] + ' ' + item[u'sugkrish'][0]
		else:
			desc = item[u'katalhlothta'][0] + ' ' + item[u'metrhseis'][0]
		if elegxos_paralias(item[u'name'][0], lat, lng, ad, desc, source, photos) != 0:
			print 'Inserted to beachplus'
		else:
			sql1 = """INSERT INTO beach (name,lat,lon,region) VALUES (%s,%s,%s,%s)"""
			args1 = (item[u'name'][0], lat, lng, r)
			cursor.execute(sql1, args1)
			sql3 = """SELECT MAX(id) FROM beach"""
			cursor.execute(sql3)
			result = cursor.fetchone()
			sql2 = """INSERT INTO beachplus(address ,source, beach_id, alternate) VALUES (%s, %s, %s, %s)"""
			args2 = (ad,source, result, item[u'name'][0])
			cursor.execute(sql2, args2)
			sql4 = """INSERT INTO description(beach_id, text, source) VALUES(%s, %s, %s)"""
			args4 = (result, desc, source)
			cursor.execute(sql4, args4)
			db.commit()
	