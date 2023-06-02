using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallRetail.WebApi.Controllers.Resources;
using SmallRetail.WebApi.Data;
using SmallRetail.WebApi.Helpers;
using SmallRetail.WebApi.Services;
using SmallRetail.WebApi.Services.DTO;

namespace SmallRetail.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int limit = 10, int offset = 0)
        {
            var products = await _service.List(limit, offset);
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(long id)
        {
            var product = await _service.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, ProductRequest request)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }
                var input = new ProductInput
                {
                    Barcode = request.Barcode,
                    Name = request.Name,
                    Price = request.Price,
                    Stock = request.Stock,
                    UserId = userId,
                };
                await _service.Update(id, input);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductRequest request)
        {
            var userId = User.GetUserId();
            if (userId is null)
            {
                return Unauthorized();
            }
            var input = new ProductInput
            {
                Barcode = request.Barcode,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                UserId = userId,
            };
            var product = await _service.Create(input);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            if (!await ProductExists(id))
            {
                return NotFound();
            }

            await _service.Delete(id);

            return NoContent();
        }

        private async Task<bool> ProductExists(long id)
        {
            return await _service.Get(id) != null;
        }
    }
}
