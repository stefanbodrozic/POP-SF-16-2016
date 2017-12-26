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

namespace POP_SF_16_2016_GUI.NoviGUI.Akcije
{
    /// <summary>
    /// Interaction logic for DodajProizvodNaAkciju.xaml
    /// </summary>
    public partial class DodajProizvodNaAkciju : Window
    {
        private Akcija akcija;
        ObservableCollection<Namestaj> listaNamestaja = new ObservableCollection<Namestaj>();

        public DodajProizvodNaAkciju(Akcija akcija)
        {
            InitializeComponent();

            this.akcija = akcija;
            
            foreach (var namestaj in Projekat.Instanca.Namestaj)
            {
                if (namestaj.Obrisan != true && namestaj.KolicinaUMagacinu > 0)
                {
                    listaNamestaja.Add(namestaj); //lista namestaja

                    foreach (var namestajNaAkciji in Projekat.Instanca.NamestajNaAkciji)
                    {
                        if (namestajNaAkciji.IdNamestaja == namestaj.Id && namestajNaAkciji.Obrisan == false)
                        {
                            listaNamestaja.Remove(namestaj); //update za listu namestaja koji nije na akciji
                        }
                    }
                }
            }
            dgNamestaj.ItemsSource = listaNamestaja;
            dgNamestaj.DataContext = akcija;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.CanUserAddRows = false;
            dgNamestaj.IsReadOnly = true;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            var ucitaneAkcije = Projekat.Instanca.Akcija;
            var izabranNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            if (izabranNamestaj != null)
            {
                var namestajNaAkciji = new NamestajNaAkciji();
                namestajNaAkciji.IdNamestaja = izabranNamestaj.Id;
                namestajNaAkciji.IdAkcije = akcija.Id;
                NamestajNaAkciji.Create(namestajNaAkciji); //novi namestaj na akciji

                double ukupnaCena = izabranNamestaj.Cena - (izabranNamestaj.Cena * (decimal.ToDouble(akcija.Popust) / 100));
                izabranNamestaj.AkcijskaCena = Math.Round(ukupnaCena, 2);
                Namestaj.Update(izabranNamestaj); //update cenu za namestaj
            }
            MessageBox.Show("Izabrani namestaj je dodat na akciju!");
            return;
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
