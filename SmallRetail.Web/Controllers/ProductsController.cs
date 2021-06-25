using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmallRetail.Data.Models;
using SmallRetail.Services;
using SmallRetail.Web.Resources;
using SmallRetail.Web.Validators;

namespace SmallRetail.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Admin")]
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
        public IActionResult Index(int limit = 10, int page = 1)
        {
            var products = _service.GetAll(limit, page);
            var productResources = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return Ok(productResources);
        }

        [HttpPost]
        public IActionResult Create(ProductRequest productRequest)
        {
            var validator = new ProductRequestValidator();
            var validationResult = validator.Validate(productRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var product = _mapper.Map<Product>(productRequest);
            _service.Create(product);
            var productResponse = _mapper.Map<ProductResponse>(product);
            return CreatedAtAction(nameof(Get), new {id = product.Id}, productResponse);
        }
        
        [HttpGet("{id:guid}")]
        public ActionResult<ProductResponse> Get(Guid id)
        {
            var product = _service.Get(id);
            if (product == null)
                return NotFound();
            var productResponse = _mapper.Map<ProductResponse>(product);
            return Ok(productResponse);
        }

        [HttpGet("Barcode/{barcode}")]
        public IActionResult GetByBarcode(string barcode)
        {
            var products = _service.Where(p => p.Barcode == barcode);
            if (products.Count() <= 0)
                return NotFound();

            var productsResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return Ok(productsResponse);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Put(ProductRequest productRequest, Guid id)
        {
            var validator = new ProductRequestValidator();
            var validationResult = validator.Validate(productRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var product = _mapper.Map<Product>(productRequest);
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
