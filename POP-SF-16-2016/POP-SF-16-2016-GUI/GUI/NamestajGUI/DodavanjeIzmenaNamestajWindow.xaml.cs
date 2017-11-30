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
        private TipOperacije tipOperacije;

        public DodavanjeIzmenaNamestajWindow(Namestaj namestaj, TipOperacije tipOperacije)
        {
            InitializeComponent();
            InicijalizujPodatke(namestaj, tipOperacije);
        }

        private void InicijalizujPodatke(Namestaj namestaj, TipOperacije operacija)
        {
            this.namestaj = namestaj;
            this.tipOperacije = operacija;

            tbNaziv.Text = namestaj.Naziv;
            tbCena.Text = namestaj.Cena.ToString();
            tbKolicina.Text = namestaj.KolicinaUMagacinu.ToString();
            tbSifra.Text = namestaj.Sifra;
            foreach (var tipNamestaja in Projekat.Instanca.TipoviNamestaja)
            {
                cbTipNamestaja.Items.Add(tipNamestaja);
            }

            //postavljanje postojeceg tipa namestaja u combobox prilikom izmene
            foreach (TipNamestaja tipNamestaja in cbTipNamestaja.Items)
            {
                if (tipNamestaja.Id == namestaj.Id)
                {
                    cbTipNamestaja.SelectedItem = tipNamestaja;
                    break;
                }
            }


        }

        //sa vezbi
        private void InicijalizujPodatkeComboBox(Namestaj namestaj, TipOperacije operacija)
        {
            this.namestaj = namestaj;
            this.tipOperacije = operacija;
            tbNaziv.Text = namestaj.Naziv;

            foreach (var tipNamestaja in Projekat.Instanca.TipoviNamestaja)
            {
                cbTipNamestaja.Items.Add(tipNamestaja);
            } 
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var ucitanNamestaj = Projekat.Instanca.Namestaj;
            var izabraniTipNamestaja = (TipNamestaja)cbTipNamestaja.SelectedItem;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    var noviNamestaj = new Namestaj
                    {
                        Id = ucitanNamestaj.Count + 1,
                        Naziv = tbNaziv.Text,
                        Cena = double.Parse(tbCena.Text),
                        KolicinaUMagacinu = int.Parse(tbKolicina.Text),
                        Sifra = tbSifra.Text,
                        TipNamestajaId = izabraniTipNamestaja.Id 

                    };
                    ucitanNamestaj.Add(noviNamestaj);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var n in ucitanNamestaj)
                    {
                        if(n.Id == namestaj.Id)
                        {
                            n.Naziv = this.tbNaziv.Text;
                            n.Cena = double.Parse(this.tbCena.Text);
                            n.KolicinaUMagacinu = int.Parse(this.tbKolicina.Text);
                            n.Sifra = this.tbSifra.Text;
                            n.TipNamestajaId = izabraniTipNamestaja.Id;
                            break;
                        }
                    }
                    break;                 
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
