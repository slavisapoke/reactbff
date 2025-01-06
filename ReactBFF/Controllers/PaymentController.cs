using Microsoft.AspNetCore.Mvc;
using ReactBFF.Model;
using Stripe;

namespace ReactBFF.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<PaymentController> _logger;
    private readonly StripeClient _stripeClient;

    public PaymentController(
        IConfiguration configuration,
        ILogger<PaymentController> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _stripeClient = new StripeClient(configuration["StripeSecretKey"]);
    }

    [HttpPost("create-intent")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentRequest request)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = 500,
            Currency = request.Currency
        };

        var service = new PaymentIntentService(_stripeClient);
        var paymentIntent = await service.CreateAsync(options);
        return Ok(new { clientSecret = paymentIntent.ClientSecret });
    }
}
