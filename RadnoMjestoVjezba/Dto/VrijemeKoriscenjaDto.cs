using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadnoMjestoVjezba.Dto
{
    public class VrijemeKoriscenjaDto
    {
        //public int Id { get; set; }
        public DateTime Od { get; set; }
        public DateTime? Do { get; set; }

        public UredjajDto Uredjaj { get; set; }
        public OsobaDto Osoba { get; set; }
    }
}
