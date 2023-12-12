using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_0ignacio.Models;
using tl2_tp10_2023_0ignacio.Repositories;
using tl2_tp10_2023_0ignacio.ViewModels;
namespace tl2_tp10_2023_0ignacio.Controllers;

public class LoginController: Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository usuarioRepository;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    [HttpGet]
    public IActionResult Index(){
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(Usuario usuario){
        if(!ModelState.IsValid) return RedirectToAction("Index");
        List<Usuario> usuarios = usuarioRepository.GetAll();
        Usuario usuarioLoggeado = usuarios.FirstOrDefault(u => u.NombreDeUsuario == usuario.NombreDeUsuario && u.Pass == usuario.Pass);
        
        if (usuarioLoggeado == null)
        {
            return RedirectToAction("Index");
        }else{
            loggearUsuario(usuarioLoggeado);
            return RedirectToRoute(new { controller = "Home", action = "Index"});
        }
    }

    private void loggearUsuario(Usuario usuario){
        HttpContext.Session.SetString("NombreDeUsuario", usuario.NombreDeUsuario);
        HttpContext.Session.SetString("Rol", usuario.Rol.ToString());
        HttpContext.Session.SetString("Id", usuario.Id.ToString());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}