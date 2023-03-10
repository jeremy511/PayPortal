using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Application.ViewModels.SavingsAccount
{
    public class BeneficiaryViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
        public string AccountId { get; set; }
    }
}
