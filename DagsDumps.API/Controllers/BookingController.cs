using Microsoft.AspNetCore.Mvc;
using DagsDumps.API.Data;
using DagsDumps.API.Models;

namespace DagsDumps.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly DagsDumpsDbContext _context;

        public BookingsController(DagsDumpsDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetAllBookings()
        {
            return Ok(_context.Bookings.ToList());
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public ActionResult<Booking> CreateBooking(Booking booking)
        {
            if (booking.PickUpDate <= booking.DropOffDate == false)
                return BadRequest("PickUpDate must be after DropOffDate.");

            var customerExists = _context.Customers.Any(c => c.CustomerId == booking.CustomerId);
            var dumpsterExists = _context.Dumpsters.Any(d => d.DumpsterId == booking.DumpsterId);
            if (!customerExists || !dumpsterExists)
                return BadRequest("Invalid CustomerId or DumpsterId.");

            var conflicts = _context.Bookings.Any(b =>
                b.DumpsterId == booking.DumpsterId &&
                !(b.PickUpDate <= booking.DropOffDate || b.DropOffDate >= booking.PickUpDate));

            if (conflicts)
                return Conflict("Dumpster is not available for the selected dates.");

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingId }, booking);
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, Booking updated)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null) return NotFound();

            booking.CustomerId = updated.CustomerId;
            booking.DumpsterId = updated.DumpsterId;
            booking.DropOffDate = updated.DropOffDate;
            booking.PickUpDate = updated.PickUpDate;
            booking.DropOffAddress = updated.DropOffAddress;
            booking.Status = updated.Status;
            booking.TotalCost = updated.TotalCost;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null) return NotFound();

            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
