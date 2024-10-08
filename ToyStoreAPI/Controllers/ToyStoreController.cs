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

    [HttpGet("allToys")]
    public ActionResult<IEnumerable<Toy>> GetAllToys()
    {
        var toys = _toyService.GetAllToys();
        return Ok(toys);
    }

    [HttpGet("category/{categoryId}")]
    public IActionResult GetToysByCategory(int categoryId)
    {
        var toys = _toyService.GetToysByCategory(categoryId);
        return Ok(toys);
    }

    [HttpGet("price")]
    public IActionResult GetToysInPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
    {
        var toys = _toyService.GetToysInPriceRange(minPrice, maxPrice);
        return Ok(toys);
    }

    [HttpGet("{id}")]
    public IActionResult GetToyById(int id)
    {
        var toy = _toyService.GetToyById(id);
        if (toy == null)
        {
            return NotFound();
        }
        return Ok(toy);
    }

    [HttpGet("getToyByName")]
    public IActionResult GetToysByName([FromQuery] string name)
    {
        var toys = _toyService.GetToysByName(name);
        return Ok(toys);
    }
}
