using Newtonsoft.Json;
using ToyStoreAPI.Models;

public class ToyService
{
    private readonly List<Toy> _toys;

    public ToyService()
    {
        string jsonData = File.ReadAllText("Data/toys.json");
        _toys = JsonConvert.DeserializeObject<List<Toy>>(jsonData);
    }

    public IEnumerable<Toy> GetAllToys()
    {
        return _toys;
    }

    public IEnumerable<Toy> GetToysByCategory(int categoryId)
    {
        return _toys.Where(toy => toy.CategoryID == categoryId);
    }

    public IEnumerable<Toy> GetToysInPriceRange(decimal minPrice, decimal maxPrice)
    {
        return _toys.Where(toy => toy.Price >= minPrice && toy.Price <= maxPrice);
    }

    public Toy GetToyById(int id)
    {
        return _toys.FirstOrDefault(toy => toy.Id == id);
    }

    public IEnumerable<Toy> GetToysByName(string name)
    {
        return _toys.Where(toy => toy.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }
}
