
using System.ComponentModel.DataAnnotations;

namespace PayPortal.Core.Application.ViewModels.Client
{
    public class SaveBeneficiaryPaymentViewModel
    {
      
        public string BeneficiaryAcc { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public int Account {get; set;}

        public bool ? HasError { get; set; }
        public string Message { get; set; }
    }
}
