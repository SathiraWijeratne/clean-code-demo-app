using System;
using Application.DependencyInjection;
using Domain.Repositories;
using Infrastructure.Repositories;
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
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string? connectionString = null)
    {
        // Register application services
        services.AddApplicationServices();

        var dbConnectionString = connectionString ?? "Data Source=users.db";

        // Register repository with connection string
        services.AddScoped<IUserRepository>(provider => new UserRepository(dbConnectionString));

        // Register your infrastructure services here
        return services;
    }
}
