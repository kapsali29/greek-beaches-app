def get_venue_info(param):
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

	client = foursquare.Foursquare(client_id='0NKJU5YU3205AZXBGQWLDWRLREZRNBGWPDHYR2PXNFOFOGIY', client_secret='DIWIFQPLO0HLBBKOCYLAO1FRK5KA0DDGS1ZTVLN2RBSQST3E')

	place=client.venues(param)
	placeUrl=place["venue"]["canonicalUrl"]
	print placeUrl
	return placeUrl