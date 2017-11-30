using POP_SF_16_2016_GUI.Model;
using POP_SF_16_2016_GUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace POP_SF_16_2016_GUI.NoviGUI.Prodaja
{
    /// <summary>
    /// Interaction logic for DodajProdajuNamestaja.xaml
    /// </summary>
    public partial class DodajProdajuNamestaja : Window
    {
        private Model.ProdajaNamestaja prodajaNamestaja;

        public DodajProdajuNamestaja(Model.ProdajaNamestaja prodajaNamestaja)
        {
            InitializeComponent();
            this.prodajaNamestaja = prodajaNamestaja;
            var listaNamestaja = new ObservableCollection<Namestaj>();
            foreach (var namestaj in Projekat.Instanca.Namestaj)
            {
                if(namestaj.Obrisan != true && namestaj.KolicinaUMagacinu > 0)
                {
                    listaNamestaja.Add(namestaj);
                }
            }
            dgNamestaj.ItemsSource = listaNamestaja;
            dgNamestaj.DataContext = prodajaNamestaja;

            
            var listaDodatnihUsluga = new ObservableCollection<DodatneUsluge>();
            foreach (var usluga in Projekat.Instanca.DodatneUsluge)
            {
                if(usluga.Obrisan != true)
                {
                    listaDodatnihUsluga.Add(usluga);
                }
            }
            dgDodatneUsluge.ItemsSource = listaDodatnihUsluga;
            dgDodatneUsluge.DataContext = prodajaNamestaja;
            tbKupac.DataContext = prodajaNamestaja;
        }

        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var ucitaneStavkeNaRacunu = Projekat.Instanca.StavkaRacuna;
            var izarbanaStavka = (Namestaj)dgNamestaj.SelectedItem;
            if (izarbanaStavka != null)
            {
                var novaStavkaNaRacunu = new StavkaRacuna()
                {
                    IdStavkeRacuna = ucitaneStavkeNaRacunu.Count(),
                    IdNamestaja = izarbanaStavka.Id,
                    Kolicina = 1
                };
                ucitaneStavkeNaRacunu.Add(novaStavkaNaRacunu);
                var id = novaStavkaNaRacunu.IdStavkeRacuna;
                prodajaNamestaja.StavkaRacuna.Add(id);
                GenericSerializer.Serialize("stavke_racuna.xml", ucitaneStavkeNaRacunu);
            }
            

        }

        private void btnDodajDodatnuUslugu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
