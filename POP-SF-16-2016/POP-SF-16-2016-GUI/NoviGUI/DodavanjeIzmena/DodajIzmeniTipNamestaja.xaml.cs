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

            this.tipNamestaja = tipNamestaja;
            this.tipOperacije = tipOperacije;

            tbNaziv.DataContext = tipNamestaja;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var ucitaniTipoviNamestaja = Projekat.Instanca.TipoviNamestaja;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    TipNamestaja.Create(tipNamestaja);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var tip in ucitaniTipoviNamestaja)
                    {
                        if(tip.Id == tipNamestaja.Id)
                        {
                            tip.Naziv = tipNamestaja.Naziv;
                            break;
                        }
                    }
                    TipNamestaja.Update(tipNamestaja);
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
    }
}
