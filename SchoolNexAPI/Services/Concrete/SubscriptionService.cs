using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Services.Concrete
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRazorpayService _rz;
        private readonly IConfiguration _cfg;
        private readonly AppDbContext _context;

        public SubscriptionService(AppDbContext context, IRazorpayService rz, IConfiguration cfg)
        {
            _context = context;
            _rz = rz;
            _cfg = cfg;
        }

        public async Task<SubscriptionPurchaseResponse> CreateSubscriptionOrderAsync(Guid schoolId, SubscriptionPurchaseRequest request)
        {
            var subscriptionType = await _context.SubscriptionType.FirstOrDefaultAsync(x => x.Id == request.SubscriptionTypeId);
            if (subscriptionType == null || !subscriptionType.IsActive)
            {
                throw new Exception("Invalid subscription type.");
            }

            decimal price = request.SubscriptionTerm == SubscriptionTerm.Yearly
                ? subscriptionType.PricePerMonth * 12 * 0.9m // Example: 10% discount for yearly
                : subscriptionType.PricePerMonth;

            // Here, you'd create a Razorpay order. For now, we just return a mock response.
            var paymentOrderId = Guid.NewGuid().ToString(); // Replace with Razorpay orderId in future

            // Save pending payment record (optional)
            await _context.PaymentTransactions.AddAsync(new PaymentTransactionModel
            {
                Id = Guid.NewGuid(),
                SchoolId = schoolId,
                SubscriptionTypeId = subscriptionType.Id,
                AmountPaid = price,
                PaymentGateway = "Razorpay",
                TransactionId = paymentOrderId,
                Status = "Pending",
                PaidOn = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            return new SubscriptionPurchaseResponse
            {
                PaymentOrderId = paymentOrderId,
                Amount = price,
                Currency = "INR"
            };
        }
        public async Task<IEnumerable<SubscriptionTypeModel>> GetPlansAsync()
    => await _context.SubscriptionType.Where(x => x.IsActive).ToListAsync();

        public async Task<SubscriptionPurchaseResponse> PurchaseAsync(Guid schoolId, SubscriptionPurchaseRequest req)
        {
            var plan = await _context.SubscriptionType.FindAsync(req.SubscriptionTypeId)
              ?? throw new Exception("Invalid plan.");
            decimal amount = req.SubscriptionTerm == SubscriptionTerm.Yearly
              ? plan.PricePerMonth * 12 * 0.9m
              : plan.PricePerMonth;
            var receipt = $"{schoolId}--{Guid.NewGuid()}";
            var order = _rz.CreateOrder(amount, "INR", receipt);

            await _context.PaymentTransactions.AddAsync(new PaymentTransactionModel
            {
                Id = Guid.NewGuid(),
                SchoolId = schoolId,
                SubscriptionTypeId = plan.Id,
                PaymentGateway = "Razorpay",
                TransactionId = order["id"].ToString(),
                Status = "Pending",
                AmountPaid = amount,
                SubscriptionTerm = req.SubscriptionTerm,
                CreatedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            return new SubscriptionPurchaseResponse
            {
                PaymentOrderId = order["id"].ToString(),
                Amount = amount,
                Currency = "INR"
            };
        }

        public async Task VerifyAsync(SubscriptionVerifyRequest req)
        {
            var tx = await _context.PaymentTransactions
              .FirstOrDefaultAsync(x => x.TransactionId == req.OrderId && x.Status == "Pending")
              ?? throw new Exception("Transaction not found");

            _rz.VerifySignature(req.OrderId, req.PaymentId, req.Signature);

            tx.Status = "Success";
            tx.PaymentId = req.PaymentId;
            tx.PaidOn = DateTime.UtcNow;

            var sub = await _context.SchoolSubscription
              .FirstOrDefaultAsync(x => x.SchoolId == tx.SchoolId);

            if (sub == null) throw new Exception("Subscription not found");

            sub.SubscriptionTypeId = tx.SubscriptionTypeId;
            sub.SubscriptionTerm = req.SubscriptionTerm;
            sub.SubscriptionExpiresOn = req.SubscriptionTerm switch
            {
                SubscriptionTerm.Monthly => DateTime.UtcNow.AddMonths(1),
                SubscriptionTerm.Yearly => DateTime.UtcNow.AddYears(1),
                _ => sub.SubscriptionExpiresOn
            };

            await _context.SaveChangesAsync();
        }
    }
}
