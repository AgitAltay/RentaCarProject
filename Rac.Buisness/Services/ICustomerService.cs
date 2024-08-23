using Rac.entity;
using Rac.entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rac.Buisness.Services
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(CustomerDto customerDto);
        Task UpdateCustomerAsync(CustomerDto customerDto);
        Task DeleteCustomerAsync(int id);
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> AuthenticateUserAsync(string email, string password);
        Task<int> GetCustomerIdByEmailAsync(string email); 
        Task<IEnumerable<ReservationDto>> GetReservationsByCustomerIdAsync(int customerId);
        Task<CustomerDto> GetCustomerByEmailAsync(string email);


    }



}
