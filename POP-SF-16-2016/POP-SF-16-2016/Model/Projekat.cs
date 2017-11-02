using POP_SF_16_2016.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016.Model
{
    class Projekat
    {
        public static Projekat Instanca { get; private set; } = new Projekat();
        private List<TipNamestaja> tipoviNamestaja;
        private List<Namestaj> Namestaj;

        public List<TipNamestaja> TipoviNamestaja
        {
            get
            {
                tipoviNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tipovi_namestaja.xml");
                return tipoviNamestaja;
            }
            set
            {

                tipoviNamestaja = value;
                GenericSerializer.Serialize<TipNamestaja>("tipovi_namestaja.xml", tipoviNamestaja);
            }
        }

        public List<Namestaj> Namestaj
        {
            get
            {
                Namestaj = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
                return Namestaj;
            }
            set
            {

                Namestaj = value;
                GenericSerializer.Serialize<Namestaj>("namestaj.xml", Namestaj);
            }
        }

    }
}
