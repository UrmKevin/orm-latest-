﻿using Microsoft.AspNetCore.Mvc;
using orm.Data;
using orm.Models;

namespace orm.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<CartProduct> GetCartProducts()
        {
            var cartproduct = _context.CartProducts.ToList();
            return cartproduct;
        }

        [HttpPost]
        public List<CartProduct> PostCartProducts([FromBody] CartProduct cartproduct)
        {
            _context.CartProducts.Add(cartproduct);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }

        [HttpDelete("{id}")]
        public List<CartProduct> DeleteCartProducts(int id)
        {
            var cartproduct = _context.CartProducts.Find(id);

            if (cartproduct == null)
            {
                return _context.CartProducts.ToList();
            }

            _context.CartProducts.Remove(cartproduct);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<CartProduct> GetCartProducts(int id)
        {
            var cartproduct = _context.CartProducts.Find(id);

            if (cartproduct == null)
            {
                return NotFound();
            }

            return cartproduct;
        }

        [HttpPut("{id}")]
        public ActionResult<List<CartProduct>> PutCartProducts(int id, [FromBody] CartProduct updatedCartProducts)
        {
            var cartproduct = _context.CartProducts.Find(id);

            if (cartproduct == null)
            {
                return NotFound();
            }

            cartproduct.ProductId = updatedCartProducts.ProductId;
            cartproduct.Quantity = updatedCartProducts.Quantity;

            _context.CartProducts.Update(cartproduct);
            _context.SaveChanges();

            return Ok(_context.CartProducts);
        }
    }
}
