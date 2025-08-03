using Microsoft.EntityFrameworkCore;
using DagsDumps.API.Models;

namespace DagsDumps.API.Data
{
    public class DagsDumpsDbContext : DbContext
    {
        public DagsDumpsDbContext(DbContextOptions<DagsDumpsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Dumpster> Dumpsters { get; set; }
    }
}
