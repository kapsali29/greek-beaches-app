import foursquare
import json
import sys, codecs, locale
import tips import tip_details

#import greek_alphabet
client = foursquare.Foursquare(client_id='0NKJU5YU3205AZXBGQWLDWRLREZRNBGWPDHYR2PXNFOFOGIY', client_secret='DIWIFQPLO0HLBBKOCYLAO1FRK5KA0DDGS1ZTVLN2RBSQST3E')


plaz=client.venues('4d298c91342d6dcb7a5104cb')


print plaz["venue"]["id"]
print plaz["venue"]["name"]
print plaz["venue"]["location"]["lat"]
print plaz["venue"]["location"]["lng"]
print plaz["venue"]["location"]["cc"]
print plaz["venue"]["location"]["city"]
#print plaz["venue"]["location"]["state"]
print plaz["venue"]["location"]["country"]
print plaz["venue"]["location"]["formattedAddress"]
print plaz["venue"]["canonicalUrl"]
print plaz["venue"]["categories"][0]["name"]
print "Stats\n"

print plaz["venue"]["stats"]["checkinsCount"]
print plaz["venue"]["stats"]["usersCount"]
print plaz["venue"]["stats"]["tipCount"]
print plaz["venue"]["stats"]["visitsCount"]
print plaz["venue"]["likes"]["count"]
print plaz["venue"]["photos"]["count"]

print"Photos\n"

for group in plaz["venue"]["photos"]["groups"]:
	for item in group["items"]:
		print item["id"]
		print item["createdAt"]
		print item["user"]["id"]
		print item["user"]["firstName"]
		#print item["user"]["lastName"]

print "Tips\n"

print plaz["venue"]["tips"]["count"]

for group in plaz["venue"]["tips"]["groups"]:
	for item in group["items"]:
		print item["id"]
		print item["text"]
		print item["createdAt"]
		print item["canonicalUrl"]
		print item["user"]["id"]
		print item["user"]["firstName"]
		#print item["user"]["lastName"]
