# !/usr/bin/python
# -*- coding: utf-8 -*-
def similar(a,b):
	import difflib
	a = a.lower()
	b = b.lower()
	p = u'\u03c0\u03b1\u03c1\u03b1\u03bb\u03af\u03b1'
	p1 = u'\u03c0\u03b1\u03c1\u03b1\u03bb\u03b9\u03b1'
	a = a.replace('beach','')
	a = a.replace('paralia','')
	a = a.replace(p,'')
	a = a.replace(p1,'')
	b = b.replace('beach','')
	b = b.replace('paralia','')
	b = b.replace(p,'')
	b = b.replace(p1,'')
	a=a.strip()
	b=b.strip()
	print a
	print b
	seq = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
	a_init = a
	b_init = b
	print 'first comparison'
	#print a
	#print b
	print seq.ratio()
	starta = a.find('(')
	enda = a.find(')')
	if starta != -1 and enda != -1 :
		resulta = a[starta+1:enda]
		resulta = resulta.strip(" ")
		tempa = a_init.replace(resulta, '')
		tempa = tempa.replace('(','')
		tempa = tempa.replace(')','')
		tempa = tempa.strip(" ")
	elif a.find(",") !=-1 :
		koma = a.find(",")
		apartA = a[0:koma].strip(' ')
		apartB = a[koma+1:len(a)].strip(' ')
	print 'the result is:'
	print a
	starb = b.find('(')
	endb = b.find(')')
	if starb != -1 and endb != -1:
		resultb = b[starb+1:endb]
		resultb = resultb.strip(" ")
		tempb = b_init.replace(resultb, '')
		tempb = tempb.replace('(','')
		tempb = tempb.replace(')','')
		tempb = tempb.strip(" ")
	elif b.find(",") !=-1 :
		koma = b.find(",")
		bpartA = b[0:koma].strip(' ')
		bpartB = b[koma+1:len(b)].strip(' ')
		print bpartB
	seq1 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
	print 'second comparison'
	print a
	print b
	print seq1.ratio()
	if starb != -1 and endb != -1 and starta != -1 and enda != -1:
		a = tempa
		b = tempb
		seqtatb = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		print seqtatb.ratio()
		rate1 = seqtatb.ratio()
		b = resultb
		seqtaresb = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate8 = seqtaresb.ratio()
		print rate8
		a = resulta
		b = tempb
		seqresatb = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		print seqresatb.ratio()
		rate2 = seqresatb.ratio()
		a = resulta
		b = resultb
		seqresaresb = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate9 = seqresaresb.ratio()
		print rate9
	else:
		rate8 = 0.0
		rate1 = 0.0
		rate2 = 0.0
		rate9 = 0.0
	if starta != -1 and enda != -1 and b.find(',') != -1:
		print '1-1'
		a = tempa
		b = bpartA
		print a
		print b
		seq12 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		
		rate12 = seq12.ratio()
		print rate12
		print '1-1'
		a = resulta
		b = bpartB
		print a
		print b
		seq13 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate13 = seq13.ratio()
		print rate13
		a = tempa
		b = bpartB
		print a
		print b
		seq14 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate14 = seq13.ratio()
		print rate14
		a = resulta
		b = bpartA
		print a
		print b
		seq15 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate15 = seq13.ratio()
		print rate15
	else:
		rate12 = 0.0
		rate15 = 0.0
		rate14 = 0.0
		rate13 = 0.0
	if starb != -1 and endb != -1 and a.find(',') != -1:
		a = apartA
		b = tempb
		seq16 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate16 = seq16.ratio()
		print rate16
		a = apartB
		b = resultb
		seq17 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate17 = seq17.ratio()
		print rate17
		b = tempb
		a = apartB
		seq18 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate18 = seq18.ratio()
		print rate18
		a = apartA
		b = resultb
		seq19 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate19 = seq19.ratio()
		print rate19
	else:
		rate18 = 0.0
		rate19 = 0.0
		rate16 = 0.0
		rate17 = 0.0
	if a.find(',') !=-1 and b.find(',') !=-1:
		a = apartA
		b = bpartA
		seq20 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate20 = seq20.ratio()
		print rate20
		a = apartB
		b = bpartB
		seq21 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate21 = seq21.ratio()
		print rate21
		a = apartB
		b = bpartA
		seq22 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate22 = seq22.ratio()
		print rate22
		a = apartA
		b = bpartB
		seq23 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate23 = seq23.ratio()
		print rate23
	else:
		rate23 = 0.0
		rate22 = 0.0
		rate21 = 0.0
		rate20 = 0.0
	if a.find(',') !=-1 and (starb == -1 and endb == -1 or b.find(',') == -1):
		a = apartA
		b = b_init
		seq24 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate24 = seq24.ratio()
		print rate24
		a = apartB
		seq25 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate25 = seq25.ratio()
		print rate25
	else:
		rate25 = 0.0
		rate24 = 0.0
	if b.find(',') !=-1 and (starta == -1 and enda == -1 or a.find(',') ==-1):
		a = a_init
		b = bpartA
		seq26 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate26 = seq26.ratio()
		print rate26
		b = bpartB
		seq27 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate27 = seq27.ratio()
		print rate27
	else:
		rate26 = 0.0
		rate27 = 0.0
	if starb != -1 and endb != -1 and (starta == -1 and enda == -1 or a.find(',') ==-1):
		b = tempb
		a = a_init
		seqab = difflib.SequenceMatcher(a=a.lower(), b=b.lower()) 
		rate3 = seqab.ratio()
		print rate3
		b = resultb
		seq30 = difflib.SequenceMatcher(a=a.lower(), b=b.lower()) 
		rate30 = seq30.ratio()
		print rate30
	else:
		rate3 = 0.0
		rate30 = 0.0
	if (starb == -1 and endb == -1 or b.find(',') == -1) and starta != -1 and enda != -1:
		b = b_init
		a = tempa
		seqba = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate4 = seqba.ratio() 
		print rate4
		a = resulta
		seq40 = difflib.SequenceMatcher(a=a.lower(), b=b.lower())
		rate40 = seq40.ratio()
		print rate40
	else:
		rate4 = 0.0
		rate40 = 0.0
	return max(seq.ratio(), seq1.ratio(), rate1,rate2, rate13, rate15, rate12, rate14, rate18, rate19, rate16, rate17, rate22, rate23, rate20, rate21, rate24, rate25, rate26, rate27, rate3, rate30, rate4, rate40)
