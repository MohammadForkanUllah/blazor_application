using BlazorExam_11.Server.Models;
using BlazorExam_11.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorExam_11.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MyDbContext _context;
        public OrderController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _context.Orders.Include(o => o.OrderItem).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var order = await _context.Orders.Include(o => o.OrderItem).FirstOrDefaultAsync(a => a.OrderId == id);
            return Ok(order);
        }


        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("products")]
        public async Task<List<Product>> Product()
        {
            return await _context.Products.ToListAsync();
        }
    }
}