using POP_SF_16_2016_GUI.Model;
using POP_SF_16_2016_GUI.Utils;
using System;
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
    /// Interaction logic for DodavanjeIzmenaSalon.xaml
    /// </summary>
    public partial class DodajIzmeniSalon : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }

        private Salon salon;
        private TipOperacije tipOperacije;

        public DodajIzmeniSalon(Salon salon, TipOperacije tipOperacije)
        {
            InitializeComponent();
        
            this.salon = salon;
            this.tipOperacije = tipOperacije;

            tbNaziv.DataContext = salon;
            tbAdresa.DataContext = salon;
            tbTelefon.DataContext = salon;
            tbEmail.DataContext = salon;
            tbWebsajt.DataContext = salon;
            tbPib.DataContext = salon;
            tbMaticniBroj.DataContext = salon;
            tbBrojZiroRacuna.DataContext = salon;
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (TestValidacije() == true)
            {
                return;
            }

            var ucitaniSaloni = Projekat.Instanca.Salon;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    Salon.Create(salon);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var s in ucitaniSaloni)
                    {
                        if(s.Id == salon.Id)
                        {
                            s.Naziv = salon.Naziv;
                            s.Adresa = salon.Adresa;
                            s.BrojZiroRacuna = salon.BrojZiroRacuna;
                            s.Email = salon.Email;
                            s.MaticniBroj = salon.MaticniBroj;
                            s.Pib = salon.Pib;
                            s.Telefon = salon.Telefon;
                            s.Websajt = salon.Websajt;
                            break;
                        }
                    }
                    Salon.Update(salon);
                    break;
                default:
                    break;
            }
            Close();
        }

        private bool TestValidacije()
        {
            BindingExpression bindEx1 = tbNaziv.GetBindingExpression(TextBox.TextProperty);
            bindEx1.UpdateSource();
            BindingExpression bindEx2 = tbAdresa.GetBindingExpression(TextBox.TextProperty);
            bindEx2.UpdateSource();
            BindingExpression bindEx3 = tbBrojZiroRacuna.GetBindingExpression(TextBox.TextProperty);
            bindEx3.UpdateSource();
            BindingExpression bindEx4 = tbEmail.GetBindingExpression(TextBox.TextProperty);
            bindEx4.UpdateSource();
            BindingExpression bindEx5 = tbMaticniBroj.GetBindingExpression(TextBox.TextProperty);
            bindEx5.UpdateSource();
            BindingExpression bindEx6 = tbPib.GetBindingExpression(TextBox.TextProperty);
            bindEx6.UpdateSource();
            BindingExpression bindEx7 = tbTelefon.GetBindingExpression(TextBox.TextProperty);
            bindEx7.UpdateSource();
            BindingExpression bindEx8 = tbWebsajt.GetBindingExpression(TextBox.TextProperty);
            bindEx8.UpdateSource();

            if (Validation.GetHasError(tbNaziv) == true || Validation.GetHasError(tbAdresa) == true || Validation.GetHasError(tbBrojZiroRacuna) == true || Validation.GetHasError(tbEmail) == true || Validation.GetHasError(tbMaticniBroj) == true || Validation.GetHasError(tbPib) == true || Validation.GetHasError(tbTelefon) == true || Validation.GetHasError(tbWebsajt) == true)
            {
                return true;
            }
            return false;
        }
    }
}
