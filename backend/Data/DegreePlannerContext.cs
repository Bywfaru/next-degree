using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DegreePlanner.Entities;

namespace DegreePlanner.Data
{
    public class DegreePlannerContext : DbContext
    {
        public DegreePlannerContext (DbContextOptions<DegreePlannerContext> options)
            : base(options)
        {
        }

        public DbSet<DegreePlanner.Entities.User> User { get; set; } = default!;
        public DbSet<DegreePlanner.Entities.Course> Course { get; set; } = default!;
    }
}
