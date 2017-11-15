using POP_SF_16_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.BLL
{
    public class TipNamestajaBLL
    {
        public static void TipNamestajaMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA TIPOM NAMESTAJA =====");
                Console.WriteLine("1. Prikazi tip namestaj");
                Console.WriteLine("2. Dodaj tip namestaja");
                Console.WriteLine("3. Izmeni tip namestaja");
                Console.WriteLine("4. Izbrisi tip namestaja");
                Console.WriteLine("5. Sortiranje tipa namestaja");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 5);
            switch (izbor)
            {
                case 1:
                    PrikazSvihTipovaNamestaja();
                    break;
                case 2:
                    DodajNoviTipNamestaja();
                    break;
                case 3:
                    IzmeniTipNamestaja();
                    break;
                case 4:
                    IzbrisiTipNamestaja();
                    break;
                case 5:
                    SortiranjeTipaNamestaja();
                    break;
                default:
                    break;
            }
        }

        private static void PrikazSvihTipovaNamestaja()
        {
            Console.WriteLine("==== LISTING TIPOVA NAMESTAJA ====");
            var ucitaniTipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
            //dodati sortiranje

            for (int i = 0; i < ucitaniTipoviNamestaja.Count; i++)
            {
                if (ucitaniTipoviNamestaja[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Id: {ucitaniTipoviNamestaja[i].Id}, Naziv: {ucitaniTipoviNamestaja[i].Naziv}");
                }
            }
            TipNamestajaMeni();
        }

        private static void DodajNoviTipNamestaja()
        {

            Console.WriteLine("===== DODAVANJE NOVOG TIPA NAMESTAJA =====");
            var ucitaniTipoviNamestaja = Projekat.Instanca.TipoviNamestaja;

            Console.WriteLine("Id tipa namestaja: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Naziv tipa namestaja: ");
            string nazivTipaNamestaja = Console.ReadLine();

            var noviTipNamestaja = new TipNamestaja()
            {
                Id = ucitaniTipoviNamestaja.Count + 1,
                Naziv = nazivTipaNamestaja
            };
            ucitaniTipoviNamestaja.Add(noviTipNamestaja);
            Projekat.Instanca.TipoviNamestaja = ucitaniTipoviNamestaja;
            TipNamestajaMeni();
        }

        private static void IzmeniTipNamestaja()
        {
            Console.WriteLine("===== IZMENA TIPA NAMESTAJA =====");
            var ucitaniTipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
            TipNamestaja tipNamestajaZaIzmenu = new TipNamestaja();
            Console.WriteLine("ID tipa namestaja za izmenu: ");
            int idTipaNamestaja = int.Parse(Console.ReadLine());
            foreach (TipNamestaja tipNamestaja in ucitaniTipoviNamestaja)
            {
                if (tipNamestaja.Id == idTipaNamestaja)
                {
                    tipNamestajaZaIzmenu = tipNamestaja;
                }
            }

            int izbor = 0;
            do
            {
                Console.WriteLine("1. Izmena naziva");
                Console.WriteLine("0. Povratak na glavni meni");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 1);
            switch (izbor)
            {
                case 1:
                    string izmenjenNazivTipaNamestaja = "";
                    do
                    {
                        Console.WriteLine("Novi naziv tipa namestaja: ");
                        izmenjenNazivTipaNamestaja = Console.ReadLine();
                    } while (izmenjenNazivTipaNamestaja == "");
                    tipNamestajaZaIzmenu.Naziv = izmenjenNazivTipaNamestaja;
                    break;
                default:
                    break;
            }
            Projekat.Instanca.TipoviNamestaja = ucitaniTipoviNamestaja;
            TipNamestajaMeni();

        }

        private static void IzbrisiTipNamestaja()
        {
            var ucitaniTipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
            TipNamestaja izbrisanTipNamestaja = null;
            do
            {
                Console.WriteLine("Id tipa namestaja za brisanje: ");
                int idTipaNamestajaZaBrisanje = int.Parse(Console.ReadLine());
                foreach (TipNamestaja tipNamestaja in ucitaniTipoviNamestaja)
                {
                    if (tipNamestaja.Obrisan != true && tipNamestaja.Id == idTipaNamestajaZaBrisanje)
                    {
                        izbrisanTipNamestaja = tipNamestaja;
                    }
                }
            } while (izbrisanTipNamestaja == null);
            izbrisanTipNamestaja.Obrisan = true;
            Projekat.Instanca.TipoviNamestaja = ucitaniTipoviNamestaja;
            TipNamestajaMeni();
        }

        private static void SortiranjeTipaNamestaja()
        {
            var ucitanTipNamestaja = Projekat.Instanca.TipoviNamestaja;
            int izbor = 0;
            do
            {
                Console.WriteLine("Sortiranje ispisa po:");
                Console.WriteLine("1. Nazivu");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 1);
            switch (izbor)
            {
                case 1:
                    ucitanTipNamestaja = ucitanTipNamestaja.OrderBy(x => x.Naziv).ToList();
                    foreach (var tipNamestaja in ucitanTipNamestaja)
                    {
                        if(tipNamestaja.Obrisan != true)
                        {
                            Console.WriteLine($"Id: {tipNamestaja.Id}, Naziv: {tipNamestaja.Naziv}");
                        }
                        
                    }
                    SortiranjeTipaNamestaja();
                    break;
                default:
                    break;
            }
        }


    }
}
