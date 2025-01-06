using Newtonsoft.Json;

namespace ReactBFF.Model
{
    public class CreatePaymentIntentRequest
    {
        public string PaymentMethodType { get; set; }

        public string Currency { get; set; }
    }
}
