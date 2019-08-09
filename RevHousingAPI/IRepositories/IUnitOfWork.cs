using RevHousingAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRoomRepository Rooms { get; }
        int Complete();
    }
}
