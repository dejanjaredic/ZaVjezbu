using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadnoMjestoVjezba.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Kancelarija> Kancelarije { get; set; }
        public DbSet<Osoba> Osobe { get; set; }
        public DbSet<Uredjaj> Uredjaji { get; set; }
        public DbSet<KoriscenjeUrednjaja> KorisceniUredjaji { get; set; }

        
    }
}
