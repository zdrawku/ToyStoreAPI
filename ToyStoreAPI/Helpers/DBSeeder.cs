using Newtonsoft.Json;
using ToyStoreAPI.Data;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Helpers
{
    public class DBSeeder
    {
        public static void Seed(ToyStoreContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();

            var transaction = dbContext.Database.BeginTransaction();

            try
            {
                SeedCategories(dbContext);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        private static void SeedCategories(ToyStoreContext dbContext)
        {
            if (!dbContext.Toys.Any())
            {
                var toysData = File.ReadAllText("./Resources/toys.json");
                var parsedToys = JsonConvert.DeserializeObject<List<ToyModel>>(toysData);

                if (parsedToys != null)
                {
                    dbContext.Toys.AddRange(parsedToys);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
