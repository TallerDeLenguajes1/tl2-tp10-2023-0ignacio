using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class UpdateTareaViewModel
{
    private int id;
    private int idTablero;
    private string nombre;
    private EstadoTarea estado;
    private string desc;
    private string color;
    private int? idUsuarioAsignado;
    private List<Usuario> usuarios;

    public int Id { get => id; set => id = value; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [Display(Name = "Id Tablero")]
    public int IdTablero { get => idTablero; set => idTablero = value; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [MaxLength(15, ErrorMessage = "El nombre debe tener hasta 15 caracteres")]
    [Display(Name = "Nombre")]
    public string Nombre { get => nombre; set => nombre = value; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [Display(Name = "Estado Tarea")]
    public EstadoTarea Estado { get => estado; set => estado = value; }

    [MaxLength(50, ErrorMessage = "La descripcion debe tener hasta 50 caracteres")]
    [Display(Name = "Descripcion")]
    public string Desc { get => desc; set => desc = value; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [Display(Name = "Color")]
    public string Color { get => color; set => color = value; }

    [Display(Name = "Id Usuario Asignado")]
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

    public UpdateTareaViewModel()
    {
    }

    public UpdateTareaViewModel(Tarea tarea, List<Usuario> usuarios)
    {
        this.id = tarea.Id;
        this.idTablero = tarea.IdTablero;
        this.nombre = tarea.Nombre;
        this.Estado = tarea.Estado;
        this.color = tarea.Color;
        this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        this.usuarios = usuarios;
    }
}