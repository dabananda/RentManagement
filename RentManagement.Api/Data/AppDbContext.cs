using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentManagement.Api.Models;

namespace RentManagement.Api.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
    }
}
