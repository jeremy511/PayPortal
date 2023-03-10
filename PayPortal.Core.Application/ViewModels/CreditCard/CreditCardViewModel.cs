using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Application.ViewModels.SavingsAccount
{
    public class CreditCardViewModel
    {
        public int Id { get; set; }
        public string Identifier { get; set; }

        public string OwnerUserName { get; set; }
    }
}
