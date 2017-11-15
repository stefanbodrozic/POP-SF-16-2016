using POP_SF_16_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.BLL
{
    public class NamestajBLL
    {
        
        public static void NamestajMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA NAMESTAJEM =====");
                Console.WriteLine("1. Prikazi namestaj");
                Console.WriteLine("2. Dodaj namestaj");
                Console.WriteLine("3. Izmeni namestaj");
                Console.WriteLine("4. Izbrisi namestaj");
                Console.WriteLine("5. Pretraga namestaja");
                Console.WriteLine("6. Sortiranje namestaja");
                Console.WriteLine("0. Izlaz");

                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 6);

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
                case 5:
                    PretragaNamestaja();
                    break;
                case 6:
                    SortiranjeNamestaja();
                    break;       
                default:
                    break;
            }
        }

        private static void PrikaziNamestaj()
        {
            Console.WriteLine("===== LISTING NAMESTAJA =====");
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            
            for (int i = 0; i < ucitanNamestaj.Count; i++)
            {
                if (ucitanNamestaj[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Naziv: {ucitanNamestaj[i].Naziv}, Cena: {ucitanNamestaj[i].Cena}, Sifra: {ucitanNamestaj[i].Sifra}, Id tipa namestaja: {ucitanNamestaj[i].TipNamestajaId}");
                }
            }
            NamestajMeni();
        }

        private static void DodajNamestaj()
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;

            Console.WriteLine("===== DODAVANJE NOVOG NAMESTAJA ====");

            Console.WriteLine("Unesite naziv: ");
            string naziv = Console.ReadLine();
            Console.WriteLine("Unesite sifru: ");
            string sifra = Console.ReadLine();
            Console.WriteLine("Unesite cenu: ");
            double cena = double.Parse(Console.ReadLine());
            Console.WriteLine("Unesite kolicinu u magacinu: ");
            int kolicinaUMagacinu = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite ID tipa namestaja: ");
            int idTipaNamestaja = int.Parse(Console.ReadLine());

            var noviNamestaj = new Namestaj()
            {
                Id = ucitanNamestaj.Count + 1,
                Naziv = naziv,
                Cena = cena,
                Sifra = sifra,
                TipNamestajaId = TipNamestaja.PronadjiTipNamestajaPoId(idTipaNamestaja).Id
                
            };
            ucitanNamestaj.Add(noviNamestaj);
            Projekat.Instanca.Namestaj = ucitanNamestaj;
            NamestajMeni();
        }


        private static void IzmeniNamestaj()
        {
            Console.WriteLine("==== IZMENA NAMESTAJA ====");
            Console.WriteLine("ID namestaja za izmenu");

            int idNamestaja = int.Parse(Console.ReadLine());
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            Namestaj namestajZaIzmenu = new Namestaj();

            foreach (Namestaj namestaj in ucitanNamestaj)
            {
                if (namestaj.Id == idNamestaja)
                {
                    namestajZaIzmenu = namestaj;
                }
            }

            int izbor = 0;
            do
            {
                Console.WriteLine("1. Izmena naziva");
                Console.WriteLine("2. Izmena sifre");
                Console.WriteLine("3. Izmena cene");
                Console.WriteLine("4. Izmena kolicine u magacinu");
                Console.WriteLine("5. Izmeni tip namestaja");
                Console.WriteLine("0. Povratak na glavni meni");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 5);
            switch (izbor)
            {
                case 1:

                    string noviNaziv = "";
                    do
                    {
                        Console.WriteLine("Novi naziv: ");
                        noviNaziv = Console.ReadLine();
                    } while (noviNaziv == "");
                    namestajZaIzmenu.Naziv = noviNaziv;
                    break;

                case 2:

                    string novaSifra = "";
                    do
                    {
                        Console.WriteLine("Nova sifra: ");
                        novaSifra = Console.ReadLine();
                    } while (novaSifra == "");
                    namestajZaIzmenu.Sifra = novaSifra;
                    break;
                case 3:

                    double novaCena = 0;
                    do
                    {
                        Console.WriteLine("Nova cena: ");
                        novaCena = double.Parse(Console.ReadLine());
                    } while (novaCena == 0);
                    namestajZaIzmenu.Cena = novaCena;
                    break;
                case 4:

                    int novaKolicina = 0;
                    do
                    {
                        Console.WriteLine("Nova kolicina: ");
                        novaKolicina = int.Parse(Console.ReadLine());
                    } while (novaKolicina < 0);
                    namestajZaIzmenu.KolicinaUMagacinu = novaKolicina;
                    break;
                case 5:

                    var tipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
                    TipNamestaja noviTipNamestaja = null;
                    int unetNoviTipNamestajaId = 0;
                    do
                    {
                        Console.WriteLine("Novi tip namestaja: ");
                        unetNoviTipNamestajaId = int.Parse(Console.ReadLine());
                        foreach (TipNamestaja tipNamestaja in tipoviNamestaja)
                        {
                            if (tipNamestaja.Id == unetNoviTipNamestajaId)
                            {
                                noviTipNamestaja = tipNamestaja;
                            }
                        }
                    } while (noviTipNamestaja == null);
                    namestajZaIzmenu.TipNamestajaId = noviTipNamestaja.Id;
                    break;
                default:
                    break;
            }
            Projekat.Instanca.Namestaj = ucitanNamestaj;
            NamestajMeni();
        }


        private static void IzbrisiNamestaj()
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            Namestaj izbrisanNamestaj = null;
            do
            {
                Console.WriteLine("Id namestaja za brisanje: ");
                int idNamestajaZaBrisanje = int.Parse(Console.ReadLine());
                foreach (Namestaj namestaj in ucitanNamestaj)
                {
                    if (namestaj.Obrisan == false & namestaj.Id == idNamestajaZaBrisanje)
                    {
                        izbrisanNamestaj = namestaj;
                    }
                }
            } while (izbrisanNamestaj == null);
            izbrisanNamestaj.Obrisan = true;
            Projekat.Instanca.Namestaj = ucitanNamestaj;
            NamestajMeni();
        }

        private static void PretragaNamestaja()
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            int izbor = 0;
            do
            {
                Console.WriteLine("Pretraga namestaja po:");
                Console.WriteLine("1. Nazivu");
                Console.WriteLine("2. Sifri");
                Console.WriteLine("3. Tipu namestaja");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 5);
            switch (izbor)
            {
                case 1:
                    Console.WriteLine("Unesite naziv za pretragu: ");
                    string naziv = Console.ReadLine();
                    foreach (Namestaj namestaj in ucitanNamestaj)
                    {
                        if (namestaj.Naziv.Contains(naziv) && namestaj.Obrisan != true)
                        {
                            Console.WriteLine($"Naziv: {namestaj.Naziv}, Sifra: {namestaj.Sifra}, Cena: {namestaj.Cena}, Kolicina u magacinu: {namestaj.KolicinaUMagacinu} Id tipa namestaja: {namestaj.TipNamestajaId}");
                        }
                    }
                    PretragaNamestaja();
                    break;
                case 2:
                    Console.WriteLine("Unesite sifru za pretragu: ");
                    string sifra = Console.ReadLine();
                    foreach (Namestaj namestaj in ucitanNamestaj)
                    {
                        if (namestaj.Sifra.Contains(sifra) && namestaj.Obrisan != true)
                        {
                            Console.WriteLine($"Naziv: {namestaj.Naziv}, Sifra: {namestaj.Sifra}, Cena: {namestaj.Cena}, Kolicina u magacinu: {namestaj.KolicinaUMagacinu} Id tipa namestaja: {namestaj.TipNamestajaId}");
                        }
                    }
                    PretragaNamestaja();
                    break;
                case 3:
                    Console.WriteLine("Unesite id tipa namestaja za pretragu: ");
                    int id = int.Parse(Console.ReadLine());
                    foreach (Namestaj namestaj in ucitanNamestaj)
                    {
                        if (namestaj.TipNamestajaId == id && namestaj.Obrisan != true)
                        {
                            Console.WriteLine($"Naziv: {namestaj.Naziv}, Sifra: {namestaj.Sifra}, Cena: {namestaj.Cena}, Kolicina u magacinu: {namestaj.KolicinaUMagacinu} Id tipa namestaja: {namestaj.TipNamestajaId}");
                        }
                    }
                    PretragaNamestaja();
                    break;

                default:
                    break;
            }
        }

        public static void SortiranjeNamestaja()
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            int izbor = 0;
            do
            {
                Console.WriteLine("Sortiranje ispisa po:");
                Console.WriteLine("1. Nazivu");
                Console.WriteLine("2. Sifri");
                Console.WriteLine("3. Ceni");
                Console.WriteLine("4. Kolicini u magacinu");
                Console.WriteLine("5. Tipu namestaja");
                Console.WriteLine("0. Izlaz");
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 5);
            switch (izbor)
            {
                case 1:
                    ucitanNamestaj = ucitanNamestaj.OrderBy(x => x.Naziv).ToList();
                    foreach (var namestaj in ucitanNamestaj)
                    {
                        if(namestaj.Obrisan != true)
                        {
                            Console.WriteLine($"Naziv: {namestaj.Naziv}, Sifra: {namestaj.Sifra}, Cena: {namestaj.Cena}, Kolicina u magacinu: {namestaj.KolicinaUMagacinu} Id tipa namestaja: {namestaj.TipNamestajaId}");
                        }
                        
                    }
                    SortiranjeNamestaja();
                    break;

                case 2:
                    ucitanNamestaj = ucitanNamestaj.OrderBy(x => x.Sifra).ToList();
                    foreach (var namestaj in ucitanNamestaj)
                    {
                        if (namestaj.Obrisan != true)
                        {
                            Console.WriteLine($"Naziv: {namestaj.Naziv}, Sifra: {namestaj.Sifra}, Cena: {namestaj.Cena}, Kolicina u magacinu: {namestaj.KolicinaUMagacinu} Id tipa namestaja: {namestaj.TipNamestajaId}");
                        }
                    }
                    SortiranjeNamestaja();
                    break;

                case 3:
                    ucitanNamestaj = ucitanNamestaj.OrderBy(x => x.Cena).ToList();
                    foreach (var namestaj in ucitanNamestaj)
                    {
                        if (namestaj.Obrisan != true)
                        {
                            Console.WriteLine($"Naziv: {namestaj.Naziv}, Sifra: {namestaj.Sifra}, Cena: {namestaj.Cena}, Kolicina u magacinu: {namestaj.KolicinaUMagacinu} Id tipa namestaja: {namestaj.TipNamestajaId}");
                        }
                    }
                    SortiranjeNamestaja();
                    break;

                case 4:
                    ucitanNamestaj = ucitanNamestaj.OrderBy(x => x.KolicinaUMagacinu).ToList();
                    foreach (var namestaj in ucitanNamestaj)
                    {
                        if (namestaj.Obrisan != true)
                        {
                            Console.WriteLine($"Naziv: {namestaj.Naziv}, Sifra: {namestaj.Sifra}, Cena: {namestaj.Cena}, Kolicina u magacinu: {namestaj.KolicinaUMagacinu} Id tipa namestaja: {namestaj.TipNamestajaId}");
                        }
                    }
                    SortiranjeNamestaja();
                    break;

                case 5:
                    ucitanNamestaj = ucitanNamestaj.OrderBy(x => x.TipNamestajaId).ToList();
                    foreach (var namestaj in ucitanNamestaj)
                    {
                        if (namestaj.Obrisan != true)
                        {
                            Console.WriteLine($"Naziv: {namestaj.Naziv}, Sifra: {namestaj.Sifra}, Cena: {namestaj.Cena}, Kolicina u magacinu: {namestaj.KolicinaUMagacinu} Id tipa namestaja: {namestaj.TipNamestajaId}");
                        }
                    }
                    SortiranjeNamestaja();
                    break;

                default: break;
            }
        }
    }
}
