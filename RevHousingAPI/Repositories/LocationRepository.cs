using Microsoft.EntityFrameworkCore;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RevHousingAPI.IRepositories;
using RevHousingAPI.Data;

namespace RevHousingAPI.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private readonly ApplicationDBContext Context;
        public LocationRepository(ApplicationDBContext context) : base(context)
        {
            Context = context;
        }

        public bool RemoveLocation(int id)
        {
            Location location = Context.Location.Find(id);
            if (location == null)
            {
                return false;
            }
            Context.Location.Remove(location);
            return true;
        }
        public IEnumerable<Location> GetAllLocations()
        {          
            return Context.Location.ToList();

        }
        public IEnumerable<Location> GetLocationByTraningCenter(string TrainingCenter)
        {
            return Context.Location.Where(c => c.TrainingCenter == TrainingCenter);
        }
        public IEnumerable<Location> GetLocationByProviderID(string ProviderId)
        {
            return Context.Location.Where(c=> c.ProviderID == ProviderId).ToList();

        }
    }
}
