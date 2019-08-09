using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.Repositories
{
    public interface IRoomRepository: IRepository<Room>
    {
        Room GetRoomWithLocation(int id);
    }
}
