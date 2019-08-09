<<<<<<< HEAD
﻿using RHEntities;
=======
﻿using Microsoft.AspNetCore.Mvc;
using RHEntities;
>>>>>>> 2917018dc422aa53a172b36b28ebd3a7eed3b34f
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.DataContext
{
    public interface ILocationContext
    {
<<<<<<< HEAD
        Task <IEnumerable<Location>> GetLocations();
         Task <Location> GetLocation(int id);
        Task CreateLocation(Location location);
        Task UpdateLocation(Location location);
        Task DeleteLocation(Location location);
        Task Save();

=======
        Task<ActionResult<IEnumerable<Location>>> GetAllLocation();
        Task<ActionResult<Location>> GetLocation(int id);
        Task<IActionResult> PutLocation(int id, Location location);
        Task<ActionResult<Location>> PostLocation(Location location);
        Task<ActionResult<Location>> DeleteLocation(int id);
        bool LocationExists(int id);
>>>>>>> 2917018dc422aa53a172b36b28ebd3a7eed3b34f
    }
}
