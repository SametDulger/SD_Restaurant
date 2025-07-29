using AutoMapper;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Application.Services;
using SD_Restaurant.Core.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SD_Restaurant.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            var payment = _mapper.Map<Payment>(createPaymentDto);
            var createdPayment = await _paymentRepository.AddAsync(payment);
            return _mapper.Map<PaymentDto>(createdPayment);
        }

        public async Task<bool> UpdatePaymentAsync(UpdatePaymentDto updatePaymentDto)
        {
            var existingPayment = await _paymentRepository.GetByIdAsync(updatePaymentDto.Id);
            if (existingPayment == null)
                return false;

            _mapper.Map(updatePaymentDto, existingPayment);
            await _paymentRepository.UpdateAsync(existingPayment);
            return true;
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                return false;

            await _paymentRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByOrderAsync(int orderId)
        {
            var payments = await _paymentRepository.GetPaymentsByOrderAsync(orderId);
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByMethodAsync(PaymentMethod paymentMethod)
        {
            var payments = await _paymentRepository.FindAsync(p => p.PaymentMethod == paymentMethod);
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByCustomerAsync(int customerId)
        {
            var payments = await _paymentRepository.GetPaymentsByCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<IEnumerable<PaymentDto>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var payments = await _paymentRepository.GetPaymentsByDateRangeAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<decimal> GetTotalPaymentsByDateAsync(DateTime date)
        {
            return await _paymentRepository.GetTotalPaymentsByDateAsync(date);
        }
    }
} 