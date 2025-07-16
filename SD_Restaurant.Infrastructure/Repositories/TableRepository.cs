using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Infrastructure.Repositories
{
    public class TableRepository : GenericRepository<Table>, ITableRepository
    {
        public TableRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync()
        {
            return await _context.Tables
                .Where(t => t.IsAvailable && t.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Table>> GetOccupiedTablesAsync()
        {
            return await _context.Tables
                .Where(t => !t.IsAvailable && t.IsActive)
                .ToListAsync();
        }

        public async Task<Table?> GetTableWithOrdersAsync(int tableId)
        {
            return await _context.Tables
                .Include(t => t.Orders)
                .FirstOrDefaultAsync(t => t.Id == tableId);
        }

        public async Task<IEnumerable<Table>> GetTablesWithOrdersAsync()
        {
            return await _context.Tables
                .Include(t => t.Orders)
                .ToListAsync();
        }
    }
} 