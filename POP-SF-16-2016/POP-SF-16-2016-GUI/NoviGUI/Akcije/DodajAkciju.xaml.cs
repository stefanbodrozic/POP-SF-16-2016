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

namespace POP_SF_16_2016_GUI.NoviGUI.DodavanjeIzmena
{
    /// <summary>
    /// Interaction logic for DodavanjeIzmenaAkcije.xaml
    /// </summary>
    public partial class DodajAkciju : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        };

        private Akcija akcija;

        public DodajAkciju(Akcija akcija)
        {
            InitializeComponent();
        
            this.akcija = akcija;

            dpPocetakAkcije.DataContext = akcija;
            dpZavrsetakAkcije.DataContext = akcija;
            tbPopust.DataContext = akcija;

            var listaNamestaja = new ObservableCollection<Namestaj>();
            foreach (var namestaj in Projekat.Instanca.Namestaj)
            {
                if (namestaj.Obrisan != true && namestaj.KolicinaUMagacinu > 0)
                {
                    listaNamestaja.Add(namestaj);
                }
            }
            dgNamestaj.ItemsSource = listaNamestaja;
            dgNamestaj.DataContext = akcija;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.CanUserAddRows = false;
            dgNamestaj.IsReadOnly = true;

            akcija.IdNamestajaNaAkciji = new ObservableCollection<int>(); //proveriti ? 
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

            if (double.Parse(tbPopust.Text) > 100 || double.Parse(tbPopust.Text) < 1)
            {
                MessageBox.Show("Greska sa popustom! Minimalan popust je 1%. Maksimalan popust je 100%!", "Greska", MessageBoxButton.OK);
                return;
            }

            var ucitaneAkcije = Projekat.Instanca.Akcija;
            //nova akcija
            akcija.Id = ucitaneAkcije.Count;   
            ucitaneAkcije.Add(akcija);

            GenericSerializer.Serialize("akcija.xml", ucitaneAkcije);
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            var izabranaStavka = (Namestaj)dgNamestaj.SelectedItem;
            if (izabranaStavka != null)
            {
                foreach (var idNamestaja in akcija.IdNamestajaNaAkciji)
                {
                    if(idNamestaja == izabranaStavka.Id)
                    {
                        MessageBox.Show("Izabrani namestaj je vec na akciji!", "Greska", MessageBoxButton.OK);
                        return;
                    }
                }
                akcija.IdNamestajaNaAkciji.Add(izabranaStavka.Id);
                MessageBox.Show("Izabrani namestaj je dodat na akciju!");
                return;
            }

        }

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "AkcijaId" || e.Column.Header.ToString() == "TipNamestajaId" || e.Column.Header.ToString() == "Sifra" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }
    }
}
