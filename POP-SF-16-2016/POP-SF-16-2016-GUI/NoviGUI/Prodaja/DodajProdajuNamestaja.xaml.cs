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
        private ProdajaNamestaja prodajaNamestaja;
        ObservableCollection<Namestaj> prodatNamestaj = new ObservableCollection<Namestaj>();
        ObservableCollection<DodatneUsluge> prodateDodatne = new ObservableCollection<DodatneUsluge>();
        double cenaBezPdv = 0;
        double ukupnaCena = 0;
        public DodajProdajuNamestaja(ProdajaNamestaja prodajaNamestaja)
        {
            InitializeComponent();
            this.prodajaNamestaja = prodajaNamestaja;

            tbCena.Text = ukupnaCena.ToString();

            tbCenaBezPdv.Text = cenaBezPdv.ToString();

            var listaNamestaja = new ObservableCollection<Namestaj>();
            foreach (var namestaj in Projekat.Instanca.Namestaj)
            {
                if (namestaj.Obrisan != true && namestaj.KolicinaUMagacinu > 0)
                {
                    listaNamestaja.Add(namestaj);
                }
            }
            dgNamestaj.ItemsSource = listaNamestaja;
            dgNamestaj.DataContext = prodajaNamestaja;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.CanUserAddRows = false;
            dgNamestaj.IsReadOnly = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);


            var listaDodatnihUsluga = new ObservableCollection<DodatneUsluge>();
            foreach (var usluga in Projekat.Instanca.DodatneUsluge)
            {
                if (usluga.Obrisan != true)
                {
                    listaDodatnihUsluga.Add(usluga);
                }
            }
            dgDodatneUsluge.ItemsSource = listaDodatnihUsluga;
            dgDodatneUsluge.DataContext = prodajaNamestaja;
            dgDodatneUsluge.IsSynchronizedWithCurrentItem = true;
            dgDodatneUsluge.CanUserAddRows = false;
            dgDodatneUsluge.IsReadOnly = true;
            dgDodatneUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            tbKupac.DataContext = prodajaNamestaja;

            ProdajaNamestaja.Create(prodajaNamestaja); //pravim novu prodaju da bih mogao da koristim njen ID
            ProdajaNamestaja.Delete(prodajaNamestaja); //postavljam da bude obrisana kako se ne bi prikazivala u slucaju da bude otkazano pravljenje racuna

            dgProdatNamestaj.ItemsSource = prodatNamestaj;
            dgProdatNamestaj.IsSynchronizedWithCurrentItem = true;
            dgProdatNamestaj.CanUserAddRows = false;
            dgProdatNamestaj.IsReadOnly = true;
            dgProdatNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            dgProdateDodatne.ItemsSource = prodateDodatne;
            dgProdateDodatne.IsSynchronizedWithCurrentItem = true;
            dgProdateDodatne.CanUserAddRows = false;
            dgProdateDodatne.IsReadOnly = true;
            dgProdateDodatne.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var izabranaStavka = (Namestaj)dgNamestaj.SelectedItem;

            if (tbKolicina.Text == "")
            {
                MessageBox.Show("Nije izabran namestaj za prodaju i/ili nije uneta kolicina!", "Greska", MessageBoxButton.OK);
                return;
            }

            if (izabranaStavka != null)
            {
                try
                {
                    int.Parse(tbKolicina.Text);
                }
                catch
                {
                    MessageBox.Show("Kolicina mora biti ceo broj!", "Greska", MessageBoxButton.OK);
                    return;
                }

                if (tbKolicina.Text == "")
                {
                    MessageBox.Show("Kolicina mora biti uneta!", "Greska", MessageBoxButton.OK);
                    return;
                }

                int unetaKolicina = int.Parse(tbKolicina.Text);
                //provera unosa kolicine
                if (unetaKolicina <= 0)
                {
                    MessageBox.Show("Uneta kolicina mora biti veca od 0!");
                    return;
                }
                if (unetaKolicina > izabranaStavka.KolicinaUMagacinu)
                {
                    MessageBox.Show("Uneta kolicina nema na stanju!");
                    return;
                }

                //ako je vec prodat namestaj update mu kolicinu
                foreach (var stavkaNamestaj in Projekat.Instanca.StavkaRacunaNamestaj.ToList()) //proveram da li postoji isti namestaj prodat i ako postoji update mu kolicinu
                {
                    if (stavkaNamestaj.Obrisan == false && stavkaNamestaj.IdProdajeNamestaja == prodajaNamestaja.Id && stavkaNamestaj.IdNamestaja == izabranaStavka.Id)
                    {
                        izabranaStavka.KolicinaUMagacinu += stavkaNamestaj.Kolicina;

                        if (izabranaStavka.AkcijskaCena == 0) //ako je akcijska cena 0 racunam po redovnoj ceni
                        {
                            cenaBezPdv -= stavkaNamestaj.Kolicina * izabranaStavka.Cena; //umanjujem cenu za postojece prodat namestaj kako bi racun bio na "nuli" za taj proizvod
                        }
                        if (izabranaStavka.AkcijskaCena != 0) // ako je akcijska cena razlicita od 0 racunam po akcijskoj ceni
                        {
                            cenaBezPdv -= stavkaNamestaj.Kolicina * izabranaStavka.AkcijskaCena; //umanjujem cenu za postojece prodat namestaj kako bi racun bio na "nuli" za taj proizvod
                        }

                        prodatNamestaj.Remove(izabranaStavka); //zbog prikaza

                        stavkaNamestaj.Kolicina += unetaKolicina;
                        StavkaRacunaNamestaj.Update(stavkaNamestaj); //update kolicinu za postojecu stavku 

                        izabranaStavka.ProdataKolicina += unetaKolicina; //update za prodatu kolicinu
                        prodatNamestaj.Add(izabranaStavka);


                        if (izabranaStavka.AkcijskaCena == 0) //ako je akcijska cena 0 racunam po redovnoj ceni
                        {
                            cenaBezPdv += stavkaNamestaj.Kolicina * izabranaStavka.Cena;
                        }
                        if (izabranaStavka.AkcijskaCena != 0) // ako je akcijska cena razlicita od 0 racunam po akcijskoj ceni
                        {
                            cenaBezPdv += stavkaNamestaj.Kolicina * izabranaStavka.AkcijskaCena;
                        }
                        ukupnaCena = (cenaBezPdv * decimal.ToDouble(prodajaNamestaja.Pdv)) + cenaBezPdv; //racuna se nova cena za prikaz

                        OsveziPrikazCene();

                        izabranaStavka.KolicinaUMagacinu -= stavkaNamestaj.Kolicina;
                        Namestaj.Update(izabranaStavka); //update za kolicinu

                        MessageBox.Show("Izabrani namestaj je dodat na racun!");
                        return;
                    }
                }

                //nova stavka na racunu
                var stavka = new StavkaRacunaNamestaj()
                {
                    IdProdajeNamestaja = prodajaNamestaja.Id,
                    IdNamestaja = izabranaStavka.Id,
                    Kolicina = unetaKolicina
                };
                StavkaRacunaNamestaj.Create(stavka);

                if (izabranaStavka.AkcijskaCena == 0) //ako je akcijska cena 0 racunam po redovnoj ceni
                {
                    cenaBezPdv += stavka.Kolicina * izabranaStavka.Cena; //cena za prikaz
                }
                if (izabranaStavka.AkcijskaCena != 0) // ako je akcijska cena razlicita od 0 racunam po akcijskoj ceni
                {
                    cenaBezPdv += stavka.Kolicina * izabranaStavka.AkcijskaCena; //cena za prikaz
                }

                ukupnaCena = (cenaBezPdv * decimal.ToDouble(prodajaNamestaja.Pdv)) + cenaBezPdv; //racuna se nova cena za prikaz

                OsveziPrikazCene();


                //update za kolicinu kada se proda namestaj
                foreach (var namestaj in Projekat.Instanca.Namestaj)
                {
                    if (namestaj.Id == izabranaStavka.Id)
                    {
                        namestaj.KolicinaUMagacinu -= stavka.Kolicina;
                        Namestaj.Update(namestaj);
                    }
                }

                izabranaStavka.ProdataKolicina = unetaKolicina; //update za prikaz prodate kolicine
                prodatNamestaj.Add(izabranaStavka); //za prikaz prodatih stavki
            }
            MessageBox.Show("Izabrani namestaj je dodat na racun!");
        }

        private void btnDodajDodatnuUslugu_Click(object sender, RoutedEventArgs e)
        {
            var izabranaStavka = (DodatneUsluge)dgDodatneUsluge.SelectedItem;

            if (tbKolicinaDodatne.Text == "")
            {
                MessageBox.Show("Nije izabrana dodatna usluga za prodaju i/ili nije uneta kolicina!", "Greska", MessageBoxButton.OK);
                return;
            }

            if (izabranaStavka != null)
            {
                try
                {
                    int.Parse(tbKolicinaDodatne.Text);
                }
                catch
                {
                    MessageBox.Show("Kolicina mora biti ceo broj!", "Greska", MessageBoxButton.OK);
                    return;
                }

                if (tbKolicinaDodatne.Text == "")
                {
                    MessageBox.Show("Kolicina mora biti uneta!", "Greska", MessageBoxButton.OK);
                    return;
                }

                int unetaKolicina = int.Parse(tbKolicinaDodatne.Text);
                //provera unosa kolicine
                if (unetaKolicina <= 0)
                {
                    MessageBox.Show("Uneta kolicina mora biti veca od 0!");
                    return;
                }

                //ako je vec prodata dodatna usluga update joj kolicinu
                foreach (var stavkaDodatna in Projekat.Instanca.StavkaRacunaDodatnaUsluga.ToList()) //proveram da li postoji ista usluga prodata i ako postoji update za nju broj prodatih komada
                {
                    if (stavkaDodatna.Obrisan == false && stavkaDodatna.IdProdajeNamestaja == prodajaNamestaja.Id && stavkaDodatna.IdDodatneUsluge == izabranaStavka.Id)
                    {
                        ukupnaCena -= (cenaBezPdv * decimal.ToDouble(prodajaNamestaja.Pdv)) + cenaBezPdv; //umanjujem cenu za postojece prodate dodatne usluge
                        cenaBezPdv -= stavkaDodatna.Kolicina * izabranaStavka.Iznos; //umanjujem cenu za postojece prodate dodatne usluge kako bi racun bio na "nuli" za taj proizvod


                        prodateDodatne.Remove(izabranaStavka);
                        stavkaDodatna.Kolicina += unetaKolicina;
                        StavkaRacunaDodatnaUsluga.Update(stavkaDodatna); //update kolicinu za postojecu stavku 

                        izabranaStavka.ProdataKolicina += unetaKolicina; //update za prodatu kolicinu
                        prodateDodatne.Add(izabranaStavka);

                        cenaBezPdv += stavkaDodatna.Kolicina * izabranaStavka.Iznos; //cena za prikaz i racunam novu cenu sa novim brojem prodatih proizvoda

                        ukupnaCena = (cenaBezPdv * decimal.ToDouble(prodajaNamestaja.Pdv)) + cenaBezPdv; //cena za prikaz i racunam novu cenu sa novim brojem prodatih proizvoda

                        OsveziPrikazCene();

                        MessageBox.Show("Izabrana dodatna usluga je dodata na racun!");
                        return;
                    }
                }

                //nova stavka na racunu
                var stavka = new StavkaRacunaDodatnaUsluga()
                {
                    IdProdajeNamestaja = prodajaNamestaja.Id,
                    IdDodatneUsluge = izabranaStavka.Id,
                    Kolicina = unetaKolicina
                };
                StavkaRacunaDodatnaUsluga.Create(stavka);

                cenaBezPdv += stavka.Kolicina * izabranaStavka.Iznos; //cena za prikaz i racunam novu cenu sa novom stavkom racuna
                ukupnaCena = (cenaBezPdv * decimal.ToDouble(prodajaNamestaja.Pdv)) + cenaBezPdv; //cena za prikaz i racunam novu cenu sa novom stavkom racuna
                OsveziPrikazCene();

                izabranaStavka.ProdataKolicina = unetaKolicina; //update za prikaz prodate kolicine
                prodateDodatne.Add(izabranaStavka); //za prikaz prodatih stavki
            }
            MessageBox.Show("Izabrana dodatna usluga je dodata na racun!");

        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            //proverava da li je racun prazan
            bool postojiStavkaNaRacunu = false;
            foreach (var stavkaNamestaj in Projekat.Instanca.StavkaRacunaNamestaj)
            {
                if (stavkaNamestaj.Obrisan == false && stavkaNamestaj.IdProdajeNamestaja == prodajaNamestaja.Id)
                {
                    postojiStavkaNaRacunu = true;
                }
            }
            foreach (var stavkaDodatna in Projekat.Instanca.StavkaRacunaDodatnaUsluga)
            {
                if (stavkaDodatna.Obrisan == false && stavkaDodatna.IdProdajeNamestaja == prodajaNamestaja.Id)
                {
                    postojiStavkaNaRacunu = true;
                }
            }

            if (postojiStavkaNaRacunu == false)
            {
                MessageBox.Show("Racun je prazan. Ne mozete izdati prazan racun!", "Greska", MessageBoxButton.OK);
                return;
            }

            double ukupnaCenaBezPdv = 0;

            foreach (var stavkaNamestaj in Projekat.Instanca.StavkaRacunaNamestaj)
            {
                if (stavkaNamestaj.Obrisan == false && stavkaNamestaj.IdProdajeNamestaja == prodajaNamestaja.Id)
                {
                    foreach (var namestaj in Projekat.Instanca.Namestaj)
                    {
                        if (namestaj.Obrisan == false && namestaj.Id == stavkaNamestaj.IdNamestaja)
                        {
                            if (namestaj.AkcijskaCena != 0) //ako je akcijska cena razlicita od 0 za racun se koristi ta cena
                            {
                                ukupnaCenaBezPdv += namestaj.AkcijskaCena * stavkaNamestaj.Kolicina;
                            }
                            if (namestaj.AkcijskaCena == 0) //ako je akcijska cena 0 za cenu se uzima redovna cena
                            {
                                ukupnaCenaBezPdv += namestaj.Cena * stavkaNamestaj.Kolicina;
                            }
                        }
                    }
                }
            }

            foreach (var stavkaDodatnaUsluga in Projekat.Instanca.StavkaRacunaDodatnaUsluga)
            {
                if (stavkaDodatnaUsluga.Obrisan == false && stavkaDodatnaUsluga.IdProdajeNamestaja == prodajaNamestaja.Id)
                {
                    foreach (var dodatnaUsluga in Projekat.Instanca.DodatneUsluge)
                    {
                        if (dodatnaUsluga.Obrisan == false && dodatnaUsluga.Id == stavkaDodatnaUsluga.IdDodatneUsluge)
                        {
                            ukupnaCenaBezPdv += dodatnaUsluga.Iznos * stavkaDodatnaUsluga.Kolicina; //na racun se dodaje i cena za dodatnu uslugu
                        }
                    }
                }
            }

            var ukupnaCenaSaPdv = (ukupnaCenaBezPdv * decimal.ToDouble(prodajaNamestaja.Pdv)) + ukupnaCenaBezPdv;
            prodajaNamestaja.UkupnaCena = ukupnaCenaSaPdv;
            prodajaNamestaja.CenaBezPdv = ukupnaCenaBezPdv;
            prodajaNamestaja.Obrisan = false; //racun je napravljen i obrisan se postavlja na false
            ProdajaNamestaja.Update(prodajaNamestaja);
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            foreach (var stavkaNamestaj in Projekat.Instanca.StavkaRacunaNamestaj)
            {
                if (stavkaNamestaj.Obrisan == false && stavkaNamestaj.IdProdajeNamestaja == prodajaNamestaja.Id)
                {
                    foreach (var namestaj in Projekat.Instanca.Namestaj)
                    {
                        if (stavkaNamestaj.IdNamestaja == namestaj.Id)
                        {
                            namestaj.KolicinaUMagacinu += stavkaNamestaj.Kolicina; //prilikom dodavanja stavke azurira se kolicina namestaja i posto se
                            Namestaj.Update(namestaj);                             //ovde otkazuje pravljenje racuna mora se ponovo azurirati kolicina i vratiti na pocetno stanje jer stavka nije prodata

                            StavkaRacunaNamestaj.Delete(stavkaNamestaj); //brisem stavku racuna
                        }
                    }
                }
            }

            foreach (var stavkaDodatnaUsluga in Projekat.Instanca.StavkaRacunaDodatnaUsluga)
            {
                if (stavkaDodatnaUsluga.Obrisan == false && stavkaDodatnaUsluga.IdProdajeNamestaja == prodajaNamestaja.Id)
                {
                    StavkaRacunaDodatnaUsluga.Delete(stavkaDodatnaUsluga); //brisem stavku racuna
                }
            }
            Close();
        }


        private void btnIzbrisiNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var izabranNamestaj = (Namestaj)dgProdatNamestaj.SelectedItem;
            if(izabranNamestaj != null)
            {
                if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete stavku sa racuna: {izabranNamestaj.Naziv}", "Brisanje stavke sa racuna", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    foreach (var stavkaNamestaj in Projekat.Instanca.StavkaRacunaNamestaj)
                    {
                        if (stavkaNamestaj.Obrisan == false && stavkaNamestaj.IdProdajeNamestaja == prodajaNamestaja.Id && stavkaNamestaj.IdNamestaja == izabranNamestaj.Id)
                        {
                            StavkaRacunaNamestaj.Delete(stavkaNamestaj); //brisem stavku racuna

                            prodatNamestaj.Remove(izabranNamestaj); //brisem za prikaz

                            if (izabranNamestaj.AkcijskaCena == 0) //ako je akcijska cena 0 racunam po redovnoj ceni
                            {
                                cenaBezPdv -= stavkaNamestaj.Kolicina * izabranNamestaj.Cena; //cena za prikaz
                                ukupnaCena -= (izabranNamestaj.Cena * decimal.ToDouble(prodajaNamestaja.Pdv) + izabranNamestaj.Cena) * stavkaNamestaj.Kolicina; //racuna se nova cena za prikaz
                            }
                            if (izabranNamestaj.AkcijskaCena != 0) // ako je akcijska cena razlicita od 0 racunam po akcijskoj ceni
                            {
                                cenaBezPdv -= stavkaNamestaj.Kolicina * izabranNamestaj.AkcijskaCena; //cena za prikaz
                                ukupnaCena -= (izabranNamestaj.AkcijskaCena * decimal.ToDouble(prodajaNamestaja.Pdv) + izabranNamestaj.AkcijskaCena) * stavkaNamestaj.Kolicina; //racuna se nova cena za prikaz
                            }

                            izabranNamestaj.KolicinaUMagacinu += stavkaNamestaj.Kolicina;
                            Namestaj.Update(izabranNamestaj); //update kolicinu u magacinu
                            OsveziPrikazCene();
                            
                        }
                    }
                }
            }
        }

        private void btnIzbrisiDodatnu_Click(object sender, RoutedEventArgs e)
        {
            var izabranaDodatna = (DodatneUsluge)dgProdateDodatne.SelectedItem;
            if (izabranaDodatna != null)
            {
                if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete stavku sa racuna: {izabranaDodatna.Naziv}", "Brisanje stavke sa racuna", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    foreach (var stavkaDodatna in Projekat.Instanca.StavkaRacunaDodatnaUsluga)
                    {
                        if(stavkaDodatna.Obrisan == false && stavkaDodatna.IdProdajeNamestaja == prodajaNamestaja.Id && stavkaDodatna.IdDodatneUsluge == izabranaDodatna.Id)
                        {
                            StavkaRacunaDodatnaUsluga.Delete(stavkaDodatna); //brisem stavku racuna

                            prodateDodatne.Remove(izabranaDodatna); //brisem za prikaz

                            cenaBezPdv -= izabranaDodatna.Iznos * stavkaDodatna.Kolicina;
                            ukupnaCena -= (izabranaDodatna.Iznos * decimal.ToDouble(prodajaNamestaja.Pdv) + izabranaDodatna.Iznos) * stavkaDodatna.Kolicina;

                            OsveziPrikazCene();
                        }
                    }
                }
            }
        }

        private void OsveziPrikazCene()
        {
            tbCena.Clear();
            tbCena.Text = ukupnaCena.ToString();
            tbCenaBezPdv.Clear();
            tbCenaBezPdv.Text = cenaBezPdv.ToString();
        }

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Sifra" || e.Column.Header.ToString() == "TipNamestajaId" || e.Column.Header.ToString() == "AkcijaId" || e.Column.Header.ToString() == "Obrisan" || e.Column.Header.ToString() == "ProdataKolicina")
            {
                e.Cancel = true;
            }
        }

        private void dgDodatneUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Obrisan" || e.Column.Header.ToString() == "ProdataKolicina")
            {
                e.Cancel = true;
            }
        }

        private void dgProdateDodatne_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void dgProdatNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Sifra" || e.Column.Header.ToString() == "TipNamestajaId" || e.Column.Header.ToString() == "AkcijaId" || e.Column.Header.ToString() == "KolicinaUMagacinu" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }
    }
}
