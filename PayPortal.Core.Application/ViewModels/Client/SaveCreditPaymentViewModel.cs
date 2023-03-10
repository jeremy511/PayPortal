
using System.ComponentModel.DataAnnotations;

namespace PayPortal.Core.Application.ViewModels.Client
{
    public class SaveLoanPaymentViewModel
    {
       
        public int Loan { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public int FromAccount {get; set;}

        public bool ? HasError { get; set; }
        public string Message { get; set; }
    }
}
