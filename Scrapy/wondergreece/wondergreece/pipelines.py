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
sys.setdefaultencoding('cp737')
sys.path.insert(0, 'C:\Users\home\greekbeachesapp')
from sunthesis import elegxos_paralias

class  WondergreecePipeline(object):
    def __init__(self):
        self.conn = MySQLdb.connect(user='root', passwd='', db='database', host='localhost', charset="utf8", use_unicode=True)
        self.cursor = self.conn.cursor()
        #log data to json file
    def process_item(self, item, spider):
        if elegxos_paralias(item['beach'], item['lat'], item['lon'], item['address'], item['description'], 'wondergreece.gr', item['photos']) != 0:
            print 'Inserted to beachplus'
        else:
            try:
                sql = """INSERT INTO beach (name,lat,lon,region) VALUES (%s,%s,%s,%s)"""
                self.cursor.execute(sql, (item['beach'], item['lat'], item['lon'], item['region']))
                sql3 = """SELECT MAX(id) FROM beach"""
                self.cursor.execute(sql3)
                result = self.cursor.fetchone()
                sql2 = """INSERT INTO beachplus(address ,source, beach_id, alternate) VALUES (%s, %s, %s, %s)"""
                self.cursor.execute(sql2, (item['address'], 'wondergreece.gr', result, item['beach'] ))
                sql4 = """INSERT INTO description(beach_id, text, source) VALUES(%s, %s, %s)"""
                self.cursor.execute(sql4,(result, item['description'], "wondergreece.gr"))
                for item in item['photos']:
                    sql5 = """INSERT INTO photos(beach_id, ph) VALUES(%s, %s)"""
                    self.cursor.execute(sql5, (result, item,))
                self.conn.commit()
            except MySQLdb.Error, e:
                print "Error %d: %s" % (e.args[0], e.args[1])
                return item