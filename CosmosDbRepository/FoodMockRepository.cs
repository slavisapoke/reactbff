using Model;
using Ports;

namespace CosmosDbRepository;

public class FoodMockRepository : IFoodRepository
{
    public Task<List<Food>> GetFoodByType(string type)
    {
        return Task.FromResult<List<Food>>([
            new Food{
                Id = Guid.NewGuid(),
                Name = "Whatever pizza",
                Ingredients = "whatever ingredients",
                PhotoName = "pizzas/spinacijpg",
                Price = 100,
                SoldOut = false,
                Type = "Pizza"
            }
            ]);
    }

    public Task<Food?> Insert(Food item)
    {
        return Task.FromResult<Food?>(item);
    }
}
