using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Services
{
    public interface IPaymentService
    {
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByOrderAsync(int orderId);
        Task<IEnumerable<Payment>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Payment>> GetPaymentsByMethodAsync(string paymentMethod);
        Task<decimal> GetTotalPaymentsByDateAsync(DateTime date);
        Task<bool> ProcessPaymentAsync(Payment payment);
    }
} 