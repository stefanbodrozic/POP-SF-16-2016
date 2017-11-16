using POP_SF_16_2016_GUI.Model;
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

namespace POP_SF_16_2016_GUI.GUI
{
    /// <summary>
    /// Interaction logic for DodajIzmeniNamestajWindow.xaml
    /// </summary>
    public partial class RadSaNamestajemWindow : Window
    {
        
        public RadSaNamestajemWindow()
        {
            InitializeComponent();
            OsveziPrikaz();
        }

        private void OsveziPrikaz()
        {
            lbNamestaj.Items.Clear();
            foreach(var namestaj in Projekat.Instanca.Namestaj)
            {
                if(namestaj.Obrisan != true)
                {
                    //lbNamestaj.Items.Add(string.Format("{0} | {1} | {2}", namestaj.Naziv, namestaj.Sifra, namestaj.KolicinaUMagacinu));
                    lbNamestaj.Items.Add(namestaj); //KAKO NAMESTITI DA PRIKAZUJE VREDNOSTI KAO IZNAD
                }
                
            }
            lbNamestaj.SelectedIndex = 0;
        }

        

        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var prazanNamestaj = new Namestaj()
            {
                Naziv = "",
                Cena = 0,
                KolicinaUMagacinu = 0,
                Sifra = "",
                TipNamestajaId = 0
            };
            var dodavanjeNamestaja = new DodavanjeIzmenaNamestajWindow(prazanNamestaj, DodavanjeIzmenaNamestajWindow.TipOperacije.DODAVANJE);
            dodavanjeNamestaja.ShowDialog();
            OsveziPrikaz();
        }

        private void btnIzmeniNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var izabraniNamestaj = (Namestaj)lbNamestaj.SelectedItem;
            var izmenaNamestaja = new DodavanjeIzmenaNamestajWindow(izabraniNamestaj, DodavanjeIzmenaNamestajWindow.TipOperacije.IZMENA);
            izmenaNamestaja.ShowDialog();
            OsveziPrikaz();
        }

        private void btnIzbrisiNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            Namestaj izbrisanNamestaj = null;
            var izabraniNamestaj = (Namestaj)lbNamestaj.SelectedItem;
            do
            {
                foreach (var namestaj in ucitanNamestaj)
                {
                    if(namestaj.Id == izabraniNamestaj.Id)
                    {
                        izbrisanNamestaj = namestaj;
                    }
                }
            } while (izbrisanNamestaj == null);
            izbrisanNamestaj.Obrisan = true;
            Projekat.Instanca.Namestaj = ucitanNamestaj;
            OsveziPrikaz();
        }

        private void btnPretraziNamestaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbSortiranje_Loaded(object sender, RoutedEventArgs e)
        {
            List<String> nacinSortiranja = new List<string>();
            nacinSortiranja.Add("Naziv");
            nacinSortiranja.Add("Sifra");
            nacinSortiranja.Add("Cena");
            nacinSortiranja.Add("Kolicina");
            nacinSortiranja.Add("Tip namestaja");
            foreach (var nacin in nacinSortiranja)
            {
                cbSortiranje.Items.Add(nacin);
            }

            cbSortiranje.Text = "Sortiraj po: ";

        }

    }
}
