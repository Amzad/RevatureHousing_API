using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RHEntities;
using RevHousingAPI.Data;
using RevHousingAPI.Repositories;
using Microsoft.AspNetCore.Cors;
using RevHousingAPI.IRepositories;

namespace RevHousingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class RoomsController : ControllerBase
    {
        /// <summary>
        /// The variable within the class to access the repository pattern method when called.
        /// </summary>
        private readonly IRoomRepository _repo;
        /// <summary>
        /// The dependency injection to call the RoomRepository
        /// </summary>
        /// <param name="repo"></param>
        public RoomsController(IRoomRepository repo)
        {
            _repo = repo;      
        }
        /// <summary>
        /// This will return all data inside the Room Table
        /// </summary>
        /// <returns>Return a list of all the Rooms in the Room table</returns>
        // GET: api/Rooms
        [HttpGet]
        public async Task<IEnumerable<Room>> GetRoom()
        {
            return _repo.GetAll();
        }

        /// <summary>
        /// This is not the recomended method to get room. This methoad will called the default get room methoad 
        /// inside the Repostiory class. This does not have the 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = _repo.Get(id);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }

        [HttpGet("Inactive/{id}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetInactiveRoomByID(int Locationid)
        {
            var room = await _repo.GetInactiveRoomAsync(Locationid); 
            
            if (room == null)
            {
                return NoContent();

            }

            return room;
        }

        [HttpGet("Location/{id}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomWithLocationID(int id)
        {
            var room = await _repo.GetRoomWithLocation(id);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }
        // PUT: api/Rooms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int? id, Room room)
        {
            _repo.Update(room);

            return NoContent();
            /*if (id != room.RoomID)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/
        }

        // POST: api/Rooms
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            _repo.Add(room);

            return StatusCode(201);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            bool isRemoved = _repo.RemoveRoom(id);
            if (isRemoved == false)
            {
                return NotFound();
            }

            return StatusCode(201);
        }

        //private bool RoomExists(int id)
        //{
        //    return _context.Room.Any(e => e.RoomID == id);
        //}
    }
}
