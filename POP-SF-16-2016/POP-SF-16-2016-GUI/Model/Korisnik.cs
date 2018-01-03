using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POP_SF_16_2016_GUI.Model
{
    public enum TipKorisnika
    {
        Administrator,
        Prodavac
    }
    public class Korisnik : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private string ime;
        private string prezime;
        private string korisnickoIme;
        private string lozinka;
        private TipKorisnika tipKorisnika;
        private bool obrisan;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Ime
        {
            get { return ime; }
            set
            {
                ime = value;
                OnPropertyChanged("Ime");
            }
        }

        public string Prezime
        {
            get { return prezime; }
            set
            {
                prezime = value;
                OnPropertyChanged("Prezime");    
            }
        }

        public string KorisnickoIme
        {
            get { return korisnickoIme; }
            set
            {
                korisnickoIme = value;
                OnPropertyChanged("KorisnickoIme");
            }
        }

        public string Lozinka
        {
            get { return lozinka; }
            set
            {
                lozinka = value;
                OnPropertyChanged("Lozinka");
            }
        }

        public TipKorisnika TipKorisnika
        {
            get { return tipKorisnika; }
            set
            {
                tipKorisnika = value;
                OnPropertyChanged("TipKorisnika");
            }
        }

        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }


        public override string ToString()
        {
            return Ime + "|" + Prezime + "|" + KorisnickoIme + "|" + Lozinka;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            Korisnik kopija = new Korisnik();
            kopija.Id = Id;
            kopija.Ime = Ime;
            kopija.Prezime = Prezime;
            kopija.KorisnickoIme = KorisnickoIme;
            kopija.Lozinka = Lozinka;
            kopija.TipKorisnika = TipKorisnika;
            return kopija;
        }

        #region Database
        public static ObservableCollection<Korisnik> GetAll()
        {
            var ucitaniKorisnici = new ObservableCollection<Korisnik>();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Korisnik WHERE Obrisan = 0;";

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                    da.Fill(ds, "Korisnik"); //izvrsava se query nad bazom
                    foreach (DataRow row in ds.Tables["Korisnik"].Rows)
                    {
                        var korisnik = new Korisnik();
                        korisnik.Id = int.Parse(row["Id"].ToString());
                        korisnik.Ime = row["Ime"].ToString();
                        korisnik.Prezime = row["Prezime"].ToString();
                        korisnik.KorisnickoIme = row["KorisnickoIme"].ToString();
                        korisnik.Lozinka = row["Lozinka"].ToString();
                        korisnik.TipKorisnika = (TipKorisnika)Enum.Parse(typeof(TipKorisnika), (row["Tip"].ToString()));
                        ucitaniKorisnici.Add(korisnik);
                    }
                }
                return ucitaniKorisnici;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom ucitavanja podataka!", "Greska", MessageBoxButton.OK);
                return ucitaniKorisnici;
            }
            
        }

        public static Korisnik Create(Korisnik korisnik)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Korisnik (Ime, Prezime, KorisnickoIme, Lozinka, Tip) VALUES (@Ime, @Prezime, @KorisnickoIme, @Lozinka, @Tip);";
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.AddWithValue("Ime", korisnik.Ime);
                    cmd.Parameters.AddWithValue("Prezime", korisnik.Prezime);
                    cmd.Parameters.AddWithValue("KorisnickoIme", korisnik.KorisnickoIme);
                    cmd.Parameters.AddWithValue("Lozinka", korisnik.Lozinka);
                    cmd.Parameters.AddWithValue("Tip", korisnik.TipKorisnika.ToString());

                    int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                    korisnik.Id = newId;
                }
                Projekat.Instanca.Korisnik.Add(korisnik); //azuriram i stanje modela
                return korisnik;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom kreiranja novog korisnika!", "Greska", MessageBoxButton.OK);
                return korisnik;
            }
            
        }
        
        public static void Update(Korisnik korisnik)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE Korisnik SET Ime = @Ime, Prezime = @Prezime, KorisnickoIme = @KorisnickoIme, Lozinka = @Lozinka, Tip = @Tip, Obrisan = @Obrisan WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("Id", korisnik.Id);
                    cmd.Parameters.AddWithValue("Ime", korisnik.Ime);
                    cmd.Parameters.AddWithValue("Prezime", korisnik.Prezime);
                    cmd.Parameters.AddWithValue("KorisnickoIme", korisnik.KorisnickoIme);
                    cmd.Parameters.AddWithValue("Lozinka", korisnik.Lozinka);
                    cmd.Parameters.AddWithValue("Tip", korisnik.TipKorisnika.ToString());
                    cmd.Parameters.AddWithValue("Obrisan", korisnik.Obrisan);

                    cmd.ExecuteNonQuery();

                    //azuriram i stanje modela
                    foreach (var k in Projekat.Instanca.Korisnik)
                    {
                        if (k.Id == korisnik.Id)
                        {
                            k.Ime = korisnik.Ime;
                            k.Prezime = korisnik.Prezime;
                            k.KorisnickoIme = korisnik.KorisnickoIme;
                            k.Lozinka = korisnik.Lozinka;
                            k.TipKorisnika = korisnik.TipKorisnika;
                            k.Obrisan = korisnik.Obrisan;
                            break;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom azuriranja korisnika!", "Greska", MessageBoxButton.OK);
                return;
            }
            
        }

        public static void Delete(Korisnik korisnik)
        {
            korisnik.Obrisan = true;
            Update(korisnik);
        }

        public static ObservableCollection<Korisnik> Search (string tekstZaPretragu)
        {
            var ucitaniKorisnici = new ObservableCollection<Korisnik>();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Korisnik WHERE Obrisan = 0 AND (Ime like @tekstZaPretragu OR Prezime like @tekstZaPretragu OR KorisnickoIme like @tekstZaPretragu OR Lozinka like @tekstZaPretragu OR Tip LIKE @tekstZaPretragu);";

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    cmd.Parameters.AddWithValue("tekstZaPretragu", '%' + tekstZaPretragu + '%');

                    da.SelectCommand = cmd;
                    da.Fill(ds, "Korisnik"); //izvrsava se query nad bazom
                    foreach (DataRow row in ds.Tables["Korisnik"].Rows)
                    {
                        var korisnik = new Korisnik();
                        korisnik.Id = int.Parse(row["Id"].ToString());
                        korisnik.Ime = row["Ime"].ToString();
                        korisnik.Prezime = row["Prezime"].ToString();
                        korisnik.KorisnickoIme = row["KorisnickoIme"].ToString();
                        korisnik.Lozinka = row["Lozinka"].ToString();
                        korisnik.TipKorisnika = (TipKorisnika)Enum.Parse(typeof(TipKorisnika), (row["Tip"].ToString()));
                        ucitaniKorisnici.Add(korisnik);
                    }
                }
                return ucitaniKorisnici;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom pretrage korisnika!", "Greska", MessageBoxButton.OK);
                return ucitaniKorisnici;
            }
            
        }
        #endregion
    }
}
