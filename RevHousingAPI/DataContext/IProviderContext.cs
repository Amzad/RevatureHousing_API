using Microsoft.AspNetCore.Mvc;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.DataContext
{
    public interface IProviderContext
    {
        Task<ActionResult<IEnumerable<Provider>>> GetAllProvider();
        Task<ActionResult<Provider>> AddProvider(Provider provider);
        Task<ActionResult<Provider>> GetProvider(int id);
        Task<IActionResult> PutProvider(int id, Provider provider);
        Task<ActionResult<Provider>> DeleteProvider(int id);
    }
}
