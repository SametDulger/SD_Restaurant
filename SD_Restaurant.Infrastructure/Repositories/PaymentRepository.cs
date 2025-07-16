using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByOrderAsync(int orderId)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .Where(p => p.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByCustomerAsync(int customerId)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .ThenInclude(o => o.Customer)
                .Where(p => p.Order != null && p.Order.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByMethodAsync(string paymentMethod)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .Where(p => p.PaymentMethod == paymentMethod)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalPaymentsByDateAsync(DateTime date)
        {
            return await _context.Payments
                .Where(p => p.PaymentDate.Date == date.Date)
                .SumAsync(p => p.Amount);
        }
    }
} 