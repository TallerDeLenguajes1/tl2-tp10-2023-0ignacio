using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class GetAllTablerosViewModel
{
    private List<TableroViewModel> tablerosPropios;
    private List<TableroViewModel> tablerosAgenos;
    public List<TableroViewModel> TablerosPropios { get => tablerosPropios; set => tablerosPropios = value; }
    public List<TableroViewModel> TablerosAgenos { get => tablerosAgenos; set => tablerosAgenos = value; }

    public GetAllTablerosViewModel(List<Tablero> tablerosProp, List<Tablero> tablerosAgen)
    {
        tablerosPropios = new List<TableroViewModel>();
        tablerosAgenos = new List<TableroViewModel>();

        foreach (var tablero in tablerosProp)
        {
            TableroViewModel tableroNuevo = new TableroViewModel(tablero);
            tablerosPropios.Add(tableroNuevo);
        }
        foreach (var tablero in tablerosAgen)
        {
            TableroViewModel tableroNuevo = new TableroViewModel(tablero);
            tablerosAgenos.Add(tableroNuevo);
        }
    }
}