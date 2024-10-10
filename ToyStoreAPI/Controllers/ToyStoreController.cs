using Microsoft.AspNetCore.Mvc;
using ToyStoreAPI.Models;

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

        /// <summary>
        /// Retrieves all available toys in the store.
        /// </summary>
        /// <returns>List of all toys with details like name, price, and category.</returns>
        /// <response code="200">Returns the list of toys.</response>
        [HttpGet("allToys")]
        public ActionResult<IEnumerable<ToyModel>> GetAllToys()
        {
            var toys = _toyService.GetAllToys();
            return Ok(toys);
        }

        /// <summary>
        /// Retrieves all toy categories.
        /// </summary>
        /// <returns>List of all categories like Infant, Toddler, Preschool, and Older Kids with descriptions.</returns>
        /// <response code="200">Returns the list of categories.</response>
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

        /// <summary>
        /// Retrieves toys filtered by the specified category ID.
        /// </summary>
        /// <param name="categoryId">Optional category ID to filter toys. If not provided, returns all toys.</param>
        /// <returns>List of toys in the specified category or all toys if no category is specified.</returns>
        /// <response code="200">Returns the list of toys in the category.</response>
        /// <response code="404">No toys found for the specified category.</response>
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

        /// <summary>
        /// Retrieves toys within the specified price range.
        /// </summary>
        /// <param name="minPrice">Minimum price for the filter.</param>
        /// <param name="maxPrice">Maximum price for the filter.</param>
        /// <returns>List of toys within the specified price range.</returns>
        /// <response code="200">Returns the list of toys within the price range.</response>
        /// <response code="404">No toys found in the specified price range.</response>
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

        /// <summary>
        /// Retrieves a specific toy by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the toy.</param>
        /// <returns>The toy with the specified ID.</returns>
        /// <response code="200">Returns the toy with the specified ID.</response>
        /// <response code="404">No toy found with the specified ID.</response>
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

        /// <summary>
        /// Retrieves toys that contain the specified name (or part of it).
        /// </summary>
        /// <param name="name">The name or part of the toy's name to search for.</param>
        /// <returns>List of toys matching the name criteria.</returns>
        /// <response code="200">Returns the list of toys matching the name.</response>
        /// <response code="404">No toys found with the specified name.</response>
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
}
