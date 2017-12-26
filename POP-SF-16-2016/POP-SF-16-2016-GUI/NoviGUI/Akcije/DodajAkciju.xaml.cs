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
            tbNazivAkcije.DataContext = akcija;

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

            Akcija.Create(akcija);
            foreach (var a in Projekat.Instanca.Akcija)
            {
                if(a.Id == akcija.Id)
                {
                    Akcija.Delete(akcija); //postavljam akciju da bude obrisana u slucaju da se otkaze dodavanje nove akcije. Na sacuvaj se obrisan menja u false!
                }
            }
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

            akcija.Obrisan = false;
            foreach (var a in Projekat.Instanca.Akcija)
            {
                if(a.Id == akcija.Id)
                {
                    a.Obrisan = akcija.Obrisan;
                    a.NazivAkcije = akcija.NazivAkcije; //preuzima vrednost za naziv akcije
                }
            }
            Akcija.Update(akcija); //update za akciju da obrisan bude false i da uzme naziv akcije
            Close();

            foreach (var proizvodNaAkciji in Projekat.Instanca.NamestajNaAkciji)
            {
                if(proizvodNaAkciji.IdAkcije == akcija.Id)
                {
                    foreach (var namestaj in Projekat.Instanca.Namestaj)
                    {
                        if(namestaj.Id == proizvodNaAkciji.IdNamestaja)
                        {
                            double ukupnaCena = namestaj.Cena - (namestaj.Cena * (decimal.ToDouble(akcija.Popust) / 100));
                            namestaj.AkcijskaCena = Math.Round(ukupnaCena, 2);
                            Namestaj.Update(namestaj); //za svaki proizvod na akciji se update akcijska cena
                        }
                    }
                }
            }
            
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            foreach (var proizvodNaAkciji in Projekat.Instanca.NamestajNaAkciji)
            {
                if (proizvodNaAkciji.IdAkcije == akcija.Id)
                {
                    if (akcija.Obrisan == true)
                    {
                        proizvodNaAkciji.Obrisan = true;
                        NamestajNaAkciji.Update(proizvodNaAkciji); //ako je otkazano pravljenje akcije namestaju koji je dodat na akciju se obrisan postavlja na true
                    }
                }
            }
            Close();
        }

        public void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            var izabranaStavka = (Namestaj)dgNamestaj.SelectedItem;
            if (izabranaStavka != null)
            {
                foreach (var namestajNaAkciji in Projekat.Instanca.NamestajNaAkciji) //provera da li je namestaj vec dodat na istu akciju ili je na nekoj drugoj akciji
                {
                    if (namestajNaAkciji.IdAkcije == akcija.Id)
                    {
                        if(namestajNaAkciji.IdNamestaja == izabranaStavka.Id)
                        {
                            MessageBox.Show("Izabrani namestaj je vec dodat na akciju!", "Greska", MessageBoxButton.OK);
                            return;
                        }
                    }

                    if (namestajNaAkciji.IdNamestaja == izabranaStavka.Id && namestajNaAkciji.Obrisan == false)
                    {
                        MessageBox.Show("Izabrani namestaj je na nekoj drugoj akciji!", "Greska", MessageBoxButton.OK);
                        return;
                    }

                }

                var noviNamestajNaAkciji = new NamestajNaAkciji()
                {
                    IdAkcije = akcija.Id,
                    IdNamestaja = izabranaStavka.Id
                };
                NamestajNaAkciji.Create(noviNamestajNaAkciji);  //dodavanje namestaja na akciju
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
