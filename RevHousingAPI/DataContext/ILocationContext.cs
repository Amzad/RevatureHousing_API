using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.DataContext
{
    public interface ILocationContext
    {
        Task <IEnumerable<Location>> GetLocations();
         Task <Location> GetLocation(int id);
        Task CreateLocation(Location location);
        Task UpdateLocation(Location location);
        Task DeleteLocation(Location location);
        Task Save();

    }
}
