using AutoMapper;
using CurriculumWebAPI.Domain;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Services;
using CurriculumWebAPI.Infrastructure.Data.Context;
using CurriculumWebAPI.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CurriculumWebAPI.DI
{
    public class BootStrap
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<Curriculum>), typeof(CurriculumReporitory));
            services.AddTransient(typeof(CurriculumService));
            services.AddTransient(typeof(Mapper));
        }
    }
}