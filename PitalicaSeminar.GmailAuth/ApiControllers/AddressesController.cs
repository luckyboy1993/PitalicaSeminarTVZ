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
    [Route("api/Addresses")]
    [ApiVersion("1.0"), ApiVersion("1.1")]
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly PitalicaDbContext _context;

        public AddressesController(PitalicaDbContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public IEnumerable<Addresses> GetAddresses()
        {
            return _context.Addresses;
        }

        // GET: api/Addresses/5
        [HttpGet("/get/{id}")]
        public async Task<IActionResult> GetAddresses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addresses = await _context.Addresses.SingleOrDefaultAsync(m => m.AddressId == id);

            if (addresses == null)
            {
                return NotFound();
            }

            return Ok(addresses);
        }

        // PUT: api/Addresses/5
        [HttpPut("/update/{id}")]
        public async Task<IActionResult> PutAddresses([FromRoute] int id, [FromBody] Addresses addresses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addresses.AddressId)
            {
                return BadRequest();
            }

            _context.Entry(addresses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressesExists(id))
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

        // POST: api/Addresses
        [HttpPost("create")]
        public async Task<IActionResult> PostAddresses([FromBody] Addresses addresses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Addresses.Add(addresses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddresses", new { id = addresses.AddressId }, addresses);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("/delete/{id}")]
        public async Task<IActionResult> DeleteAddresses([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addresses = await _context.Addresses.SingleOrDefaultAsync(m => m.AddressId == id);
            if (addresses == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(addresses);
            await _context.SaveChangesAsync();

            return Ok(addresses);
        }

        private bool AddressesExists(int id)
        {
            return _context.Addresses.Any(e => e.AddressId == id);
        }
    }
}