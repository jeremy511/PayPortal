using PayPortal.Core.Domain.Common;

namespace PayPortal.Core.Domain.Entities
{
    public class Beneficiary : AuditableBaseEntity
    {
        public string FullName { get; set; }
        public string UserId { get; set; }
        public string AccountId { get; set; }


    }
}
