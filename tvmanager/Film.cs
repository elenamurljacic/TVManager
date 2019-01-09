﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvmanager
{
    class Film: ITVSadrzaj
    {
        public Film(string ime, string opis, string zanr, int duljina, int prioritet, int dobnaskupina, string redatelj, string gglumac, string prikazivanje)
        {
            Ime = ime;
            Opis = opis;
            Duljina = duljina;
            Prioritet = prioritet;
            DobnaSkupina = dobnaskupina;
            Redatelj = redatelj;
            GlavniGlumac = gglumac;
            Prikazivanje = prikazivanje;
        }
        //implementacija sučelja ITVSadrzaj
        //promjena
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

        //ostatak implementacije klase Film
        public string Redatelj;
        public string GlavniGlumac;
        public string Prikazivanje;
        public override string ToString()
        {
            return Ime + ", zanr: " + Zanr + ". Kratak opis: " + Opis + " Trajanje: " + Duljina + " min. " + "Prioritet: "
                   + Prioritet + ", dobna skupina " + DobnaSkupina + ". Redatelj: " + Redatelj + ", glavni glumac: "
                   + GlavniGlumac + ".";
        }
    }
}
