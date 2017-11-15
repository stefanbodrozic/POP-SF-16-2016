using POP_SF_16_2016_GUI.Model;
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

namespace POP_SF_16_2016_GUI.GUI
{
    /// <summary>
    /// Interaction logic for NamestajWindow.xaml
    /// </summary>
    public partial class NamestajWindow : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }

        private Namestaj namestaj;
        private TipOperacije operacija;
        

        public NamestajWindow(Namestaj namestaj, TipOperacije operacija)
        {
            InitializeComponent();

            InicijalizujPodatke(namestaj, operacija);
        }

        private void InicijalizujPodatke(Namestaj namestaj, TipOperacije operacija)
        {
            this.namestaj = namestaj;
            this.operacija = operacija;

            tbNaziv.Text = namestaj.Naziv;
            
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var listaNamestaja = Projekat.Instanca.Namestaj;
            switch (operacija)
            {
                case TipOperacije.DODAVANJE:
                    
                    //zavrsiti za sve ostalo!

                    var noviNamestaj = new Namestaj()
                    {
                        Id = listaNamestaja.Count + 1,
                        Naziv = tbNaziv.Text
                    };
                    listaNamestaja.Add(noviNamestaj);
                    break;
                case TipOperacije.IZMENA:

                    var namestajZaIzmenu = listaNamestaja.SingleOrDefault(x => x.Id == namestaj.Id);
                    //var namestajZaIzmenu = Namestaj.PronadjiNamestajPoId(namestaj.Id);
                    namestajZaIzmenu.Naziv = tbNaziv.Text;
                    break;
                default:
                    break;
            }

            Projekat.Instanca.Namestaj = listaNamestaja;
            Close();
        }
    }
}
