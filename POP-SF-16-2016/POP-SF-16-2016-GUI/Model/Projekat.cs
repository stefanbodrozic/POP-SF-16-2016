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

        public ObservableCollection<NamestajNaAkciji> NamestajNaAkciji { get; set; }

        public ObservableCollection<DodatneUsluge> DodatneUsluge { get; set; }

        public ObservableCollection<Korisnik> Korisnik { get; set; }
        
        public ObservableCollection<ProdajaNamestaja> ProdajaNamestaja { get; set; }

        public ObservableCollection<Salon> Salon { get; set; }

        public ObservableCollection<StavkaRacunaNamestaj> StavkaRacunaNamestaj { get; set; }

        public ObservableCollection<StavkaRacunaDodatnaUsluga> StavkaRacunaDodatnaUsluga { get; set; }

        private Projekat()
        {
            TipoviNamestaja = TipNamestaja.GetAll();
            
            Namestaj = Model.Namestaj.GetAll();
            
            Akcija = Model.Akcija.GetAll();
           
            NamestajNaAkciji = Model.NamestajNaAkciji.GetAll();

            DodatneUsluge = Model.DodatneUsluge.GetAll();
            
            Korisnik = Model.Korisnik.GetAll();
            
            ProdajaNamestaja = Model.ProdajaNamestaja.GetAll();
            
            Salon = Model.Salon.GetAll();

            StavkaRacunaNamestaj = Model.StavkaRacunaNamestaj.GetAll();

            StavkaRacunaDodatnaUsluga = Model.StavkaRacunaDodatnaUsluga.GetAll();
        }        
    }
}
