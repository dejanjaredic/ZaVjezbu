using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RadnoMjestoVjezba.Dto;
using RadnoMjestoVjezba.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RadnoMjestoVjezba.Controllers
{
    [Route("api/[controller]")]
    public class BaseController<T, TDto> : Controller where T : class where TDto : class
    {
        protected readonly DataContext _context;
        private readonly DbSet<T> _dbSet;
        protected readonly IMapper _mapper;
        public BaseController(DataContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _mapper = mapper;
        }

        // GET: api/<controller>
        /// <summary>
        /// Izlistavanje svih Entiteta iz Tabele
        /// </summary>
        /// <returns>list</returns>
        [HttpGet("getalldata")]
        protected virtual IActionResult GetAllData()
        {
            
            var getAll = _dbSet.ProjectTo<TDto>(_mapper.ConfigurationProvider).ToList();
            
            return Ok(getAll);
        }
        /// <summary>
        /// Brisanje entiteta po zadatom Id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        protected virtual IActionResult DeleteData(int id)
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
        protected virtual IActionResult GetDataById(int id)
        {
            
            var getData = _dbSet.Find(id);
            var mappingData = _mapper.Map<TDto>(getData);
            return Ok(mappingData);
        }

        [HttpPost("adddata")]
        protected virtual IActionResult AddData(TDto input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (input == null)
                    {
                        return NoContent();
                    }
                    
                    
                    var mapperData = _mapper.Map<T>(input);
                    _dbSet.Add(mapperData);
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
        protected virtual IActionResult IzmjenaPoId(int id, TDto input)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {


                    var data = _dbSet.Find(id);
                    //var updated
                    //= _context.Attach(input).Entity;
                    //_context.Entry(updated).State = EntityState.Modified;
                    _mapper.Map(input, data);
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok(data);
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
        }

        

    }
}
