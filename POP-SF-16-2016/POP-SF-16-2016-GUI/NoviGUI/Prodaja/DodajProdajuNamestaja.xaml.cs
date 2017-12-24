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
                if(namestaj.Obrisan != true && namestaj.KolicinaUMagacinu > 0)
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
                if(usluga.Obrisan != true)
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
        }

        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var ucitaneProdajeNamestaja = Projekat.Instanca.ProdajaNamestaja;
            var ucitaneStavkeNaRacunu = Projekat.Instanca.StavkaRacuna;
            var izarbanaStavka = (Namestaj)dgNamestaj.SelectedItem;

            if (tbKolicina.Text == "")
            {
                MessageBox.Show("Nije izabran namestaj za prodaju i/ili nije uneta kolicina!", "Greska", MessageBoxButton.OK);
                return;
            }

            if (izarbanaStavka != null)
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
                if (unetaKolicina > izarbanaStavka.KolicinaUMagacinu)
                {
                    MessageBox.Show("Uneta kolicina nema na stanju!");
                    return;
                }


                var novaStavkaNaRacunu = new StavkaRacuna()
                {
                    IdStavkeRacuna = ucitaneStavkeNaRacunu.Count(),
                    IdNamestaja = izarbanaStavka.Id,
                    KolicinaNamestaja = unetaKolicina
                };
                ucitaneStavkeNaRacunu.Add(novaStavkaNaRacunu);
                prodajaNamestaja.StavkaNaRacunu.Add(novaStavkaNaRacunu.IdStavkeRacuna);

                //update za kolicinu u magacinu kada se proda namestaj
                foreach (var namestaj in Projekat.Instanca.Namestaj)
                {
                    if(izarbanaStavka.Id == namestaj.Id)
                    {
                        namestaj.KolicinaUMagacinu -= novaStavkaNaRacunu.KolicinaNamestaja;
                        GenericSerializer.Serialize("namestaj.xml", Projekat.Instanca.Namestaj);
                    }
                }
            }
            GenericSerializer.Serialize("stavke_racuna.xml", ucitaneStavkeNaRacunu);
            MessageBox.Show("Izabrani namestaj je dodat na racun!");
        }

        private void btnDodajDodatnuUslugu_Click(object sender, RoutedEventArgs e)
        {
            var ucitaneStavkeNaRacunu = Projekat.Instanca.StavkaRacuna;
            var izabranaStavka = (DodatneUsluge)dgDodatneUsluge.SelectedItem;
            if (izabranaStavka != null)
            {
                var novaStavkaNaRacunu = new StavkaRacuna()
                {
                    IdStavkeRacuna = ucitaneStavkeNaRacunu.Count(),
                    IdDodatneUsluge = izabranaStavka.Id,
                    KolicinaNamestaja = 1
                };
                ucitaneStavkeNaRacunu.Add(novaStavkaNaRacunu);
                prodajaNamestaja.StavkaNaRacunu.Add(novaStavkaNaRacunu.IdStavkeRacuna);
            }
            
            
            GenericSerializer.Serialize("stavke_racuna.xml", ucitaneStavkeNaRacunu);
            MessageBox.Show("Izabrana dodatna usluga je dodata na racun!");
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            var ucitaneProdajeNamestaja = Projekat.Instanca.ProdajaNamestaja;
            prodajaNamestaja.Id = ucitaneProdajeNamestaja.Count;
            prodajaNamestaja.BrojRacuna = "r" + ucitaneProdajeNamestaja.Count;
            prodajaNamestaja.DatumProdaje = DateTime.Now;
            prodajaNamestaja.Kupac = tbKupac.Text;

            ucitaneProdajeNamestaja.Add(prodajaNamestaja);

            double cena = 0;
            foreach (var stavkaId in prodajaNamestaja.StavkaNaRacunu)
            {
                //var stavka = Projekat.Instanca.StavkaRacuna.SingleOrDefault(x => x.IdStavkeRacuna == stavkaId);
                foreach (var stavka in Projekat.Instanca.StavkaRacuna)
                {
                    if (stavka.IdStavkeRacuna == stavkaId)
                    {
                        foreach (var namestaj in Projekat.Instanca.Namestaj)
                        {
                            if(stavka.IdNamestaja == namestaj.Id)
                            {
                                cena += namestaj.Cena * stavka.KolicinaNamestaja;
                            }
                        }
                        foreach (var dodatnaUsluga in Projekat.Instanca.DodatneUsluge)
                        {
                            if(stavka.IdDodatneUsluge == dodatnaUsluga.Id)
                            {
                                cena += dodatnaUsluga.Iznos;
                            }
                        }
                    }
                }
            }
            prodajaNamestaja.UkupnaCena = cena;

            GenericSerializer.Serialize("prodaja_namestaja.xml", ucitaneProdajeNamestaja);
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
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
            if(e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Obrisan")
            {
                e.Cancel = true;
            }
        }
    }
}
