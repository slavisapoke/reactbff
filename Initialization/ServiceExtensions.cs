using CosmosDbRepository;
using Microsoft.Extensions.DependencyInjection;
using Ports;

namespace Initialization;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IFoodRepository, FoodRepository>();
    }
}
