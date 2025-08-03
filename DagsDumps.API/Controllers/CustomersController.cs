using Microsoft.AspNetCore.Mvc;
using DagsDumps.API.Data;
using DagsDumps.API.Models;

namespace DagsDumps.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly DagsDumpsDbContext _context;

        public CustomersController(DagsDumpsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            return Ok(_context.Customers.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer updated)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            customer.FullName = updated.FullName;
            customer.Email = updated.Email;
            customer.PhoneNumber = updated.PhoneNumber;
            customer.Address = updated.Address;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
