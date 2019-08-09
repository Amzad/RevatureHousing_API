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
using RevHousingAPI.DataContext;

namespace RevHousingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ProvidersController : ControllerBase
    {
        private readonly IProviderContext _dal;
        public ProvidersController(IProviderContext dal)
        {
            _dal = dal;
        }

        // GET: api/Providers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProvider()
        {
            return await _dal.GetAllProvider();
        }

        //// GET: api/Providers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(int id)
        {
            var provider = await _dal.GetProvider(id);
            if (provider == null)
            {
                return NotFound();
            }
            return provider;
        }

        //// PUT: api/Providers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvider(int id, Provider provider)
        {
            if (id != provider.ProviderID)
            {
                return BadRequest();
            }

            //_dal.Entry(provider).State = EntityState.Modified;


            await _dal.PutProvider(id, provider);


            return NoContent();
        }

        // POST: api/Providers
        [HttpPost]
        public async Task<ActionResult<Provider>> PostProvider(Provider provider)
        {
            //_context.Provider.Add(provider);
            //await _context.SaveChangesAsync();
            await _dal.AddProvider(provider);
            return CreatedAtAction("GetProvider", new { id = provider.ProviderID }, provider);
        }

        //// DELETE: api/Providers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Provider>> DeleteProvider(int id)
        {
            var provider = await _dal.DeleteProvider(id);
            if (provider == null)
            {
                return NotFound();
            }

            return provider;
        }

        //private bool ProviderExists(int id)
        //{
        //    return _context.Provider.Any(e => e.ProviderID == id);
        //}
    }
}
