using CoreVision.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreVision.Infrastructure.Contexts
{
    public class CoreVisionDbContext : IdentityDbContext
    {
        public CoreVisionDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>(entity =>
            {
                entity.ToTable("Companies");
                entity.HasIndex(e => e.RNC).IsUnique();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            });

            builder.Entity<TimeLog>(entity =>
            {
                entity.ToTable("TimeLogs");
                entity.Property(e => e.EntryTime).HasColumnType("time");
                entity.Property(e => e.DepartureTime).HasColumnType("time");
                entity.Property(e => e.HoursWorked).HasPrecision(18, 2);
                entity.Property(e => e.Status).HasConversion<string>();
            });

            builder.Entity<Internship>(entity =>
            {
                entity.ToTable("InterShips");
                entity.Property(e => e.Status).HasConversion<string>();
            });

            builder.Entity<SupervisorIntern>(entity =>
            {
                entity.ToTable("SupervisorInterns");
            });
         
            builder.Entity<Report>(entity =>
            {
                entity.ToTable("Reports");
            });
        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SupervisorIntern> SupervisorInterns { get; set; }
        public DbSet<Internship> Internships { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}