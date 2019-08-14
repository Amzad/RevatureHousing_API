using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHEntities;
using RevHousingAPI;
using Microsoft.AspNetCore.Cors;
using RevHousingAPI.Data;
using RevHousingAPI.IRepositories;
using RevHousingAPI.Repositories;
using System.Net;

namespace RevHousingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class LocationsController : ControllerBase
    {
        /// <summary>
        /// Private local variable for the dependency injection
        /// </summary>
       // private readonly ApplicationDBContext _context;
        private readonly ILocationRepository _repo;
        /// <summary>
        /// Default constructor for the dependency injection   
        /// </summary>
        /// <param name="repo">The location Repository</param>
        public LocationsController(ILocationRepository repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// This is the default for get location not customrize.
        /// </summary>
        /// <returns>This will return all the location that exist in the database.</returns>
        // GET: api/Locations
        [HttpGet]
        public async Task<IEnumerable<Location>> GetLocation()
        {
            return _repo.GetAll();
        }
        /// <summary>
        /// This Api controller will search all the location under the same provider.
        /// </summary>
        /// <param name="providerId">The provider id come form the azure ad and this string is in the location table.</param>
        /// <returns>Return a list of location object.</returns>
        [HttpGet("Provider/{ProviderID}")]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocationByProvider(string providerId)
        {
            return await _repo.GetLocationByProviderID(providerId);
        }
        /// <summary>
        /// Will search avilable location by training center. 
        /// </summary>
        /// <param name="tclocation">the training center string in the location table</param>
        /// <returns>A list of Location object.</returns>
        [HttpGet("Site/{TCLocation}")]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocationByTrainingCenter(string tclocation)
        {
            return await _repo.GetLocationByTraningCenter(tclocation);
        }
        /// <summary>
        /// default get that will get one location, with the unqiue location id
        /// </summary>
        /// <param name="id">Primary key Loaction Id </param>
        /// <returns>One location object with that id.</returns>
        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            return _repo.Get(id);
        }
        /// <summary>
        /// update location object in the location table.
        /// </summary>
        /// <param name="id">the location id</param>
        /// <param name="location">the location object</param>
        /// <returns>return status code 204 or bad request  </returns>
        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {

            if (location == null)
            {
                return BadRequest(ModelState);

            }

            else
            {
                _repo.Update(location);

                return NoContent();
            }
          
            /*

            if (id != location.LocationID)
            {
                ModelState.AddModelError("", $"Location{location.Address} Id doesn't match");

                return BadRequest();
            }

            await _location.UpdateLocation(location);


            return NoContent();*/
        }
        /// <summary>
        /// Add new location to the database
        /// </summary>
        /// <param name="location">accept location object with all the parameter</param>
        /// <returns>201 created</returns>
        // POST: api/Locations
        [HttpPost]
        public ActionResult PostLocation(Location location)
        {
            _repo.Add(location);

            return StatusCode(201);
        }
        /// <summary>
        /// Controller to be updated ... this is incorrect
        /// </summary>
        /// <param name="LocationId">Location</param>
        /// <returns>OK</returns>
        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Location>> DeleteLocation(int LocationId)
        {
            bool isRemoved = _repo.RemoveLocation(LocationId);
            if (isRemoved == false)
            {
                return NotFound();
            }

            return Ok();
        }
    }

    //// GET: api/Locations/5
    //[HttpGet("{id}")]
    //public async Task<ActionResult<Location>> GetLocation(int id)
    //{
    //    var location = await _context.Location.FindAsync(id);

    //    if (location == null)
    //    {
    //        return NotFound();
    //    }

    //    return location;
    //}

    //// PUT: api/Locations/5
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutLocation(int id, Location location)
    //{
    //    if (id != location.LocationID)
    //    {
    //        return BadRequest();
    //    }

    //    _context.Entry(location).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!LocationExists(id))
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }

    //    return NoContent();
    //}

    //// POST: api/Locations
    //[HttpPost]
    //public async Task<ActionResult<Location>> PostLocation(Location location)
    //{
    //    _context.Location.Add(location);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction("GetLocation", new { id = location.LocationID }, location);
    //}

    //// DELETE: api/Locations/5
    //[HttpDelete("{id}")]
    //public async Task<ActionResult<Location>> DeleteLocation(int id)
    //{
    //    var location = await _context.Location.FindAsync(id);
    //    if (location == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Location.Remove(location);
    //    await _context.SaveChangesAsync();

    //    return location;
    //}

    //private bool LocationExists(int id)
    //{
    //    return _context.Location.Any(e => e.LocationID == id);
    //}


}
