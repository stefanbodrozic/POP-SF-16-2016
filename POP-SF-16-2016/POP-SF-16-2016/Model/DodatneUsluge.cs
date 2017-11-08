using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016.Model
{
    public class DodatneUsluge
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public double Iznos { get; set; }
        public bool Obrisan { get; set; }

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
    }

    
}
