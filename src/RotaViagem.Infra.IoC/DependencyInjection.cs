using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RotaViagem.Application.Mapping;
using RotaViagem.Application.Services;
using RotaViagem.Application.Services.Interfaces;
using RotaViagem.Domain.Interfaces.Repositories;
using RotaViagem.Infra.Data.Context;
using RotaViagem.Infra.Data.Repositories;

namespace RotaViagem.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection ResolveDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => {
            options.UseSqlite(configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });

        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));


        services.AddScoped<IRotaRepository, RotaRepository>();
        services.AddScoped<IRotaService, RotaService>();


        return services;
    }
}
