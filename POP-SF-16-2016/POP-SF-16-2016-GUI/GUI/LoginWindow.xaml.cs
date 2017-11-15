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
    /// Interaction logic for LoginGUI.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnPrijava_Click(object sender, RoutedEventArgs e)
        {
            string korisnickoIme = tbKorisnickoIme.Text;
            string lozinka = pbLozinka.Password;
            var ucitaniKorisnici = Projekat.Instanca.Korisnik;
            foreach (Korisnik korisnik in ucitaniKorisnici)
            {
                if (korisnik.Obrisan != true && korisnik.KorisnickoIme == korisnickoIme && korisnik.Lozinka == lozinka)
                {
                    var tipKorisnika = korisnik.TipKorisnika;
                    switch (tipKorisnika)
                    {
                        case TipKorisnika.Administrator:
                            var administrator = new AdministratorWindow();
                            this.Close();
                            administrator.ShowDialog();
                            return;
                        case TipKorisnika.Prodavac:
                            var prodavac = new ProdavacWindow();
                            this.Close();
                            prodavac.ShowDialog();
                            return;
                    }
                }
            }
            MessageBox.Show("Pogresni podaci za prijavu!", "Greska", MessageBoxButton.OK);
            return;


        }

        private void btnRegistracija_Click(object sender, RoutedEventArgs e)
        {
            var registracija = new RegistracijaWindow();
            this.Close();
            registracija.ShowDialog();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
