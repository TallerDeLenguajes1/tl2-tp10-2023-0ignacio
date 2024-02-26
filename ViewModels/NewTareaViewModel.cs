using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class NewTareaViewModel
{
    private TareaRestriccionesViewModel tarea;
    private List<Tablero> tablerosPropios;
    private List<Usuario> usuarios;
    public TareaRestriccionesViewModel Tarea { get => tarea; set => tarea = value; }
    public List<Tablero> TablerosPropios { get => tablerosPropios; set => tablerosPropios = value; }
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

    public NewTareaViewModel()
    {
    }

    public NewTareaViewModel(TareaRestriccionesViewModel tarea, List<Tablero> tablerosPropios, List<Usuario> usuarios)
    {
        this.usuarios = usuarios;
        this.tarea = tarea;
        this.tablerosPropios = tablerosPropios;
    }

}