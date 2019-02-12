using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BaseController<T> : Controller where T : class 
    {
        protected readonly DataContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseController(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        // GET: api/<controller>
        /// <summary>
        /// Izlistavanje svih Entiteta iz Tabele
        /// </summary>
        /// <returns>list</returns>
        [HttpGet("getalldata")]
        public virtual IActionResult GetAllData()
        {
            var getAll = _dbSet;
            
            var getAllQuery =
                getAll.Select(x => x);

            return Ok(getAllQuery.ToList());
        }
        /// <summary>
        /// Brisanje entiteta po zadatom Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public virtual IActionResult DeleteData(int id)
        {
            var deleteData = _dbSet.Find(id);
            if (deleteData == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(deleteData);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                var greska = new GreskaDto
                {
                    Poruka = "Brisanje uredjaja nije dozvoljeno "

                };
                return BadRequest(greska);
            }

            return Ok(deleteData);

        }
        /// <summary>
        /// Pretraga entiteta po Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid/{id}")]
        public virtual IActionResult EditData(int id)
        {
            
            var uredjaji = _dbSet.Find(id);
            return Ok(uredjaji);
        }

        [HttpPost("adddata")]
        public virtual IActionResult AddData(T input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (input == null)
                    {
                        return NoContent();
                    }

                    var uredjaj = input;
                    _context.Add(uredjaj);
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok(" Kreiran u Tabeli Uredjaji");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        /// <summary>
        /// Izmjena entiteta po Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("mijenjanje/{id}")]
        public virtual IActionResult IzmjenaPoId(int id, T input)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {


                    // var uredjaji = _dbSet.Find(id);
                    var updated
                    = _context.Attach(input).Entity;
                    _context.Entry(updated).State = EntityState.Modified;
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok(updated);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }
        

    }
}
