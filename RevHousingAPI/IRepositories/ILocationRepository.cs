using RevHousingAPI.Repositories;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.IRepositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        IEnumerable<Location> GetAllLocations();
        Location GetLocation(int id);
        Location UpdateLocation(Location location);
        Location DeleteLocation(Location location);

    }
 
}
