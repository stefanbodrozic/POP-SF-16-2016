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
    /// Interaction logic for DodajIzmeniDodatneUsluge.xaml
    /// </summary>
    public partial class DodajIzmeniDodatneUsluge : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }

        private DodatneUsluge dodatneUsluge;
        private TipOperacije tipOperacije;

        public DodajIzmeniDodatneUsluge(DodatneUsluge dodatneUsluge, TipOperacije tipOperacije)
        {
            InitializeComponent();

            this.dodatneUsluge = dodatneUsluge;
            this.tipOperacije = tipOperacije;

            tbNaziv.DataContext = dodatneUsluge.Naziv;
            tbIznos.DataContext = (dodatneUsluge.Iznos).ToString();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var ucitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    dodatneUsluge.Id = ucitaneDodatneUsluge.Count + 1;
                    ucitaneDodatneUsluge.Add(dodatneUsluge);
                    break;
                case TipOperacije.IZMENA:
                    break;
                default:
                    break;    
            }
            GenericSerializer.Serialize("dodatne_uslge.xml", ucitaneDodatneUsluge);
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
