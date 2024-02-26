using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class GetListsTareasViewModel
{
    List<TareaViewModel> tareasPropias;
    List<TareaViewModel> tareasAsignadas;
    List<TareaViewModel> tareasNoAsignadas;
    public List<TareaViewModel> TareasAsignadas { get => tareasAsignadas; set => tareasAsignadas = value; }
    public List<TareaViewModel> TareasNoAsignadas { get => tareasNoAsignadas; set => tareasNoAsignadas = value; }
    public List<TareaViewModel> TareasPropias { get => tareasPropias; set => tareasPropias = value; }

    public GetListsTareasViewModel()
    {
    }

    public GetListsTareasViewModel(List<Tarea> tareasAsig, List<Tarea> tareasProp, List<Tarea> tareasNoAsig)
    {
        this.tareasAsignadas = new List<TareaViewModel>();
        this.tareasPropias = new List<TareaViewModel>();
        this.tareasNoAsignadas = new List<TareaViewModel>();
        
        foreach (var t in tareasAsig)
        {
            TareaViewModel tarea = new TareaViewModel(t);
            this.tareasAsignadas.Add(tarea);
        }
        foreach (var t in tareasProp)
        {
            TareaViewModel tarea = new TareaViewModel(t);
            this.tareasPropias.Add(tarea);
        }
        foreach (var t in tareasNoAsig)
        {
            TareaViewModel tarea = new TareaViewModel(t);
            this.tareasNoAsignadas.Add(tarea);
        }
    }
    
}