using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.Model
{
    public class Akcija : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int id;
        private DateTime datumPocetka;
        private DateTime datumZavrsetka;
        private decimal popust;
        private ObservableCollection<int> idNamestajaNaAkciji;
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

        public DateTime DatumPocetka
        {
            get { return datumPocetka; }
            set
            {
                datumPocetka = value;
                OnPropertyChanged("DatumPocetka");
            }
        }

        public DateTime DatumZavrsetka
        {
            get { return datumZavrsetka; }
            set
            {
                datumZavrsetka = value;
                OnPropertyChanged("DatumZavrsetka");
            }
        }

        public decimal Popust
        {
            get { return popust; }
            set
            {
                popust = value;
                OnPropertyChanged("Popust");
            }
        }

        public ObservableCollection<int> IdNamestajaNaAkciji
        {
            get { return idNamestajaNaAkciji; }
            set
            {
                idNamestajaNaAkciji = value;
                OnPropertyChanged("IdNamestajaNaAkciji");
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

        public override string ToString()
        {
            return DatumPocetka + "-" + DatumZavrsetka + "|" + Popust;
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
            Akcija kopija = new Akcija();
            kopija.Id = Id;
            kopija.DatumPocetka = DatumPocetka;
            kopija.DatumZavrsetka = DatumZavrsetka;
            kopija.Popust = Popust;
            kopija.IdNamestajaNaAkciji = IdNamestajaNaAkciji;
            return kopija;
        }


    }
}
