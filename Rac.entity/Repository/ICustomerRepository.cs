using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rac.entity.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<int> GetCustomerIdByEmailAsync(string email);
        Task<IEnumerable<Reservation>> GetReservationsByCustomerIdAsync(int customerId);
        Task<Customer> GetByEmailAsync(string email);



    }
}
