using CosmosDbRepository;
using Microsoft.Extensions.DependencyInjection;
using Ports;

namespace Initialization;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
#if DEBUG
        services.AddScoped<IFoodRepository, FoodMockRepository>();
#else
        services.AddScoped<IFoodRepository, FoodRepository>();
#endif
    }
}
