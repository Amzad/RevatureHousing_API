using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RHEntities;

namespace RevHousingAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> context)
            : base(context)
        {
        }

        public DbSet<Provider> Provider { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Room> Room { get; set; }
    }
}
