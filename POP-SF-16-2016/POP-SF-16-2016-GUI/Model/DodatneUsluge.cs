using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.Model
{
    public class DodatneUsluge : INotifyPropertyChanged
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

        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
 
}
