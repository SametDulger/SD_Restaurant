using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Services
{
    public interface ITableService
    {
        Task<Table> GetTableByIdAsync(int id);
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table> CreateTableAsync(Table table);
        Task UpdateTableAsync(Table table);
        Task DeleteTableAsync(int id);
        Task<IEnumerable<Table>> GetTablesByStatusAsync(string status);
        Task<IEnumerable<Table>> GetTablesByLocationAsync(string location);
        Task<Table> GetTableByNumberAsync(int tableNumber);
        Task<bool> IsTableAvailableAsync(int tableId);
    }
} 