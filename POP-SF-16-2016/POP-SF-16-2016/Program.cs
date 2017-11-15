using POP_SF_16_2016_GUI.BLL;
using POP_SF_16_2016_GUI.Model;
using POP_SF_16_2016_GUI.Tests;
using POP_SF_16_2016_GUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI
{
    class Program
    {

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
            //var SalonLista = new List<Salon>();
            //SalonLista.Add(s1);
            //GenericSerializer.Serialize<Salon>("salon_namestaja.xml", SalonLista);


            Console.WriteLine($"===== Dobrodosli u salon namestaja {s1.Naziv}. PRIJAVA NA SISTEM =====");
            PrijavaNaSistem();
        }

        private static void PrijavaNaSistem()
        {
            var ucitaniKorisnici = Projekat.Instanca.Korisnik;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Korisnicko ime: ");
                string KorisnickoIme = Console.ReadLine();
                Console.WriteLine("Lozinka: ");
                string Lozinka = Console.ReadLine();
                foreach(Korisnik korisnik in ucitaniKorisnici)
                {
                    if(korisnik.KorisnickoIme == KorisnickoIme && korisnik.Lozinka == Lozinka)
                    {
                        IspisiGlavniMeni();
                        return;
                    }
                }
            }
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
                    Console.WriteLine("3. Rad sa akcijama");
                    Console.WriteLine("4. Rad sa dodatnim uslugama");
                    Console.WriteLine("5. Rad sa korisnicima");
                    Console.WriteLine("6. Rad sa prodajom namestaja");
                    Console.WriteLine("7. Rad sa salonom");               
                    Console.WriteLine("0. Izlaz iz aplikacije");
                    Console.Write("Unos: ");
                    izbor = int.Parse(Console.ReadLine());
                  
                    
                } while (izbor < 0 || izbor > 7);
                switch (izbor)
                {
                    case 1:
                        NamestajBLL.NamestajMeni();
                        break;
                    case 2:
                        TipNamestajaBLL.TipNamestajaMeni();
                        break;
                    case 3:
                        AkcijaBLL.AkcijeMeni();
                        break;
                    case 4:
                        DodatneUslugeBLL.DodatneUslugeMeni();
                        break;
                    case 5:
                        KorisniciBLL.KorisniciMeni();
                        break;
                    case 6:
                        ProdajaNamestajaBLL.ProdajaNamestajaMeni();
                        break;
                    case 7:
                        SalonBLL.SalonMeni();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);
        }
    }
}
