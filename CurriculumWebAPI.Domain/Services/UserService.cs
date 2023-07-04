using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Services
{
    public class UserService
    {
        private readonly IRepository<User> _reposity;
        private readonly ISignInManager _signInManager;

        public UserService(IRepository<User> repository, ISignInManager signInManager)
        {
            _reposity = repository;
            _signInManager = signInManager;
        }

        public async Task<Token> UserAuthenticate(User userToAuth)
        {
            try
            {
                var userInDb = _signInManager.GetByEmail(userToAuth.UserName);

                if (userInDb is null) return null;

                var token = await _signInManager.AuthenticateAsync(userToAuth);

                return token;
            }

            catch(AuthenticationException authEx)
            {
                throw new AuthenticationException("Falha na autenticação. Credenciais inválidas.");
            }
        }


        public async Task<bool> CreateUser(User User)
        {
            var exists = await _reposity.GetById(User.Id);

            if(exists is null)
            {
                return await _reposity.AddNew(User);
            }

            return false;
        }
    }
}