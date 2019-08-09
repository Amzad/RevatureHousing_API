using RevHousingAPI.IRepositories;
using RevHousingAPI.Repositories;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.Controllers
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDBContext _context;

        public UnitOfWork(ApplicationDBContext context)
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
