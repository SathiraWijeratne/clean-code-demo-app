using System;
using Application.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

/// <summary>
/// ServiceContainer class for managing dependency injection in the Infrastructure layer.
/// </summary>
public static class ServiceContainer
{
    /// <summary>
    /// Adds infrastructure services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The updated IServiceCollection.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Register application services
        services.AddApplicationServices();

        // Register your infrastructure services here
        return services;
    }
}
