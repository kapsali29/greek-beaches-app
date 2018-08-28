#!/usr/bin/python
#-*- coding: iso-8859-7 -*-
from scrapy.contrib.spiders import CrawlSpider, Rule
from scrapy.linkextractors import LinkExtractor
from scrapy.contrib.linkextractors.sgml import SgmlLinkExtractor
from scrapy.selector import Selector
from scrapy.loader import ItemLoader
from wondergreece.items import WondergreeceItem
from geopy.geocoders import Nominatim
import re
import time
import json
import string

class MySpider(CrawlSpider):
	name = 'wonder'
	allowed_domains = ["wondergreece.gr"]
	start_urls = ["http://www.wondergreece.gr/v1/el/Perioxes/"]

	rules = (
		Rule(LinkExtractor( allow=() , restrict_xpaths=('//div[@class="row-fluid regionbox"]//div[@class="rclass"]//h4/a')), follow=True),
		Rule(LinkExtractor(allow=(r'Perioxes/.*/Fysi/Paralies/.*',)), callback='parse_items',follow=True),
        )
	def parse_items(self,response):
		sel = Selector(response)
		item = WondergreeceItem()
		item['beach'] = sel.xpath('//div[@class="title span6 pull-left"]/h3/text()').extract()[0]
		item['description'] = sel.xpath('//section[@class="entry-content media clearfix"]/p[@class="text-justify"]/text() | //section[@class="entry-content media clearfix"]/p[2]/text()').extract()
		if item['description'] != []:
			item['description'] = item['description'][0]
		else:
			item['description'] = u'\u0394\u03b5\u03bd \u03c5\u03c0\u03ac\u03c1\u03c7\u03b5\u03b9 \u03c0\u03b5\u03c1\u03b9\u03b3\u03c1\u03b1\u03c6\u03ae'
		#try:
			#print item['description']
		#except:
		#	item['description'] = item['description'].encode('cp737', 'ignore')
		ph = sel.xpath('//ul[@class="slides"]//li//img')
		fot = []
		for f in ph:
			photo = f.xpath('@src').extract()
			fot.append(photo)
		item['photos'] = fot
		print item['photos']
		latlon = sel.xpath('//script').re(r'"latlng":(.*)')
		if latlon != []:
			l = str(latlon[0])
			l = l.replace('"', '')
			start = l.find('a')
			l = l[0:start]
			item['lat'] = float(l[0:l.find(',')])
			item['lon'] = float(l[l.find(',') + 1:l.rfind(',')])
		else:
			item['lat'] = 00.0000000
			item['lon'] = 00.0000000
		time.sleep(2)
		geolocator = Nominatim()
		coord=(float(item['lat']),float(item['lon']))
		location = geolocator.reverse(coord, timeout=10)
		item['address'] = location.address
		if 'state_district' in location.raw['address'].keys():
			item['region'] = location.raw['address']['state_district']
		return item