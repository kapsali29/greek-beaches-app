# greek-beaches-app

Στα πλαίσια της διπλωματικής αυτής αναπτύχθηκε μια εφαρμογή, που  συγκεντρώνει πληροφορία από το κοινωνικό δίκτυο Foursquare (api) καθώς και από άλλες σελίδες του Διαδικτύου (scaping).
Εφόσον γίνει η εξόρυξη της πληροφόρίας, έπεται η αποθήκευσή της σε μία MySQL βάση δεδομένων, τα στοιχεία που εξάγωνται είναι το όνομα της παραλίας, το γεωγραφικό μήκος και πλάτος της τοποθεσίας που βρίσκεται η παραλία, η διεύθυνση, η περιοχή της παραλίας καθώς και περιγραφές και σχόλια των χρηστών των σελίδων του διαδικτύου και του Foursquare. Η πληροφορία υπάρχει στη βάση δεδομένων γίνεται expose μέσω web services τα οποία καταναλώνονται από την εφαρμογή.
Αρχικά παρουσιάζεται μια λίστα των παραλιών της Ελλάδας, στην συνέχεια με την ενργοποίηση του GPS του κινητού παρουσιάζονται οι πιο κοντινές στον χρήστη παραλίες. Επίσης υπάρχει μπάρα αναζήτησης των παραλιών, καθώς και χάρτης που δείχνει τις πιο κοντινές στον χρήστη παραλίες

Στη συνέχεια ακολουθεί video demonstration της εφαρμογής:

!(app demo)[https://www.youtube.com/watch?v=BvZWS-H9eK4]

How Scrapy framework gets the information
!(scrapy demo)[https://www.youtube.com/watch?v=GAxQwDerO_I]
!(Portia scraping hub)[https://www.youtube.com/watch?time_continue=17&v=YKJ43CMxGfs]
