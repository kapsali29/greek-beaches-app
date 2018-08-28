def goToTsek(plid):
    # !/usr/bin/python
    # -*- coding: utf-8 -*-
    import MySQLdb
    import foursquare
    import json
    import time
    import os
    import sys
    import codecs, locale
    from tips import tip_details
    import string
    reload(sys)
    sys.setdefaultencoding('utf-8')
    import sys
    sys.path.insert(0, 'C:\Users\home\greekbeachesapp')
    from sunthesis import elegxos_paralias
    servername = "localhost"
    username = "root"
    password = ""
    dbname = "database"
    db = MySQLdb.connect(servername, username, password, dbname, charset="utf8", use_unicode=True)
    cursor = db.cursor()
    sql0 = """INSERT INTO tsek(pid) VALUES(%s)"""
    args0 = (plid)
    cursor.execute(sql0, args0)
    db.commit()
    db.close()