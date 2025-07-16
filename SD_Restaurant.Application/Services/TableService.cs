using AutoMapper;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SD_Restaurant.Application.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public TableService(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TableDto>> GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TableDto>>(tables);
        }

        public async Task<TableDto> GetTableByIdAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            return _mapper.Map<TableDto>(table);
        }

        public async Task<TableDto> CreateTableAsync(CreateTableDto createTableDto)
        {
            var table = _mapper.Map<Table>(createTableDto);
            var createdTable = await _tableRepository.AddAsync(table);
            return _mapper.Map<TableDto>(createdTable);
        }

        public async Task<bool> UpdateTableAsync(UpdateTableDto updateTableDto)
        {
            var existingTable = await _tableRepository.GetByIdAsync(updateTableDto.Id);
            if (existingTable == null)
                return false;

            _mapper.Map(updateTableDto, existingTable);
            await _tableRepository.UpdateAsync(existingTable);
            return true;
        }

        public async Task<bool> DeleteTableAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
                return false;

            await _tableRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<TableDto>> GetTablesByStatusAsync(string status)
        {
            var tables = await _tableRepository.FindAsync(t => t.Status == status);
            return _mapper.Map<IEnumerable<TableDto>>(tables);
        }

        public async Task<IEnumerable<TableDto>> GetTablesByLocationAsync(string location)
        {
            var tables = await _tableRepository.FindAsync(t => t.Location == location);
            return _mapper.Map<IEnumerable<TableDto>>(tables);
        }

        public async Task<bool> UpdateTableStatusAsync(int id, string status)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
                return false;

            table.Status = status;
            await _tableRepository.UpdateAsync(table);
            return true;
        }

        public async Task<IEnumerable<TableDto>> GetAvailableTablesAsync()
        {
            var tables = await _tableRepository.GetAvailableTablesAsync();
            return _mapper.Map<IEnumerable<TableDto>>(tables);
        }

        public async Task<IEnumerable<TableDto>> GetOccupiedTablesAsync()
        {
            var tables = await _tableRepository.GetOccupiedTablesAsync();
            return _mapper.Map<IEnumerable<TableDto>>(tables);
        }
    }
} 