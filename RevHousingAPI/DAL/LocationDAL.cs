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


    }
}
