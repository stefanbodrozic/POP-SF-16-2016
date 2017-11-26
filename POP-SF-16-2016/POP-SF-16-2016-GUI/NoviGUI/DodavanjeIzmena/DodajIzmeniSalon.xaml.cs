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
            InicijalizujPodatke(salon, tipOperacije);
        }

        private void InicijalizujPodatke(Salon salon, TipOperacije tipOperacije)
        {
            this.salon = salon;
            this.tipOperacije = tipOperacije;

            tbNaziv.Text = salon.Naziv;
            tbAdresa.Text = salon.Adresa;
            tbTelefon.Text = salon.Telefon;
            tbEmail.Text = salon.Email;
            tbWebsajt.Text = salon.Websajt;
            tbPib.Text = (salon.Pib).ToString();
            tbMaticniBroj.Text = (salon.MaticniBroj).ToString();
            tbBrojZiroRacuna.Text = salon.BrojZiroRacuna;
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
                    var noviSalon = new Salon
                    {
                        Id = ucitaniSaloni.Count + 1,
                        Naziv = tbNaziv.Text,
                        Adresa = tbAdresa.Text,
                        Telefon = tbTelefon.Text,
                        Email = tbEmail.Text,
                        Websajt = tbWebsajt.Text,
                        Pib = int.Parse(tbPib.Text),
                        MaticniBroj = int.Parse(tbMaticniBroj.Text),
                        BrojZiroRacuna = tbBrojZiroRacuna.Text
                    };
                    ucitaniSaloni.Add(noviSalon);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var trazeniSalon in ucitaniSaloni)
                    {
                        if (trazeniSalon.Id == salon.Id)
                        {
                            trazeniSalon.Naziv = tbNaziv.Text;
                            trazeniSalon.Adresa = tbAdresa.Text;
                            trazeniSalon.Telefon = tbTelefon.Text;
                            trazeniSalon.Email = tbEmail.Text;
                            trazeniSalon.Websajt = tbWebsajt.Text;
                            trazeniSalon.Pib = int.Parse(tbPib.Text);
                            trazeniSalon.MaticniBroj = int.Parse(tbMaticniBroj.Text);
                            trazeniSalon.BrojZiroRacuna = tbBrojZiroRacuna.Text;
                        }
                    }
                    break;
                default:
                    break;
            }
            Projekat.Instanca.Salon = ucitaniSaloni;
            Close();
        }
    }
}
