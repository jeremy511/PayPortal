using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Core.Application.Dtos.Payment
{
    public class PaymentResponse
    {
        public string FullName { get; set; }
        public double PaymentAmount { get; set; }
        public int DestinaryAccount { get; set; }
        public int SenderAccount { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }
    }
}
