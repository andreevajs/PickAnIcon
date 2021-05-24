using Microsoft.EntityFrameworkCore;
using PickAnIcon.Database.Entities;
using System;

namespace PickAnIcon.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}
