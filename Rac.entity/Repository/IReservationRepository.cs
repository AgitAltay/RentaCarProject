using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rac.entity.Repository
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetActiveReservationsAsync();
        Task<IEnumerable<Reservation>> GetReservationsByCustomerIdAsync(int customerId);


    }
}
