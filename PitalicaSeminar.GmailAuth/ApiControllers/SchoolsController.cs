using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PitalicaSeminar.DAL.Entities;

namespace PitalicaSeminar.GmailAuth.ApiControllers
{
    [Produces("application/json")]
    [Route("api/Schools")]
    [ApiVersion("1.0"), ApiVersion("1.1")]
    [Authorize]
    public class SchoolsController : Controller
    {
        private readonly PitalicaDbContext _context;

        public SchoolsController(PitalicaDbContext context)
        {
            _context = context;
        }

        // GET: api/Schools
        [HttpGet]
        public IEnumerable<Schools> GetSchools()
        {
            return _context.Schools.Include(m => m.Users);
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchools([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schools = await _context.Schools.SingleOrDefaultAsync(m => m.SchoolId == id);

            if (schools == null)
            {
                return NotFound();
            }

            return Ok(schools);
        }

        // PUT: api/Schools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchools([FromRoute] int id, [FromBody] Schools schools)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schools.SchoolId)
            {
                return BadRequest();
            }

            _context.Entry(schools).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Schools
        [HttpPost]
        public async Task<IActionResult> PostSchools([FromBody] Schools schools)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Schools.Add(schools);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchools", new { id = schools.SchoolId }, schools);
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchools([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schools = await _context.Schools.SingleOrDefaultAsync(m => m.SchoolId == id);
            if (schools == null)
            {
                return NotFound();
            }

            _context.Schools.Remove(schools);
            await _context.SaveChangesAsync();

            return Ok(schools);
        }

        private bool SchoolsExists(int id)
        {
            return _context.Schools.Any(e => e.SchoolId == id);
        }
    }
}