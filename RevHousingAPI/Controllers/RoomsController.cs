﻿using System;
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
        private readonly ApplicationDBContext _context;
        private readonly IRoomRepository _repo;

        public RoomsController(IRoomRepository repo)
        {
            //_context = context;
            _repo = repo;        //  new RoomRepository(_context);
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<IEnumerable<Room>> GetRoom()
        {
            return _repo.GetAll();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            return _repo.Get(id);
            /*var room = await _context.Room.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;*/
        }
        [HttpGet("Location/{id}")]
        public async Task<IEnumerable<Room>> GetRoomWithLocationID(int id)
        {
            return _repo.GetRoomWithLocation(id);
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

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.RoomID == id);
        }
    }
}
