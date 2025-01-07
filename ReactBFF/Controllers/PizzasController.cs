using Microsoft.AspNetCore.Mvc;
using Model;
using Ports;

namespace ReactBFF.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzasController : ControllerBase
{
    private readonly ILogger<PizzasController> _logger;
    private readonly IConfiguration _configuration;

    public PizzasController(ILogger<PizzasController> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IEnumerable<Food>> Get([FromServices] IFoodRepository foodRepo)
    {
        _logger.LogInformation("Getting pizzas");

        var result = await foodRepo.GetFoodByType("pizza");

        _logger.LogInformation("Returning pizzasss: " + result.Count);

        return result;
    }

    [HttpGet("config")]
    public string GetConfig()
    {
        return _configuration["CosmosDb:ConnectionString"];
    }
}
