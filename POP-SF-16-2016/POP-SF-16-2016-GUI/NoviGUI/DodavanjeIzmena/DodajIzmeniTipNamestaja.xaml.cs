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
    /// Interaction logic for DodajIzmeniTipNamestaja.xaml
    /// </summary>
    public partial class DodajIzmeniTipNamestaja : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }
        private TipNamestaja tipNamestaja;
        private TipOperacije tipOperacije;

        public DodajIzmeniTipNamestaja(TipNamestaja tipNamestaja, TipOperacije tipOperacije)
        {
            InitializeComponent();
            InicijalizujPodatke(tipNamestaja, tipOperacije);
        }

        public void InicijalizujPodatke(TipNamestaja tipNamestaja, TipOperacije tipOperacije)
        {
            this.tipNamestaja = tipNamestaja;
            this.tipOperacije = tipOperacije;
            
            tbNaziv.Text = tipNamestaja.Naziv;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var ucitaniTipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    var noviTipNamestaja = new TipNamestaja
                    {
                        Id = ucitaniTipoviNamestaja.Count + 1,
                        Naziv = tbNaziv.Text
                    };
                    ucitaniTipoviNamestaja.Add(noviTipNamestaja);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var trazeniTipNamestaja in ucitaniTipoviNamestaja)
                    {
                        if(trazeniTipNamestaja.Id == tipNamestaja.Id)
                        {
                            trazeniTipNamestaja.Naziv = tbNaziv.Text;
                        }
                    }
                    break;
                default:
                    break;
            }
            Projekat.Instanca.TipoviNamestaja = ucitaniTipoviNamestaja;
            Close();

        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
