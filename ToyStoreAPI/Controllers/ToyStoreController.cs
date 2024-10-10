using Microsoft.AspNetCore.Mvc;
using ToyStoreAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class ToysController : ControllerBase
{
    private readonly ToyService _toyService;

    public ToysController(ToyService toyService)
    {
        _toyService = toyService;
    }

    // Get all toys
    [HttpGet("allToys")]
    public ActionResult<IEnumerable<ToyModel>> GetAllToys()
    {
        var toys = _toyService.GetAllToys();
        return Ok(toys);
    }

    // Get all categories
    [HttpGet("categories")]
    public ActionResult<IEnumerable<CategoryModel>> GetAllCategories()
    {
        var categories = new List<CategoryModel>
        {
            new CategoryModel { Id = 1, Name = "Infant", Description= "0-12 months" },
            new CategoryModel { Id = 2, Name = "Toddler", Description= "1-3 years" },
            new CategoryModel { Id = 3, Name = "Preschool", Description= "3-5 years" },
            new CategoryModel { Id = 4, Name = "Older Kids", Description= "5+ years" }
        };

        return Ok(categories);
    }

    // Get toys by category (categoryId is optional)
    [HttpGet("toysByCategoryID")]
    public ActionResult<IEnumerable<ToyModel>> GetToysByCategory([FromQuery] int? categoryId)
    {
        IEnumerable<ToyModel> toys;

        if (categoryId.HasValue && categoryId.Value != 0)
        {
            toys = _toyService.GetToysByCategory(categoryId.Value);
        }
        else
        {
            // If no categoryId is provided, return all toys
            toys = _toyService.GetAllToys();
        }

        if (toys == null || !toys.Any())
        {
            return NotFound("No toys found.");
        }

        return Ok(toys);
    }


    // Get toys in a specific price range
    [HttpGet("price")]
    public ActionResult<IEnumerable<ToyModel>> GetToysInPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
    {
        var toys = _toyService.GetToysInPriceRange(minPrice, maxPrice);
        if (toys == null || !toys.Any())
        {
            return NotFound($"No toys found in the price range from {minPrice} to {maxPrice}.");
        }
        return Ok(toys);
    }

    // Get toy by ID
    [HttpGet("{id}")]
    public ActionResult<ToyModel> GetToyById(int id)
    {
        var toy = _toyService.GetToyById(id);
        if (toy == null)
        {
            return NotFound($"No toy found with ID {id}.");
        }
        return Ok(toy);
    }

    // Get toys by name
    [HttpGet("getToyByName")]
    public ActionResult<IEnumerable<ToyModel>> GetToysByName([FromQuery] string name)
    {
        var toys = _toyService.GetToysByName(name);
        if (toys == null || !toys.Any())
        {
            return NotFound($"No toys found with the name containing '{name}'.");
        }
        return Ok(toys);
    }
}
