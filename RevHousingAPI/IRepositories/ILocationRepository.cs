using Microsoft.AspNetCore.Mvc;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.IRepositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        bool RemoveLocation(int id);
        Task<ActionResult<IEnumerable<Location>>> GetAllLocations();
        Task<ActionResult<IEnumerable<Location>>> GetLocationByTraningCenter(string TrainingCenter);
        Task<ActionResult<IEnumerable<Location>>> GetLocationByProviderID(string ProviderID);
    }

}
