using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016.Model
{
    public class TipNamestaja
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public bool Obrisan { get; set; }

        public static TipNamestaja PronadjiTipNamestajaPoId(int id)
        {
            //foreach(var tipNamestaja in Projekat.Instanca.TipoviNamestaja)
            //{
            //    if(tipNamestaja.Id == id)
            //    {
            //        return tipNamestaja;
            //    }
            //}
            //return null;

            return Projekat.Instanca.TipoviNamestaja.SingleOrDefault(x => x.Id == id); //isto kao for petlja....
        }
    }
}
