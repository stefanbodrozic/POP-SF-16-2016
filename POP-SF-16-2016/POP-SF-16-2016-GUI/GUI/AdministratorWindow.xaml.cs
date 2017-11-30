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

namespace POP_SF_16_2016_GUI.GUI
{
    /// <summary>
    /// Interaction logic for AdministratorGUI.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public AdministratorWindow()
        {
            InitializeComponent();
        }

        private void btnAkcija_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnProdajaNamestaja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDodatneUsluge_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnKorisnik_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTipNamestaja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSalon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {
            var namestaj = new RadSaNamestajemWindow();
            this.Close();
            namestaj.ShowDialog();

        }
    }
}
