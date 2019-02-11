using System;
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
        /// <summary>
        /// Dodjela uredjaja nekoj osobi
        /// </summary>
        /// <param name="name">Ime osobe za dodjelu</param>
        /// <param name="surname">Prezime Osobe za dodjelu</param>
        /// <param name="device">Uredjaj koji dodjeljujemo</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("koriscenjeuredjaja/{name}/{surname}/{device}")]
        public IActionResult KoriscenjeUredjaja(string name, string surname, string device)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
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
                        korUredjaji.Where(x => x.UredjajId == uredjajiQuery && x.VrijemeDo == null).Select(y => y.Id);
                    //---------------------------- Broj pronadjenih uredjaja ------------------------------
                    var korUredjajiBroj = _context.KorisceniUredjaji;
                    var korUredjajiBrojQuery =
                        korUredjaji.Where(x => x.UredjajId == uredjajiQuery && x.VrijemeDo == null).Count();

                    var izmjena = _context.KorisceniUredjaji.Find(korUredjajiQuery.FirstOrDefault());

                    if (korUredjajiBrojQuery != 0)
                    {
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
                    transaction.Commit();
                    return Ok(korUredjajiQuery.ToString());
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            
            
        }
        /// <summary>
        /// Brisanje istorije po id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
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
        /// <summary>
        /// Izlistavanje istorije svih koriscenih uredjaja
        /// </summary>
        /// <returns></returns>
        [HttpGet("izlistavanjeistorije")]
        public IActionResult IzlistavanjeIstorije()
        {
            var istorija = _context.KorisceniUredjaji;
            var istorijaQuery =
                istorija.Select(x => x);
            return Ok(istorijaQuery.ToList());
        }
        /// <summary>
        /// prema imenu i prezimenu osobe izlistava koji uredjaj koristi
        /// </summary>
        /// <param name="name">Ime</param>
        /// <param name="surname">Prezime</param>
        /// <returns></returns>
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
        /// <summary>
        /// Po imenu uredjaja izbacuje osobu koja ga koristi
        /// </summary>
        /// <param name="ime">Ime Uredjaja</param>
        /// <returns></returns>
        [HttpGet("izlistavanjepouredjaju/{ime}")]
        public IActionResult IzlistavanjePoUredjaju(string ime)
        {
            // ------------------------ Izlistavanje uredjaja po imenu i vracanje njihovog Id ------------------------
            var uredjaji = _context.Uredjaji;
            var uredjajiQuery =
                uredjaji.Where(x => x.Name.Contains(ime)).Select(s => s.Id).FirstOrDefault();
            // ---------------------- Izlistavanje Istoriije uredjaja i dobijanje Id od osoba koje koriste trazeni Uredjaj --------------
            var idOsobaUredjaja = _context.KorisceniUredjaji;
            var idOsobaUredjajaQuery =
                idOsobaUredjaja.Where(x => x.UredjajId == uredjajiQuery).Select(s => s.OsobaId).FirstOrDefault();
            // ------------------------- Dobijanje Imena Osobe po dobijenom Id ----------------------------------------------
            var imeOsobe = _context.Osobe;
            var imeOsobeQuery =
                imeOsobe.Where(x => x.Id == idOsobaUredjajaQuery).Select(name => name.Ime);
            return Ok(imeOsobeQuery.ToList());
        }

    }
}
