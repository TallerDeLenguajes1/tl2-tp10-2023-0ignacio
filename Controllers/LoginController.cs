using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_0ignacio.Models;
using tl2_tp10_2023_0ignacio.Repositories;
using tl2_tp10_2023_0ignacio.ViewModels;
namespace tl2_tp10_2023_0ignacio.Controllers;

public class LoginController: Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Index(){
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(Usuario usuario){
        try
        {
            if(!ModelState.IsValid) return RedirectToAction("Index");
            Usuario usuarioLoggeado = _usuarioRepository.ValidateUsuario(usuario);
            
            if (usuarioLoggeado.NombreDeUsuario == null)
            {
                _logger.LogWarning("Intento de acceso invalido - Usuario: " + usuario.NombreDeUsuario + " - Clave ingresada: " + usuario.Pass);
                return RedirectToAction("Index");
            }else{
                loggearUsuario(usuarioLoggeado);
                _logger.LogInformation("El usuario " + usuario.NombreDeUsuario + " ingreso correctamente!");
                return RedirectToRoute(new { controller = "Home", action = "Index"});
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new { controller = "Login", action = "Index"});
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