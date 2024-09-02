using CurriculumWebAPI.App.MapperConfig;
using CurriculumWebAPI.DI;
using CurriculumWebAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CurriculumWebAPI.Domain.Models;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using CurriculumWebAPI.App;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson( 
    options =>
    options.SerializerSettings.ReferenceLoopHandling = 
    Newtonsoft.Json.ReferenceLoopHandling.Ignore);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});


builder.Services.AddDbContext<MyContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


//Configuração do CORS
builder.Services.AddCors(
    opt => opt.AddPolicy(
        Configuration.CorsPolicyName,
        policy =>
        policy.WithOrigins
        (   new string[]
        {   Configuration.DevEnv_BackendUrl,
            Configuration.DevEnv_FrontendUrl,
            Configuration.ProdEnv_FrontendUrl
        })
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials())
    );

BootStrap.Configure(builder);

builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Configuration.CorsPolicyName);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();