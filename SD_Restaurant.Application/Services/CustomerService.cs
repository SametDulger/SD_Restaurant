using AutoMapper;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SD_Restaurant.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return customer != null ? _mapper.Map<CustomerDto>(customer) : null;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);
            var createdCustomer = await _customerRepository.AddAsync(customer);
            return _mapper.Map<CustomerDto>(createdCustomer);
        }

        public async Task<bool> UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(updateCustomerDto.Id);
            if (existingCustomer == null)
                return false;

            _mapper.Map(updateCustomerDto, existingCustomer);
            await _customerRepository.UpdateAsync(existingCustomer);
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return false;

            await _customerRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<CustomerDto>> SearchCustomersAsync(string term)
        {
            var customers = await _customerRepository.FindAsync(c => 
                c.FirstName.Contains(term) || 
                c.LastName.Contains(term) || 
                (c.Email != null && c.Email.Contains(term)) || 
                (c.Phone != null && c.Phone.Contains(term)));
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto?> GetCustomerByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(email);
            return customer != null ? _mapper.Map<CustomerDto>(customer) : null;
        }

        public async Task<CustomerDto?> GetCustomerByPhoneAsync(string phone)
        {
            var customer = await _customerRepository.GetCustomerByPhoneAsync(phone);
            return customer != null ? _mapper.Map<CustomerDto>(customer) : null;
        }

        public async Task<IEnumerable<CustomerDto>> GetActiveCustomersAsync()
        {
            var customers = await _customerRepository.GetActiveCustomersAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }
    }
} 