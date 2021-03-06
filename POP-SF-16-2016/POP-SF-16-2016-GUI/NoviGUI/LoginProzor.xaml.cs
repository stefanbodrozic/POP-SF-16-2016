﻿using POP_SF_16_2016_GUI.Model;
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
using MahApps.Metro.Controls;

namespace POP_SF_16_2016_GUI.NoviGUI
{
    /// <summary>
    /// Interaction logic for LoginProzor.xaml
    /// </summary>
    public partial class LoginProzor : Window
    {
        public LoginProzor()
        {
            InitializeComponent();
        }

        private int brojacPrijava = 0;

        private void btnPrijava_Click(object sender, RoutedEventArgs e)
        {
            this.brojacPrijava++;
            string korisnickoIme = tbKorisnickoIme.Text;
            string lozinka = pbLozinka.Password;
            var ucitaniKorisnici = Projekat.Instanca.Korisnik;
            foreach (Korisnik korisnik in ucitaniKorisnici)
            {
                if (korisnik.Obrisan != true && korisnik.KorisnickoIme == korisnickoIme && korisnik.Lozinka == lozinka)
                {
                    var glavniProzor = new GlavniProzor(korisnik);
                    this.Close();
                    glavniProzor.ShowDialog();
                    return;
                }
            }
            MessageBox.Show("Pogresni podaci za prijavu!", "Greska", MessageBoxButton.OK);
            if (brojacPrijava == 3)
            {
                MessageBox.Show("Iskoristili ste 3 pokusaja za prijavu. Program ce biti zatvoren!", "Greska", MessageBoxButton.OK, MessageBoxImage.Hand);
                Close();
            }
            return;            
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
