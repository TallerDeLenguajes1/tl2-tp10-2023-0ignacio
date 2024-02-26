using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class TareaUsuarioTableroViewModel
{
    private int id;
    private int idTablero;
    private string nombre;
    private EstadoTarea estado;
    private string desc;
    private string color;
    private int? idUsuarioAsignado;
    private string nombreTablero;
    private string nombreUsuarioAsignado;

    public int Id { get => id; set => id = value; }
    public int IdTablero { get => idTablero; set => idTablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public string Desc { get => desc; set => desc = value; }
    public string Color { get => color; set => color = value; }
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    public string NombreTablero { get => nombreTablero; set => nombreTablero = value; }
    public string NombreUsuarioAsignado { get => nombreUsuarioAsignado; set => nombreUsuarioAsignado = value; }

    public TareaUsuarioTableroViewModel()
    {
    }

    public TareaUsuarioTableroViewModel(int id,int idTablero, int? idUsuarioAsignado, string nombre, EstadoTarea estado, string desc, string color, string nombreTablero, string nombreUsuarioAsignado)
    {
        this.id = id;
        this.idTablero = idTablero;
        this.nombre = nombre;
        this.Estado = estado;
        this.Desc = desc;
        this.color = color;
        this.IdUsuarioAsignado = idUsuarioAsignado;
        this.nombreTablero = nombreTablero;
        this.nombreUsuarioAsignado = nombreUsuarioAsignado;
    }

    public TareaUsuarioTableroViewModel(TareaUsuarioTableroViewModel tarea)
        {
            id = tarea.id;
            idTablero = tarea.idTablero;
            nombre = tarea.nombre;
            estado = tarea.estado;
            desc = tarea.desc;
            color = tarea.color;
            idUsuarioAsignado = tarea.idUsuarioAsignado;
            nombreTablero = tarea.nombreTablero;
            nombreUsuarioAsignado = tarea.nombreUsuarioAsignado;
        }

}