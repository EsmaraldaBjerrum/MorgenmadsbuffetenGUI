using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Morgenmadsbuffeten.Data;
using Morgenmadsbuffeten.Models;
using System;
using System.Diagnostics;
using System.Linq;
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

        public IActionResult RestaurantPage([Bind("RoomNumber,CheckedInAdults,CheckedInChildren")] BreakfastOrder breakfastOrder)
        {
            BreakfastOrder OGBreakfastOrder;

            try
            {
                OGBreakfastOrder = _db.BreakfastOrders.First(x => x.RoomNumber == breakfastOrder.RoomNumber && x.Date == DateTime.Now);
            }
            catch
            {
                return NotFound();
            }
            //if (RoomNumber != breakfastOrder.RoomNumber)
            //{
            //    return NotFound();
            //}

            OGBreakfastOrder.CheckedInAdults = breakfastOrder.CheckedInAdults;
            OGBreakfastOrder.CheckedInChildren = breakfastOrder.CheckedInChildren;

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(OGBreakfastOrder);
                    _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!BreakfastOrderExists(breakfastOrder.BreakfastOrderId))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                    throw;
                }
                return RedirectToAction(nameof(HomeController.HomePage));
            }
            return View(breakfastOrder);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize("IsReception")]
        public IActionResult ReceptionAddGuests()
        {
            return View();
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
