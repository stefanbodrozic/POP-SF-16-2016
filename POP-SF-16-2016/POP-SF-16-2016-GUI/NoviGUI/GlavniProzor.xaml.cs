using POP_SF_16_2016_GUI.Model;
using POP_SF_16_2016_GUI.NoviGUI.Akcije;
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
        private int selektovanoZaIzmenu = 0;
        private ICollectionView view;

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
                case 1:
                    if (((ProdajaNamestaja)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case 2:
                    if (((Namestaj)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case 3:
                    if (((TipNamestaja)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case 4:
                    if (((DodatneUsluge)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case 5:
                    if (((Akcija)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case 6:
                    if (((Korisnik)obj).Obrisan == false)
                    {
                        return true; // treba da se prikaze, zadovoljava kriterijum
                    }
                    break;
                case 7:
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
            selektovanoZaIzmenu = 1;
            dgPrikazStavki.ItemsSource = Projekat.Instanca.ProdajaNamestaja;
            dgPrikazStavki.DataContext = this;
            dgPrikazStavki.IsSynchronizedWithCurrentItem = true;
            btnPrikaziStavke.Visibility = Visibility.Visible;
            btnIzmeni.Visibility = Visibility.Hidden;

            dgPrikazStavki.CanUserAddRows = false;
            dgPrikazStavki.IsReadOnly = true;

            dgPrikazStavki.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            view = CollectionViewSource.GetDefaultView(Projekat.Instanca.ProdajaNamestaja);
            view.Filter = FilterNeobrisanihStavki;
            dgPrikazStavki.ItemsSource = view;
            btnProizvodiNaAkciji.Visibility = Visibility.Hidden;
        }

        private void btnNamestaj_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 2;
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
        }

        private void btnTipNamestaja_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 3;
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
        }

        private void btnDodatneUsluge_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 4;
            dgPrikazStavki.ItemsSource = Projekat.Instanca.DodatneUsluge;
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
        }

        private void btnAkcije_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 5;
            dgPrikazStavki.ItemsSource = Projekat.Instanca.Akcija;
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
        }

        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 6;
            dgPrikazStavki.ItemsSource = Projekat.Instanca.Korisnik;
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
        }

        private void btnSalon_Click(object sender, RoutedEventArgs e)
        {
            selektovanoZaIzmenu = 7;
            dgPrikazStavki.ItemsSource = Projekat.Instanca.Salon;
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
                    };
                    var novaProdaja = new DodajProdajuNamestaja(prodajaNamestaja);
                    novaProdaja.ShowDialog();
                    view.Refresh();
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
                        NazivAkcije = ""
                    };
                    var dodavanjeAkcije = new DodajAkciju(praznaAkcija);
                    dodavanjeAkcije.ShowDialog();
                    view.Refresh();
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
                    var izabraniNamestaj = (Namestaj)dgPrikazStavki.SelectedItem;
                    if(izabraniNamestaj != null)
                    {
                        Namestaj kopijaNamestaja = (Namestaj)izabraniNamestaj.Clone();
                        var izmenaNamestaja = new DodajIzmeniNamestaj(kopijaNamestaja, DodajIzmeniNamestaj.TipOperacije.IZMENA);
                        izmenaNamestaja.ShowDialog();
                    }
                    break;
                case 3:
                    var izabraniTipNamestaja = (TipNamestaja)dgPrikazStavki.SelectedItem;
                    if (izabraniTipNamestaja != null)
                    {
                        TipNamestaja kopijaTipaNamestaja = (TipNamestaja)izabraniTipNamestaja.Clone();
                        var izmenaTipaNamestaja = new DodajIzmeniTipNamestaja(kopijaTipaNamestaja, DodajIzmeniTipNamestaja.TipOperacije.IZMENA);
                        izmenaTipaNamestaja.ShowDialog();
                    }
                    break;
                case 4:
                    var izabranaDodatnaUsluga = (DodatneUsluge)dgPrikazStavki.SelectedItem;
                    if(izabranaDodatnaUsluga != null)
                    {
                        DodatneUsluge kopijaDodatneUsluge = (DodatneUsluge)izabranaDodatnaUsluga.Clone();
                        var izmenaDodatneUsluge = new DodajIzmeniDodatneUsluge(kopijaDodatneUsluge, DodajIzmeniDodatneUsluge.TipOperacije.IZMENA);
                        izmenaDodatneUsluge.ShowDialog();
                    }
                    break;
                case 5:
                    var izabranaAkcija = (Akcija)dgPrikazStavki.SelectedItem;
                    if (izabranaAkcija != null)
                    {
                        Akcija kopijaAkcije = (Akcija)izabranaAkcija.Clone();
                        var izmenaAkcije = new IzmeniAkciju(kopijaAkcije);
                        izmenaAkcije.ShowDialog();
                    }
                    break;
                case 6:
                    var izabraniKorisnik = (Korisnik)dgPrikazStavki.SelectedItem;
                    if (izabraniKorisnik != null)
                    {
                        Korisnik kopijaKorisnika = (Korisnik)izabraniKorisnik.Clone();
                        var izmenaKorisnika = new DodajIzmeniKorisnik(kopijaKorisnika, DodajIzmeniKorisnik.TipOperacije.IZMENA);
                        izmenaKorisnika.ShowDialog();
                    }
                    break;
                case 7:
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

        
        
        private void btnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                case 1:
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
                case 2:
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
                case 3:
                    var izabraniTipNamestaja = (TipNamestaja)dgPrikazStavki.SelectedItem;
                    if(izabraniTipNamestaja != null)
                    {
                        if (MessageBox.Show($"Da li ste sigurni da zelite da izbrisete tip namestaja: {izabraniTipNamestaja.Naziv}", "Brisanje tipa namestaja", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            foreach (var tipNamestaja in Projekat.Instanca.TipoviNamestaja)
                            {
                                if (tipNamestaja.Id == izabraniTipNamestaja.Id)
                                {
                                    TipNamestaja.Delete(tipNamestaja);
                                    view.Refresh();

                                    foreach (var namestaj in Projekat.Instanca.Namestaj)
                                    {
                                        if (namestaj.TipNamestajaId == izabraniTipNamestaja.Id)
                                        {
                                            Namestaj.Delete(namestaj);
                                        }
                                    }
                                }
                            }
                        };
                    }
                    break;
                case 4:
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
                case 5:
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

                                    foreach (var namestajAkcija in Projekat.Instanca.NamestajNaAkciji)
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
                case 6:
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
                case 7:
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

        private void dgPrikazStavki_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) 
        {
            if(e.Column.Header.ToString() == "Id" || e.Column.Header.ToString() == "Obrisan") //izbacivanje
            {
                e.Cancel = true;
            }
            switch (selektovanoZaIzmenu)
            {
                case 1:
                    if (e.Column.Header.ToString() == "StavkaNaRacunu")
                    {
                        e.Cancel = true;
                    }
                    break;
                case 2:
                    if (e.Column.Header.ToString() == "AkcijaId" || e.Column.Header.ToString() == "TipNamestajaId" || e.Column.Header.ToString() == "ProdataKolicina") 
                    {
                        e.Cancel = true;
                    }
                    break;
                case 3:
                    break;
                case 4:
                    if (e.Column.Header.ToString() == "ProdataKolicina")
                    {
                        e.Cancel = true;
                    }
                    break;
                case 5:
                    if (e.Column.Header.ToString() == "IdNamestaja" || e.Column.Header.ToString() == "IdNamestajaNaAkciji")
                    {
                        e.Cancel = true;
                    }
                    break;
                case 6:
                    break;
                case 7:
                    break;
            }
        }

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

        private void btnSortiranje_Click(object sender, RoutedEventArgs e)
        {
            switch (selektovanoZaIzmenu)
            {
                case 1:
                    break;

                case 2:
                    break;
                
            }
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            var tekstZaPretragu = tbPretraga.Text;
            switch (selektovanoZaIzmenu)
            {
                case 1:
                    ObservableCollection<ProdajaNamestaja> pretragaProdaja = ProdajaNamestaja.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaProdaja;
                    tbPretraga.Clear();
                    break;
                case 2:
                    ObservableCollection<Namestaj> pretragaNamestaj = Namestaj.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaNamestaj;
                    tbPretraga.Clear();
                    break;
                case 3:
                    ObservableCollection<TipNamestaja> pretragaTipa = TipNamestaja.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaTipa;
                    tbPretraga.Clear();
                    break;
                case 4:
                    ObservableCollection<DodatneUsluge> pretragaDodatne = DodatneUsluge.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaDodatne;
                    tbPretraga.Clear();
                    break;
                case 5:
                    ObservableCollection<Akcija> pretragaAkcija = Akcija.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaAkcija;
                    tbPretraga.Clear();
                    break;
                case 6:
                    ObservableCollection<Korisnik> pretragaKorisnik = Korisnik.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaKorisnik;
                    tbPretraga.Clear();
                    break;
                case 7:
                    ObservableCollection<Salon> pretragaSalon = Salon.Search(tekstZaPretragu);
                    dgPrikazStavki.ItemsSource = pretragaSalon;
                    tbPretraga.Clear();
                    break;
                default:
                    break;
            }
        }
    };
}


