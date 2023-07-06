using CurriculumWebAPI.Domain.Exceptions;
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
        private readonly IUserIdentity _signInManager;

        public UserService(IRepository<User> repository, IUserIdentity signInManager)
        {
            _reposity = repository;
            _signInManager = signInManager;
        }

        public async Task<Token> UserAuthenticate(User userToAuth)
        {
            try
            {
                var userInDb = await _signInManager.UserExists(userToAuth.Email);

                if (!userInDb) return new Token();

                var token = await _signInManager.SignIn(userToAuth);

                return token;
            }

            catch(AuthenticationException authEx)
            {
                throw new AuthenticationException("Falha na autenticação. Credenciais inválidas.");
            }
        }


        public async Task<Token> CreateUser(User User)
        {

            var exists = await _reposity.GetById(User.Id);

            if (exists is null)
            {
                var result = await _reposity.AddNew(User);

                if (result < 0)
                    throw new SaveFailedException("Falha ao salvar as alterações no banco de dados.");

                else return await _signInManager.AuthNewUSer(User);
            }

            return new Token();
        }
    }
}