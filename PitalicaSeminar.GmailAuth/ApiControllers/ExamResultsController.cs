using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PitalicaSeminar.DAL.Entities;

namespace PitalicaSeminar.GmailAuth.ApiControllers
{
    [Produces("application/json")]
    [Route("api/ExamResults")]
    public class ExamResultsController : Controller
    {
        private readonly PitalicaDbContext _context;

        public ExamResultsController(PitalicaDbContext context)
        {
            _context = context;
        }

        // GET: api/ExamResults
        [HttpGet]
        public IEnumerable<ExamResults> GetExamResults()
        {
            return _context.ExamResults;
        }

        // GET: api/ExamResults/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamResults([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var examResults = await _context.ExamResults.SingleOrDefaultAsync(m => m.Id == id);

            if (examResults == null)
            {
                return NotFound();
            }

            return Ok(examResults);
        }

        // PUT: api/ExamResults/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamResults([FromRoute] int id, [FromBody] ExamResults examResults)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != examResults.Id)
            {
                return BadRequest();
            }

            _context.Entry(examResults).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamResultsExists(id))
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

        // POST: api/ExamResults
        [HttpPost]
        public async Task<IActionResult> PostExamResults([FromBody] ExamResults examResults)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ExamResults.Add(examResults);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExamResults", new { id = examResults.Id }, examResults);
        }

        // DELETE: api/ExamResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamResults([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var examResults = await _context.ExamResults.SingleOrDefaultAsync(m => m.Id == id);
            if (examResults == null)
            {
                return NotFound();
            }

            _context.ExamResults.Remove(examResults);
            await _context.SaveChangesAsync();

            return Ok(examResults);
        }

        private bool ExamResultsExists(int id)
        {
            return _context.ExamResults.Any(e => e.Id == id);
        }
    }
}