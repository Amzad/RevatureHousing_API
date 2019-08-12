using Microsoft.EntityFrameworkCore;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RevHousingAPI.IRepositories;
using RevHousingAPI.Data;

namespace RevHousingAPI.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly ApplicationDBContext Context;
        public RoomRepository(ApplicationDBContext context) : base(context)
        {
            Context = context;
        }
        public IEnumerable<Room> GetRoomWithLocation(int id)
        {
            return Context.Room.Where(c => c.LocationID == id && c.IsActive == true).ToList();
        }
        public ApplicationDBContext ApplicationDBContext
        {
            get { return Context as ApplicationDBContext; }
        }

        public bool RemoveRoom(int id)
        {
            Room room = Context.Room.Find(id);
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