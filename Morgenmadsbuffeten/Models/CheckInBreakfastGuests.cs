using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Morgenmadsbuffeten.Models
{
    public class CheckInBreakfastGuests
    {
        public long RoomNumber { get; set; }
        public int CheckedInAdults { get; set; }
        public int CheckedInChildren { get; set; }
    }
}
