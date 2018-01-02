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
    public class DodatneUsluge : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private string naziv;
        private double iznos;
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

        public double Iznos
        {
            get { return iznos; }
            set
            {
                iznos = value;
                OnPropertyChanged("Iznos");
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

        public static DodatneUsluge PronadjiDodatnuUsluguPoId(int Id)
        {
            foreach (var DodatneUsluge in Projekat.Instanca.DodatneUsluge)
            {
                if (DodatneUsluge.Id == Id)
                {
                    return DodatneUsluge;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return Naziv + "|" + Iznos;
        }

        private int prodataKolicina;
        public int ProdataKolicina
        {
            get { return prodataKolicina; }
            set { prodataKolicina = value; }
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
            DodatneUsluge kopija = new DodatneUsluge();
            kopija.Id = Id;
            kopija.Naziv = Naziv;
            kopija.Iznos = Iznos;
            return kopija;
        }

        #region Database
        public static ObservableCollection<DodatneUsluge> GetAll()
        {
            var ucitaneDodatneUsluge = new ObservableCollection<DodatneUsluge>();
            using(var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM DodatneUsluge WHERE Obrisan=0";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "DodatneUsluge"); //izvrsava se query nad bazom
                foreach (DataRow row in ds.Tables["DodatneUsluge"].Rows)
                {
                    var dodatnaUsluga = new DodatneUsluge();
                    dodatnaUsluga.Id = int.Parse(row["Id"].ToString());
                    dodatnaUsluga.Naziv = row["Naziv"].ToString();
                    dodatnaUsluga.Iznos = double.Parse(row["Iznos"].ToString());
                    ucitaneDodatneUsluge.Add(dodatnaUsluga);
                }
            }
            return ucitaneDodatneUsluge;
        }

        public static DodatneUsluge Create(DodatneUsluge dodatnaUsluga)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO DodatneUsluge(Naziv, Iznos) VALUES (@Naziv, @Iznos);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("Naziv", dodatnaUsluga.Naziv);
                cmd.Parameters.AddWithValue("Iznos", dodatnaUsluga.Iznos);
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScala izvrsava query
                dodatnaUsluga.Id = newId;
            }
            Projekat.Instanca.DodatneUsluge.Add(dodatnaUsluga); //azuriram i stanje modela
            return dodatnaUsluga;
        }

        public static void Update(DodatneUsluge dodatnaUsluga)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"UPDATE DodatneUsluge SET Naziv = @Naziv, Iznos = @Iznos, Obrisan = @Obrisan WHERE Id = @Id;";
                cmd.Parameters.AddWithValue("Id", dodatnaUsluga.Id);
                cmd.Parameters.AddWithValue("Naziv", dodatnaUsluga.Naziv);
                cmd.Parameters.AddWithValue("Iznos", dodatnaUsluga.Iznos);
                cmd.Parameters.AddWithValue("Obrisan", dodatnaUsluga.Obrisan);

                cmd.ExecuteNonQuery();

                //azuriram i stanje modela
                foreach (var du in Projekat.Instanca.DodatneUsluge)
                {
                    if(du.Id == dodatnaUsluga.Id)
                    {
                        du.Naziv = dodatnaUsluga.Naziv;
                        du.Iznos = dodatnaUsluga.Iznos;
                        du.Obrisan = dodatnaUsluga.Obrisan;
                        break;
                    }
                }
            }
        }
        
        public static void Delete(DodatneUsluge dodatnaUsluga)
        {
            dodatnaUsluga.Obrisan = true;
            Update(dodatnaUsluga);
        }
        #endregion

        public static ObservableCollection<DodatneUsluge> Search (string tekstZaPretragu)
        {
            var ucitaneDodatneUsluge = new ObservableCollection<DodatneUsluge>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM DodatneUsluge WHERE Obrisan=0 AND (Naziv LIKE @tekstZaPretragu OR Iznos LIKE @tekstZaPretragu);";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.Parameters.AddWithValue("tekstZaPretragu", '%' + tekstZaPretragu + '%');

                da.SelectCommand = cmd;
                da.Fill(ds, "DodatneUsluge"); //izvrsava se query nad bazom
                foreach (DataRow row in ds.Tables["DodatneUsluge"].Rows)
                {
                    var dodatnaUsluga = new DodatneUsluge();
                    dodatnaUsluga.Id = int.Parse(row["Id"].ToString());
                    dodatnaUsluga.Naziv = row["Naziv"].ToString();
                    dodatnaUsluga.Iznos = double.Parse(row["Iznos"].ToString());
                    ucitaneDodatneUsluge.Add(dodatnaUsluga);
                }
            }
            return ucitaneDodatneUsluge;

        }
    }

}
