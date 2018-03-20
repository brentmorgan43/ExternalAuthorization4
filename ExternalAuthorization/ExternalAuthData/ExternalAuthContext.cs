using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalAuthData
{
    public class ExternalAuthContext : DbContext
    {
        public ExternalAuthContext(DbContextOptions<ExternalAuthContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Claim>().ToTable("Claim");
        }
    }

    public class ExternalAuthContextFactory : IDesignTimeDbContextFactory<ExternalAuthContext>
    {
        public ExternalAuthContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExternalAuthContext>();
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=ExternalAuthDevDB;Integrated Security=True;Pooling=False;MultipleActiveResultSets=True;");

            return new ExternalAuthContext(optionsBuilder.Options);
        }
    }
}
