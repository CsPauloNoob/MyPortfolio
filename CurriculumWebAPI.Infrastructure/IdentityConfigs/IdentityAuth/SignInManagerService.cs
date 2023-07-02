using CurriculumWebAPI.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Infrastructure.IdentityConfiguration.IdentityAuth
{
    public class SignInManagerService : ISignInManager
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SignInManagerService(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            return true;
        }
    }
}