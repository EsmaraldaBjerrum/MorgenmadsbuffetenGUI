using System.ComponentModel.DataAnnotations;

namespace Morgenmadsbuffeten.Models
{
   public class CheckInBreakfastGuests
   {
      [Display(Name = "Room number")]
      public long RoomNumber { get; set; }
      [Display(Name = "Checked In Adults")]
      public int CheckedInAdults { get; set; }
      [Display(Name = "Checked In Children")]
      public int CheckedInChildren { get; set; }
   }
}
