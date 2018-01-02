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

        public DodajProdajuNamestaja(ProdajaNamestaja prodajaNamestaja)
        {
            InitializeComponent();
            this.prodajaNamestaja = prodajaNamestaja;
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
            tbKupac.DataContext = prodajaNamestaja;

            ProdajaNamestaja.Create(prodajaNamestaja); //pravim novu prodaju da bih mogao da koristim njen ID
            ProdajaNamestaja.Delete(prodajaNamestaja); //postavljam da bude obrisana kako se ne bi prikazivala u slucaju da bude otkazano pravljenje racuna
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

                //nova stavka na racunu
                var stavka = new StavkaRacunaNamestaj()
                {
                    IdProdajeNamestaja = prodajaNamestaja.Id,
                    IdNamestaja = izabranaStavka.Id,
                    Kolicina = unetaKolicina
                };
                StavkaRacunaNamestaj.Create(stavka);

                //update za kolicinu kada se proda namestaj
                foreach (var namestaj in Projekat.Instanca.Namestaj)
                {
                    if (namestaj.Id == izabranaStavka.Id)
                    {
                        namestaj.KolicinaUMagacinu -= stavka.Kolicina;
                        Namestaj.Update(namestaj);
                    }
                }
            }
            MessageBox.Show("Izabrani namestaj je dodat na racun!");
        }

        private void btnDodajDodatnuUslugu_Click(object sender, RoutedEventArgs e)
        {
            var izabranaStavka = (DodatneUsluge)dgDodatneUsluge.SelectedItem;

            if (tbKolicina.Text == "")
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

                //nova stavka na racunu
                var stavka = new StavkaRacunaDodatnaUsluga()
                {
                    IdProdajeNamestaja = prodajaNamestaja.Id,
                    IdDodatneUsluge = izabranaStavka.Id,
                    Kolicina = unetaKolicina
                };
                StavkaRacunaDodatnaUsluga.Create(stavka);   
            }
            MessageBox.Show("Izabrana dodatna usluga je dodata na racun!");

        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            // proveriti da li nesto postoji na racunu i ako ne postoji onemoguciti potvrdu


            double ukupnaCenaBezPdv = 0;

            foreach (var stavkaNamestaj in Projekat.Instanca.StavkaRacunaNamestaj)
            {
                if (stavkaNamestaj.IdProdajeNamestaja == prodajaNamestaja.Id)
                {
                    foreach (var namestaj in Projekat.Instanca.Namestaj)
                    {
                        if (namestaj.Id == stavkaNamestaj.IdNamestaja)
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
                if (stavkaDodatnaUsluga.IdProdajeNamestaja == prodajaNamestaja.Id)
                {
                    foreach (var dodatnaUsluga in Projekat.Instanca.DodatneUsluge)
                    {
                        if (dodatnaUsluga.Id == stavkaDodatnaUsluga.IdDodatneUsluge)
                        {
                            ukupnaCenaBezPdv += dodatnaUsluga.Iznos * stavkaDodatnaUsluga.Kolicina; //na racun se dodaje i cena za dodatnu uslugu
                        }
                    }
                }
            }
            
            var ukupnaCenaSaPdv = (ukupnaCenaBezPdv * double.Parse(prodajaNamestaja.Pdv.ToString())) + ukupnaCenaBezPdv;
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
                if(stavkaNamestaj.IdProdajeNamestaja == prodajaNamestaja.Id)
                {
                    foreach (var namestaj in Projekat.Instanca.Namestaj)
                    {
                        if(stavkaNamestaj.IdNamestaja == namestaj.Id)
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
                if (stavkaDodatnaUsluga.IdProdajeNamestaja == prodajaNamestaja.Id)
                {
                    StavkaRacunaDodatnaUsluga.Delete(stavkaDodatnaUsluga); //brisem stavku racuna
                }
            }
            Close();
        }

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Sifra" || e.Column.Header.ToString() == "TipNamestajaId" || e.Column.Header.ToString() == "AkcijaId" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void dgDodatneUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }
    }
}
