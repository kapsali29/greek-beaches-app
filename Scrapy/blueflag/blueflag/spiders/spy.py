#!/usr/bin/python
  #-*- coding: iso-8859-7 -*-
from scrapy.contrib.spiders import CrawlSpider, Rule
from scrapy.linkextractors import LinkExtractor
from scrapy.selector import Selector
from scrapy.loader import ItemLoader
from blueflag.items import BlueflagItem
from scrapy.http import HtmlResponse
import re
import os, sys
import  codecs, locale
import unicodedata
import sys,shutil
import nltk
import string
import unicodedata
import urllib
import urllib2
import re
import geocoder
from unidecode import unidecode
from geopy.geocoders import Nominatim

import sys
reload(sys)  # Reload does the trick!
sys.setdefaultencoding('utf8')
class MySpider(CrawlSpider):
    name= "blue"
    allowed_domains = ["blueflag.org"]
    start_urls = ["http://www.blueflag.org/menu/awarded-sites/2015/northern-hemisphere/greece/"]
    rules = ( 
        Rule(LinkExtractor( allow=() , restrict_xpaths=('//div[@class="ctn"]/ul//li')), follow=True),
        Rule(LinkExtractor( allow=() , restrict_xpaths=('//table/tbody//tr/td[3]')),callback="parse_items" , follow=True),
    )


    def parse_items(self, response):
       sel=Selector(response)
       item=BlueflagItem()
       item['beach_name']=sel.xpath("//div[@class='inner-float']/h1/text()").extract()
       t=sel.xpath("//div[@class='col2']//p/text() | //p[@class='MsoNormal']//span/text()").extract()
       t=[x.encode("utf8","replace") for x in t]
       item['txt']=''.join(t)
       print item['txt']
       item['region']=sel.xpath("//div[@class='ctn']/p/a[1]/text()").extract()
       item['address']=sel.xpath("//div[@class='col2']/h2/text()").extract()
       item['lat']=sel.xpath("//script").re(r'lat =\ ([0-9.]+)')
       print item['lat']
       item['lng']=sel.xpath("//script").re(r'lon =\ ([0-9.]+)')
       print item['lng']
       geolocator = Nominatim()
       location = geolocator.reverse(item['lat'] + item['lng'])
       print item['address']
       if 'village' in location.raw['address'].keys():
            item['region'] = location.raw['address']['state_district'] + ' ' + location.raw['address']['village']
            print item['region']
       else:
            item['region'] = location.raw['address']['state_district']
            print item['region']
       return(item)