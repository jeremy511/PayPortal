
using System.ComponentModel.DataAnnotations;

namespace PayPortal.Core.Application.ViewModels.Client
{
    public class SaveExpressPaymentViewModel
    {
        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string DestinaryAccount { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public string ? FromAccount {get; set;}

        public bool ? HasError { get; set; }
        public string Message { get; set; }
    }
}
