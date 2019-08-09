using RevHousingAPI.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RHEntities;

namespace RevHousingAPI.Repositories
{
    public class unitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;

        public unitOfWork(ApplicationDBContext context)
        {
            _context = context;
            Rooms = new RoomRepository(_context);
        }
        public IRoomRepository Rooms { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
