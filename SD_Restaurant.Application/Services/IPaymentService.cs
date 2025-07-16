using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SD_Restaurant.Application.DTOs;

namespace SD_Restaurant.Application.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
        Task<PaymentDto> GetPaymentByIdAsync(int id);
        Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto);
        Task<bool> UpdatePaymentAsync(UpdatePaymentDto updatePaymentDto);
        Task<bool> DeletePaymentAsync(int id);
        Task<IEnumerable<PaymentDto>> GetPaymentsByOrderAsync(int orderId);
        Task<IEnumerable<PaymentDto>> GetPaymentsByMethodAsync(string paymentMethod);
        Task<IEnumerable<PaymentDto>> GetPaymentsByCustomerAsync(int customerId);
        Task<IEnumerable<PaymentDto>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalPaymentsByDateAsync(DateTime date);
    }
} 