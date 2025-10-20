using ecommerce.Data;
using ecommerce.Dtos;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderDto dto)
        {
            try
            {
                // Validate CustomerId
                if (dto.CustomerId <= 0)
                    return BadRequest(new { message = "CustomerId is required." });

                // Check if customer exists
                var customerExists = _context.Customers.Any(c => c.Id == dto.CustomerId);
                if (!customerExists)
                    return BadRequest(new { message = "Customer does not exist." });

                // Validate that at least one product is included
                if (dto.OrderProducts == null || dto.OrderProducts.Count == 0)
                    return BadRequest(new { message = "Order must contain at least one product." });

                // Get product prices and validate all products exist
                var productIds = dto.OrderProducts.Select(p => p.ProductId).ToList();
                var products = _context.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToList();

                // Missing products check
                var missingProducts = productIds.Except(products.Select(p => p.Id)).ToList();
                if (missingProducts.Any())
                    return BadRequest(new { message = $"Some products do not exist: {string.Join(", ", missingProducts)}" });

                // Check stock levels
                var outOfStock = products.Where(p => p.Stock <= 0).ToList();
                if (outOfStock.Any())
                    return BadRequest(new { message = $"Out of stock: {string.Join(", ", outOfStock.Select(p => p.Name))}" });

                // Calculate total price
                double totalPrice = products.Sum(p => p.Price);

                // Create new Order entity
                var order = new Order
                {
                    CustomerId = dto.CustomerId,
                    Status = "Pending",
                    TotalPrice = totalPrice,
                    OrderProducts = dto.OrderProducts.Select(p => new OrderProduct
                    {
                        ProductId = p.ProductId
                    }).ToList()
                };

                // Add order to database
                _context.Orders.Add(order);
                _context.SaveChanges();

                // Return success result
                return Ok(new
                {
                    message = "Order created successfully.",
                    totalPrice
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while creating the order.",
                    error = ex.Message  
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                var order = _context.Orders
                    .Where(o => o.Id == id)
                    .Select(o => new
                    {
                        OrderId = o.Id,
                        CustomerName = o.Customer.Name,
                        o.Status,
                        ProductsCount = o.OrderProducts.Count
                    })
                    .FirstOrDefault();

                if (order == null)
                    return NotFound(new { message = "Order not found." });

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while retrieving the order.",
                    error = ex.Message
                });
            }
        }

        [HttpPost("{id}")]
        public IActionResult UpdateOrderStatus(int id)
        {
            try
            {
                // Load order with products
                var order = _context.Orders
                    .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                    .FirstOrDefault(o => o.Id == id);

                if (order == null)
                    return NotFound(new { message = "Order not found." });

                if (order.Status == "Delivered")
                    return BadRequest(new { message = "Order already delivered." });

                // Update order status
                order.Status = "Delivered";

                // Decrease stock for each product in the order
                foreach (var op in order.OrderProducts)
                {
                    if (op.Product.Stock <= 0)
                        return BadRequest(new { message = $"Product '{op.Product.Name}' is out of stock." });

                    op.Product.Stock -= 1;
                }

                _context.SaveChanges();

                return Ok(new
                {
                    message = "Order delivered successfully.",
                    orderId = order.Id,
                    order.Status
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while updating the order status.",
                    error = ex.Message
                });
            }
        }

    }
}
