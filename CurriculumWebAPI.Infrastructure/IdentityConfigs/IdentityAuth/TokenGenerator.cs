using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CurriculumWebAPI.Infrastructure.IdentityConfigs.IdentityAuth
{
    public class TokenGenerator
    {

        public async Task<Token> BuildToken(ApplicationUser user)
        {

            var authClaims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, user.Email.ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretService.Secret));
            var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: credentials
            );

            return new Token()
            {
                MyToken = new JwtSecurityTokenHandler()
                .WriteToken(token),
                Expiration = token.ValidTo
            };

            /*var claims = new[]
{
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretService.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // tempo de expiração do token: 3 hora
            var expiration = DateTime.UtcNow.AddHours(3);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new Token()
            {
                MyToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };*/
        }
    }
}