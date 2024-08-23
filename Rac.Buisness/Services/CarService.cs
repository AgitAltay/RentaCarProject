using AutoMapper;
using Microsoft.EntityFrameworkCore;
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


namespace Rac.Buisness.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly RentACarContext _context;

        public CarService(ICarRepository carRepository, IMapper mapper, RentACarContext context)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _context = context;

        }

        public async Task AddCarAsync(CarDto carDto)
        {
            var car = _mapper.Map<Car>(carDto);
            await _carRepository.AddAsync(car);
        }

        public async Task UpdateCarAsync(CarDto carDto)
        {
            var car = _mapper.Map<Car>(carDto);
            await _carRepository.UpdateAsync(car);
        }

        public async Task DeleteCarAsync(int id)
        {
            await _carRepository.DeleteAsync(id);
        }

        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            return _mapper.Map<CarDto>(car);
        }

        public async Task<IEnumerable<CarDto>> GetAllCarsAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CarDto>>(cars);
        }
        public async Task<IEnumerable<CarDto>> SearchCarsAsync(string query)
        {
            return await _context.Cars
                .Where(c => c.Description.Contains(query) || c.Model.Contains(query))
                .Select(c => new CarDto
                {
                    Id = c.Id,
                    Description = c.Description,
                    Model = c.Model,
                    Year = c.Year,
                    DailyPrice = c.DailyPrice,
                    Image = c.Image
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<CarDto>> GetAvailableCarsAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            var availableCars = cars.Where(c => !c.IsRented);
            return _mapper.Map<IEnumerable<CarDto>>(availableCars);
        }

        public async Task<IEnumerable<CarDto>> GetRentedCarsAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            var rentedCars = cars.Where(c => c.IsRented);
            return _mapper.Map<IEnumerable<CarDto>>(rentedCars);
        }

        public async Task RentCarAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car != null && !car.IsRented)
            {
                car.IsRented = true;
                await _carRepository.UpdateAsync(car);
            }
        }

        public async Task ReturnCarAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car != null && car.IsRented)
            {
                car.IsRented = false;
                await _carRepository.UpdateAsync(car);
            }
        }
        public async Task<IEnumerable<Car>> GetCarsByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Cars
                                 .Where(car => ids.Contains(car.Id))
                                 .ToListAsync();
        }

        public async Task<bool> CarExistsAsync(int carId)
        {
            var car = await _carRepository.GetByIdAsync(carId);
            return car != null;
        }

    }
}
