using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Rac.Buisness.Services;
using Rac.entity;
using Rac.entity.DTOs;
using Rac.Web.Models;
using System.Security.Claims;

namespace Rac.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ICarService _carService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public UserController(IReservationService reservationService, ICarService carService, ICustomerService customerService, IMapper mapper)
        {
            _reservationService = reservationService;
            _carService = carService;
            _customerService = customerService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCarsAsync();
            return View(cars);
        }



        [HttpGet]
        public async Task<IActionResult> Reserve(int carId)
        {
            var car = await _carService.GetCarByIdAsync(carId);
            if (car == null)
            {
                return NotFound();
            }

            var model = new ReserveViewModel
            {
                CarId = carId,
                DailyPrice = car.DailyPrice
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(ReserveViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var car = await _carService.GetCarByIdAsync(model.CarId);
            if (car == null)
            {
                ModelState.AddModelError("", "The car you are trying to reserve does not exist.");
                return View(model);
            }

            var reservationDto = new ReservationDto
            {
                CarId = model.CarId,
                CustomerId = 1,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            try
            {
                await _reservationService.AddReservationAsync(reservationDto);
                return RedirectToAction("ReservationSuccess");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating reservation: {ex.Message}");
                return View(model);
            }
        
    }
        public async Task<IActionResult> Details(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
    }
}

