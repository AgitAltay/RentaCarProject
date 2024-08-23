using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rac.Buisness.Services;
using Rac.entity;
using Rac.entity.DTOs;
using Rac.Web.Models;

namespace Rac.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICarService _carService;
        private readonly IReservationService _reservationService;

        public AdminController(ICarService carService, IReservationService reservationService)
        {
            _carService = carService;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCarsAsync();
            return View(cars);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarDto carDto)
        {
            if (ModelState.IsValid)
            {
                await _carService.AddCarAsync(carDto);
                return RedirectToAction(nameof(Index));
            }
            return View(carDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CarDto carDto)
        {
            if (id != carDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _carService.UpdateCarAsync(carDto);
                return RedirectToAction(nameof(Index));
            }
            return View(carDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _carService.DeleteCarAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index");
            }

            query = query.ToLower();
            var cars = await _carService.GetAllCarsAsync();
            var filteredCars = cars.Where(c => c.Description.ToLower().Contains(query) || c.Model.ToLower().Contains(query)).ToList();
            return View("Index", filteredCars);
        }

        public async Task<IActionResult> RentedCars()
        {
            var rentedReservations = await _reservationService.GetRentedCarsAsync();
            var rentedCarIds = rentedReservations.Select(r => r.CarId).ToList();
            var rentedCars = await _carService.GetCarsByIdsAsync(rentedCarIds);

            var rentedCarsViewModel = rentedCars.Select(car => new CarDto
            {
                Id = car.Id,
                Description = car.Description,
                Model = car.Model,
                Year = car.Year,
                DailyPrice = car.DailyPrice
            }).ToList();

            return View(rentedCarsViewModel);
        }
        public async Task<IActionResult> AvailableCars()
        {
            var allCars = await _carService.GetAllCarsAsync();
            var rentedReservations = await _reservationService.GetRentedCarsAsync();

            var rentedCarIds = rentedReservations.Select(r => r.CarId).ToList();
            var availableCars = allCars.Where(car => !rentedCarIds.Contains(car.Id)).ToList();

            return View(availableCars);
        }

        [HttpPost]
        public async Task<IActionResult> CancelReservation(int carId)
        {
            var reservation = (await _reservationService.GetRentedCarsAsync())
                              .FirstOrDefault(r => r.CarId == carId);

            if (reservation != null)
            {
                reservation.IsActive = false;
                await _reservationService.UpdateReservationAsync(reservation);

               
                var car = await _carService.GetCarByIdAsync(carId);
                if (car != null)
                {
                    car.IsRented = false;
                    await _carService.UpdateCarAsync(car);
                }
            }

            return RedirectToAction("RentedCars");
        }

        public async Task<IActionResult> UpdateRentedCars()
        {
            var rentedReservations = await _reservationService.GetRentedCarsAsync();
            var rentedCarIds = rentedReservations.Select(r => r.CarId).ToList();
            var rentedCars = await _carService.GetCarsByIdsAsync(rentedCarIds);

            var rentedCarsViewModel = rentedCars.Select(car => new CarDto
            {
                Id = car.Id,
                Description = car.Description,
                Model = car.Model,
                Year = car.Year,
                DailyPrice = car.DailyPrice
            }).ToList();

            return PartialView("_RentedCarsPartial", rentedCarsViewModel);
        }
    }
}





