using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class UpdateUsuarioViewModel
{
    private int id;
    private string nombreDeUsuario;
    private string pass;
    private Roles rol;

    
    public int Id { get => id; set => id = value; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [MaxLength(10, ErrorMessage = "El nombre debe tener hasta 10 caracteres")]
    [Display(Name = "Nombre de Usuario")]
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [StringLength(16, ErrorMessage = "La contraseña debe tener entre 4 y 16 caracteres"), MinLength(4)]
    [Display(Name = "Contraseña")]
    public string Pass { get => pass; set => pass = value; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [Display(Name = "Rol")]
    public Roles Rol { get => rol; set => rol = value; }

    public UpdateUsuarioViewModel()
    {
    }

    public UpdateUsuarioViewModel(Usuario usuarioModificado)
    {
        id = usuarioModificado.Id;
        nombreDeUsuario = usuarioModificado.NombreDeUsuario;
        pass = usuarioModificado.Pass;
        rol = usuarioModificado.Rol;
    }
}