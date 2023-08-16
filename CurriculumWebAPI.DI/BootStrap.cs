using AutoMapper;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Services;
using CurriculumWebAPI.Infrastructure.Data.Context;
using CurriculumWebAPI.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using CurriculumWebAPI.Infrastructure.IdentityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using CurriculumWebAPI.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using CurriculumWebAPI.Infrastructure.IdentityConfiguration.IdentityAuth;
using CurriculumWebAPI.Infrastructure.IdentityConfigs.IdentityAuth;
using CurriculumWebAPI.Infrastructure.PDF;
using CurriculumWebAPI.Domain.Models.CurriculumBody;

namespace CurriculumWebAPI.DI
{
    public class BootStrap
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var services = builder.Services;
            SecretService.Secret = builder.Configuration.GetValue<string>("JWT:Key");

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MyContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretService.Secret)),
                    ClockSkew = TimeSpan.Zero
                });

            #region DI region

            services.AddScoped(typeof(IRepository<Curriculum>), typeof(CurriculumReporitory));
            services.AddScoped(typeof(IRepositoryForCollections<Formacao>), typeof(FormacaoRepository));
            services.AddScoped(typeof(IRepositoryForCollections<Habilidades>), typeof(HabilidadeRepository));
            services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
            services.AddScoped(typeof(IRepository<Contato>), typeof(ContatoRepository));
            services.AddScoped(typeof(IUserIdentity), typeof(UserIdentity));
            services.AddScoped(typeof(IPdfGenerator), typeof(PdfGenerator));

            services.AddSingleton(typeof(TokenGenerator));
            services.AddSingleton(typeof(PdfGenerator));
            services.AddTransient(typeof(UserService));
            services.AddTransient(typeof(CurriculumService));
            services.AddTransient(typeof(FormacaoService));
            services.AddTransient(typeof(HabilidadeService));
            services.AddTransient(typeof(PdfService));

            services.AddTransient(typeof(Mapper));

            #endregion
        }
    }
}