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
    class NamestajNaAkciji : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private int idAkcije;
        private int idNamestaja;
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
        public int IdNamestaja
        {
            get { return idNamestaja; }
            set
            {
                idNamestaja = value;
                OnPropertyChanged("IdNamestaja");
            }
        }

        public int IdAkcije
        {
            get { return idAkcije; }
            set
            {
                idAkcije = value;
                OnPropertyChanged("IdAkcije");
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


        public object Clone()
        {
            NamestajNaAkciji kopija = new NamestajNaAkciji();
            kopija.IdAkcije = IdAkcije;
            kopija.IdNamestaja = IdNamestaja;
            kopija.Obrisan = Obrisan;
            return kopija;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Database
        
        public static ObservableCollection<NamestajNaAkciji> GetAll()
        {
            var ucitanNamestajNaAkciji = new ObservableCollection<NamestajNaAkciji>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM NamestajNaAkciji WHERE Obrisan = 0;";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "NamestajNaAkciji"); //izvrsava se query
                foreach (DataRow row in ds.Tables["NamestajNaAkciji"].Rows)
                {
                    var namestajNaAkciji = new NamestajNaAkciji();
                    namestajNaAkciji.Id = int.Parse(row["Id"].ToString());
                    namestajNaAkciji.idAkcije = int.Parse(row["IdAkcije"].ToString());
                    namestajNaAkciji.IdNamestaja = int.Parse(row["IdNamestaja"].ToString());

                    ucitanNamestajNaAkciji.Add(namestajNaAkciji);
                }
            }
            return ucitanNamestajNaAkciji;
        }

        public static NamestajNaAkciji Create(NamestajNaAkciji namestajNaAkciji)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO NamestajNaAkciji(IdAkcije, IdNamestaja) VALUES (@IdAkcije, @IdNamestaja);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("IdAkcije", namestajNaAkciji.IdAkcije);
                cmd.Parameters.AddWithValue("IdNamestaja", namestajNaAkciji.IdNamestaja);

                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                namestajNaAkciji.Id = newId;
            }
            Projekat.Instanca.NamestajNaAkciji.Add(namestajNaAkciji); //azuriram i stanje modela
            return namestajNaAkciji;
        }

        public static void Update(NamestajNaAkciji namestajNaAkciji)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE NamestajNaAkciji SET IdAkcije = @IdAkcije, IdNamestaja = @IdNamestaja, Obrisan = @Obrisan WHERE Id = @Id;";
                cmd.Parameters.AddWithValue("Id", namestajNaAkciji.Id);
                cmd.Parameters.AddWithValue("IdAkcije", namestajNaAkciji.IdAkcije);
                cmd.Parameters.AddWithValue("IdNamestaja", namestajNaAkciji.IdNamestaja);
                cmd.Parameters.AddWithValue("Obrisan", namestajNaAkciji.Obrisan);

                cmd.ExecuteNonQuery();

                //azuriram i stanje modela
                foreach (var n in Projekat.Instanca.NamestajNaAkciji)
                {
                    if (n.Id == namestajNaAkciji.Id)
                    {
                        n.IdAkcije = namestajNaAkciji.IdAkcije;
                        n.IdNamestaja = namestajNaAkciji.IdNamestaja;
                        n.Obrisan = namestajNaAkciji.Obrisan;
                    }
                }

            }
        }

        public static void Delete(NamestajNaAkciji namestajNaAkciji)
        {
            namestajNaAkciji.Obrisan = true;
            Update(namestajNaAkciji);
        }
        #endregion

    }
}
