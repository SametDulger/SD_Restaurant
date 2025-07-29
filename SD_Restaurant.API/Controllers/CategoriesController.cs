using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Application.Services;

namespace SD_Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDto>>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(ApiResponse<IEnumerable<CategoryDto>>.SuccessResult(categories, "Kategoriler başarıyla getirildi"));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(ApiResponse<CategoryDto>.ErrorResult("Kategori bulunamadı"));
            }
            return Ok(ApiResponse<CategoryDto>.SuccessResult(category, "Kategori başarıyla getirildi"));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<CategoryDto>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var createdCategory = await _categoryService.CreateCategoryAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, 
                ApiResponse<CategoryDto>.SuccessResult(createdCategory, "Kategori başarıyla oluşturuldu"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (id != updateCategoryDto.Id)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz veri", GetModelStateErrors()));
            }

            var result = await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Kategori bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Kategori başarıyla güncellendi"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResult("Kategori bulunamadı"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new object(), "Kategori başarıyla silindi"));
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