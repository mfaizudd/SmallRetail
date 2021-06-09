using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmallRetail.Data.Models;
using SmallRetail.Services;

namespace SmallRetail.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _service;

        public ProductsController(ILogger<ProductsController> logger, IProductService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _service.Create(product);
            return CreatedAtAction(nameof(Get), new {id = product.Id}, product);
        }
        
        [HttpGet("{id:guid}")]
        public ActionResult<Product> Get(Guid id)
        {
            var product = _service.Get(id);
            if (product == null)
                return NotFound();
            return product;
        }

        [HttpPut]
        public IActionResult Put(Product product)
        {
            try
            {
                _service.Update(product);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }

            return NoContent();
        }
    }
}
