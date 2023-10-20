using Microsoft.AspNetCore.Mvc;
using orm.Data;
using orm.Models;

namespace orm.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            var orders = _context.Orders.ToList();
            return orders;
        }

        [HttpPost]
        public List<Order> PostOrders([FromBody] Order orders)
        {
            _context.Orders.Add(orders);
            _context.SaveChanges();
            return _context.Orders.ToList();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrders(int id)
        {
            var orders = _context.Orders.Find(id);

            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            _context.SaveChanges();
            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrders(int id)
        {
            var orders = _context.Orders.Find(id);

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Order>> PutOrders(int id, [FromBody] Order updatedOrder)
        {
            var orders = _context.Orders.Find(id);

            if (orders == null)
            {
                return NotFound();
            }

            orders.created = updatedOrder.created;
            orders.TotalSum = updatedOrder.TotalSum;
            orders.Paid = updatedOrder.Paid;
            orders.CartProduct = updatedOrder.CartProduct;
            orders.Person = updatedOrder.Person;

            _context.Orders.Update(orders);
            _context.SaveChanges();

            return Ok(_context.Orders);
        }
    }
}
