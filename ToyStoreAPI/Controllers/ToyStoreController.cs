using Microsoft.AspNetCore.Mvc;
using ToyStoreAPI.Data;
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
            var categories =CategoriesDataHelper.GetCategories();

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

        /// <summary>
        /// Retrieves toys that contain the specified name (or part of it).
        /// </summary>
        /// <param name="id">The unique ID of the toy.</param>
        /// <returns>List of toys matching the name criteria.</returns>
        /// <response code="200">Returns the list of toys matching the name.</response>
        /// <response code="404">No toys found with the specified name.</response>
        [HttpDelete("deleteToyById")]
        public ActionResult<ToyModel> DeleteToyById(int id)
        {
            var toy = _toyService.DeleteById(id);
            if (toy == null)
            {
                return NotFound($"No toy found with ID {id}.");
            }
            return Ok(toy);
        }

        /// <summary>
        /// Updates an existing toy with the specified ID.
        /// </summary>
        /// <param name="id">The unique ID of the toy to update.</param>
        /// <param name="toy">The toy data to update with, including name, price, and category.</param>
        /// <returns>The updated toy details.</returns>
        /// <response code="200">Returns the updated toy details.</response>
        /// <response code="400">The provided category ID is invalid or does not exist.</response>
        /// <response code="404">No toy found with the specified ID.</response>
        [HttpPut("updateToy/{id}")]
        public ActionResult<ToyModel> UpdateToy([FromBody] ToyModel toy, int id)
        {
            if (!CategoriesDataHelper.CategoryExists(toy.CategoryID))
            {
                return BadRequest($"Invalid category ID {toy.CategoryID}. Please provide a valid category.");
            }

            var updatedToy = _toyService.Upsert(toy, id);
            if (updatedToy == null)
            {
                return NotFound($"No toy found with ID {id}.");
            }
            return Ok(updatedToy);
        }

        /// <summary>
        /// Creates a new toy in the store.
        /// </summary>
        /// <param name="toy">The toy data to create, including name, price, and category.</param>
        /// <returns>The created toy details.</returns>
        /// <response code="200">Returns the created toy details.</response>
        /// <response code="400">The provided category ID is invalid or does not exist.</response>
        [HttpPost("createToy")]
        public ActionResult<ToyModel> CreateToy([FromBody] ToyModel toy)
        {
            if (!CategoriesDataHelper.CategoryExists(toy.CategoryID))
            {
                return BadRequest($"Invalid category ID {toy.CategoryID}. Please provide a valid category.");
            }

            var insertedToy = _toyService.Upsert(toy);

            return Ok(insertedToy);
        }

    }
}
