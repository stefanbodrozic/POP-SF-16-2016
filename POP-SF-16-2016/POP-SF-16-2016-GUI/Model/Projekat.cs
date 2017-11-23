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
        //private List<TipNamestaja> TipoviNamestajaLista;
        //private List<Namestaj> NamestajLista;
        private List<Akcija> AkcijeLista;
        private List<DodatneUsluge> DodatneUslugeLista;
        private List<Korisnik> KorisniciLista;
        private List<ProdajaNamestaja> ProdajaNamestajaLista;
        private List<Salon> SalonLista;

        public ObservableCollection<TipNamestaja> TipoviNamestaja { get; set; }
        //{
            //get
            //{
            //    TipoviNamestajaLista = GenericSerializer.Deserialize<TipNamestaja>("tipovi_namestaja.xml");
            //    return TipoviNamestajaLista;
            //}
            //set
            //{
            //    TipoviNamestajaLista = value;
            //    GenericSerializer.Serialize<TipNamestaja>("tipovi_namestaja.xml", TipoviNamestajaLista);
            //}
        //}

        public ObservableCollection<Namestaj> Namestaj { get; set; }
        //{
            //get
            //{
            //    NamestajLista = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
            //    return NamestajLista;
            //}
            //set
            //{
            //    NamestajLista = value;
            //    GenericSerializer.Serialize<Namestaj>("namestaj.xml", NamestajLista);
            //}
        //}

        private Projekat()
        {
            TipoviNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tipovi_namestaja.xml");
            Namestaj = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");

        }

        //public List<Akcija> Akcija
        //{
        //    get
        //    {
        //        AkcijeLista = GenericSerializer.Deserialize<Akcija>("akcija.xml");
        //        return AkcijeLista;
        //    }
        //    set
        //    {
        //        AkcijeLista = value;
        //        GenericSerializer.Serialize<Akcija>("akcija.xml", AkcijeLista);
        //    }
        //}

        //public List<DodatneUsluge> DodatneUsluge
        //{
        //    get
        //    {
        //        DodatneUslugeLista = GenericSerializer.Deserialize<DodatneUsluge>("dodatne_usluge.xml");
        //        return DodatneUslugeLista;
        //    }
        //    set
        //    {
        //        DodatneUslugeLista = value;
        //        GenericSerializer.Serialize<DodatneUsluge>("dodatne_usluge.xml", DodatneUslugeLista);
        //    }
        //}

        //public List<Korisnik> Korisnik
        //{
        //    get
        //    {
        //        KorisniciLista = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
        //        return KorisniciLista;
        //    }
        //    set
        //    {
        //        KorisniciLista = value;
        //        GenericSerializer.Serialize<Korisnik>("korisnici.xml", KorisniciLista);
        //    }
        //}

        //public List<ProdajaNamestaja> ProdajaNamestaja
        //{
        //    get
        //    {
        //        ProdajaNamestajaLista = GenericSerializer.Deserialize<ProdajaNamestaja>("prodaja_namestaja.xml");
        //        return ProdajaNamestajaLista;
        //    }
        //    set
        //    {
        //        ProdajaNamestajaLista = value;
        //        GenericSerializer.Serialize<ProdajaNamestaja>("prodaja_namestaja.xml", ProdajaNamestajaLista);
        //    }
        //}

        //public List<Salon> Salon
        //{
        //    get
        //    {
        //        SalonLista = GenericSerializer.Deserialize<Salon>("salon.xml");
        //        return SalonLista;
        //    }
        //    set
        //    {
        //        SalonLista = value;
        //        GenericSerializer.Serialize<Salon>("salon.xml", SalonLista);
        //    }
        //}
        
    }
}
