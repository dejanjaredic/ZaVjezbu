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
    public class KancelarijaController : BaseController<Kancelarija>
    {
        //protected readonly DataContext _context;


        public KancelarijaController(DataContext context) : base(context)
        {
        }
         ///<summary>
        /// Pretraga Po Opisu kancelarije, ako kancelarija u opisu sadrzi rijec pretrage dobijamo podatke kancelarije
         ///</summary>
         ///<param name = "opis" > opis </ param >
         ///< returns ></ returns >
        [HttpGet("pretragapoopisu/{opis}")]
        public virtual IActionResult PretragaPoOpisu(string opis)
        {
            var kancelarije = _context.Kancelarije;
            var kancelarijeQuery =
                kancelarije.Where(x => x.Opis.Contains(opis)).AsNoTracking();

            return Ok(kancelarijeQuery.ToList());
        }
        /// <summary>
        /// Izlistavanje svih Kancelarija
        /// </summary>
        /// <returns></returns>
        [HttpGet("getalldata")]
        public IActionResult GetAllData()
        {
            return base.GetAllData();
        }
        /// <summary>
        /// Brisanje postojecih kancelarija po Id
        /// </summary>
        /// <param name="id">Id kancelarije</param>
        /// <returns></returns>
        [HttpDelete("brisanjepoid")]
        public IActionResult DeleteData(int id)
        {
            return base.DeleteData(id);
        }
        /// <summary>
        /// Pretraga Kancelarije po Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("prettragapoid")]
        public IActionResult GetDataById(int id)
        {
            return base.GetDataById(id);
        }
        /// <summary>
        /// Izmjena propertija postojece Kancelarije
        /// </summary>
        /// <param name="id">id Kancelarije Za izmjenu</param>
        /// <param name="input">Opis Kancelarije</param>
        /// <returns></returns>
        [HttpPut("izmjenapoid/{id}")]
        public IActionResult IzmjenaPoId(int id, Kancelarija input)
        {
            return base.IzmjenaPoId(id, input);
        }

    }
}
