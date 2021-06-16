using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmallRetail.Data.Models;
using SmallRetail.Services;
using SmallRetail.Web.Resources;

namespace SmallRetail.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger, IProductService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _service.GetAll();
            var productResources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(products);
            return Ok(productResources);
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

        [HttpPut("{id:guid}")]
        public IActionResult Put(Product product, Guid id)
        {
            try
            {
                _service.Update(product, id);
            }
            catch (ArgumentException e)
            {
                _logger.Log(LogLevel.Information, e.Message);
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
                _logger.Log(LogLevel.Information, e.Message);
                return NotFound();
            }

            return NoContent();
        }
    }
}
