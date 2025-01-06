
namespace ReactBFF.Model;

public record CreatePaymentIntentRequest(string PaymentMethodType, string Currency);
