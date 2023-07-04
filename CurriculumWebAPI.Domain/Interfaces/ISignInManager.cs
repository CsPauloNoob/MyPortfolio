using CurriculumWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Interfaces
{
    public  interface ISignInManager
    {
        Task<Token> AuthenticateAsync(User user);
        public Task<User> GetByEmail(string email);
    }
}
