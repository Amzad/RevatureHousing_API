using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevHousingAPI.DataContext;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.DAL
{
    public class ProviderDAL: IProviderContext
    {
        private readonly ApplicationDBContext _context;

        public ProviderDAL(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Provider>> AddProvider(Provider provider)
        {
            _context.Provider.Add(provider);
            await _context.SaveChangesAsync();
            return provider;
        }

        public async Task<ActionResult<Provider>> GetProvider(int id)
        {
            var provider = await _context.Provider.FindAsync(id);

            if (provider == null)
            {
                return null;
            }

            return provider;
        }

        public async Task<ActionResult<IEnumerable<Provider>>> GetAllProvider()
        {
            return await _context.Provider.ToListAsync();
        }

        public async Task<IActionResult> PutProvider(int id, Provider provider)
        {
            _context.Entry(provider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return null;
        }

        private bool ProviderExists(int id)
        {
            return _context.Provider.Any(e => e.ProviderID == id);
        }

        public async Task<ActionResult<Provider>> DeleteProvider(int id)
        {
            var provider = await _context.Provider.FindAsync(id);
            if (provider == null)
            {
                return null;
            }

            _context.Provider.Remove(provider);
            await _context.SaveChangesAsync();

            return provider;
        }
    }
}
