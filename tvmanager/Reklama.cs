using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvmanager
{
    class Reklama: ITVSadrzaj
    {
        //implementacija sučelja
        private string ime = "";
        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }
        private string opis = "";
        public string Opis
        {
            get { return opis; }
            set { opis = value; }
        }
        private string zanr = "";
        public string Zanr
        {
            get { return zanr; }
            set { zanr = value; }
        }
        private int duljina = 0;
        public int Duljina
        {
            get { return duljina; }
            set { duljina = value; }
        }
        private int prioritet = 0;
        public int Prioritet
        {
            get { return prioritet; }
            set { prioritet = value; }
        }
        private int dobnaSkupina = 0;
        public int DobnaSkupina
        {
            get { return dobnaSkupina; }
            set { dobnaSkupina = value; }
        }

        //ostatak implementacije klase Reklama
        public int Cijena;
        public override string ToString()
        {
            return Ime + ", zanr: " + Zanr + ". Kratak opis: " + Opis + " Trajanje: " + Duljina + " min. " + "Prioritet: "
                   + Prioritet + ", dobna skupina " + DobnaSkupina + ". Cijena: " + Cijena;
        }
    }
}
