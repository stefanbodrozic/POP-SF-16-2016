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
            OsveziPrikaz(Projekat.Instanca.Namestaj);
        }

        private void OsveziPrikaz(List<Namestaj> listaNamestaja)
        {
            lbNamestaj.Items.Clear();
            foreach(var namestaj in listaNamestaja)
            {
                if(namestaj.Obrisan != true)
                {
                    lbNamestaj.Items.Add(namestaj);
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
            OsveziPrikaz(Projekat.Instanca.Namestaj);
        }

        private void btnIzmeniNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var izabraniNamestaj = (Namestaj)lbNamestaj.SelectedItem;
            var izmenaNamestaja = new DodavanjeIzmenaNamestajWindow(izabraniNamestaj, DodavanjeIzmenaNamestajWindow.TipOperacije.IZMENA);

            izmenaNamestaja.ShowDialog();

            OsveziPrikaz(Projekat.Instanca.Namestaj);
        }

        private void btnIzbrisiNamestaj_Click(object sender, RoutedEventArgs e)
        {

            var izabraniNamestaj = (Namestaj)lbNamestaj.SelectedItem;

            if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete namestaj: {izabraniNamestaj.Naziv}", "Brisanje namestaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var ucitanNamestaj = Projekat.Instanca.Namestaj;
                foreach (var namestaj in ucitanNamestaj)
                {
                    if (namestaj.Id == izabraniNamestaj.Id)
                    {
                        namestaj.Obrisan = true;                        
                    }
                }
                Projekat.Instanca.Namestaj = ucitanNamestaj;
                OsveziPrikaz(ucitanNamestaj);
            }   
        }

        private void btnPretraziNamestaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void rbNaziv_Checked(object sender, RoutedEventArgs e)
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            ucitanNamestaj = ucitanNamestaj.OrderBy(x => x.Naziv).ToList();
            OsveziPrikaz(ucitanNamestaj);
        }

        private void rbSifra_Checked(object sender, RoutedEventArgs e)
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            ucitanNamestaj = ucitanNamestaj.OrderBy(x => x.Sifra).ToList();
            OsveziPrikaz(ucitanNamestaj);
        }

        private void rbCena_Checked(object sender, RoutedEventArgs e)
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            ucitanNamestaj = ucitanNamestaj.OrderBy(x => x.Cena).ToList();
            OsveziPrikaz(ucitanNamestaj);
        }

        private void rbKolicina_Checked(object sender, RoutedEventArgs e)
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            ucitanNamestaj = ucitanNamestaj.OrderBy(x => x.KolicinaUMagacinu).ToList();
            OsveziPrikaz(ucitanNamestaj);
        }

        private void rbTipNamestaja_Checked(object sender, RoutedEventArgs e)
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            ucitanNamestaj = ucitanNamestaj.OrderBy(x => x.TipNamestajaId).ToList();
            OsveziPrikaz(ucitanNamestaj);
        }

        private void rbNazivPretraga_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbSifraPretraga_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbTipNamestajaPrertaga_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
