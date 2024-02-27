using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class UpdateEstadoTareaViewModel
{
    private int id;
    private int idTablero;
    private string nombre;
    private EstadoTarea estado;
    private string desc;
    private string color;
    private int? idUsuarioAsignado;
    

    public int Id { get => id; set => id = value; }

    public int IdTablero { get => idTablero; set => idTablero = value; }

    public string Nombre { get => nombre; set => nombre = value; }

    [Required(ErrorMessage = "Este campo no puede estar vacio")]
    [Display(Name = "Estado Tarea")]
    public EstadoTarea Estado { get => estado; set => estado = value; }

    public string Desc { get => desc; set => desc = value; }

    public string Color { get => color; set => color = value; }

    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
   

    public UpdateEstadoTareaViewModel()
    {
    }

    public UpdateEstadoTareaViewModel(Tarea tarea)
    {
        this.id = tarea.Id;
        this.idTablero = tarea.IdTablero;
        this.nombre = tarea.Nombre;
        this.Estado = tarea.Estado;
        this.color = tarea.Color;
        this.IdUsuarioAsignado = tarea.IdUsuarioAsignado;
    }
}