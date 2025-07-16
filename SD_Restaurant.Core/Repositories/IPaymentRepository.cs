using SD_Restaurant.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Restaurant.Core.Repositories
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByOrderAsync(int orderId);
        Task<IEnumerable<Payment>> GetPaymentsByCustomerAsync(int customerId);
        Task<IEnumerable<Payment>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Payment>> GetPaymentsByMethodAsync(string paymentMethod);
        Task<decimal> GetTotalPaymentsByDateAsync(DateTime date);
    }
} 