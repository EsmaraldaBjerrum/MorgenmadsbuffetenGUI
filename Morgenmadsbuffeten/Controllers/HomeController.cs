using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Morgenmadsbuffeten.Models;
using System.Diagnostics;

namespace Morgenmadsbuffeten.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult KitchenPage()
        {
            return View();
        }

        public IActionResult RestaurantPage()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ReceptionAddGuests()
        {
            return View();
        }
        public IActionResult ReceptionOverview()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
