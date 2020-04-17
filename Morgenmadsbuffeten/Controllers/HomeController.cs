﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Morgenmadsbuffeten.Data;
using Morgenmadsbuffeten.Models;
using System;
using System.Collections.Generic;
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

        // GET: BreakfastOrders/Edit/
        public async Task<IActionResult> RestaurantPage(long? id)
        {
            if (id == null)
            {
                return View(new CheckInBreakfastGuests());
            }

            var breakfastOrder = await _db.BreakfastOrders.FindAsync(id);
            if (breakfastOrder == null)
            {
                return NotFound();
            }
            return View(breakfastOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestaurantPage([Bind("RoomNumber,CheckedInAdults,CheckedInChildren")] CheckInBreakfastGuests checkInGuests)
        {
            BreakfastOrder OGBreakfastOrder;
            
            try
            {
                OGBreakfastOrder = _db.BreakfastOrders.Where(x => x.RoomNumber == checkInGuests.RoomNumber && x.Date == DateTime.Now.Date).FirstOrDefault();
            }
            catch
            {
                //return RedirectToAction(nameof(HomeController.HomePage));
                return NotFound();
            }

            OGBreakfastOrder.CheckedInAdults = checkInGuests.CheckedInAdults;
            OGBreakfastOrder.CheckedInChildren = checkInGuests.CheckedInChildren;

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(OGBreakfastOrder);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(HomeController.HomePage));
            }
            return View(checkInGuests);
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
