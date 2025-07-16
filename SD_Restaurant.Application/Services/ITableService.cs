using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Services
{
    public interface ITableService
    {
        Task<IEnumerable<TableDto>> GetAllTablesAsync();
        Task<TableDto> GetTableByIdAsync(int id);
        Task<TableDto> CreateTableAsync(CreateTableDto createTableDto);
        Task<bool> UpdateTableAsync(UpdateTableDto updateTableDto);
        Task<bool> DeleteTableAsync(int id);
        Task<IEnumerable<TableDto>> GetTablesByStatusAsync(string status);
        Task<IEnumerable<TableDto>> GetTablesByLocationAsync(string location);
        Task<bool> UpdateTableStatusAsync(int id, string status);
        Task<IEnumerable<TableDto>> GetAvailableTablesAsync();
        Task<IEnumerable<TableDto>> GetOccupiedTablesAsync();
    }
} 