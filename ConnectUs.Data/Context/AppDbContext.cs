using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ConnectUs.Entity.Entities;
namespace ConnectUs.Data.Context
{
    public class AppDbContext : DbContext
    {
        protected AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Auth> Auth { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Phone> Phone { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<Address> Address { get; set; }

        public DbSet<OurServices> OurServices { get; set; }

        protected override void  OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
