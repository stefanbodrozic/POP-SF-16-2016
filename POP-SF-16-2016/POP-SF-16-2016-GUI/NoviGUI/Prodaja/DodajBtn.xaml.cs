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

namespace POP_SF_16_2016_GUI.NoviGUI.Prodaja
{
    /// <summary>
    /// Interaction logic for DodajBtn.xaml
    /// </summary>
    public partial class DodajBtn : Window
    {
        public enum TipOperacije
        {
            NAMESTAJ,
            DODATNAUSLUGA
        };

        public DodajBtn(object izabranaStavka, TipOperacije tipOperacije)
        {
            InitializeComponent();
            


        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
