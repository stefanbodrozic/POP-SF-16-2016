using POP_SF_16_2016_GUI.Model;
using POP_SF_16_2016_GUI.NoviGUI.DodavanjeIzmena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP_SF_16_2016_GUI.NoviGUI
{
    /// <summary>
    /// Interaction logic for GlavniProzor.xaml
    /// </summary>
    public partial class GlavniProzor : Window
    {
        public Namestaj izabraniNamestaj { get; set; }
        //napraviti objekat bind koji menja path u zavisnosti sta je izabrano. trenutno je napravljen staticki bind
        // u switch case koristiti case enum :...   umesto case 1:...
        List<Namestaj> ucitanNamestaj = new List<Namestaj>();
        List<TipNamestaja> ucitaniTipoviNamestaja = new List<TipNamestaja>();
        List<DodatneUsluge> ucitaneDodatneUsluge = new List<DodatneUsluge>();
        List<Akcija> ucitaneAkcije = new List<Akcija>();
        List<Korisnik> ucitaniKorisnici = new List<Korisnik>();
        List<Salon> ucitaniSaloni = new List<Salon>();

        private int selektovanoZaIzmenu = 0;

        public GlavniProzor(Korisnik prijavljenKorisnik)
        {
            InitializeComponent();
            if (prijavljenKorisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                btnNamestaj.Visibility = Visibility.Hidden;
                btnTipNamestaja.Visibility = Visibility.Hidden;
                btnDodatneUsluge.Visibility = Visibility.Hidden;
                btnAkcije.Visibility = Visibility.Hidden;
                btnKorisnici.Visibility = Visibility.Hidden;
                btnSalon.Visibility = Visibility.Hidden;
            }
            tbPrikazKorisnika.Text = ($"{prijavljenKorisnik.Ime} {prijavljenKorisnik.Prezime} \n{prijavljenKorisnik.TipKorisnika}");
            UcitajNamestajZaPrikaz();
            UcitajTipNamestajaZaPrikaz();
            UcitajDodatneUslugeZaPrikaz();
            UcitajAkcijeZaPrikaz();
            UcitajKorisnikeZaPrikaz();
            UcitajSalonZaPrikaz();
        }
        private void UcitajNamestajZaPrikaz()
        {
            //foreach (var namestaj in Projekat.Instanca.Namestaj)
            //{
            //    if (namestaj.Obrisan != true)
            //    {
            //        ucitanNamestaj.Add(namestaj);
            //    }
            //}
            dgPrikazStavki.ItemsSource = Projekat.Instanca.Namestaj;
            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
        }

        private void UcitajTipNamestajaZaPrikaz()
        {
            //foreach (var tipNamestaja in Projekat.Instanca.TipoviNamestaja)
            //{
            //    if (tipNamestaja.Obrisan != true)
            //    {
            //        ucitaniTipoviNamestaja.Add(tipNamestaja);
            //    }
            //}
            dgPrikazStavki.ItemsSource = Projekat.Instanca.TipoviNamestaja;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
        }

        private void UcitajDodatneUslugeZaPrikaz()
        {
            foreach (var dodatneUsluge in Projekat.Instanca.DodatneUsluge)
            {
                if (dodatneUsluge.Obrisan != true)
                {
                    ucitaneDodatneUsluge.Add(dodatneUsluge);
                }
            }
        }

        private void UcitajAkcijeZaPrikaz()
        {
            foreach (var akcija in Projekat.Instanca.Akcija)
            {
                if (akcija.Obrisan != true)
                {
                    ucitaneAkcije.Add(akcija);
                }
            }
        }

        private void UcitajKorisnikeZaPrikaz()
        {
            foreach (var korisnik in Projekat.Instanca.Korisnik)
            {
                if (korisnik.Obrisan != true)
                {
                    ucitaniKorisnici.Add(korisnik);
                }
            }
        }

        private void UcitajSalonZaPrikaz()
        {
            foreach (var salon in Projekat.Instanca.Salon)
            {
                if (salon.Obrisan != true)
                {
                    ucitaniSaloni.Add(salon);
                }
            }
        }

        //private void OsveziPrikaz<T>(List<T> listaZaPrikaz)
        //{
        //    lbPrikazStavki.Items.Clear();
        //    foreach (var stavka in listaZaPrikaz)
        //    {
        //        lbPrikazStavki.Items.Add(stavka);
        //    }
        //    lbPrikazStavki.SelectedIndex = 0;
        //}

        private void btnProdajaNamestaja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {
            //OsveziPrikaz(ucitanNamestaj);
            selektovanoZaIzmenu = 2;
        }

        private void btnTipNamestaja_Click(object sender, RoutedEventArgs e)
        {
            //OsveziPrikaz(ucitaniTipoviNamestaja);
            selektovanoZaIzmenu = 3;
        }

        private void btnDodatneUsluge_Click(object sender, RoutedEventArgs e)
        {
            //OsveziPrikaz(ucitaneDodatneUsluge);
            selektovanoZaIzmenu = 4;
        }

        private void btnAkcije_Click(object sender, RoutedEventArgs e)
        {
            //OsveziPrikaz(ucitaneAkcije);
            selektovanoZaIzmenu = 5;
        }

        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            //OsveziPrikaz(ucitaniKorisnici);
            selektovanoZaIzmenu = 6;
        }

        private void btnSalon_Click(object sender, RoutedEventArgs e)
        {
            //OsveziPrikaz(ucitaniSaloni);
            selektovanoZaIzmenu = 7;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                case 1:
                    break;
                case 2:
                    var prazanNamestaj = new Namestaj()
                    {
                        Naziv = "",
                        Cena = 0,
                        KolicinaUMagacinu = 0,
                        Sifra = "",
                        TipNamestajaId = 0
                    };
                    var dodavanjeNamestaja = new DodajIzmeniNamestaj(prazanNamestaj, DodajIzmeniNamestaj.TipOperacije.DODAVANJE);
                    dodavanjeNamestaja.ShowDialog();
                    ucitanNamestaj.Clear();
                    UcitajNamestajZaPrikaz();
                    //OsveziPrikaz(ucitanNamestaj);
                    break;
                case 3:
                    var prazanTipNamestaja = new TipNamestaja()
                    {
                        Naziv = ""
                    };
                    var dodavanjeTipaNamestaja = new DodajIzmeniTipNamestaja(prazanTipNamestaja, DodajIzmeniTipNamestaja.TipOperacije.DODAVANJE);
                    dodavanjeTipaNamestaja.ShowDialog();
                    ucitaniTipoviNamestaja.Clear();
                    UcitajTipNamestajaZaPrikaz();
                    //OsveziPrikaz(ucitaniTipoviNamestaja);
                    break;
                case 4:
                    var praznaDodatnaUsluga = new DodatneUsluge()
                    {
                        Naziv = "",
                        Iznos = 0
                    };
                    var dodavanjeDodatneUsluge = new DodajIzmeniDodatneUsluge(praznaDodatnaUsluga, DodajIzmeniDodatneUsluge.TipOperacije.DODAVANJE);
                    dodavanjeDodatneUsluge.ShowDialog();
                    ucitaneDodatneUsluge.Clear();
                    UcitajDodatneUslugeZaPrikaz();
                    //OsveziPrikaz(ucitaneDodatneUsluge);
                    break;
                case 5:
                    var praznaAkcija = new Akcija()
                    {
                        DatumPocetka = default(DateTime),
                        DatumZavrsetka = default(DateTime),
                        Popust = 0,
                    };
                    var dodavanjeAkcije = new DodajIzmeniAkcija(praznaAkcija, DodajIzmeniAkcija.TipOperacije.DODAVANJE);
                    dodavanjeAkcije.ShowDialog();
                    ucitaneAkcije.Clear();
                    UcitajAkcijeZaPrikaz();
                    //OsveziPrikaz(ucitaneAkcije);
                    break;
                case 6:
                    var prazanKorisnik = new Korisnik()
                    {
                        Ime = "",
                        Prezime = "",
                        KorisnickoIme = "",
                        Lozinka = "",
                        TipKorisnika = TipKorisnika.Prodavac
                    };
                    var dodavanjeKorisnika = new DodajIzmeniKorisnik(prazanKorisnik, DodajIzmeniKorisnik.TipOperacije.DODAVANJE);
                    dodavanjeKorisnika.ShowDialog();
                    ucitaniKorisnici.Clear();
                    UcitajKorisnikeZaPrikaz();
                    //OsveziPrikaz(ucitaniKorisnici);
                    break;
                case 7:
                    var prazanSalon = new Salon()
                    {
                        Naziv = "",
                        Adresa = "",
                        Telefon = "",
                        Email = "",
                        Websajt = "",
                        PIB = 0,
                        MaticniBroj = 0,
                        BrojZiroRacuna = ""
                    };
                    var dodavanjeSalona = new DodajIzmeniSalon(prazanSalon, DodajIzmeniSalon.TipOperacije.DODAVANJE);
                    dodavanjeSalona.ShowDialog();
                    ucitaniSaloni.Clear();
                    UcitajSalonZaPrikaz();
                    //OsveziPrikaz(ucitaniSaloni);
                    break;
                default:
                    break;

            }
        }


        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                case 1:
                    break;

                case 2:
                    //var izabraniNamestaj = (Namestaj)dgPrikazStavki.SelectedItem;

                    var izmenaNamestaja = new DodajIzmeniNamestaj(izabraniNamestaj, DodajIzmeniNamestaj.TipOperacije.IZMENA);
                    izmenaNamestaja.ShowDialog();
                    ucitanNamestaj.Clear();
                    UcitajNamestajZaPrikaz();
                    //OsveziPrikaz(ucitanNamestaj);
                    break;

                case 3:
                    var izabraniTipNamestaja = (TipNamestaja)dgPrikazStavki.SelectedItem;
                    var izmenaTipaNamestaja = new DodajIzmeniTipNamestaja(izabraniTipNamestaja, DodajIzmeniTipNamestaja.TipOperacije.IZMENA);
                    izmenaTipaNamestaja.ShowDialog();
                    ucitaniTipoviNamestaja.Clear();
                    UcitajTipNamestajaZaPrikaz();
                    //OsveziPrikaz(ucitaniTipoviNamestaja);
                    break;
                case 4:
                    var izabranaDodatnaUsluga = (DodatneUsluge)dgPrikazStavki.SelectedItem;
                    var izmenaDodatneUsluge = new DodajIzmeniDodatneUsluge(izabranaDodatnaUsluga, DodajIzmeniDodatneUsluge.TipOperacije.IZMENA);
                    izmenaDodatneUsluge.ShowDialog();
                    ucitaneDodatneUsluge.Clear();
                    UcitajDodatneUslugeZaPrikaz();
                    //OsveziPrikaz(ucitaneDodatneUsluge);
                    break;
                case 5:
                    var izabranaAkcija = (Akcija)dgPrikazStavki.SelectedItem;
                    var izmenaAkcije = new DodajIzmeniAkcija(izabranaAkcija, DodajIzmeniAkcija.TipOperacije.IZMENA);
                    izmenaAkcije.ShowDialog();
                    ucitaneAkcije.Clear();
                    UcitajAkcijeZaPrikaz();
                    //OsveziPrikaz(ucitaneAkcije);
                    break;
                case 6:
                    var izabraniKorisnik = (Korisnik)dgPrikazStavki.SelectedItem;
                    var izmenaKorisnika = new DodajIzmeniKorisnik(izabraniKorisnik, DodajIzmeniKorisnik.TipOperacije.IZMENA);
                    izmenaKorisnika.ShowDialog();
                    ucitaniKorisnici.Clear();
                    UcitajKorisnikeZaPrikaz();
                    //OsveziPrikaz(ucitaniKorisnici);
                    break;
                case 7:
                    var izabraniSalon = (Salon)dgPrikazStavki.SelectedItem;
                    var izmenaSalona = new DodajIzmeniSalon(izabraniSalon, DodajIzmeniSalon.TipOperacije.IZMENA);
                    izmenaSalona.ShowDialog();
                    ucitaniSaloni.Clear();
                    UcitajSalonZaPrikaz();
                    //OsveziPrikaz(ucitaniSaloni);
                    break;
                default:
                    break;
            }

        }

        private void btnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                case 1:
                    break;
                case 2:
                    var izabraniNamestaj = (Namestaj)dgPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete namestaj: {izabraniNamestaj.Naziv}", "Brisanje namestaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaNamestaja = Projekat.Instanca.Namestaj;
                        foreach (var namestaj in listaNamestaja)
                        {
                            if (namestaj.Obrisan != true && namestaj.Id == izabraniNamestaj.Id)
                            {
                                namestaj.Obrisan = true;
                            }   
                        }
                        Projekat.Instanca.Namestaj = listaNamestaja;
                        ucitanNamestaj.Clear();
                        UcitajNamestajZaPrikaz();
                        //OsveziPrikaz(ucitanNamestaj);
                    }
                    break;
                case 3:
                    var izabraniTipNamestaja = (TipNamestaja)dgPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete tip namestaja: {izabraniTipNamestaja.Naziv}", "Brisanje tipa namestaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaTipovaNamestaja = Projekat.Instanca.TipoviNamestaja;
                        foreach (var tipNamestaja in listaTipovaNamestaja)
                        {
                            if(tipNamestaja.Id == izabraniTipNamestaja.Id)
                            {
                                tipNamestaja.Obrisan = true;
                            }
                        }
                        Projekat.Instanca.TipoviNamestaja = listaTipovaNamestaja;
                        ucitaniTipoviNamestaja.Clear();
                        UcitajTipNamestajaZaPrikaz();
                        //OsveziPrikaz(ucitaniTipoviNamestaja);
                    };
                    break;
                case 4:
                    var izabranaDodatnaUsluga = (DodatneUsluge)dgPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete dodatnu uslugu: {izabranaDodatnaUsluga.Naziv}", "Brisanje dodatne usluge", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaDodatnihUsluga = Projekat.Instanca.DodatneUsluge;
                        foreach (var dodatnaUsluga in listaDodatnihUsluga)
                        {
                            if (dodatnaUsluga.Id == izabranaDodatnaUsluga.Id)
                            {
                                dodatnaUsluga.Obrisan = true;
                            }
                        }
                        Projekat.Instanca.DodatneUsluge = listaDodatnihUsluga;
                        ucitaneDodatneUsluge.Clear();
                        UcitajDodatneUslugeZaPrikaz();
                        //OsveziPrikaz(ucitaneDodatneUsluge);
                    }
                    break;
                case 5:
                    var izabranaAkcija = (Akcija)dgPrikazStavki.SelectedItem;
                    if(MessageBox.Show("Da li ste sigurni da zelite da izbrisete akciju?", "Brisanje akcije", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaAkcija = Projekat.Instanca.Akcija;
                        foreach (var akcija in listaAkcija)
                        {
                            if (akcija.Id == izabranaAkcija.Id)
                            {
                                akcija.Obrisan = true;
                            }
                        }
                        Projekat.Instanca.Akcija = listaAkcija;
                        ucitaneAkcije.Clear();
                        UcitajAkcijeZaPrikaz();
                        //OsveziPrikaz(ucitaneAkcije);
                    };
                    break;
                case 6:
                    var izabraniKorisnik = (Korisnik)dgPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete korisnika:{izabraniKorisnik.Ime + " " + izabraniKorisnik.Prezime}", "Brisanje korisnika", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaKorisnika = Projekat.Instanca.Korisnik;
                        foreach (var korisnik in listaKorisnika)
                        {
                            if (korisnik.Id == izabraniKorisnik.Id)
                            {
                                korisnik.Obrisan = true;
                            }
                        }
                        Projekat.Instanca.Korisnik = listaKorisnika;
                        ucitaniKorisnici.Clear();
                        UcitajKorisnikeZaPrikaz();
                        //OsveziPrikaz(ucitaniKorisnici);
                    };
                    break;
                case 7:
                    var izabraniSalon = (Salon)dgPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete salon:{izabraniSalon.Naziv}", "Brisanje salona", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaSalona = Projekat.Instanca.Salon;
                        foreach (var salon in listaSalona)
                        {
                            if (salon.Id == izabraniSalon.Id)
                            {
                                salon.Obrisan = true;
                            }
                        }
                        Projekat.Instanca.Salon = listaSalona;
                        ucitaniSaloni.Clear();
                        UcitajSalonZaPrikaz();
                        //OsveziPrikaz(ucitaniSaloni);
                    }
                    break;

                default:
                    break;
            }
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            var loginProzor = new LoginProzor();
            Close();
            loginProzor.ShowDialog();
        }
    };
}


