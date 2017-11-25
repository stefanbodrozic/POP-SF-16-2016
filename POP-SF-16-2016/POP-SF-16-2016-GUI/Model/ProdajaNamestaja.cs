using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.Model
{
    public class ProdajaNamestaja : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //public const double PDV = 0.02;  dodati
        private int id;
        private DateTime datumProdaje;
        private string brojRacuna;
        private string kupac;
        private List<int> idProdatogNamestaja;
        private List<int> idDodatneUsluge;
        private double ukupnaCena;
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

        public List<int> IdProdatogNamestaja
        {
            get { return idProdatogNamestaja; }
            set
            {
                idProdatogNamestaja = value;
                OnPropertyChanged("IdProdatogNamestaja");
            }
        }

        public List<int> IdDodatneUsluge
        {
            get { return idDodatneUsluge; }
            set
            {
                idDodatneUsluge = value;
                OnPropertyChanged("IdDodatneUsluge");
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

    }
}
