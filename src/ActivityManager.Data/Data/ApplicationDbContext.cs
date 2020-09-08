using System.Threading.Tasks;
using ActivityManager.Data.Entities;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ActivityManager.Data
{
    public class ApplicationDbContext : IdentityDbContext, IDataProtectionKeyContext, IPersistedGrantDbContext
    {
        public ApplicationDbContext() : this(
            new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ActivityManager;Trusted_Connection=True;MultipleActiveResultSets=true")
            .Options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Asp.Net Core
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        // Identity Server 4
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        // Activity Manager
        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Asp.Net Core
            modelBuilder.Entity<DataProtectionKey>()
                .ToTable("DataProtectionKeys");

            // Identity Server 4
            modelBuilder.Entity<PersistedGrant>()
                .ToTable("PersistedGrants")
                .HasKey(o => new { o.Key });
            modelBuilder.Entity<DeviceFlowCodes>()
                .ToTable("DeviceFlowCodes")
                .HasKey(o => new { o.UserCode });

            // Activity Manager
            modelBuilder.Entity<Activity>().ToTable("Activities");
            modelBuilder.Entity<UserActivity>()
                .ToTable("UserActivities")
                .HasKey(o => new { o.UserId, o.ActivityId });
        }
    }
}
