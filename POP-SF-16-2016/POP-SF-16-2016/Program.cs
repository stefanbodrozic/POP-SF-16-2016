using POP_SF_16_2016.Model;
using POP_SF_16_2016.Tests;
using POP_SF_16_2016.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016
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
            var UcitaniKorisnici = Projekat.Instanca.Korisnik;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Korisnicko ime: ");
                string KorisnickoIme = Console.ReadLine();
                Console.WriteLine("Lozinka: ");
                string Lozinka = Console.ReadLine();
                foreach(Korisnik Korisnik in UcitaniKorisnici)
                {
                    if(Korisnik.KorisnickoIme == KorisnickoIme && Korisnik.Lozinka == Lozinka)
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
                        NamestajMeni();
                        break;
                    case 2:
                        TipNamestajaMeni();
                        break;
                    case 3:
                        AkcijeMeni();
                        break;
                    case 4:
                        DodatneUslugeMeni();
                        break;
                    case 5:
                        KorisniciMeni();
                        break;
                    case 6:
                        ProdajaNamestajaMeni();
                        break;
                    case 7:
                        SalonMeni();
                        break;
                    default:
                        break;
                }

            } while (izbor != 0);
        }
//      NAMESTAJ
        private static void NamestajMeni()
        {
            int izbor = 0;
            do
            {
                do
                {
                    Console.WriteLine("===== RAD SA NAMESTAJEM =====");
                    IspisiCRUDMeni();

                    Console.Write("Unos: ");
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

        private static void PrikaziNamestaj()
        {
            Console.WriteLine("===== LISTING NAMESTAJA =====");
            var UcitanNamestaj = Projekat.Instanca.Namestaj;
            
            for(int i = 0; i < UcitanNamestaj.Count; i++)
            {
                if(UcitanNamestaj[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Naziv: {UcitanNamestaj[i].Naziv}, Cena: {UcitanNamestaj[i].Cena}, Sifra: {UcitanNamestaj[i].Sifra}, Id namestaja: {UcitanNamestaj[i].Id}, Id tipa namestaja: {UcitanNamestaj[i].TipNamestajaId}, Obrisan: {UcitanNamestaj[i].Obrisan}");
                }
            }
            NamestajMeni();
        }
        
        private static void DodajNamestaj()
        {
            var UcitanNamestaj = Projekat.Instanca.Namestaj;

            Console.WriteLine("===== DODAVANJE NOVOG NAMESTAJA ====");

            Console.WriteLine("Unesite naziv: ");
            string Naziv = Console.ReadLine();
            Console.WriteLine("Unesite sifru: ");
            string Sifra = Console.ReadLine();
            Console.WriteLine("Unesite cenu: ");
            double Cena = double.Parse(Console.ReadLine());
            Console.WriteLine("Unesite kolicinu u magacinu: ");
            int KolicinaUMagacinu = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite ID tipa namestaja: ");
            int IdTipaNamestaja = int.Parse(Console.ReadLine());

            var NoviNamestaj = new Namestaj()
            {
                Id = UcitanNamestaj.Count + 1,
                Naziv = Naziv,
                Cena = Cena,
                Sifra = Sifra,
                TipNamestajaId = PronadjiTipNamestaja(IdTipaNamestaja).Id
            };
            UcitanNamestaj.Add(NoviNamestaj);
            Projekat.Instanca.Namestaj = UcitanNamestaj;
            NamestajMeni();
        }

        
        private static void IzmeniNamestaj()
        {
            Console.WriteLine("==== IZMENA NAMESTAJA ====");
            Console.WriteLine("ID namestaja za izmenu");

            int IdNamestaja = int.Parse(Console.ReadLine());
            var UcitanNamestaj = Projekat.Instanca.Namestaj;
            Namestaj NamestajZaIzmenu = new Namestaj();

            foreach(Namestaj Namestaj in UcitanNamestaj)
            {
                if(Namestaj.Id == IdNamestaja)
                {
                    NamestajZaIzmenu = Namestaj;
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

                    string NoviNaziv = "";
                    do
                    {
                        Console.WriteLine("Novi naziv: ");
                        NoviNaziv = Console.ReadLine();
                    } while (NoviNaziv == "");
                    NamestajZaIzmenu.Naziv = NoviNaziv;
                    break;

                case 2:

                    string NovaSifra = "";
                    do
                    {
                        Console.WriteLine("Nova sifra: ");
                        NovaSifra = Console.ReadLine();
                    } while (NovaSifra == "");
                    NamestajZaIzmenu.Sifra = NovaSifra;
                    break;
                case 3:

                    double NovaCena = 0;
                    do
                    {
                        Console.WriteLine("Nova cena: ");
                        NovaCena = double.Parse(Console.ReadLine());
                    } while (NovaCena == 0);
                    NamestajZaIzmenu.Cena = NovaCena;
                    break;
                case 4:

                    int NovaKolicina = 0;
                    do
                    {
                        Console.WriteLine("Nova kolicina: ");
                        NovaKolicina = int.Parse(Console.ReadLine());
                    } while (NovaKolicina < 0);
                    NamestajZaIzmenu.KolicinaUMagacinu = NovaKolicina;
                    break;
                case 5:

                    var TipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
                    TipNamestaja NoviTipNamestaja = null;
                    int UnetNoviTipNamestajaId = 0;
                    do
                    {
                        Console.WriteLine("Novi tip namestaja: ");
                        UnetNoviTipNamestajaId = int.Parse(Console.ReadLine());
                        foreach (TipNamestaja TipNamestaja in TipoviNamestaja)
                        {
                            if (TipNamestaja.Id == UnetNoviTipNamestajaId)
                            {
                                NoviTipNamestaja = TipNamestaja;
                            }
                        }
                    } while (NoviTipNamestaja == null);
                    NamestajZaIzmenu.TipNamestajaId = NoviTipNamestaja.Id;
                    break;
                default:
                    break;
            }
            Projekat.Instanca.Namestaj = UcitanNamestaj;
            NamestajMeni();
        }


        private static void IzbrisiNamestaj()
        {
            var UcitanNamestaj = Projekat.Instanca.Namestaj;
            Namestaj IzbrisanNamestaj = null;
            do
            {
                Console.WriteLine("Id namestaja za brisanje: ");
                int IdNamestajaZaBrisanje = int.Parse(Console.ReadLine());
                foreach (Namestaj Namestaj in UcitanNamestaj)
                {
                    if (Namestaj.Obrisan == false & Namestaj.Id == IdNamestajaZaBrisanje)
                    {
                        IzbrisanNamestaj = Namestaj;
                    }
                }
            } while (IzbrisanNamestaj == null);
            IzbrisanNamestaj.Obrisan = true;
            Projekat.Instanca.Namestaj = UcitanNamestaj;
            NamestajMeni();
        }
        


//      TIP NAMESTAJA
        private static void TipNamestajaMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA TIPOM NAMESTAJA =====");
                IspisiCRUDMeni();
                Console.Write("Unos: ");
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

        private static void PrikazSvihTipovaNamestaja()
        {
            Console.WriteLine("==== LISTING TIPOVA NAMESTAJA ====");
            var UcitaniTipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
            for (int i = 0; i < UcitaniTipoviNamestaja.Count; i++)
            {
                if (UcitaniTipoviNamestaja[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Id: {UcitaniTipoviNamestaja[i].Id}, Naziv: {UcitaniTipoviNamestaja[i].Naziv}");
                }
            }
            TipNamestajaMeni();
        }

        private static void DodajNoviTipNamestaja()
        {
            
            Console.WriteLine("===== DODAVANJE NOVOG TIPA NAMESTAJA =====");
            var UcitaniTipoviNamestaja = Projekat.Instanca.TipoviNamestaja;

            Console.WriteLine("Id tipa namestaja: ");
            int Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Naziv tipa namestaja: ");
            string NazivTipaNamestaja = Console.ReadLine();

            var NoviTipNamestaja = new TipNamestaja()
            {
                Id = UcitaniTipoviNamestaja.Count + 1,
                Naziv = NazivTipaNamestaja
            };
            UcitaniTipoviNamestaja.Add(NoviTipNamestaja);
            Projekat.Instanca.TipoviNamestaja = UcitaniTipoviNamestaja;
            TipNamestajaMeni();
        }

        private static void IzmeniTipNamestaja()
        {
            Console.WriteLine("===== IZMENA TIPA NAMESTAJA =====");
            var UcitaniTipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
            TipNamestaja TipNamestajaZaIzmenu = new TipNamestaja();
            Console.WriteLine("ID tipa namestaja za izmenu: ");
            int IdTipaNamestaja = int.Parse(Console.ReadLine());
            foreach(TipNamestaja TipNamestaja in UcitaniTipoviNamestaja)
            {
                if (TipNamestaja.Id == IdTipaNamestaja)
                {
                    TipNamestajaZaIzmenu = TipNamestaja;
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
                    string IzmenjenNazivTipaNamestaja = "";
                    do
                    {
                        Console.WriteLine("Novi naziv tipa namestaja: ");
                        IzmenjenNazivTipaNamestaja = Console.ReadLine();
                    } while (IzmenjenNazivTipaNamestaja == "");
                    TipNamestajaZaIzmenu.Naziv = IzmenjenNazivTipaNamestaja;
                    break;
                default:
                    break;
            }
            Projekat.Instanca.TipoviNamestaja = UcitaniTipoviNamestaja;
            TipNamestajaMeni();

        }

        private static void IzbrisiTipNamestaja()
        {
            var UcitaniTipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
            TipNamestaja IzbrisanTipNamestaja = null;
            do
            {
                Console.WriteLine("Id tipa namestaja za brisanje: ");
                int IdTipaNamestajaZaBrisanje = int.Parse(Console.ReadLine());
                foreach(TipNamestaja TipNamestaja in UcitaniTipoviNamestaja)
                {
                    if(TipNamestaja.Obrisan != true && TipNamestaja.Id == IdTipaNamestajaZaBrisanje)
                    {
                        IzbrisanTipNamestaja = TipNamestaja;
                    }
                }
            } while (IzbrisanTipNamestaja == null);
            IzbrisanTipNamestaja.Obrisan = true;
            Projekat.Instanca.TipoviNamestaja = UcitaniTipoviNamestaja;
            TipNamestajaMeni();
        }


//      AKCIJE
        private static void AkcijeMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA AKCIJAMA =====");
                IspisiCRUDMeni();
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
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
                default:
                    break;
            }
        }

        private static void PrikazSvihAkcija()
        {
            Console.WriteLine("===== LISTING AKCIJA =====");
            var UcitaneAkcije = Projekat.Instanca.Akcija;
            for (int i = 0; i < UcitaneAkcije.Count; i++)
            {
                if (UcitaneAkcije[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Id akcije: {UcitaneAkcije[i].Id}, Popust: {UcitaneAkcije[i].Popust}, Datum pocetka: {UcitaneAkcije[i].DatumPocetka}, Datum zavrsetka: {UcitaneAkcije[i].DatumZavrsetka}, Id namestaja na akciji: {UcitaneAkcije[i].IdNamestaja}");
                }
            }
            AkcijeMeni();
        }

        private static void DodajNovuAkciju()
        {
            Console.WriteLine("===== DODAVANJE NOVE AKCIJE =====");
            var UcitaneAkcije = Projekat.Instanca.Akcija;

            Console.WriteLine("Datum pocetka (dan/mesec/godina): ");
            var DatumPocetka = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Datum zavrsetka (dan/mesec/godina): ");
            var DatumZavrsetka = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Id namestaja na akciji: ");
            int IdNamestajaNaAkciji = int.Parse(Console.ReadLine());
            Console.WriteLine("Popust: ");
            decimal Popust = decimal.Parse(Console.ReadLine());

            var NovaAkcija = new Akcija()
            {
                Id = UcitaneAkcije.Count + 1,
                DatumPocetka = DatumPocetka,
                DatumZavrsetka = DatumZavrsetka,
                IdNamestaja = IdNamestajaNaAkciji,
                Popust = Popust
            };
            UcitaneAkcije.Add(NovaAkcija);
            Projekat.Instanca.Akcija = UcitaneAkcije;
            AkcijeMeni();
        }

        private static void IzmeniAkciju()
        {
            Console.WriteLine("===== IZMENA AKCIJE =====");
            var UcitaneAkcije = Projekat.Instanca.Akcija;
            Akcija AkcijaZaIzmenu = new Akcija();
            Console.WriteLine("Id akcije za izmenu: ");
            int IdAkcijeZaIzmenu = int.Parse(Console.ReadLine());
            foreach(Akcija Akcija in UcitaneAkcije)
            {
                if(Akcija.Id == IdAkcijeZaIzmenu)
                {
                    AkcijaZaIzmenu = Akcija;
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
                    string IzmenjenDatumPocetka = "";
                    do
                    {
                        Console.WriteLine("Novi datum pocetka akcije (dan/mesec/godina): ");
                        IzmenjenDatumPocetka = Console.ReadLine();
                    } while (IzmenjenDatumPocetka == "");
                    AkcijaZaIzmenu.DatumPocetka = DateTime.Parse(IzmenjenDatumPocetka);
                    break;

                case 2:
                    string IzmenjenDatumZavrsetka = "";
                    do
                    {
                        Console.WriteLine("Novi datum zavrsetka akcije (dan/mesec/godina): ");
                        IzmenjenDatumZavrsetka = Console.ReadLine();
                    } while (IzmenjenDatumZavrsetka == "");
                    AkcijaZaIzmenu.DatumZavrsetka = DateTime.Parse(IzmenjenDatumZavrsetka);
                    break;
                case 3:
                    int IzmenjenIdNamestajaNaAkciji = 0;
                    do
                    {
                        Console.WriteLine("Novi id namestaja: ");
                        IzmenjenIdNamestajaNaAkciji = int.Parse(Console.ReadLine());
                    } while (IzmenjenIdNamestajaNaAkciji < 0);
                    AkcijaZaIzmenu.IdNamestaja = IzmenjenIdNamestajaNaAkciji;
                    break;
                case 4:
                    decimal IzmenjenPopust = 0;
                    do
                    {
                        Console.WriteLine("Novi popust: ");
                        IzmenjenPopust = decimal.Parse(Console.ReadLine());
                    } while (IzmenjenPopust < 0);
                    AkcijaZaIzmenu.Popust = IzmenjenPopust;
                    break;
                default:
                    break;
            }
            Projekat.Instanca.Akcija = UcitaneAkcije;
            AkcijeMeni();
        }

        private static void IzbrisiAkciju()
        {
            var UcitaneAkcije = Projekat.Instanca.Akcija;
            Akcija IzbrisanaAkcija = new Akcija();
            do
            {
                Console.WriteLine("Id akcije za brisanje: ");
                int IdAkcijeZaBrisanje = int.Parse(Console.ReadLine());
                foreach (Akcija Akcija in UcitaneAkcije)
                {
                    if (Akcija.Obrisan != true && Akcija.Id == IdAkcijeZaBrisanje)
                    {
                        IzbrisanaAkcija = Akcija;
                    }
                }
            } while (IzbrisanaAkcija == null);
            IzbrisanaAkcija.Obrisan = true;
            Projekat.Instanca.Akcija = UcitaneAkcije;
            AkcijeMeni();
        }


//      DODATNE USLUGE
        private static void DodatneUslugeMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA DODATNIM USLUGAMA =====");
                IspisiCRUDMeni();
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
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
                default:
                    break;
            }
        }

        private static void PrikazSvihDodatnihUsluga()
        {
            Console.WriteLine("===== LISTING DODATNIH USLUGA =====");
            var UcitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;
            for (int i = 0; i < UcitaneDodatneUsluge.Count; i++)
            {
                if(UcitaneDodatneUsluge[i].Obrisan != true)
                {
                    Console.WriteLine($"Naziv usluge: {UcitaneDodatneUsluge[i].Naziv}, Cena: {UcitaneDodatneUsluge[i].Iznos}");
                }
            }
            DodatneUslugeMeni();
        }

        private static void DodajNovuDodatnuUslugu()
        {
            Console.WriteLine("===== DODAVANJE NOVE DODATNE USLUGE =====");
            var UcitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;

            Console.WriteLine("Naziv usluge: ");
            string Naziv = Console.ReadLine();
            Console.WriteLine("Iznos: ");
            double Iznos = double.Parse(Console.ReadLine());

            var NovaDodatnaUsluga = new DodatneUsluge()
            {
                Id = UcitaneDodatneUsluge.Count + 1,
                Naziv = Naziv,
                Iznos = Iznos
            };

            UcitaneDodatneUsluge.Add(NovaDodatnaUsluga);
            Projekat.Instanca.DodatneUsluge = UcitaneDodatneUsluge;
            DodatneUslugeMeni();
        }

        private static void IzmeniDodatnuUslugu()
        {
            Console.WriteLine("===== IZMENA DODATNE USLUGE =====");
            var UcitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;
            DodatneUsluge DodatnaUslugaZaIzmenu = new DodatneUsluge();
            Console.WriteLine("Id dodatne usluge za izmenu: ");
            int IdDodatneUsluge = int.Parse(Console.ReadLine());
            foreach(DodatneUsluge DodatneUsluge in UcitaneDodatneUsluge)
            {
                if(DodatneUsluge.Id == IdDodatneUsluge)
                {
                    DodatnaUslugaZaIzmenu = DodatneUsluge;
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
                    String IzmenjenNaziv = "";
                    do
                    {
                        Console.WriteLine("Novi naziv usluge: ");
                        IzmenjenNaziv = Console.ReadLine();
                    } while (IzmenjenNaziv == "");
                    DodatnaUslugaZaIzmenu.Naziv = IzmenjenNaziv;
                    break;
                case 2:
                    double IzmenjenIznos = 0;
                    do
                    {
                        Console.WriteLine("Novi iznos: ");
                        IzmenjenIznos = double.Parse(Console.ReadLine());
                    } while (IzmenjenIznos < 0);
                    DodatnaUslugaZaIzmenu.Iznos = IzmenjenIznos;
                    break;
                default:
                    break;
            }
            Projekat.Instanca.DodatneUsluge = UcitaneDodatneUsluge;
            DodatneUslugeMeni();
        }

        private static void IzbrisiDodatnuUsluge()
        {
            var UcitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;
            DodatneUsluge IzbrisanaDodatnaUsluga = null;
            do
            {
                Console.WriteLine("Id dodatne usluge za brisanje: ");
                int IdDodatneUslugeZaBrisanje = int.Parse(Console.ReadLine());
                foreach (DodatneUsluge DodatneUsluge in UcitaneDodatneUsluge)
                {
                    if (DodatneUsluge.Obrisan != true && DodatneUsluge.Id == IdDodatneUslugeZaBrisanje)
                    {
                        IzbrisanaDodatnaUsluga = DodatneUsluge;
                    }
                }
            } while (IzbrisanaDodatnaUsluga == null);
            IzbrisanaDodatnaUsluga.Obrisan = true;
            Projekat.Instanca.DodatneUsluge = UcitaneDodatneUsluge;
            DodatneUslugeMeni();
        }


//      KORISNICI
        private static void KorisniciMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA KORISNICIMA =====");
                IspisiCRUDMeni();
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
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
                default:
                    break;
            }
        }

        private static void PrikazSvihKorisnika()
        {
            Console.WriteLine("===== LISTING KORISNIKA =====");
            var UcitaniKorisnici = Projekat.Instanca.Korisnik;
            for (int i = 0; i < UcitaniKorisnici.Count; i++)
            {
                if(UcitaniKorisnici[i].Obrisan != true)
                {
                    Console.WriteLine($"Ime: {UcitaniKorisnici[i].Ime}, Prezime: {UcitaniKorisnici[i].Prezime}, Korisnicko ime: {UcitaniKorisnici[i].KorisnickoIme}, Tip korisnika: {UcitaniKorisnici[i].TipKorisnika}");
                }
            }
            KorisniciMeni();
        }

        private static void DodajNovogKorisnika()
        {
            Console.WriteLine("===== DODAVANJE NOVOG KORISNIKA =====");
            var UcitaniKorisnici = Projekat.Instanca.Korisnik;

            Console.WriteLine("Ime: ");
            string Ime = Console.ReadLine();
            Console.WriteLine("Prezime: ");
            string Prezime = Console.ReadLine();
            Console.WriteLine("Korisnicko ime: ");
            string KorisnickoIme = Console.ReadLine();
            Console.WriteLine("Lozinka: ");
            string Lozinka = Console.ReadLine();
            Console.WriteLine("Tip korisnika: 1) Administrator 2)Prodavac");
            TipKorisnika TipKorisnika;
            int izbor = int.Parse(Console.ReadLine());
           
            switch (izbor)
            {
                case 1:
                    TipKorisnika = TipKorisnika.Administrator;
                    break;
                case 2:
                    TipKorisnika = TipKorisnika.Prodavac;
                    break;
                default:
                    TipKorisnika = TipKorisnika.Prodavac;
                    break;
            }
            

            var NoviKorisnik = new Korisnik()
            {
                Id = UcitaniKorisnici.Count + 1,
                Ime = Ime,
                Prezime = Prezime,
                KorisnickoIme = KorisnickoIme,
                Lozinka = Lozinka,
                TipKorisnika = TipKorisnika
            };

            UcitaniKorisnici.Add(NoviKorisnik);
            Projekat.Instanca.Korisnik = UcitaniKorisnici;
            KorisniciMeni();
        }

        private static void IzmeniKorisnika()
        {
            Console.WriteLine("===== IZMENA KORISNIKA =====");
            var UcitaniKorisnici = Projekat.Instanca.Korisnik;
            Korisnik KorisnikZaIzmenu = new Korisnik();
            Console.WriteLine("Id korisnika za izmenu: ");
            int IdKorisnikaZaIzmenu = int.Parse(Console.ReadLine());
            foreach (Korisnik Korisnik in UcitaniKorisnici)
            {
                if (Korisnik.Id == IdKorisnikaZaIzmenu)
                {
                    KorisnikZaIzmenu = Korisnik;
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
                    String IzmenjenoIme = "";
                    do
                    {
                        Console.WriteLine("Novo ime korisnika: ");
                        IzmenjenoIme = Console.ReadLine();
                    } while (IzmenjenoIme == "");
                    KorisnikZaIzmenu.Ime = IzmenjenoIme;
                    break;
                case 2:
                    String IzmenjenoPrezime = "";
                    do
                    {
                        Console.WriteLine("Novo prezime korisnika: ");
                        IzmenjenoPrezime = Console.ReadLine();
                    } while (IzmenjenoPrezime == "");
                    KorisnikZaIzmenu.Prezime = IzmenjenoPrezime;
                    break;
                case 3:
                    string IzmenjenoKorisnickoIme = "";
                    do
                    {
                        Console.WriteLine("Novo korisnicko ime: ");
                        IzmenjenoKorisnickoIme = Console.ReadLine();
                    } while (IzmenjenoKorisnickoIme == "");
                    KorisnikZaIzmenu.KorisnickoIme = IzmenjenoKorisnickoIme;
                    break;
                case 4:
                    string IzmenjenaLozinka = "";
                    do
                    {
                        Console.WriteLine("Nova lozinka: ");
                        IzmenjenaLozinka = Console.ReadLine();
                    } while (IzmenjenaLozinka == "");
                    KorisnikZaIzmenu.Lozinka = IzmenjenaLozinka;
                    break;

                case 5:
                    Console.WriteLine("Tip korisnika: 1) Administrator 2)Prodavac");
                    int izborTip = int.Parse(Console.ReadLine());
                    
                    switch (izborTip)
                    {
                        case 1:
                            KorisnikZaIzmenu.TipKorisnika = TipKorisnika.Administrator;
                            break;
                        case 2:
                            KorisnikZaIzmenu.TipKorisnika = TipKorisnika.Prodavac;
                            break;
                        default:
                            KorisnikZaIzmenu.TipKorisnika = TipKorisnika.Prodavac;
                            break;  
                    } 
                    break;
                default:
                    break;
            }

            Projekat.Instanca.Korisnik = UcitaniKorisnici;
            KorisniciMeni();
        }

        private static void IzbrisiKorisnika()
        {
            var UcitaniKorisnici = Projekat.Instanca.Korisnik;
            Korisnik IzbrisanKorisnik = null;
            do
            {
                Console.WriteLine("Id korisnika za brisanje: ");
                int IdKorisnikaZaBrisanje = int.Parse(Console.ReadLine());
                foreach (Korisnik Korisnik in UcitaniKorisnici)
                {
                    if (Korisnik.Obrisan != true && Korisnik.Id == IdKorisnikaZaBrisanje)
                    {
                        IzbrisanKorisnik = Korisnik;
                    }
                }
            } while (IzbrisanKorisnik == null);
            IzbrisanKorisnik.Obrisan = true;
            Projekat.Instanca.Korisnik = UcitaniKorisnici;
            KorisniciMeni();
        }



//      PRODAJA NAMESTAJA
        private static void ProdajaNamestajaMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA PRODAJOM NAMESTAJA =====");
                IspisiCRUDMeni();
                Console.Write("Unos: ");
                izbor = int.Parse(Console.ReadLine());
            } while (izbor < 0 || izbor > 4);
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
                default:
                    break;
            }
        }

        private static void PrikazSvihProdajaNamestaja()
        {
            Console.WriteLine("===== PRIKAZ SVIH PRODAJA NAMESTAJA =====");
            var UcitanaProdajaNamestaja = Projekat.Instanca.ProdajaNamestaja;
            for (int i = 0; i < UcitanaProdajaNamestaja.Count; i++)
            {
                if(UcitanaProdajaNamestaja[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Broj racuna: {UcitanaProdajaNamestaja[i].BrojRacuna}, Kupac: {UcitanaProdajaNamestaja[i].Kupac}, Ukupna cena: {UcitanaProdajaNamestaja[i].UkupnaCena}");
                }
            }
            ProdajaNamestajaMeni();

        }

        private static void DodajNovuProdajuNamestaja()
        {
            var DodatneUslugeUcitane = Projekat.Instanca.DodatneUsluge;

            var ProdajaNamestajaUcitano = Projekat.Instanca.ProdajaNamestaja;

            Console.WriteLine("Datum prodaje: ");
            string DatumProdaje = Console.ReadLine();
            Console.WriteLine("Broj racuna: ");
            string BrojRacuna = Console.ReadLine();
            Console.WriteLine("Kupac: ");
            string Kupac = Console.ReadLine();
            double UkupnaCena = 0;

            var UcitanNamestaj = Projekat.Instanca.Namestaj;
            Console.WriteLine("Prikaz dostupnog namestaja koji moze biti prodat");

            for (int i = 0; i < UcitanNamestaj.Count; i++)
            {
                if (UcitanNamestaj[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Naziv: {UcitanNamestaj[i].Naziv}, Cena: {UcitanNamestaj[i].Cena}, Sifra: {UcitanNamestaj[i].Sifra}, Id namestaja: {UcitanNamestaj[i].Id}, Id tipa namestaja: {UcitanNamestaj[i].TipNamestajaId}, Obrisan: {UcitanNamestaj[i].Obrisan}");
                }
            }
            Console.WriteLine();
            int unos = 0;
            List<Namestaj> NamestajZaProdajuLista = new List<Namestaj>();
            do
            {
                Console.WriteLine("Za prekid uneti 0. Namestaji koji su prodati: ");
                unos = int.Parse(Console.ReadLine());
                Namestaj PronadjenNamestaj = Namestaj.PronadjiNamestajPoId(unos);
                
                if(PronadjenNamestaj != null)
                {
                    UkupnaCena += PronadjenNamestaj.Cena;
                    NamestajZaProdajuLista.Add(PronadjenNamestaj);
                }
                

            } while (unos != 0 && unos > 0);

            var UcitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;
            Console.WriteLine("Prikaz dodatnih usluga koje su u ponudi");

            for (int i = 0; i < UcitaneDodatneUsluge.Count; i++)
            {
                if (UcitaneDodatneUsluge[i].Obrisan != true)
                {
                    Console.WriteLine($"{i + 1}. Naziv usluge: {UcitaneDodatneUsluge[i].Naziv}, Cena: {UcitaneDodatneUsluge[i].Iznos}");
                }
            }

            Console.WriteLine();
            int unosId = 0;
            List<DodatneUsluge> DodatneUslugeLista = new List<DodatneUsluge>();
            do
            {
                Console.WriteLine("Za prekid uneti 0. Dodatne usluge koji su prodate: ");
                unosId = int.Parse(Console.ReadLine());
                DodatneUsluge PronadjenaDodatnaUsluga = DodatneUsluge.PronadjiDodatnuUsluguPoId(unosId);
                if(PronadjenaDodatnaUsluga != null)
                {
                    UkupnaCena += PronadjenaDodatnaUsluga.Iznos;
                    DodatneUslugeLista.Add(PronadjenaDodatnaUsluga);
                }
                
                UkupnaCena += PronadjenaDodatnaUsluga.Iznos;
            } while (unos != 0 && unos > 0);

            var NovaProdajaNamestaja = new ProdajaNamestaja()
            {
                Id = ProdajaNamestajaUcitano.Count + 1,
                BrojRacuna = BrojRacuna,
                DatumProdaje = DateTime.Parse(DatumProdaje),
                DodatneUsluge = DodatneUslugeLista,
                Kupac = Kupac,
                UkupnaCena = UkupnaCena,
                
                
                //kako ubaciti listu namestaja????? ne cuvati ceo objekat namestaja. cuva se id namestaja. prepraviti namestajprodajalista da cuva samo id namestaja
            };

            ProdajaNamestajaUcitano.Add(NovaProdajaNamestaja);
            Projekat.Instanca.ProdajaNamestaja = ProdajaNamestajaUcitano;
            ProdajaNamestajaMeni();
        }

        private static void IzmeniProdajuNamestaja()
        {
            Console.WriteLine("===== IZMENA TIPA NAMESTAJA =====");
            var UcitaneProdajeNamestaja = Projekat.Instanca.ProdajaNamestaja;
            ProdajaNamestaja ProdajaNamestajaZaIzmenu = new ProdajaNamestaja();
            Console.WriteLine("Id prodaje namestaja za izmenu: ");
            int IdProdajeNamestajaZaIzmenu = int.Parse(Console.ReadLine());
            foreach (ProdajaNamestaja ProdajaNamestaja in UcitaneProdajeNamestaja)
            {
                if(ProdajaNamestaja.Id == IdProdajeNamestajaZaIzmenu)
                {
                    ProdajaNamestajaZaIzmenu = ProdajaNamestaja;
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
                    string IzmenjenDatumProdaje = "";
                    do
                    {
                        Console.WriteLine("Novi datum prodaje: ");
                        IzmenjenDatumProdaje = Console.ReadLine();
                    } while (IzmenjenDatumProdaje == "");
                    ProdajaNamestajaZaIzmenu.DatumProdaje = DateTime.Parse(IzmenjenDatumProdaje);
                    break;
                case 2:
                    string IzmenjenBrojRacuna = "";
                    do
                    {
                        Console.WriteLine("Novi broj racuna: ");
                        IzmenjenBrojRacuna = Console.ReadLine();
                    } while (IzmenjenBrojRacuna == "");
                    ProdajaNamestajaZaIzmenu.BrojRacuna = IzmenjenBrojRacuna;
                    break;
                case 3:
                    string IzmenaKupca = "";
                    do
                    {
                        Console.WriteLine("Novi kupac: ");
                        IzmenaKupca = Console.ReadLine();
                    } while (IzmenaKupca == "");
                    ProdajaNamestajaZaIzmenu.Kupac = IzmenaKupca;
                    break;
                default:
                    break;
            }
            Projekat.Instanca.ProdajaNamestaja = UcitaneProdajeNamestaja;
        }
        

        private static void IzbrisiProdajuNamestaja()
        {
            var UcitaneProdajeNamestaja = Projekat.Instanca.ProdajaNamestaja;
            ProdajaNamestaja IzbrisanaProdajaNamestaja = null;
            do
            {
                Console.WriteLine("Id prodaje namestaja za brisanje: ");
                int IdProdajeNamestaja = int.Parse(Console.ReadLine());
                foreach (ProdajaNamestaja ProdajaNamestaja in UcitaneProdajeNamestaja)
                {
                    if (ProdajaNamestaja.Obrisan != true && ProdajaNamestaja.Id == IdProdajeNamestaja)
                    {
                        IzbrisanaProdajaNamestaja = ProdajaNamestaja;
                    }
                }
            } while (IzbrisanaProdajaNamestaja == null);
            IzbrisanaProdajaNamestaja.Obrisan = true;
            Projekat.Instanca.ProdajaNamestaja = UcitaneProdajeNamestaja;
            ProdajaNamestajaMeni();
        }


        
//      SALON 
        private static void SalonMeni()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("===== RAD SA SALONOM =====");
                IspisiCRUDMeni();
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
            var UcitaniSaloni = Projekat.Instanca.Salon;
            for (int i = 0; i < UcitaniSaloni.Count; i++)
            {
                if (UcitaniSaloni[i].Obrisan != true)
                {
                    Console.WriteLine($"Naziv: {UcitaniSaloni[i].Naziv}, Adresa: {UcitaniSaloni[i].Adresa}, Telefon: {UcitaniSaloni[i].Telefon}, Websajt: {UcitaniSaloni[i].Websajt}");
                }
            }
            SalonMeni();
        }

        private static void DodajNoviSalon()
        {
            Console.WriteLine("===== DODAVANJE NOVOG SALONA =====");
            var UcitaniSaloni = Projekat.Instanca.Salon;

            Console.WriteLine("Naziv: ");
            string Naziv = Console.ReadLine();
            Console.WriteLine("Adresa: ");
            string Adresa = Console.ReadLine();
            Console.WriteLine("Telefon: ");
            string Telefon = Console.ReadLine();
            Console.WriteLine("Email :");
            string Email = Console.ReadLine();
            Console.WriteLine("Websajt: ");
            string Websajt = Console.ReadLine();
            Console.WriteLine("PIB: ");
            int PIB = int.Parse(Console.ReadLine());
            Console.WriteLine("Maticni broj: ");
            int MaticniBroj = int.Parse(Console.ReadLine());
            Console.WriteLine("Broj ziro racuna: ");
            string BrojZiroRacuna = Console.ReadLine();

            var NoviSalon = new Salon()
            {
                Id = UcitaniSaloni.Count + 1,
                Naziv = Naziv,
                Adresa = Adresa,
                Telefon = Telefon,
                Email = Email,
                Websajt = Websajt,
                PIB = PIB,
                MaticniBroj = MaticniBroj,
                BrojZiroRacuna = BrojZiroRacuna
            };
            UcitaniSaloni.Add(NoviSalon);
            Projekat.Instanca.Salon = UcitaniSaloni;
            SalonMeni();
        }

        private static void IzmeniSalon()
        {
            Console.WriteLine("===== IZMENA SALONA =====");
            var UcitaniSaloni = Projekat.Instanca.Salon;
            Salon SalonZaIzmenu = new Salon();
            Console.WriteLine("Id salona za izmenu: ");
            int IdSalonaZaIzmenu = int.Parse(Console.ReadLine());
            foreach(Salon Salon in UcitaniSaloni)
            {
                if(Salon.Id == IdSalonaZaIzmenu)
                {
                    SalonZaIzmenu = Salon;
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
                    string IzmenjenNaziv = "";
                    do
                    {
                        Console.WriteLine("Novi naziv salona: ");
                        IzmenjenNaziv = Console.ReadLine();
                    } while (IzmenjenNaziv == "");
                    SalonZaIzmenu.Naziv = IzmenjenNaziv;
                    break;
                case 2:
                    string IzmenjenaAdresa = "";
                    do
                    {
                        Console.WriteLine("Nova adresa salona: ");
                        IzmenjenaAdresa = Console.ReadLine();
                    } while (IzmenjenaAdresa == "");
                    SalonZaIzmenu.Adresa = IzmenjenaAdresa;
                    break;
                case 3:
                    string IzmenjenTelefon = "";
                    do
                    {
                        Console.WriteLine("Novi broj telefona: ");
                        IzmenjenTelefon = Console.ReadLine();
                    } while (IzmenjenTelefon == "");
                    SalonZaIzmenu.Telefon = IzmenjenTelefon;
                    break;
                case 4:
                    string IzmenjenEmail = "";
                    do
                    {
                        Console.WriteLine("Novi email: ");
                        IzmenjenEmail = Console.ReadLine();
                    } while (IzmenjenEmail == "");
                    SalonZaIzmenu.Email = IzmenjenEmail;
                    break;
                case 5:
                    string IzmenjenWebsajt = "";
                    do
                    {
                        Console.WriteLine("Novi websajt");
                        IzmenjenWebsajt = Console.ReadLine();
                    } while (IzmenjenWebsajt == "");
                    SalonZaIzmenu.Websajt = IzmenjenWebsajt;
                    break;
                case 6:
                    int IzmenjenPIB = 0;
                    do
                    {
                        Console.WriteLine("Novi PIB: ");
                        IzmenjenPIB = int.Parse(Console.ReadLine());
                    } while (IzmenjenPIB < 0);
                    SalonZaIzmenu.PIB = IzmenjenPIB;
                    break;
                case 7:
                    int IzmenjenMaticniBroj = 0;
                    do
                    {
                        Console.WriteLine("Novi maticni broj: ");
                        IzmenjenMaticniBroj = int.Parse(Console.ReadLine());
                    } while (IzmenjenMaticniBroj < 0);
                    SalonZaIzmenu.MaticniBroj = IzmenjenMaticniBroj;
                    break;
                case 8:
                    string IzmenjenBrojZiroRacuna = "";
                    do
                    {
                        Console.WriteLine("Novi broj ziro racuna: ");
                        IzmenjenBrojZiroRacuna = Console.ReadLine();
                    } while (IzmenjenBrojZiroRacuna == "");
                    SalonZaIzmenu.BrojZiroRacuna = IzmenjenBrojZiroRacuna;
                    break;
                default:
                    break;         
            }
            Projekat.Instanca.Salon = UcitaniSaloni;
            SalonMeni();
        }

        private static void IzbrisiSalon()
        {
            var UcitaniSaloni = Projekat.Instanca.Salon;
            Salon IzbrisanSalon = null;
            do
            {
                Console.WriteLine("Id salona za brisanje: ");
                int IdSalonaZaBrisanje = int.Parse(Console.ReadLine());
                foreach (Salon Salon in UcitaniSaloni)
                {
                    if (Salon.Obrisan != true && Salon.Id == IdSalonaZaBrisanje)
                    {
                        IzbrisanSalon = Salon;
                    }
                }
            } while (IzbrisanSalon == null);
            IzbrisanSalon.Obrisan = true;
            Projekat.Instanca.Salon = UcitaniSaloni;
            SalonMeni();
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
            var TipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
            TipNamestaja TrazeniTipNamestaja = null;
            
            foreach (TipNamestaja TipNamestaja in TipoviNamestaja)
            {
                if (TipNamestaja.Id == IdTipaNamestaja)
                {
                    TrazeniTipNamestaja = TipNamestaja;
                }
            }
            return TrazeniTipNamestaja;
            
        }


    }
}
