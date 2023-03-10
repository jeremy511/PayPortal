using PayPortal.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Domain.Entities
{
    public class Transaction : AuditableBaseEntity
    {
        public string TransactionTo { get; set; }
        public string TransactionBy { get; set; }

        public double AmountOfMoney { get; set; }

        public int ProductsId { get; set; }

        public Products Products { get; set; }


    }
}
