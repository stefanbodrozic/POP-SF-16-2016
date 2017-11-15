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
    /// Interaction logic for RegistracijaWindow.xaml
    /// </summary>
    public partial class RegistracijaWindow : Window
    {
        public RegistracijaWindow()
        {
            InitializeComponent();
        }

        private void cbTipKorisnika_Loaded(object sender, RoutedEventArgs e)
        {
            Korisnik k = new Korisnik();
            List<TipKorisnika> tipoviKorisnika = new List<TipKorisnika>();
            foreach (var tip in Enum.GetValues(typeof(TipKorisnika)))
            {
                cbTipKorisnika.Items.Add(tip);
            }

            cbTipKorisnika.SelectedIndex = 1;
            
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRegistrujKorisnika_Click(object sender, RoutedEventArgs e)
        {
            var ucitaniKorisnici = Projekat.Instanca.Korisnik;
            string ime = tbIme.Text;
            string prezime = tbPrezime.Text;
            string korisnickoIme = tbKorisnickoIme.Text;
            string lozinka = pbLozinka.Password;
            TipKorisnika tipKorisnika = (TipKorisnika)cbTipKorisnika.SelectedItem;

            if (String.IsNullOrWhiteSpace(ime) || String.IsNullOrWhiteSpace(prezime) || String.IsNullOrWhiteSpace(korisnickoIme) || String.IsNullOrWhiteSpace(lozinka))
            {
                MessageBox.Show("Niste uneli podatke!", "Greska", MessageBoxButton.OK);
                return;
            }

            var noviKorisnik = new Korisnik()
            {
                Id = ucitaniKorisnici.Count + 1,
                Ime = ime,
                Prezime = prezime,
                KorisnickoIme = korisnickoIme,
                Lozinka = lozinka,
                TipKorisnika = tipKorisnika
            };
            ucitaniKorisnici.Add(noviKorisnik);

            Projekat.Instanca.Korisnik = ucitaniKorisnici;
            MessageBox.Show("Korisnik uspesno registrovan", "Potvrda", MessageBoxButton.OK);
            this.Close();
        }
    }
}
