using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IConfiguration _config;

        public UsersController(IUserService service, ILogger<UsersController> logger, IMapper mapper, IConfiguration config)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
            _config = config;
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

        [HttpPost("[action]")]
        public IActionResult Login(string username, string password)
        {
            if (_service.Login(username, password))
                return Unauthorized();

            var Claims = new List<Claim>
            {
                new Claim("type", "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                Claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(token));
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
            var userResponse = _mapper.Map<UserResponse>(user);
            return CreatedAtAction(nameof(Get), new { user.Id }, userResponse);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(UserRequest userRequest, Guid id)
        {
            var validator = new UserRequestValidator(true);
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