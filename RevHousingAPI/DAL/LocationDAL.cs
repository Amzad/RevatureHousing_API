using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RevHousingAPI.DataContext;
using RHEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevHousingAPI.DAL
{
    public class LocationDAL : ILocationContext
    {
        private readonly ApplicationDBContext _context;

        public LocationDAL(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<ActionResult<RHEntities.Location>> DeleteLocation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<RHEntities.Location>> GetLocation(int id)
        {
            throw new NotImplementedException();
        }

        public bool LocationExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<RHEntities.Location>> PostLocation(RHEntities.Location location)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> PutLocation(int id, RHEntities.Location location)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<RHEntities.Location>>> GetAllLocation()
        {
            return await _context.Location.ToListAsync();
        }


    }
}
