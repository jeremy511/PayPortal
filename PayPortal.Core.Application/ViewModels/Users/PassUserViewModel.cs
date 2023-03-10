
using System.ComponentModel.DataAnnotations;

namespace PayPortal.Core.Application.ViewModels.Users
{
    public class PassUserViewModel
    {
        public string Id { get; set; }


        [Required(ErrorMessage = "*Obligatorio*")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?#&])[A-Za-z\d$@$!%*#?&]{7,100}[^'\s]", ErrorMessage = "La contraseña debe tener minimo 8 digitos, 1 letra mayuscula, 1 caracter especial($@$!%*?.#&) y al menos 1 numero. Ejemplo Pass123#")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "*Obligatorio*")]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas deben coincidir.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
       
    }
}
