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
        private readonly ApplicationDBContext _Context;
        public LocationRepository(ApplicationDBContext context) : base(context)
        {
            _Context = context;
        }

        public bool RemoveLocation(int id)
        {
            Location location = _Context.Location.Find(id);
            if (location == null)
            {
                return false;
            }

            _Context.Location.Remove(location);

            SaveChanges();
            return true;
        }
        public IEnumerable<Location> GetAllLocations()
        {          
            return _Context.Location.ToList();

        }
        public IEnumerable<Location> GetLocationByTraningCenter(string TrainingCenter)
        {
            return _Context.Location.Where(c => c.TrainingCenter == TrainingCenter);
        }
        public IEnumerable<Location> GetLocationByProviderID(string ProviderId)
        {
            return _Context.Location.Where(c=> c.ProviderID == ProviderId).ToList();

        }
    }
}
