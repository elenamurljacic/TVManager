using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvmanager
{
    class Serija: ITVSadrzaj
    {
        public Serija(string ime, string opis, string zanr,
            int duljina, int prioritet, int dobnaSkupina,
            string redatelj, string sezona, int epizode, string prikazivanje)
        {
            Ime = ime;
            Opis = opis;
            Zanr = zanr;
            Duljina = duljina;
            Prioritet = prioritet;
            DobnaSkupina = dobnaSkupina;
            Redatelj = redatelj;
            Sezona = sezona;
            Epizode = epizode;
            Prikazivanje = prikazivanje;

        }
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

        //ostatak implementacije klase Serija
        public string Redatelj;
        public string Sezona;
        public int Epizode;
        public string Prikazivanje;

        public override string ToString()
        {
            return Ime + ", zanr: " + Zanr + ". Kratak opis: " + Opis + " Trajanje: " + Duljina + " min. " + "Prioritet: "
                   + Prioritet + ", dobna skupina " + DobnaSkupina + ". Redatelj: " + Redatelj + ", sezona: " + Sezona
                   + ", broj epizoda: " + Epizode + ".";
        }
    }
}
