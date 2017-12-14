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
    /// Interaction logic for DodavanjeIzmenaAkcije.xaml
    /// </summary>
    public partial class DodajIzmeniAkcija : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        };

        private Akcija akcija;
        private TipOperacije tipOperacije;

        public DodajIzmeniAkcija(Akcija akcija, TipOperacije tipOperacije)
        {
            InitializeComponent();
        
            this.akcija = akcija;
            this.tipOperacije = tipOperacije;

            dpPocetakAkcije.DataContext = akcija;
            dpZavrsetakAkcije.DataContext = akcija;
            tbPopust.DataContext = akcija;
            //punjenje comboboxa sa namestajem koji nije obrisan
            var namestajZaPonudu = new List<Namestaj>();
            foreach (var namestaj in Projekat.Instanca.Namestaj)
            {
                if (namestaj.Obrisan != true)
                {
                    namestajZaPonudu.Add(namestaj);
                }
            }
            cbNamestaj.ItemsSource = namestajZaPonudu;
            cbNamestaj.DataContext = akcija;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (dpPocetakAkcije.SelectedDate < DateTime.Today || dpPocetakAkcije.SelectedDate > dpZavrsetakAkcije.SelectedDate)
            {
                MessageBox.Show("Greska sa datumom pocetka akcije!", "Greska", MessageBoxButton.OK);
                return;
            }
            try
            {
                double.Parse(tbPopust.Text);
            }
            catch
            {
                MessageBox.Show("Greska prilikom unosa popusta!", "Greska", MessageBoxButton.OK);
                return;
            }

            if (cbNamestaj.SelectedItem == null)
            {
                MessageBox.Show("Namestaj ne moze biti neodredjen!", "Greska", MessageBoxButton.OK);
                return;
            }

            var ucitaneAkcije = Projekat.Instanca.Akcija;
            var namestajAkcija = (Namestaj)cbNamestaj.SelectedItem;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    akcija.Id = ucitaneAkcije.Count;
                    akcija.IdNamestaja = namestajAkcija.Id;
                    ucitaneAkcije.Add(akcija);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var a in ucitaneAkcije)
                    {
                        if(a.Id == akcija.Id)
                        {
                            a.DatumPocetka = akcija.DatumPocetka;
                            a.DatumZavrsetka = akcija.DatumZavrsetka;
                            a.IdNamestaja = namestajAkcija.Id;
                            a.Popust = akcija.Popust;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
            GenericSerializer.Serialize("akcija.xml", ucitaneAkcije);
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
