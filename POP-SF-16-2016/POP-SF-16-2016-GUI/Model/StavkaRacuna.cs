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
    public class StavkaRacuna: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int idStavkeRacuna;
        private int idProdajeNamestaja;
        private int idNamestaja;
        private int kolicinaNamestaja;
        private int idDodatneUsluge;
        private int kolicinaDodatnihUsluga;
        private bool obrisan;

        public int IdStavkeRacuna
        {
            get { return idStavkeRacuna; }
            set
            {
                idStavkeRacuna = value;
                OnPropertyChanged("IdStavkeRacuna");
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

        public int KolicinaNamestaja
        {
            get { return kolicinaNamestaja; }
            set
            {
                kolicinaNamestaja = value;
                OnPropertyChanged("Kolicina");
            }
        }

        public int IdDodatneUsluge
        {
            get { return idDodatneUsluge; }
            set
            {
                idDodatneUsluge = value;
                OnPropertyChanged("IdDodatneUsluge");
            }
        }

        public int KolicinaDodatnihUsluga
        {
            get { return kolicinaDodatnihUsluga; }
            set
            {
                kolicinaDodatnihUsluga = value;
                OnPropertyChanged("KolicinaDodatnihUsluga");
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
            return IdNamestaja + " " + KolicinaNamestaja + " " + IdDodatneUsluge + " " + KolicinaDodatnihUsluga;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Database

        public static ObservableCollection<StavkaRacuna> GetAll()
        {
            var ucitaneStavke = new ObservableCollection<StavkaRacuna>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM StavkaRacuna WHERE Obrisan = 0;";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "StavkaRacuna"); //izvrsava se query nad bazom
                foreach (DataRow row in ds.Tables["StavkaRacuna"].Rows)
                {
                    var stavka = new StavkaRacuna();
                    stavka.IdStavkeRacuna = int.Parse(row["Id"].ToString());
                    stavka.IdProdajeNamestaja = int.Parse(row["IdProdaje"].ToString());
                    stavka.IdNamestaja = int.Parse(row["IdNamestaja"].ToString());
                    stavka.KolicinaNamestaja = int.Parse(row["KolicinaNamestaja"].ToString());
                    stavka.IdDodatneUsluge = int.Parse(row["IdDodatneUsluge"].ToString());
                    stavka.KolicinaDodatnihUsluga = int.Parse(row["KoliicnaDodatneUsluge"].ToString());

                    ucitaneStavke.Add(stavka);
                }
            }
            return ucitaneStavke;
        }

        public static StavkaRacuna Create (StavkaRacuna stavka)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO StavkaRacuna (IdProdaje, IdNamestaja, KolicinaNamestaja, IdDodatneUsluge, KolicinaDodatneUsluge) VALUES (@IdProdaje, @IdNamestaja, @KolicinaNamestaja, @IdDodatneUsluge, @KolicinaDodatneUsluge);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdProdaje", stavka.IdProdajeNamestaja);
                cmd.Parameters.AddWithValue("IdNamestaja", stavka.IdNamestaja);
                cmd.Parameters.AddWithValue("KolicinaNamestaja", stavka.KolicinaNamestaja);
                cmd.Parameters.AddWithValue("IdDodatneUsluge", stavka.IdDodatneUsluge);
                cmd.Parameters.AddWithValue("KolicinaDodatneUsluge", stavka.KolicinaDodatnihUsluga);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                stavka.IdStavkeRacuna = newId;
            }
            Projekat.Instanca.StavkaRacuna.Add(stavka);
            return stavka;
        }


        public static void Update(StavkaRacuna stavka)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();

                cmd.CommandText = "UPDATE StavkaRacuna SET IdProdaje = @IdProdaje, IdNamestaja = @IdNamestaja, KolicinaNamestaja = @KolicinaNamestaja, IdDodatneUsluge = @IdDodatneUsluge, KolicinaDodatneUsluge = @KolicinaDodatneUsluge WHERE Id = @Id;";
                cmd.Parameters.AddWithValue("Id", stavka.IdStavkeRacuna);
                cmd.Parameters.AddWithValue("IdProdaje", stavka.IdProdajeNamestaja);
                cmd.Parameters.AddWithValue("IdNamestaja", stavka.IdNamestaja);
                cmd.Parameters.AddWithValue("KolicinaNamestaja", stavka.KolicinaNamestaja);
                cmd.Parameters.AddWithValue("IdDodatneUsluge", stavka.IdDodatneUsluge);
                cmd.Parameters.AddWithValue("KolicinaDodatneUsluge", stavka.KolicinaDodatnihUsluga);
                cmd.Parameters.AddWithValue("Obrisan", stavka.Obrisan);

                cmd.ExecuteNonQuery();

                //azuriram i stanje modela
                foreach (var s in Projekat.Instanca.StavkaRacuna)
                {
                    if(s.IdStavkeRacuna == stavka.IdStavkeRacuna)
                    {
                        s.IdProdajeNamestaja = stavka.IdProdajeNamestaja;
                        s.IdNamestaja = stavka.IdNamestaja;
                        s.KolicinaNamestaja = stavka.KolicinaNamestaja;
                        s.IdDodatneUsluge = stavka.IdDodatneUsluge;
                        s.KolicinaDodatnihUsluga = stavka.KolicinaDodatnihUsluga;
                        s.Obrisan = stavka.Obrisan;
                        break;
                    }
                }
            
            }
        }

        public static void Delete(StavkaRacuna stavka)
        {
            stavka.Obrisan = true;
            Update(stavka);
        }
        #endregion
    }
}
