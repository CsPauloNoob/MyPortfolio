using CurriculumWebAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Infrastructure.IdentityConfiguration.IdentityAuth
{
    public class UserManagerService : IUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserManagerService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateUserAsync(string email, string password)
        {
            var user = new ApplicationUser();
            user.Email = email;

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }
    }
}