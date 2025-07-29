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
        public async Task<ActionResult<ApiResponse<IEnumerable<RecipeDto>>>> GetRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(ApiResponse<IEnumerable<RecipeDto>>.SuccessResult(recipes, "Reçeteler başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<RecipeDto>>> GetRecipe(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
                return NotFound(ApiResponse<RecipeDto>.ErrorResult("Reçete bulunamadı"));

            return Ok(ApiResponse<RecipeDto>.SuccessResult(recipe, "Reçete başarıyla getirildi"));
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<RecipeDto>>>> GetRecipesByProduct(int productId)
        {
            var recipes = await _recipeService.GetRecipesByProductAsync(productId);
            return Ok(ApiResponse<IEnumerable<RecipeDto>>.SuccessResult(recipes, "Ürün reçeteleri getirildi"));
        }

        [HttpGet("ingredient/{ingredientId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<RecipeDto>>>> GetRecipesByIngredient(int ingredientId)
        {
            var recipes = await _recipeService.GetRecipesByIngredientAsync(ingredientId);
            return Ok(ApiResponse<IEnumerable<RecipeDto>>.SuccessResult(recipes, "Malzeme reçeteleri getirildi"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<RecipeDto>>> CreateRecipe(CreateRecipeDto createRecipeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<RecipeDto>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var createdRecipe = await _recipeService.CreateRecipeAsync(createRecipeDto);
            return CreatedAtAction(nameof(GetRecipe), new { id = createdRecipe.Id }, 
                ApiResponse<RecipeDto>.SuccessResult(createdRecipe, "Reçete başarıyla oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateRecipe(int id, UpdateRecipeDto updateRecipeDto)
        {
            if (id != updateRecipeDto.Id)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var result = await _recipeService.UpdateRecipeAsync(updateRecipeDto);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Reçete bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Reçete başarıyla güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteRecipe(int id)
        {
            var result = await _recipeService.DeleteRecipeAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Reçete bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Reçete başarıyla silindi"));
        }

        private System.Collections.Generic.List<string> GetModelStateErrors()
        {
            var errors = new System.Collections.Generic.List<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            return errors;
        }
    }
} 