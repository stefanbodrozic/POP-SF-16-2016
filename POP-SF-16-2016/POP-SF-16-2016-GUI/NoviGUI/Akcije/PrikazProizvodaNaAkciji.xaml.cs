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
                        foreach (var namestajAkcija in Projekat.Instanca.NamestajNaAkciji)
                        {
                            if(namestaj.Id == namestajAkcija.IdNamestaja && namestajAkcija.IdAkcije == akcija.Id && namestajAkcija.Obrisan == false)
                            {
                                namestajNaAkciji.Add(namestaj); //namestaj na izabranoj akciji
                            }
                        }
                    }
                }
            }
            dgNamestaj.ItemsSource = namestajNaAkciji;
            dgNamestaj.DataContext = this;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.CanUserAddRows = false;
            dgNamestaj.IsReadOnly = true;
        }

        private void btnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            var ucitaneAkcije = Projekat.Instanca.Akcija;
            var izabraniNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            if(izabraniNamestaj != null)
            {
                if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete namestaj sa akcije?", "Brisanje namestaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    foreach (var a in ucitaneAkcije)
                    {
                        if (a.Id == akcija.Id)
                        {
                            foreach (var namestajAkcija in Projekat.Instanca.NamestajNaAkciji)
                            {
                                if (namestajAkcija.IdAkcije == akcija.Id && namestajAkcija.IdNamestaja == izabraniNamestaj.Id && namestajAkcija.Obrisan == false)
                                {
                                    namestajAkcija.Obrisan = true;
                                    NamestajNaAkciji.Update(namestajAkcija); //update 
                                    namestajNaAkciji.Remove(izabraniNamestaj); //i sklanja sa liste za prikaz

                                    izabraniNamestaj.AkcijskaCena = 0;
                                    Namestaj.Update(izabraniNamestaj); //update za namestaj da akcijska cena bude 0
                                }
                            }
                        }
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

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            var dodavanje = new DodajProizvodNaAkciju(akcija, namestajNaAkciji);
            dodavanje.ShowDialog();
        }
    }
}
