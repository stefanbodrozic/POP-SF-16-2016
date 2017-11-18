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
    /// Interaction logic for DodajIzmeniNamestaj.xaml
    /// </summary>
    public partial class DodajIzmeniNamestaj : Window
    {
        public enum TipOperacije
        {
            DODAVANJE,
            IZMENA
        }
        private Namestaj namestaj;
        private TipOperacije tipOperacije;
        private List<Namestaj> prosledjenaListaNamestaja;


        public DodajIzmeniNamestaj(Namestaj namestaj, TipOperacije tipOperacije, List<Namestaj> prosledjenaListaNamestaja)
        {
            InitializeComponent();
            InicijalizujPodatke(namestaj, tipOperacije, prosledjenaListaNamestaja);
        }

        private void InicijalizujPodatke(Namestaj namestaj, TipOperacije tipOperacije, List<Namestaj> prosledjenaListaNamestaja)
        {
            this.namestaj = namestaj;
            this.tipOperacije = tipOperacije;
            this.prosledjenaListaNamestaja = prosledjenaListaNamestaja;

            tbNaziv.Text = namestaj.Naziv;
            tbCena.Text = namestaj.Cena.ToString();
            tbKolicina.Text = namestaj.KolicinaUMagacinu.ToString();
            tbSifra.Text = namestaj.Sifra;
            foreach (var tipNamestaja in Projekat.Instanca.TipoviNamestaja)
            {
                cbTipNamestaja.Items.Add(tipNamestaja);
                cbTipNamestaja.SelectedIndex = 0;
            }

            ////punjenje combobox-a tipovima namestaja
            //foreach (var tipNamestaja in Projekat.Instanca.TipoviNamestaja)
            //{
            //    if(tipNamestaja.Obrisan != true)
            //    {
            //        cbTipNamestaja.Items.Add(tipNamestaja);
            //    }  
            //}

            //postavljanje postojeceg tipa namestaja u combobox prilikom izmene
            foreach (TipNamestaja tipNamestaja in cbTipNamestaja.Items)
            {
                if (tipNamestaja.Id == namestaj.Id)
                {
                    cbTipNamestaja.SelectedItem = tipNamestaja;
                    break;
                }
            }
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            //pronalazenje tipa namestaja koji je izabrao korisnik
            var izabraniTipNamestaja = (TipNamestaja)cbTipNamestaja.SelectedItem;
            switch (tipOperacije)
            {
                case TipOperacije.DODAVANJE:
                    var noviNamestaj = new Namestaj
                    {
                        Id = prosledjenaListaNamestaja.Count + 1,
                        Naziv = tbNaziv.Text,
                        Cena = double.Parse(tbCena.Text),
                        KolicinaUMagacinu = int.Parse(tbKolicina.Text),
                        Sifra = tbSifra.Text,
                        TipNamestajaId = izabraniTipNamestaja.Id
                    };
                    prosledjenaListaNamestaja.Add(noviNamestaj);
                    break;
                case TipOperacije.IZMENA:
                    foreach (var trazeniNamestaj in prosledjenaListaNamestaja)
                    {
                        if (trazeniNamestaj.Id == namestaj.Id)
                        {
                            trazeniNamestaj.Naziv = this.tbNaziv.Text;
                            trazeniNamestaj.Cena = double.Parse(this.tbCena.Text);
                            trazeniNamestaj.KolicinaUMagacinu = int.Parse(this.tbKolicina.Text);
                            trazeniNamestaj.Sifra = this.tbSifra.Text;
                            trazeniNamestaj.TipNamestajaId = izabraniTipNamestaja.Id;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
            Projekat.Instanca.Namestaj = prosledjenaListaNamestaja;
            Close();

        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
