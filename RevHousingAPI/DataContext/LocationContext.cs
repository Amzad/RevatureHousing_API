using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RHEntities;

namespace RevHousingAPI.DataContext
{
    public class LocationContext : ILocationContext
    {
        private ApplicationDBContext _context;
        public LocationContext(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task CreateLocation(Location location)
        {
            _context.Location.Add(location);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLocation(Location location)
        {
            _context.Remove(location);
            await _context.SaveChangesAsync();

        }

        public async Task<Location> GetLocation(int id)
        {
            return await _context.Location.FindAsync(id);
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await _context.Location.ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        

        public async Task UpdateLocation(Location location)
        {
            _context.Update(location);
            await _context.SaveChangesAsync();

        }
    }
}