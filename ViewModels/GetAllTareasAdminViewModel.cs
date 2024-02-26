using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class GetAllTareasAdminViewModel
{
    List<TareaUsuarioTableroViewModel> tareasVM;
    
    public List<TareaUsuarioTableroViewModel> TareasVM { get => tareasVM; set => tareasVM = value; }

    public GetAllTareasAdminViewModel()
    {
    }

    public GetAllTareasAdminViewModel(List<TareaUsuarioTableroViewModel> tareas)
    {
        this.tareasVM = new List<TareaUsuarioTableroViewModel>();
        
        
        foreach (var t in tareas)
        {
            TareaUsuarioTableroViewModel tarea = new TareaUsuarioTableroViewModel(t);
            this.tareasVM.Add(tarea);
        }
    }
}