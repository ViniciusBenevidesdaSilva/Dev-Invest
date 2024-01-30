using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tech_Invest_API.Application.Interfaces;
using Tech_Invest_API.Application.Mapper.Profiles;
using Tech_Invest_API.Application.Services;
using Tech_Invest_API.Domain.Interfaces;
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

        // Application Layer
        services.AddScoped<IUsuarioService, UsuarioService>();

        // Data Layer
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}
