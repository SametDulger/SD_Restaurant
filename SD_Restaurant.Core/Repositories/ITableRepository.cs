using SD_Restaurant.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Core.Repositories
{
    public interface ITableRepository : IGenericRepository<Table>
    {
        Task<IEnumerable<Table>> GetAvailableTablesAsync();
        Task<IEnumerable<Table>> GetOccupiedTablesAsync();
        Task<Table?> GetTableWithOrdersAsync(int tableId);
        Task<IEnumerable<Table>> GetTablesWithOrdersAsync();
    }
} 