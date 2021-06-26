using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var transactionResponses =
                _mapper.Map<IEnumerable<TransactionResponse>>(transactions);
            var response = new ResponseWrapper<IEnumerable<TransactionResponse>>(transactionResponses);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<TransactionResponse> Get(Guid id)
        {
            var transaction = _service.Get(id);
            if (transaction == null)
                return NotFound();

            var transactionResponse = _mapper.Map<TransactionResponse>(transaction);

            var response = new ResponseWrapper<TransactionResponse>(transactionResponse);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(TransactionRequest transactionRequest)
        {
            var validator = new TransactionRequestValidator();
            var validationResult = validator.Validate(transactionRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var transaction = _mapper.Map<Transaction>(transactionRequest);
            _service.Create(transaction);
            var transactionResponse = _mapper.Map<TransactionResponse>(transaction);
            return CreatedAtAction(nameof(Get), new { transaction.Id }, transactionResponse);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(TransactionRequest transactionRequest, Guid id)
        {
            var validator = new TransactionRequestValidator();
            var validationResult = validator.Validate(transactionRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var transaction = _mapper.Map<Transaction>(transactionRequest);
            try
            {
                _service.Update(transaction, id);
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