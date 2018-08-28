def get_venue_photo(param):
	#!/usr/bin/python
	#-*- coding: iso-8859-7 -*-
	import foursquare
	import json
	import MySQLdb
	import  codecs, locale
	import unicodedata
	import sys,shutil
	import nltk
	import string
	import unicodedata
	from unidecode import unidecode
	#sys.setdefaultencoding('iso-8859-1')

	client = foursquare.Foursquare(client_id='0NKJU5YU3205AZXBGQWLDWRLREZRNBGWPDHYR2PXNFOFOGIY', client_secret='DIWIFQPLO0HLBBKOCYLAO1FRK5KA0DDGS1ZTVLN2RBSQST3E')

	ph=client.venues.photos(param, params={'group': 'venue'})
	for photo in ph["photos"]["items"]:
		photo_url=photo["prefix"]+"width500"+photo["suffix"]
		print photo_url
		# disconnect from server