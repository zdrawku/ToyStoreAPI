using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Services
{
    public class ToyService
    {
        private List<Toy> _toys;

        // Constructor to load the data from the JSON file
        public ToyService()
        {
            // Assuming the toys.json file is in the Data folder in the project
            var jsonData = File.ReadAllText("Data/toys.json");
            _toys = JsonConvert.DeserializeObject<List<Toy>>(jsonData);
        }

        public List<Toy> GetAllToys()
        {
            return _toys;
        }

        public List<Toy> GetToysByCategory(Category category)
        {
            return _toys.FindAll(t => t.Category == category);
        }

        public List<Toy> GetToysByPriceRange(decimal from, decimal to)
        {
            return _toys.FindAll(t => t.Price >= from && t.Price <= to);
        }

        public List<Toy> GetToysByName(string name)
        {
            return _toys.FindAll(t => t.Name.ToLower().Contains(name.ToLower()));
        }

        public Toy GetToyById(int id)
        {
            return _toys.Find(t => t.Id == id);
        }
    }
}
