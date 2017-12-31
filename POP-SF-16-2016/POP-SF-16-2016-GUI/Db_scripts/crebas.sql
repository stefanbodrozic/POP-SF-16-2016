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
	Tip VARCHAR (15) NOT NULL CHECK (Tip IN('Administrator', 'Prodavac')) DEFAULT ('Prodavac'),
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
	NazivAkcije VARCHAR(50),
	Obrisan BIT DEFAULT ((0))
)

--akcija i namestaj na akciji ce se povezati preko IdAkcije u NamestajNaAkciji i Id u Akciji
GO
CREATE TABLE NamestajNaAkciji(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	IdAkcije INT NULL,
	FOREIGN KEY (IdAkcije) REFERENCES Akcija(Id),
	IdNamestaja INT,
	FOREIGN KEY(IdNamestaja) REFERENCES Namestaj(Id),
	Obrisan BIT DEFAULT ((0))
)

GO
CREATE TABLE ProdajaNamestaja(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	Pdv NUMERIC(1,1) DEFAULT 0.2 ,
	DatumProdaje DATETIME DEFAULT CURRENT_TIMESTAMP,
	BrojRacuna VARCHAR(10),
	Kupac VARCHAR(50),
	UkupnaCena NUMERIC(9, 2) DEFAULT 0,
	CenaBezPdv NUMERIC(9, 2) DEFAULT 0,
	Obrisan BIT DEFAULT ((0))
)

CREATE TABLE StavkaRacunaNamestaj(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	IdProdaje INT,
	FOREIGN KEY (IdProdaje) REFERENCES ProdajaNamestaja(Id),
	IdNamestaja INT,
	FOREIGN KEY (IdNamestaja) REFERENCES Namestaj(Id),
	Kolicina INT DEFAULT(1),
	Obrisan BIT DEFAULT ((0))
)

CREATE TABLE StavkaRacunaDodatnaUsluga(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	IdProdaje INT,
	FOREIGN KEY (IdProdaje) REFERENCES ProdajaNamestaja(Id),
	IdDodatneUsluge INT,
	FOREIGN KEY (IdDodatneUsluge) REFERENCES DodatneUsluge(Id),
	Kolicina INT DEFAULT(1),
	Obrisan BIT DEFAULT ((0))
)

GO
CREATE TABLE Salon(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	Naziv VARCHAR(30),
	Adresa VARCHAR(30),
	Telefon VARCHAR(30),
	Email VARCHAR(30),
	Websajt VARCHAR(30),
	Pib INT,
	MaticniBroj INT,
	BrojZiroRacuna VARCHAR(30),
	Obrisan BIT DEFAULT ((0))
)
