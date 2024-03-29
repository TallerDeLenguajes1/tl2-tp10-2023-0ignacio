using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class NewTableroViewModel
{
    private int idUsuarioPropietario;
    private string nombre;
    private string desc;
    private List<Usuario> usuarios;

    [Required(ErrorMessage = "Complete el campo")]
    [Display(Name = "Id Usuario Propietario")]
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    
    [Required(ErrorMessage = "Complete el campo")]
    [MaxLength(10, ErrorMessage = "El nombre debe tener hasta 10 caracteres")]
    [Display(Name = "Nombre")]
    public string Nombre { get => nombre; set => nombre = value; }

    [Required(ErrorMessage = "Complete el campo")]
    [MaxLength(50, ErrorMessage = "La descripcion debe tener hasta 50 caracteres")]
    [Display(Name = "Descripcion")]
    public string Desc { get => desc; set => desc = value; }
    
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

    public NewTableroViewModel()
    {
    }

    public NewTableroViewModel(TableroViewModel tablero, List<Usuario> usuarios)
    {
        this.usuarios = usuarios;
        this.idUsuarioPropietario = tablero.IdUsuarioPropietario;
        this.nombre = tablero.Nombre;
        this.desc = tablero.Desc;
    }

}