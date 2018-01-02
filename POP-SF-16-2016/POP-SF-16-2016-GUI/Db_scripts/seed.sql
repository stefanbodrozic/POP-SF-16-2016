INSERT INTO TipNamestaja (Naziv) VALUES ('Krevet');
INSERT INTO TipNamestaja (Naziv) VALUES ('Ugaona garnitura');
INSERT INTO TipNamestaja (Naziv) VALUES ('Kauc');


INSERT INTO Namestaj (TipNamestajaId, Naziv, Cena, Sifra, Kolicina) VALUES (1, 'Francuski krevet', 123.5, 'FKR1', 22);
INSERT INTO Namestaj (TipNamestajaId, Naziv, Cena, Sifra, Kolicina) VALUES (2, 'Sofija ugaona', 223.9, 'U1', 12);
INSERT INTO Namestaj (TipNamestajaId, Naziv, Cena, Sifra, Kolicina) VALUES (3, 'Ivan kauc', 723.5, 'KA1', 2);


INSERT INTO Korisnik (Ime, Prezime, KorisnickoIme, Lozinka) VALUES ('Petar', 'Petrovic', 'pera', 'p');
INSERT INTO Korisnik (Ime, Prezime, KorisnickoIme, Lozinka) VALUES ('Marko', 'Markovic', 'marko', 'm1');
INSERT INTO Korisnik (Ime, Prezime, KorisnickoIme, Lozinka, Tip) VALUES ('Zdravko', 'Zdravkovic', 'a', 'a', 'Administrator');


INSERT INTO DodatneUsluge (Naziv, Iznos) VALUES ('Dostava na kucnu adresu', 999.9);
INSERT INTO DodatneUsluge (Naziv, Iznos) VALUES ('Montaza', 1200.0);
INSERT INTO DodatneUsluge (Naziv, Iznos) VALUES ('Dostava i montaza', 1999.9);


INSERT INTO Akcija (Popust, NazivAkcije) VALUES (10, 'Novogodisnja akcija');


INSERT INTO NamestajNaAkciji (IdAkcije, IdNamestaja) VALUES (1, 2);
INSERT INTO NamestajNaAkciji (IdAkcije, IdNamestaja) VALUES (1, 3);


DECLARE @PoslednjiId int;
SELECT @PoslednjiId = MAX(ProdajaNamestaja.Id) FROM ProdajaNamestaja;
--SELECT @PoslednjiId AS PoslednjiId;

IF @PoslednjiId IS NULL
	 SET @PoslednjiId = 0;

DECLARE @NoviId int;
SELECT @NoviId = @PoslednjiId + 1;
--SELECT @NoviId AS NoviId;

INSERT INTO ProdajaNamestaja (BrojRacuna, Kupac) VALUES ('R' + (CONVERT(VARCHAR(4), @NoviId)), 'Pera Peric');

INSERT INTO StavkaRacunaNamestaj (IdProdaje, IdNamestaja, Kolicina) VALUES (1, 1, 2);

INSERT INTO StavkaRacunaDodatnaUsluga(IdProdaje, IdDodatneUsluge, Kolicina) VALUES (1, 1, 2);

INSERT INTO Salon (Naziv, Adresa, Telefon, Email, Websajt, Pib, MaticniBroj, BrojZiroRacuna) VALUES ('Forma FTNalee', 'Trg Dositeja Obradovica 61', '021/454-3433', 'dekan@ftn.uns.ac.rs', 'www.ftn.uns.ac.rs', 132231, 122, '840-000171666-451');



