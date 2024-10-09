using Newtonsoft.Json;
using ToyStoreAPI.Models;

public class ToyService
{
    private readonly List<ToyModel> _toys;

    public ToyService()
    {
        string jsonData = File.ReadAllText("Data/toys.json");
        _toys = JsonConvert.DeserializeObject<List<ToyModel>>(jsonData);
    }

    public IEnumerable<ToyModel> GetAllToys()
    {
        return _toys;
    }

    public IEnumerable<ToyModel> GetToysByCategory(int categoryId)
    {
        return _toys.Where(toy => toy.CategoryID == categoryId);
    }

    public IEnumerable<ToyModel> GetToysInPriceRange(decimal minPrice, decimal maxPrice)
    {
        return _toys.Where(toy => toy.Price >= minPrice && toy.Price <= maxPrice);
    }

    public ToyModel GetToyById(int id)
    {
        return _toys.FirstOrDefault(toy => toy.Id == id);
    }

    public IEnumerable<ToyModel> GetToysByName(string name)
    {
        return _toys.Where(toy => toy.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }
}
