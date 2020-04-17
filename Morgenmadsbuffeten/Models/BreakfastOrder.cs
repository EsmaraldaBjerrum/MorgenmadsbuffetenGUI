using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Morgenmadsbuffeten.Models
{
    public class BreakfastOrder
    {
        [Required]        
        public string BreakfastOrderId {get { return BreakfastOrderId; } set { BreakfastOrderId = Date.Date.ToOADate().ToString() + RoomNumber; } }
        [Required]
        public long RoomNumber { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public int Adults { get; set; } = 0;
        public int CheckedInAdults { get; set; } = 0;
        public int Children { get; set; } = 0;
        public int CheckedInChildren { get; set; } = 0;
       
    }
}
