def check_double_entry(param):
	#!/usr/bin/python
	import MySQLdb
	import foursquare
	import json
	import os
	import  codecs, locale
	from tips import tip_details
	import unicodedata
	import sys,nltk,shutil
	import string
	import unicodedata
	import chardet
	from unidecode import unidecode
	servername="localhost"
	username="root"
	password=""
	dbname="database"
	db = MySQLdb.connect(servername,username,password,dbname,charset="utf8", use_unicode=True )
	cursor = db.cursor()
	sql3="""SELECT page_id FROM beachplus """
	cursor.execute(sql3)
	#results=list(cursor.fetchall())
	#print results
	#if param in results:
		#return True
	ids=[row[0] for row in cursor.fetchall()]
	is_in_db = param in ids
	if   is_in_db:
		print True
		return True