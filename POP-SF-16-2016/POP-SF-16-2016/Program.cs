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

            TipoviNamestaja.Add(tp1);

            var tp2 = new TipNamestaja()
            {
                Id = 2,
                Naziv = "Sofa"
            };

            TipoviNamestaja.Add(tp2);

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
            IspisiGlavniMeni();
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
                    case 2:
                        RadSaTipomNamestaja();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);
        }

        private static void RadSaTipomNamestaja()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA TIPOM NAMESTAJA =====");
                IspisiCRUDMeni();

                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);

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
                default:
                    break;
            }
        }

        private static void IzbrisiTipNamestaja()
        {
            TipNamestaja izbrisanTipNamestaja = null;
            do
            {
                Console.WriteLine("Id tipa namestaja za brisanje");
                int idTipaNamestaja = int.Parse(Console.ReadLine());
                foreach (TipNamestaja tipNamestaja in TipoviNamestaja)
                {
                    if (tipNamestaja.Obrisan == false && tipNamestaja.Id.Equals(idTipaNamestaja))
                    {
                        izbrisanTipNamestaja = tipNamestaja;
                    }
                }
            } while (izbrisanTipNamestaja == null);
            izbrisanTipNamestaja.Obrisan = true;
        }

        private static void IzmeniTipNamestaja()
        {
            Console.WriteLine("===== IZMENA TIPA NAMESTAJA =====");
            Console.WriteLine("ID tipa namestaja za izmenu: ");
            int idTipaNamestaja = int.Parse(Console.ReadLine());
            TipNamestaja trazeniTipNamestaja = PronadjiTipNamestaja(idTipaNamestaja);

            int izbor = 0;
            do
            {
                Console.WriteLine("1. Izmena naziva");
                Console.WriteLine("0. Povratak na glavni meni");

                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 1);
            switch (izbor)
            {
                case 1:
                    IzmenaNazivaTipaNamestaja(idTipaNamestaja);
                    break;
                default:
                    break;
            }
        }

        private static void IzmenaNazivaTipaNamestaja(int idTipaNamestaja)
        {
            string izmenjenNazivTipaNamestaja = "";
            ;
            do
            {
                Console.WriteLine("Novi naziv tipa namestaja: ");
                izmenjenNazivTipaNamestaja = Console.ReadLine();
            } while (izmenjenNazivTipaNamestaja == "");

            TipNamestaja pronadjenTipNamestaja = PronadjiTipNamestaja(idTipaNamestaja);
            pronadjenTipNamestaja.Naziv = izmenjenNazivTipaNamestaja;
        }

        private static void DodajNoviTipNamestaja()
        {
            Console.WriteLine("===== DODAVANJE NOVOG TIPA NAMESTAJA =====");
            Console.WriteLine("Id tipa namestaja: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Naziv tipa namestaja: ");
            string nazivTipaNamestaja = Console.ReadLine();
            
            var noviTipNamestaja = new TipNamestaja() { Id = id, Naziv = nazivTipaNamestaja };
            TipoviNamestaja.Add(noviTipNamestaja);
        }

        private static void PrikazSvihTipovaNamestaja()
        {
            Console.WriteLine("==== LISTING TIPOVA NAMESTAJA ====");

            for (int i = 0; i < TipoviNamestaja.Count; i++)
            {   
                if(TipoviNamestaja[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Id: {TipoviNamestaja[i].Id}, Naziv: {TipoviNamestaja[i].Naziv}");
                }
                
            }
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
                    case 4:
                        IzbrisiNamestaj();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);

        }

        private static void IzbrisiNamestaj()
        {
            Namestaj izbrisanNamestaj = null;
            do
            {
                Console.WriteLine("Id namestaja za brisanje: ");
                int idNamestajaZaBrisanje = int.Parse(Console.ReadLine());
                foreach (Namestaj namestaj in Namestaj)
                {
                    if (namestaj.Obrisan == false & namestaj.Id.Equals(idNamestajaZaBrisanje))
                    {
                        izbrisanNamestaj = namestaj;
                    }
                }
            } while (izbrisanNamestaj == null);
            izbrisanNamestaj.Obrisan = true;

        }

        private static void IzmeniNamestaj()
        {
            Console.WriteLine("==== IZMENA NAMESTAJA ====");
            Console.WriteLine("ID namestaja za izmenu");
            int idNamestaja = int.Parse(Console.ReadLine());

            Namestaj trazeniNamestaj = PronadjiNamestaj(idNamestaja);

            int izbor = 0;
            do
            {
                Console.WriteLine("1. Izmena naziva");
                Console.WriteLine("2. Izmena sifre");
                Console.WriteLine("3. Izmena cene");
                Console.WriteLine("4. Izmena kolicine u magacinu");
                Console.WriteLine("5. Izmeni tip namestaja");
                //akcija?
                Console.WriteLine("0. Povratak na glavni meni");

                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 5);
            switch (izbor)
            {
                case 1:
                    IzmenaNazivaNamestaja(trazeniNamestaj);
                    break;
                    
                case 2:
                    IzmenaSifreNamestaja(trazeniNamestaj);
                    break;
                case 3:
                    IzmenaCeneNamestaja(trazeniNamestaj);
                    break;
                case 4:
                    IzmenaKolicineNamestaja(trazeniNamestaj);
                    break;
                case 5:
                    IzmenaTipaNamestaja(trazeniNamestaj);
                    break;
                default:
                    break;
            }
        }

        private static void IzmenaTipaNamestaja(Namestaj trazeniNamestaj)
        {
            TipNamestaja noviTipNamestaja = null;
            int unetNoviTipNamestaja = 0;
            do
            {
                Console.WriteLine("Novi tip namestaja: ");
                unetNoviTipNamestaja = int.Parse(Console.ReadLine());
                foreach (TipNamestaja tipNamestaja in TipoviNamestaja)
                {
                    if (tipNamestaja.Id.Equals(unetNoviTipNamestaja))
                    {
                        noviTipNamestaja = tipNamestaja;
                    }
                }
            } while (noviTipNamestaja == null);
            trazeniNamestaj.TipNamestaja.Id = noviTipNamestaja.Id;
        }

        private static void IzmenaKolicineNamestaja(Namestaj trazeniNamestaj)
        {
            int novaKolicina = 0;
            do
            {
                Console.WriteLine("Nova kolicina: ");
                novaKolicina = int.Parse(Console.ReadLine());
            } while (novaKolicina == 0);
            trazeniNamestaj.KolicinaUMagacinu = novaKolicina;
        }

        private static void IzmenaCeneNamestaja(Namestaj trazeniNamestaj)
        {
            double novaCena = 0;
            do
            {
                Console.WriteLine("Nova cena: ");
                novaCena = double.Parse(Console.ReadLine());
            } while (novaCena == 0);
            trazeniNamestaj.Cena = novaCena;
        }

        private static void IzmenaSifreNamestaja(Namestaj trazeniNamestaj)
        {
            string novaSifra = "";
            do
            {
                Console.WriteLine("Nova sifra: ");
                novaSifra = Console.ReadLine();
            } while (novaSifra == "");
            trazeniNamestaj.Sifra = novaSifra;
        }

        private static void IzmenaNazivaNamestaja(Namestaj trazeniNamestaj)
        {
            string noviNaziv = "";
            do
            {
                Console.WriteLine("Novi naziv: ");
                noviNaziv = Console.ReadLine();
            } while (noviNaziv == "");
            trazeniNamestaj.Naziv = noviNaziv;
            
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
            Console.WriteLine("Unesite ID tipa namestaja: "); 
            int idTipaNamestaja = int.Parse(Console.ReadLine());

            var noviNamestaj = new Namestaj()
            {
                Id = Namestaj.Count + 1,
                Naziv = naziv,
                Cena = cena,
                TipNamestaja = PronadjiTipNamestaja(idTipaNamestaja)
            };
            Namestaj.Add(noviNamestaj);

        }

        private static void PrikaziNamestaj()
        {
            Console.WriteLine("==== LISTING NAMESTAJA ====");

            for (int i = 0; i < Namestaj.Count; i++)
            {   
                if(Namestaj[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. naziv: {Namestaj[i].Naziv}, cena: {Namestaj[i].Cena}, id tipa namestaja: {Namestaj[i].TipNamestaja.Id}, obrisan: {Namestaj[i].Obrisan}");
                }
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

        private static TipNamestaja PronadjiTipNamestaja(int IdTipaNamestaja)
        {
            TipNamestaja trazeniTipNamestaja = null;
            foreach (var tipNamestaja in TipoviNamestaja)
            {
                if (tipNamestaja.Id == IdTipaNamestaja)
                {
                    trazeniTipNamestaja = tipNamestaja;
                }
            }
            return trazeniTipNamestaja;
        }

        private static Namestaj PronadjiNamestaj(int Id)
        {
            Namestaj pronadjenNamestaj = null;
            foreach(Namestaj namestaj in Namestaj)
            {
                if (namestaj.Id.Equals(Id))
                {
                    pronadjenNamestaj = namestaj;
                }
            }
            return pronadjenNamestaj;
        }

    }
}