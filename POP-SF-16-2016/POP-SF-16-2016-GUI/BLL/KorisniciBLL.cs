using POP_SF_16_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.BLL
{
    class KorisniciBLL
    {

        public static void KorisniciMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA KORISNICIMA =====");
                Console.WriteLine("1. Prikazi korisnike");
                Console.WriteLine("2. Dodaj korisnika");
                Console.WriteLine("3. Izmeni korisnika");
                Console.WriteLine("4. Izbrisi korisnika");
                Console.WriteLine("5. Pretraga korisnika");
                Console.WriteLine("6. Sortiranje korisnika");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 6);
            switch (izbor)
            {
                case 1:
                    PrikazSvihKorisnika();
                    break;
                case 2:
                    DodajNovogKorisnika();
                    break;
                case 3:
                    IzmeniKorisnika();
                    break;
                case 4:
                    IzbrisiKorisnika();
                    break;
                case 5:
                    PretragaKorisnika();
                    break;
                case 6:
                    SortiranjeKorisnika();
                    break;
                default:
                    break;
            }
        }

        private static void PrikazSvihKorisnika()
        {
            Console.WriteLine("===== LISTING KORISNIKA =====");
            var ucitaniKorisnici = Projekat.Instanca.Korisnik;
            for (int i = 0; i < ucitaniKorisnici.Count; i++)
            {
                if (ucitaniKorisnici[i].Obrisan != true)
                {
                    Console.WriteLine($"Ime: {ucitaniKorisnici[i].Ime}, Prezime: {ucitaniKorisnici[i].Prezime}, Korisnicko ime: {ucitaniKorisnici[i].KorisnickoIme}, Tip korisnika: {ucitaniKorisnici[i].TipKorisnika}");
                }
            }
            KorisniciMeni();
        }

        private static void DodajNovogKorisnika()
        {
            Console.WriteLine("===== DODAVANJE NOVOG KORISNIKA =====");
            var ucitaniKorisnici = Projekat.Instanca.Korisnik;

            Console.WriteLine("Ime: ");
            string ime = Console.ReadLine();
            Console.WriteLine("Prezime: ");
            string prezime = Console.ReadLine();
            Console.WriteLine("Korisnicko ime: ");
            string korisnickoIme = Console.ReadLine();
            Console.WriteLine("Lozinka: ");
            string lozinka = Console.ReadLine();
            Console.WriteLine("Tip korisnika: 1) Administrator 2)Prodavac");
            TipKorisnika tipKorisnika;
            int izbor = int.Parse(Console.ReadLine());

            switch (izbor)
            {
                case 1:
                    tipKorisnika = TipKorisnika.Administrator;
                    break;
                case 2:
                    tipKorisnika = TipKorisnika.Prodavac;
                    break;
                default:
                    tipKorisnika = TipKorisnika.Prodavac;
                    break;
            }


            var noviKorisnik = new Korisnik()
            {
                Id = ucitaniKorisnici.Count + 1,
                Ime = ime,
                Prezime = prezime,
                KorisnickoIme = korisnickoIme,
                Lozinka = lozinka,
                TipKorisnika = tipKorisnika
            };

            ucitaniKorisnici.Add(noviKorisnik);
            Projekat.Instanca.Korisnik = ucitaniKorisnici;
            KorisniciMeni();
        }

        private static void IzmeniKorisnika()
        {
            Console.WriteLine("===== IZMENA KORISNIKA =====");
            var ucitaniKorisnici = Projekat.Instanca.Korisnik;
            Korisnik korisnikZaIzmenu = new Korisnik();
            Console.WriteLine("Id korisnika za izmenu: ");
            int idKorisnikaZaIzmenu = int.Parse(Console.ReadLine());
            foreach (Korisnik korisnik in ucitaniKorisnici)
            {
                if (korisnik.Id == idKorisnikaZaIzmenu)
                {
                    korisnikZaIzmenu = korisnik;
                }
            }

            int izbor = 0;
            do
            {
                Console.WriteLine("1. Izmena imena");
                Console.WriteLine("2. Izmena prezimena");
                Console.WriteLine("3. Izmena korisnickog imena");
                Console.WriteLine("4. Izmena lozinke");
                Console.WriteLine("5. Izmena tipa korisnika");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 5);
            switch (izbor)
            {
                case 1:
                    String izmenjenoIme = "";
                    do
                    {
                        Console.WriteLine("Novo ime korisnika: ");
                        izmenjenoIme = Console.ReadLine();
                    } while (izmenjenoIme == "");
                    korisnikZaIzmenu.Ime = izmenjenoIme;
                    break;
                case 2:
                    String izmenjenoPrezime = "";
                    do
                    {
                        Console.WriteLine("Novo prezime korisnika: ");
                        izmenjenoPrezime = Console.ReadLine();
                    } while (izmenjenoPrezime == "");
                    korisnikZaIzmenu.Prezime = izmenjenoPrezime;
                    break;
                case 3:
                    string izmenjenoKorisnickoIme = "";
                    do
                    {
                        Console.WriteLine("Novo korisnicko ime: ");
                        izmenjenoKorisnickoIme = Console.ReadLine();
                    } while (izmenjenoKorisnickoIme == "");
                    korisnikZaIzmenu.KorisnickoIme = izmenjenoKorisnickoIme;
                    break;
                case 4:
                    string izmenjenaLozinka = "";
                    do
                    {
                        Console.WriteLine("Nova lozinka: ");
                        izmenjenaLozinka = Console.ReadLine();
                    } while (izmenjenaLozinka == "");
                    korisnikZaIzmenu.Lozinka = izmenjenaLozinka;
                    break;

                case 5:
                    Console.WriteLine("Tip korisnika: 1) Administrator 2)Prodavac");
                    int izborTip = int.Parse(Console.ReadLine());

                    switch (izborTip)
                    {
                        case 1:
                            korisnikZaIzmenu.TipKorisnika = TipKorisnika.Administrator;
                            break;
                        case 2:
                            korisnikZaIzmenu.TipKorisnika = TipKorisnika.Prodavac;
                            break;
                        default:
                            korisnikZaIzmenu.TipKorisnika = TipKorisnika.Prodavac;
                            break;
                    }
                    break;
                default:
                    break;
            }

            Projekat.Instanca.Korisnik = ucitaniKorisnici;
            KorisniciMeni();
        }

        private static void IzbrisiKorisnika()
        {
            var ucitaniKorisnici = Projekat.Instanca.Korisnik;
            Korisnik izbrisanKorisnik = null;
            do
            {
                Console.WriteLine("Id korisnika za brisanje: ");
                int idKorisnikaZaBrisanje = int.Parse(Console.ReadLine());
                foreach (Korisnik korisnik in ucitaniKorisnici)
                {
                    if (korisnik.Obrisan != true && korisnik.Id == idKorisnikaZaBrisanje)
                    {
                        izbrisanKorisnik = korisnik;
                    }
                }
            } while (izbrisanKorisnik == null);
            izbrisanKorisnik.Obrisan = true;
            Projekat.Instanca.Korisnik = ucitaniKorisnici;
            KorisniciMeni();
        }

        private static void PretragaKorisnika()
        {
            var ucitaniKorisnici = Projekat.Instanca.Korisnik;
            int izbor = 0;
            do
            {
                Console.WriteLine("Pretraga korisnika po:");
                Console.WriteLine("1. Imenu");
                Console.WriteLine("2. Prezimenu");
                Console.WriteLine("3. Korisnickom imenu");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 3);
            switch (izbor)
            {
                case 1:
                    Console.WriteLine("Unesite ime za pretragu: ");
                    string ime = Console.ReadLine();
                    foreach (var korisnik in ucitaniKorisnici)
                    {
                        if (korisnik.Ime == ime && korisnik.Obrisan != true)
                        {
                            Console.WriteLine($"Ime: {korisnik.Ime}, Prezime: {korisnik.Prezime}, Korisnicko ime: {korisnik.KorisnickoIme}, Tip korisnika: {korisnik.TipKorisnika}");
                        }
                    }
                    PretragaKorisnika();
                    break;
                case 2:
                    Console.WriteLine("Unesite prezime za pretragu: ");
                    string prezime = Console.ReadLine();
                    foreach (var korisnik in ucitaniKorisnici)
                    {
                        if (korisnik.Prezime == prezime && korisnik.Obrisan != true)
                        {
                            Console.WriteLine($"Ime: {korisnik.Ime}, Prezime: {korisnik.Prezime}, Korisnicko ime: {korisnik.KorisnickoIme}, Tip korisnika: {korisnik.TipKorisnika}");
                        }
                    }
                    PretragaKorisnika();
                    break;
                case 3:
                    Console.WriteLine("Unesite korisnicko ime za pretragu: ");
                    string korisnickoIme = Console.ReadLine();
                    foreach (var korisnik in ucitaniKorisnici)
                    {
                        if (korisnik.KorisnickoIme == korisnickoIme && korisnik.Obrisan != true)
                        {
                            Console.WriteLine($"Ime: {korisnik.Ime}, Prezime: {korisnik.Prezime}, Korisnicko ime: {korisnik.KorisnickoIme}, Tip korisnika: {korisnik.TipKorisnika}");
                        }
                    }
                    PretragaKorisnika();
                    break;
                default:
                    break;
            }
        }
        private static void SortiranjeKorisnika()
        {
            var ucitaniKorisnici = Projekat.Instanca.Korisnik;
            int izbor = 0;
            do
            {
                Console.WriteLine("Sortiranje ispisa po:");
                Console.WriteLine("1. Imenu");
                Console.WriteLine("2. Prezimenu");
                Console.WriteLine("3. Korisnickom imenu");
                Console.WriteLine("4. Tipu korisnika");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    ucitaniKorisnici = ucitaniKorisnici.OrderBy(x => x.Ime).ToList();
                    foreach (var korisnik in ucitaniKorisnici)
                    {
                        if(korisnik.Obrisan != true)
                        {
                            Console.WriteLine($"Ime: {korisnik.Ime}, Prezime: {korisnik.Prezime}, Korisnicko ime: {korisnik.KorisnickoIme}, Tip korisnika: {korisnik.TipKorisnika}");
                        }
                    }
                    SortiranjeKorisnika();
                    break;
                case 2:
                    ucitaniKorisnici = ucitaniKorisnici.OrderBy(x => x.Prezime).ToList();
                    foreach (var korisnik in ucitaniKorisnici)
                    {
                        if(korisnik.Obrisan != true)
                        {
                            Console.WriteLine($"Ime: {korisnik.Ime}, Prezime: {korisnik.Prezime}, Korisnicko ime: {korisnik.KorisnickoIme}, Tip korisnika: {korisnik.TipKorisnika}");
                        }
                    }
                    SortiranjeKorisnika();
                    break;
                case 3:
                    ucitaniKorisnici = ucitaniKorisnici.OrderBy(x => x.KorisnickoIme).ToList();
                    foreach (var korisnik in ucitaniKorisnici)
                    {
                        if (korisnik.Obrisan != true)
                        {
                            Console.WriteLine($"Ime: {korisnik.Ime}, Prezime: {korisnik.Prezime}, Korisnicko ime: {korisnik.KorisnickoIme}, Tip korisnika: {korisnik.TipKorisnika}");
                        }
                    }
                    SortiranjeKorisnika();
                    break;
                case 4:
                    ucitaniKorisnici = ucitaniKorisnici.OrderBy(x => x.TipKorisnika).ToList();
                    foreach (var korisnik in ucitaniKorisnici)
                    {
                        if (korisnik.Obrisan != true)
                        {
                            Console.WriteLine($"Ime: {korisnik.Ime}, Prezime: {korisnik.Prezime}, Korisnicko ime: {korisnik.KorisnickoIme}, Tip korisnika: {korisnik.TipKorisnika}");
                        }
                    }
                    SortiranjeKorisnika();
                    break;
                default:
                    break;


            }
        }
    }
}
