using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class NewTableroViewModel
{
    private TableroRestriccionesViewModel tablero;
    private List<Usuario> usuarios;
    public TableroRestriccionesViewModel Tablero { get => tablero; set => tablero = value; }
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

    public NewTableroViewModel()
    {
    }

    public NewTableroViewModel(TableroRestriccionesViewModel tablero, List<Usuario> usuarios)
    {
        this.usuarios = usuarios;
        this.tablero = tablero;
    }

}