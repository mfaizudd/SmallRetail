using System;
using System.Collections.Generic;
using AutoMapper;
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
        public ActionResult<UserResponse> Get(Guid id)
        {
            var user = _service.Get(id);
            if (user == null)
                return NotFound();

            var userResponse = _mapper.Map<UserResponse>(user);
            return Ok(userResponse);
        }

        [HttpPost]
        public IActionResult Create(UserRequest userRequest)
        {
            var validator = new UserRequestValidator();
            var validationResult = validator.Validate(userRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = _mapper.Map<User>(userRequest);
            _service.Create(user);
            return CreatedAtAction(nameof(Get), new {user.Id}, user);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(UserRequest userRequest, Guid id)
        {
            var validator = new UserRequestValidator();
            var validationResult = validator.Validate(userRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = _mapper.Map<User>(userRequest);
            try
            {
                _service.Update(user, id);
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