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

        public DodajIzmeniDodatneUsluge(DodatneUsluge dodatnaUsluga, TipOperacije tipOperacije)
        {
            InitializeComponent();

            this.dodatneUsluge = dodatnaUsluga;
            this.tipOperacije = tipOperacije;

            tbNaziv.DataContext = dodatnaUsluga;
            tbIznos.DataContext = dodatnaUsluga;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double.Parse(tbIznos.Text);
            }
            catch
            {
                MessageBox.Show("Greska prilikom unosa cene!", "Greska", MessageBoxButton.OK);
                return;
            }
            this.DialogResult = true;
            var ucitaneDodatneUsluge = Projekat.Instanca.DodatneUsluge;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    //dodatneUsluge.Id = ucitaneDodatneUsluge.Count;
                    //ucitaneDodatneUsluge.Add(dodatneUsluge);
                    DodatneUsluge.Create(dodatneUsluge);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var usluga in ucitaneDodatneUsluge)
                    {
                        if(usluga.Id == dodatneUsluge.Id)
                        {
                            usluga.Iznos = dodatneUsluge.Iznos;
                            usluga.Naziv = dodatneUsluge.Naziv;
                            break;
                        }
                    }
                    DodatneUsluge.Update(dodatneUsluge);
                    break;
                default:
                    break;    
            }
            //GenericSerializer.Serialize("dodatne_usluge.xml", ucitaneDodatneUsluge);
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
