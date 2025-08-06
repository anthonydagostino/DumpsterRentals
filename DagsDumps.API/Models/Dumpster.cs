using System.Collections.Generic;

namespace DagsDumps.API.Models
{
    public class Dumpster
    {
        public int DumpsterId { get; set; }
        public int SizeYards { get; set; }         // e.g. 10, 15, 20
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float MaxWeightTons { get; set; }
        public decimal OverageFeePerTon { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<Booking> Bookings { get; set; }
    }
}
