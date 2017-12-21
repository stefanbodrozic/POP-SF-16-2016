using POP_SF_16_2016_GUI.Model;
using POP_SF_16_2016_GUI.Utils;
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

namespace POP_SF_16_2016_GUI.NoviGUI.DodavanjeIzmena
{
    /// <summary>
    /// Interaction logic for DodajIzmeniNamestaj.xaml
    /// </summary>
    public partial class DodajIzmeniNamestaj : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }
        private Namestaj namestaj;
        private TipOperacije tipOperacije;

        public DodajIzmeniNamestaj(Namestaj namestaj, TipOperacije tipOperacije)
        {
            InitializeComponent();

            this.namestaj = namestaj;
            this.tipOperacije = tipOperacije;

            tbNaziv.DataContext = namestaj;
            tbCena.DataContext = namestaj;
            tbKolicina.DataContext = namestaj;
            tbSifra.DataContext = namestaj;

            cbTipNamestaja.ItemsSource = Projekat.Instanca.TipoviNamestaja;
            cbTipNamestaja.DataContext = namestaj;
            cbTipNamestaja.SelectedIndex = 1;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double.Parse(tbCena.Text);
                int.Parse(tbKolicina.Text);
            }
            catch
            {
                MessageBox.Show("Greska prilikom unosa cene ili kolicine!", "Greska", MessageBoxButton.OK);
                return;
            }
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            var izabranTip = (TipNamestaja)cbTipNamestaja.SelectedItem;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    Namestaj.Create(namestaj);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var n in ucitanNamestaj)
                    {
                        if(namestaj.Id == n.Id)
                        {
                            n.TipNamestajaId = namestaj.TipNamestaja.Id;
                            n.Naziv = namestaj.Naziv;
                            n.Cena = namestaj.Cena;
                            n.KolicinaUMagacinu = namestaj.KolicinaUMagacinu;
                            n.Sifra = namestaj.Sifra;
                            break;
                        }
                    }
                    Namestaj.Update(namestaj);
                    break;
                default:
                    break;
            }
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
