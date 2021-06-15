using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmallRetail.Data;
using SmallRetail.Data.Models;
using SmallRetail.Services;
using SmallRetail.Web.Resources;

namespace SmallRetail.Web.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _service;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController(IUserService service, ILogger<UsersController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var users = _service.GetAll();
            var userResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(users);
            return Ok(userResources);
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var user = _service.Get(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _service.Create(user);
            return CreatedAtAction(nameof(Get), new {user.Id}, user);
        }

        [HttpPut]
        public IActionResult Update(User user)
        {
            try
            {
                _service.Update(user);
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