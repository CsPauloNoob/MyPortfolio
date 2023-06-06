using CurriculumWebAPI.Domain;
using CurriculumWebAPI.Domain.Services;
using CurriculumWebAPI.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MyPortfolio.CurriculumWebAPI.Models;

namespace CurriculumWebAPI.DI
{
    public class BootStrap
    {
        public static void Configure(IServiceCollection services)
        {

            services.AddTransient(typeof(IRepository<Curriculum>), typeof(CurriculumReporitory));
            services.AddTransient(typeof(CurriculumService));
        }
    }
}