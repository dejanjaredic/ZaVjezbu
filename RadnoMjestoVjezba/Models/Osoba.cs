using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RadnoMjestoVjezba.Models
{
    public class Osoba
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int KancelarijaId { get; set; }
        [ForeignKey("KancelarijaId")]
        public Kancelarija Kancelarija { get; set; }

        public List<KoriscenjeUrednjaja> KoriscenjeUrednjaja { get; set; }
    }
}
