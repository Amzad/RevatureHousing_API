using Microsoft.EntityFrameworkCore;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RevHousingAPI.IRepositories;
using RevHousingAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace RevHousingAPI.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private readonly ApplicationDBContext _Context;
        public LocationRepository(ApplicationDBContext context) : base(context)
        {
            _Context = context;
        }
        /// <summary>
        ///Return true if location is deleted return false if location is null 
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <returns>Bool true or false</returns>
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
        /// <summary>
        /// Return all locations that exist in the location table database.
        /// </summary>
        /// <returns>List of location object</returns>
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations()
        {          
            return await _Context.Location.ToListAsync();

        }
        /// <summary>
        /// use the trainng center as a search condition to find object that is related to that training center.
        /// </summary>
        /// <param name="TrainingCenter">The name of the training center</param>
        /// <returns> A list of location object</returns>
        public async Task<ActionResult<IEnumerable<Location>>> GetLocationByTraningCenter(string TrainingCenter)
        {
            return await _Context.Location.Where(c => c.TrainingCenter == TrainingCenter).ToListAsync();
        }
        /// <summary>
        /// Search Databse to find all location with the same provider ID.
        /// </summary>
        /// <param name="ProviderId">provider ID string from azure ad </param>
        /// <returns>A list of location Object</returns>
        public async Task<ActionResult<IEnumerable<Location>>> GetLocationByProviderID(string ProviderId)
        {
            return await _Context.Location.Where(c=> c.ProviderID == ProviderId).ToListAsync();

        }
    }
}
