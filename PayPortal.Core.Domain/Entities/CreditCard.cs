using PayPortal.Core.Domain.Common;

namespace PayPortal.Core.Domain.Entities
{
    public class CreditCard : AuditableBaseEntity
    {
        public string Identifier { get; set; }
        public double Limit { get; set; }
        public double Owed {get; set;}
        public string OwnerName {get; set;}
        public string OwnerUserName { get; set;}
        public int ProductsId { get; set; }


        //Navegation Property
        public Products Products { get; set; }


    }
}
