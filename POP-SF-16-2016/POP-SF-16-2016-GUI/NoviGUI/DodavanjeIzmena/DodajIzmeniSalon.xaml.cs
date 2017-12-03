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
            var ucitaniSaloni = Projekat.Instanca.Salon;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    salon.Id = ucitaniSaloni.Count;
                    ucitaniSaloni.Add(salon);
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
                    break;
                default:
                    break;
            }
            GenericSerializer.Serialize("salon.xml", ucitaniSaloni);
            Close();
        }
    }
}
