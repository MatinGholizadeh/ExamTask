using ExamTask.Application.Abstractions.Persistence.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExamTask.Application.DependencyInjection;

/// <summary>
/// Registers services for the Application layer, including MediatR, FluentValidation, etc.
/// </summary>
public static class ApplicationServiceRegistration
{
    /// <summary>
    /// Adds application-layer services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register MediatR handlers from this assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Register FluentValidation validators from this assembly
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}