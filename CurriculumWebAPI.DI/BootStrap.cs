using AutoMapper;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Services;
using CurriculumWebAPI.Infrastructure.Data.Context;
using CurriculumWebAPI.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using CurriculumWebAPI.Infrastructure.IdentityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using CurriculumWebAPI.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using CurriculumWebAPI.Infrastructure.IdentityConfiguration.IdentityAuth;
using CurriculumWebAPI.Infrastructure.IdentityConfigs;

namespace CurriculumWebAPI.DI
{
    public class BootStrap
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddIdentity<ApplicationUser, IdentityRole>(options => 
                options.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<MyContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                    ClockSkew = TimeSpan.Zero
                });
        

            services.AddScoped(typeof(IRepository<Curriculum>), typeof(CurriculumReporitory));
            services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
            services.AddScoped(typeof(IUserIdentity), typeof(UserIdentity));
            services.AddSingleton(typeof(TokenGenerator));
            services.AddTransient(typeof(CurriculumService));
            services.AddTransient(typeof(Mapper));
            services.AddTransient(typeof(UserService));


            SecretService.Secret = builder.Configuration.GetConnectionString("DefaultConnection");
        }
    }
}