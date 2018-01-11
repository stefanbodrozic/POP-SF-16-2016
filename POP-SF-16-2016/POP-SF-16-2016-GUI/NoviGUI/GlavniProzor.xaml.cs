using POP_SF_16_2016_GUI.Model;
using POP_SF_16_2016_GUI.NoviGUI.Akcije;
using POP_SF_16_2016_GUI.NoviGUI.DodavanjeIzmena;
using POP_SF_16_2016_GUI.NoviGUI.Prodaja;
using POP_SF_16_2016_GUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
        private enum SelektovanoZaIzmenu
        {
            Nista,
            ProdajaNamestaja,
            Namestaj,
            TipNamestaja,
            DodatneUsluge,
            Akcije,
            Korisnik,
            Salon
        }
        private SelektovanoZaIzmenu selektovanoZaIzmenu;
        private ICollectionView view;

        public GlavniProzor(Korisnik prijavljenKorisnik)
        {
            InitializeComponent();

            ProveriAkcije();

            if (prijavljenKorisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                btnNamestaj.Visibility = Visibility.Hidden;
                btnTipNamestaja.Visibility = Visibility.Hidden;
                btnDodatneUsluge.Visibility = Visibility.Hidden;
                btnAkcije.Visibility = Visibility.Hidden;
                btnKorisnici.Visibility = Visibility.Hidden;
                btnSalon.Visibility = Visibility.Hidden;
                btnIzmeni.Visibility = Visibility.Hidden;
                btnIzbrisi.Visibility = Visibility.Hidden;
                btnProizvodiNaAkciji.Visibility = Visibility.Hidden;
            }
            tbPrikazKorisnika.Text = ($"{prijavljenKorisnik.Ime} {prijavljenKorisnik.Prezime} \n{prijavljenKorisnik.TipKorisnika}");
            btnPrikaziStavke.Visibility = Visibility.Hidden;
            btnProizvodiNaAkciji.Visibility = Visibility.Hidden;
        }

        private bool FilterNeobrisanihStavki(object obj)
        {
            switch (selektovanoZaIzmenu)
            {
                case SelektovanoZaIzmenu.ProdajaNamestaja:
                    if (((ProdajaNamestaja)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case SelektovanoZaIzmenu.Namestaj:
                    if (((Namestaj)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case SelektovanoZaIzmenu.TipNamestaja:
                    if (((TipNamestaja)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case SelektovanoZaIzmenu.DodatneUsluge:
                    if (((DodatneUsluge)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case SelektovanoZaIzmenu.Akcije:
                    if (((Akcija)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case SelektovanoZaIzmenu.Korisnik:
                    if (((Korisnik)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case SelektovanoZaIzmenu.Salon:
                    if (((Salon)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
            }
            return false;
        }

        private void btnProdajaNamestaja_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = SelektovanoZaIzmenu.ProdajaNamestaja;
            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
            dgPrikazStavki.CanUserAddRows = false;
            dgPrikazStavki.IsReadOnly = true;
            dgPrikazStavki.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            view = CollectionViewSource.GetDefaultView(Projekat.Instanca.ProdajaNamestaja);
            view.Filter = FilterNeobrisanihStavki;
            dgPrikazStavki.ItemsSource = view;

            btnProizvodiNaAkciji.Visibility = Visibility.Hidden;
            btnPrikaziStavke.Visibility = Visibility.Visible;
            btnIzmeni.Visibility = Visibility.Hidden;

            var prodajaSort = new List<string>();
            prodajaSort.Add("Rastuce: Datum prodaje");
            prodajaSort.Add("Rastuce: Broj racuna");
            prodajaSort.Add("Rastuce: Ukupna cena");
            prodajaSort.Add("Rastuce: Cena bez pdv");
            prodajaSort.Add("Rastuce: Kupac");
            prodajaSort.Add("Opadajuce: Datum prodaje");
            prodajaSort.Add("Opadajuce: Broj racuna");
            prodajaSort.Add("Opadajuce: Ukupna cena");
            prodajaSort.Add("Opadajuce: Cena bez pdv");
            prodajaSort.Add("Opadajuce: Kupac");

            cbSortiraj.ItemsSource = prodajaSort;
        }

        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = SelektovanoZaIzmenu.Namestaj;

            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
            dgPrikazStavki.CanUserAddRows = false;
            dgPrikazStavki.IsReadOnly = true;
            dgPrikazStavki.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            view = CollectionViewSource.GetDefaultView(Projekat.Instanca.Namestaj);
            view.Filter = FilterNeobrisanihStavki;
            dgPrikazStavki.ItemsSource = view;

            btnPrikaziStavke.Visibility = Visibility.Hidden;
            btnIzbrisi.Visibility = Visibility.Visible;
            btnIzmeni.Visibility = Visibility.Visible;
            btnProizvodiNaAkciji.Visibility = Visibility.Hidden;

            var namestajSort = new List<string>();
            namestajSort.Add("Rastuce: Naziv");
            namestajSort.Add("Rastuce: Sifra");
            namestajSort.Add("Rastuce: Cena");
            namestajSort.Add("Rastuce: Akcijska cena");
            namestajSort.Add("Rastuce: Kolicina u magacinu");
            namestajSort.Add("Rastuce: Tip namestaja");
            namestajSort.Add("Opadajuce: Naziv");
            namestajSort.Add("Opadajuce: Sifra");
            namestajSort.Add("Opadajuce: Cena");
            namestajSort.Add("Opadajuce: Akcijska cena");
            namestajSort.Add("Opadajuce: Kolicina u magacinu");
            namestajSort.Add("Opadajuce: Tip namestaja");

            cbSortiraj.ItemsSource = namestajSort;
        }

        private void btnTipNamestaja_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = SelektovanoZaIzmenu.TipNamestaja;

            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
            dgPrikazStavki.CanUserAddRows = false;
            dgPrikazStavki.IsReadOnly = true;
            dgPrikazStavki.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            view = CollectionViewSource.GetDefaultView(Projekat.Instanca.TipoviNamestaja);
            view.Filter = FilterNeobrisanihStavki;
            dgPrikazStavki.ItemsSource = view;

            btnPrikaziStavke.Visibility = Visibility.Hidden;
            btnIzbrisi.Visibility = Visibility.Visible;
            btnIzmeni.Visibility = Visibility.Visible;
            btnProizvodiNaAkciji.Visibility = Visibility.Hidden;

            var tipNamestajaSort = new List<string>();
            tipNamestajaSort.Add("Rastuce: Naziv");
            tipNamestajaSort.Add("Opadajuce: Naziv");

            cbSortiraj.ItemsSource = tipNamestajaSort;
        }

        private void btnDodatneUsluge_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = SelektovanoZaIzmenu.DodatneUsluge;

            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
            dgPrikazStavki.CanUserAddRows = false;
            dgPrikazStavki.IsReadOnly = true;
            dgPrikazStavki.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            view = CollectionViewSource.GetDefaultView(Projekat.Instanca.DodatneUsluge);
            view.Filter = FilterNeobrisanihStavki;
            dgPrikazStavki.ItemsSource = view;

            btnPrikaziStavke.Visibility = Visibility.Hidden;
            btnIzbrisi.Visibility = Visibility.Visible;
            btnIzmeni.Visibility = Visibility.Visible;
            btnProizvodiNaAkciji.Visibility = Visibility.Hidden;

            var dodatneSort = new List<string>();
            dodatneSort.Add("Rastuce: Naziv");
            dodatneSort.Add("Rastuce: Iznos");
            dodatneSort.Add("Opadajuce: Naziv");
            dodatneSort.Add("Opadajuce: Iznos");

            cbSortiraj.ItemsSource = dodatneSort;
        }

        private void btnAkcije_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = SelektovanoZaIzmenu.Akcije;

            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
            dgPrikazStavki.CanUserAddRows = false;
            dgPrikazStavki.IsReadOnly = true;
            dgPrikazStavki.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            view = CollectionViewSource.GetDefaultView(Projekat.Instanca.Akcija);
            view.Filter = FilterNeobrisanihStavki;
            dgPrikazStavki.ItemsSource = view;

            btnPrikaziStavke.Visibility = Visibility.Hidden;
            btnIzbrisi.Visibility = Visibility.Visible;
            btnIzmeni.Visibility = Visibility.Visible;
            btnProizvodiNaAkciji.Visibility = Visibility.Visible;

            var akcijaSort = new List<string>();
            akcijaSort.Add("Rastuce: Datum pocetka");
            akcijaSort.Add("Rastuce: Datum zavrsetka");
            akcijaSort.Add("Rastuce: Popust");
            akcijaSort.Add("Rastuce: Naziv akcije");
            akcijaSort.Add("Opadajuce: Datum pocetka");
            akcijaSort.Add("Opadajuce: Datum zavrsetka");
            akcijaSort.Add("Opadajuce: Popust");
            akcijaSort.Add("Opadajuce: Naziv akcije");

            cbSortiraj.ItemsSource = akcijaSort;
        }

        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = SelektovanoZaIzmenu.Korisnik;

            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
            dgPrikazStavki.CanUserAddRows = false;
            dgPrikazStavki.IsReadOnly = true;
            dgPrikazStavki.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            view = CollectionViewSource.GetDefaultView(Projekat.Instanca.Korisnik);
            view.Filter = FilterNeobrisanihStavki;
            dgPrikazStavki.ItemsSource = view;

            btnPrikaziStavke.Visibility = Visibility.Hidden;
            btnIzbrisi.Visibility = Visibility.Visible;
            btnIzmeni.Visibility = Visibility.Visible;
            btnProizvodiNaAkciji.Visibility = Visibility.Hidden;

            var korisniciSort = new List<string>();
            korisniciSort.Add("Rastuce: Ime");
            korisniciSort.Add("Rastuce: Prezime");
            korisniciSort.Add("Rastuce: Korisnicko ime");
            korisniciSort.Add("Rastuce: Lozinka");
            korisniciSort.Add("Rastuce: Tip korisnika");
            korisniciSort.Add("Opadajuce: Ime");
            korisniciSort.Add("Opadajuce: Prezime");
            korisniciSort.Add("Opadajuce: Korisnicko ime");
            korisniciSort.Add("Opadajuce: Lozinka");
            korisniciSort.Add("Opadajuce: Tip korisnika");

            cbSortiraj.ItemsSource = korisniciSort;
        }

        private void btnSalon_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = SelektovanoZaIzmenu.Salon;

            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
            dgPrikazStavki.CanUserAddRows = false;
            dgPrikazStavki.IsReadOnly = true;
            dgPrikazStavki.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            view = CollectionViewSource.GetDefaultView(Projekat.Instanca.Salon);
            view.Filter = FilterNeobrisanihStavki;
            dgPrikazStavki.ItemsSource = view;

            btnPrikaziStavke.Visibility = Visibility.Hidden;
            btnIzbrisi.Visibility = Visibility.Visible;
            btnIzmeni.Visibility = Visibility.Visible;
            btnProizvodiNaAkciji.Visibility = Visibility.Hidden;

            var salonSort = new List<string>();
            salonSort.Add("Rastuce: Naziv");
            salonSort.Add("Rastuce: Adresa");
            salonSort.Add("Rastuce: Telefon");
            salonSort.Add("Rastuce: Email");
            salonSort.Add("Rastuce: Websajt");
            salonSort.Add("Rastuce: Pib");
            salonSort.Add("Rastuce: Maticni broj");
            salonSort.Add("Rastuce: Broj ziro racuna");
            salonSort.Add("Opadajuce: Naziv");
            salonSort.Add("Opadajuce: Adresa");
            salonSort.Add("Opadajuce: Telefon");
            salonSort.Add("Opadajuce: Email");
            salonSort.Add("Opadajuce: Websajt");
            salonSort.Add("Opadajuce: Pib");
            salonSort.Add("Opadajuce: Maticni broj");
            salonSort.Add("Opadajuce: Broj ziro racuna");

            cbSortiraj.ItemsSource = salonSort;
        }
        #region Dodavanje
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                case SelektovanoZaIzmenu.ProdajaNamestaja:
                    var prodajaNamestaja = new ProdajaNamestaja()
                    {
                        BrojRacuna = "",
                        DatumProdaje = DateTime.Now,
                        Kupac = "",
                        UkupnaCena = 0,
                    };
                    var novaProdaja = new DodajProdajuNamestaja(prodajaNamestaja);
                    novaProdaja.ShowDialog();
                    view.Refresh();
                    break;
                case SelektovanoZaIzmenu.Namestaj:
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
                case SelektovanoZaIzmenu.TipNamestaja:
                    var prazanTipNamestaja = new TipNamestaja()
                    {
                        Naziv = ""
                    };
                    var dodavanjeTipaNamestaja = new DodajIzmeniTipNamestaja(prazanTipNamestaja, DodajIzmeniTipNamestaja.TipOperacije.DODAVANJE);
                    dodavanjeTipaNamestaja.ShowDialog();
                    break;
                case SelektovanoZaIzmenu.DodatneUsluge:
                    var praznaDodatnaUsluga = new DodatneUsluge()
                    {
                        Naziv = "",
                        Iznos = 0
                    };
                    var dodavanjeDodatneUsluge = new DodajIzmeniDodatneUsluge(praznaDodatnaUsluga, DodajIzmeniDodatneUsluge.TipOperacije.DODAVANJE);
                    dodavanjeDodatneUsluge.ShowDialog();
                    break;
                case SelektovanoZaIzmenu.Akcije:
                    var praznaAkcija = new Akcija()
                    {
                        DatumPocetka = DateTime.Today,
                        DatumZavrsetka = DateTime.Today,
                        Popust = 0,
                        NazivAkcije = ""
                    };
                    var dodavanjeAkcije = new DodajAkciju(praznaAkcija);
                    dodavanjeAkcije.ShowDialog();
                    view.Refresh();
                    break;
                case SelektovanoZaIzmenu.Korisnik:
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
                case SelektovanoZaIzmenu.Salon:
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
        #endregion

        #region Izmena
        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                //case SelektovanoZaIzmenu.:
                //    break;
                case SelektovanoZaIzmenu.Namestaj:
                    var izabraniNamestaj = (Namestaj)dgPrikazStavki.SelectedItem;
                    if(izabraniNamestaj != null)
                    {
                        Namestaj kopijaNamestaja = (Namestaj)izabraniNamestaj.Clone();
                        var izmenaNamestaja = new DodajIzmeniNamestaj(kopijaNamestaja, DodajIzmeniNamestaj.TipOperacije.IZMENA);
                        izmenaNamestaja.ShowDialog();
                    }
                    break;
                case SelektovanoZaIzmenu.TipNamestaja:
                    var izabraniTipNamestaja = (TipNamestaja)dgPrikazStavki.SelectedItem;
                    if (izabraniTipNamestaja != null)
                    {
                        TipNamestaja kopijaTipaNamestaja = (TipNamestaja)izabraniTipNamestaja.Clone();
                        var izmenaTipaNamestaja = new DodajIzmeniTipNamestaja(kopijaTipaNamestaja, DodajIzmeniTipNamestaja.TipOperacije.IZMENA);
                        izmenaTipaNamestaja.ShowDialog();
                    }
                    break;
                case SelektovanoZaIzmenu.DodatneUsluge:
                    var izabranaDodatnaUsluga = (DodatneUsluge)dgPrikazStavki.SelectedItem;
                    if(izabranaDodatnaUsluga != null)
                    {
                        DodatneUsluge kopijaDodatneUsluge = (DodatneUsluge)izabranaDodatnaUsluga.Clone();
                        var izmenaDodatneUsluge = new DodajIzmeniDodatneUsluge(kopijaDodatneUsluge, DodajIzmeniDodatneUsluge.TipOperacije.IZMENA);
                        izmenaDodatneUsluge.ShowDialog();
                    }
                    break;
                case SelektovanoZaIzmenu.Akcije:
                    var izabranaAkcija = (Akcija)dgPrikazStavki.SelectedItem;
                    if (izabranaAkcija != null)
                    {
                        Akcija kopijaAkcije = (Akcija)izabranaAkcija.Clone();
                        var izmenaAkcije = new IzmeniAkciju(kopijaAkcije);
                        izmenaAkcije.ShowDialog();
                    }
                    break;
                case SelektovanoZaIzmenu.Korisnik:
                    var izabraniKorisnik = (Korisnik)dgPrikazStavki.SelectedItem;
                    if (izabraniKorisnik != null)
                    {
                        Korisnik kopijaKorisnika = (Korisnik)izabraniKorisnik.Clone();
                        var izmenaKorisnika = new DodajIzmeniKorisnik(kopijaKorisnika, DodajIzmeniKorisnik.TipOperacije.IZMENA);
                        izmenaKorisnika.ShowDialog();
                    }
                    break;
                case SelektovanoZaIzmenu.Salon:
                    var izabraniSalon = (Salon)dgPrikazStavki.SelectedItem;
                    if(izabraniSalon != null)
                    {
                        Salon kopijaSalona = (Salon)izabraniSalon.Clone();
                        var izmenaSalona = new DodajIzmeniSalon(kopijaSalona, DodajIzmeniSalon.TipOperacije.IZMENA);
                        izmenaSalona.ShowDialog();
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Brisanje
        private void btnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                case SelektovanoZaIzmenu.ProdajaNamestaja:
                    var izabranaProdaja = (ProdajaNamestaja)dgPrikazStavki.SelectedItem;
                    if (izabranaProdaja != null)
                    {
                        if (MessageBox.Show("Da li ste sigurno da zelite da izbrisete izabranu prodaju?", "Brisanje prodaje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            foreach (var prodaja in Projekat.Instanca.ProdajaNamestaja)
                            {
                                if (prodaja.Obrisan != true && prodaja.Id == izabranaProdaja.Id)
                                {
                                    ProdajaNamestaja.Delete(prodaja);
                                    view.Refresh();

                                    foreach (var stavkaNamestaj in Projekat.Instanca.StavkaRacunaNamestaj)
                                    {
                                        if (stavkaNamestaj.IdProdajeNamestaja == prodaja.Id && stavkaNamestaj.Obrisan == false)
                                        {
                                            stavkaNamestaj.Obrisan = true;
                                            StavkaRacunaNamestaj.Update(stavkaNamestaj); //brisem i stavku racuna 
                                        }
                                    }
                                    foreach (var stavkaDodatnaUsluga in Projekat.Instanca.StavkaRacunaDodatnaUsluga)
                                    {
                                        if (stavkaDodatnaUsluga.IdProdajeNamestaja == prodaja.Id && stavkaDodatnaUsluga.Obrisan == false)
                                        {
                                            stavkaDodatnaUsluga.Obrisan = true;
                                            StavkaRacunaDodatnaUsluga.Update(stavkaDodatnaUsluga); //brisem i stavku racuna 
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case SelektovanoZaIzmenu.Namestaj:
                    var izabraniNamestaj = (Namestaj)dgPrikazStavki.SelectedItem;
                    if (izabraniNamestaj != null)
                    {
                        if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete namestaj: {izabraniNamestaj.Naziv}", "Brisanje namestaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            foreach (var namestaj in Projekat.Instanca.Namestaj)
                            {
                                if (namestaj.Obrisan != true && namestaj.Id == izabraniNamestaj.Id)
                                {
                                    Namestaj.Delete(namestaj);
                                    view.Refresh();
                                }
                            }
                        }
                    }
                    
                    break;
                case SelektovanoZaIzmenu.TipNamestaja:
                    var izabraniTipNamestaja = (TipNamestaja)dgPrikazStavki.SelectedItem;
                    if(izabraniTipNamestaja != null)
                    {
                        if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete tip namestaja: {izabraniTipNamestaja.Naziv}", "Brisanje tipa namestaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            foreach (var tipNamestaja in Projekat.Instanca.TipoviNamestaja)
                            {
                                if (tipNamestaja.Id == izabraniTipNamestaja.Id)
                                {
                                    foreach (var namestaj in Projekat.Instanca.Namestaj)
                                    {
                                        if (namestaj.TipNamestajaId == izabraniTipNamestaja.Id)
                                        {
                                            Namestaj.Delete(namestaj);
                                        }
                                    }
                                    TipNamestaja.Delete(tipNamestaja);
                                    view.Refresh();                                    
                                }
                            }
                        };
                    }
                    break;
                case SelektovanoZaIzmenu.DodatneUsluge:
                    var izabranaDodatnaUsluga = (DodatneUsluge)dgPrikazStavki.SelectedItem;
                    if (izabranaDodatnaUsluga != null)
                    {
                        if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete dodatnu uslugu: {izabranaDodatnaUsluga.Naziv}", "Brisanje dodatne usluge", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            foreach (var dodatnaUsluga in Projekat.Instanca.DodatneUsluge)
                            {
                                if (dodatnaUsluga.Id == izabranaDodatnaUsluga.Id)
                                {
                                    DodatneUsluge.Delete(dodatnaUsluga);
                                    view.Refresh();
                                }
                            }
                        }
                    }
                    break;
                case SelektovanoZaIzmenu.Akcije:
                    var izabranaAkcija = (Akcija)dgPrikazStavki.SelectedItem;
                    if (izabranaAkcija != null)
                    {
                        if (MessageBox.Show("Da li ste sigurni da zelite da izbrisete akciju?", "Brisanje akcije", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            foreach (var akcija in Projekat.Instanca.Akcija)
                            {
                                if (akcija.Id == izabranaAkcija.Id)
                                {
                                    Akcija.Delete(akcija);
                                    view.Refresh();

                                    foreach (var namestajAkcija in Projekat.Instanca.NamestajNaAkciji) //za cenu 
                                    {
                                        if (namestajAkcija.IdAkcije == akcija.Id && akcija.Obrisan == true)
                                        {
                                            foreach (var namestaj in Projekat.Instanca.Namestaj)
                                            {
                                                if (namestajAkcija.IdNamestaja == namestaj.Id)
                                                {
                                                    namestaj.AkcijskaCena = 0;
                                                    Namestaj.Update(namestaj); //update za namestaj da akcijska cena bude 0 posto akcija vise ne postoji

                                                    namestajAkcija.Obrisan = true;
                                                    NamestajNaAkciji.Update(namestajAkcija); //namestajNaAkciji se brise
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        };
                    }
                    
                    break;
                case SelektovanoZaIzmenu.Korisnik:
                    var izabraniKorisnik = (Korisnik)dgPrikazStavki.SelectedItem;
                    if(izabraniKorisnik != null)
                    {
                        if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete korisnika:{izabraniKorisnik.Ime + " " + izabraniKorisnik.Prezime}", "Brisanje korisnika", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            foreach (var korisnik in Projekat.Instanca.Korisnik)
                            {
                                if (korisnik.Id == izabraniKorisnik.Id)
                                {
                                    Korisnik.Delete(korisnik);
                                    view.Refresh();
                                }
                            }
                        };
                    }
                    break;
                case SelektovanoZaIzmenu.Salon:
                    var izabraniSalon = (Salon)dgPrikazStavki.SelectedItem;
                    if(izabraniSalon != null)
                    {
                        if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete salon:{izabraniSalon.Naziv}", "Brisanje salona", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            foreach (var salon in Projekat.Instanca.Salon)
                            {
                                if (salon.Id == izabraniSalon.Id)
                                {
                                    Salon.Delete(salon);
                                    view.Refresh();
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

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

        #region autogeneratingcolumn
        private void dgPrikazStavki_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) 
        {
            if(e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Obrisan") //izbacivanje
            {
                e.Cancel = true;
            }
            switch (selektovanoZaIzmenu)
            {
                case SelektovanoZaIzmenu.ProdajaNamestaja:
                    if (e.Column.Header.ToString() == "StavkaNaRacunu")
                    {
                        e.Cancel = true;
                    }
                    if (e.Column.Header.ToString() == "CenaBezPdv")
                    {
                        e.Column.Header = "Cena bez pdv";
                    }
                    if (e.Column.Header.ToString() == "DatumProdaje")
                    {
                        e.Column.Header = "Datum prodaje";
                    }
                    if (e.Column.Header.ToString() == "BrojRacuna")
                    {
                        e.Column.Header = "Broj racuna";
                    }
                    if (e.Column.Header.ToString() == "UkupnaCena")
                    {
                        e.Column.Header = "Ukupna cena";
                    }
                    break;
                case SelektovanoZaIzmenu.Namestaj:
                    if (e.Column.Header.ToString() == "AkcijaId" || e.Column.Header.ToString() == "TipNamestajaId" || e.Column.Header.ToString() == "ProdataKolicina") 
                    {
                        e.Cancel = true;
                    }
                    if (e.Column.Header.ToString() == "AkcijskaCena")
                    {
                        e.Column.Header = "Akcijska cena";
                    }
                    if (e.Column.Header.ToString() == "KolicinaUMagacinu")
                    {
                        e.Column.Header = "Kolicina u magacinu";
                    }
                    if (e.Column.Header.ToString() == "TipNamestaja")
                    {
                        e.Column.Header = "Tip namestaja";
                    }
                    break;
                case SelektovanoZaIzmenu.TipNamestaja:
                    break;
                case SelektovanoZaIzmenu.DodatneUsluge:
                    if (e.Column.Header.ToString() == "ProdataKolicina")
                    {
                        e.Cancel = true;
                    }
                    break;
                case SelektovanoZaIzmenu.Akcije:
                    if (e.Column.Header.ToString() == "IdNamestaja" || e.Column.Header.ToString() == "IdNamestajaNaAkciji")
                    {
                        e.Cancel = true;
                    }
                    if (e.Column.Header.ToString() == "DatumPocetka")
                    {
                        e.Column.Header = "Datum pocetka";
                    }
                    if (e.Column.Header.ToString() == "DatumZavrsetka")
                    {
                        e.Column.Header = "Datum zavrsetka";
                    }
                    if (e.Column.Header.ToString() == "NazivAkcije")
                    {
                        e.Column.Header = "Naziv akcije";
                    }
                    break;
                case SelektovanoZaIzmenu.Korisnik:
                    if (e.Column.Header.ToString() == "KorisnickoIme")
                    {
                        e.Column.Header = "Korisnicko ime";
                    }
                    if (e.Column.Header.ToString() == "TipKorisnika")
                    {
                        e.Column.Header = "Tip korisnika";
                    }
                    break;
                case SelektovanoZaIzmenu.Salon:
                    if (e.Column.Header.ToString() == "MaticniBroj")
                    {
                        e.Column.Header = "Maticni broj";
                    }
                    if (e.Column.Header.ToString() == "BrojZiroRacuna")
                    {
                        e.Column.Header = "Broj ziro racuna";
                    }
                    break;
            }
        }
        #endregion

        private void btnPrikaziStavke_Click(object sender, RoutedEventArgs e)
        {
            var izabranaProdaja = (ProdajaNamestaja)dgPrikazStavki.SelectedItem;
            if(izabranaProdaja != null)
            {
                var prikaz = new PrikazRacuna(izabranaProdaja);
                prikaz.ShowDialog();
            }
            

        }

        private void btnProizvodiNaAkciji_Click(object sender, RoutedEventArgs e)
        {
            var izabranaAkcija = (Akcija)dgPrikazStavki.SelectedItem;
            if(izabranaAkcija != null)
            {
                var prikazProizvodaNaAkciji = new PrikazProizvodaNaAkciji(izabranaAkcija);
                prikazProizvodaNaAkciji.ShowDialog();
            }
        }

        #region Pretraga

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            var tekstZaPretragu = tbPretraga.Text;
            switch (selektovanoZaIzmenu)
            {
                case SelektovanoZaIzmenu.ProdajaNamestaja:
                    ObservableCollection<ProdajaNamestaja> pretragaProdaja = ProdajaNamestaja.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaProdaja;
                    tbPretraga.Clear();
                    break;
                case SelektovanoZaIzmenu.Namestaj:
                    ObservableCollection<Namestaj> pretragaNamestaj = Namestaj.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaNamestaj;
                    tbPretraga.Clear();
                    break;
                case SelektovanoZaIzmenu.TipNamestaja:
                    ObservableCollection<TipNamestaja> pretragaTipa = TipNamestaja.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaTipa;
                    tbPretraga.Clear();
                    break;
                case SelektovanoZaIzmenu.DodatneUsluge:
                    ObservableCollection<DodatneUsluge> pretragaDodatne = DodatneUsluge.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaDodatne;
                    tbPretraga.Clear();
                    break;
                case SelektovanoZaIzmenu.Akcije:
                    ObservableCollection<Akcija> pretragaAkcija = Akcija.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaAkcija;
                    tbPretraga.Clear();
                    break;
                case SelektovanoZaIzmenu.Korisnik:
                    ObservableCollection<Korisnik> pretragaKorisnik = Korisnik.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaKorisnik;
                    tbPretraga.Clear();
                    break;
                case SelektovanoZaIzmenu.Salon:
                    ObservableCollection<Salon> pretragaSalon = Salon.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaSalon;
                    tbPretraga.Clear();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Sortiranje
        private void cbSortiraj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                case SelektovanoZaIzmenu.ProdajaNamestaja:
                    var sortProdaja = (string)cbSortiraj.SelectedItem;
                    if(sortProdaja != null)
                    {
                        switch (sortProdaja)
                        {
                            case "Rastuce: Datum prodaje":
                                dgPrikazStavki.ItemsSource = ProdajaNamestaja.Sort("DatumProdaje");
                                break;
                            case "Rastuce: Broj racuna":
                                dgPrikazStavki.ItemsSource = ProdajaNamestaja.Sort("BrojRacuna");
                                break;
                            case "Rastuce: Ukupna cena":
                                dgPrikazStavki.ItemsSource = ProdajaNamestaja.Sort("UkupnaCena");
                                break;
                            case "Rastuce: Cena bez pdv":
                                dgPrikazStavki.ItemsSource = ProdajaNamestaja.Sort("CenaBezPdv");
                                break;
                            case "Rastuce: Kupac":
                                dgPrikazStavki.ItemsSource = ProdajaNamestaja.Sort("Kupac");
                                break;
                            case "Opadajuce: Datum prodaje":
                                dgPrikazStavki.ItemsSource = ProdajaNamestaja.Sort("ODatumProdaje");
                                break;
                            case "Opadajuce: Broj racuna":
                                dgPrikazStavki.ItemsSource = ProdajaNamestaja.Sort("OBrojRacuna");
                                break;
                            case "Opadajuce: Ukupna cena":
                                dgPrikazStavki.ItemsSource = ProdajaNamestaja.Sort("OUkupnaCena");
                                break;
                            case "Opadajuce: Cena bez pdv":
                                dgPrikazStavki.ItemsSource = ProdajaNamestaja.Sort("OCenaBezPdv");
                                break;
                            case "Opadajuce: Kupac":
                                dgPrikazStavki.ItemsSource = ProdajaNamestaja.Sort("OKupac");
                                break;

                            default:
                                break;
                        }
                    }
                    break;
                case SelektovanoZaIzmenu.Namestaj:
                    var sortNamestaj = (string)cbSortiraj.SelectedItem;
                    if (sortNamestaj != null)
                    {
                        switch (sortNamestaj)
                        {
                            case "Rastuce: Naziv":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("Naziv");
                                break;
                            case "Rastuce: Sifra":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("Sifra");
                                break;
                            case "Rastuce: Cena":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("Cena");
                                break;
                            case "Rastuce: Akcijska cena":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("AkcijskaCena");
                                break;
                            case "Rastuce: Kolicina u magacinu":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("Kolicina");
                                break;
                            case "Rastuce: Tip namestaja":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("TipNamestajaId");
                                break;
                            case "Opadajuce: Naziv":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("ONaziv");
                                break;
                            case "Opadajuce: Sifra":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("OSifra");
                                break;
                            case "Opadajuce: Cena":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("OCena");
                                break;
                            case "Opadajuce: Akcijska cena":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("OAkcijskaCena");
                                break;
                            case "Opadajuce: Kolicina u magacinu":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("OKolicina");
                                break;
                            case "Opadajuce: Tip namestaja":
                                dgPrikazStavki.ItemsSource = Namestaj.Sort("OTipNamestajaId");
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case SelektovanoZaIzmenu.TipNamestaja:
                    var sortTip = (string)cbSortiraj.SelectedItem;
                    if(sortTip != null)
                    {
                        switch (sortTip)
                        {
                            case "Rastuce: Naziv":
                                dgPrikazStavki.ItemsSource = TipNamestaja.Sort("Naziv");
                                break;
                            case "Opadajuce: Naziv":
                                dgPrikazStavki.ItemsSource = TipNamestaja.Sort("ONaziv");
                                break;

                        }
                    }
                    break;
                case SelektovanoZaIzmenu.DodatneUsluge:
                    var sortDodatne = (string)cbSortiraj.SelectedItem;
                    if(sortDodatne != null)
                    {
                        switch (sortDodatne)
                        {
                            case "Rastuce: Naziv":
                                dgPrikazStavki.ItemsSource = DodatneUsluge.Sort("Naziv");
                                break;
                            case "Rastuce: Iznos":
                                dgPrikazStavki.ItemsSource = DodatneUsluge.Sort("Iznos");
                                break;
                            case "Opadajuce: Naziv":
                                dgPrikazStavki.ItemsSource = DodatneUsluge.Sort("ONaziv");
                                break;
                            case "Opadajuce: Iznos":
                                dgPrikazStavki.ItemsSource = DodatneUsluge.Sort("OIznos");
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case SelektovanoZaIzmenu.Akcije:
                    var sortAkcija = (string)cbSortiraj.SelectedItem;
                    if(sortAkcija != null)
                    {
                        switch (sortAkcija)
                        {
                            case "Rastuce: Datum pocetka":
                                dgPrikazStavki.ItemsSource = Akcija.Sort("DatumPocetka");
                                break;
                            case "Rastuce: Datum zavrsetka":
                                dgPrikazStavki.ItemsSource = Akcija.Sort("DatumZavrsetka");
                                break;
                            case "Rastuce: Popust":
                                dgPrikazStavki.ItemsSource = Akcija.Sort("Popust");
                                break;
                            case "Rastuce: Naziv akcije":
                                dgPrikazStavki.ItemsSource = Akcija.Sort("NazivAkcije");
                                break;
                            case "Opadajuce: Datum pocetka":
                                dgPrikazStavki.ItemsSource = Akcija.Sort("ODatumPocetka");
                                break;
                            case "Opadajuce: Datum zavrsetka":
                                dgPrikazStavki.ItemsSource = Akcija.Sort("ODatumZavrsetka");
                                break;
                            case "Opadajuce: Popust":
                                dgPrikazStavki.ItemsSource = Akcija.Sort("OPopust");
                                break;
                            case "Opadajuce: Naziv akcije":
                                dgPrikazStavki.ItemsSource = Akcija.Sort("ONazivAkcije");
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case SelektovanoZaIzmenu.Korisnik:
                    var korisniciSort = (string)cbSortiraj.SelectedItem;
                    if (korisniciSort != null)
                    {
                        switch (korisniciSort)
                        {
                            case "Rastuce: Ime":
                                dgPrikazStavki.ItemsSource = Korisnik.Sort("Ime");
                                break;
                            case "Rastuce: Prezime":
                                dgPrikazStavki.ItemsSource = Korisnik.Sort("Prezime");
                                break;
                            case "Rastuce: Korisnicko ime":
                                dgPrikazStavki.ItemsSource = Korisnik.Sort("KorisnickoIme");
                                break;
                            case "Rastuce: Lozinka":
                                dgPrikazStavki.ItemsSource = Korisnik.Sort("Lozinka");
                                break;
                            case "Rastuce: Tip korisnika":
                                dgPrikazStavki.ItemsSource = Korisnik.Sort("TipKorisnika");
                                break;
                            case "Opadajuce: Ime":
                                dgPrikazStavki.ItemsSource = Korisnik.Sort("OIme");
                                break;
                            case "Opadajuce: Prezime":
                                dgPrikazStavki.ItemsSource = Korisnik.Sort("OPrezime");
                                break;
                            case "Opadajuce: Korisnicko ime":
                                dgPrikazStavki.ItemsSource = Korisnik.Sort("OKorisnickoIme");
                                break;
                            case "Opadajuce: Lozinka":
                                dgPrikazStavki.ItemsSource = Korisnik.Sort("OLozinka");
                                break;
                            case "Opadajuce: Tip korisnika":
                                dgPrikazStavki.ItemsSource = Korisnik.Sort("OTipKorisnika");
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case SelektovanoZaIzmenu.Salon:
                    var salonSort = (string)cbSortiraj.SelectedItem;
                    if (salonSort != null)
                    {
                        switch (salonSort)
                        {
                            case "Rastuce: Naziv":
                                dgPrikazStavki.ItemsSource = Salon.Sort("Naziv");
                                break;
                            case "Rastuce: Adresa":
                                dgPrikazStavki.ItemsSource = Salon.Sort("Adresa");
                                break;
                            case "Rastuce: Telefon":
                                dgPrikazStavki.ItemsSource = Salon.Sort("Telefon");
                                break;
                            case "Rastuce: Email":
                                dgPrikazStavki.ItemsSource = Salon.Sort("Email");
                                break;
                            case "Rastuce: Websajt":
                                dgPrikazStavki.ItemsSource = Salon.Sort("Websajt");
                                break;
                            case "Rastuce: Pib":
                                dgPrikazStavki.ItemsSource = Salon.Sort("Pib");
                                break;
                            case "Rastuce: Maticni broj":
                                dgPrikazStavki.ItemsSource = Salon.Sort("MaticniBroj");
                                break;
                            case "Rastuce: Broj ziro racuna":
                                dgPrikazStavki.ItemsSource = Salon.Sort("BrojZiroRacuna");
                                break;
                            case "Opadajuce: Rastuce: Naziv":
                                dgPrikazStavki.ItemsSource = Salon.Sort("ONaziv");
                                break;
                            case "Opadajuce: Adresa":
                                dgPrikazStavki.ItemsSource = Salon.Sort("OAdresa");
                                break;
                            case "Opadajuce: Telefon":
                                dgPrikazStavki.ItemsSource = Salon.Sort("OTelefon");
                                break;
                            case "Opadajuce: Email":
                                dgPrikazStavki.ItemsSource = Salon.Sort("OEmail");
                                break;
                            case "Opadajuce: Websajt":
                                dgPrikazStavki.ItemsSource = Salon.Sort("OWebsajt");
                                break;
                            case "Opadajuce: Pib":
                                dgPrikazStavki.ItemsSource = Salon.Sort("OPib");
                                break;
                            case "Opadajuce: Maticni broj":
                                dgPrikazStavki.ItemsSource = Salon.Sort("OMaticniBroj");
                                break;
                            case "Opadajuce: Broj ziro racuna":
                                dgPrikazStavki.ItemsSource = Salon.Sort("OBrojZiroRacuna");
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion 


        private void ProveriAkcije()
        {
            foreach (var akcija in Projekat.Instanca.Akcija)
            {
                if (akcija.Obrisan == false && akcija.DatumZavrsetka.Date < DateTime.Now.Date)
                {
                    foreach (var namestajAkcija in Projekat.Instanca.NamestajNaAkciji)
                    {
                        if (namestajAkcija.Obrisan == false && namestajAkcija.IdAkcije == akcija.Id)
                        {
                            foreach (var namestaj in Projekat.Instanca.Namestaj)
                            {
                                if (namestaj.Obrisan == false && namestajAkcija.IdNamestaja == namestaj.Id && namestaj.AkcijskaCena != 0)
                                {
                                    namestaj.AkcijskaCena = 0;
                                    Namestaj.Update(namestaj);

                                    NamestajNaAkciji.Delete(namestajAkcija);

                                    Akcija.Delete(akcija);
                                }
                            }
                        }
                    }
                }
            }
        }
    };
}


