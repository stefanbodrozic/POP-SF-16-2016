using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016_GUI.Model
{
    public class Akcija
    {
        public int Id { get; set; }
        public DateTime DatumPocetka { get; set; }
        public decimal Popust { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        //dodati za koji namestaj se vezuje akcija
        public int IdNamestaja { get; set; }

        public bool Obrisan { get; set; }
    }
}
