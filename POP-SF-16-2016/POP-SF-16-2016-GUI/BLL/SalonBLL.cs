using POP_SF_16_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.BLL
{
    class SalonBLL
    {

        public static void SalonMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA SALONOM =====");
                Console.WriteLine("1. Prikazi salon");
                Console.WriteLine("2. Dodaj salon");
                Console.WriteLine("3. Izmeni salon");
                Console.WriteLine("4. Izbrisi salon");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    PrikazSvihSalona();
                    break;
                case 2:
                    DodajNoviSalon();
                    break;
                case 3:
                    IzmeniSalon();
                    break;
                case 4:
                    IzbrisiSalon();
                    break;
                default:
                    break;
            }
        }

        private static void PrikazSvihSalona()
        {
            Console.WriteLine("===== LISTING SALONA =====");
            var ucitaniSaloni = Projekat.Instanca.Salon;
            for (int i = 0; i < ucitaniSaloni.Count; i++)
            {
                if (ucitaniSaloni[i].Obrisan != true)
                {
                    Console.WriteLine($"Naziv: {ucitaniSaloni[i].Naziv}, Adresa: {ucitaniSaloni[i].Adresa}, Telefon: {ucitaniSaloni[i].Telefon}, Websajt: {ucitaniSaloni[i].Websajt}");
                }
            }
            SalonMeni();
        }

        private static void DodajNoviSalon()
        {
            Console.WriteLine("===== DODAVANJE NOVOG SALONA =====");
            var ucitaniSaloni = Projekat.Instanca.Salon;

            Console.WriteLine("Naziv: ");
            string naziv = Console.ReadLine();
            Console.WriteLine("Adresa: ");
            string adresa = Console.ReadLine();
            Console.WriteLine("Telefon: ");
            string telefon = Console.ReadLine();
            Console.WriteLine("Email :");
            string email = Console.ReadLine();
            Console.WriteLine("Websajt: ");
            string websajt = Console.ReadLine();
            Console.WriteLine("PIB: ");
            int pib = int.Parse(Console.ReadLine());
            Console.WriteLine("Maticni broj: ");
            int maticniBroj = int.Parse(Console.ReadLine());
            Console.WriteLine("Broj ziro racuna: ");
            string brojZiroRacuna = Console.ReadLine();

            var noviSalon = new Salon()
            {
                Id = ucitaniSaloni.Count + 1,
                Naziv = naziv,
                Adresa = adresa,
                Telefon = telefon,
                Email = email,
                Websajt = websajt,
                PIB = pib,
                MaticniBroj = maticniBroj,
                BrojZiroRacuna = brojZiroRacuna
            };
            ucitaniSaloni.Add(noviSalon);
            Projekat.Instanca.Salon = ucitaniSaloni;
            SalonMeni();
        }

        private static void IzmeniSalon()
        {
            Console.WriteLine("===== IZMENA SALONA =====");
            var ucitaniSaloni = Projekat.Instanca.Salon;
            Salon salonZaIzmenu = new Salon();
            Console.WriteLine("Id salona za izmenu: ");
            int idSalonaZaIzmenu = int.Parse(Console.ReadLine());
            foreach (Salon salon in ucitaniSaloni)
            {
                if (salon.Id == idSalonaZaIzmenu)
                {
                    salonZaIzmenu = salon;
                }
            }

            int izbor = 0;
            do
            {
                Console.WriteLine("1. Izmena naziva");
                Console.WriteLine("2. Izmena adresa");
                Console.WriteLine("3. Izmena telefona");
                Console.WriteLine("4. Izmena email-a");
                Console.WriteLine("5. Izmena websajta");
                Console.WriteLine("6. Izmena PIB-a");
                Console.WriteLine("7. Izmena maticnog broja");
                Console.WriteLine("8. Izmena broja ziro racuna");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 8);
            switch (izbor)
            {
                case 1:
                    string izmenjenNaziv = "";
                    do
                    {
                        Console.WriteLine("Novi naziv salona: ");
                        izmenjenNaziv = Console.ReadLine();
                    } while (izmenjenNaziv == "");
                    salonZaIzmenu.Naziv = izmenjenNaziv;
                    break;
                case 2:
                    string izmenjenaAdresa = "";
                    do
                    {
                        Console.WriteLine("Nova adresa salona: ");
                        izmenjenaAdresa = Console.ReadLine();
                    } while (izmenjenaAdresa == "");
                    salonZaIzmenu.Adresa = izmenjenaAdresa;
                    break;
                case 3:
                    string izmenjenTelefon = "";
                    do
                    {
                        Console.WriteLine("Novi broj telefona: ");
                        izmenjenTelefon = Console.ReadLine();
                    } while (izmenjenTelefon == "");
                    salonZaIzmenu.Telefon = izmenjenTelefon;
                    break;
                case 4:
                    string izmenjenEmail = "";
                    do
                    {
                        Console.WriteLine("Novi email: ");
                        izmenjenEmail = Console.ReadLine();
                    } while (izmenjenEmail == "");
                    salonZaIzmenu.Email = izmenjenEmail;
                    break;
                case 5:
                    string izmenjenWebsajt = "";
                    do
                    {
                        Console.WriteLine("Novi websajt");
                        izmenjenWebsajt = Console.ReadLine();
                    } while (izmenjenWebsajt == "");
                    salonZaIzmenu.Websajt = izmenjenWebsajt;
                    break;
                case 6:
                    int izmenjenPIB = 0;
                    do
                    {
                        Console.WriteLine("Novi PIB: ");
                        izmenjenPIB = int.Parse(Console.ReadLine());
                    } while (izmenjenPIB < 0);
                    salonZaIzmenu.PIB = izmenjenPIB;
                    break;
                case 7:
                    int izmenjenMaticniBroj = 0;
                    do
                    {
                        Console.WriteLine("Novi maticni broj: ");
                        izmenjenMaticniBroj = int.Parse(Console.ReadLine());
                    } while (izmenjenMaticniBroj < 0);
                    salonZaIzmenu.MaticniBroj = izmenjenMaticniBroj;
                    break;
                case 8:
                    string izmenjenBrojZiroRacuna = "";
                    do
                    {
                        Console.WriteLine("Novi broj ziro racuna: ");
                        izmenjenBrojZiroRacuna = Console.ReadLine();
                    } while (izmenjenBrojZiroRacuna == "");
                    salonZaIzmenu.BrojZiroRacuna = izmenjenBrojZiroRacuna;
                    break;
                default:
                    break;
            }
            Projekat.Instanca.Salon = ucitaniSaloni;
            SalonMeni();
        }

        private static void IzbrisiSalon()
        {
            var ucitaniSaloni = Projekat.Instanca.Salon;
            Salon izbrisanSalon = null;
            do
            {
                Console.WriteLine("Id salona za brisanje: ");
                int idSalonaZaBrisanje = int.Parse(Console.ReadLine());
                foreach (Salon salon in ucitaniSaloni)
                {
                    if (salon.Obrisan != true && salon.Id == idSalonaZaBrisanje)
                    {
                        izbrisanSalon = salon;
                    }
                }
            } while (izbrisanSalon == null);
            izbrisanSalon.Obrisan = true;
            Projekat.Instanca.Salon = ucitaniSaloni;
            SalonMeni();
        }
    }
}