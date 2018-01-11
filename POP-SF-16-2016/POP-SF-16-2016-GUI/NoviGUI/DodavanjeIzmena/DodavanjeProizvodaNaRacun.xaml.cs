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

            foreach (var namestaj in Projekat.Instanca.Namestaj)
            {
                namestajZaPrikaz.Add(namestaj);
            }

            dgNamestaj.ItemsSource = namestajZaPrikaz;
            dgNamestaj.DataContext = this;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.CanUserAddRows = false;
            dgNamestaj.IsReadOnly = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            foreach (var dodatna in Projekat.Instanca.DodatneUsluge)
            {
                dodatneUslugeZaPrikaz.Add(dodatna);
            }

            dgDodatna.ItemsSource = dodatneUslugeZaPrikaz;
            dgDodatna.DataContext = this;
            dgDodatna.IsSynchronizedWithCurrentItem = true;
            dgDodatna.CanUserAddRows = false;
            dgDodatna.IsReadOnly = true;
            dgDodatna.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void btnDodajDodatna_Click(object sender, RoutedEventArgs e)
        {
            if (tbKolicinaDodatna.Text == "")
            {
                MessageBox.Show("Nije izabrana dodatna usluga za prodaju i/ili nije uneta kolicina!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int.Parse(tbKolicinaDodatna.Text);
            }
            catch
            {
                MessageBox.Show("Kolicina mora biti ceo broj!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (tbKolicinaDodatna.Text == "")
            {
                MessageBox.Show("Kolicina mora biti uneta!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (int.Parse(tbKolicinaDodatna.Text) < 1)
            {
                MessageBox.Show("Kolicina mora biti veca od 0!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var izabranaDodatna = (DodatneUsluge)dgDodatna.SelectedItem;
            if (izabranaDodatna != null)
            {
                if (MessageBox.Show($"Da li ste sigurni da zelite da dodate stavku na racun: {izabranaDodatna.Naziv}", "Dodavanje stavke na racun", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int kolicina = int.Parse(tbKolicinaDodatna.Text);
                    bool postojiUsluga = false;
                    foreach (var stavkaDodatna in Projekat.Instanca.StavkaRacunaDodatnaUsluga.ToList()) //proveram da li postoji ista usluga prodata i ako postoji update za nju broj prodatih komada i cenu
                    {
                        if (stavkaDodatna.Obrisan == false && stavkaDodatna.IdProdajeNamestaja == prodaja.Id && stavkaDodatna.IdDodatneUsluge == izabranaDodatna.Id)
                        {
                            prodateDodatneUsluge.Remove(izabranaDodatna); //prikaz u drugom prozoru
                            postojiUsluga = true;

                            stavkaDodatna.Kolicina += kolicina;
                            StavkaRacunaDodatnaUsluga.Update(stavkaDodatna); //update kolicinu za postojecu stavku 

                            prodaja.CenaBezPdv += (izabranaDodatna.Iznos * kolicina);
                            prodaja.UkupnaCena += (((izabranaDodatna.Iznos * decimal.ToDouble(prodaja.Pdv)) + izabranaDodatna.Iznos) * kolicina);
                            ProdajaNamestaja.Update(prodaja); //update cenu

                            izabranaDodatna.ProdataKolicina += kolicina; //update za prodatu kolicinu
                            prodateDodatneUsluge.Add(izabranaDodatna); //prikaz u drugom prozoru
                        }
                    }

                    foreach (var stavkaDodatna2 in Projekat.Instanca.StavkaRacunaDodatnaUsluga.ToList()) //proveravam da li postoji ista usluga prodata na istom racunu i ako ne postoji pravim novu
                    {
                        if (postojiUsluga == false)
                        {
                            if (stavkaDodatna2.Obrisan == false && stavkaDodatna2.IdProdajeNamestaja == prodaja.Id && stavkaDodatna2.IdDodatneUsluge != izabranaDodatna.Id) //ako postoji stavka na racunu ali nema prodata usluga
                            {
                                var novaStavka = new StavkaRacunaDodatnaUsluga();
                                novaStavka.IdProdajeNamestaja = prodaja.Id;
                                novaStavka.IdDodatneUsluge = izabranaDodatna.Id;
                                novaStavka.Kolicina = kolicina;
                                StavkaRacunaDodatnaUsluga.Create(novaStavka); //pravim novu stavku racuna


                                prodaja.CenaBezPdv += (izabranaDodatna.Iznos * kolicina);
                                prodaja.UkupnaCena += (((izabranaDodatna.Iznos * decimal.ToDouble(prodaja.Pdv)) + izabranaDodatna.Iznos) * kolicina);
                                ProdajaNamestaja.Update(prodaja); //update cenu

                                izabranaDodatna.ProdataKolicina = kolicina; //update za kolicinu
                                prodateDodatneUsluge.Add(izabranaDodatna); //prikaz u drugom prozoru

                                break;
                            }
                            var novaStavka2 = new StavkaRacunaDodatnaUsluga(); //u slucaju da ne postoji stavka na racunu (prazan racun), pravim novu stavku i vezujem za ovaj racun
                            novaStavka2.IdProdajeNamestaja = prodaja.Id;
                            novaStavka2.IdDodatneUsluge = izabranaDodatna.Id;
                            novaStavka2.Kolicina = kolicina;
                            StavkaRacunaDodatnaUsluga.Create(novaStavka2);

                            prodaja.CenaBezPdv += izabranaDodatna.Iznos * kolicina;
                            prodaja.UkupnaCena += ((izabranaDodatna.Iznos * decimal.ToDouble(prodaja.Pdv)) + izabranaDodatna.Iznos) * kolicina;
                            ProdajaNamestaja.Update(prodaja); //update cenu

                            izabranaDodatna.ProdataKolicina = kolicina;
                            prodateDodatneUsluge.Add(izabranaDodatna); //prikaz u drugom prozoru
                            break;
                        }

                    }
                }
            }
        }

        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        {
            if (tbKolicinaNamestaj.Text == "")
            {
                MessageBox.Show("Nije izabran namestaj za prodaju i/ili nije uneta kolicina!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int.Parse(tbKolicinaNamestaj.Text);
            }
            catch
            {
                MessageBox.Show("Kolicina mora biti ceo broj!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (tbKolicinaNamestaj.Text == "")
            {
                MessageBox.Show("Kolicina mora biti uneta!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (int.Parse(tbKolicinaNamestaj.Text) < 1)
            {
                MessageBox.Show("Kolicina mora biti veca od 0!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var izabranNamestaj = (Namestaj)dgNamestaj.SelectedItem;
            if (izabranNamestaj != null)
            {
                if (MessageBox.Show($"Da li ste sigurni da zelite da dodate stavku na racun: {izabranNamestaj.Naziv}", "Dodavanje stavke na racun", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int kolicina = int.Parse(tbKolicinaNamestaj.Text);
                    bool postojiUsluga = false;
                    foreach (var stavkaNamestaj in Projekat.Instanca.StavkaRacunaNamestaj.ToList()) //proveram da li postoji isti namestaj prodat i ako postoji update mu kolicinu i cenu na racunu 
                    {
                        if (stavkaNamestaj.Obrisan == false && stavkaNamestaj.IdProdajeNamestaja == prodaja.Id && stavkaNamestaj.IdNamestaja == izabranNamestaj.Id)
                        {
                            prodatNamestaj.Remove(izabranNamestaj); //prikaz u drugom prozoru
                            postojiUsluga = true;

                            stavkaNamestaj.Kolicina += kolicina;
                            StavkaRacunaNamestaj.Update(stavkaNamestaj); //update kolicinu za postojecu stavku 

                            if (izabranNamestaj.AkcijskaCena == 0) //ako je akcijska cena 0 racunam po redovnoj ceni
                            {
                                prodaja.CenaBezPdv += (izabranNamestaj.Cena * kolicina);
                                prodaja.UkupnaCena += (((izabranNamestaj.Cena * decimal.ToDouble(prodaja.Pdv)) + izabranNamestaj.Cena) * kolicina);
                            }

                            if (izabranNamestaj.AkcijskaCena != 0)// ako je akcijska cena razlicita od 0 racunam po akcijskoj ceni
                            {
                                prodaja.CenaBezPdv += (izabranNamestaj.AkcijskaCena * kolicina);
                                prodaja.UkupnaCena += (((izabranNamestaj.AkcijskaCena * decimal.ToDouble(prodaja.Pdv)) + izabranNamestaj.AkcijskaCena) * kolicina);
                            }
                            ProdajaNamestaja.Update(prodaja); //update cenu

                            izabranNamestaj.ProdataKolicina += kolicina; //update za prodatu kolicinu
                            prodatNamestaj.Add(izabranNamestaj); //prikaz u drugom prozoru
                        }
                    }

                    foreach (var stavkaNamestaj2 in Projekat.Instanca.StavkaRacunaNamestaj.ToList()) //proveravam da li postoji isti namestaj prodat na istom racunu i ako ne postoji pravim novu stavku sa namestajem
                    {
                        if (postojiUsluga == false)
                        {
                            if (stavkaNamestaj2.Obrisan == false && stavkaNamestaj2.IdProdajeNamestaja == prodaja.Id && stavkaNamestaj2.IdNamestaja != izabranNamestaj.Id) //ako postoji stavka na racunu ali nema prodat namestaj
                            {
                                var novaStavka = new StavkaRacunaNamestaj();
                                novaStavka.IdProdajeNamestaja = prodaja.Id;
                                novaStavka.IdNamestaja = izabranNamestaj.Id;
                                novaStavka.Kolicina = kolicina;
                                StavkaRacunaNamestaj.Create(novaStavka);

                                if (izabranNamestaj.AkcijskaCena == 0) //ako je akcijska cena 0 racunam po redovnoj ceni
                                {
                                    prodaja.CenaBezPdv += (izabranNamestaj.Cena * kolicina);
                                    prodaja.UkupnaCena += (((izabranNamestaj.Cena * decimal.ToDouble(prodaja.Pdv)) + izabranNamestaj.Cena) * kolicina);
                                }

                                if(izabranNamestaj.AkcijskaCena != 0)// ako je akcijska cena razlicita od 0 racunam po akcijskoj ceni
                                {
                                    prodaja.CenaBezPdv += (izabranNamestaj.AkcijskaCena * kolicina);
                                    prodaja.UkupnaCena += (((izabranNamestaj.AkcijskaCena * decimal.ToDouble(prodaja.Pdv)) + izabranNamestaj.AkcijskaCena) * kolicina);
                                }
                                ProdajaNamestaja.Update(prodaja); //update cenu

                                izabranNamestaj.ProdataKolicina = kolicina;
                                prodatNamestaj.Add(izabranNamestaj); //prikaz u drugom prozoru

                                break;
                            }
                            var novaStavka2 = new StavkaRacunaNamestaj(); //u slucaju da ne postoji stavka na racunu (prazan racun), pravim novu stavku i vezujem za ovaj racun
                            novaStavka2.IdProdajeNamestaja = prodaja.Id;
                            novaStavka2.IdNamestaja = izabranNamestaj.Id;
                            novaStavka2.Kolicina = kolicina;
                            StavkaRacunaNamestaj.Create(novaStavka2);

                            if (izabranNamestaj.AkcijskaCena == 0) //ako je akcijska cena 0 racunam redovnu cenu
                            {
                                prodaja.CenaBezPdv += izabranNamestaj.Cena * kolicina;
                                prodaja.UkupnaCena += ((izabranNamestaj.Cena * decimal.ToDouble(prodaja.Pdv)) + izabranNamestaj.Cena) * kolicina;
                            }

                            if (izabranNamestaj.AkcijskaCena != 0) //ako je akcijska cena razlicita od 0 racunam po akcijskoj ceni
                            {
                                prodaja.CenaBezPdv += (izabranNamestaj.AkcijskaCena * kolicina);
                                prodaja.UkupnaCena += (((izabranNamestaj.AkcijskaCena * decimal.ToDouble(prodaja.Pdv)) + izabranNamestaj.AkcijskaCena) * kolicina);
                            }
                            ProdajaNamestaja.Update(prodaja); //update cenu

                            izabranNamestaj.ProdataKolicina = kolicina;
                            prodatNamestaj.Add(izabranNamestaj); //prikaz u drugom prozoru
                            break;
                        }
                    }
                }
            }
        }

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "TipNamestajaId" || e.Column.Header.ToString() == "AkcijaId" || e.Column.Header.ToString() == "Sifra" || e.Column.Header.ToString() == "ProdataKolicina" || e.Column.Header.ToString() == "KolicinaUMagacinu" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void dgDodatna_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "ProdataKolicina" || e.Column.Header.ToString() == "Obrisan")
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

