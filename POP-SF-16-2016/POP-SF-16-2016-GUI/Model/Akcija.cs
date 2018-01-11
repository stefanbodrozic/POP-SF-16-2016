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
    public class Akcija : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private DateTime datumPocetka;
        private DateTime datumZavrsetka;
        private decimal popust;
        private string nazivAkcije;
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

        public DateTime DatumPocetka
        {
            get { return datumPocetka; }
            set
            {
                datumPocetka = value;
                OnPropertyChanged("DatumPocetka");
            }
        }

        public DateTime DatumZavrsetka
        {
            get { return datumZavrsetka; }
            set
            {
                datumZavrsetka = value;
                OnPropertyChanged("DatumZavrsetka");
            }
        }

        public decimal Popust
        {
            get { return popust; }
            set
            {
                popust = value;
                OnPropertyChanged("Popust");
            }
        }

        public string NazivAkcije
        {
            get { return nazivAkcije; }
            set
            {
                nazivAkcije = value;
                OnPropertyChanged("NazivAkcije");

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
            return DatumPocetka + "-" + DatumZavrsetka + "|" + Popust;
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
            Akcija kopija = new Akcija();
            kopija.Id = Id;
            kopija.DatumPocetka = DatumPocetka;
            kopija.DatumZavrsetka = DatumZavrsetka;
            kopija.Popust = Popust;
            kopija.NazivAkcije = NazivAkcije;
            return kopija;
        }

        #region Database

        public static ObservableCollection<Akcija> GetAll()
        {
            var ucitaneAkcije = new ObservableCollection<Akcija>();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan = 0";

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                    da.Fill(ds, "Akcija"); //izvrsava se query nad bazom
                    foreach (DataRow row in ds.Tables["Akcija"].Rows)
                    {
                        var akcija = new Akcija();
                        akcija.Id = int.Parse(row["Id"].ToString());
                        akcija.DatumPocetka = DateTime.Parse(row["DatumPocetka"].ToString());
                        akcija.DatumZavrsetka = DateTime.Parse(row["DatumZavrsetka"].ToString());
                        akcija.Popust = decimal.Parse(row["Popust"].ToString());
                        akcija.NazivAkcije = row["NazivAkcije"].ToString();

                        ucitaneAkcije.Add(akcija);
                    }
                }
                return ucitaneAkcije;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom ucitavanja podataka!", "Greska", MessageBoxButton.OK);
                return ucitaneAkcije;
            }
            
        }

        public static Akcija Create(Akcija akcija)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Akcija(DatumPocetka, DatumZavrsetka, Popust, NazivAkcije) VALUES (@DatumPocetka, @DatumZavrsetka, @Popust, @NazivAkcije);";
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.AddWithValue("DatumPocetka", akcija.DatumPocetka);
                    cmd.Parameters.AddWithValue("DatumZavrsetka", akcija.DatumZavrsetka);
                    cmd.Parameters.AddWithValue("Popust", akcija.Popust);
                    cmd.Parameters.AddWithValue("NazivAkcije", akcija.NazivAkcije);

                    int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                    akcija.Id = newId;
                }
                Projekat.Instanca.Akcija.Add(akcija); //azuriram i stanje modela
                return akcija;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom kreiranja nove akcije!", "Greska", MessageBoxButton.OK);
                return akcija;
            }
            
        }

        public static void Update(Akcija akcija)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE Akcija SET DatumPocetka = @DatumPocetka, DatumZavrsetka = @DatumZavrsetka, Popust = @Popust, NazivAkcije = @NazivAkcije, Obrisan = @Obrisan WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("Id", akcija.Id);
                    cmd.Parameters.AddWithValue("DatumPocetka", akcija.DatumPocetka);
                    cmd.Parameters.AddWithValue("DatumZavrsetka", akcija.DatumZavrsetka);
                    cmd.Parameters.AddWithValue("Popust", akcija.Popust);
                    cmd.Parameters.AddWithValue("NazivAkcije", akcija.NazivAkcije);
                    cmd.Parameters.AddWithValue("Obrisan", akcija.Obrisan);

                    cmd.ExecuteNonQuery();

                    //azuriram stanje modela
                    foreach (var a in Projekat.Instanca.Akcija)
                    {
                        if (a.Id == akcija.Id)
                        {
                            a.DatumPocetka = akcija.DatumPocetka;
                            a.DatumZavrsetka = akcija.DatumZavrsetka;
                            a.Popust = akcija.Popust;
                            a.NazivAkcije = akcija.NazivAkcije;
                            a.Obrisan = akcija.Obrisan;
                            break;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom azuriranja akcije!", "Greska", MessageBoxButton.OK);
                return;
            }
            
        }

        public static void Delete(Akcija akcija)
        {
            akcija.obrisan = true;
            Update(akcija);
        }

        public static ObservableCollection<Akcija> Search(string tekstZaPretragu)
        {
            var ucitaneAkcije = new ObservableCollection<Akcija>();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan = 0 AND (DatumPocetka LIKE @tekstZaPretragu OR DatumZavrsetka LIKE @tekstZaPretragu OR Popust LIKE @tekstZaPretragu OR NazivAkcije LIKE @tekstZaPretragu);";

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    cmd.Parameters.AddWithValue("tekstZaPretragu", '%' + tekstZaPretragu + '%');

                    da.SelectCommand = cmd;
                    da.Fill(ds, "Akcija"); //izvrsava se query nad bazom
                    foreach (DataRow row in ds.Tables["Akcija"].Rows)
                    {
                        var akcija = new Akcija();
                        akcija.Id = int.Parse(row["Id"].ToString());
                        akcija.DatumPocetka = DateTime.Parse(row["DatumPocetka"].ToString());
                        akcija.DatumZavrsetka = DateTime.Parse(row["DatumZavrsetka"].ToString());
                        akcija.Popust = decimal.Parse(row["Popust"].ToString());
                        akcija.NazivAkcije = row["NazivAkcije"].ToString();

                        ucitaneAkcije.Add(akcija);
                    }
                }
                return ucitaneAkcije;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom pretrage akcije!", "Greska", MessageBoxButton.OK);
                return ucitaneAkcije;
            }
        }

        public static ObservableCollection<Akcija> Sort(string sortiranje)
        {
            var ucitaneAkcije = new ObservableCollection<Akcija>();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Akcija WHERE Obrisan = 0 ";

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    switch (sortiranje)
                    {
                        case "DatumPocetka":
                            cmd.CommandText += "ORDER BY DatumPocetka;";
                            break;
                        case "DatumZavrsetka":
                            cmd.CommandText += "ORDER BY DatumZavrsetka;";
                            break;
                        case "Popust":
                            cmd.CommandText += "ORDER BY Popust;";
                            break;
                        case "NazivAkcije":
                            cmd.CommandText += "ORDER BY NazivAkcije;";
                            break;
                        case "ODatumPocetka":
                            cmd.CommandText += "ORDER BY DatumPocetka DESC;";
                            break;
                        case "ODatumZavrsetka":
                            cmd.CommandText += "ORDER BY DatumZavrsetka DESC;";
                            break;
                        case "OPopust":
                            cmd.CommandText += "ORDER BY Popust DESC;";
                            break;
                        case "ONazivAkcije":
                            cmd.CommandText += "ORDER BY NazivAkcije DESC;";
                            break;
                        default:
                            break;
                    }

                    da.SelectCommand = cmd;
                    da.Fill(ds, "Akcija"); //izvrsava se query nad bazom
                    foreach (DataRow row in ds.Tables["Akcija"].Rows)
                    {
                        var akcija = new Akcija();
                        akcija.Id = int.Parse(row["Id"].ToString());
                        akcija.DatumPocetka = DateTime.Parse(row["DatumPocetka"].ToString());
                        akcija.DatumZavrsetka = DateTime.Parse(row["DatumZavrsetka"].ToString());
                        akcija.Popust = decimal.Parse(row["Popust"].ToString());
                        akcija.NazivAkcije = row["NazivAkcije"].ToString();

                        ucitaneAkcije.Add(akcija);
                    }
                }
                return ucitaneAkcije;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom ucitavanja podataka!", "Greska", MessageBoxButton.OK);
                return ucitaneAkcije;
            }

        }

        #endregion
    }
}
