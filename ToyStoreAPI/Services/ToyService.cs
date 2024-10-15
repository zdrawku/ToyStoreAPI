using Microsoft.EntityFrameworkCore;
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

    public IEnumerable<ToyModel> GetToysByPriceRange(decimal? minPrice, decimal? maxPrice)
    {
        var toys = _dbContext.Toys.AsQueryable();

        if (minPrice.HasValue)
        {
            toys = toys.Where(t => t.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            toys = toys.Where(t => t.Price <= maxPrice.Value);
        }

        return toys.ToList();
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

    public ToyModel Update(ToyModel toy)
    {
        var toyExists = _dbContext.Toys.Any(x => x.Id == toy.Id);
        if (!toyExists)
        {
            return null;
        }

        _dbContext.Toys.Update(toy);
        _dbContext.SaveChanges();
        return toy;
    }
    
    public ToyModel Create(ToyModel toy)
    {
        toy.Id = 0;
        _dbContext.Toys.Add(toy);
        _dbContext.SaveChanges();
        return toy;
    }

}
