using System;
using System.ComponentModel.DataAnnotations;

namespace Morgenmadsbuffeten.Models
{
   public class KitchenModel
   {
      public int TotalAdultsForChosenDate { get; set; }
      public int TotalChildrenForChosenDate { get; set; }
      public int TotalForChosenDate { get; set; }
      [Display(Name="Checked In Adults")]
      public int TotalAdultsCheckedIn { get; set; }
      [Display(Name = "Checked In Children")]
      public int TotalChildrenCheckedIn { get; set; }
      public int NotCheckedInAdults { get; set; }
      public int NotCheckedInChildren { get; set; }
      public int NotCheckedInTotal { get; set; }
      public String ChosenDate { get; set; }
   }
}
