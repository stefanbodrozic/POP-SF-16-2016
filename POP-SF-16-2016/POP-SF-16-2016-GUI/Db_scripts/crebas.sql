CREATE TABLE TipNamestaja(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	Naziv VARCHAR(80),
	Obrisan BIT DEFAULT ((0))
)

GO
CREATE TABLE Namestaj(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	TipNamestajaId INT,
	Naziv VARCHAR(60),
	Cena NUMERIC(9, 2),
	AkcijskaCena NUMERIC (9, 2) DEFAULT ((0)),
	Sifra VARCHAR(10),
	Kolicina INT,
	--akcijaId ?
	Obrisan BIT DEFAULT ((0)),

	FOREIGN KEY (TipNamestajaId) REFERENCES TipNamestaja(Id)
)

GO
CREATE TABLE Korisnik(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	Ime VARCHAR(20),
	Prezime VARCHAR(20),
	KorisnickoIme VARCHAR(20),
	Lozinka VARCHAR(20),
	--tip korisnika enumeracija ?
	Obrisan BIT DEFAULT ((0))
)
GO
CREATE TABLE DodatneUsluge(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	Naziv VARCHAR(30),
	Iznos NUMERIC(9, 2),
	Obrisan BIT DEFAULT ((0))
)

GO
CREATE TABLE Akcija(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	DatumPocetka DATETIME DEFAULT GETDATE(),
	DatumZavrsetka DATETIME DEFAULT GETDATE(),
	Popust NUMERIC(9, 2),
	Obrisan BIT DEFAULT ((0))
)

--akcija i namestaj na akciji ce se povezati preko IdAkcije u NamestajNaAkciji i Id u Akciji
GO
CREATE TABLE NamestajNaAkciji(
	IdAkcije INT,
	FOREIGN KEY (IdAkcije) REFERENCES Akcija(Id),
	IdNamestaja INT,
	FOREIGN KEY(IdNamestaja) REFERENCES Namestaj(Id)
)

GO
CREATE TABLE ProdajaNamestaja(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	DatumProdaje DATETIME DEFAULT GETDATE(),
	BrojRacuna VARCHAR(10),
	Kupac VARCHAR(50),
	UkupnaCena NUMERIC(9, 2),
	Obrisan BIT DEFAULT ((0))
)

--prodaja namestaja i stavkaracuna ce se povezati preko Id u ProdajaNamestaja i IdProdaje u StavkaRacuna
GO
CREATE TABLE StavkaRacuna(
	Id INT PRIMARY KEY IDENTITY(1, 1), --potrebno??
	IdProdaje INT,
	FOREIGN KEY (IdProdaje) REFERENCES ProdajaNamestaja(Id),
	IdNamestaja INT,
	FOREIGN KEY (IdNamestaja) REFERENCES Namestaj(Id),
	KolicinaNamestaja INT DEFAULT(1),
	IdDodatneUsluge INT,
	FOREIGN KEY (IdDodatneUsluge) REFERENCES DodatneUsluge(Id),
	KolicinaDodatneUsluge INT DEFAULT(1)
)

