using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class NewTareaOperadorViewModel
{
    private NewTareaViewModel tarea;
    private List<Tablero> tablerosPropios;
    public NewTareaViewModel Tarea { get => tarea; set => tarea = value; }
    public List<Tablero> TablerosPropios { get => tablerosPropios; set => tablerosPropios = value; }
    
    public NewTareaOperadorViewModel()
    {
    }

    public NewTareaOperadorViewModel(NewTareaViewModel tarea, List<Tablero> tablerosPropios)
    {
        this.tarea = tarea;
        this.tablerosPropios = tablerosPropios;
    }

}