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
    [Route("api/Exams")]
    [ApiVersion("1.0"), ApiVersion("1.1")]
    [Authorize]
    public class ExamsController : Controller
    {
        private readonly PitalicaDbContext _context;

        public ExamsController(PitalicaDbContext context)
        {
            _context = context;
        }

        // GET: api/Exams
        [HttpGet]
        public IEnumerable<Exams> GetExams()
        {
            return _context.Exams.Include(m => m.Questions).Include(e=>e.ExamResults);
        }

        // GET: api/Exams/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExams([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exams = await _context.Exams.SingleOrDefaultAsync(m => m.ExamId == id);

            if (exams == null)
            {
                return NotFound();
            }

            return Ok(exams);
        }

        // PUT: api/Exams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExams([FromRoute] int id, [FromBody] Exams exams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exams.ExamId)
            {
                return BadRequest();
            }

            _context.Entry(exams).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamsExists(id))
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

        // POST: api/Exams
        [HttpPost]
        public async Task<IActionResult> PostExams([FromBody] Exams exams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Exams.Add(exams);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExams", new { id = exams.ExamId }, exams);
        }

        // DELETE: api/Exams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExams([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exams = await _context.Exams.SingleOrDefaultAsync(m => m.ExamId == id);
            if (exams == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exams);
            await _context.SaveChangesAsync();

            return Ok(exams);
        }

        private bool ExamsExists(int id)
        {
            return _context.Exams.Any(e => e.ExamId == id);
        }
    }
}