# encoding=latin-1
import sys
reload(sys)
sys.setdefaultencoding('latin-1')
import unicodedata
import geonames.adapters.search
from django.utils.encoding import smart_str, smart_unicode
_USERNAME = 'kapsali29'
sa = geonames.adapters.search.Search(_USERNAME)
items = []
result = sa.query('').country('gr').max_rows(1000).execute()
for id_, name in result.get_flat_results():
    item = geonames.compat.make_unicode(" [{1}]").format(id_, name) 
    items.append(item)
items = [unicodedata.normalize('NFKD', x).encode('latin-1','ignore') for x in items]
print items