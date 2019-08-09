using Microsoft.EntityFrameworkCore;
using RevHousingAPI.IRepositories;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDBContext context) : base(context)
        {
        }
        public Room GetRoomWithLocation(int id)
        {
            return ApplicationDBContext.Room.Include(a => a.LocationID).SingleOrDefault(a => a.LocationID == id);
        }
        public ApplicationDBContext ApplicationDBContext
        {
            get { return Context as ApplicationDBContext; }
        }
    }
}
