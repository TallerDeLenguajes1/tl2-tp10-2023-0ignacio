using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_0ignacio.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Complete el campo")]
        [Display(Name = "Nombre de usuario")]
        public string NombreDeUsuario {get; set;}

        [Required(ErrorMessage = "Complete el campo")]
        [Display(Name = "Contraseña")]
        [PasswordPropertyText]
        public string Pass {get; set;}
    }
}