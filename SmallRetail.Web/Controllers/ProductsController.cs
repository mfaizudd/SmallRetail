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
    [Authorize(Policy = "User")]
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
        [AllowAnonymous]
        public IActionResult Get(int limit = 10, int page = 1)
        {
            var totalProducts = _service.Count;
            var totalPages = (int)Math.Ceiling((double)totalProducts / limit);
            if (page > totalPages || page < 1)
                return NotFound();
            var products = _service.GetAll(limit, page);
            var productResources = _mapper.Map<IEnumerable<ProductResponse>>(products);
            var response = new PagedResponse<IEnumerable<ProductResponse>>(productResources)
            {
                TotalItems = totalProducts,
                CurrentPage = page,
                TotalPages = totalPages
            };
            if (page > 1)
            {
                var prevResource = new LinkedResource(Url.Action(nameof(Get), new { limit, page = page - 1 }));
                response.Links.Add(LinkedResourceType.Prev, prevResource);
            }
            if (page < response.TotalPages)
            {
                var nextResource = new LinkedResource(Url.Action(nameof(Get), new { limit, page = page + 1 }));
                response.Links.Add(LinkedResourceType.Next, nextResource);
            }
            return Ok(response);
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
        [AllowAnonymous]
        public ActionResult<ProductResponse> Get(Guid id)
        {
            var product = _service.Get(id);
            if (product == null)
                return NotFound();
            var productResponse = _mapper.Map<ProductResponse>(product);
            return Ok(new Response<ProductResponse>(productResponse));
        }

        [HttpGet("Barcode/{barcode}")]
        public IActionResult GetByBarcode(string barcode)
        {
            var products = _service.Where(p => p.Barcode == barcode);
            if (!products.Any())
                return NotFound();

            var productsResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return Ok(new Response<IEnumerable<ProductResponse>>(productsResponse));
        }

        [HttpPut("{id:guid}")]
        public IActionResult Put(ProductRequest productRequest, Guid id)
        {
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
