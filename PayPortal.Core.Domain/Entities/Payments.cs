using PayPortal.Core.Domain.Common;


namespace PayPortal.Core.Domain.Entities
{
    public class Payments : AuditableBaseEntity
    {
        public string PaymentTo { get; set; }
        public string PaymentBy { get; set; }

        public double AmountOfMoney { get; set; }

        public int ProductsId { get; set; }

        public Products Products { get; set; }
    }
}
