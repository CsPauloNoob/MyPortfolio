using AutoMapper;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Authorization;
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
    public class AuthenticateUserController : ControllerBase
    {
        private readonly Mapper _mapper;
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public AuthenticateUserController(UserService userService, Mapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
            _configuration = new ConfigurationManager();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> CreateUser([FromBody] UserInputModel userInfo)
        {
            userInfo.Id = Guid.NewGuid();

            var token = await _userService.CreateUser(_mapper.Map<User>(userInfo));

            return CreatedAtAction(nameof(CreateUser),new
            {
                authToken = token.MyToken,
                user = userInfo
            });
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> SignIn([FromBody] UserInputModel userInfo)
        {
            var token = await _userService.UserAuthenticate(_mapper.Map<User>(userInfo));

            if (token.MyToken is null) return NotFound("Usuário não encontrado");

            return Ok(new
            {
                authToken = token.MyToken,
            });
        }
    }
}