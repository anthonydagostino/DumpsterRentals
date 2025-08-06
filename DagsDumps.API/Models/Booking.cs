using System;

namespace DagsDumps.API.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int DumpsterId { get; set; }
        public Dumpster Dumpster { get; set; }

        public DateTime DropOffDate { get; set; }
        public DateTime PickUpDate { get; set; }

        public string DropOffAddress { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
