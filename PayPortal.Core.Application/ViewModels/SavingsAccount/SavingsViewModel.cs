using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Application.ViewModels.SavingsAccount
{
    public class SavingsViewModel
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string OwnerUserName { get; set; }
        public double Amount { get; set; }
        public int IsMain { get; set; }
        public string OwnerName { get; set; }
    }
}
