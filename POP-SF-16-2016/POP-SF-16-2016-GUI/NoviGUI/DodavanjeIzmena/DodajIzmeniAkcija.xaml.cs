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
        private List<Akcija> prosledjenaListaAkcija;

        public DodajIzmeniAkcija(Akcija akcija, TipOperacije tipOperacije, List<Akcija> prosledjenaListaAkcija)
        {
            InitializeComponent();
            InicijalizujPodatke(akcija, tipOperacije, prosledjenaListaAkcija);
        }

        private void InicijalizujPodatke(Akcija akcija, TipOperacije tipOperacije, List<Akcija> prosledjenaListaAkcija)
        {
            this.akcija = akcija;
            this.tipOperacije = tipOperacije;
            this.prosledjenaListaAkcija = prosledjenaListaAkcija;

            tbDatumPocetka.Text = (akcija.DatumPocetka.Date).ToString();
            tbDatumZavrsetka.Text = (akcija.DatumZavrsetka.Date).ToString();
            tbPopust.Text = (akcija.Popust).ToString();  
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    var novaAkcija = new Akcija()
                    {
                        Id = prosledjenaListaAkcija.Count + 1,
                        DatumPocetka = DateTime.Parse(tbDatumPocetka.Text),
                        DatumZavrsetka = DateTime.Parse(tbDatumZavrsetka.Text),
                        Popust = decimal.Parse(tbPopust.Text)
                    };
                    prosledjenaListaAkcija.Add(novaAkcija);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var trazenaAkcija in prosledjenaListaAkcija)
                    {
                        if(trazenaAkcija.Id == akcija.Id)
                        {
                            trazenaAkcija.DatumPocetka = DateTime.Parse(tbDatumPocetka.Text);
                            trazenaAkcija.DatumZavrsetka = DateTime.Parse(tbDatumZavrsetka.Text);
                            trazenaAkcija.Popust = decimal.Parse(tbPopust.Text);
                        }
                    }
                    break;
                default:
                    break;
            }
            Projekat.Instanca.Akcija = prosledjenaListaAkcija;
            Close();
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
