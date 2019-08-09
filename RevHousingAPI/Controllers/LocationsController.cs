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

      
    }
}
