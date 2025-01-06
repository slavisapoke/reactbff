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
        _logger.LogInformation(request.ToString());
        var options = new PaymentIntentCreateOptions
        {
            Amount = 500,
            Currency = request.Currency
        };

        var service = new PaymentIntentService(_stripeClient);
        var paymentIntent = await service.CreateAsync(options);
        return Ok(new { clientSecret = paymentIntent.ClientSecret });
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> PaymentWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        string endpointSecret = _configuration["StripeWebhookSecret"]!;
        try
        {
            var stripeEvent = EventUtility.ParseEvent(json);
            var signatureHeader = Request.Headers["Stripe-Signature"];

            stripeEvent = EventUtility.ConstructEvent(json,
                    signatureHeader, endpointSecret);
            //TODO handle by type...
            _logger.LogInformation($"Logged event: {json}");
            return Ok();
        }
        catch (StripeException e)
        {
            _logger.LogError(e, "Failed to handle stripe event: " + e.Message);
            return BadRequest();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "General error handling stripe event: " + e.Message);
            return StatusCode(500);
        }
    }
}
