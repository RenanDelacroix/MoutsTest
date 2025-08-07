using DeveloperStore.Application.Events;
using DeveloperStore.Domain.Interfaces;
using DeveloperStore.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DeveloperStore.CrossCutting.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaleRepository, SaleRepository>();

        services.AddSingleton<IEventPublisher, MongoEventPublisher>();

        services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(configuration.GetConnectionString("MongoDb")));

        return services;
    }

}
