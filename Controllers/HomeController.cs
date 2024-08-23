using Microsoft.AspNetCore.Mvc;
using Rac.Data;
using Rac.Web.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Rac.Buisness.Services;
using Rac.entity.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Rac.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerService _customerService;

        public HomeController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var customer = await _customerService.GetCustomerByEmailAsync(email);

            if (customer != null && customer.Password == password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                    new Claim(ClaimTypes.Role, customer.Role.ToString())
                };

                var identity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                if (customer.Role == 0) // Admin
                {
                    return RedirectToAction("Index", "Admin");
                }
                else // User
                {
                    return RedirectToAction("Index", "User");
                }
            }

            // Hatalý giriþ
            ViewBag.ErrorMessage = "Invalid Email or Password.";
            return View();
        }
    }
}