using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public string ? CreatedBy { get; set; }
        public DateTime ? CreatedDate { get; set; }
        public string ? LastModifiedBy { get; set; }
        public DateTime ? LastModified { get; set; }
    }
}
