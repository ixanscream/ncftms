using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ncframework.Models
{
    public class IxContext : DbContext
    {
        public IxContext(DbContextOptions<IxContext> options)
            : base(options)
        { }

        public DbSet<Access> Access { get; set; }
        

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Lookup> Lookup { get; set; }
        public DbSet<Menu> Menu { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
               .HasIndex(p => new { p.Code })
               .IsUnique(true);

            modelBuilder.Entity<Lookup>()
              .HasIndex(p => new { p.Code })
              .IsUnique(true);
        }

    }
}
