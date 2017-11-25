using POP_SF_16_2016_GUI.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.Model
{
    class Projekat
    {
        public static Projekat Instanca { get; private set; } = new Projekat();

        public ObservableCollection<TipNamestaja> TipoviNamestaja { get; set; }

        public ObservableCollection<Namestaj> Namestaj { get; set; }

        public ObservableCollection<Akcija> Akcija { get; set; }

        public ObservableCollection<DodatneUsluge> DodatneUsluge { get; set; }

        public ObservableCollection<Korisnik> Korisnik { get; set; }
        
        public ObservableCollection<ProdajaNamestaja> ProdajaNamestaja { get; set; }

        public ObservableCollection<Salon> Salon { get; set; }

        private Projekat()
        {
            TipoviNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tipovi_namestaja.xml");
            Namestaj = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
        }        
    }
}
