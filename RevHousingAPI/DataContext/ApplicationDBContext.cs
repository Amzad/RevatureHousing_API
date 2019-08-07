using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RHEntities;

namespace RevHousingAPI
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> context)
            : base(context)
        {
        }

        //protected override void onconfiguring(dbcontextoptionsbuilder optionsbuilder)
        //{
        //    if (!optionsbuilder.isconfigured)
        //    {
        //        optionsbuilder.usesqlserver(@"Server=tcp:team7pizza.database.windows.net,1433;Initial Catalog=team7pizzaDB;Persist Security Info=False;User ID=team7;Password=RevatureQNS1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        //    }
        //}

        public DbSet<Provider> Provider { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Room> Room { get; set; }
    }
}
