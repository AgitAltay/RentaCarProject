using Rac.entity;
using Rac.entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rac.Buisness.Services
{
    public interface ICarService
    {
        Task AddCarAsync(CarDto carDto);
        Task UpdateCarAsync(CarDto carDto);
        Task DeleteCarAsync(int id);
        Task<CarDto> GetCarByIdAsync(int id);
        Task<IEnumerable<CarDto>> GetAllCarsAsync();
        Task<IEnumerable<CarDto>> SearchCarsAsync(string query);
        Task RentCarAsync(int id);
        Task ReturnCarAsync(int id);
        Task<IEnumerable<CarDto>> GetAvailableCarsAsync();
        Task<IEnumerable<CarDto>> GetRentedCarsAsync();
        Task<IEnumerable<Car>> GetCarsByIdsAsync(IEnumerable<int> ids);
        Task<bool> CarExistsAsync(int carId);


    }

}
