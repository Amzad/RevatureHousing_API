using Microsoft.AspNetCore.Mvc;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.IRepositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        IEnumerable<Room> GetRoomWithLocation(int id);
        Task<ActionResult<IEnumerable<Room>>> GetInactiveRoomAsync(int Locationid);

        bool RemoveRoom(int id);
    }
}