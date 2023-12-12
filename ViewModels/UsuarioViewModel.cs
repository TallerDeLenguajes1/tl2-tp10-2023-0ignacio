using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class UsuarioViewModel
{
    private int id;
    private string nombreDeUsuario;
    private Roles rol;

    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public Roles Rol { get => rol; set => rol = value; }

    public UsuarioViewModel()
    {
    }
    
    public UsuarioViewModel(Usuario usuario)
    {
        this.Id = usuario.Id;
        this.NombreDeUsuario = usuario.NombreDeUsuario;
        this.Rol = usuario.Rol;
    }
}