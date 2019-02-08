using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadnoMjestoVjezba.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RadnoMjestoVjezba.Controllers
{
    [Route("api/[controller]")]
    public class KancelarijaController : Controller
    {
        protected readonly DataContext _context;

        public KancelarijaController(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Kreiranje Kancelarije
        /// </summary>
        /// <param name="input">Property Modela Kancelarije</param>
        /// <returns></returns>
        [HttpPost("kreiranjekancelarije")]
        public IActionResult KreiranjeKancelarije(Kancelarija input)
        {
            if (input == null)
            {
                return BadRequest();
            }

            _context.Kancelarije.Add(input);
            _context.SaveChanges();
            return Ok("Kreirana Kancelarija ");
        }
        /// <summary>
        /// Izlistavanje svih Kancelarija
        /// </summary>
        /// <returns></returns>
        [HttpGet("izlistavanjesvihkancelarija")]
        public IActionResult IzlistavanjeKancelarija()
        {
            var kancelarije = _context.Kancelarije;
            var kancelarijeQuery =
                kancelarije.Select(x => x);

            return Ok(kancelarijeQuery.ToList());
        }
        /// <summary>
        /// Pretraga Kancelarije po Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("izlistavanjepoid/{id}")]
        public IActionResult IzlistavanjePoId(int id)
        {
            var kancelarije = _context.Kancelarije;
            var kancelarijeQuery =
                kancelarije.Where(x => x.Id == id);

            return Ok(kancelarijeQuery.ToList());
        }
        /// <summary>
        /// Izmjena Kancelarije Po Id
        /// </summary>
        /// <param name="id">id kancelarije</param>
        /// <param name="input">Propertty Kancelarije</param>
        /// <returns></returns>
        [HttpPut("izmjenakancelarije/{id}")]
        public IActionResult IzmjenaKancelarije(int id, Kancelarija input)
        {
            
            if (id == null)
            {
                return BadRequest();
            }

            _context.Entry(input).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();

        }
        /// <summary>
        /// Pretraga Po Opisu kancelarije, ako kancelarija u opisu sadrzi rijec pretrage dobijamo podatke kancelarije
        /// </summary>
        /// <param name="opis">opis</param>
        /// <returns></returns>
        [HttpGet("pretragapoopisu/{opis}")]
        public IActionResult PretragaPoOpisu(string opis)
        {
            var kancelarije = _context.Kancelarije;
            var kancelarijeQuery =
                kancelarije.Where(x => x.Opis.Contains(opis));

            return Ok(kancelarijeQuery.ToList());
        }
    }
}
