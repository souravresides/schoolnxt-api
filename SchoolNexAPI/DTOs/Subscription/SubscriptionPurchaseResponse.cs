namespace SchoolNexAPI.DTOs.Subscription
{
    public class SubscriptionPurchaseResponse
    {
        public string PaymentOrderId { get; set; } // e.g., Razorpay Order Id
        public string PaymentGateway { get; set; } = "Razorpay";
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "INR";
    }

}
