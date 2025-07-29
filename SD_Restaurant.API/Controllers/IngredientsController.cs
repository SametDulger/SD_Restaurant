using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;

namespace SD_Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<IngredientDto>>>> GetIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            return Ok(ApiResponse<IEnumerable<IngredientDto>>.SuccessResult(ingredients, "Malzemeler başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<IngredientDto>>> GetIngredient(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound(ApiResponse<IngredientDto>.ErrorResult("Malzeme bulunamadı"));
            }
            return Ok(ApiResponse<IngredientDto>.SuccessResult(ingredient, "Malzeme başarıyla getirildi"));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<IngredientDto>>>> SearchIngredients([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return BadRequest(ApiResponse<IEnumerable<IngredientDto>>.ErrorResult("Arama terimi gerekli"));
            }

            var ingredients = await _ingredientService.GetIngredientsByNameAsync(term);
            return Ok(ApiResponse<IEnumerable<IngredientDto>>.SuccessResult(ingredients, "Arama sonuçları"));
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<ApiResponse<IEnumerable<IngredientDto>>>> GetLowStockIngredients()
        {
            var ingredients = await _ingredientService.GetLowStockIngredientsAsync();
            return Ok(ApiResponse<IEnumerable<IngredientDto>>.SuccessResult(ingredients, "Düşük stok malzemeleri"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<IngredientDto>>> CreateIngredient(CreateIngredientDto createIngredientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<IngredientDto>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var createdIngredient = await _ingredientService.CreateIngredientAsync(createIngredientDto);
            return CreatedAtAction(nameof(GetIngredient), new { id = createdIngredient.Id }, 
                ApiResponse<IngredientDto>.SuccessResult(createdIngredient, "Malzeme başarıyla oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateIngredient(int id, UpdateIngredientDto updateIngredientDto)
        {
            if (id != updateIngredientDto.Id)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var result = await _ingredientService.UpdateIngredientAsync(updateIngredientDto);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Malzeme bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Malzeme başarıyla güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteIngredient(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Malzeme bulunamadı"));
            }

            await _ingredientService.DeleteIngredientAsync(id);
            return Ok(ApiResponse<object>.SuccessResult(new object(), "Malzeme başarıyla silindi"));
        }

        private List<string> GetModelStateErrors()
        {
            var errors = new List<string>();
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