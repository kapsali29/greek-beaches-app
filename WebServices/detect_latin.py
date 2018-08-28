# -*- coding: utf-8 -*-
def isEnglish(s):
	try:
		s.decode('ascii')
	except:
		return False
	else:
		return True