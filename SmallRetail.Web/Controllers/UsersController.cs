using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "Admin")]
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
        public IActionResult Get(int limit = 10, int page = 1)
        {
            var totalUsers = _service.Count;
            var totalPages = (int)Math.Ceiling((double)totalUsers / limit);
            if (page > totalPages || page < 1)
                return NotFound();
            var users = _service.GetAll(limit, page);
            var userResources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResponse>>(users);
            var response = new PagedResponse<IEnumerable<UserResponse>>(userResources)
            {
                TotalItems = totalUsers,
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

        [HttpGet("{id:guid}")]
        public ActionResult<UserResponse> Get(Guid id)
        {
            var user = _service.Get(id);
            if (user == null)
                return NotFound();

            var userResponse = _mapper.Map<UserResponse>(user);
            var response = new Response<UserResponse>(userResponse);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var user = _service.Login(loginRequest.Username, loginRequest.Password);
            if (user == null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim("type", user.Type.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost]
        [AllowAnonymous]
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