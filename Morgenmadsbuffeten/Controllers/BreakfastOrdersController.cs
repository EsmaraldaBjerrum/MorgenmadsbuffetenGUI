using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Morgenmadsbuffeten.Data;
using Morgenmadsbuffeten.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Morgenmadsbuffeten.Controllers
{
    public class BreakfastOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BreakfastOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BreakfastOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.BreakfastOrders.ToListAsync());
        }

        // GET: BreakfastOrders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            DateTime date = Convert.ToDateTime(id);
            var breakfastOrders = await _context.BreakfastOrders.Where(m => m.Date.Date == date.Date).ToListAsync();
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
    }
}

      // GET: BreakfastOrders/Create
      //public IActionResult Create()
      //{
      //   return View();
      //}

      //// POST: BreakfastOrders/Create
      //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      //[HttpPost]
      //[ValidateAntiForgeryToken]
      //public async Task<IActionResult> Create([Bind("RoomNumber,Date,Adults,CheckedInAdults,Children,CheckedInChildren")] BreakfastOrder breakfastOrder)
      //{
      //   if (ModelState.IsValid)
      //   {
      //      _context.Add(breakfastOrder);
      //      await _context.SaveChangesAsync();
      //      return RedirectToAction(nameof(Index));
      //   }
      //   return View(breakfastOrder);
      //}

      // GET: BreakfastOrders/Edit/5
      //public async Task<IActionResult> Edit(long? id)
      //{
      //   if (id == null)
      //   {
      //      return NotFound();
      //   }

      //   var breakfastOrder = await _context.BreakfastOrders.FindAsync(id);
      //   if (breakfastOrder == null)
      //   {
      //      return NotFound();
      //   }
      //   return View(breakfastOrder);
      //}

      // POST: BreakfastOrders/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//      [HttpPost]
//      [ValidateAntiForgeryToken]
//      public async Task<IActionResult> Edit(long id, [Bind("BreakfastOrderId,RoomNumber,Date,Adults,CheckedInAdults,Children,CheckedInChildren")] BreakfastOrder breakfastOrder)
//      {
//         if (id != breakfastOrder.BreakfastOrderId)
//         {
//            return NotFound();
//         }

//         if (ModelState.IsValid)
//         {
//            try
//            {
//               _context.Update(breakfastOrder);
//               await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//               if (!BreakfastOrderExists(breakfastOrder.BreakfastOrderId))
//               {
//                  return NotFound();
//               }
//               else
//               {
//                  throw;
//               }
//            }
//            return RedirectToAction(nameof(Index));
//         }
//         return View(breakfastOrder);
//      }

//      // GET: BreakfastOrders/Delete/5
//      public async Task<IActionResult> Delete(long? id)
//      {
//         if (id == null)
//         {
//            return NotFound();
//         }

//         var breakfastOrder = await _context.BreakfastOrders
//             .FirstOrDefaultAsync(m => m.BreakfastOrderId == id);
//         if (breakfastOrder == null)
//         {
//            return NotFound();
//         }

//         return View(breakfastOrder);
//      }

//      // POST: BreakfastOrders/Delete/5
//      [HttpPost, ActionName("Delete")]
//      [ValidateAntiForgeryToken]
//      public async Task<IActionResult> DeleteConfirmed(long id)
//      {
//         var breakfastOrder = await _context.BreakfastOrders.FindAsync(id);
//         _context.BreakfastOrders.Remove(breakfastOrder);
//         await _context.SaveChangesAsync();
//         return RedirectToAction(nameof(Index));
//      }

//      private bool BreakfastOrderExists(long id)
//      {
//         return _context.BreakfastOrders.Any(e => e.BreakfastOrderId == id);
//      }
//   }
//}
