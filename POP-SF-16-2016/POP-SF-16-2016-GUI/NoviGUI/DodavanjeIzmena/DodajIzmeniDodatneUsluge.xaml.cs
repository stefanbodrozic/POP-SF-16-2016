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
        private List<DodatneUsluge> prosledjenaListaDodatneUsluge;

        public DodajIzmeniDodatneUsluge(DodatneUsluge dodatneUsluge, TipOperacije tipOperacije, List<DodatneUsluge> prosledjenaListaDodatneUsluge)
        {
            InitializeComponent();
            InicijalizujPodatke(dodatneUsluge, tipOperacije, prosledjenaListaDodatneUsluge);
        }
        public void InicijalizujPodatke(DodatneUsluge dodatneUsluge, TipOperacije tipOperacije, List<DodatneUsluge> prosledjenaListaDodatneUsluge)
        {
            this.dodatneUsluge = dodatneUsluge;
            this.tipOperacije = tipOperacije;
            this.prosledjenaListaDodatneUsluge = prosledjenaListaDodatneUsluge;

            tbNaziv.Text = dodatneUsluge.Naziv;
            tbIznos.Text = (dodatneUsluge.Iznos).ToString();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    var novaDodatnaUsluga = new DodatneUsluge
                    {
                        Id = prosledjenaListaDodatneUsluge.Count + 1,
                        Naziv = tbNaziv.Text,
                        Iznos = double.Parse(tbIznos.Text)
                    };
                    prosledjenaListaDodatneUsluge.Add(novaDodatnaUsluga);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var trazenaDodatnaUsluga in prosledjenaListaDodatneUsluge)
                    {
                        if(trazenaDodatnaUsluga.Id == dodatneUsluge.Id)
                        {
                            trazenaDodatnaUsluga.Naziv = tbNaziv.Text;
                            trazenaDodatnaUsluga.Iznos = double.Parse(tbIznos.Text);
                        }
                    }
                    break;
                default:
                    break;    
            }
            Projekat.Instanca.DodatneUsluge = prosledjenaListaDodatneUsluge;
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
