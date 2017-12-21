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

        public ObservableCollection<StavkaRacuna> StavkaRacuna { get; set; }

        private Projekat()
        {
            //TipoviNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tipovi_namestaja.xml");
            TipoviNamestaja = TipNamestaja.GetAll();
            //Namestaj = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
            Namestaj = Model.Namestaj.GetAll();
            Akcija = GenericSerializer.Deserialize<Akcija>("akcija.xml");
            DodatneUsluge = GenericSerializer.Deserialize<DodatneUsluge>("dodatne_usluge.xml");
            Korisnik = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
            ProdajaNamestaja = GenericSerializer.Deserialize<ProdajaNamestaja>("prodaja_namestaja.xml");
            Salon = GenericSerializer.Deserialize<Salon>("salon.xml");
            StavkaRacuna = GenericSerializer.Deserialize<StavkaRacuna>("stavke_racuna.xml");
        }        
    }
}
