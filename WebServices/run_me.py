# !/usr/bin/python
# -*- coding: utf-8 -*-
import sys
reload(sys)
sys.setdefaultencoding('utf-8')
import unicodedata
from beach_search import extract
import geonames.adapters.search
from django.utils.encoding import smart_str, smart_unicode
_USERNAME = 'kapsali'
sa = geonames.adapters.search.Search(_USERNAME)
items = []
result = sa.query('').country('gr').max_rows(1000).execute()
for id_, name in result.get_flat_results():
    item = geonames.compat.make_unicode(" [{1}]").format(id_, name) 
    items.append(item)
items = [unicodedata.normalize('NFKD', x).encode('latin-1','ignore') for x in items]
print items
for item in items:
	print item 
	print '\n'
	try:
		extract(item)
	except:
		sep = ','
		text = ''.join(item)
		rest = text.split(sep,1)[0]
		try:
			extract(rest)
		except:
			continue
		