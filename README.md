# POP-SF-16-2016

Realizovati stand-alone GUI .NET aplikaciju u WPF tehnologiji za administraciju
poslovanja salona nameštaja.

Aplikacija treba da omogući administraciju:
1. Podataka o salonu - Evidentira se naziv, adresa, telefon, email, adresa internet sajta, PIB,
matični broj i broj žiro računa.
2. Nameštaja - Za svaki komad nameštaja evidentira se naziv, šifra, jedinična cena, količina u
magacinu i tip nameštaja (npr. kreveti, predsoblja, kuhinjski nameštaj, ...).
3. Prodaje nameštaja - Svaka prodaja sadrži određen broj komada nameštaja. Pri tome se
evidentira datum prodaje, broj računa i kupac. Prilikom prodaje, salon može kupcu da pruži i
dodatne usluge (npr. prevoz, montaža). Prilikom prodaje nameštaja izračunava se ukupna
cena. Pri formiranju ukupne cene, na cenu se dodaje PDV.
4. Akcijskih prodaja - Svaka akcija ima datum početka i završetka. U toku trajanja akcije
određeni komadi nameštaja su na popustu. Popust se određuje za svaki komad nameštaja
pojedinačno.
5. Korisnika aplikacije - Evidentiraju se osobe koje imaju pravo da koriste aplikaciju. Za svakog
korisnika evidentira se ime, prezime, korisničko ime, lozinka, tip korisnika (postoje dva tipa
korisnika aplikacije - administrator i prodavac).

Administracija navedenih podataka podrazumeva pregled, unos, izmenu i brisanje podataka. Sva
brisanja su logička (element se proglašava neaktivnim, a ne uklanja se fizički).
Aplikaciju može koristiti samo ulogovani korisnik. Korisnike, nameštaj i podatke o akcijama ažurira
administrator. Prodavac je zadužen za evidenciju prodaje nameštaja.
Za sve navedene entitete, pri prikazu je potrebno omogućiti:
1. Sortiranje po svakom od entiteta.
2. Pretragu podataka.
Nameštaj je potrebno pretraživati po tipu nameštaja, šifri i nazivu. Prodaju nameštaja je
potrebno pretraživati po imenu ili prezimenu kupca, broju računa, prodatom komadu
nameštaja i datumu prodaje.
Za akcijske prodaje, potrebno je omogućiti prikaz aktuelnih akcija.
Korisnike je potrebno pretraživati po imenu, prezimenu i korisničkom imenu.
Perzistenciju podataka realizovati korišćenjem relacione baze podataka.
