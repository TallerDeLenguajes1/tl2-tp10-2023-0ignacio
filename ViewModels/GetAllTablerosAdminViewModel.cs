using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class GetAllTablerosAdminViewModel
{
    private List<TableroUsuarioViewModel> tableros;
    public List<TableroUsuarioViewModel> Tableros { get => tableros; set => tableros = value; }

    public GetAllTablerosAdminViewModel(List<TableroUsuarioViewModel> tableros)
    {
        this.tableros = new List<TableroUsuarioViewModel>();
        foreach (var tablero in tableros)
        {
            TableroUsuarioViewModel tableroNuevo = new TableroUsuarioViewModel(tablero);
            this.tableros.Add(tableroNuevo);
        }
    }

}