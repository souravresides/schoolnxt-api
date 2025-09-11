namespace SchoolNexAPI.Models
{
    public class PaymentMethodModel
    {
        public Guid Id { get; set; }
        public string MethodName { get; set; }
        public int Value { get; set; }
    }

    public class PaymentStatusModel
    {
        public Guid Id { get; set; }
        public string StatusName { get; set; }
        public int Value { get; set; }
    }
}
