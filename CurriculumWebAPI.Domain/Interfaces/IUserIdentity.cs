using CurriculumWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Interfaces
{
    public  interface IUserIdentity
    {
        Task<Token> SignIn(User user);
        public Task<Token> AuthNewUSer(User user);
        public Task<bool> UserExists(string email);
    }
}
