using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

/// <summary>
/// ServiceContainer class for managing dependency injection in the Application layer.
/// </summary>
public static class ServiceContainer
{
    /// <summary>
    /// Adds application services to the specified IServiceColalection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The updated IServiceCollection.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())); // automatically registers all MediatR handlers

        return services;
    }
}
