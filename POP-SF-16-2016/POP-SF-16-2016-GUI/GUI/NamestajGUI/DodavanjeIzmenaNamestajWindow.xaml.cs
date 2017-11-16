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
    /// Interaction logic for DodavanjeIzmenaNamestajWindow.xaml
    /// </summary>
    public partial class DodavanjeIzmenaNamestajWindow : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }
        private Namestaj namestaj;
        private TipOperacije operacija;

        public DodavanjeIzmenaNamestajWindow(Namestaj namestaj, TipOperacije tipOperacije)
        {
            InitializeComponent();
            InicijalizujPodatke(namestaj, operacija);
        }

        private void InicijalizujPodatke(Namestaj namestaj, TipOperacije operacija)
        {
            this.namestaj = namestaj;
            this.operacija = operacija;

            tbNaziv.Text = namestaj.Naziv;
            tbCena.Text = namestaj.Cena.ToString();
            tbKolicina.Text = namestaj.KolicinaUMagacinu.ToString();
            tbSifra.Text = namestaj.Sifra;
            cbTipNamestaja.SelectedIndex = namestaj.TipNamestajaId;
 
        }

        private void cbTipNamestaja_Loaded(object sender, RoutedEventArgs e)
        {
            var ucitaniTipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
            foreach (var tipNamestaja in ucitaniTipoviNamestaja)
            {
                cbTipNamestaja.Items.Add(tipNamestaja.Naziv);
            }
            cbTipNamestaja.SelectedIndex = 0;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            switch (operacija)
            {
                case TipOperacije.DODAVANJE:
                    var noviNamestaj = new Namestaj
                    {
                        Id = ucitanNamestaj.Count + 1,
                        Naziv = tbNaziv.Text,
                        Cena = double.Parse(tbCena.Text),
                        KolicinaUMagacinu = int.Parse(tbKolicina.Text),
                        Sifra = tbSifra.Text,
                        TipNamestajaId = cbTipNamestaja.SelectedIndex
                    };
                    ucitanNamestaj.Add(noviNamestaj);
                    break;
                case TipOperacije.IZMENA:
                    var namestajZaIzmenu = Namestaj.PronadjiNamestajPoId(namestaj.Id);

                    namestajZaIzmenu.Naziv = tbNaziv.Text;
                    namestajZaIzmenu.Sifra = tbSifra.Text;
                    namestajZaIzmenu.KolicinaUMagacinu = int.Parse(tbKolicina.Text);
                    namestajZaIzmenu.Cena = double.Parse(tbCena.Text);
                    namestajZaIzmenu.TipNamestajaId = cbTipNamestaja.SelectedIndex;
                    
                    break;
                    // KAKO NAMESTITI DA NE DODAJE NOVI NAMESTAJ VEC DA IZMENI POSTOJECI
                default:
                    break;
            }
            Projekat.Instanca.Namestaj = ucitanNamestaj;
            Close();

        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
