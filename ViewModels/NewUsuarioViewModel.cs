using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class NewUsuarioViewModel
{
    private string nombreDeUsuario;
    private string pass;
    private Roles rol;

    [Required(ErrorMessage = "Complete el campo")]
    [MaxLength(10, ErrorMessage = "El nombre debe tener hasta 10 caracteres")]
    [Display(Name = "Nombre de Usuario")]
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    
    [Required(ErrorMessage = "Complete el campo")]
    [StringLength(16, ErrorMessage = "La contraseña debe tener entre 8 y 16 caracteres"), MinLength(8)]
    [Display(Name = "Contraseña")]
    [PasswordPropertyText]
    public string Pass { get => pass; set => pass = value; }
    
    [Required(ErrorMessage = "Complete el campo")]
    [Display(Name = "Rol")]
    public Roles Rol { get => rol; set => rol = value; }
    public NewUsuarioViewModel()
    {
    }

    public NewUsuarioViewModel(Usuario usuarioNuevo)
    {
        this.nombreDeUsuario = usuarioNuevo.NombreDeUsuario;
        this.pass = usuarioNuevo.Pass;
        this.rol = usuarioNuevo.Rol;
    }

}