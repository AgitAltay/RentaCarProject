using Rac.entity.Repository;
using Rac.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Rac.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(RentACarContext context) : base(context) { }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<int> GetCustomerIdByEmailAsync(string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            return customer?.Id ?? 0; 
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByCustomerIdAsync(int customerId)
        {
            return await _context.Reservations
                                 .Where(r => r.CustomerId == customerId)
                                 .ToListAsync();
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            return await _context.Customers.SingleOrDefaultAsync(c => c.Email == email);
        }
    }

}
