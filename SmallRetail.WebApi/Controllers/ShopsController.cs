using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallRetail.WebApi.Controllers.Resources;
using SmallRetail.WebApi.Data;
using SmallRetail.WebApi.Helpers;
using SmallRetail.WebApi.Services;
using SmallRetail.WebApi.Services.DTO;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmallRetail.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopsController : ControllerBase
    {
        private readonly IShopService _service;

        public ShopsController(IShopService service)
        {
            _service = service;
        }

        // GET: api/Shops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shop>>> GetShops(int limit = 10, int offset = 0)
        {
            var shops = await _service.List(limit, offset);
            return Ok(shops);
        }

        // GET: api/Shops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shop>> GetShop(long id)
        {
            var shop = await _service.Get(id);

            if (shop == null)
            {
                return NotFound();
            }

            return shop;
        }

        // PUT: api/Shops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShop(long id, ShopRequest request)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }
                var input = new ShopInput
                {
                    UserId = userId,
                    Name = request.Name,
                };
                await _service.Update(id, input);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ShopExists(id))
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

        // POST: api/Shops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shop>> PostShop(ShopRequest request)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var input = new ShopInput
            {
                UserId = userId,
                Name = request.Name,
            };
            var shop = await _service.Create(input);

            return CreatedAtAction("GetShop", new { id = shop.Id }, shop);
        }

        // DELETE: api/Shops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(long id)
        {
            if (!await ShopExists(id))
            {
                return NotFound();
            }

            await _service.Delete(id);

            return NoContent();
        }

        [HttpPost("{id}/products")]
        public async Task<IActionResult> AddProducts(long id, ShopProductInput[] inputs)
        {
            if (!await ShopExists(id))
            {
                return NotFound();
            }

            await _service.AddProducts(id, inputs);

            return NoContent();
        }

        private async Task<bool> ShopExists(long id)
        {
            return await _service.Get(id) != null;
        }
    }
}
