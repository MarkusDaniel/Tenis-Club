using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _3.Beadando
{
    public enum PalyaTipus
    {
        Fu,
        Salak,
        Muanyag
    }
    public class TeniszKlub
    {
        private Dictionary<int, (PalyaTipus, bool)> palyak = 
            new Dictionary<int, (PalyaTipus, bool)>();//pályák tárolására

        private List<string> tagok = new List<string>();

        private List<Foglalas> foglalasok = new List<Foglalas>();

        //Összes foglalás
        public List<Foglalas> OsszesFoglalas(DateTime datum, int palyaSzam)
        {
            return foglalasok.FindAll(foglalas => foglalas.Datum == datum && foglalas.PalyaSzam == palyaSzam);
        }

        // Összes tag lekérdezése
        public List<string> OsszesTag()
        {
            return tagok;
        }

        //Pályák lekérdezése
        public Dictionary<int, (PalyaTipus, bool)> OsszesPalya()
        {
            return palyak;
        }

        // Új pálya létrehozása
        public void UjPaja(int palyaSzam, PalyaTipus tipus, bool fedett)
        {
            palyak.Add(palyaSzam, (tipus, fedett));
        }

        //Tag belép
        public void UjTag(string tagNev)
        {
            tagok.Add(tagNev);
        }

        //Pálya felszámolása
        public void PalyaFelszamolasa(int palyaSzam)
        {
            palyak.Remove(palyaSzam);
        }

        //Tag kilép
        public void TagKilep(string tagNev)
        {
            tagok.Remove(tagNev);
        }

        // A foglalás árának meghatározása
        public double Kaukcio(PalyaTipus tipus, bool fedett)
        {
            double alapAr = 0;

            switch (tipus)
            {
                case PalyaTipus.Fu:
                    alapAr = 5000;
                    break;

                case PalyaTipus.Salak:
                    alapAr = 3000;
                    break;

                case PalyaTipus.Muanyag:
                    alapAr = 2000;
                    break;

            }
            if(fedett == true)
            {
                return alapAr * 1.2;
            }
            else
            {
                return alapAr;
            }

        }

        //Időpont foglalás 
        public void UjFoglalás(string tagNev, int palyaSzam, DateTime datum, int ora)
        {
            foglalasok.Add(new Foglalas(tagNev, palyaSzam, datum, ora));
        }

        //Tag visszamondja a foglalást
        public void FoglalasLemondas(string tagNev, int palyaSzam, DateTime datum, int ora)
        {
            foglalasok.RemoveAll(foglalas => foglalas.TagNev == tagNev &&
                                             foglalas.PalyaSzam == palyaSzam &&
                                             foglalas.Datum == datum &&
                                             foglalas.Ora == ora);
        }

        public List<(int PalyaSzam, DateTime FoglalasDatum, int FoglalasOra)> TagFoglalasok(string tagNev, DateTime datum)
        {
            var foglaltPalyak = new List<(int PalyaSzam, DateTime FoglalasDatum, int FoglalasOra)>();

            foreach (var foglalas in foglalasok)
            {
                if (foglalas.TagNev == tagNev && foglalas.Datum.Date == datum.Date)
                {
                    foglaltPalyak.Add((foglalas.PalyaSzam, foglalas.Datum, foglalas.Ora));
                }
            }

            return foglaltPalyak;
        }

        public double NapiOsszbevetel(DateTime datum)
        {
            double osszeg = 0;

            // Számítsuk ki az összes foglalás árát az adott napon
            foreach (var foglalas in foglalasok)
            {
                if (foglalas.Datum.Date == datum.Date)
                {
                    var palyaAdatok = palyak[foglalas.PalyaSzam];
                    var palyaTipus = palyaAdatok.Item1;
                    var fedett = palyaAdatok.Item2;

                    double foglalasAr = Kaukcio(palyaTipus, fedett);
                    osszeg += foglalasAr;
                }
            }

            return osszeg;
        }
    }

}
