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

namespace POP_SF_16_2016_GUI.Model
{
    class StavkaRacunaDodatnaUsluga : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private int idProdajeNamestaja;
        private int idDodatneUsluge;
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

        public int IdDodatneUsluge
        {
            get { return idDodatneUsluge; }
            set
            {
                idDodatneUsluge = value;
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

        public static ObservableCollection<StavkaRacunaDodatnaUsluga> GetAll()
        {
            var ucitaneStavke = new ObservableCollection<StavkaRacunaDodatnaUsluga>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM StavkaRacunaDodatnaUsluga WHERE Obrisan = 0;";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "StavkaRacunaDodatnaUsluga");
                foreach (DataRow row in ds.Tables["StavkaRacunaDodatnaUsluga"].Rows)
                {
                    var stavka = new StavkaRacunaDodatnaUsluga();
                    stavka.Id = int.Parse(row["Id"].ToString());
                    stavka.IdProdajeNamestaja = int.Parse(row["IdProdaje"].ToString());
                    stavka.IdDodatneUsluge = int.Parse(row["IdDodatneUsluge"].ToString());
                    stavka.Kolicina = int.Parse(row["Kolicina"].ToString());

                    ucitaneStavke.Add(stavka);
                }
            }
            return ucitaneStavke;
        }

        public static StavkaRacunaDodatnaUsluga Create(StavkaRacunaDodatnaUsluga stavka)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO StavkaRacunaDodatnaUsluga (IdProdaje, IdDodatneUsluge, Kolicina) VALUES (@IdProdaje, @IdDodatneUsluge, @Kolicina);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdProdaje", stavka.IdProdajeNamestaja);
                cmd.Parameters.AddWithValue("IdDodatneUsluge", stavka.IdDodatneUsluge);
                cmd.Parameters.AddWithValue("Kolicina", stavka.Kolicina);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                stavka.Id = newId;

            }
            Projekat.Instanca.StavkaRacunaDodatnaUsluga.Add(stavka);
            return stavka;
        }

        public static void Update(StavkaRacunaDodatnaUsluga stavka)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();

                cmd.CommandText = "UPDATE StavkaRacunaDodatnaUsluga SET IdProdaje = @IdProdaje, IdDodatneUsluge = @IdDodatneUsluge, Kolicina = @Kolicina, Obrisan = @Obrisan WHERE Id = @Id;";

                cmd.Parameters.AddWithValue("Id", stavka.Id);
                cmd.Parameters.AddWithValue("IdProdaje", stavka.IdProdajeNamestaja);
                cmd.Parameters.AddWithValue("IdDodatneUsluge", stavka.IdDodatneUsluge);
                cmd.Parameters.AddWithValue("Kolicina", stavka.Kolicina);
                cmd.Parameters.AddWithValue("Obrisan", stavka.Obrisan);

                cmd.ExecuteNonQuery();

                //azuriram i stanje modela
                foreach (var s in Projekat.Instanca.StavkaRacunaDodatnaUsluga)
                {
                    if (s.Id == stavka.Id)
                    {
                        s.IdProdajeNamestaja = stavka.IdProdajeNamestaja;
                        s.IdDodatneUsluge = stavka.IdDodatneUsluge;
                        s.Kolicina = stavka.Kolicina;
                        s.Obrisan = stavka.Obrisan;
                        break;
                    }
                }
            }
        }

        public static void Delete(StavkaRacunaDodatnaUsluga stavka)
        {
            stavka.Obrisan = true;
            Update(stavka);
        }
        #endregion
    }
}
