using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadnoMjestoVjezba.Dto;
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
        public IActionResult KreiranjeKancelarije(KreiranjeKancelarijeDtocs input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (input == null)
                    {
                        return BadRequest();
                    }

                    var kancelarija = new Kancelarija
                    {
                        Opis = input.Opis
                    };
                    _context.Kancelarije.Add(kancelarija);
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok("Kreirana Kancelarija ");
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
                
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
                kancelarije.Select(x => x).AsNoTracking();

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
                kancelarije.Where(x => x.Id == id).AsNoTracking();

            return Ok(kancelarijeQuery.ToList());
        }
        /// <summary>
        /// Izmjena Kancelarije Po Id
        /// </summary>
        /// <param name="id">id kancelarije</param>
        /// <param name="input">Propertty Kancelarije</param>
        /// <returns></returns>
        [HttpPut("izmjenakancelarije/{id}")]
        public IActionResult IzmjenaKancelarije(int id, KreiranjeKancelarijeDtocs input)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (id == null)
                    {
                        return BadRequest();
                    }

                    var izmjena = _context.Kancelarije.Find(id);
                    izmjena.Opis = input.Opis;
                    // _context.Entry(input).State = EntityState.Modified;
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

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
                kancelarije.Where(x => x.Opis.Contains(opis)).AsNoTracking();

            return Ok(kancelarijeQuery.ToList());
        }
        /// <summary>
        /// Metod za brisanje kancelarija, ukolika u njoj ima ljudi nije je moguce izbrisati
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("brisanjekancelarije/{id}")]
        public IActionResult BrisanjeKancelarije(int id)
        {
            var kancelarija = _context.Kancelarije.Find(id);

            if (id == 0)
            {
                return BadRequest("Id nije validan");
            }

            try
            {
                _context.Kancelarije.Remove(kancelarija);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                var greska = new GreskaDto
                {
                    Poruka = "Brisaje Kancelarije nije dozvoljeno"
                };
            }

            return Ok("Obrisano");
        }
    }
}
