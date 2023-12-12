using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class GetAllTablerosViewModel
{
    List<TableroViewModel> tablerosVM;
    public List<TableroViewModel> TablerosVM { get => tablerosVM; set => tablerosVM = value; }

    public GetAllTablerosViewModel(List<Tablero> tableros)
    {
        tablerosVM = new List<TableroViewModel>();

        foreach (var tablero in tableros)
        {
            TableroViewModel tableroNuevo = new TableroViewModel(tablero);
            tablerosVM.Add(tableroNuevo);
        }
    }
}