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
    public class Salon : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private string naziv;
        private string adresa;
        private string telefon;
        private string email;
        private string websajt;
        private int pib;
        private int maticniBroj;
        private string brojZiroRacuna;
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

        public string Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        public string Adresa
        {
            get { return adresa; }
            set
            {
                adresa = value;
                OnPropertyChanged("Adresa");
            }
        }

        public string Telefon
        {
            get { return telefon; }
            set
            {
                telefon = value;
                OnPropertyChanged("Telefon");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public int Pib
        {
            get { return pib; }
            set
            {
                pib = value;
                OnPropertyChanged("Pib");
            }
        }


        public string Websajt
        {
            get { return websajt; }
            set
            {
                websajt = value;
                OnPropertyChanged("Websajt");
            }
        }

        public int MaticniBroj
        {
            get { return maticniBroj; }
            set
            {
                maticniBroj = value;
                OnPropertyChanged("MaticniBroj");
            }
        }

        public string BrojZiroRacuna
        {
            get { return brojZiroRacuna; }
            set
            {
                brojZiroRacuna = value;
                OnPropertyChanged("BrojZiroRacuna");
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
            return Naziv + "|" + Adresa + "|" + Telefon + "|" + Websajt;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            Salon kopija = new Salon();
            kopija.Id = Id;
            kopija.Naziv = Naziv;
            kopija.Adresa = Adresa;
            kopija.Telefon = Telefon;
            kopija.Email = Email;
            kopija.Websajt = Websajt;
            kopija.Pib = Pib;
            kopija.MaticniBroj = MaticniBroj;
            kopija.BrojZiroRacuna = BrojZiroRacuna;
            return kopija;
        }

        #region Database

        public static ObservableCollection<Salon> GetAll()
        {
            var ucitaniSaloni = new ObservableCollection<Salon>();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Salon WHERE Obrisan = 0;";

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                    da.Fill(ds, "Salon"); //izvrsava se query nad bazom
                    foreach (DataRow row in ds.Tables["Salon"].Rows)
                    {
                        var salon = new Salon();
                        salon.Id = int.Parse(row["Id"].ToString());
                        salon.Naziv = row["Naziv"].ToString();
                        salon.Adresa = row["Adresa"].ToString();
                        salon.Telefon = row["Telefon"].ToString();
                        salon.Email = row["Email"].ToString();
                        salon.Websajt = row["Websajt"].ToString();
                        salon.Pib = int.Parse(row["Pib"].ToString());
                        salon.MaticniBroj = int.Parse(row["MaticniBroj"].ToString());
                        salon.BrojZiroRacuna = row["BrojZiroRacuna"].ToString();
                        ucitaniSaloni.Add(salon);
                    }
                }
                return ucitaniSaloni;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom ucitavanja podataka!", "Greska", MessageBoxButton.OK);
                return ucitaniSaloni;
            }
            
            
        }

        public static Salon Create(Salon salon)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Salon (Naziv, Adresa, Telefon, Email, Websajt, Pib, MaticniBroj, BrojZiroRacuna) VALUES (@Naziv, @Adresa, @Telefon, @Email, @Websajt, @Pib, @MaticniBroj, @BrojZiroRacuna);";
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.AddWithValue("Naziv", salon.Naziv);
                    cmd.Parameters.AddWithValue("Adresa", salon.Adresa);
                    cmd.Parameters.AddWithValue("Telefon", salon.Telefon);
                    cmd.Parameters.AddWithValue("Email", salon.Email);
                    cmd.Parameters.AddWithValue("Websajt", salon.Websajt);
                    cmd.Parameters.AddWithValue("Pib", salon.Pib);
                    cmd.Parameters.AddWithValue("MaticniBroj", salon.MaticniBroj);
                    cmd.Parameters.AddWithValue("BrojZiroRacuna", salon.BrojZiroRacuna);
                    int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                    salon.Id = newId;
                }
                Projekat.Instanca.Salon.Add(salon); //azuriram i stanje modela
                return salon;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom kreiranja novog salona!", "Greska", MessageBoxButton.OK);
                return salon;
            }
            
        }

        public static void Update(Salon salon)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE Salon SET Naziv = @Naziv, Adresa = @Adresa, Telefon = @Telefon, Email = @Email, Websajt = @Websajt, Pib = @Pib, MaticniBroj = @MaticniBroj, BrojZiroRacuna = @BrojZiroRacuna, Obrisan = @Obrisan WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", salon.Id);
                    cmd.Parameters.AddWithValue("Naziv", salon.Naziv);
                    cmd.Parameters.AddWithValue("Adresa", salon.Adresa);
                    cmd.Parameters.AddWithValue("Telefon", salon.Telefon);
                    cmd.Parameters.AddWithValue("Email", salon.Email);
                    cmd.Parameters.AddWithValue("Websajt", salon.Websajt);
                    cmd.Parameters.AddWithValue("Pib", salon.Pib);
                    cmd.Parameters.AddWithValue("MaticniBroj", salon.MaticniBroj);
                    cmd.Parameters.AddWithValue("BrojZiroRacuna", salon.BrojZiroRacuna);
                    cmd.Parameters.AddWithValue("Obrisan", salon.Obrisan);

                    cmd.ExecuteNonQuery();

                    //azuriram i stanje modela
                    foreach (var s in Projekat.Instanca.Salon)
                    {
                        if (s.Id == salon.Id)
                        {
                            s.Naziv = salon.Naziv;
                            s.Adresa = salon.Adresa;
                            s.Telefon = salon.Telefon;
                            s.Email = salon.Email;
                            s.Websajt = salon.Websajt;
                            s.Pib = salon.Pib;
                            s.MaticniBroj = salon.MaticniBroj;
                            s.BrojZiroRacuna = salon.BrojZiroRacuna;
                            s.Obrisan = salon.Obrisan;
                            break;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom azuriranja salon!", "Greska", MessageBoxButton.OK);
                return;
            }
            
        }

        public static void Delete(Salon salon)
        {
            salon.Obrisan = true;
            Update(salon);
        }

        public static ObservableCollection<Salon> Search(string tekstZaPretragu)
        {
            var ucitaniSaloni = new ObservableCollection<Salon>();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Salon WHERE Obrisan = 0 AND (Naziv LIKE @tekstZaPretragu OR Adresa LIKE @tekstZaPretragu OR Telefon LIKE @tekstZaPretragu OR Email LIKE @tekstZaPretragu OR Websajt LIKE @tekstZaPretragu OR Pib LIKE @tekstZaPretragu OR MaticniBroj LIKE @tekstZaPretragu OR BrojZiroRacuna LIKE @tekstZaPretragu);";

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    cmd.Parameters.AddWithValue("tekstZaPretragu", '%' + tekstZaPretragu + '%');

                    da.SelectCommand = cmd;
                    da.Fill(ds, "Salon"); //izvrsava se query nad bazom
                    foreach (DataRow row in ds.Tables["Salon"].Rows)
                    {
                        var salon = new Salon();
                        salon.Id = int.Parse(row["Id"].ToString());
                        salon.Naziv = row["Naziv"].ToString();
                        salon.Adresa = row["Adresa"].ToString();
                        salon.Telefon = row["Telefon"].ToString();
                        salon.Email = row["Email"].ToString();
                        salon.Websajt = row["Websajt"].ToString();
                        salon.Pib = int.Parse(row["Pib"].ToString());
                        salon.MaticniBroj = int.Parse(row["MaticniBroj"].ToString());
                        salon.BrojZiroRacuna = row["BrojZiroRacuna"].ToString();
                        ucitaniSaloni.Add(salon);
                    }
                }
                return ucitaniSaloni;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom pretrage salona!", "Greska", MessageBoxButton.OK);
                return ucitaniSaloni;
            }
        }
        #endregion
    }
}