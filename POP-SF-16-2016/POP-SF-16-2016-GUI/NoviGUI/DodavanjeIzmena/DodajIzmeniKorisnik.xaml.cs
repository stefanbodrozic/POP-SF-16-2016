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

namespace POP_SF_16_2016_GUI.NoviGUI
{
    /// <summary>
    /// Interaction logic for DodajIzmeniKorisnik.xaml
    /// </summary>
    public partial class DodajIzmeniKorisnik : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }

        private Korisnik korisnik;
        private TipOperacije tipOperacije;

        public DodajIzmeniKorisnik(Korisnik korisnik, TipOperacije tipOperacije)
        {
            InitializeComponent();
            InicijalizujPodatke(korisnik, tipOperacije);
        }
        
        private void InicijalizujPodatke(Korisnik korisnik, TipOperacije tipOperacije)
        {
            this.korisnik = korisnik;
            this.tipOperacije = tipOperacije;
            
            tbIme.Text = korisnik.Ime;
            tbPrezime.Text = korisnik.Prezime;
            tbKorisnickoIme.Text = korisnik.KorisnickoIme;
            pbLozinka.Password = korisnik.Lozinka;
            //dodavanje tipa korisnika u combobox
            foreach (var tipKorisnika in Enum.GetValues(typeof(TipKorisnika)))
            {
                cbTipKorisnika.Items.Add(tipKorisnika);
            }

            //postavljanje tipa korisnika
            foreach (TipKorisnika tipKorisnika in cbTipKorisnika.Items)
            {
                if(tipKorisnika == korisnik.TipKorisnika)
                {
                    cbTipKorisnika.SelectedItem = tipKorisnika;
                    break;
                }
            }

        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var ucitaniKorisnici = Projekat.Instanca.Korisnik;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    var noviKorisnik = new Korisnik
                    {
                        Id = ucitaniKorisnici.Count + 1,
                        Ime = tbIme.Text,
                        Prezime = tbPrezime.Text,
                        KorisnickoIme = tbKorisnickoIme.Text,
                        Lozinka = pbLozinka.Password,
                        TipKorisnika = (TipKorisnika)cbTipKorisnika.SelectedItem
                    };
                    ucitaniKorisnici.Add(noviKorisnik);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var trazeniKorisnik in ucitaniKorisnici)
                    {
                        if(trazeniKorisnik.Id == korisnik.Id)
                        {
                            trazeniKorisnik.Ime = tbIme.Text;
                            trazeniKorisnik.Prezime = tbPrezime.Text;
                            trazeniKorisnik.KorisnickoIme = tbKorisnickoIme.Text;
                            trazeniKorisnik.Lozinka = pbLozinka.Password;
                            trazeniKorisnik.TipKorisnika = (TipKorisnika)cbTipKorisnika.SelectedItem;
                        }
                    }
                    break;
                default:
                    break;
            }
            Projekat.Instanca.Korisnik = ucitaniKorisnici;
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
