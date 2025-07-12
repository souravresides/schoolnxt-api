using Razorpay.Api;

namespace SchoolNexAPI.Services.Abstract
{
    public interface IRazorpayService
    {
        Order CreateOrder(decimal amount, string currency, string receiptId);
        bool VerifySignature(string orderId, string paymentId, string signature);
        bool VerifyWebhookSignature(string payload, string razorpaySignature, string secret);
    }
}
