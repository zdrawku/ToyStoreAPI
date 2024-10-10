using Newtonsoft.Json;
using ToyStoreAPI.Data;
using ToyStoreAPI.Models;

public class ToyService
{
    private readonly ToyStoreContext _dbContext;

    public ToyService(ToyStoreContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<ToyModel> GetAllToys()
    {
        return _dbContext.Toys.ToList();
    }

    public IEnumerable<ToyModel> GetToysByCategory(int categoryId)
    {
        return _dbContext.Toys.Where(toy => toy.CategoryID == categoryId);
    }

    public IEnumerable<ToyModel> GetToysInPriceRange(decimal minPrice, decimal maxPrice)
    {
        return _dbContext.Toys.Where(toy => toy.Price >= minPrice && toy.Price <= maxPrice);
    }

    public ToyModel GetToyById(int id)
    {
        return _dbContext.Toys.FirstOrDefault(toy => toy.Id == id);
    }

    public IEnumerable<ToyModel> GetToysByName(string name)
    {
        return _dbContext.Toys.Where(toy => toy.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public ToyModel DeleteById(int id)
    {
        var toy = GetToyById(id);

        if (toy != null)
        {
            _dbContext.Remove(toy);
            _dbContext.SaveChanges();
        }

        return toy;
    }

    public ToyModel Upsert(ToyModel toy, int id = 0)
    {
        if (id == 0)
        {
            _dbContext.Toys.Add(toy);
        }
        else
        {
            var toyExists = _dbContext.Toys.Any(x => x.Id == id);
            if (!toyExists)
            {
                return null;
            }

            toy.Id = id;
            _dbContext.Toys.Update(toy);
        }
        _dbContext.SaveChanges();
        return toy;
    }
}
