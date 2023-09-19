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
        private readonly IUserIdentity _authManager;

        public UserService(IRepository<User> repository, IUserIdentity signInManager)
        {
            _reposity = repository;
            _authManager = signInManager;
        }

        public async Task<Token> UserAuthenticate(User userToAuth)
        {
            try
            {
                var userInDb = await _authManager.UserExists(userToAuth.Email);

                if (!userInDb) return new Token();

                var token = await _authManager.SignIn(userToAuth);

                return token;
            }

            catch(AuthenticationException authEx)
            {
                throw new AuthenticationException("Falha na autenticação. Credenciais inválidas.");
            }
        }


        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _reposity.GetByEmail(email);

            if (user is null)
                throw new NotFoundInDatabaseException("Falha no banco, ou usuário não existe");

            else return user;
        }


        public async Task<Token> CreateUser(User User)
        {
            User.Id = Guid.NewGuid().ToString();
            var exists = await _reposity.GetById(User.Id);

            if (exists is null)
            {
                var result = await _reposity.AddNew(User);

                if (result < 0)
                    throw new SaveFailedException("Falha ao salvar as alterações no banco de dados.");

                else return await _authManager.AuthNewUSer(User);
            }

            return new Token();
        }


        public async Task<bool> UpdateUser(User user)
        {
            var result = await _reposity.Update(user);

            return result;
        }
    }
}