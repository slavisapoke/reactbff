using Model;

namespace Ports;

public interface IFoodRepository
{
    Task<List<Food>> GetFoodByType(string type);
    Task<Food?> Insert(Food item);
}
