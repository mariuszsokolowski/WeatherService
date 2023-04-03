using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Data.Entities;

namespace WeatherService.Data
{
    public class DBContext : DbContext
    {
        public DbSet<City> City { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().
            HasIndex(e => new { e.CountryCode, e.Name })
            .IsUnique();

            modelBuilder.Entity<City>()
            .Property(x => x.CountryCode).HasMaxLength(2);

            modelBuilder.Entity<City>()
             .Property(x => x.CreateDate).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}