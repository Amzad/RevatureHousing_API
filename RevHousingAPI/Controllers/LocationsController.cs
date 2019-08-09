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
using RevHousingAPI.DataContext;

namespace RevHousingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class LocationsController : ControllerBase
    {

        private ILocationContext _location;

        public LocationsController( ILocationContext location)
        {
            _location = location;

        //private readonly ApplicationDBContext _context;
        private readonly ILocationContext _dal;
        public LocationsController(ILocationContext dal)
        {
            _dal = dal;

        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocation()
        {

            var locations=await  _location.GetLocations();

            if (locations == null)
            {
                return NotFound();
            }

            return Ok(locations);
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task <ActionResult> GetLocation(int id)
        {
            var location = await _location.GetLocation(id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutLocation(int id, Location location)
        {
            if (location==null)
            {
                return BadRequest(ModelState);

            }

            if (id != location.LocationID)
            {
                ModelState.AddModelError("", $"Location{location.Address} Id doesn't match");

                return BadRequest();
            }

            await _location.UpdateLocation(location);


            return NoContent();
        }

        // POST: api/Locations
        [HttpPost]
        public  ActionResult PostLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _location.CreateLocation(location);

            return CreatedAtAction("GetLocation", new { id = location.LocationID }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLocation(int id)
        {
            var location = await _location.GetLocation(id);

            if (location == null)
            {
                return NotFound();
            }

           await _location.DeleteLocation(location);

            return NoContent();
        }

      

            return await _dal.GetAllLocation();
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
}
