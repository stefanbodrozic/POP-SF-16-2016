using POP_SF_16_2016.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016.BLL
{
    class ProdajaNamestajaBLL
    {
        public static void ProdajaNamestajaMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA PRODAJOM NAMESTAJA =====");
                Console.WriteLine("1. Prikazi prodaju namestaja");
                Console.WriteLine("2. Dodaj prodaju namestaja");
                Console.WriteLine("3. Izmeni prodaju namestaja");
                Console.WriteLine("4. Izbrisi prodaju namestaja");
                Console.WriteLine("5. Pretraga prodaje namestaja");
                Console.WriteLine("6. Sortiranje prodaje namestaja");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 6);
            switch (izbor)
            {
                case 1:
                    PrikazSvihProdajaNamestaja();
                    break;
                case 2:
                    DodajNovuProdajuNamestaja();
                    break;
                case 3:
                    IzmeniProdajuNamestaja();
                    break;
                case 4:
                    IzbrisiProdajuNamestaja();
                    break;
                case 5:
                    PretragaProdajeNamestaja();
                    break;
                case 6:
                    SortiranjeProdajeNamestaja();
                    break;
                default:
                    break;
            }
        }

        private static void PrikazSvihProdajaNamestaja()
        {
            Console.WriteLine("===== PRIKAZ SVIH PRODAJA NAMESTAJA =====");
            var ucitanaProdajaNamestaja = Projekat.Instanca.ProdajaNamestaja;
            for (int i = 0; i < ucitanaProdajaNamestaja.Count; i++)
            {
                if (ucitanaProdajaNamestaja[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Broj racuna: {ucitanaProdajaNamestaja[i].BrojRacuna}, Kupac: {ucitanaProdajaNamestaja[i].Kupac}, Ukupna cena: {ucitanaProdajaNamestaja[i].UkupnaCena}");
                }
            }
            ProdajaNamestajaMeni();

        }

        private static void DodajNovuProdajuNamestaja()
        {
            var dodatneUslugeUcitane = Projekat.Instanca.DodatneUsluge;

            var prodajaNamestajaUcitano = Projekat.Instanca.ProdajaNamestaja;

            Console.WriteLine("Datum prodaje: ");
            string datumProdaje = Console.ReadLine();
            Console.WriteLine("Broj racuna: ");
            string brojRacuna = Console.ReadLine();
            Console.WriteLine("Kupac: ");
            string kupac = Console.ReadLine();
            double ukupnaCena = 0;

            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            Console.WriteLine("Prikaz dostupnog namestaja koji moze biti prodat");

            for (int i = 0; i < ucitanNamestaj.Count; i++)
            {
                if (ucitanNamestaj[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Naziv: {ucitanNamestaj[i].Naziv}, Cena: {ucitanNamestaj[i].Cena}, Sifra: {ucitanNamestaj[i].Sifra}, Id namestaja: {ucitanNamestaj[i].Id}, Id tipa namestaja: {ucitanNamestaj[i].TipNamestajaId}, Obrisan: {ucitanNamestaj[i].Obrisan}");
                }
            }
            Console.WriteLine();
            int unos = 0;
            List<int> namestajIdZaProdajuLista = new List<int>();
            do
            {
                Console.WriteLine("Za prekid uneti 0. Namestaji koji su prodati: ");
                unos = int.Parse(Console.ReadLine());
                Namestaj pronadjenNamestaj = Namestaj.PronadjiNamestajPoId(unos);

                if (pronadjenNamestaj != null)
                {
                    ukupnaCena += pronadjenNamestaj.Cena;
                    namestajIdZaProdajuLista.Add(pronadjenNamestaj.Id);
                }


            } while (unos != 0 && unos > 0);

            var ucitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;
            Console.WriteLine("Prikaz dodatnih usluga koje su u ponudi");

            for (int i = 0; i < ucitaneDodatneUsluge.Count; i++)
            {
                if (ucitaneDodatneUsluge[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Naziv usluge: {ucitaneDodatneUsluge[i].Naziv}, Cena: {ucitaneDodatneUsluge[i].Iznos}");
                }
            }

            Console.WriteLine();
            int unosId = 0;
            List<int> dodatneUslugeIdLista = new List<int>();
            do
            {
                Console.WriteLine("Za prekid uneti 0. Dodatne usluge koji su prodate: ");
                unosId = int.Parse(Console.ReadLine());
                DodatneUsluge pronadjenaDodatnaUsluga = DodatneUsluge.PronadjiDodatnuUsluguPoId(unosId);
                if (pronadjenaDodatnaUsluga != null)
                {
                    ukupnaCena += pronadjenaDodatnaUsluga.Iznos;
                    dodatneUslugeIdLista.Add(pronadjenaDodatnaUsluga.Id);
                }
            } while (unos != 0 && unos > 0);

            var novaProdajaNamestaja = new ProdajaNamestaja()
            {
                Id = prodajaNamestajaUcitano.Count + 1,
                BrojRacuna = brojRacuna,
                DatumProdaje = DateTime.Parse(datumProdaje),
                DodatneUsluge = dodatneUslugeIdLista,
                Kupac = kupac,
                UkupnaCena = ukupnaCena,
                NamestajZaProdaju = namestajIdZaProdajuLista
            };

            prodajaNamestajaUcitano.Add(novaProdajaNamestaja);
            Projekat.Instanca.ProdajaNamestaja = prodajaNamestajaUcitano;
            ProdajaNamestajaMeni();
        }

        private static void IzmeniProdajuNamestaja()
        {
            Console.WriteLine("===== IZMENA TIPA NAMESTAJA =====");
            var ucitaneProdajeNamestaja = Projekat.Instanca.ProdajaNamestaja;
            ProdajaNamestaja prodajaNamestajaZaIzmenu = new ProdajaNamestaja();
            Console.WriteLine("Id prodaje namestaja za izmenu: ");
            int idProdajeNamestajaZaIzmenu = int.Parse(Console.ReadLine());
            foreach (ProdajaNamestaja prodajaNamestaja in ucitaneProdajeNamestaja)
            {
                if (prodajaNamestaja.Id == idProdajeNamestajaZaIzmenu)
                {
                    prodajaNamestajaZaIzmenu = prodajaNamestaja;
                }
            }
            int izbor = 0;
            do
            {
                Console.WriteLine("1. Izmena datuma prodaje");
                Console.WriteLine("2. Izmena broja racuna");
                Console.WriteLine("3. Izmena kupca");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 3);
            switch (izbor)
            {
                case 1:
                    string izmenjenDatumProdaje = "";
                    do
                    {
                        Console.WriteLine("Novi datum prodaje: ");
                        izmenjenDatumProdaje = Console.ReadLine();
                    } while (izmenjenDatumProdaje == "");
                    prodajaNamestajaZaIzmenu.DatumProdaje = DateTime.Parse(izmenjenDatumProdaje);
                    break;
                case 2:
                    string izmenjenBrojRacuna = "";
                    do
                    {
                        Console.WriteLine("Novi broj racuna: ");
                        izmenjenBrojRacuna = Console.ReadLine();
                    } while (izmenjenBrojRacuna == "");
                    prodajaNamestajaZaIzmenu.BrojRacuna = izmenjenBrojRacuna;
                    break;
                case 3:
                    string izmenaKupca = "";
                    do
                    {
                        Console.WriteLine("Novi kupac: ");
                        izmenaKupca = Console.ReadLine();
                    } while (izmenaKupca == "");
                    prodajaNamestajaZaIzmenu.Kupac = izmenaKupca;
                    break;
                default:
                    break;
            }
            Projekat.Instanca.ProdajaNamestaja = ucitaneProdajeNamestaja;
        }


        private static void IzbrisiProdajuNamestaja()
        {
            var ucitaneProdajeNamestaja = Projekat.Instanca.ProdajaNamestaja;
            ProdajaNamestaja izbrisanaProdajaNamestaja = null;
            do
            {
                Console.WriteLine("Id prodaje namestaja za brisanje: ");
                int idProdajeNamestaja = int.Parse(Console.ReadLine());
                foreach (ProdajaNamestaja prodajaNamestaja in ucitaneProdajeNamestaja)
                {
                    if (prodajaNamestaja.Obrisan != true && prodajaNamestaja.Id == idProdajeNamestaja)
                    {
                        izbrisanaProdajaNamestaja = prodajaNamestaja;
                    }
                }
            } while (izbrisanaProdajaNamestaja == null);
            izbrisanaProdajaNamestaja.Obrisan = true;
            Projekat.Instanca.ProdajaNamestaja = ucitaneProdajeNamestaja;
            ProdajaNamestajaMeni();
        }

        private static void PretragaProdajeNamestaja()
        {
            var ucitaneProdajeNamestaja = Projekat.Instanca.ProdajaNamestaja;
            int izbor = 0;
            do
            {
                Console.WriteLine("Pretraga prodaje namestaja po:");
                Console.WriteLine("1. Kupcu");
                Console.WriteLine("2. Broju racuna");
                Console.WriteLine("3. Prodatom broju namestaja");
                Console.WriteLine("4. Datumu prodaje");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 5);
            switch (izbor)
            {
                case 1:
                    Console.WriteLine("Unesite ime kupca za pretragu: ");
                    string kupac = Console.ReadLine();
                    foreach (var prodajaNamestaja in ucitaneProdajeNamestaja)
                    {
                        if (prodajaNamestaja.Kupac == kupac && prodajaNamestaja.Obrisan != true)
                        {
                            Console.WriteLine($"Broj racuna: {prodajaNamestaja.BrojRacuna}, Kupac: {prodajaNamestaja.Kupac}, Datum prodaje: {prodajaNamestaja.DatumProdaje}");
                        }
                    }
                    PretragaProdajeNamestaja();
                    break;
                case 2:
                    Console.WriteLine("Unesite ime broja racuna za pretragu: ");
                    string brojRacuna = Console.ReadLine();
                    foreach (var prodajaNamestaja in ucitaneProdajeNamestaja)
                    {
                        if (prodajaNamestaja.BrojRacuna == brojRacuna && prodajaNamestaja.Obrisan != true)
                        {
                            Console.WriteLine($"Broj racuna: {prodajaNamestaja.BrojRacuna}, Kupac: {prodajaNamestaja.Kupac}, Datum prodaje: {prodajaNamestaja.DatumProdaje}");
                        }
                    }
                    PretragaProdajeNamestaja();
                    break;
                case 3:
                    Console.WriteLine("Unesite prodat broj namestaja za pretragu: ");
                    int prodatBrojNamestaja = int.Parse(Console.ReadLine());
                    foreach (var prodajaNamestaja in ucitaneProdajeNamestaja)
                    {
                        if (prodajaNamestaja.NamestajZaProdaju.Count == prodatBrojNamestaja && prodajaNamestaja.Obrisan != true)
                        {
                            Console.WriteLine($"Broj racuna: {prodajaNamestaja.BrojRacuna}, Kupac: {prodajaNamestaja.Kupac}, Datum prodaje: {prodajaNamestaja.DatumProdaje}");
                        }
                    }
                    PretragaProdajeNamestaja();
                    break;
                case 4:
                    Console.WriteLine("Unesite datum prodaje za pretragu: ");
                    DateTime datumProdaje = DateTime.Parse(Console.ReadLine());
                    foreach (var prodajaNamestaja in ucitaneProdajeNamestaja)
                    {
                        if (prodajaNamestaja.DatumProdaje.Date == datumProdaje) 
                        {
                            Console.WriteLine($"Broj racuna: {prodajaNamestaja.BrojRacuna}, Kupac: {prodajaNamestaja.Kupac}, Datum prodaje: {prodajaNamestaja.DatumProdaje}");
                        }
                    }
                    PretragaProdajeNamestaja();
                    break;
                default:
                    break;
            }
        }
        

        private static void SortiranjeProdajeNamestaja()
        {
            var ucitaneProdajeNamestaja = Projekat.Instanca.ProdajaNamestaja;
            int izbor = 0;
            do
            {
                Console.WriteLine("Sortiranje prodaje namestaja po:");
                Console.WriteLine("1. Datumu prodaje");
                Console.WriteLine("2. Broju racuna");
                Console.WriteLine("3. Ukupnoj ceni");
                Console.WriteLine("4. Kupcu");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    ucitaneProdajeNamestaja = ucitaneProdajeNamestaja.OrderBy(x => x.DatumProdaje).ToList();
                    foreach (var prodajaNamestaja in ucitaneProdajeNamestaja)
                    {
                        if (prodajaNamestaja.Obrisan != true)
                        {
                            Console.WriteLine($"Datum prodaje: {prodajaNamestaja.DatumProdaje.Date}, Broj racuna: {prodajaNamestaja.BrojRacuna}, Ukupna cena: {prodajaNamestaja.UkupnaCena}, Kupac: {prodajaNamestaja.Kupac}");
                        }
                    }
                    SortiranjeProdajeNamestaja();
                    break;
                case 2:
                    ucitaneProdajeNamestaja = ucitaneProdajeNamestaja.OrderBy(x => x.BrojRacuna).ToList();
                    foreach (var prodajaNamestaja in ucitaneProdajeNamestaja)
                    {
                        if (prodajaNamestaja.Obrisan != true)
                        {
                            Console.WriteLine($"Datum prodaje: {prodajaNamestaja.DatumProdaje.Date}, Broj racuna: {prodajaNamestaja.BrojRacuna}, Ukupna cena: {prodajaNamestaja.UkupnaCena}, Kupac: {prodajaNamestaja.Kupac}");
                        }
                    }
                    SortiranjeProdajeNamestaja();
                    break;
                case 3:
                    ucitaneProdajeNamestaja = ucitaneProdajeNamestaja.OrderBy(x => x.UkupnaCena).ToList();
                    foreach (var prodajaNamestaja in ucitaneProdajeNamestaja)
                    {
                        if (prodajaNamestaja.Obrisan != true)
                        {
                            Console.WriteLine($"Datum prodaje: {prodajaNamestaja.DatumProdaje.Date}, Broj racuna: {prodajaNamestaja.BrojRacuna}, Ukupna cena: {prodajaNamestaja.UkupnaCena}, Kupac: {prodajaNamestaja.Kupac}");
                        }
                    }
                    SortiranjeProdajeNamestaja();
                    break;
                case 4:
                    ucitaneProdajeNamestaja = ucitaneProdajeNamestaja.OrderBy(x => x.Kupac).ToList();
                    foreach (var prodajaNamestaja in ucitaneProdajeNamestaja)
                    {
                        if (prodajaNamestaja.Obrisan != true)
                        {
                            Console.WriteLine($"Datum prodaje: {prodajaNamestaja.DatumProdaje.Date}, Broj racuna: {prodajaNamestaja.BrojRacuna}, Ukupna cena: {prodajaNamestaja.UkupnaCena}, Kupac: {prodajaNamestaja.Kupac}");
                        }
                    }
                    SortiranjeProdajeNamestaja();
                    break;
                default:
                    break;
            }
        }
    }
}
