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
    /// Interaction logic for DodavanjeIzmenaAkcije.xaml
    /// </summary>
    public partial class DodajIzmeniAkcija : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        };

        private Akcija akcija;
        private TipOperacije tipOperacije;

        public DodajIzmeniAkcija(Akcija akcija, TipOperacije tipOperacije)
        {
            InitializeComponent();
        
            this.akcija = akcija;
            this.tipOperacije = tipOperacije;

            tbDatumPocetka.DataContext = akcija;
            tbDatumZavrsetka.DataContext = akcija;
            tbPopust.DataContext = akcija;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            var ucitaneAkcije = Projekat.Instanca.Akcija;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    akcija.Id = ucitaneAkcije.Count;
                    ucitaneAkcije.Add(akcija);
                    break;
                case TipOperacije.IZMENA:

                    break;
                default:
                    break;
            }
            GenericSerializer.Serialize("akcija.xml", ucitaneAkcije);
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
