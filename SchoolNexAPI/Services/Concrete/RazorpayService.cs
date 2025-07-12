using Razorpay.Api;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Services.Concrete
{
    public class RazorpayService : IRazorpayService
    {
        private readonly RazorpayClient _client;
        private readonly string _webhookSecret;


        public RazorpayService(IConfiguration config)
        {
            var key = config["Razorpay:Key"];
            var secret = config["Razorpay:Secret"];
            _client = new RazorpayClient(key, secret);
            _webhookSecret = config["Razorpay:WebhookSecret"] ?? secret;
        }

        public Order CreateOrder(decimal amount, string currency, string receiptId)
        {
            var options = new Dictionary<string, object>
            {
                ["amount"] = (int)(amount * 100),
                ["currency"] = currency,
                ["receipt"] = receiptId,
                ["payment_capture"] = 1
            };
            return _client.Order.Create(options);
        }

        public bool VerifySignature(string orderId, string paymentId, string signature)
        {
            var attributes = new Dictionary<string, string>
                {
                    { "razorpay_order_id", orderId },
                    { "razorpay_payment_id", paymentId },
                    { "razorpay_signature", signature }
                };

            try
            {
                Utils.ValidatePaymentSignature(attributes);
                return true;
            }
            catch (Razorpay.Api.Errors.SignatureVerificationError)
            {
                return false;
            }
        }

        public bool VerifyWebhookSignature(string payload, string razorpaySignature, string secret)
        {
            Utils.verifyWebhookSignature(payload, razorpaySignature, secret);
            return true;
        }
    }
}
