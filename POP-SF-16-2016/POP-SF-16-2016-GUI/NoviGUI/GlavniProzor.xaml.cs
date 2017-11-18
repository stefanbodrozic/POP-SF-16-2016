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
        private bool selektovanNamestaj = false;

        List<Namestaj> ucitanNamestaj = new List<Namestaj>();
        List<TipNamestaja> ucitaniTipoviNamestaja = new List<TipNamestaja>();


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
            List<TipNamestaja> ucitaniTipoviNamestaja = new List<TipNamestaja>();
            foreach (var tipNamestaja in Projekat.Instanca.TipoviNamestaja)
            {
                if (tipNamestaja.Obrisan != true)
                {
                    ucitaniTipoviNamestaja.Add(tipNamestaja);
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
            selektovanNamestaj = true;
        }

        private void btnTipNamestaja_Click(object sender, RoutedEventArgs e)
        {

            OsveziPrikaz(ucitaniTipoviNamestaja);
        }

        private void btnDodatneUsluge_Click(object sender, RoutedEventArgs e)
        {
            List<DodatneUsluge> ucitaneDodatneUsluge = new List<DodatneUsluge>();
            foreach (var dodatnaUsluga in Projekat.Instanca.DodatneUsluge)
            {
                if (dodatnaUsluga.Obrisan != true)
                {
                    ucitaneDodatneUsluge.Add(dodatnaUsluga);
                }
            }
            OsveziPrikaz(ucitaneDodatneUsluge);
        }

        private void btnAkcije_Click(object sender, RoutedEventArgs e)
        {
            List<Akcija> ucitaneAkcije = new List<Akcija>();
            foreach (var akcija in Projekat.Instanca.Akcija)
            {
                if (akcija.Obrisan != true)
                {
                    ucitaneAkcije.Add(akcija);
                }
            }
            OsveziPrikaz(ucitaneAkcije);
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
            if(selektovanNamestaj == true)
            {
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
            }
        }

        
        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if(selektovanNamestaj == true)
            {
                var izabraniNamestaj = (Namestaj)lbPrikazStavki.SelectedItem;
                var izmenaNamestaja = new DodajIzmeniNamestaj(izabraniNamestaj, DodajIzmeniNamestaj.TipOperacije.IZMENA, ucitanNamestaj);
                izmenaNamestaja.ShowDialog();
                OsveziPrikaz(ucitanNamestaj);
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


