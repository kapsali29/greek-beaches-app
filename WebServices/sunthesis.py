def elegxos_paralias(sname, slat, slon, addr, desc, s, ph):
	# !/usr/bin/python
	#-*- coding: iso-8859-7 -*-
	import MySQLdb
	import foursquare
	import sys
	reload(sys)  # Reload does the trick!
	sys.setdefaultencoding('cp737')
	from para import krithrio_omoiothtas
	from distance import haversine
	from insert import goToTsek
	servername = "localhost"
	username = "root"
	password = ""
	dbname = "database"
	client = foursquare.Foursquare(client_id='0NKJU5YU3205AZXBGQWLDWRLREZRNBGWPDHYR2PXNFOFOGIY',
                                   client_secret='DIWIFQPLO0HLBBKOCYLAO1FRK5KA0DDGS1ZTVLN2RBSQST3E')
	db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
	cursor = db.cursor()
	sql1 = """SELECT id, lat, lon, name FROM beach"""
	cursor.execute(sql1)
	results = cursor.fetchall()
	lats = []
	ids = []
	names = []
	lons = []
	for row in results:
		id = row[0]
		lat = row[1]
		lon = row[2]
		name = row[3]
		lats.append(lat)
		names.append(name)
		lons.append(lon)
		ids.append(id)
	lats = [float(x) for x in lats]
	lons = [float(x) for x in lons]
	ids = [int(x) for x in ids]
	i = len(names)-1
	simularity = 0
	while i >=0:
		if haversine(slon, slat, lons[i], lats[i]) <=1 and krithrio_omoiothtas(names[i], sname) >= 0.80 or haversine(slon, slat, lons[i], lats[i]) <0.100:
			print 'simularity here'
			print sname
			print names[i]
			sql2 = """INSERT INTO beachplus (address, source, beach_id, alternate) VALUES (%s, %s, %s, %s)"""
			args2 = (addr,s,ids[i],sname)
			cursor.execute(sql2, args2)
			if s == 'terrabook.com':
				sqlp = """INSERT INTO photos(beach_id, ph) VALUES (%s, %s)"""
				if ph != []:
					for string in ph:
						cursor.execute(sqlp, (ids[i], string))
				sql3 = """INSERT INTO description (beach_id, text, source) VALUES (%s, %s, %s)"""
				txt = ''
				for item in desc:
					txt = txt + item
				try:
					cursor.execute(sql3,(ids[i], txt, s))
				except:
					txt = txt.encode('cp737', 'ignore')
					cursor.execute(sql3,(ids[i], txt, s))
				db.commit()
			elif s == 'Foursquare':
				tip = client.venues.tips(desc)
				for item in tip["tips"]["items"]:
					tip_text = item["text"]
					sql6 = """INSERT INTO description(beach_id, text, source) VALUES(%s, %s, %s)"""
					args6 = (ids[i], tip_text, s)
					cursor.execute(sql6, args6)
				db.commit()
				venuephotos = client.venues.photos(desc, params={'group': 'venue'})
				for photo in venuephotos["photos"]["items"]:
					photo_url=photo["prefix"]+"width500"+photo["suffix"]
					sqlph = """INSERT INTO photos (beach_id, ph) VALUES (%s, %s)"""
					cursor.execute(sqlph, (ids[i], photo_url))
				db.commit()
			elif s == 'destinationskiathos.com':
				sql3 = """INSERT INTO description (beach_id, text, source) VALUES (%s, %s, %s)"""
				cursor.execute(sql3,(ids[i], desc, s))
				sqlphoto = """INSERT INTO photos (beach_id, ph) VALUES (%s, %s)"""
				cursor.execute(sqlphoto, (ids[i], ph))
				db.commit()
			elif s == 'cleanbeaches.gr':
				sql3 = """INSERT INTO description (beach_id, text, source) VALUES (%s, %s, %s)"""
				cursor.execute(sql3,(ids[i], desc, s))
				db.commit()
			else:
				sql3 = """INSERT INTO description (beach_id, text, source) VALUES (%s, %s, %s)"""
				cursor.execute(sql3,(ids[i], desc, s))
				if ph != []:
					for item in ph:
						sqlph = """INSERT INTO photos (beach_id, ph) VALUES (%s, %s)"""
						cursor.execute(sqlph, (ids[i], item))
				db.commit()
			db.commit()
			simularity = simularity + 1
		i = i - 1
	db.close()	
	return simularity
