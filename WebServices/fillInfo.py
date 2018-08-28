#!/usr/bin/env python
# -*- coding: utf-8 -*-
import MySQLdb
servername = "localhost"
username = "root"
password = ""
dbname = "database"
myquery = "SELECT *FROM beach WHERE region = %s"
db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
cur = db.cursor()

fetchId = "SELECT id FROM beach"
cur.execute(fetchId)
idds = cur.fetchall()
ids = []
fetchPhoto = "SELECT ph FROM photos WHERE beach_id = %s LIMIT 1"
for iding in idds:
	print iding[0]
	cur.execute(fetchPhoto, (int(iding[0]),))
	photo =  cur.fetchall()
	if photo == ():
		print ''
	else:
		print photo[0][0]
		qup = "UPDATE beach SET photo = %s WHERE id = %s"
		args = (photo[0][0], int(iding[0]))
		cur.execute(qup, args)
		db.commit()