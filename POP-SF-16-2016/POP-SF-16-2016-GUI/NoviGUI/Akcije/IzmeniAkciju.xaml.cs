using POP_SF_16_2016_GUI.Model;
using POP_SF_16_2016_GUI.Utils;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for IzmeniAkciju.xaml
    /// </summary>
    public partial class IzmeniAkciju : Window
    {
        private Akcija akcija;

        public IzmeniAkciju(Akcija akcija)
        {
            InitializeComponent();
            this.akcija = akcija;

            dpPocetakAkcije.DataContext = akcija;
            dpZavrsetakAkcije.DataContext = akcija;
            tbPopust.DataContext = akcija;
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

            foreach (var a in Projekat.Instanca.Akcija)
            {
                if(a.Id == akcija.Id)
                {
                    a.DatumPocetka = akcija.DatumPocetka;
                    a.DatumZavrsetka = akcija.DatumZavrsetka;
                    a.Popust = akcija.Popust;
                    Akcija.Update(akcija);
                }
                foreach (var namestajAkcija in Projekat.Instanca.NamestajNaAkciji)
                {
                    if(namestajAkcija.IdAkcije == a.Id && namestajAkcija.Obrisan == false)
                    {
                        foreach (var namestaj in Projekat.Instanca.Namestaj)
                        {
                            if(namestajAkcija.IdNamestaja == namestaj.Id)
                            {
                                double ukupnaCena = namestaj.Cena - (namestaj.Cena * (decimal.ToDouble(akcija.Popust) / 100));
                                namestaj.AkcijskaCena = Math.Round(ukupnaCena, 2);
                                Namestaj.Update(namestaj); //ako se izmeni popust izmenice se i akcijska cena namestaja
                            }
                        }
                    }
                }
            }
            
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNamestajNaAkciji_Click(object sender, RoutedEventArgs e)
        {
            var prikazProizvodaNaAkciji = new PrikazProizvodaNaAkciji(akcija);
            prikazProizvodaNaAkciji.ShowDialog();
            
        }
    }
}
