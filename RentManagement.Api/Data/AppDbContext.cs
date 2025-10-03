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
        public DbSet<RentalAgreement> RentalAgreements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RentalAgreement>()
                .HasOne(ra => ra.Shop)
                .WithMany(s => s.RentalAgreements)
                .HasForeignKey(ra => ra.ShopId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentalAgreement>()
                .HasOne(ra => ra.Tenant)
                .WithMany(t => t.RentalAgreements)
                .HasForeignKey(ra => ra.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentalAgreement>()
                .HasIndex(ra => new { ra.ShopId, ra.IsActive })
                .IsUnique()
                .HasFilter("[IsActive] = 1");
        }
    }
}
