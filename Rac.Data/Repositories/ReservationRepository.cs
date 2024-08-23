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
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(RentACarContext context) : base(context)
        {
        }

       
        public async Task<IEnumerable<Reservation>> GetActiveReservationsAsync()
        {
            return await _context.Reservations.Where(r => r.IsActive).ToListAsync();
        }
        public async Task<IEnumerable<Reservation>> GetReservationsByCustomerIdAsync(int customerId)
        {
            return await _dbSet.Where(r => r.CustomerId == customerId).ToListAsync();
        }
    }
}
