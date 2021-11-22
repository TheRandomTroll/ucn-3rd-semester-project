using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StreetPatch.API.Services.Interfaces;
using StreetPatch.Data.Entities;
using StreetPatch.Data.Entities.DTO;
using StreetPatch.Data.Repositories;

namespace StreetPatch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfigurationSection _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UsersRepository _usersRepository;

        public AuthController(IConfiguration configuration, UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService, UsersRepository usersRepository)
        {
            _jwtSettings = configuration.GetSection("Jwt");
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _usersRepository = usersRepository;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var user = _mapper.Map<SignUpDto, ApplicationUser>(signUpDto);

            var userCreateResult = await _userManager.CreateAsync(user, signUpDto.Password);

            if (userCreateResult.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }

        /// <summary>
        /// Generate a JWT token based on input credentials.
        /// </summary>
        /// <param name="userModel">The input model (email and password)</param>
        /// <returns>A JWT token with a time-to-live of 10 hours.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/auth/login
        ///     {
        ///         "email": "example.com",
        ///         "password": "example"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created JWT token.</response>
        /// <response code="400">If there is any problem with the creation of the token (Invalid password, email, problems with the fields).</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(UserLoginDto userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user == null)
            {
                return BadRequest($"Could not find user with email ${userModel.Email}");
            }

            if (!await _userManager.CheckPasswordAsync(user, userModel.Password))
            {
                return BadRequest($"Invalid password for user {userModel.Email}.");
            }

            var signingCredentials = _tokenService.GetSigningCredentials();
            var claims = _tokenService.GetClaims(userModel);
            var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(token);
        }
    }
}