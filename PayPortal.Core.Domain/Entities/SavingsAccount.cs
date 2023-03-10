
using PayPortal.Core.Domain.Common;

namespace PayPortal.Core.Domain.Entities
{
    public class SavingsAccount : AuditableBaseEntity
    {
        public string Identifier { get; set; }
        public double Amount { get; set; }
        public int IsMain { get; set; }
        public string OwnerName { get; set; }

        public string OwnerUserName { get; set; }

        public int ProductsId { get; set; }

        //Navegation Property

        public Products Products { get; set; }
    }
}
