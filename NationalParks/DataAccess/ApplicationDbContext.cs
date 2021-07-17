using Microsoft.EntityFrameworkCore;
using NationalParks.Models;

namespace NationalParks.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        public DbSet<Class1> Classes { get; set; }
        public DbSet<Location_1> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class1>()
            .ToTable("Class1");

            modelBuilder.Entity<Location_1>()
            .ToTable("Location1");

            //base.OnModelCreating(modelBuilder);
        }
    }
}