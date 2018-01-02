using POP_SF_16_2016_GUI.Model;
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
    /// Interaction logic for DodavanjeProizvodaNaRacun.xaml
    /// </summary>
    public partial class DodavanjeProizvodaNaRacun : Window
    {
        ProdajaNamestaja prodaja;
        ObservableCollection<Namestaj> namestajZaPrikaz = new ObservableCollection<Namestaj>();
        ObservableCollection<DodatneUsluge> dodatneUslugeZaPrikaz = new ObservableCollection<DodatneUsluge>();
        ObservableCollection<DodatneUsluge> prodateDodatneUsluge = new ObservableCollection<DodatneUsluge>();
        ObservableCollection<Namestaj> prodatNamestaj = new ObservableCollection<Namestaj>();
        public DodavanjeProizvodaNaRacun(ProdajaNamestaja prodaja, ObservableCollection<DodatneUsluge> prodateDodatneUsluge, ObservableCollection<Namestaj> prodatNamestaj)
        {
            InitializeComponent();

            this.prodaja = prodaja;
            this.prodatNamestaj = prodatNamestaj;
            this.prodateDodatneUsluge = prodateDodatneUsluge;

            foreach (var stavkaNamestaj in Projekat.Instanca.StavkaRacunaNamestaj)
            {
                foreach (var n in Projekat.Instanca.Namestaj)
                {
                    if (stavkaNamestaj.IdProdajeNamestaja == prodaja.Id) //ako postoji stavka vezana za izabrani racun
                    {
                        if (stavkaNamestaj.Obrisan == false && stavkaNamestaj.IdNamestaja != n.Id) //ako nije obrisana i namestaj nije na tom racunu
                        {
                            this.namestajZaPrikaz.Add(n); //dodajem sve namestaje koji nisu na tom racunu
                        }
                    }
                }
            }

            dgNamestaj.ItemsSource = this.namestajZaPrikaz;
            dgNamestaj.DataContext = this;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.CanUserAddRows = false;
            dgNamestaj.IsReadOnly = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            foreach (var stavkaDodatna in Projekat.Instanca.StavkaRacunaDodatnaUsluga)
            {
                if (stavkaDodatna.Obrisan == false && stavkaDodatna.IdProdajeNamestaja == prodaja.Id)
                {
                    foreach (var dodatna in Projekat.Instanca.DodatneUsluge)
                    {
                        if (stavkaDodatna.IdDodatneUsluge != dodatna.Id)
                        {
                            this.dodatneUslugeZaPrikaz.Add(dodatna);
                        }
                    }

                    

                }


                dgDodatna.ItemsSource = this.dodatneUslugeZaPrikaz;
                dgDodatna.DataContext = this;
                dgDodatna.IsSynchronizedWithCurrentItem = true;
                dgDodatna.CanUserAddRows = false;
                dgDodatna.IsReadOnly = true;
                dgDodatna.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


            }
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDodajDodatna_Click(object sender, RoutedEventArgs e)
        {
            if (tbKolicinaDodatna.Text == "")
            {
                MessageBox.Show("Nije izabrana dodatna usluga za prodaju i/ili nije uneta kolicina!", "Greska", MessageBoxButton.OK);
                return;
            }

            try
            {
                int.Parse(tbKolicinaDodatna.Text);
            }
            catch
            {
                MessageBox.Show("Kolicina mora biti ceo broj!", "Greska", MessageBoxButton.OK);
                return;
            }

            if (tbKolicinaDodatna.Text == "")
            {
                MessageBox.Show("Kolicina mora biti uneta!", "Greska", MessageBoxButton.OK);
                return;
            }

            if (int.Parse(tbKolicinaDodatna.Text) < 1)
            {
                MessageBox.Show("Kolicina mora biti veca od 0!", "Greska", MessageBoxButton.OK);
                return;
            }

            var izabranaDodatna = (DodatneUsluge)dgDodatna.SelectedItem;
            if (izabranaDodatna != null)
            {
                if (MessageBox.Show($"Da li ste sigurni da zelite da dodate stavku na racun: {izabranaDodatna.Naziv}", "Dodavanje stavke na racun", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int kolicina = int.Parse(tbKolicinaDodatna.Text);

                    var novaStavka = new StavkaRacunaDodatnaUsluga();
                    novaStavka.IdProdajeNamestaja = prodaja.Id;
                    novaStavka.IdDodatneUsluge = izabranaDodatna.Id;
                    novaStavka.Kolicina = kolicina;
                    StavkaRacunaDodatnaUsluga.Create(novaStavka); //pravim novu stavku racuna

                    izabranaDodatna.ProdataKolicina = kolicina;

                    prodaja.CenaBezPdv += (izabranaDodatna.Iznos * kolicina);
                    prodaja.UkupnaCena += (((izabranaDodatna.Iznos * double.Parse(prodaja.Pdv.ToString())) + izabranaDodatna.Iznos) * kolicina);
                    ProdajaNamestaja.Update(prodaja); //update cenu

                    dodatneUslugeZaPrikaz.Remove(izabranaDodatna); //za prikaz
                    
                    prodateDodatneUsluge.Add(izabranaDodatna); //prikaz u drugom prozoru
                }
            }
        }

        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        {
            if (tbKolicinaNamestaj.Text == "")
            {
                MessageBox.Show("Nije izabran namestaj za prodaju i/ili nije uneta kolicina!", "Greska", MessageBoxButton.OK);
                return;
            }

            try
            {
                int.Parse(tbKolicinaNamestaj.Text);
            }
            catch
            {
                MessageBox.Show("Kolicina mora biti ceo broj!", "Greska", MessageBoxButton.OK);
                return;
            }

            if (tbKolicinaNamestaj.Text == "")
            {
                MessageBox.Show("Kolicina mora biti uneta!", "Greska", MessageBoxButton.OK);
                return;
            }

            if (int.Parse(tbKolicinaNamestaj.Text) < 1)
            {
                MessageBox.Show("Kolicina mora biti veca od 0!", "Greska", MessageBoxButton.OK);
                return;
            }

            var izabranNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            if (izabranNamestaj != null)
            {
                if (MessageBox.Show($"Da li ste sigurni da zelite da dodate stavku na racun: {izabranNamestaj.Naziv}", "Dodavanje stavke na racun", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int kolicina = int.Parse(tbKolicinaNamestaj.Text);

                    var novaStavka = new StavkaRacunaNamestaj();
                    novaStavka.IdNamestaja = izabranNamestaj.Id;
                    novaStavka.IdProdajeNamestaja = prodaja.Id;
                    novaStavka.Kolicina = kolicina;
                    StavkaRacunaNamestaj.Create(novaStavka); //pravim novu stavku racuna

                    izabranNamestaj.ProdataKolicina = kolicina;

                    prodaja.CenaBezPdv += (izabranNamestaj.Cena * kolicina);
                    prodaja.UkupnaCena += (((izabranNamestaj.Cena * double.Parse(prodaja.Pdv.ToString())) + izabranNamestaj.Cena) * kolicina);
                    ProdajaNamestaja.Update(prodaja); //update za cenu

                    namestajZaPrikaz.Remove(izabranNamestaj); //za prikaz

                    prodatNamestaj.Add(izabranNamestaj); //prikaz u drugom prozoru
                }

            }

        }

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "TipNamestajaId" || e.Column.Header.ToString() == "AkcijaId" || e.Column.Header.ToString() == "Sifra" || e.Column.Header.ToString() == "KolicinaUMagacinu" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }
    }
}
