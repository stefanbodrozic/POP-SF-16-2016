﻿
using POP_SF_16_2016_GUI.Model;
using POP_SF_16_2016_GUI.GUI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
