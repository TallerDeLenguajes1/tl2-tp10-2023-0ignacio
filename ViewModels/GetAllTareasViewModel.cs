using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class GetAllTareasViewModel
{
    List<TareaViewModel> tareasVM;
    
    public List<TareaViewModel> TareasVM { get => tareasVM; set => tareasVM = value; }

    public GetAllTareasViewModel()
    {
    }

    public GetAllTareasViewModel(List<Tarea> tareas)
    {
        this.tareasVM = new List<TareaViewModel>();
        
        
        foreach (var t in tareas)
        {
            TareaViewModel tarea = new TareaViewModel(t);
            this.tareasVM.Add(tarea);
        }
    }
}