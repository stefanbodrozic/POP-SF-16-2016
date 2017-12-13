using POP_SF_16_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace POP_SF_16_2016_GUI.NoviGUI.Prodaja
{
    /// <summary>
    /// Interaction logic for PrikazProdatihStavki.xaml
    /// </summary>
    public partial class PrikazProdatihStavki : Window
    {

        private ProdajaNamestaja prodajaNamestaja;
        public PrikazProdatihStavki(ProdajaNamestaja prodajaNamestaja)
        {
            InitializeComponent();

            var prodatNamestaj = new ObservableCollection<Namestaj>();
            var prodateDodatneUsluge = new ObservableCollection<DodatneUsluge>();

            this.prodajaNamestaja = prodajaNamestaja;
            foreach (var stavkaId in prodajaNamestaja.StavkaNaRacunu)
            {
                //var stavka = Projekat.Instanca.StavkaRacuna.SingleOrDefault(x => x.IdStavkeRacuna == stavkaId);
                foreach (var stavka in Projekat.Instanca.StavkaRacuna)
                {
                    if (stavka.IdStavkeRacuna == stavkaId)
                    {
                        foreach (var namestaj in Projekat.Instanca.Namestaj)
                        {
                            if (stavka.IdNamestaja == namestaj.Id)
                            {
                                prodatNamestaj.Add(namestaj);
                            }
                        }
                        foreach (var dodatnaUsluga in Projekat.Instanca.DodatneUsluge)
                        {
                            if (stavka.IdDodatneUsluge == dodatnaUsluga.Id)
                            {
                                prodateDodatneUsluge.Add(dodatnaUsluga);
                            }
                        }
                    }
                }
            }
            //ciscenje od default vrednosti
            foreach (var namestaj in prodatNamestaj.ToList())
            {
                if(namestaj.Id == 0)
                {
                    prodatNamestaj.Remove(namestaj);
                }
            }
            foreach (var usluga in prodateDodatneUsluge.ToList())
            {
                if(usluga.Id == 0)
                {
                    prodateDodatneUsluge.Remove(usluga);
                }
            }

            dgNamestaj.ItemsSource = prodatNamestaj;
            dgNamestaj.DataContext = this;
            dgNamestaj.IsSynchronizedWithCurrentItem = true;
            dgNamestaj.CanUserAddRows = false;
            dgNamestaj.IsReadOnly = true;
            dgNamestaj.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            dgDodatneUsluge.ItemsSource = prodateDodatneUsluge;
            dgDodatneUsluge.DataContext = this;
            dgDodatneUsluge.IsSynchronizedWithCurrentItem = true;
            dgDodatneUsluge.CanUserAddRows = false;
            dgDodatneUsluge.IsReadOnly = true;
            dgDodatneUsluge.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Obrisan" || e.Column.Header.ToString() == "Sifra" || e.Column.Header.ToString() == "KolicinaUMagacinu" || e.Column.Header.ToString() == "TipNamestajaId" || e.Column.Header.ToString() == "AkcijaId") //izbacivanje
            {
                e.Cancel = true;
            }
        }

        private void dgDodatneUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Obrisan" ) //izbacivanje
            {
                e.Cancel = true;
            }
        }
    }
}
