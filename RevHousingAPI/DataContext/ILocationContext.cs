using Microsoft.AspNetCore.Mvc;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.DataContext
{
    public interface ILocationContext
    {
        Task<ActionResult<IEnumerable<Location>>> GetAllLocation();
        Task<ActionResult<Location>> GetLocation(int id);
        Task<IActionResult> PutLocation(int id, Location location);
        Task<ActionResult<Location>> PostLocation(Location location);
        Task<ActionResult<Location>> DeleteLocation(int id);
        bool LocationExists(int id);
    }
}
