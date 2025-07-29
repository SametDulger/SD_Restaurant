using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;

namespace SD_Restaurant.Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingredientRepository, IStockRepository stockRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync()
        {
            var ingredients = await _ingredientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
        }

        public async Task<IngredientDto> GetIngredientByIdAsync(int id)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task<IngredientDto> CreateIngredientAsync(CreateIngredientDto createIngredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(createIngredientDto);
            ingredient.CreatedDate = DateTime.UtcNow;
            var createdIngredient = await _ingredientRepository.AddAsync(ingredient);
            return _mapper.Map<IngredientDto>(createdIngredient);
        }

        public async Task<bool> UpdateIngredientAsync(UpdateIngredientDto updateIngredientDto)
        {
            var existingIngredient = await _ingredientRepository.GetByIdAsync(updateIngredientDto.Id);
            if (existingIngredient == null)
                return false;

            _mapper.Map(updateIngredientDto, existingIngredient);
            existingIngredient.UpdatedDate = DateTime.UtcNow;
            await _ingredientRepository.UpdateAsync(existingIngredient);
            return true;
        }

        public async Task<bool> DeleteIngredientAsync(int id)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            if (ingredient == null)
                return false;

            await _ingredientRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<IngredientDto>> GetIngredientsByNameAsync(string name)
        {
            var ingredients = await _ingredientRepository.GetIngredientsByNameAsync(name);
            return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
        }

        public async Task<IEnumerable<IngredientDto>> GetLowStockIngredientsAsync()
        {
            var ingredients = await _ingredientRepository.GetLowStockIngredientsAsync();
            return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
        }
    }
} 