using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Beadando
{
    public class Foglalas
    {
        public string TagNev { get; set; }
        public int PalyaSzam { get; set; }
        public DateTime Datum { get; set; }
        public int Ora { get; set; }

        public Foglalas(string tagNev, int palyaSzam, DateTime datum, int ora)
        {
            TagNev = tagNev;
            PalyaSzam = palyaSzam;
            Datum = datum;
            Ora = ora;
        }
    }
}
