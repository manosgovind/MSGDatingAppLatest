using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingAppLatest.API.Data;
using DatingAppLatest.API.DTOs;
using DatingAppLatest.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace DatingAppLatest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository authRepo, IConfiguration config)
        {
            _config = config;
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDTO userDto)
        {
            // do validation here
            userDto.UserName = userDto.UserName.ToLower();
            if (await _authRepo.IsUserExists(userDto.UserName))
                return BadRequest("Username already exists.");

            var userToCreate = new User
            {
                UserName = userDto.UserName
            };

            var createdUser = _authRepo.Register(userToCreate, userDto.Password);
            // return CreatedAtRoute
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userLoginDTO)
        {
            var userFromRepo = await _authRepo.Login(userLoginDTO.UserName, userLoginDTO.Password);

            if (userFromRepo == null)
                return Unauthorized();

            // if user passed the login process successfully, we need to create a token for acessing the resource
            // token based auth part

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier , userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_config.GetSection("AppSettings:Token").Value.ToString()));

            // signing credentials -  neeed a key and security algorithm to hash the key 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);



            var tokenDescriptor1 = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(new TimeSpan(1, 0, 0, 0)),
                SigningCredentials = creds

            };


            var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken()
            // just a comment to check the git status
        }

    }
}