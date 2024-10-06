using Microsoft.AspNetCore.Mvc;
using ToyStoreAPI.Models;
using ToyStoreAPI.Services;

namespace ToyStoreAPI.Controllers
{
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
        [HttpGet]
        public ActionResult<List<Toy>> GetAllToys()
        {
            return Ok(_toyService.GetAllToys());
        }

        // Get toys by category
        [HttpGet("category/{category}")]
        public ActionResult<List<Toy>> GetToysByCategory(Category category)
        {
            var filteredToys = _toyService.GetToysByCategory(category);
            return Ok(filteredToys);
        }

        // Get toys within a price range
        [HttpGet("price")]
        public ActionResult<List<Toy>> GetToysByPriceRange([FromQuery] decimal from, [FromQuery] decimal to)
        {
            var filteredToys = _toyService.GetToysByPriceRange(from, to);
            return Ok(filteredToys);
        }

        // Get toys by name (case insensitive)
        [HttpGet("name/{name}")]
        public ActionResult<List<Toy>> GetToysByName(string name)
        {
            var filteredToys = _toyService.GetToysByName(name);
            return Ok(filteredToys);
        }

        // Get toy by ID
        [HttpGet("{id}")]
        public ActionResult<Toy> GetToyById(int id)
        {
            var toy = _toyService.GetToyById(id);
            if (toy == null)
            {
                return NotFound(new { message = "Toy not found" });
            }
            return Ok(toy);
        }
    }
}
