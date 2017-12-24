using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.Model
{
    public class StavkaRacuna: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int idStavkeRacuna;
        private int idNamestaja;
        private int kolicinaNamestaja;
        private int idDodatneUsluge;
        private int kolicinaDodatnihUsluga;
        
        public int IdStavkeRacuna
        {
            get { return idStavkeRacuna; }
            set
            {
                idStavkeRacuna = value;
                OnPropertyChanged("IdStavkeRacuna");
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

    }
}
