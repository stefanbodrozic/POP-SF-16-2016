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
    public class ProdajaNamestaja : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private float PDV = 0.2F;
        private int id;
        private DateTime datumProdaje;
        private string brojRacuna;
        private string kupac;
        private double ukupnaCena;
        private bool obrisan;

        //private ObservableCollection<int> stavkaNaRacunu; //u kolekciju cu ubacivati id stavke!

        //public ObservableCollection<int> StavkaNaRacunu
        //{
        //    get { return stavkaNaRacunu; }
        //    set
        //    {
        //        stavkaNaRacunu = value;
        //        OnPropertyChanged("StavkaNaRacunu");
        //    }
        //}

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public DateTime DatumProdaje
        {
            get { return datumProdaje; }
            set
            {
                datumProdaje = value;
                OnPropertyChanged("DatumProdaje");
            }
        }

        public string BrojRacuna
        {
            get { return brojRacuna; }
            set
            {
                brojRacuna = value;
                OnPropertyChanged("BrojRacuna");
            }
        }

        public string Kupac
        {
            get { return kupac; }
            set
            {
                kupac = value;
                OnPropertyChanged("Kupac");
            }
        }

        public double UkupnaCena
        {
            get { return ukupnaCena; }
            set
            {
                ukupnaCena = value;
                OnPropertyChanged("UkupnaCena");
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
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            ProdajaNamestaja kopija = new ProdajaNamestaja();
            kopija.Id = Id;
            kopija.Kupac = Kupac;
            kopija.BrojRacuna = BrojRacuna;
            kopija.DatumProdaje = DatumProdaje;
            //kopija.StavkaNaRacunu = StavkaNaRacunu;
            kopija.UkupnaCena = UkupnaCena;
            kopija.Obrisan = Obrisan;
            return kopija;
        }

        #region Database

        public static ObservableCollection<ProdajaNamestaja> GetAll()
        {
            var ucitaneProdaje = new ObservableCollection<ProdajaNamestaja>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM ProdajaNamestaja WHERE Obrisan=0";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "ProdajaNamestaja"); //izvrsava se query nad bazom
                foreach (DataRow row in ds.Tables["ProdajaNamestaja"].Rows)
                {
                    var prodaja = new ProdajaNamestaja();
                    prodaja.Id = int.Parse(row["Id"].ToString());
                    prodaja.PDV = float.Parse(row["Pdv"].ToString());
                    prodaja.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                    prodaja.BrojRacuna = row["BrojRacuna"].ToString();
                    prodaja.Kupac = row["Kupac"].ToString();
                    prodaja.UkupnaCena = double.Parse(row["UkupnaCena"].ToString());

                    ucitaneProdaje.Add(prodaja);
                }
            }
            return ucitaneProdaje;
        }

        public static ProdajaNamestaja Create(ProdajaNamestaja prodaja)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO ProdajaNamestaja (Pdv, DatumProdaje, BrojRacuna, Kupac, UkupnaCena) VALUES (@Pdv, @DatumProdaje, @BrojRacuna, @Kupac, @UkupnaCena);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Pdv", prodaja.PDV);
                cmd.Parameters.AddWithValue("DatumProdaje", prodaja.DatumProdaje);
                cmd.Parameters.AddWithValue("BrojRacuna", prodaja.BrojRacuna);
                cmd.Parameters.AddWithValue("Kupac", prodaja.Kupac);
                cmd.Parameters.AddWithValue("UkupnaCena", prodaja.UkupnaCena);
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                prodaja.Id = newId;
            }
            Projekat.Instanca.ProdajaNamestaja.Add(prodaja); //azuriram i stanje modela
            return prodaja;
        }

        public static void Update(ProdajaNamestaja prodaja)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE ProdajaNamestaja SET Pdv = @Pdv, DatumProdaje = @DatumProdaje, BrojRacuna = @BrojRacuna, Kupac = @Kupac, UkupnaCena = @UkupnaCena, Obrisan = @Obrisan WHERE Id = @Id;";
                cmd.Parameters.AddWithValue("Id", prodaja.Id);
                cmd.Parameters.AddWithValue("Pdv", prodaja.PDV);
                cmd.Parameters.AddWithValue("DatumProdaje", prodaja.DatumProdaje);
                cmd.Parameters.AddWithValue("BrojRacuna", prodaja.BrojRacuna);
                cmd.Parameters.AddWithValue("Kupac", prodaja.Kupac);
                cmd.Parameters.AddWithValue("UkupnaCena", prodaja.UkupnaCena);
                cmd.Parameters.AddWithValue("Obrisan", prodaja.Obrisan);

                cmd.ExecuteNonQuery();

                //azuriram stanje modela
                foreach (var p in Projekat.Instanca.ProdajaNamestaja)
                {
                    if(p.Id == prodaja.Id)
                    {
                        p.PDV = prodaja.PDV;
                        p.DatumProdaje = prodaja.DatumProdaje;
                        p.BrojRacuna = prodaja.BrojRacuna;
                        p.Kupac = prodaja.Kupac;
                        p.UkupnaCena = prodaja.UkupnaCena;
                        p.Obrisan = prodaja.Obrisan;
                        break;
                    }
                }

            }
        }

        public static void Delete(ProdajaNamestaja prodaja)
        {
            prodaja.Obrisan = true;
            Update(prodaja);
        }
        #endregion
    }
}
