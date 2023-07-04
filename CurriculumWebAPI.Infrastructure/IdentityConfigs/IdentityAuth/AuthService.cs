using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Services;
using CurriculumWebAPI.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace CurriculumWebAPI.Infrastructure.IdentityConfiguration.IdentityAuth
{
    public class AuthService : ISignInManager
    {
        private readonly MyContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager ,MyContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Autentica e retorna um token caso o user exista
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException"></exception>
        public async Task<Token> AuthenticateAsync(User user)
        {
            var appUser = await _userManager.FindByEmailAsync(user.Email);
            var signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, user.Password, false);

            if (signInResult.Succeeded)
            {
                var userRoles = await _userManager.FindByEmailAsync(user.Email);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, appUser.UserName),
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                    new Claim(ClaimTypes.Email, appUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, appUser.Email.ToString())
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretService.Secret));

                var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new Token() { MyToken = new JwtSecurityTokenHandler()
                    .WriteToken(token), Expiration = token.ValidTo };
            }

            else throw new AuthenticationException("Falha na autenticação. Credenciais inválidas.");
        }


        public async Task<User> GetByEmail(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null) return null;

            return new User();
        }
    }
}