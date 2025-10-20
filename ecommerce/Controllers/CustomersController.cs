using ecommerce.Data;
using ecommerce.Models;
using ecommerce.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }


        // Get All Customers
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var customers = _context.Customers.ToList();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving customers", error = ex.Message });
            }
        }

        // Get Customer by ID 
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Console.WriteLine($"Looking for customer with ID = {id}");

                var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

                if (customer == null)
                    return NotFound(new { message = "Customer not found" });

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving customer", error = ex.Message });
            }
        }

        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            var validator = new CustomerValidator();
            var validationResult = validator.Validate(customer);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { Errors = errors });
            }

            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetById),
                    new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error", error = ex.Message });
            }
        }
    }
}
