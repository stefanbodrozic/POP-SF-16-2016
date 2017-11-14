using POP_SF_16_2016.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016.BLL
{
    class DodatneUslugeBLL
    {
        public static void DodatneUslugeMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA DODATNIM USLUGAMA =====");
                Console.WriteLine("1. Prikazi dodatne usluge");
                Console.WriteLine("2. Dodaj dodatnu uslugu");
                Console.WriteLine("3. Izmeni dodatnu uslugu");
                Console.WriteLine("4. Izbrisi dodatnu uslugu");
                Console.WriteLine("5. Sortiranje dodatnih usluga");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 5);
            switch (izbor)
            {
                case 1:
                    PrikazSvihDodatnihUsluga();
                    break;
                case 2:
                    DodajNovuDodatnuUslugu();
                    break;
                case 3:
                    IzmeniDodatnuUslugu();
                    break;
                case 4:
                    IzbrisiDodatnuUsluge();
                    break;
                case 5:
                    SortiranjeDodatnihUsluga();
                    break;
                default:
                    break;
            }
        }

        private static void PrikazSvihDodatnihUsluga()
        {
            Console.WriteLine("===== LISTING DODATNIH USLUGA =====");
            var ucitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;
            for (int i = 0; i < ucitaneDodatneUsluge.Count; i++)
            {
                if (ucitaneDodatneUsluge[i].Obrisan != true)
                {
                    Console.WriteLine($"Naziv usluge: {ucitaneDodatneUsluge[i].Naziv}, Cena: {ucitaneDodatneUsluge[i].Iznos}");
                }
            }
            DodatneUslugeMeni();
        }

        private static void DodajNovuDodatnuUslugu()
        {
            Console.WriteLine("===== DODAVANJE NOVE DODATNE USLUGE =====");
            var ucitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;

            Console.WriteLine("Naziv usluge: ");
            string naziv = Console.ReadLine();
            Console.WriteLine("Iznos: ");
            double iznos = double.Parse(Console.ReadLine());

            var novaDodatnaUsluga = new DodatneUsluge()
            {
                Id = ucitaneDodatneUsluge.Count + 1,
                Naziv = naziv,
                Iznos = iznos
            };

            ucitaneDodatneUsluge.Add(novaDodatnaUsluga);
            Projekat.Instanca.DodatneUsluge = ucitaneDodatneUsluge;
            DodatneUslugeMeni();
        }

        private static void IzmeniDodatnuUslugu()
        {
            Console.WriteLine("===== IZMENA DODATNE USLUGE =====");
            var ucitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;
            DodatneUsluge dodatnaUslugaZaIzmenu = new DodatneUsluge();
            Console.WriteLine("Id dodatne usluge za izmenu: ");
            int idDodatneUsluge = int.Parse(Console.ReadLine());
            foreach (DodatneUsluge dodatneUsluge in ucitaneDodatneUsluge)
            {
                if (dodatneUsluge.Id == idDodatneUsluge)
                {
                    dodatnaUslugaZaIzmenu = dodatneUsluge;
                }
            }
            int izbor = 0;
            do
            {
                Console.WriteLine("1. Izmena naziva");
                Console.WriteLine("2. Izmena iznosa");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 2);
            switch (izbor)
            {
                case 1:
                    String izmenjenNaziv = "";
                    do
                    {
                        Console.WriteLine("Novi naziv usluge: ");
                        izmenjenNaziv = Console.ReadLine();
                    } while (izmenjenNaziv == "");
                    dodatnaUslugaZaIzmenu.Naziv = izmenjenNaziv;
                    break;
                case 2:
                    double izmenjenIznos = 0;
                    do
                    {
                        Console.WriteLine("Novi iznos: ");
                        izmenjenIznos = double.Parse(Console.ReadLine());
                    } while (izmenjenIznos < 0);
                    dodatnaUslugaZaIzmenu.Iznos = izmenjenIznos;
                    break;
                default:
                    break;
            }
            Projekat.Instanca.DodatneUsluge = ucitaneDodatneUsluge;
            DodatneUslugeMeni();
        }

        private static void IzbrisiDodatnuUsluge()
        {
            var ucitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;
            DodatneUsluge izbrisanaDodatnaUsluga = null;
            do
            {
                Console.WriteLine("Id dodatne usluge za brisanje: ");
                int idDodatneUslugeZaBrisanje = int.Parse(Console.ReadLine());
                foreach (DodatneUsluge dodatneUsluge in ucitaneDodatneUsluge)
                {
                    if (dodatneUsluge.Obrisan != true && dodatneUsluge.Id == idDodatneUslugeZaBrisanje)
                    {
                        izbrisanaDodatnaUsluga = dodatneUsluge;
                    }
                }
            } while (izbrisanaDodatnaUsluga == null);
            izbrisanaDodatnaUsluga.Obrisan = true;
            Projekat.Instanca.DodatneUsluge = ucitaneDodatneUsluge;
            DodatneUslugeMeni();
        }

        private static void SortiranjeDodatnihUsluga()
        {
            var ucitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;
            int izbor = 0;
            do
            {
                Console.WriteLine("Sortiranje dodatnih usluga po:");
                Console.WriteLine("1. Nazivu");
                Console.WriteLine("2. Iznosu");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 2);
            switch (izbor)
            {
                case 1:
                    ucitaneDodatneUsluge = ucitaneDodatneUsluge.OrderBy(x => x.Naziv).ToList();
                    foreach (var dodatnaUsluga in ucitaneDodatneUsluge)
                    {
                        if (dodatnaUsluga.Obrisan != true)
                        {
                            Console.WriteLine($"Naziv: {dodatnaUsluga.Naziv}, Iznos: {dodatnaUsluga.Iznos}");
                        }
                    }
                    SortiranjeDodatnihUsluga();
                    break;
                case 2:
                    ucitaneDodatneUsluge = ucitaneDodatneUsluge.OrderBy(x => x.Iznos).ToList();
                    foreach (var dodatnaUsluga in ucitaneDodatneUsluge)
                    {
                        if (dodatnaUsluga.Obrisan != true)
                        {
                            Console.WriteLine($"Naziv: {dodatnaUsluga.Naziv}, Iznos: {dodatnaUsluga.Iznos}");
                        }
                    }
                    SortiranjeDodatnihUsluga();
                    break;
                default:
                    break;
            }
        }
    }
}
