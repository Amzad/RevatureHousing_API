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

       // private readonly ApplicationDBContext _context;
        private readonly ILocationRepository _repo;

        public LocationsController(ILocationRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<IEnumerable<Location>> GetLocation()
        {
            return _repo.GetAll();
        }
        [HttpGet("Provider/{ProviderID}")]
        public async Task<IEnumerable<Location>> GetLocationByProvider(string providerId)
        {
            return _repo.GetLocationByProviderID(providerId);
        }
        [HttpGet("Site/{TCLocation}")]
        public async Task<IEnumerable<Location>> GetLocationByTrainingCenter(string tclocation)
        {
            return _repo.GetLocationByTraningCenter(tclocation);
        }
        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            return _repo.Get(id);
        }

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

        // POST: api/Locations
        [HttpPost]
        public ActionResult PostLocation(Location location)
        {
            _repo.Add(location);

            return StatusCode(201);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Location>> DeleteLocation(Location location)
        {
            bool isRemoved = _repo.RemoveLocation(location.LocationID);
            if (isRemoved == false)
            {
                return NotFound();
            }

            return location;
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
