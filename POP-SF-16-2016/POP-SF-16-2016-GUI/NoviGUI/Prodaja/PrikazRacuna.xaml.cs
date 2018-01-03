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
    /// Interaction logic for PrikazRacuna.xaml
    /// </summary>
    public partial class PrikazRacuna : Window
    {
        ProdajaNamestaja prodaja;
        ObservableCollection<Namestaj> prodatNamestaj = new ObservableCollection<Namestaj>();
        ObservableCollection<DodatneUsluge> prodateDodatneUsluge = new ObservableCollection<DodatneUsluge>();
        public PrikazRacuna(ProdajaNamestaja prodaja)
        {
            InitializeComponent();
            this.prodaja = prodaja;

            ObservableCollection<ProdajaNamestaja> prodajaNamestaja = new ObservableCollection<ProdajaNamestaja>();
            foreach (var p in Projekat.Instanca.ProdajaNamestaja)
            {
                if (p.Id == prodaja.Id)
                {
                    prodajaNamestaja.Add(p); //za podatke o prodaji
                }
            }

            dgRacun.ItemsSource = prodajaNamestaja;
            dgRacun.DataContext = this;
            dgRacun.IsSynchronizedWithCurrentItem = true;
            dgRacun.CanUserAddRows = false;
            dgRacun.IsReadOnly = true;
            dgRacun.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            foreach (var stavkaNamestaj in Projekat.Instanca.StavkaRacunaNamestaj)
            {
                if (stavkaNamestaj.IdProdajeNamestaja == prodaja.Id && stavkaNamestaj.Obrisan == false)
                {
                    foreach (var namestaj in Projekat.Instanca.Namestaj)
                    {
                        if (stavkaNamestaj.IdNamestaja == namestaj.Id)
                        {
                            namestaj.ProdataKolicina = stavkaNamestaj.Kolicina; //update prodatu kolicinu
                            prodatNamestaj.Add(namestaj); //prikazujem prodat namestaj 
                        }
                    }
                }
            }

            dgNamestaj.ItemsSource = prodatNamestaj; 
            dgNamestaj.DataContext = this;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.CanUserAddRows = false;
            dgNamestaj.IsReadOnly = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            foreach (var stavkaDodatna in Projekat.Instanca.StavkaRacunaDodatnaUsluga)
            {
                if (stavkaDodatna.IdProdajeNamestaja == prodaja.Id)
                {
                    foreach (var dodatnaUsluga in Projekat.Instanca.DodatneUsluge)
                    {
                        if (stavkaDodatna.IdDodatneUsluge == dodatnaUsluga.Id && stavkaDodatna.Obrisan == false)
                        {
                            dodatnaUsluga.ProdataKolicina = stavkaDodatna.Kolicina; //update prodatu kolicinu
                            prodateDodatneUsluge.Add(dodatnaUsluga); //prikazujem prodate dodatne usluge
                        }
                    }
                }
            }
            dgDodatnaUsluga.ItemsSource = prodateDodatneUsluge;
            dgDodatnaUsluga.DataContext = this;
            dgDodatnaUsluga.IsSynchronizedWithCurrentItem = true;
            dgDodatnaUsluga.CanUserAddRows = false;
            dgDodatnaUsluga.IsReadOnly = true;
            dgDodatnaUsluga.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

        }

        private void btnDodajNaRacun_Click(object sender, RoutedEventArgs e)
        {
            var dodaj = new DodavanjeProizvodaNaRacun(prodaja, prodateDodatneUsluge, prodatNamestaj);
            dodaj.ShowDialog(); //dodavanje na racun
        }

        private void btnIzbrisiDodatnaUsluga_Click(object sender, RoutedEventArgs e)
        {
            var izabranaDodatna = (DodatneUsluge)dgDodatnaUsluga.SelectedItem;
            if (izabranaDodatna != null)
            {
                if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete stavku sa racuna: {izabranaDodatna.Naziv}", "Brisanje stavke sa racuna", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    foreach (var stavkaDodatna in Projekat.Instanca.StavkaRacunaDodatnaUsluga)
                    {
                        if (stavkaDodatna.IdDodatneUsluge == izabranaDodatna.Id)
                        {
                            StavkaRacunaDodatnaUsluga.Delete(stavkaDodatna); //brisem stavku sa racuna

                            prodateDodatneUsluge.Remove(izabranaDodatna); //brisanje za prikaz

                            prodaja.UkupnaCena -= Math.Round((((izabranaDodatna.Iznos * double.Parse(prodaja.Pdv.ToString())) + izabranaDodatna.Iznos) * stavkaDodatna.Kolicina), 2); //update za cenu
                            prodaja.CenaBezPdv -= Math.Round((izabranaDodatna.Iznos * stavkaDodatna.Kolicina), 2);   //update za cenu
                            ProdajaNamestaja.Update(prodaja);
                        }
                    }
                }
            }
        }

        private void btnIzbrisiNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var izabranNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            if (izabranNamestaj != null)
            {
                if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete stavku sa racuna: {izabranNamestaj.Naziv}", "Brisanje stavke sa racuna", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    foreach (var stavkaNamestaj in Projekat.Instanca.StavkaRacunaNamestaj)
                    {
                        if (stavkaNamestaj.IdNamestaja == izabranNamestaj.Id)
                        {
                            stavkaNamestaj.Obrisan = true;
                            StavkaRacunaNamestaj.Update(stavkaNamestaj); //brisem stavku sa racuna

                            prodatNamestaj.Remove(izabranNamestaj); //brisanje za prikaz

                            if(izabranNamestaj.AkcijskaCena == 0)
                            {
                                prodaja.UkupnaCena -= Math.Round((((izabranNamestaj.Cena * double.Parse(prodaja.Pdv.ToString())) + izabranNamestaj.Cena) * stavkaNamestaj.Kolicina), 2); //update za cenu
                                prodaja.CenaBezPdv -= Math.Round((izabranNamestaj.Cena * stavkaNamestaj.Kolicina), 2); //update za cenu
                                ProdajaNamestaja.Update(prodaja);
                            }
                            if(izabranNamestaj.AkcijskaCena != 0)
                            {
                                prodaja.UkupnaCena -= Math.Round((((izabranNamestaj.AkcijskaCena * double.Parse(prodaja.Pdv.ToString())) + izabranNamestaj.AkcijskaCena) * stavkaNamestaj.Kolicina), 2); //update za cenu
                                prodaja.CenaBezPdv -= Math.Round((izabranNamestaj.AkcijskaCena * stavkaNamestaj.Kolicina), 2); //update za cenu
                                ProdajaNamestaja.Update(prodaja);
                            }                            

                            izabranNamestaj.KolicinaUMagacinu += stavkaNamestaj.Kolicina; //namestaj je sklonjen sa racuna i kolicina u magacinu se mora vratiti na stanje pre prodaje
                            Namestaj.Update(izabranNamestaj);
                        }
                    }
                }
            }
        }

        private void dgRacun_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "TipNamestajaId" || e.Column.Header.ToString() == "AkcijaId" || e.Column.Header.ToString() == "Sifra" || e.Column.Header.ToString() == "KolicinaUMagacinu" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void dgDodatnaUsluga_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }


        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}



