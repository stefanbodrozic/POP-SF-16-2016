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

            //

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
            List<Korisnik> ucitaniKorisnici = new List<Korisnik>();
            foreach (var korisnik in Projekat.Instanca.Korisnik)
            {
                if (korisnik.Obrisan != true)
                {
                    ucitaniKorisnici.Add(korisnik);
                }
            }
            OsveziPrikaz(ucitaniKorisnici);
        }

        private void btnSalon_Click(object sender, RoutedEventArgs e)
        {
            List<Salon> ucitaniSaloni = new List<Salon>();
            foreach (var salon in Projekat.Instanca.Salon)
            {
                if (salon.Obrisan != true)
                {
                    ucitaniSaloni.Add(salon);
                }
            }
            OsveziPrikaz(ucitaniSaloni);
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
                    var novaAkcija = new Akcija()
                    {
                        DatumPocetka = default(DateTime),
                        DatumZavrsetka = default(DateTime),
                        Popust = 0,
                    };
                    var dodavanjeAkcije = new DodajIzmeniAkcija(novaAkcija, DodajIzmeniAkcija.TipOperacije.DODAVANJE, ucitaneAkcije);
                    dodavanjeAkcije.ShowDialog();
                    OsveziPrikaz(ucitaneAkcije);
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
                    break;
                case 7:
                    break;

                default:
                    break;
            }

        }

        private void btnIzbrisi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    };
}


