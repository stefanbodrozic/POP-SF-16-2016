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
    public class ProdajaNamestaja : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private decimal pdv = 0.2M;
        private int id;
        private DateTime datumProdaje;
        private string brojRacuna;
        private string kupac;
        private double ukupnaCena;
        private bool obrisan;
        private double cenaBezPdv;

        public double CenaBezPdv
        {
            get { return cenaBezPdv; }
            set
            {
                cenaBezPdv = value;
                OnPropertyChanged("CenaBezPdv");
            }
        }


        public decimal Pdv
        {
            get { return pdv; }
            set
            {
                pdv = 0.2M;
            }
        }


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
            kopija.UkupnaCena = UkupnaCena;
            kopija.Obrisan = Obrisan;
            return kopija;
        }

        #region Database

        public static ObservableCollection<ProdajaNamestaja> GetAll()
        {
            var ucitaneProdaje = new ObservableCollection<ProdajaNamestaja>();
            try
            {
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
                        prodaja.Pdv = decimal.Parse(row["Pdv"].ToString());
                        prodaja.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                        prodaja.BrojRacuna = row["BrojRacuna"].ToString();
                        prodaja.Kupac = row["Kupac"].ToString();
                        prodaja.UkupnaCena = double.Parse(row["UkupnaCena"].ToString());
                        prodaja.cenaBezPdv = double.Parse(row["CenaBezPdv"].ToString());

                        ucitaneProdaje.Add(prodaja);
                    }
                }
                return ucitaneProdaje;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom ucitavanja podataka!", "Greska", MessageBoxButton.OK);
                return ucitaneProdaje;
            }
            
        }

        public static ProdajaNamestaja Create(ProdajaNamestaja prodaja)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO ProdajaNamestaja (Kupac) VALUES (@Kupac);";
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                    cmd.Parameters.AddWithValue("Kupac", prodaja.Kupac);
                    int newId = int.Parse(cmd.ExecuteScalar().ToString()); //ExecuteScalar izvrsava query
                    prodaja.Id = newId;
                    prodaja.BrojRacuna = "R" + prodaja.Id; //azuriram broj racuna

                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandText += "UPDATE ProdajaNamestaja SET BrojRacuna = @BrojRacuna WHERE Id = @Id;";
                    cmd1.Parameters.AddWithValue("BrojRacuna", "R" + prodaja.Id);
                    cmd1.Parameters.AddWithValue("Id", prodaja.Id);
                    cmd1.ExecuteNonQuery();
                }
                Projekat.Instanca.ProdajaNamestaja.Add(prodaja); //azuriram i stanje modela
                return prodaja;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom kreiranja nove prodaje!", "Greska", MessageBoxButton.OK);
                return prodaja;
            }
            
        }

        public static void Update(ProdajaNamestaja prodaja)
        {
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE ProdajaNamestaja SET Kupac = @Kupac, UkupnaCena = @UkupnaCena, CenaBezPdv = @CenaBezPdv, Obrisan = @Obrisan WHERE Id = @Id;";
                    cmd.Parameters.AddWithValue("Id", prodaja.Id);
                    cmd.Parameters.AddWithValue("Kupac", prodaja.Kupac);
                    cmd.Parameters.AddWithValue("UkupnaCena", prodaja.UkupnaCena);
                    cmd.Parameters.AddWithValue("CenaBezPdv", prodaja.CenaBezPdv);
                    cmd.Parameters.AddWithValue("Obrisan", prodaja.Obrisan);

                    cmd.ExecuteNonQuery();

                    //azuriram stanje modela
                    foreach (var p in Projekat.Instanca.ProdajaNamestaja)
                    {
                        if (p.Id == prodaja.Id)
                        {
                            p.Kupac = prodaja.Kupac;
                            p.UkupnaCena = prodaja.UkupnaCena;
                            p.CenaBezPdv = prodaja.CenaBezPdv;
                            p.Obrisan = prodaja.Obrisan;
                            break;
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom azuriranja prodaje!", "Greska", MessageBoxButton.OK);
                return;
            }

        }

        public static void Delete(ProdajaNamestaja prodaja)
        {
            prodaja.Obrisan = true;
            Update(prodaja);
        }

        public static ObservableCollection<ProdajaNamestaja> Search(string tekstZaPretragu)
        {
            ObservableCollection<ProdajaNamestaja> ucitaneProdaje = new ObservableCollection<ProdajaNamestaja>();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["POP"].ConnectionString))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM ProdajaNamestaja WHERE Obrisan=0 AND (Pdv LIKE @tekstZaPretragu OR DatumProdaje LIKE @tekstZaPretragu OR BrojRacuna LIKE @tekstZaPretragu OR Kupac LIKE @tekstZaPretragu OR UkupnaCena LIKE @tekstZaPretragu OR CenaBezPdv LIKE @tekstZaPretragu);";

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    cmd.Parameters.AddWithValue("tekstZaPretragu", '%' + tekstZaPretragu + '%');

                    da.SelectCommand = cmd;
                    da.Fill(ds, "ProdajaNamestaja"); //izvrsava se query nad bazom
                    foreach (DataRow row in ds.Tables["ProdajaNamestaja"].Rows)
                    {
                        var prodaja = new ProdajaNamestaja();
                        prodaja.Id = int.Parse(row["Id"].ToString());
                        prodaja.Pdv = decimal.Parse(row["Pdv"].ToString());
                        prodaja.DatumProdaje = DateTime.Parse(row["DatumProdaje"].ToString());
                        prodaja.BrojRacuna = row["BrojRacuna"].ToString();
                        prodaja.Kupac = row["Kupac"].ToString();
                        prodaja.UkupnaCena = double.Parse(row["UkupnaCena"].ToString());
                        prodaja.cenaBezPdv = double.Parse(row["CenaBezPdv"].ToString());

                        ucitaneProdaje.Add(prodaja);
                    }
                }
                return ucitaneProdaje;
            }
            catch
            {
                MessageBox.Show("Doslo je do greske sa radom baze podataka prilikom pretrage prodaje!", "Greska", MessageBoxButton.OK);
                return ucitaneProdaje;
            }
            
        }
        #endregion
    }
}
