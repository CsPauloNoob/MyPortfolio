using AutoMapper;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Services;
using CurriculumWebAPI.Infrastructure.Data.Context;
using CurriculumWebAPI.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using CurriculumWebAPI.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using CurriculumWebAPI.Infrastructure.IdentityConfigs.IdentityAuth;
using CurriculumWebAPI.Infrastructure.PDF;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Infrastructure.IdentityConfigs;
using WkHtmlToPdfDotNet.Contracts;
using WkHtmlToPdfDotNet;

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
            
            services.AddTransient(typeof(IRepository<Curriculum>), typeof(CurriculumReporitory));
            services.AddTransient(typeof(IRepositoryForCollections<Formacao>), typeof(FormacaoRepository));
            services.AddTransient(typeof(IRepositoryForCollections<Habilidades>), typeof(HabilidadeRepository));
            services.AddTransient(typeof(IRepositoryForCollections<Cursos_Extras>), typeof(CursosExtrasRepository));
            services.AddTransient(typeof(IRepositoryForCollections<Experiencia_Profissional>), typeof(ExperienciaProfissionalRepository));
            services.AddTransient(typeof(IRepository<User>), typeof(UserRepository));
            services.AddTransient(typeof(IRepository<Contato>), typeof(ContatoRepository));
            services.AddSingleton(typeof(IPdfGenerator), typeof(PdfGenerator));
            services.AddTransient(typeof(IUserIdentity), typeof(UserIdentity));
            //services.AddTransient(typeof(IPdfGenerator), typeof(PdfGenerator));

            services.AddSingleton(typeof(TokenGenerator));
            services.AddTransient(typeof(UserService));
            services.AddTransient(typeof(CurriculumService));
            services.AddTransient(typeof(FormacaoService));
            services.AddTransient(typeof(HabilidadeService));
            services.AddTransient(typeof(CursosExtrasService));
            services.AddTransient(typeof(ExperienciaProfissionalService));
            services.AddTransient(typeof(PdfService));

            services.AddTransient(typeof(Mapper));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            #endregion

            var _services = services.BuildServiceProvider();

            var dbcontext = _services.GetService<MyContext>();
            dbcontext.Database.EnsureCreated();
        }
    }
}