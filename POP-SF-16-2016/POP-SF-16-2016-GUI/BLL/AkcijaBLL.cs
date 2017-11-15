using POP_SF_16_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.BLL
{
    class AkcijaBLL
    {
        public static void AkcijeMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA AKCIJAMA =====");
                Console.WriteLine("1. Prikazi akcije");
                Console.WriteLine("2. Dodaj akciju");
                Console.WriteLine("3. Izmeni akciju");
                Console.WriteLine("4. Izbrisi akciju");
                Console.WriteLine("5. Sortiranje akcija");
                Console.WriteLine("6. Prikaz aktuelnih akcija");
                Console.WriteLine("0. Izlaz");

                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 6);
            switch (izbor)
            {
                case 1:
                    PrikazSvihAkcija();
                    break;
                case 2:
                    DodajNovuAkciju();
                    break;
                case 3:
                    IzmeniAkciju();
                    break;
                case 4:
                    IzbrisiAkciju();
                    break;
                case 5:
                    SortiranjeAkcija();
                    break;
                case 6:
                    PrikazAktuelnihAkcija();
                    break;
                default:
                    break;
            }
        }

        private static void PrikazSvihAkcija()
        {
            Console.WriteLine("===== LISTING AKCIJA =====");
            var ucitaneAkcije = Projekat.Instanca.Akcija;
            for (int i = 0; i < ucitaneAkcije.Count; i++)
            {
                if (ucitaneAkcije[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Id akcije: {ucitaneAkcije[i].Id}, Popust: {ucitaneAkcije[i].Popust}, Datum pocetka: {ucitaneAkcije[i].DatumPocetka}, Datum zavrsetka: {ucitaneAkcije[i].DatumZavrsetka}, Id namestaja na akciji: {ucitaneAkcije[i].IdNamestaja}");
                }
            }
            AkcijeMeni();
        }

        private static void DodajNovuAkciju()
        {
            Console.WriteLine("===== DODAVANJE NOVE AKCIJE =====");
            var ucitaneAkcije = Projekat.Instanca.Akcija;

            Console.WriteLine("Datum pocetka (dan/mesec/godina): ");
            var datumPocetka = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Datum zavrsetka (dan/mesec/godina): ");
            var datumZavrsetka = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Id namestaja na akciji: ");
            int idNamestajaNaAkciji = int.Parse(Console.ReadLine());
            Console.WriteLine("Popust: ");
            decimal popust = decimal.Parse(Console.ReadLine());

            var novaAkcija = new Akcija()
            {
                Id = ucitaneAkcije.Count + 1,
                DatumPocetka = datumPocetka,
                DatumZavrsetka = datumZavrsetka,
                IdNamestaja = idNamestajaNaAkciji,
                Popust = popust
            };
            ucitaneAkcije.Add(novaAkcija);
            Projekat.Instanca.Akcija = ucitaneAkcije;
            AkcijeMeni();
        }

        private static void IzmeniAkciju()
        {
            Console.WriteLine("===== IZMENA AKCIJE =====");
            var ucitaneAkcije = Projekat.Instanca.Akcija;
            Akcija akcijaZaIzmenu = new Akcija();
            Console.WriteLine("Id akcije za izmenu: ");
            int idAkcijeZaIzmenu = int.Parse(Console.ReadLine());
            foreach (Akcija akcija in ucitaneAkcije)
            {
                if (akcija.Id == idAkcijeZaIzmenu)
                {
                    akcijaZaIzmenu = akcija;
                }
            }

            int izbor = 0;
            do
            {
                Console.WriteLine("1. Izmena datuma pocetka");
                Console.WriteLine("2. Izmena datuma zavrsetka");
                Console.WriteLine("3. Izmena id-a namestaja");
                Console.WriteLine("4. Izmena popusta");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    string izmenjenDatumPocetka = "";
                    do
                    {
                        Console.WriteLine("Novi datum pocetka akcije (dan/mesec/godina): ");
                        izmenjenDatumPocetka = Console.ReadLine();
                    } while (izmenjenDatumPocetka == "");
                    akcijaZaIzmenu.DatumPocetka = DateTime.Parse(izmenjenDatumPocetka);
                    break;

                case 2:
                    string izmenjenDatumZavrsetka = "";
                    do
                    {
                        Console.WriteLine("Novi datum zavrsetka akcije (dan/mesec/godina): ");
                        izmenjenDatumZavrsetka = Console.ReadLine();
                    } while (izmenjenDatumZavrsetka == "");
                    akcijaZaIzmenu.DatumZavrsetka = DateTime.Parse(izmenjenDatumZavrsetka);
                    break;
                case 3:
                    int izmenjenIdNamestajaNaAkciji = 0;
                    do
                    {
                        Console.WriteLine("Novi id namestaja: ");
                        izmenjenIdNamestajaNaAkciji = int.Parse(Console.ReadLine());
                    } while (izmenjenIdNamestajaNaAkciji < 0);
                    akcijaZaIzmenu.IdNamestaja = izmenjenIdNamestajaNaAkciji;
                    break;
                case 4:
                    decimal izmenjenPopust = 0;
                    do
                    {
                        Console.WriteLine("Novi popust: ");
                        izmenjenPopust = decimal.Parse(Console.ReadLine());
                    } while (izmenjenPopust < 0);
                    akcijaZaIzmenu.Popust = izmenjenPopust;
                    break;
                default:
                    break;
            }
            Projekat.Instanca.Akcija = ucitaneAkcije;
            AkcijeMeni();
        }

        private static void IzbrisiAkciju()
        {
            var ucitaneAkcije = Projekat.Instanca.Akcija;
            Akcija izbrisanaAkcija = new Akcija();
            do
            {
                Console.WriteLine("Id akcije za brisanje: ");
                int idAkcijeZaBrisanje = int.Parse(Console.ReadLine());
                foreach (Akcija akcija in ucitaneAkcije)
                {
                    if (akcija.Obrisan != true && akcija.Id == idAkcijeZaBrisanje)
                    {
                        izbrisanaAkcija = akcija;
                    }
                }
            } while (izbrisanaAkcija == null);
            izbrisanaAkcija.Obrisan = true;
            Projekat.Instanca.Akcija = ucitaneAkcije;
            AkcijeMeni();
        }

        private static void SortiranjeAkcija()
        {
            var ucitaneAkcije = Projekat.Instanca.Akcija;
            int izbor = 0;
            do
            {
                Console.WriteLine("Sortiranje akcija po:");
                Console.WriteLine("1. Datumu pocetka akcije");
                Console.WriteLine("2. Datumu zavrsetka akcije");
                Console.WriteLine("3. Popustu");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 3);
            switch (izbor)
            {
                case 1:
                    ucitaneAkcije = ucitaneAkcije.OrderBy(x => x.DatumPocetka).ToList();
                    foreach (var akcija in ucitaneAkcije)
                    {
                        if(akcija.Obrisan != true)
                        {
                            Console.WriteLine($"Datum pocetka: {akcija.DatumPocetka}, Datum zavrsetka: {akcija.DatumZavrsetka}, Popust: {akcija.Popust}");
                        }
                    }
                    SortiranjeAkcija();
                    break;
                case 2:
                    ucitaneAkcije = ucitaneAkcije.OrderBy(x => x.DatumZavrsetka).ToList();
                    foreach (var akcija in ucitaneAkcije)
                    {
                        if (akcija.Obrisan != true)
                        {
                            Console.WriteLine($"Datum pocetka: {akcija.DatumPocetka}, Datum zavrsetka: {akcija.DatumZavrsetka}, Popust: {akcija.Popust}");
                        }
                    }
                    SortiranjeAkcija();
                    break;
                case 3:
                    ucitaneAkcije = ucitaneAkcije.OrderBy(x => x.Popust).ToList();
                    foreach (var akcija in ucitaneAkcije)
                    {
                        if (akcija.Obrisan != true)
                        {
                            Console.WriteLine($"Datum pocetka: {akcija.DatumPocetka}, Datum zavrsetka: {akcija.DatumZavrsetka}, Popust: {akcija.Popust}");
                        }
                    }
                    SortiranjeAkcija();
                    break;
            }
        }


        private static void PrikazAktuelnihAkcija()
        {
            var ucitaneAkcije = Projekat.Instanca.Akcija;
            foreach(var akcija in ucitaneAkcije)
            {
                if(akcija.DatumPocetka < DateTime.Now && DateTime.Now < akcija.DatumZavrsetka)
                {
                    
                    Console.WriteLine($"Datum pocetka: {akcija.DatumPocetka}, Datum zavrsetka: {akcija.DatumZavrsetka}, Popust: {akcija.Popust}");
                }
            }
            AkcijeMeni();
        }


    }
}
