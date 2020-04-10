using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Morgenmadsbuffeten.Models
{
    public class BreakfastOrder
    {
        public long BreakfastOrderId { get; set; }
        [Required]
        public long RoomNumber { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Adults { get; set; }
        public int CheckedInAdults { get; set; }
        [Required]
        public int Children { get; set; }
        public int CheckedInChildren { get; set; }
    }
}
