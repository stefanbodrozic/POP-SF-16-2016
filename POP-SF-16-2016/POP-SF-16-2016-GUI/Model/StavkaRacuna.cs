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
        private int kolicina;
        private int idDodatneUsluge;

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

        public int Kolicina
        {
            get { return kolicina; }
            set
            {
                kolicina = value;
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


        public override string ToString()
        {
            return IdNamestaja + " " + Kolicina + " " + IdDodatneUsluge;
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
