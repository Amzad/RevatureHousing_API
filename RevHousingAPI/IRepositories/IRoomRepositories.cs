﻿using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.IRepositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        IEnumerable<Room> GetRoomWithLocation(int id);

        bool RemoveRoom(int id);
    }
}