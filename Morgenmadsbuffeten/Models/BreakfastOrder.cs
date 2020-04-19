using System;
using System.ComponentModel.DataAnnotations;

namespace Morgenmadsbuffeten.Models
{
   public class BreakfastOrder
   {
      [Required]
      public long BreakfastOrderId { get; set; }
      [Required]
      [Display(Name = "Room number")]
      public long RoomNumber { get; set; }
      [Required]
      [Display(Name = "Date")]
      public DateTime Date { get; set; }
      [Display(Name = "Adults")]
      public int Adults { get; set; } = 0;
      public int CheckedInAdults { get; set; } = 0;
      [Display(Name = "Children")]
      public int Children { get; set; } = 0;
      public int CheckedInChildren { get; set; } = 0;

   }
}
