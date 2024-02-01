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
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        // Entity Framework
        services.AddEntityFrameworkSqlServer()
            .AddDbContext<TechInvestDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DataBase"))
            );

        // Auto Mapper
        services.AddSingleton(r =>
        {
            var mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile<UsuarioProfile>();
            });

            return mapperConfiguration.CreateMapper();
        });

        // Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
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
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("UpdateUsuarioPolicy", policy =>
            {
                policy.Requirements.Add(new UpdateUsuarioRequirement());
            });
        });

        services.AddSingleton<IAuthorizationHandler, UpdateUsuarioAuthorizationHandler>();

        // Application Layer
        services.AddScoped<IUsuarioService, UsuarioService>();

        // Data Layer
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        services.AddHttpContextAccessor();
    }
}
