using AutoMapper;
using Rac.Data;
using Rac.Data.Repositories;
using Rac.entity;
using Rac.entity.DTOs;
using Rac.entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Rac.Buisness.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;


        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IReservationRepository reservationRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _reservationRepository = reservationRepository;

        }

        public async Task AddCustomerAsync(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer != null)
            {
                await _customerRepository.DeleteAsync(id);
            }
            else
            {
                throw new Exception("Customer not found.");
            }
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> AuthenticateUserAsync(string email, string password)
        {
            var customers = await _customerRepository.GetAllAsync();
            var customer = customers.FirstOrDefault(c => c.Email == email && c.Password == password);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<int> GetCustomerIdByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            return customer?.Id ?? 0; 
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByCustomerIdAsync(int customerId)
        {
            var reservations = await _reservationRepository.GetReservationsByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<CustomerDto> GetCustomerByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            return customer != null ? new CustomerDto { Id = customer.Id, Email = customer.Email, Password = customer.Password, Role = customer.Role } : null;
        }
    }
}