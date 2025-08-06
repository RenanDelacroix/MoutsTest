using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DeveloperStore.CrossCutting.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // MediatR
        services.AddSingleton<ServiceFactory>(sp => type => sp.GetRequiredService(type));
        services.AddSingleton<IMediator, Mediator>();

        services.Scan(scan => scan
            .FromAssemblies(Assembly.Load("DeveloperStore.Application"))
            .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );

        // AutoMapper
        services.AddSingleton<IMapper>(sp =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DeveloperStore.CrossCutting.Mappers.MappingProfile());
            });

            return config.CreateMapper();
        });

        return services;
    }
}
