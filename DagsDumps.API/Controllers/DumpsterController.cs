using Microsoft.AspNetCore.Mvc;
using DagsDumps.API.Data;
using DagsDumps.API.Models;

namespace DagsDumps.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DumpstersController : ControllerBase
    {
        private readonly DagsDumpsDbContext _context;

        public DumpstersController(DagsDumpsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Dumpster>> GetAllDumpsters()
        {
            return Ok(_context.Dumpsters.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Dumpster> GetDumpster(int id)
        {
            var dumpster = _context.Dumpsters.Find(id);
            if (dumpster == null) return NotFound();
            return Ok(dumpster);
        }

        [HttpPost]
        public ActionResult<Dumpster> CreateDumpster(Dumpster dumpster)
        {
            // Force EF/SQL Server to generate the primary key
            dumpster.DumpsterId = 0;

            _context.Dumpsters.Add(dumpster);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetDumpster), new { id = dumpster.DumpsterId }, dumpster);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDumpster(int id, Dumpster updated)
        {
            var dumpster = _context.Dumpsters.Find(id);
            if (dumpster == null) return NotFound();

            dumpster.SizeYards = updated.SizeYards;
            dumpster.Description = updated.Description;
            dumpster.Price = updated.Price;
            dumpster.MaxWeightTons = updated.MaxWeightTons;
            dumpster.OverageFeePerTon = updated.OverageFeePerTon;
            dumpster.IsAvailable = updated.IsAvailable;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDumpster(int id)
        {
            var dumpster = _context.Dumpsters.Find(id);
            if (dumpster == null) return NotFound();

            _context.Dumpsters.Remove(dumpster);
            _context.SaveChanges();
            return NoContent();
        }

        // GET: api/dumpsters/available?from=2025-08-10&to=2025-08-12&sizeYards=10
        [HttpGet("available")]
        public ActionResult<IEnumerable<Dumpster>> GetAvailable(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            [FromQuery] int? sizeYards)
        {
            if (to <= from) return BadRequest("End date must be after start date.");

            var q = _context.Dumpsters.AsQueryable();
            if (sizeYards.HasValue)
                q = q.Where(d => d.SizeYards == sizeYards.Value);

            // overlap if NOT (existing ends <= start OR existing starts >= end)
            var overlappingDumpsterIds = _context.Bookings
                .Where(b => !(b.PickUpDate <= from || b.DropOffDate >= to))
                .Select(b => b.DumpsterId);

            var available = q.Where(d => !overlappingDumpsterIds.Contains(d.DumpsterId))
                             .ToList();

            return Ok(available);
        }

    }
}
