using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using trainee_projectmanagement.Models;

namespace trainee_projectmanagement.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Admin> Admins { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.FullName).IsRequired().HasMaxLength(255);
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(255);
        modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.SchoolAttended).IsRequired().HasMaxLength(255);
        modelBuilder.Entity<User>().Property(u => u.Role).IsRequired().HasMaxLength(255);
        modelBuilder.Entity<User>().Property(u => u.CoursesInterested).HasMaxLength(500);

        // Configure Admin entity
        modelBuilder.Entity<Admin>().HasKey(a => a.Id);
        modelBuilder.Entity<Admin>().Property(a => a.Email).IsRequired().HasMaxLength(255);
        modelBuilder.Entity<Admin>().Property(a => a.PasswordHash).IsRequired();
        modelBuilder.Entity<Admin>().Property(a => a.FullName).IsRequired().HasMaxLength(255);

        // Seed admin user
        var adminHash = BCrypt.Net.BCrypt.HashPassword("M!rand@22");
        modelBuilder.Entity<Admin>().HasData(
            new Admin
            {
                Id = 1,
                Email = "admiranda@sscgi.com",
                PasswordHash = adminHash,
                FullName = "Admiranda Admin",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}
