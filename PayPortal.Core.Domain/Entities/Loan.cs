using PayPortal.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Domain.Entities
{
    public class Loan : AuditableBaseEntity
    {
        public string Identifier { get; set; }
        public double Amount { get; set; }
        public double Owed { get; set; }
        public string OwnerName { get; set; }
        public string OwnerUserName { get; set; }

        public int ProductsId { get; set; }

        //Navegation Property

        public Products Products { get; set; }

    }
}
