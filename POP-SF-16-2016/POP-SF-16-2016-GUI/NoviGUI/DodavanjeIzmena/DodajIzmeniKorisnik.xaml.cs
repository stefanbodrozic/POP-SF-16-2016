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

            this.korisnik = korisnik;
            this.tipOperacije = tipOperacije;
            
            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            tbLozinka.DataContext = korisnik;
            //dodavanje tipa korisnika u combobox
            var tipoviKorisnika = new List<TipKorisnika>();
            tipoviKorisnika.Add(TipKorisnika.Administrator);
            tipoviKorisnika.Add(TipKorisnika.Prodavac);
            cbTipKorisnika.ItemsSource = tipoviKorisnika;
            cbTipKorisnika.DataContext = korisnik;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (TestValidacije() == true)
            {
                return;
            }

            var ucitaniKorisnici = Projekat.Instanca.Korisnik;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    Korisnik.Create(korisnik);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var k in ucitaniKorisnici)
                    {
                        if(k.Id == korisnik.Id)
                        {
                            k.Ime = korisnik.Ime;
                            k.Prezime = korisnik.Prezime;
                            k.KorisnickoIme = korisnik.KorisnickoIme;
                            k.Lozinka = korisnik.Lozinka;
                            k.TipKorisnika = korisnik.TipKorisnika;
                            break;
                        }
                    }
                    Korisnik.Update(korisnik);
                    break;
                default:
                    break;
            }
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool TestValidacije()
        {
            BindingExpression bindEx1 = tbIme.GetBindingExpression(TextBox.TextProperty);
            bindEx1.UpdateSource();
            BindingExpression bindEx2 = tbPrezime.GetBindingExpression(TextBox.TextProperty);
            bindEx2.UpdateSource();
            BindingExpression bindEx3 = tbKorisnickoIme.GetBindingExpression(TextBox.TextProperty);
            bindEx3.UpdateSource();
            BindingExpression bindEx4 = tbLozinka.GetBindingExpression(TextBox.TextProperty);
            bindEx4.UpdateSource();

            if (Validation.GetHasError(tbIme) == true || Validation.GetHasError(tbPrezime) == true || Validation.GetHasError(tbKorisnickoIme) == true || Validation.GetHasError(tbLozinka) == true)
            {
                return true;
            }
            return false;
        }
    }
}
