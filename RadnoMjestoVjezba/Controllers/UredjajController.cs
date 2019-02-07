using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RadnoMjestoVjezba.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RadnoMjestoVjezba.Controllers
{
    [Route("api/[controller]")]
    public class UredjajController : Controller
    {
        protected readonly DataContext _context;

        public UredjajController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("kreiranjeuredjaja")]
        public IActionResult KreiranjeUredjaja(Uredjaj input)
        {
            if (input == null)
            {
                return NoContent();
            }

            _context.Uredjaji.Add(input);
            _context.SaveChanges();
            return Ok(input.Name + " Kreiran u Tabeli Uredjaji");
        }

        [HttpGet("izlistavanjesvihuredjaja")]
        public IActionResult IzlistavanjeUredjaja()
        {
            var uredjaji =_context.Uredjaji.ToList();
            return Ok(uredjaji);
        }

        [HttpGet("uredjajipoid/{id}")]
        public IActionResult UredjajiPoId(int id)
        {
            var uredjaji = _context.Uredjaji;
            var uredjajiQuery =
                uredjaji.Where(x => x.Id == id);

            return Ok(uredjajiQuery.ToList());
        }

        [HttpPut("mijnjanjeuredjaja/{id}")]
        public IActionResult MijenjanjeUredjaja(int id, Uredjaj input)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var uredjaji = _context.Uredjaji.Find(id);
            uredjaji.Name = input.Name;
            _context.SaveChanges();
            return Ok(uredjaji.Name);
        }

        [HttpDelete("brisanjeuredjaja/{id}")]
        public IActionResult BrisanjeUredjaja(int id)
        {
            var uredjaji = _context.Uredjaji.Find(id);
            if (uredjaji == null)
            {
                return NotFound();
            }

            _context.Uredjaji.Remove(uredjaji);
            _context.SaveChanges();
            return Ok("obrisano");
        }

        [HttpGet("pretragauredjajapoimenu/{name}")]
        public IActionResult PretragaPoImenu(string name)
        {
            var uredjaji = _context.Uredjaji;
            var uredjajiQuery =
                uredjaji.Where(x => x.Name.Contains(name));

            return Ok(uredjajiQuery.ToList());
        }

    }
}
