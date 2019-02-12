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
        [HttpGet("getalldata")]
        public IActionResult GetAllData()
        {
            return base.GetAllData();
        }

        [HttpDelete("brisanjepoid")]
        public IActionResult DeleteData(int id)
        {
            return base.DeleteData(id);
        }

        [HttpGet("prettragapoid")]
        public IActionResult GetDataById(int id)
        {
            return base.GetDataById(id);
        }

        [HttpPost("kreiranjeuredjaja")]
        public IActionResult AddData(Uredjaj input)
        {
            return base.AddData(input);
        }

        [HttpPut("izmjenapoid/{id}")]
        public IActionResult IzmjenaPoId(int id, Uredjaj input)
        {
            return base.IzmjenaPoId(id, input);
        }

    }
}
