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

    

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [Display(Name = "Id")]
    public int Id { get => id; set => id = value; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [MaxLength(10, ErrorMessage = "El nombre debe tener hasta 10 caracteres")]
    [Display(Name = "Nombre de Usuario")]
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [Range(8, 16, ErrorMessage = "La contreseña debe tener entre 8 y 16 caracteres")]
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