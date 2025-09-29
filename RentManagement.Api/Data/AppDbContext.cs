using Microsoft.EntityFrameworkCore;
using RentManagement.Api.Models;

namespace RentManagement.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Shop> Shops { get; set; }
    }
}
