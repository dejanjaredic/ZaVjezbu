using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadnoMjestoVjezba.Dto
{
    public class KreiranjeOsobeDto
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public KreiranjeKancelarijeDtocs Kancelarija { get; set; }
    }
}
