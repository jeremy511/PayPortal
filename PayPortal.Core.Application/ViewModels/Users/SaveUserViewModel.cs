
using System.ComponentModel.DataAnnotations;

namespace PayPortal.Core.Application.ViewModels.Users
{
    public class SaveUserViewModel
    {
        public string ? Id { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [RegularExpression(@"^\d{3}-\d{7}\-\d{1}$", ErrorMessage = "Cedula no valido Ejemplo de Cedula xxx-xxxxxxx-x")]
        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string IdCard { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?#&])[A-Za-z\d$@$!%*#?&]{7,100}[^'\s]", ErrorMessage = "La contraseña debe tener minimo 8 digitos, 1 letra mayuscula, 1 caracter especial($@$!%*?.#&) y al menos 1 numero. Ejemplo Pass123#")]
        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "*Obligado*")]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas deben coincidir.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^\+[1-1]\([8-8]\d{1}[9-9]\)-\d{3}\-\d{4}$", ErrorMessage = "Telefono no valido Ejemplo de telefono +1(8x9)-xxx-xxxx")]
        [Required(ErrorMessage = "*Obligado*")]
        [DataType(DataType.PhoneNumber)]
        public string Tel { get; set; }

        
        public string UserType { get; set; }

        [DataType(DataType.Currency)]
        public double? StartingAmount { get; set; }
        public bool  HasError { get; set; }
        public string? Error { get; set; }
    }
}
