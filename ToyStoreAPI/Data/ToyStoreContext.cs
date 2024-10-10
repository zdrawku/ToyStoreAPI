using Microsoft.EntityFrameworkCore;
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
                .ValueGeneratedOnAdd();
        }
    }
}
