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
        public IEnumerable<Location> GetAllLocations()
        {          
            return ApplicationDBContext.Location.ToList();

        }
        public IEnumerable<Location> GetLocationByTraningCenter(string TrainingCenter)
        {
            return ApplicationDBContext.Location.Where(c => c.TrainingCenter == TrainingCenter);
        }
    }
}
