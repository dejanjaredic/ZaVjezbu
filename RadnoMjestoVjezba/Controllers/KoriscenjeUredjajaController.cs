﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RadnoMjestoVjezba.Dto;
using RadnoMjestoVjezba.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RadnoMjestoVjezba.Controllers
{
    [Route("api/[controller]")]
    public class KoriscenjeUredjajaController : Controller
    {
        protected readonly DataContext _context;

        public KoriscenjeUredjajaController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("koriscenjeuredjaja/{name}/{surname}/{device}")]
        public IActionResult KoriscenjeUredjaja(string name, string surname, string device, [FromBody]VrijemeKoriscenjaDto input)
        {
            var histotry = new KoriscenjeUrednjaja
            {
                VrijemeOd = DateTime.Now,
                
            };
            
            // ------------- Pretraga osobe po imenu i prezimenu i izbacivanje njenog id radi dodjele uredjaju ---------------------
            var osobe = _context.Osobe;
            var osobeQuery =
                osobe.Where(x => x.Ime.Equals(name) && x.Prezime.Equals(surname)).Select(osoba => osoba.Id).FirstOrDefault();
            // ------------------ Pretraga uredjaja i izbacivanje njegovog id --------------------
            var uredjaji = _context.Uredjaji;
            var uredjajiQuery =
                uredjaji.Where(x => x.Name.Equals(device)).Select(d => d.Id).FirstOrDefault();

            // --------------------- provjera koristi li neko dati uredjaj --------------------
            var korUredjaji = _context.KorisceniUredjaji;
            var korUredjajiQuery =
                korUredjaji.Where(x => x.UredjajId == uredjajiQuery).Select(y => y.Id).FirstOrDefault();
            if (korUredjajiQuery != 0)
            {
                var izmjena =_context.KorisceniUredjaji.Find(korUredjajiQuery);
                izmjena.VrijemeDo = DateTime.Now;
                _context.SaveChanges();
            }

            if (osobeQuery != null && uredjajiQuery != null)
            {
                histotry.OsobaId = osobeQuery;
                histotry.UredjajId = uredjajiQuery;
            }
            else
            {
                return BadRequest();
            }

            _context.KorisceniUredjaji.Add(histotry);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("brisanjeistorije/{id}")]
        public IActionResult BrisanjeIstorije(int id)
        {
            var istorija = _context.KorisceniUredjaji.Find(id);
            if (istorija == null)
            {
                return BadRequest();
            }

            _context.KorisceniUredjaji.Remove(istorija);
            _context.SaveChanges();
            return Ok(istorija);
        }

        [HttpGet("izlistavanjeistorije")]
        public IActionResult IzlistavanjeIstorije()
        {
            var istorija = _context.KorisceniUredjaji;
            var istorijaQuery =
                istorija.Select(x => x);

            return Ok(istorijaQuery.ToList());
        }

        [HttpGet("pretragapoosobi/{name}/{surname}")]
        public IActionResult PretragaPoOsobi(string name, string surname)
        {
            var osobe = _context.Osobe;
            var osobeQuery =
                osobe.Where(x => x.Ime == name && x.Prezime == surname).Select(i => i.Id).FirstOrDefault();
            var istorija = _context.KorisceniUredjaji;
            var istorijaQuery =
                istorija.Where(x => x.OsobaId == osobeQuery);

            return Ok(istorijaQuery.ToList());
        }

    }
}