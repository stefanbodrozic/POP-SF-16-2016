using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.Model
{
    public class Namestaj
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Sifra{ get; set; }
        public double Cena { get; set; }
        public int KolicinaUMagacinu { get; set; }
        public int TipNamestajaId{ get; set; }
        public Akcija Akcija { get; set; }

        public bool Obrisan{ get; set; }

        public static Namestaj PronadjiNamestajPoId(int Id)
        {
            foreach (var Namestaj in Projekat.Instanca.Namestaj)
            {
                if (Namestaj.Id == Id)
                {
                    return Namestaj;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return Naziv + "|" + Sifra + "|" + Cena + "|" + KolicinaUMagacinu + "|" + TipNamestajaId;
        }

    }
}
