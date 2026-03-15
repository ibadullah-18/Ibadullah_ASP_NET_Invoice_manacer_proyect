using Ibadullah_ASP_NET_Invoice_manacer_proyect.Services;
using Ibadullah_ASP_NET_Invoice_manacer_proyect.Services.Interfaces;

namespace Ibadullah_ASP_NET_Invoice_manacer_proyect.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IInvoiceDocumentService, InvoiceDocumentService>();

        return services;
    }
}