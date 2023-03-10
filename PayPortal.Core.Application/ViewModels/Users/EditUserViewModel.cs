
using System.ComponentModel.DataAnnotations;

namespace PayPortal.Core.Application.ViewModels.Users
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string IdCard { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        public string UserType { get; set; }

        [DataType(DataType.Currency)]
        public double? AditionalAmount { get; set; }
        public string? Pass { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
       
    }
}
