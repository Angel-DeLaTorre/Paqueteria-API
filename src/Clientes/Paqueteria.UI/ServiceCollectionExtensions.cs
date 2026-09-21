using Microsoft.Extensions.DependencyInjection;
using Radzen;

namespace Paqueteria.UI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomUiComponents(this IServiceCollection services)
    {
        services.AddRadzenComponents();
        return services;
    }
}