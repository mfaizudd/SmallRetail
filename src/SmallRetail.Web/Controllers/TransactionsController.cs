using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmallRetail.Data.Models;
using SmallRetail.Services;
using SmallRetail.Web.Resources;

namespace SmallRetail.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _service;
        private readonly IMapper _mapper;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var transactions = _service.GetAll();
            var transactionResources =
                _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionResource>>(transactions);
            return Ok(transactionResources);
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
            _service.Create(transaction);
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