﻿using Microsoft.EntityFrameworkCore;
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
        public RoomRepository(ApplicationDBContext context) : base(context)
        {
            _Context = context;
        }

        public async Task<ActionResult<IEnumerable<Room>>> GetInactiveRoomAsync(int Locationid)
        {
            //throw new NotImplementedException();
            return await _Context.Room.Where(h => h.LocationID == Locationid && h.IsActive == false).ToListAsync();
        }

        public IEnumerable<Room> GetRoomWithLocation(int id)
        {
            return _Context.Room.Where(c => c.LocationID == id && c.IsActive == true).ToList();
        }

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
    }
}