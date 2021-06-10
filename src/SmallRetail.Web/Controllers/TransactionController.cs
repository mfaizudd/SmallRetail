using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmallRetail.Data.Models;
using SmallRetail.Services;

namespace SmallRetail.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _service;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Transaction> Get(int id)
        {
            var transaction = _service.Get(id);
            if (transaction == null)
                return NotFound();

            return transaction;
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            try
            {
                _service.Create(transaction);
            }
            catch (ArgumentException e)
            {
                _logger.Log(LogLevel.Information, e.Message);
                return NotFound();
            }

            return CreatedAtAction(nameof(Get), new {Id = transaction.Id}, transaction);
        }

        [HttpPut]
        public IActionResult Update(Transaction transaction)
        {
            try
            {
                _service.Update(transaction);
            }
            catch (ArgumentException e)
            {
                _logger.Log(LogLevel.Information, e.Message);
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
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