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
        private int idStavkeRacuna;
        private int idNamestaja;
        private int kolicina;

        public event PropertyChangedEventHandler PropertyChanged;

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


        public override string ToString()
        {
            return IdNamestaja + " " + Kolicina;
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
