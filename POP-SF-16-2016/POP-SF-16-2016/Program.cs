using POP_SF_16_2016.Model;
using POP_SF_16_2016.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016
{
    class Program
    {
        private static List<Namestaj> Namestaj = new List<Namestaj>();
        private static List<TipNamestaja> TipoviNamestaja = new List<TipNamestaja>();
        static void Main(string[] args)
        {
            Salon s1 = new Salon()
            {
                Id = 1,
                Adresa = "Trg Dositeja Obradovica 6",
                BrojZiroRacuna = "840-000171666-45",
                Email = "dekan@ftn.uns.ac.rs",
                MaticniBroj = 324324324,
                Naziv = "Forma FTNale",
                PIB = 123213,
                Telefon = "021/454-3434",
                Websajt = "http://www.ftn.uns.ac.rs"
            };

            var tp1 = new TipNamestaja()
            {
                Id = 1,
                Naziv = "Krevet"
            };

            var tp2 = new TipNamestaja()
            {
                Id = 2,
                Naziv = "Sofa"
            };
 
            var namestaj1 = new Namestaj()
            {
                Id = 1,
                Cena = 777,
                TipNamestaja = tp1,
                Naziv = "Ekstra krevet socijalni",
                KolicinaUMagacinu = 100,
                Sifra = "KR3993434SC"
            };
            Namestaj.Add(namestaj1);

            Console.WriteLine($"===== Dobrodosli u salon namestaja {s1.Naziv}=====");
            Console.ReadLine();
        }

        private static void IspisiGlavniMeni()
        {
            int izbor = 0;
            do
            {
                do
                {
                    Console.WriteLine("==== GLAVNI MENI =====");
                    Console.WriteLine("1. Rad sa namestajem");
                    Console.WriteLine("2. Rad sa tipom namestaja");
                    //... dovrsiti
                    Console.WriteLine("0. Izlaz iz aplikacije");
                    
                    izbor = int.Parse(Console.ReadLine());
                } while (izbor < 0 || izbor > 2);
                switch (izbor)
                {
                    case 1:
                        NamestajMeni();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);
        }

        private static void NamestajMeni()
        {
            int izbor = 0;
            do
            {
                do
                {
                    Console.WriteLine("===== RAD SA NAMESTAJEM =====");
                    IspisiCRUDMeni();

                    izbor = int.Parse(Console.ReadLine());
                } while (izbor < 0 || izbor > 4);

                switch (izbor)
                {
                    case 1:
                        PrikaziNamestaj();
                        break;
                    case 2:
                        DodajNamestaj();
                        break;
                    case 3:
                        IzmeniNamestaj();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);

        }

        private static void IzmeniNamestaj()
        {
            Console.WriteLine("==== IZMENA NAMESTAJA ====");
            Console.WriteLine("ID namestaja");
            string idNamestaja = Console.ReadLine();

            
        }

        private static void DodajNamestaj()
        {
            Console.WriteLine("===== DODAVANJE NOVOG NAMESTAJA ====");

            Console.WriteLine("Unesite naziv: ");
            string naziv = Console.ReadLine();
            Console.WriteLine("Unesite sifru: ");
            string Sifra = Console.ReadLine();
            Console.WriteLine("Unesite cenu: ");
            double cena = double.Parse(Console.ReadLine());
            Console.WriteLine("Kolicina u magacinu: ");
            int KolicinaUMagacinu = int.Parse(Console.ReadLine());
            Console.WriteLine("Tip namestaja: ");

            Console.WriteLine("Unesite ID tipa namestaja: "); 
            int idTipaNamestaja = int.Parse(Console.ReadLine());

            TipNamestaja trazeniTipNamestaja = null;
            foreach (var tipNamestaja in TipoviNamestaja)
            {
                if (tipNamestaja.Id == idTipaNamestaja)
                {      
                    trazeniTipNamestaja = tipNamestaja;
                }

                var noviNamestaj = new Namestaj()
                {
                    Id = Namestaj.Count + 1,
                    Naziv = naziv,
                    Cena = cena,
                    TipNamestaja = trazeniTipNamestaja
                };

                Namestaj.Add(noviNamestaj);
            }
        }

        private static void PrikaziNamestaj()
        {
            Console.WriteLine("==== LISTING NAMESTAJA ====");

            for (int i = 0; i < Namestaj.Count; i++)
            {
                Console.WriteLine($"{i + 1}. naziv: {Namestaj[i].Naziv}, cena: {Namestaj[i].Cena}");
            }
        }

        private static void IspisiCRUDMeni()
        {
            Console.WriteLine("1. Prikazi listing");
            Console.WriteLine("2. Dodaj novi");
            Console.WriteLine("3. Izmeni postojeci");
            Console.WriteLine("4. Obrisi postojeci");
            Console.WriteLine("0. Povratak na glavni meni");
        }

    }
}


