using DeveloperStore.Domain.Interfaces;
using DeveloperStore.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperStore.CrossCutting.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ISaleRepository, SaleRepository>();
        return services;
    }
}
