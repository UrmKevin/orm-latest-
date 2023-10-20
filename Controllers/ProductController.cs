using Microsoft.AspNetCore.Mvc;
using orm.Data;
using orm.Models;

namespace orm.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Product> GetProducts()
        {
            var products = _context.Products.ToList();
            return products;
        }

        [HttpPost]
        public List<Product> PostProducts([FromBody] Product products)
        {
            _context.Products.Add(products);
            _context.SaveChanges();
            return _context.Products.ToList();
        }

        [HttpDelete("{id}")]
        public List<Product> DeleteProducts(int id)
        {
            var products = _context.Products.Find(id);

            if (products == null)
            {
                return _context.Products.ToList();
            }

            _context.Products.Remove(products);
            _context.SaveChanges();
            return _context.Products.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProducts(int id)
        {
            var products = _context.Products.Find(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Product>> PutProducts(int id, [FromBody] Product updatedProduct)
        {
            var products = _context.Products.Find(id);

            if (products == null)
            {
                return NotFound();
            }

            products.Name = updatedProduct.Name;
            products.Price = updatedProduct.Price;
            products.Image = updatedProduct.Image;
            products.Active = updatedProduct.Active;
            products.Stock = updatedProduct.Stock;
            products.CategoryId = updatedProduct.CategoryId;

            _context.Products.Update(products);
            _context.SaveChanges();

            return Ok(_context.Products);
        }
    }
}
