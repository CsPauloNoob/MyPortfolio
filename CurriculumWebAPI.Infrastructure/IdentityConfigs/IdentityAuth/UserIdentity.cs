using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Services;
using CurriculumWebAPI.Infrastructure.Data.Context;
using CurriculumWebAPI.Infrastructure.IdentityConfigs.IdentityAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace CurriculumWebAPI.Infrastructure.IdentityConfiguration.IdentityAuth
{
    public class UserIdentity : IUserIdentity
    {
        private readonly MyContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenGenerator _tokenGenerator;

        public UserIdentity(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            MyContext context, TokenGenerator tokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }


        /// <summary>
        /// Autentica e retorna um token caso o user exista
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException"></exception>
        public async Task<Token> SignIn(User user)
        {
            var appUser = await _userManager.FindByEmailAsync(user.Email);
            var signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, user.Password, false);

            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.FindByEmailAsync(user.Email);

                return await _tokenGenerator.BuildToken(appUser);
            }

            else throw new AuthenticationException("Falha na autenticação. Credenciais inválidas.");
        }

        public async Task<Token> AuthNewUSer(User user)
        {
            var appUser = await _userManager.FindByEmailAsync(user.Email);

            return await _tokenGenerator.BuildToken(appUser);
        }


        public async Task<bool> UserExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) return false;

            return true;
        }
    }
}