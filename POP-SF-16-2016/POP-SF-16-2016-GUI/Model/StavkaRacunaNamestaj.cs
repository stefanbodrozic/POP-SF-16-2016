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
    class StavkaRacunaNamestaj : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private int idProdajeNamestaja;
        private int idNamestaja;
        private int kolicina;
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

        public int IdProdajeNamestaja
        {
            get { return idProdajeNamestaja; }
            set
            {
                idProdajeNamestaja = value;
                OnPropertyChanged("IdProdajeNamestaja");
            }
        }

        public int IdNamestaja
        {
            get { return idNamestaja; }
            set
            {
                idNamestaja = value;
                OnPropertyChanged("IdNamestaja");
            }
        }

        public int Kolicina
        {
            get { return kolicina; }
            set
            {
                kolicina = value;
                OnPropertyChanged("Kolicina");
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

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Database

        public static ObservableCollection<StavkaRacunaNamestaj> GetAll()
        {
            var ucitaneStavke = new ObservableCollection<StavkaRacunaNamestaj>();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM StavkaRacunaNamestaj WHERE Obrisan = 0;";

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                    da.Fill(ds, "StavkaRacunaNamestaj");
                    foreach (DataRow row in ds.Tables["StavkaRacunaNamestaj"].Rows)
                    {
                        var stavka = new StavkaRacunaNamestaj();
                        stavka.Id = int.Parse(row["Id"].ToString());
                        stavka.IdProdajeNamestaja = int.Parse(row["IdProdaje"].ToString());
                        stavka.IdNamestaja = int.Parse(row["IdNamestaja"].ToString());
                        stavka.Kolicina = int.Parse(row["Kolicina"].ToString());

                        ucitaneStavke.Add(stavka);
                    }
                }
                return ucitaneStavke;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom ucitavanja podataka!", "Greska", MessageBoxButton.OK);
                return ucitaneStavke;
            }
            
            
        }

        public static StavkaRacunaNamestaj Create(StavkaRacunaNamestaj stavka)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO StavkaRacunaNamestaj (IdProdaje, IdNamestaja, Kolicina) VALUES (@IdProdaje, @IdNamestaja, @Kolicina);";
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.AddWithValue("IdProdaje", stavka.IdProdajeNamestaja);
                    cmd.Parameters.AddWithValue("IdNamestaja", stavka.IdNamestaja);
                    cmd.Parameters.AddWithValue("Kolicina", stavka.Kolicina);

                    int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                    stavka.Id = newId;
                }
                Projekat.Instanca.StavkaRacunaNamestaj.Add(stavka);
                return stavka;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom kreiranja nove stavke racuna!", "Greska", MessageBoxButton.OK);
                return stavka;
            }
            
        }

        public static void Update(StavkaRacunaNamestaj stavka)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();

                    cmd.CommandText = "UPDATE StavkaRacunaNamestaj SET IdProdaje = @IdProdaje, IdNamestaja = @IdNamestaja, Kolicina = @Kolicina, Obrisan = @Obrisan WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("Id", stavka.Id);
                    cmd.Parameters.AddWithValue("IdProdaje", stavka.IdProdajeNamestaja);
                    cmd.Parameters.AddWithValue("IdNamestaja", stavka.IdNamestaja);
                    cmd.Parameters.AddWithValue("Kolicina", stavka.Kolicina);
                    cmd.Parameters.AddWithValue("Obrisan", stavka.Obrisan);

                    cmd.ExecuteNonQuery();

                    //azuriram i stanje modela
                    foreach (var s in Projekat.Instanca.StavkaRacunaNamestaj)
                    {
                        if (s.Id == stavka.Id)
                        {
                            s.IdProdajeNamestaja = stavka.IdProdajeNamestaja;
                            s.IdNamestaja = stavka.IdNamestaja;
                            s.Kolicina = stavka.Kolicina;
                            s.Obrisan = stavka.Obrisan;
                            break;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom azuriranja stavke racuna!", "Greska", MessageBoxButton.OK);
                return;
            }
            
        }

        public static void Delete(StavkaRacunaNamestaj stavka)
        {
            stavka.Obrisan = true;
            Update(stavka);
        }
        #endregion
    }
}
