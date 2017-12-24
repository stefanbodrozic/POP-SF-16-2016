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
using System.Xml.Serialization;

namespace POP_SF_16_2016_GUI.Model
{
    public class Namestaj : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private string naziv;
        private string sifra;
        private double cena;
        private double akcijskaCena;
        private int kolicinaUMagacinu;
        private int tipNamestajaId;
        private int akcijaId;
        private bool obrisan;

        private TipNamestaja tipNamestaja;

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

        public string Sifra
        {
            get { return sifra; }
            set
            {
                sifra = value;
                OnPropertyChanged("Sifra");
            }
        }
        
        public double Cena
        {
            get { return cena; }
            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }

        public double AkcijskaCena
        {
            get { return akcijskaCena; }
            set
            {
                akcijskaCena = value;
                OnPropertyChanged("AkcijskaCena");
            }
        }

        public int KolicinaUMagacinu
        {
            get { return kolicinaUMagacinu; }
            set
            {
                kolicinaUMagacinu = value;
                OnPropertyChanged("KolicinaUMagacinu");
            }
        }

        public int TipNamestajaId
        {
            get { return tipNamestajaId; }
            set
            {
                tipNamestajaId = value;
                OnPropertyChanged("TipNamestajaId");
            }
        }

        public int AkcijaId
        {
            get { return akcijaId; }
            set
            {
                akcijaId = value;
                OnPropertyChanged("AkcijaId");
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

        [XmlIgnore]
        public TipNamestaja TipNamestaja
        {
            get
            {
                if(tipNamestaja == null)
                {
                    return TipNamestaja.PronadjiTipNamestajaPoId(tipNamestajaId);
                }
                return tipNamestaja;
            }
            set
            {
                tipNamestaja = value;
                TipNamestajaId = tipNamestaja.Id;
                OnPropertyChanged("TipNamestaja");
            }
        }

        public static Namestaj PronadjiNamestajPoId(int Id)
        {
            foreach (var Namestaj in Projekat.Instanca.Namestaj)
            {
                if (Namestaj.Id == Id)
                {
                    return Namestaj;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return Naziv + "|" + Sifra + "|" + Cena + "|" + KolicinaUMagacinu + "|" + TipNamestajaId;
        }

        protected void OnPropertyChanged(string propertyName) //uraditi za svaku klasu modela
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public object Clone()
        {
            Namestaj kopija = new Namestaj();
            kopija.Id = Id;
            kopija.Naziv = Naziv;
            kopija.Sifra = Sifra;
            kopija.Cena = Cena;
            kopija.AkcijskaCena = AkcijskaCena;
            kopija.KolicinaUMagacinu = KolicinaUMagacinu;
            kopija.AkcijaId = AkcijaId;
            kopija.TipNamestajaId = TipNamestajaId;
            kopija.TipNamestaja = TipNamestaja;
            return kopija;
        }

        #region Database
        public static ObservableCollection<Namestaj> GetAll()
        {
            var listaNamestaja = new ObservableCollection<Namestaj>();
            using(var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Namestaj WHERE Obrisan=0";

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;
                da.Fill(ds, "Namestaj"); //izvrsava se query nad bazom
                foreach (DataRow row in ds.Tables["Namestaj"].Rows)
                {
                    var namestaj = new Namestaj();
                    namestaj.Id = int.Parse(row["Id"].ToString());
                    namestaj.Naziv = row["Naziv"].ToString();
                    namestaj.Sifra = row["Sifra"].ToString();
                    namestaj.Cena = double.Parse(row["Cena"].ToString());
                    namestaj.AkcijskaCena = double.Parse(row["AkcijskaCena"].ToString());
                    namestaj.KolicinaUMagacinu = int.Parse(row["Kolicina"].ToString());
                    namestaj.TipNamestajaId = int.Parse(row["TipNamestajaId"].ToString());
                    //dodati akciju
                    listaNamestaja.Add(namestaj);
                }
            }
            return listaNamestaja;
        }

        public static Namestaj Create(Namestaj namestaj)
        {
            using(var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO Namestaj (TipNamestajaId, Naziv, Cena, AkcijskaCena, Sifra, Kolicina) VALUES (@TipNamestajaId, @Naziv, @Cena, @AkcijskaCena, @Sifra, @Kolicina);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                cmd.Parameters.AddWithValue("TipNamestajaId", namestaj.TipNamestajaId);
                cmd.Parameters.AddWithValue("Naziv", namestaj.Naziv);
                cmd.Parameters.AddWithValue("Cena", namestaj.Cena);
                cmd.Parameters.AddWithValue("AkcijskaCena", namestaj.AkcijskaCena);
                cmd.Parameters.AddWithValue("Sifra", namestaj.Sifra);
                cmd.Parameters.AddWithValue("Kolicina", namestaj.KolicinaUMagacinu);
                //dodati akciju
                int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                namestaj.Id = newId;
            }
            Projekat.Instanca.Namestaj.Add(namestaj); //azuriram i stanje modela
            return namestaj;
        }

        public static void Update(Namestaj namestaj)
        {
            using(var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"UPDATE Namestaj SET TipNamestajaId = @TipNamestajaId, Naziv = @Naziv, Cena = @Cena, AkcijskaCena = @AkcijskaCena, Sifra = @Sifra, Kolicina = @Kolicina, Obrisan = @Obrisan WHERE Id = @Id;";
                cmd.Parameters.AddWithValue("Id", namestaj.Id);
                cmd.Parameters.AddWithValue("TipNamestajaId", namestaj.TipNamestajaId);
                cmd.Parameters.AddWithValue("Naziv", namestaj.Naziv);
                cmd.Parameters.AddWithValue("Cena", namestaj.Cena);
                cmd.Parameters.AddWithValue("AkcijskaCena", namestaj.AkcijskaCena);
                cmd.Parameters.AddWithValue("Sifra", namestaj.Sifra);
                cmd.Parameters.AddWithValue("Kolicina", namestaj.KolicinaUMagacinu);
                cmd.Parameters.AddWithValue("Obrisan", namestaj.Obrisan);
                //dodati akciju

                cmd.ExecuteNonQuery();

                //azuriram stanje modela
                foreach (var n in Projekat.Instanca.Namestaj)
                {
                    if(n.Id == namestaj.Id)
                    {
                        n.TipNamestajaId = namestaj.TipNamestajaId;
                        n.Naziv = namestaj.Naziv;
                        n.Cena = namestaj.Cena;
                        n.AkcijskaCena = namestaj.AkcijskaCena;
                        n.Sifra = namestaj.Sifra;
                        n.KolicinaUMagacinu = namestaj.KolicinaUMagacinu;
                        n.Obrisan = namestaj.Obrisan;
                        break;
                    }
                }
            }
        }

        public static void Delete(Namestaj namestaj)
        {
            namestaj.Obrisan = true;
            Update(namestaj);
        }
        #endregion
    }
}