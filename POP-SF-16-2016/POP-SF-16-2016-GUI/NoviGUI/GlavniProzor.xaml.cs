using POP_SF_16_2016_GUI.Model;
using POP_SF_16_2016_GUI.NoviGUI.DodavanjeIzmena;
using POP_SF_16_2016_GUI.NoviGUI.Prodaja;
using POP_SF_16_2016_GUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace POP_SF_16_2016_GUI.NoviGUI
{
    /// <summary>
    /// Interaction logic for GlavniProzor.xaml
    /// </summary>
    public partial class GlavniProzor : Window
    {
        //public Namestaj IzabraniNamestaj { get; set; }

        //napraviti objekat bind koji menja path u zavisnosti sta je izabrano. trenutno je napravljen staticki bind
        // u switch case koristiti case enum :...   umesto case 1:...

        private int selektovanoZaIzmenu = 0;
        private ICollectionView view;
        public Namestaj IzabraniNamestaj { get; set; } //za izmenu binding

        public GlavniProzor(Korisnik prijavljenKorisnik)
        {
            InitializeComponent();
            if (prijavljenKorisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                btnNamestaj.Visibility = Visibility.Hidden;
                btnTipNamestaja.Visibility = Visibility.Hidden;
                btnDodatneUsluge.Visibility = Visibility.Hidden;
                btnAkcije.Visibility = Visibility.Hidden;
                btnKorisnici.Visibility = Visibility.Hidden;
                btnSalon.Visibility = Visibility.Hidden;
            }
            tbPrikazKorisnika.Text = ($"{prijavljenKorisnik.Ime} {prijavljenKorisnik.Prezime} \n{prijavljenKorisnik.TipKorisnika}");

            //view = CollectionViewSource.GetDefaultView(Projekat.Instanca.Namestaj);
            //view.Filter = FilterNeobrisanogNamestaja;
        }

        private bool FilterNeobrisanogNamestaja(object obj)
        {
            return ((Namestaj)obj).Obrisan == false;  //false&false = true


            //2 nacin
            //if (((Namestaj)obj).Obrisan == false){
            //    return true; // treba da se prikaze, zadovoljava kriterijum
            //}else
            //{
            //    return false;
            //}

            //3 nacin
            //return !((Namestaj)obj).Obrisan 
        }

        private void btnProdajaNamestaja_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 1;
            dgPrikazStavki.ItemsSource = Projekat.Instanca.ProdajaNamestaja;
            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
        }

        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 2;
            //dgPrikazStavki.ItemsSource = Projekat.Instanca.Namestaj;
            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;

            dgPrikazStavki.CanUserAddRows = false;
            dgPrikazStavki.IsReadOnly = true;

            dgPrikazStavki.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            view = CollectionViewSource.GetDefaultView(Projekat.Instanca.Namestaj);
            view.Filter = FilterNeobrisanogNamestaja;
            dgPrikazStavki.ItemsSource = view;
        }

        private void btnTipNamestaja_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 3;
            dgPrikazStavki.ItemsSource = Projekat.Instanca.TipoviNamestaja;
            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
        }

        private void btnDodatneUsluge_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 4;
            dgPrikazStavki.ItemsSource = Projekat.Instanca.DodatneUsluge;
            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
        }

        private void btnAkcije_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 5;
            dgPrikazStavki.ItemsSource = Projekat.Instanca.Akcija;
            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
        }

        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 6;
            dgPrikazStavki.ItemsSource = Projekat.Instanca.Korisnik;
            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
        }

        private void btnSalon_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 7;
            dgPrikazStavki.ItemsSource = Projekat.Instanca.Salon;
            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                case 1:
                    var prodajaNamestaja = new ProdajaNamestaja()
                    {
                        BrojRacuna = "",
                        DatumProdaje = DateTime.Today,
                        Kupac = "",
                        UkupnaCena = 0,
                        StavkaNaRacunu = new ObservableCollection<int>()
                    };
                    var novaProdaja = new DodajProdajuNamestaja(prodajaNamestaja);
                    novaProdaja.ShowDialog();
                    break;
                case 2:
                    var prazanNamestaj = new Namestaj()
                    {
                        Naziv = "",
                        Cena = 0,
                        KolicinaUMagacinu = 0,
                        Sifra = "",
                        TipNamestajaId = 0
                    };
                    var dodavanjeNamestaja = new DodajIzmeniNamestaj(prazanNamestaj, DodajIzmeniNamestaj.TipOperacije.DODAVANJE);
                    dodavanjeNamestaja.ShowDialog();
                    break;
                case 3:
                    var prazanTipNamestaja = new TipNamestaja()
                    {
                        Naziv = ""
                    };
                    var dodavanjeTipaNamestaja = new DodajIzmeniTipNamestaja(prazanTipNamestaja, DodajIzmeniTipNamestaja.TipOperacije.DODAVANJE);
                    dodavanjeTipaNamestaja.ShowDialog();
                    break;
                case 4:
                    var praznaDodatnaUsluga = new DodatneUsluge()
                    {
                        Naziv = "",
                        Iznos = 0
                    };
                    var dodavanjeDodatneUsluge = new DodajIzmeniDodatneUsluge(praznaDodatnaUsluga, DodajIzmeniDodatneUsluge.TipOperacije.DODAVANJE);
                    dodavanjeDodatneUsluge.ShowDialog();
                    break;
                case 5:
                    var praznaAkcija = new Akcija()
                    {
                        DatumPocetka = DateTime.Today,
                        DatumZavrsetka = DateTime.Today,
                        Popust = 0,
                    };
                    var dodavanjeAkcije = new DodajIzmeniAkcija(praznaAkcija, DodajIzmeniAkcija.TipOperacije.DODAVANJE);
                    dodavanjeAkcije.ShowDialog();
                    break;
                case 6:
                    var prazanKorisnik = new Korisnik()
                    {
                        Ime = "",
                        Prezime = "",
                        KorisnickoIme = "",
                        Lozinka = "",
                        TipKorisnika = TipKorisnika.Prodavac
                    };
                    var dodavanjeKorisnika = new DodajIzmeniKorisnik(prazanKorisnik, DodajIzmeniKorisnik.TipOperacije.DODAVANJE);
                    dodavanjeKorisnika.ShowDialog();
                    break;
                case 7:
                    var prazanSalon = new Salon()
                    {
                        Naziv = "",
                        Adresa = "",
                        Telefon = "",
                        Email = "",
                        Websajt = "",
                        Pib = 0,
                        MaticniBroj = 0,
                        BrojZiroRacuna = ""
                    };
                    var dodavanjeSalona = new DodajIzmeniSalon(prazanSalon, DodajIzmeniSalon.TipOperacije.DODAVANJE);
                    dodavanjeSalona.ShowDialog();
                    break;
                default:
                    break;
            }
        }


        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                case 1:
                    break;
                case 2:
                    // SelectedItem="{Binding Path = IzabraniNamestaj}  napraviti ovo da dinamicki menja path!!!!
                    //var izabraniNamestaj = (Namestaj)dgPrikazStavki.SelectedItem;   otpada zbog ovog iznad

                    Namestaj kopijaNamestaja = (Namestaj)IzabraniNamestaj.Clone();
                    var izmenaNamestaja = new DodajIzmeniNamestaj(kopijaNamestaja, DodajIzmeniNamestaj.TipOperacije.IZMENA);
                    izmenaNamestaja.ShowDialog();;
                    break;

                case 3:
                    var izabraniTipNamestaja = (TipNamestaja)dgPrikazStavki.SelectedItem;
                    var izmenaTipaNamestaja = new DodajIzmeniTipNamestaja(izabraniTipNamestaja, DodajIzmeniTipNamestaja.TipOperacije.IZMENA);
                    izmenaTipaNamestaja.ShowDialog();
                    break;
                case 4:
                    var izabranaDodatnaUsluga = (DodatneUsluge)dgPrikazStavki.SelectedItem;
                    var izmenaDodatneUsluge = new DodajIzmeniDodatneUsluge(izabranaDodatnaUsluga, DodajIzmeniDodatneUsluge.TipOperacije.IZMENA);
                    izmenaDodatneUsluge.ShowDialog();
                    break;
                case 5:
                    var izabranaAkcija = (Akcija)dgPrikazStavki.SelectedItem;
                    var izmenaAkcije = new DodajIzmeniAkcija(izabranaAkcija, DodajIzmeniAkcija.TipOperacije.IZMENA);
                    izmenaAkcije.ShowDialog();
                    break;
                case 6:
                    var izabraniKorisnik = (Korisnik)dgPrikazStavki.SelectedItem;
                    var izmenaKorisnika = new DodajIzmeniKorisnik(izabraniKorisnik, DodajIzmeniKorisnik.TipOperacije.IZMENA);
                    izmenaKorisnika.ShowDialog();
                    break;
                case 7:
                    var izabraniSalon = (Salon)dgPrikazStavki.SelectedItem;
                    var izmenaSalona = new DodajIzmeniSalon(izabraniSalon, DodajIzmeniSalon.TipOperacije.IZMENA);
                    izmenaSalona.ShowDialog();
                    break;
                default:
                    break;
            }

        }

        
        
        private void btnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                case 1:
                    break;
                case 2:
                    var izabraniNamestaj = (Namestaj)dgPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete namestaj: {izabraniNamestaj.Naziv}", "Brisanje namestaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaNamestaja = Projekat.Instanca.Namestaj;
                        foreach (var namestaj in listaNamestaja)
                        {
                            if (namestaj.Obrisan != true && namestaj.Id == izabraniNamestaj.Id)
                            {
                                namestaj.Obrisan = true;
                                view.Refresh();
                            }   
                        }
                        GenericSerializer.Serialize("namestaj.xml", listaNamestaja);
                    }
                    break;
                case 3:
                    var izabraniTipNamestaja = (TipNamestaja)dgPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete tip namestaja: {izabraniTipNamestaja.Naziv}", "Brisanje tipa namestaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaTipovaNamestaja = Projekat.Instanca.TipoviNamestaja;
                        foreach (var tipNamestaja in listaTipovaNamestaja)
                        {
                            if (tipNamestaja.Id == izabraniTipNamestaja.Id)
                            {
                                tipNamestaja.Obrisan = true;
                            }
                        }
                        GenericSerializer.Serialize("tipovi_namestaja.xml", listaTipovaNamestaja);
                    };
                    break;
                case 4:
                    var izabranaDodatnaUsluga = (DodatneUsluge)dgPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete dodatnu uslugu: {izabranaDodatnaUsluga.Naziv}", "Brisanje dodatne usluge", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaDodatnihUsluga = Projekat.Instanca.DodatneUsluge;
                        foreach (var dodatnaUsluga in listaDodatnihUsluga)
                        {
                            if (dodatnaUsluga.Id == izabranaDodatnaUsluga.Id)
                            {
                                dodatnaUsluga.Obrisan = true;
                            }
                        }
                        GenericSerializer.Serialize("dodatne_usluge.xml", listaDodatnihUsluga);
                    }
                    break;
                case 5:
                    var izabranaAkcija = (Akcija)dgPrikazStavki.SelectedItem;
                    if (MessageBox.Show("Da li ste sigurni da zelite da izbrisete akciju?", "Brisanje akcije", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaAkcija = Projekat.Instanca.Akcija;
                        foreach (var akcija in listaAkcija)
                        {
                            if (akcija.Id == izabranaAkcija.Id)
                            {
                                akcija.Obrisan = true;
                            }
                        }
                        GenericSerializer.Serialize("akcija.xml", listaAkcija);
                    };
                    break;
                case 6:
                    var izabraniKorisnik = (Korisnik)dgPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete korisnika:{izabraniKorisnik.Ime + " " + izabraniKorisnik.Prezime}", "Brisanje korisnika", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaKorisnika = Projekat.Instanca.Korisnik;
                        foreach (var korisnik in listaKorisnika)
                        {
                            if (korisnik.Id == izabraniKorisnik.Id)
                            {
                                korisnik.Obrisan = true;
                            }
                        }
                        GenericSerializer.Serialize("korisnici.xml", listaKorisnika);
                    };
                    break;
                case 7:
                    var izabraniSalon = (Salon)dgPrikazStavki.SelectedItem;
                    if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete salon:{izabraniSalon.Naziv}", "Brisanje salona", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var listaSalona = Projekat.Instanca.Salon;
                        foreach (var salon in listaSalona)
                        {
                            if (salon.Id == izabraniSalon.Id)
                            {
                                salon.Obrisan = true;
                            }
                        }
                        GenericSerializer.Serialize("salon.xml", listaSalona);
                    }
                    break;

                default:
                    break;
            }
        }

        private void btnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            var loginProzor = new LoginProzor();
            Close();
            loginProzor.ShowDialog();
        }

        private void dgPrikazStavki_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) //za namestaj
        {
            if(e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "TipNamestajaId" || e.Column.Header.ToString() == "Obrisan") //izbacivanje
            {
                e.Cancel = true;
            } 
        }
    };
}


