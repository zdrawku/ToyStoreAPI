using Newtonsoft.Json;
using ToyStoreAPI.Data;
using ToyStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ToyStoreAPI.Helpers
{
    public class DBSeeder
    {
        public static void Seed(ToyStoreContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();

            using var transaction = dbContext.Database.BeginTransaction();
            try
            {
                SeedCategories(dbContext);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error during seeding: {ex.Message}");
                throw;
            }
        }

        private static void SeedCategories(ToyStoreContext dbContext)
        {
            if (dbContext.Toys.Any())
            {
                return;
            }

            var toysData = File.ReadAllText("./Resources/toys.json");
            var parsedToys = JsonConvert.DeserializeObject<List<ToyModel>>(toysData);

            if (parsedToys?.Any() == true)
            {
                dbContext.Toys.AddRange(parsedToys);
                dbContext.SaveChanges();
            }
        }
    }
}
