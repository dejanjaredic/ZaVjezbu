﻿using System;
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
    public class OsobaController : Controller
    {
        protected readonly DataContext _context;

        public OsobaController(DataContext context)
        {
            _context = context;
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
        public IActionResult Kreiranjeosobe([FromBody]OsobaDto input)
        {
            var osoba = new Osoba
            {
                Ime = input.Ime,
                Prezime = input.Prezime,  
            };
            var sveKancelarije = _context.Kancelarije;
            var sveKancelarijeQuery = sveKancelarije.Select(x => x.Opis);
            if (sveKancelarijeQuery.Contains(input.Kancelarija.Opis) )
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
            return Ok(osoba);  
        }
        /// <summary>
        /// Funkcija Izlistava Osobe koje rade u kancelariji ciji opis pretrazujemo
        /// </summary>
        /// <param name="opis">pis kancelarije</param>
        /// <returns>List</returns>
        [HttpGet("pretragaosobapokancelariji/{opis}")]
        public IActionResult PretragaPoKoancelariji(string opis)
        {
            var getData = _context.Osobe.Include(x => x.Kancelarija);
            var getDataQuery =
                getData.GroupBy(g => g.Kancelarija.Opis).Where(n => n.Key.Contains(opis)).Select(s =>
                    new {OpisKancelarije = s.Key, Radnici = s.Select(n => n.Ime + " " + n.Prezime)});
            return Ok(getDataQuery.ToList());
        }
        /// <summary>
        /// Funkcija izlistava imena, prezimena i opis kancelarija u kojima rade
        /// </summary>
        /// <returns>list</returns>
        [HttpGet("izlistavanjesvihosoba")]
        public IActionResult IzlistavanjeSvihOsoba()
        {
            var getData = _context.Osobe;
            var dataQuery =
                getData.Select(s => new
                {
                    Ime = s.Ime,
                    Prezime = s.Prezime,
                    Kancelarija = s.Kancelarija.Opis
                });

            return Ok(dataQuery.ToList());
        }
        /// <summary>
        /// Izlistavanje osoba po Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet("izlistavanjepoid/{id}")]
        public IActionResult IzlistavanjePoId(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var getData = _context.Osobe;
            var dataQuery =
                getData.Where(x => x.Id == id).Select(s => new
                    {Ime = s.Ime, Prezime = s.Prezime, Kancelarija = s.Kancelarija.Opis});
            return Ok(dataQuery.ToList());
        }
        [HttpPut("izmjenapostojeceosobe/{id}")]
        public IActionResult IzmjenaPostojeceOsobe(int id, [FromBody]OsobaDto input)
        {
            var osobe = _context.Osobe.Find(id);
            if (osobe == null)
            {
                return NotFound();
            }
            osobe.Ime = input.Ime;
            osobe.Prezime = input.Prezime;
            _context.SaveChanges();
            return Ok(osobe);  
        }

        [HttpDelete("brisanjeosobe/{id}")]
        public IActionResult BrisanjeOsobe(int id)
        {
            
            var osobe = _context.Osobe.Find(id);
            if (osobe == null)
            {
                return NotFound();
            }
            _context.Osobe.Remove(osobe);
            _context.SaveChanges();
            return NoContent();
        }
    }
}