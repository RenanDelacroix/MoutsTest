using DeveloperStore.Application.Events;
using DeveloperStore.Domain.Interfaces;
using DeveloperStore.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace DeveloperStore.CrossCutting.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddSingleton<IEventPublisher, MongoEventPublisher>();

        // 1. Corrige o Guid
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;

        // 2. Registra os tipos
        if (!BsonClassMap.IsClassMapRegistered(typeof(SaleCreatedEvent)))
        {
            BsonClassMap.RegisterClassMap<SaleCreatedEvent>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
            });
        }

        // 3. Mongo client configurado corretamente
        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("MongoDb"));
            settings.GuidRepresentation = GuidRepresentation.Standard;
            return new MongoClient(settings);
        });

        return services;
    }

}
