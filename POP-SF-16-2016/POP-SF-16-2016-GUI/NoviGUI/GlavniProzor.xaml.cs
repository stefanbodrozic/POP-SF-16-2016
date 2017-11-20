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
            NapuniSveListe();
        }
        private void NapuniSveListe()
        {
            //namestaj
            foreach (var namestaj in Projekat.Instanca.Namestaj)
            {
                if (namestaj.Obrisan != true)
                {
                    ucitanNamestaj.Add(namestaj);
                }
            }

            //tip namestaja
            foreach (var tipNamestaja in Projekat.Instanca.TipoviNamestaja)
            {
                if (tipNamestaja.Obrisan != true)
                {
                    ucitaniTipoviNamestaja.Add(tipNamestaja);
                }
            }

            //dodatne usluge
            foreach (var dodatneUsluge in Projekat.Instanca.DodatneUsluge)
            {
                if(dodatneUsluge.Obrisan != true)
                {
                    ucitaneDodatneUsluge.Add(dodatneUsluge);
                }
            }

            //akcije
            foreach (var akcija in Projekat.Instanca.Akcija)
            {
                if (akcija.Obrisan != true)
                {
                    ucitaneAkcije.Add(akcija);
                }
            }

            //korisnici
            foreach (var korisnik in Projekat.Instanca.Korisnik)
            {
                if(korisnik.Obrisan != true)
                {
                    ucitaniKorisnici.Add(korisnik);
                }
            }

            //salon
            foreach (var salon in Projekat.Instanca.Salon)
            {
                if(salon.Obrisan != true)
                {
                    ucitaniSaloni.Add(salon);
                }
            }

        }


        private void OsveziPrikaz<T>(List<T> listaZaPrikaz)
        {
            lbPrikazStavki.Items.Clear();
            foreach (var stavka in listaZaPrikaz)
            {
                lbPrikazStavki.Items.Add(stavka);
            }
            lbPrikazStavki.SelectedIndex = 0;
        }

        private void btnProdajaNamestaja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {
            OsveziPrikaz(ucitanNamestaj);
            selektovanoZaIzmenu = 2;
        }

        private void btnTipNamestaja_Click(object sender, RoutedEventArgs e)
        {
            OsveziPrikaz(ucitaniTipoviNamestaja);
            selektovanoZaIzmenu = 3;
        }

        private void btnDodatneUsluge_Click(object sender, RoutedEventArgs e)
        {
            OsveziPrikaz(ucitaneDodatneUsluge);
            selektovanoZaIzmenu = 4;
        }

        private void btnAkcije_Click(object sender, RoutedEventArgs e)
        {
            OsveziPrikaz(ucitaneAkcije);
            selektovanoZaIzmenu = 5;
        }

        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            OsveziPrikaz(ucitaniKorisnici);
            selektovanoZaIzmenu = 6;
        }

        private void btnSalon_Click(object sender, RoutedEventArgs e)
        {
            OsveziPrikaz(ucitaniSaloni);
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
                    var dodavanjeNamestaja = new DodajIzmeniNamestaj(prazanNamestaj, DodajIzmeniNamestaj.TipOperacije.DODAVANJE, ucitanNamestaj);
                    dodavanjeNamestaja.ShowDialog();
                    OsveziPrikaz(ucitanNamestaj);
                    break;
                case 3:
                    var prazanTipNamestaja = new TipNamestaja()
                    {
                        Naziv = ""
                    };
                    var dodavanjeTipaNamestaja = new DodajIzmeniTipNamestaja(prazanTipNamestaja, DodajIzmeniTipNamestaja.TipOperacije.DODAVANJE, ucitaniTipoviNamestaja);
                    dodavanjeTipaNamestaja.ShowDialog();
                    OsveziPrikaz(ucitaniTipoviNamestaja);
                    break;
                case 4:
                    var praznaDodatnaUsluga = new DodatneUsluge()
                    {
                        Naziv = "",
                        Iznos = 0
                    };
                    var dodavanjeDodatneUsluge = new DodajIzmeniDodatneUsluge(praznaDodatnaUsluga, DodajIzmeniDodatneUsluge.TipOperacije.DODAVANJE, ucitaneDodatneUsluge);
                    dodavanjeDodatneUsluge.ShowDialog();
                    OsveziPrikaz(ucitaneDodatneUsluge);
                    break;
                case 5:
                    var praznaAkcija = new Akcija()
                    {
                        DatumPocetka = default(DateTime),
                        DatumZavrsetka = default(DateTime),
                        Popust = 0,
                    };
                    var dodavanjeAkcije = new DodajIzmeniAkcija(praznaAkcija, DodajIzmeniAkcija.TipOperacije.DODAVANJE, ucitaneAkcije);
                    dodavanjeAkcije.ShowDialog();
                    OsveziPrikaz(ucitaneAkcije);
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
                    var dodavanjeKorisnika = new DodajIzmeniKorisnik(prazanKorisnik, DodajIzmeniKorisnik.TipOperacije.DODAVANJE, ucitaniKorisnici);
                    dodavanjeKorisnika.ShowDialog();
                    OsveziPrikaz(ucitaniKorisnici);
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
                    var dodavanjeSalona = new DodajIzmeniSalon(prazanSalon, DodajIzmeniSalon.TipOperacije.DODAVANJE, ucitaniSaloni);
                    dodavanjeSalona.ShowDialog();
                    OsveziPrikaz(ucitaniSaloni);
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
                    var izabraniNamestaj = (Namestaj)lbPrikazStavki.SelectedItem;
                    var izmenaNamestaja = new DodajIzmeniNamestaj(izabraniNamestaj, DodajIzmeniNamestaj.TipOperacije.IZMENA, ucitanNamestaj);
                    izmenaNamestaja.ShowDialog();
                    OsveziPrikaz(ucitanNamestaj);
                    break;

                case 3:
                    var izabraniTipNamestaja = (TipNamestaja)lbPrikazStavki.SelectedItem;
                    var izmenaTipaNamestaja = new DodajIzmeniTipNamestaja(izabraniTipNamestaja, DodajIzmeniTipNamestaja.TipOperacije.IZMENA, ucitaniTipoviNamestaja);
                    izmenaTipaNamestaja.ShowDialog();
                    OsveziPrikaz(ucitaniTipoviNamestaja);
                    break;
                case 4:
                    var izabranaDodatnaUsluga = (DodatneUsluge)lbPrikazStavki.SelectedItem;
                    var izmenaDodatneUsluge = new DodajIzmeniDodatneUsluge(izabranaDodatnaUsluga, DodajIzmeniDodatneUsluge.TipOperacije.IZMENA, ucitaneDodatneUsluge);
                    izmenaDodatneUsluge.ShowDialog();
                    OsveziPrikaz(ucitaneDodatneUsluge);
                    break;
                case 5:
                    var izabranaAkcija = (Akcija)lbPrikazStavki.SelectedItem;
                    var izmenaAkcije = new DodajIzmeniAkcija(izabranaAkcija, DodajIzmeniAkcija.TipOperacije.IZMENA, ucitaneAkcije);
                    izmenaAkcije.ShowDialog();
                    OsveziPrikaz(ucitaneAkcije);
                    break;
                case 6:
                    var izabraniKorisnik = (Korisnik)lbPrikazStavki.SelectedItem;
                    var izmenaKorisnika = new DodajIzmeniKorisnik(izabraniKorisnik, DodajIzmeniKorisnik.TipOperacije.IZMENA, ucitaniKorisnici);
                    izmenaKorisnika.ShowDialog();
                    OsveziPrikaz(ucitaniKorisnici);
                    break;
                case 7:
                    var izabraniSalon = (Salon)lbPrikazStavki.SelectedItem;
                    var izmenaSalona = new DodajIzmeniSalon(izabraniSalon, DodajIzmeniSalon.TipOperacije.IZMENA, ucitaniSaloni);
                    izmenaSalona.ShowDialog();
                    OsveziPrikaz(ucitaniSaloni);
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
                    var izabraniNamestaj = (Namestaj)lbPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete namestaj: {izabraniNamestaj.Naziv}", "Brisanje namestaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (var namestaj in ucitanNamestaj)
                        {
                            if (namestaj.Id == izabraniNamestaj.Id)
                            {
                                namestaj.Obrisan = true;
                            }
                            
                        }
                        Projekat.Instanca.Namestaj = ucitanNamestaj;

                        //napravljena trenutna lista da bi se prilikom brisanja namestaja lista istovremeno osvezila i prikazala namestaje koji nisu obrisani
                        List< Namestaj > trenutnaListaNamestaja = new List<Namestaj>();
                        foreach (var namestaj in ucitanNamestaj)
                        {
                            if(namestaj.Obrisan != true)
                            {
                                trenutnaListaNamestaja.Add(namestaj);
                            }
                        }
                        OsveziPrikaz(trenutnaListaNamestaja);
                        ucitanNamestaj = trenutnaListaNamestaja;
                    }
                    break;
                case 3:
                    var izabraniTipNamestaja = (TipNamestaja)lbPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete tip namestaja: {izabraniTipNamestaja.Naziv}", "Brisanje tipa namestaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (var tipNamestaja in ucitaniTipoviNamestaja)
                        {
                            if(tipNamestaja.Id == izabraniTipNamestaja.Id)
                            {
                                tipNamestaja.Obrisan = true;
                            }
                        }
                    }
                    Projekat.Instanca.TipoviNamestaja = ucitaniTipoviNamestaja;
                    List<TipNamestaja> trenutnaListaTipaNamestaja = new List<TipNamestaja>();
                    foreach (var tipNamestaja in ucitaniTipoviNamestaja)
                    {
                        if(tipNamestaja.Obrisan != true)
                        {
                            trenutnaListaTipaNamestaja.Add(tipNamestaja);
                        }
                    }
                    OsveziPrikaz(trenutnaListaTipaNamestaja);
                    ucitaniTipoviNamestaja = trenutnaListaTipaNamestaja;
                    break;
                case 4:
                    var izabranaDodatnaUsluga = (DodatneUsluge)lbPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete dodatnu uslugu: {izabranaDodatnaUsluga.Naziv}", "Brisanje dodatne usluge", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (var dodatnaUsluga in ucitaneDodatneUsluge)
                        {
                            if (dodatnaUsluga.Id == izabranaDodatnaUsluga.Id)
                            {
                                dodatnaUsluga.Obrisan = true;
                            }
                        }
                    }
                    Projekat.Instanca.DodatneUsluge = ucitaneDodatneUsluge;

                    List<DodatneUsluge> trenutnaListaDodatneUsluge = new List<DodatneUsluge>();
                    foreach (var dodatnaUsluga in ucitaneDodatneUsluge)
                    {
                        if (dodatnaUsluga.Obrisan != true)
                        {
                            trenutnaListaDodatneUsluge.Add(dodatnaUsluga);
                        }
                    }
                    OsveziPrikaz(trenutnaListaDodatneUsluge);
                    ucitaneDodatneUsluge = trenutnaListaDodatneUsluge;
                    break;
                case 5:
                    var izabranaAkcija = (Akcija)lbPrikazStavki.SelectedItem;
                    if(MessageBox.Show("Da li ste sigurni da zelite da izbrisete akciju?", "Brisanje akcije", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (var akcija in ucitaneAkcije)
                        {
                            if (akcija.Id == izabranaAkcija.Id)
                            {
                                akcija.Obrisan = true;
                            }
                        }
                    }
                    Projekat.Instanca.Akcija = ucitaneAkcije;

                    List<Akcija> trenutnaListaAkcije= new List<Akcija>();
                    foreach (var akcija in ucitaneAkcije)
                    {
                        if (akcija.Obrisan != true)
                        {
                            trenutnaListaAkcije.Add(akcija);
                        }
                    }
                    OsveziPrikaz(trenutnaListaAkcije);
                    ucitaneAkcije = trenutnaListaAkcije;
                    break;
                case 6:
                    var izabraniKorisnik = (Korisnik)lbPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete korisnika:{izabraniKorisnik.Ime + " " + izabraniKorisnik.Prezime}", "Brisanje korisnika", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (var korisnik in ucitaniKorisnici)
                        {
                            if (korisnik.Id == izabraniKorisnik.Id)
                            {
                                korisnik.Obrisan = true;
                            }
                        }
                    }
                    Projekat.Instanca.Korisnik = ucitaniKorisnici;

                    List<Korisnik> trenutnaListaKorisnici = new List<Korisnik>();
                    foreach (var korisnik in ucitaniKorisnici)
                    {
                        if (korisnik.Obrisan != true)
                        {
                            trenutnaListaKorisnici.Add(korisnik);
                        }
                    }
                    OsveziPrikaz(trenutnaListaKorisnici);
                    ucitaniKorisnici = trenutnaListaKorisnici;
                    break;
                case 7:
                    var izabraniSalon = (Salon)lbPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete salon:{izabraniSalon.Naziv}", "Brisanje salona", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (var salon in ucitaniSaloni)
                        {
                            if (salon.Id == izabraniSalon.Id)
                            {
                                salon.Obrisan = true;
                            }
                        }
                    }
                    Projekat.Instanca.Salon = ucitaniSaloni;

                    List<Salon> trenutnaListaSalona = new List<Salon>();
                    foreach (var salon in ucitaniSaloni)
                    {
                        if (salon.Obrisan != true)
                        {
                            trenutnaListaSalona.Add(salon);
                        }
                    }
                    OsveziPrikaz(trenutnaListaSalona);
                    ucitaniSaloni = trenutnaListaSalona;
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


