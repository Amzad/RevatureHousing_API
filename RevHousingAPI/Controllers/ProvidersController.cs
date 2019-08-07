using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHEntities;
using RevHousingAPI;
using Microsoft.AspNetCore.Cors;

namespace RevHousingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ProvidersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ProvidersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Providers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProvider()
        {
            return await _context.Provider.ToListAsync();
        }

        // GET: api/Providers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(int id)
        {
            var provider = await _context.Provider.FindAsync(id);

            if (provider == null)
            {
                return NotFound();
            }

            return provider;
        }

        // PUT: api/Providers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvider(int id, Provider provider)
        {
            if (id != provider.ProviderID)
            {
                return BadRequest();
            }

            _context.Entry(provider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(id))
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

        // POST: api/Providers
        [HttpPost]
        public async Task<ActionResult<Provider>> PostProvider(Provider provider)
        {
            _context.Provider.Add(provider);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvider", new { id = provider.ProviderID }, provider);
        }

        // DELETE: api/Providers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Provider>> DeleteProvider(int id)
        {
            var provider = await _context.Provider.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            _context.Provider.Remove(provider);
            await _context.SaveChangesAsync();

            return provider;
        }

        private bool ProviderExists(int id)
        {
            return _context.Provider.Any(e => e.ProviderID == id);
        }
    }
}
