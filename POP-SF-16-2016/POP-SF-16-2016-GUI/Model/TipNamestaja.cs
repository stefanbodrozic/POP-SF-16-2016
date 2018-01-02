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
    public class TipNamestaja : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private string naziv;
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

        public bool Obrisan
        {
            get { return obrisan; }
            set
            {
                obrisan = value;
                OnPropertyChanged("Obrisan");
            }
        }


        public static TipNamestaja PronadjiTipNamestajaPoId(int id)
        {
            foreach (var tipNamestaja in Projekat.Instanca.TipoviNamestaja)
            {
                if (tipNamestaja.Id == id)
                {
                    return tipNamestaja;
                }
            }
            return null;
            //return Projekat.Instanca.TipoviNamestaja.SingleOrDefault(x => x.Id == id); //isto kao for petlja....
        }

        public override string ToString()
        {
            return Naziv;
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
            TipNamestaja kopija = new TipNamestaja();
            kopija.Id = Id;
            kopija.Naziv = Naziv;
            return kopija;
        }

        #region Database
        public static ObservableCollection<TipNamestaja> GetAll()
        {
            var ucitaniTipoviNamestaja = new ObservableCollection<TipNamestaja>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) //sve sto je ovde definisano je samo tu i vidljivo 
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM TipNamestaja WHERE Obrisan=0";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "TipNamestaja");  //izvrsava se query nad bazom
                foreach (DataRow row in ds.Tables["TipNamestaja"].Rows)
                {
                    var tipNamestaja = new TipNamestaja();
                    tipNamestaja.Id = int.Parse(row["Id"].ToString());
                    tipNamestaja.Naziv = row["Naziv"].ToString();
                    tipNamestaja.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    ucitaniTipoviNamestaja.Add(tipNamestaja);
                }
            }
            return ucitaniTipoviNamestaja;
        }

        public static TipNamestaja Create(TipNamestaja tipNamestaja)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO TipNamestaja (Naziv) VALUES (@Naziv);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Naziv", tipNamestaja.Naziv);
                
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                tipNamestaja.Id = newId;
            }
            Projekat.Instanca.TipoviNamestaja.Add(tipNamestaja); //azuriram i stanje modela
            return tipNamestaja;
        }

        public static void Update(TipNamestaja tipNamestaja)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE TipNamestaja SET Naziv=@Naziv, Obrisan=@Obrisan WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", tipNamestaja.Id);
                cmd.Parameters.AddWithValue("Naziv", tipNamestaja.Naziv);
                cmd.Parameters.AddWithValue("Obrisan", tipNamestaja.Obrisan);

                cmd.ExecuteNonQuery();

                //azuriram stanje modela
                foreach (var tn in Projekat.Instanca.TipoviNamestaja)
                {
                    if (tn.Id == tipNamestaja.Id)
                    {
                        tn.Naziv = tipNamestaja.Naziv;
                        tn.Obrisan = tipNamestaja.Obrisan;
                        break;
                    }
                }
            }
        }

        public static void Delete(TipNamestaja tipNamestaja)
        {
            tipNamestaja.Obrisan = true;
            Update(tipNamestaja);
        }

        public static ObservableCollection<TipNamestaja> Search(string tekstZaPretragu)
        {
            var ucitaniTipovi = new ObservableCollection<TipNamestaja>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString)) //sve sto je ovde definisano je samo tu i vidljivo 
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM TipNamestaja WHERE Obrisan=0 AND Naziv like @tekstZaPretragu;";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.Parameters.AddWithValue("tekstZaPretragu", '%' + tekstZaPretragu + '%');

                da.SelectCommand = cmd;
                da.Fill(ds, "TipNamestaja");  //izvrsava se query nad bazom
                foreach (DataRow row in ds.Tables["TipNamestaja"].Rows)
                {
                    var tipNamestaja = new TipNamestaja();
                    tipNamestaja.Id = int.Parse(row["Id"].ToString());
                    tipNamestaja.Naziv = row["Naziv"].ToString();
                    tipNamestaja.Obrisan = bool.Parse(row["Obrisan"].ToString());

                    ucitaniTipovi.Add(tipNamestaja);
                }
            }
            return ucitaniTipovi;
        }
        #endregion
    }
}
