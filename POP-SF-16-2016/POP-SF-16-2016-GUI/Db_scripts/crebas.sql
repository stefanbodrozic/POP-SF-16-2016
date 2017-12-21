CREATE TABLE TipNamestaja(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	Naziv VARCHAR(80),
	Obrisan BIT NOT NULL DEFAULT ((0))
)

GO
CREATE TABLE Namestaj(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	TipNamestajaId INT,
	Naziv VARCHAR(60),
	Cena NUMERIC(9, 2),
	Sifra VARCHAR(10),
	Kolicina INT,
	--akcijaId
	Obrisan BIT NOT NULL DEFAULT ((0)),
	FOREIGN KEY (TipNamestajaId) REFERENCES TipNamestaja(Id)
)