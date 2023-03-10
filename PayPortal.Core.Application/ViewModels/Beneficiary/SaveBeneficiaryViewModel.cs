
using System.ComponentModel.DataAnnotations;

namespace PayPortal.Core.Application.ViewModels.SavingsAccount
{
    public class SaveBeneficiaryViewModel
    {
        public string ? Id { get; set; }
        public string ? FullName { get; set; }
        public string ? UserId { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string AccountId { get; set; }

        public bool ?HasMessage { get; set; }

        public string? Message { get; set; }
    }
}
