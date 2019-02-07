using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadnoMjestoVjezba.Dto
{
    public class OsobaDto
    {
        //public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public KancelarijaDto Kancelarija { get; set; }
        public VrijemeKoriscenjaDto KoriscenjeUredjaja { get; set; }
    }
}
