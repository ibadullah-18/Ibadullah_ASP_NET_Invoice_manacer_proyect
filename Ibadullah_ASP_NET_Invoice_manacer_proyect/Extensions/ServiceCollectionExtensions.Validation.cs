using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Extensions;

public static class ValidationServiceExtension
{
    public static IServiceCollection AddFluentValidationConfig(
        this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}