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
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly RentACarContext _context;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper, RentACarContext context)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task AddReservationAsync(ReservationDto reservationDto)
        {
            var carId = reservationDto.CarId;
            var carExists = await _context.Cars.AnyAsync(c => c.Id == carId);

            if (!carExists)
            {
                throw new Exception($"Car ID {carId} does not exist in the database.");
            }

            var reservation = new Reservation
            {
                CarId = carId,
                CustomerId = reservationDto.CustomerId,
                StartDate = reservationDto.StartDate,
                EndDate = reservationDto.EndDate,
                IsActive = true
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(ReservationDto reservationDto)
        {
            // ReservationDto'yu Reservation türüne dönüştür
            var reservation = _mapper.Map<Reservation>(reservationDto);
            // Rezervasyonu güncelle
            await _reservationRepository.UpdateAsync(reservation);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await _reservationRepository.DeleteAsync(id);
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }
        public async Task<IEnumerable<ReservationDto>> GetRentedCarsAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return reservations.Where(r => r.IsActive).Select(r => new ReservationDto
            {
                CarId = r.CarId,
                CustomerId = r.CustomerId,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                IsActive = r.IsActive
            });
        }


    }
}