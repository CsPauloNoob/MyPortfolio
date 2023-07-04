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
    public class UserController : ControllerBase
    {
        private readonly Mapper _mapper;
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(UserService userService, Mapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
            _configuration = new ConfigurationManager();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<TokenInputModel>> CreateUser([FromBody] UserInputModel userInfo)
        {
            await _userService.CreateUser(_mapper.Map<User>(userInfo));

            return null;
        }

        [HttpPost("{email}")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenInputModel>> SignIn([FromBody] UserInputModel userInfo)
        {
            var token = _mapper.Map<TokenInputModel>
                (await _userService.UserAuthenticate(_mapper.Map<User>(userInfo)));

            return token;
        }
    }
}