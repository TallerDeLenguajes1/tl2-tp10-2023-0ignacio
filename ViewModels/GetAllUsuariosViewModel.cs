using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_0ignacio.Models;

namespace tl2_tp10_2023_0ignacio.ViewModels;

public class GetAllUsuariosViewModel
{
    private List<UsuarioViewModel> usuariosVM;
    public List<UsuarioViewModel> UsuariosVM { get => usuariosVM; set => usuariosVM = value; }
    
    public GetAllUsuariosViewModel(List<Usuario> usuarios)
    {
        usuariosVM = new List<UsuarioViewModel>();

        foreach (var usuario in usuarios)
        {
            UsuarioViewModel usuarioNuevo = new UsuarioViewModel(usuario);
            UsuariosVM.Add(usuarioNuevo);
        }
    }

}