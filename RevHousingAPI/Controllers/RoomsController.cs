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
        /// inside the Repostiory class. This does not have the check condition for if the room is active!
        /// </summary>
        /// <param name="RoomID"></param>
        /// <returns>Return the room object with that room id.</returns>
        // GET: api/Rooms/5
        [HttpGet("{RoomID}")]
        public ActionResult<Room> GetRoom(int RoomID)
        {
            var room = _repo.Get(RoomID);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }
        /// <summary>
        /// This mehoad will get a list of Room that is inactive that is related to that location ID. 
        /// </summary>
        /// <param name="LocationID">LocationID is the FK that is inside the Room Table</param>
        /// <returns>A list of Room Object</returns>
        [HttpGet("Inactive/{LocationID}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetInactiveRoomByID(int LocationID)
        {
            var room = await _repo.GetInactiveRoomAsync(LocationID); 
            
            if (room == null)
            {
                return NoContent();

            }

            return room;
        }
        /// <summary>
        /// Connect to the GetRoomWithLocation mehoad in the Room Repository folder. 
        /// </summary>
        /// <param name="LocationID">LocationID is the FK that is inside the Room Table</param>
        /// <returns>A list of Room Object</returns>
        [HttpGet("Location/{LocationID}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomWithLocationID(int LocationID)
        {
            var room = await _repo.GetRoomWithLocation(LocationID);
            if (room == null)
            {
                return NotFound();
            }
            return room;
        }
        /// <summary>
        /// The default Put method to update Room in DB.
        /// </summary>
        /// <param name="RoomID">The Room ID</param>
        /// <param name="room">The room object being change</param>
        /// <returns>statuscode: 204 when success, BadRequest if the room id doesnt match.</returns>
        // PUT: api/Rooms/5
        [HttpPut("{RoomID}")]
        public async Task<IActionResult> PutRoom(int? RoomID, Room room)
        {
            if(RoomID != room.RoomID)
            {
                return BadRequest();
            }
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
        /// <summary>
        /// This api controller to Add Room object to the room table.
        /// </summary>
        /// <param name="room">The Room Object To be add to DB</param>
        /// <returns>Return 201 for created</returns>
        // POST: api/Rooms
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            if (!_repo.isRoomExist(room))
            {
                _repo.Add(room);
                return StatusCode(201);
            }
            return StatusCode(409);
        }
        /// <summary>
        /// This Api controller is use to deactive room. 
        /// </summary>
        /// <param name="RoomID">The Room Id</param>
        /// <returns>Return Not Found, or Statuscode 202 </returns>
        // DELETE: api/Rooms/5
        [HttpDelete("{RoomID}")]
        public async Task<ActionResult<Room>> DeleteRoom(int RoomID)
        {
            bool isRemoved = _repo.RemoveRoom(RoomID);
            if (isRemoved == false)
            {
                return NotFound();
            }

            return StatusCode(202);
        }

        //private bool RoomExists(int id)
        //{
        //    return _context.Room.Any(e => e.RoomID == id);
        //}
    }
}
