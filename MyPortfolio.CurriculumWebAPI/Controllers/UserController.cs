using AutoMapper;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CurriculumWebAPI.App.Controllers
{

    [ApiController]
    [Route("V1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Mapper _mapper;
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(UserService userService, Mapper mapper
            UserManager<>)
        {
            _mapper = mapper;
            _userService = userService;
            _configuration = new ConfigurationManager();
        }


        [HttpPost]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInputModel userInfo)
        {
            await _userService.CreateUser(_mapper.Map<User>(userInfo));

            return null;
        }







        private UserToken BuildToken(UserInputModel userInfo)
        {
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("meuValor", "oque voce quiser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // tempo de expiração do token: 1 hora
            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
