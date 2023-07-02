using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Services
{
    public class UserService
    {
        private readonly IRepository<User> _reposity;

        public UserService(IRepository<User> repository)
        {
            _reposity = repository;
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