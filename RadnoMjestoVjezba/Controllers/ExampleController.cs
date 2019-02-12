using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RadnoMjestoVjezba.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RadnoMjestoVjezba.Controllers
{
    [Route("api/[controller]")]
    public class ExampleController : BaseController<Uredjaj>
    {
        public ExampleController(DataContext context) : base(context)
        {
        }

        public override IActionResult GetAllData()
        {
            return base.GetAllData();
        }
        public override IActionResult DeleteData(int id)
        {
            return base.DeleteData(id);
        }

    }
}
