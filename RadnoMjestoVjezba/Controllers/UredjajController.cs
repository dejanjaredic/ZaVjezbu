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
        /// <summary>
        /// Kreiranje Uredjaja
        /// </summary>
        /// <param name="input">property modela Uredjaj</param>
        /// <returns></returns>
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
        /// <summary>
        /// Izlistavanje svih Uredjaja
        /// </summary>
        /// <returns></returns>
        [HttpGet("izlistavanjesvihuredjaja")]
        public IActionResult IzlistavanjeUredjaja()
        {
            var uredjaji =_context.Uredjaji.ToList();
            return Ok(uredjaji);
        }
        /// <summary>
        /// pretrazuje uredjaj po id
        /// </summary>
        /// <param name="id">id uredjaja</param>
        /// <returns></returns>
        [HttpGet("uredjajipoid/{id}")]
        public IActionResult UredjajiPoId(int id)
        {
            var uredjaji = _context.Uredjaji;
            var uredjajiQuery =
                uredjaji.Where(x => x.Id == id);

            return Ok(uredjajiQuery.ToList());
        }
        /// <summary>
        /// Izmjena Uredjaja
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="input">Property uredjaja</param>
        /// <returns></returns>
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
        /// <summary>
        /// Brisanje Uredjaja
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
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
        /// <summary>
        /// Pretrazivanje uredjaja op imenu
        /// </summary>
        /// <param name="name">ime uredjaja</param>
        /// <returns></returns>
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
