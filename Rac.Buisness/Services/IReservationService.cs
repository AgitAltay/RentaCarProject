using Rac.entity;
using Rac.entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rac.Buisness.Services
{
    public interface IReservationService
    {
        Task AddReservationAsync(ReservationDto reservationDto);
        Task UpdateReservationAsync(ReservationDto reservationDto);
        Task DeleteReservationAsync(int id);
        Task<ReservationDto> GetReservationByIdAsync(int id);
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<IEnumerable<ReservationDto>> GetRentedCarsAsync();

    }






}

