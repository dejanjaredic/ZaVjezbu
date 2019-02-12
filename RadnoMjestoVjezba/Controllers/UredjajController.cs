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
    public class UredjajController : BaseController<Uredjaj>
    {
        protected readonly DataContext _context;
        public UredjajController(DataContext context) : base(context)
        {
        }
        

    }
}
