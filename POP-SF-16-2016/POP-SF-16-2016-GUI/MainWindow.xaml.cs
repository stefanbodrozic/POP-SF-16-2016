//using POP_SF_16_2016_GUI.GUI;
using System.Windows;
using System.Windows.Controls;

namespace POP_SF_16_2016_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //OsveziPrikaz();
        }

        //private void OsveziPrikaz()
        //{
        //    lbNamestaj.Items.Clear();
        //    foreach (var namestaj in Projekat.Instanca.Namestaj)
        //    {
        //        lbNamestaj.Items.Add(namestaj);
        //    }

        //    lbNamestaj.SelectedIndex = 0;

        //}

        //private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        //{
        //    var prazanNamestaj = new Namestaj()
        //    {
        //        Naziv = ""
        //    };

        //    var namestajProzor = new DodajIzmeniNamestajWindow(prazanNamestaj, DodajIzmeniNamestajWindow.TipOperacije.DODAVANJE);
        //    namestajProzor.ShowDialog();

        //    OsveziPrikaz();
        //}
        private void btnDodajNamestaj_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}
        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {

        }
        //private void btnIzmeniNamestaj_Click(object sender, RoutedEventArgs e)
        //{
        //    var izabraniNamestaj = (Namestaj)lbNamestaj.SelectedItem;
        //    var namestajProzor = new DodajIzmeniNamestajWindow(izabraniNamestaj, DodajIzmeniNamestajWindow.TipOperacije.IZMENA);
        //    namestajProzor.ShowDialog();
        //    OsveziPrikaz();

        //}
        private void btnIzmeniNamestaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lbNamestaj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
