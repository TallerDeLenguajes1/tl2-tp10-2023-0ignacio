using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class TableroUsuarioViewModel
{
    private int idTablero;
    private int idUsuarioPropietario;
    private string nombreTablero;
    private string descTablero;
    private string nombreDeUsuarioPropietario;

    public TableroUsuarioViewModel()
    {
    }

    public TableroUsuarioViewModel(TableroUsuarioViewModel tablero)
    {
        this.idTablero = tablero.idTablero;
        this.idUsuarioPropietario = tablero.idUsuarioPropietario;
        this.nombreTablero = tablero.nombreTablero;
        this.descTablero = tablero.descTablero;
        this.nombreDeUsuarioPropietario = tablero.nombreDeUsuarioPropietario;
    }
    
    public TableroUsuarioViewModel(int idTablero, int idUsuarioPropietario, string nombreTablero, string descTablero, string nombreDeUsuarioPropietario)
    {
        this.idTablero = idTablero;
        this.idUsuarioPropietario = idUsuarioPropietario;
        this.nombreTablero = nombreTablero;
        this.descTablero = descTablero;
        this.nombreDeUsuarioPropietario = nombreDeUsuarioPropietario;
    }

    public int IdTablero { get => idTablero; set => idTablero = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string NombreTablero { get => nombreTablero; set => nombreTablero = value; }
    public string DescTablero { get => descTablero; set => descTablero = value; }
    public string NombreDeUsuarioPropietario { get => nombreDeUsuarioPropietario; set => nombreDeUsuarioPropietario = value; }
}