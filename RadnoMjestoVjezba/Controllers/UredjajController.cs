using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadnoMjestoVjezba.Dto;
using RadnoMjestoVjezba.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RadnoMjestoVjezba.Controllers
{
    [Route("api/[controller]")]
    public class UredjajController : BaseController<Uredjaj>
    {
        protected readonly DataContext _context;
        public UredjajController(DataContext context) : base(context)
        {
        }
        /// <summary>
        /// Izlistavanje svih Uredjaja
        /// </summary>
        /// <returns></returns>
        [HttpGet("getalldata")]
        public IActionResult GetAllData()
        {
            return base.GetAllData();
        }
        /// <summary>
        /// Brisanje uredjaja po Id
        /// </summary>
        /// <param name="id">id uredjaja</param>
        /// <returns></returns>
        [HttpDelete("brisanjepoid")]
        public IActionResult DeleteData(int id)
        {
            return base.DeleteData(id);
        }
        /// <summary>
        /// Pretraga uredjaja po Id
        /// </summary>
        /// <param name="id">id uredjaja</param>
        /// <returns></returns>
        [HttpGet("prettragapoid")]
        public IActionResult GetDataById(int id)
        {
            return base.GetDataById(id);
        }
        /// <summary>
        /// Kreiranje novog uredjaja
        /// </summary>
        /// <param name="input">Podaci za unos. Napomena !!! Id se ne unosi</param>
        /// <returns></returns>
        [HttpPost("kreiranjeuredjaja")]
        public IActionResult AddData(Uredjaj input)
        {
            return base.AddData(input);
        }
        /// <summary>
        /// Izmjena propertija postojeceg uredjaja
        /// </summary>
        /// <param name="id">id uredjaja koji zelimo da izmijenjamo</param>
        /// <param name="input">ime uredjaja</param>
        /// <returns></returns>
        [HttpPut("izmjenapoid/{id}")]
        public IActionResult IzmjenaPoId(int id, Uredjaj input)
        {
            return base.IzmjenaPoId(id, input);
        }

    }
}
