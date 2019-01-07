using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvmanager
{
    interface ITVSadrzaj
    {
        string Ime { get; set; }
        string Opis { get; set; }
        string Zanr { get; set; }
        int Duljina { get; set; }
        int Prioritet { get; set; }
        int DobnaSkupina { get; set; }
    }
}
