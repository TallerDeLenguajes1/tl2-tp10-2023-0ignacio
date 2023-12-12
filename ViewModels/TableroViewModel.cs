using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

public class TableroViewModel
{
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string desc;

    public TableroViewModel()
    {
    }

    public TableroViewModel(Tablero tablero)
    {
        this.id = tablero.Id;
        this.idUsuarioPropietario = tablero.IdUsuarioPropietario;
        this.nombre = tablero.Nombre;
        this.desc = tablero.Desc;
    }

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Desc { get => desc; set => desc = value; }
}