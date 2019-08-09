using Microsoft.EntityFrameworkCore;
using RevHousingAPI.IRepositories;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDBContext context) : base(context)
        {
        }
        public ApplicationDBContext ApplicationDBContext
        {
            get { return Context as ApplicationDBContext; }
        }
        public Location DeleteLocation(Location location)
        {

            throw new NotImplementedException();
            
        }

        public IEnumerable<Location> GetAllLocations()
        {          
            return ApplicationDBContext.Location.ToList();

        }

        public Location GetLocation(int id)
        {
            return ApplicationDBContext.Location.SingleOrDefault(c => c.LocationID == id);
        }

        public Location UpdateLocation(Location location)
        {
            throw new NotImplementedException();
        }
        //public async Task CreateLocation(Location location)
        //{
        //    _context.Location.Add(location);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteLocation(Location location)
        //{
        //    _context.Remove(location);
        //    await _context.SaveChangesAsync();

        //}

        //public async Task<Location> GetLocation(int id)
        //{
        //    return await _context.Location.FindAsync(id);
        //}

        //public async Task<IEnumerable<Location>> GetLocations()
        //{
        //    return await _context.Location.ToListAsync();
        //}

        //public async Task Save()
        //{
        //    await _context.SaveChangesAsync();
        //}


        //public async Task UpdateLocation(Location location)
        //{
        //    _context.Update(location);
        //    await _context.SaveChangesAsync();

        //}
    }
}
