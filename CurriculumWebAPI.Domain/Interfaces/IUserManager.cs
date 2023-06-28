using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Interfaces
{
    public interface IUserManager
    {
        Task<bool> CreateUserAsync(string username, string password);
    }
}