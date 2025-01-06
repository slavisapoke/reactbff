using Microsoft.AspNetCore.Mvc;

namespace ReactBFF.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzasController : ControllerBase
{
    private readonly ILogger<PizzasController> _logger;

    public PizzasController(ILogger<PizzasController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Pizza> Get()
    {
        List<Pizza> result = [
            new Pizza
            {
                Id = 1,
                Name = "Focaccia",
                Ingredients = "Bread with italian olive oil and rosemary",
                PhotoName = "pizzas/focaccia.jpg",
                Price = 6,
                SoldOut = false
            },
            new Pizza
            {
                Id = 2,
                Name = "Pizza Margherita",
                Ingredients = "Tomato and mozarella",
                PhotoName = "pizzas/margherita.jpg",
                Price = 10,
                SoldOut = false
            }];

        _logger.LogInformation("Returning pizzasss: " + result.Count);

        return result;
    }
}
