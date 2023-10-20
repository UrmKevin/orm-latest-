using Microsoft.AspNetCore.Mvc;
using orm.Data;
using orm.Models;

namespace orm.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Category> GetCategories()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

        [HttpPost]
        public List<Category> PostCategories([FromBody] Category categories)
        {
            _context.Categories.Add(categories);
            _context.SaveChanges();
            return _context.Categories.ToList();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategories(int id)
        {
            var categories = _context.Categories.Find(id);

            if (categories == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categories);
            _context.SaveChanges();
            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategories(int id)
        {
            var categories = _context.Categories.Find(id);

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Category>> PutCategories(int id, [FromBody] Category updatedCategories)
        {
            var categories = _context.Person.Find(id);

            if (categories == null)
            {
                return NotFound();
            }

            categories.FirstName = updatedCategories.Name;

            _context.Person.Update(categories);
            _context.SaveChanges();

            return Ok(_context.Person);
        }
    }
}
