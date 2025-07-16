using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipesByProduct(int productId)
        {
            var recipes = await _recipeService.GetRecipesByProductAsync(productId);
            return Ok(recipes);
        }

        [HttpGet("ingredient/{ingredientId}")]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipesByIngredient(int ingredientId)
        {
            var recipes = await _recipeService.GetRecipesByIngredientAsync(ingredientId);
            return Ok(recipes);
        }

        [HttpGet("product/{productId}/ingredient/{ingredientId}")]
        public async Task<ActionResult<RecipeDto>> GetRecipeByProductAndIngredient(int productId, int ingredientId)
        {
            var recipe = await _recipeService.GetRecipeByProductAndIngredientAsync(productId, ingredientId);
            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDto>> CreateRecipe(CreateRecipeDto createRecipeDto)
        {
            var recipe = await _recipeService.CreateRecipeAsync(createRecipeDto);
            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, UpdateRecipeDto updateRecipeDto)
        {
            if (id != updateRecipeDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _recipeService.UpdateRecipeAsync(updateRecipeDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            await _recipeService.DeleteRecipeAsync(id);
            return NoContent();
        }
    }
} 