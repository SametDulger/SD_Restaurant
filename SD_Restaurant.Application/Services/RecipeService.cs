using AutoMapper;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecipeDto>> GetAllRecipesAsync()
        {
            var recipes = await _recipeRepository.GetAllRecipesWithDetailsAsync();
            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }

        public async Task<RecipeDto> GetRecipeByIdAsync(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            return _mapper.Map<RecipeDto>(recipe);
        }

        public async Task<RecipeDto> CreateRecipeAsync(CreateRecipeDto createRecipeDto)
        {
            var recipe = _mapper.Map<Recipe>(createRecipeDto);
            var createdRecipe = await _recipeRepository.AddAsync(recipe);
            return _mapper.Map<RecipeDto>(createdRecipe);
        }

        public async Task<bool> UpdateRecipeAsync(UpdateRecipeDto updateRecipeDto)
        {
            var existingRecipe = await _recipeRepository.GetByIdAsync(updateRecipeDto.Id);
            if (existingRecipe == null)
                return false;

            _mapper.Map(updateRecipeDto, existingRecipe);
            await _recipeRepository.UpdateAsync(existingRecipe);
            return true;
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            if (recipe == null)
                return false;

            await _recipeRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<RecipeDto>> GetRecipesByProductAsync(int productId)
        {
            var recipes = await _recipeRepository.GetRecipesByProductAsync(productId);
            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }

        public async Task<IEnumerable<RecipeDto>> GetRecipesByIngredientAsync(int ingredientId)
        {
            var recipes = await _recipeRepository.GetRecipesByIngredientAsync(ingredientId);
            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }

        public async Task<RecipeDto> GetRecipeByProductAndIngredientAsync(int productId, int ingredientId)
        {
            var recipe = await _recipeRepository.GetRecipeByProductAndIngredientAsync(productId, ingredientId);
            return _mapper.Map<RecipeDto>(recipe);
        }
    }
} 