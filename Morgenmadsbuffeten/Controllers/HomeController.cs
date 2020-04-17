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

      public async Task<IActionResult> KitchenPage(string id)
      {
         DateTime date = Convert.ToDateTime(id);
         var breakfastOrders = await _db.BreakfastOrders.Where(m => m.Date.Date == date.Date).ToListAsync();

         if (breakfastOrders == null)
         {
            return NotFound();
         }

         var totalAdultsForChosenDate = 0;
         var totalChildrenForChosenDate = 0;
         var totalAdultsCheckedIn = 0;
         var totalChildrenCheckedIn = 0;

         foreach (var breakfeastOrder in breakfastOrders)
         {
            totalAdultsForChosenDate += breakfeastOrder.Adults;
            totalChildrenForChosenDate += breakfeastOrder.Children;
            totalAdultsCheckedIn += breakfeastOrder.CheckedInAdults;
            totalChildrenCheckedIn += breakfeastOrder.CheckedInChildren;
         }

         var kitchenModel = new KitchenModel
         {
            TotalAdultsCheckedIn = totalAdultsCheckedIn,
            TotalAdultsForChosenDate = totalAdultsForChosenDate,
            TotalChildrenCheckedIn = totalChildrenCheckedIn,
            TotalChildrenForChosenDate = totalChildrenForChosenDate,
            TotalForChosenDate = totalChildrenForChosenDate + totalAdultsForChosenDate,
            NotCheckedInAdults = totalAdultsForChosenDate - totalAdultsCheckedIn,
            NotCheckedInChildren = totalChildrenForChosenDate - totalChildrenCheckedIn,
            NotCheckedInTotal = (totalAdultsForChosenDate - totalAdultsCheckedIn) +
                                (totalChildrenForChosenDate - totalChildrenCheckedIn),
            ChosenDate = date.ToString("yyyy-MM-dd")
         };

         return View(kitchenModel);
      }

      public IActionResult RestaurantPage()
      {
         return View();
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
