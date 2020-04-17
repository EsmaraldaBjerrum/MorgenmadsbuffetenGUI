using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Morgenmadsbuffeten.Data;
using Morgenmadsbuffeten.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Morgenmadsbuffeten.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
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

        [Authorize("IsReception")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReceptionAddGuests([Bind("RoomNumber,Date,Adults,CheckedInAdults,Children,CheckedInChildren")] BreakfastOrder breakfastOrder)
        {
            if (ModelState.IsValid)
            {
                _db.Add(breakfastOrder);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(ReceptionAddGuests));
            }
            return View(breakfastOrder);
        }

        [Authorize("IsReception")]
        public async Task<IActionResult> ReceptionOverview()
        {
            return View(await _db.BreakfastOrders.ToListAsync());
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
