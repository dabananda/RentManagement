using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentManagement.Api.Models;
using RentManagement.Api.Security;

namespace RentManagement.Api.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUser _currentUser;
        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUser currentUser) : base(options)
        {
            _currentUser = currentUser;
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<RentalAgreement> RentalAgreements { get; set; }
        public DbSet<RentRecord> RentRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shop>()
                .HasQueryFilter(e => e.CreatedByUserId == _currentUser.UserId);

            modelBuilder.Entity<Tenant>()
                .HasQueryFilter(e => e.CreatedByUserId == _currentUser.UserId);

            modelBuilder.Entity<RentalAgreement>()
                .HasQueryFilter(e => e.CreatedByUserId == _currentUser.UserId);

            modelBuilder.Entity<RentRecord>()
                .HasQueryFilter(e => e.CreatedByUserId == _currentUser.UserId);

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

            modelBuilder.Entity<RentRecord>()
                .HasIndex(r => new { r.AgreementId, r.Year, r.Month })
                .IsUnique();

            modelBuilder.Entity<RentRecord>()
                .HasOne(r => r.Agreement)
                .WithMany()
                .HasForeignKey(r => r.AgreementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
