using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tech_Invest_API.Application.Interfaces;
using Tech_Invest_API.Application.Mapper.Profiles;
using Tech_Invest_API.Application.Services;
using Tech_Invest_API.Domain.Interfaces;
using Tech_Invest_API.Domain.Utils.AuthorizationHandler;
using Tech_Invest_API.Infrastructure.Data.Data;
using Tech_Invest_API.Infrastructure.Data.Repository;

namespace Tech_Invest_API.Infrastructure.IoC;

public class DependencyContainer
{
    private static IServiceCollection _services;
    private static IConfiguration _configuration;

    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        _services = services;
        _configuration = configuration;

        RegisterEntityFramework();

        RegisterAutoMapper();

        RegisterAuthentication();

        RegisterInjections();
    }

    private static void RegisterEntityFramework()
    {
        // Entity Framework
        _services.AddEntityFrameworkSqlServer()
            .AddDbContext<TechInvestDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("DataBase"))
            );
    }

    private static void RegisterAutoMapper()
    {
        // Auto Mapper
        _services.AddSingleton(r =>
        {
            var mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile<UsuarioProfile>();
                mc.AddProfile<TipoInvestimentoProfile>();
            });

            return mapperConfiguration.CreateMapper();
        });
    }

    private static void RegisterAuthentication()
    {
        // Authentication
        _services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidAudience = _configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token inválido: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token inválido: " + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });

        _services.AddAuthorization(options =>
        {
            options.AddPolicy("UpdateUsuarioPolicy", policy =>
            {
                policy.Requirements.Add(new UpdateUsuarioRequirement());
            });
        });

        _services.AddSingleton<IAuthorizationHandler, UpdateUsuarioAuthorizationHandler>();

        _services.AddHttpContextAccessor();
    }

    private static void RegisterInjections()
    {
        // Application Layer
        _services.AddScoped<IUsuarioService, UsuarioService>();
        _services.AddScoped<ITipoInvestimentoService, TipoInvestimentoService>();

        // Data Layer
        _services.AddScoped<IUsuarioRepository, UsuarioRepository>(); 
        _services.AddScoped<ITipoInvestimentoRepository, TipoInvestimentoRepository>();
    }
}
