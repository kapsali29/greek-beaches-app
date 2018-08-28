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
project = conn[77608]
jobs = project.jobs(state='finished')
jobs_id = [x.id for x in jobs]
print jobs_id
job = project.job(u'77608/1/1')
source = 'e-zakynthos.com'
servername = "localhost"
username = "root"
password = ""
dbname = "database"
db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
cursor = db.cursor()
z = u'\u0396\u03ac\u03ba\u03c5\u03bd\u03b8\u03bf\u03c2'
for item in job.items():
	if u'latlon' not in item.keys():
		continue
	else:
		item[u'name'] = ''.join(item[u'name']).replace(z,'')
		item[u'name'] = item[u'name'].replace('e-', '')
		item[u'name'] = item[u'name'].strip(" ")
		print item[u'name']
		print str(item[u'latlon'][0])
		inp = str(item[u'latlon'][0])
		startlat = inp.find('@')
		endlat = inp.find(',')
		lat = float(inp[startlat+1:endlat])
		print lat
		endlng = inp.find('z')
		if '15z' in str(item[u'latlon'][0]):
			lng = inp[endlat+1:endlng].replace(',15','')
		elif '16z' in str(item[u'latlon'][0]):
			lng = inp[endlat+1:endlng].replace(',16','')
		elif '14z' in str(item[u'latlon'][0]):
			lng = inp[endlat+1:endlng].replace(',14','')
		else:
			lng = inp[endlat+1:endlng].replace(',13','')
		lng = float(lng)
		print lng
		f = []
		if u'variants' in item.keys():
			print item[u'variants']
			for data in item[u'variants']:
				for ph in data[u'photos']:
					f.append(ph)
		else:
			f = []
		#r = u"Ζάκυνθος, Ιόνια Νησιά"
		geolocator = Nominatim()
		location = geolocator.reverse([lat, lng], timeout=10)
		ad = location.address
		if 'state_district' in location.raw['address'].keys():
			state_district = location.raw['address']['state_district']
		else:
			state_district = ' '
		r = state_district
		time.sleep(2)
		if elegxos_paralias(item[u'name'], lat, lng, ad, item[u'description'], source, f) != 0:
			print 'Inserted to beachplus' 
		else:
			sql1 = """INSERT INTO beach (name,lat,lon,region) VALUES (%s,%s,%s,%s)"""
			args1 = (item[u'name'], lat, lng, r)
			cursor.execute(sql1, args1)
			sql3 = """SELECT MAX(id) FROM beach"""
			cursor.execute(sql3)
			result = cursor.fetchone()
			sql2 = """INSERT INTO beachplus(address ,source, beach_id, alternate) VALUES (%s, %s, %s, %s)"""
			args2 = (ad,source, result, item[u'name'])
			cursor.execute(sql2, args2)
			sql4 = """INSERT INTO description(beach_id, text, source) VALUES(%s, %s, %s)"""
			args4 = (result, item[u'description'], source)
			cursor.execute(sql4, args4)
			sqlp = """INSERT INTO photos(beach_id, ph) VALUES(%s, %s)"""
			for string in f:
				cursor.execute(sqlp, (result, string))

			db.commit()
			print 'end'