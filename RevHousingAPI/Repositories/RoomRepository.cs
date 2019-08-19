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
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly ApplicationDBContext _Context;
        /// <summary>
        /// constructor and database service injection
        /// </summary>
        /// <param name="context"></param>
        public RoomRepository(ApplicationDBContext context) : base(context)
        {
            _Context = context;
        }

        /// <summary>
        /// search the database to find a list room with the location id and rooms are not acitve
        /// </summary>
        /// <param name="Locationid"></param>
        /// <returns>a list of rooms are not acitve</returns>
        public async Task<ActionResult<IEnumerable<Room>>> GetInactiveRoomAsync(int Locationid)
        {
            //throw new NotImplementedException();
            return await _Context.Room.Where(h => h.LocationID == Locationid && h.IsActive == false).ToListAsync();
        }

        /// <summary>
        /// search the database to find a list of room with the location id and rooms are acitve 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a list of rooms are acitve</returns>
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomWithLocation(int id)
        {
            return await _Context.Room.Where(c => c.LocationID == id && c.IsActive == true).ToListAsync();
        }

        /// <summary>
        /// Search the room id in the database if it's found change it to not active
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true or false if the room is found</returns>
        public bool RemoveRoom(int id)
        {
            Room room = _Context.Room.Find(id);
            if (room == null)
            {
                return false;
            }

            room.IsActive = false;
            SaveChanges();
            return true;
        }

        /// <summary>
        /// search the database to check if the room is exist in the dabatse 
        /// </summary>
        /// <param name="room"></param>
        /// <returns>true or false if the room is found</returns>
        public bool isRoomExist(Room room)
        {
            Room loc = _Context.Room.Find(room.RoomID);
            if (loc == null)
            {
                return false;
            }
            return true;

        }
    }
}