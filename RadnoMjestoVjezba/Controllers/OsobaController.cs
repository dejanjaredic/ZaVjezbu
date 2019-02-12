using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadnoMjestoVjezba.Dto;
using RadnoMjestoVjezba.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RadnoMjestoVjezba.Controllers
{
    [Route("api/[controller]")]
    public class OsobaController : BaseController<Osoba>
    {
        //protected readonly DataContext _context;
        public OsobaController(DataContext context) : base(context)
        {
        }

        /// <summary>
        /// Funkcija za kreiranje nove osobe i dodjela stranog kljuca kancelarije.
        /// U zavisnosti od opisa posla dodjeljuje strani kljuc toj osobi
        /// ili kreira novu kancelariju ako se opis ne podudara sa postojecom
        /// </summary>
        /// 
        /// <param name="input">Kreiranje Osobe</param>
        /// <returns></returns>
        [HttpPost("kreiranjesobe")]
        public virtual IActionResult AddData(KreiranjeOsobeDto input)
        {
            //return base.AddData(input);
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var osoba = new Osoba
                    {
                        Ime = input.Ime,
                        Prezime = input.Prezime,
                    };
                    var sveKancelarije = _context.Kancelarije;
                    var sveKancelarijeQuery = sveKancelarije.Select(x => x.Opis);
                    if (sveKancelarijeQuery.Contains(input.Kancelarija.Opis))
                    {
                        var getKancelarijaId = _context.Kancelarije;
                        var getKancelarijaQuery =
                            getKancelarijaId.Where(x => x.Opis.Contains(input.Kancelarija.Opis)).Select(y => y.Id).FirstOrDefault();
                        if (getKancelarijaQuery != null)
                        {
                            osoba.KancelarijaId = getKancelarijaQuery;
                        }
                    }
                    else
                    {
                        osoba.Kancelarija = new Kancelarija
                        {
                            Opis = input.Kancelarija.Opis
                        };
                    }
                    _context.Osobe.Add(osoba);
                    _context.SaveChanges();

                    transaction.Commit();
                    return Ok(osoba.ToString());
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }
        /// <summary>
        /// Funkcija Izlistava Osobe koje rade u kancelariji ciji opis pretrazujemo
        /// </summary>
        /// <param name="opis">pis kancelarije</param>
        /// <returns>List</returns>
        [HttpGet("pretragaosobapokancelariji/{opis}")]
        public virtual IActionResult PretragaPoKoancelariji(string opis)
        {
            var getData = _context.Osobe.Include(x => x.Kancelarija);
            var getDataQuery =
                getData.GroupBy(g => g.Kancelarija.Opis).Where(n => n.Key.Contains(opis)).Select(s =>
                    new {OpisKancelarije = s.Key, Radnici = s.Select(n => n.Ime + " " + n.Prezime)}).AsNoTracking();
            return Ok(getDataQuery.ToList());
        }

        [HttpPut("izmjenapostojeceosobe/{id}")]
        public virtual IActionResult IzmjenaPoId(int id, IzmjenaOsobeDto input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var osobe = _context.Osobe.Find(id);
                    if (osobe == null)
                    {
                        return NotFound();
                    }
                    osobe.Ime = input.Ime;
                    osobe.Prezime = input.Prezime;
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok(osobe);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        /// <summary>
        /// Izlistavanje svih osoba
        /// </summary>
        /// <returns></returns>
        [HttpGet("getalldata")]
        public IActionResult GetAllData()
        {
            return base.GetAllData();
        }
        /// <summary>
        /// brisanje osoba po zadatom id
        /// </summary>
        /// <param name="id">Id osobe</param>
        /// <returns></returns>
        [HttpDelete("brisanjepoid")]
        public IActionResult DeleteData(int id)
        {
            return base.DeleteData(id);
        }
        /// <summary>
        /// Pretraga Osobe po Id
        /// </summary>
        /// <param name="id">Id osobe za pretragu</param>
        /// <returns></returns>
        [HttpGet("prettragapoid")]
        public IActionResult GetDataById(int id)
        {
            return base.GetDataById(id);
        }

    }
}
