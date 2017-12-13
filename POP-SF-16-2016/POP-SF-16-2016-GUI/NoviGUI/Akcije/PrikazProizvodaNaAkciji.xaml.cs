using POP_SF_16_2016_GUI.Model;
using POP_SF_16_2016_GUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for PrikazProizvodaNaAkciji.xaml
    /// </summary>
    public partial class PrikazProizvodaNaAkciji : Window
    {
        private ICollectionView view;
        private Akcija akcija;

        ObservableCollection<Namestaj> namestajNaAkciji = new ObservableCollection<Namestaj>();
        public PrikazProizvodaNaAkciji(Akcija akcija)
        {
            InitializeComponent();
            this.akcija = akcija;

            

            foreach (var a in Projekat.Instanca.Akcija)
            {
                if(a.Id == akcija.Id)
                {
                    foreach (var namestaj in Projekat.Instanca.Namestaj)
                    {
                        foreach (var idNamestaja in akcija.IdNamestajaNaAkciji)
                        {
                            if (idNamestaja == namestaj.Id)
                            {
                                namestajNaAkciji.Add(namestaj);
                            }
                        }
                    }
                }
            }
            view = CollectionViewSource.GetDefaultView(namestajNaAkciji);
            dgNamestaj.ItemsSource = view;
            dgNamestaj.DataContext = this;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.CanUserAddRows = false;
            dgNamestaj.IsReadOnly = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            
        }

        private void btnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            var ucitaneAkcije = Projekat.Instanca.Akcija;
            var izabraniNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            if(izabraniNamestaj != null)
            {
                foreach (var a in ucitaneAkcije)
                {
                    if (a.Id == akcija.Id)
                    {
                        a.IdNamestajaNaAkciji.Remove(izabraniNamestaj.Id);
                        namestajNaAkciji.Remove(izabraniNamestaj);

                        GenericSerializer.Serialize("akcija.xml", ucitaneAkcije);
                        MessageBox.Show("Namestaj je izbrisan sa akcije!");
                        view.Refresh();
                    }
                }
                

            }
            
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
