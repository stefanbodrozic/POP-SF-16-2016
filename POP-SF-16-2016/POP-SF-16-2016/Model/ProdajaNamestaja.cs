using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF_16_2016.Model
{
    public class ProdajaNamestaja
    {
        List<Namestaj> NamestajZaProdaju { get; set; }
        public int Id { get; set; }
        public DateTime DatumProdaje { get; set; }
        public string BrojRacuna { get; set; }
        public string Kupac { get; set; }
        public List<DodatneUsluge> DodatneUsluge { get; set; }
  
        public const double PDV = 0.02;
        public double UkupnaCena { get; set; }
        public bool Obrisan { get; set; }
    }
}
