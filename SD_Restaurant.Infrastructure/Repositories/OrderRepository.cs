using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Infrastructure.Data;

namespace SD_Restaurant.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(o => o.Table)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate && o.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByTableAsync(int tableId)
        {
            return await _dbSet
                .Include(o => o.Table)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Where(o => o.TableId == tableId && o.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            return await _dbSet
                .Include(o => o.Table)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Where(o => o.Status == status && o.IsActive)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithItemsAsync(int orderId)
        {
            return await _dbSet
                .Include(o => o.Table)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.IsActive);
        }

        public async Task<IEnumerable<Order>> GetOrdersByEmployeeAsync(int employeeId)
        {
            return await _dbSet
                .Include(o => o.Table)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Where(o => o.EmployeeId == employeeId && o.IsActive)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate && o.IsActive)
                .SumAsync(o => o.FinalAmount);
        }

        public async Task<string> GenerateOrderNumberAsync()
        {
            var today = DateTime.Today;
            var orderCount = await _dbSet
                .CountAsync(o => o.OrderDate.Date == today);

            return $"ORD-{today:yyyyMMdd}-{orderCount + 1:D4}";
        }
    }
} 