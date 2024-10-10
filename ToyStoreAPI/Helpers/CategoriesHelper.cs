using ToyStoreAPI.Models;

namespace ToyStoreAPI.Helpers
{
    public class CategoriesHelper
    {
        public static List<CategoryModel> GetCategories()
        {
            return new List<CategoryModel>
            {
                new CategoryModel { Id = 1, Name = "Infant", Description= "0-12 months" },
                new CategoryModel { Id = 2, Name = "Toddler", Description= "1-3 years" },
                new CategoryModel { Id = 3, Name = "Preschool", Description= "3-5 years" },
                new CategoryModel { Id = 4, Name = "Older Kids", Description= "5+ years" }
            };
        }

        public static bool CategoryExists(int categoryId)
        {
            return GetCategories().Any(x => x.Id == categoryId);
        }
    }
}
