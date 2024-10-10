using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Data
{
    public class ToyStoreContext : DbContext
    {
        public ToyStoreContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ToyModel> Toys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToyModel>()
               .Property(a => a.Id)
               .HasValueGenerator((_, __) => new IdentityColumnGenerator(101));
        }
    }
}