#!/usr/bin/python
#-*- coding: iso-8859-7 -*-
# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: http://doc.scrapy.org/en/latest/topics/item-pipeline.html

import sys
import string
import MySQLdb
import hashlib
from scrapy.exceptions import DropItem
from scrapy.http import Request
import codecs
import unicodedata
from unidecode import unidecode
import  codecs, locale
import unicodedata
import sys,shutil
import nltk
import urllib
import urllib2
import os, sys
import sys
reload(sys)  # Reload does the trick!
sys.setdefaultencoding('utf8')

class  BlueflagPipeline(object):

    def __init__(self):
        self.conn = MySQLdb.connect(user='root', passwd='', db='database', host='localhost', charset="utf8", use_unicode=True)
        self.cursor = self.conn.cursor()
        #log data to json file
        
         
    def process_item(self, item, spider): 

        try:
            
            sql = """INSERT INTO blueflag (beach_name,lat,lng,region,address,txt) VALUES (%s,%s,%s,%s,%s,%s)"""
            self.cursor.execute(sql,(item['beach_name'],item['lat'],item['lng'],item['region'],item['address'],item['txt']))
            self.conn.commit()

        except MySQLdb.Error, e:
            print "Error %d: %s" % (e.args[0], e.args[1])
 
            return item