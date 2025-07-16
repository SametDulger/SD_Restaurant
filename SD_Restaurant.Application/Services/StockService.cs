using AutoMapper;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockDto>> GetAllStocksAsync()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StockDto>>(stocks);
        }

        public async Task<StockDto> GetStockByIdAsync(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            return _mapper.Map<StockDto>(stock);
        }

        public async Task<StockDto> CreateStockAsync(CreateStockDto createStockDto)
        {
            var stock = _mapper.Map<Stock>(createStockDto);
            stock.CreatedDate = DateTime.UtcNow;
            var createdStock = await _stockRepository.AddAsync(stock);
            return _mapper.Map<StockDto>(createdStock);
        }

        public async Task<bool> UpdateStockAsync(UpdateStockDto updateStockDto)
        {
            var existingStock = await _stockRepository.GetByIdAsync(updateStockDto.Id);
            if (existingStock == null)
                return false;

            _mapper.Map(updateStockDto, existingStock);
            existingStock.UpdatedDate = DateTime.UtcNow;
            await _stockRepository.UpdateAsync(existingStock);
            return true;
        }

        public async Task<bool> DeleteStockAsync(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
                return false;

            await _stockRepository.DeleteAsync(id);
            return true;
        }

        public async Task<StockDto> GetStockByProductAndLocationAsync(int productId, string location)
        {
            var stock = await _stockRepository.GetStockByProductAndLocationAsync(productId, location);
            return _mapper.Map<StockDto>(stock);
        }

        public async Task<IEnumerable<StockDto>> GetStocksByLocationAsync(string location)
        {
            var stocks = await _stockRepository.GetStocksByLocationAsync(location);
            return _mapper.Map<IEnumerable<StockDto>>(stocks);
        }

        public async Task<IEnumerable<StockDto>> GetLowStockItemsAsync()
        {
            var stocks = await _stockRepository.GetLowStockItemsAsync();
            return _mapper.Map<IEnumerable<StockDto>>(stocks);
        }

        public async Task UpdateStockQuantityAsync(int productId, string location, decimal quantity)
        {
            await _stockRepository.UpdateStockQuantityAsync(productId, location, quantity);
        }

        public async Task<bool> CheckStockAvailabilityAsync(int productId, string location, decimal requiredQuantity)
        {
            var stock = await _stockRepository.GetStockByProductAndLocationAsync(productId, location);
            return stock != null && stock.Quantity >= requiredQuantity;
        }

        public async Task<IEnumerable<StockDto>> GetStocksByProductAsync(int productId)
        {
            var stocks = await _stockRepository.GetStocksByProductAsync(productId);
            return _mapper.Map<IEnumerable<StockDto>>(stocks);
        }
    }
} 